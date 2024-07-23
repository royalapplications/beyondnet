using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.C;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

using Settings = Beyond.NET.CodeGenerator.Generator.C.Settings;

namespace Beyond.NET.CodeGenerator.Syntax.C;

public class CTypeSyntaxWriter: ICSyntaxWriter, ITypeSyntaxWriter
{
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, ICSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new CConstructorSyntaxWriter() },
        { MemberTypes.Property, new CPropertySyntaxWriter() },
        { MemberTypes.Method, new CMethodSyntaxWriter() },
        { MemberTypes.Field, new CFieldSyntaxWriter() },
        { MemberTypes.Event, new CEventSyntaxWriter() }
    };
    
    private CDestructorSyntaxWriter m_destructorSyntaxWriter = new();
    private CTypeOfSyntaxWriter m_typeOfSyntaxWriter = new();
    
    public CTypeSyntaxWriter(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((Type)@object, state, configuration);
    }

    public string Write(Type type, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        // Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        if (type.IsPointer ||
            type.IsByRef ||
            type.IsNullableValueType(out _)) {
            // No need to generate C code for those kinds of types

            return string.Empty;
        }

        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            return Builder.SingleLineComment($"Type \"{type.Name}\" was skipped. Reason: It has no full name.")
                .ToString();
        }
        
        string cTypeName = type.CTypeName();

        CCodeBuilder sb = new();

        if (type.IsEnum) {
            string enumdefCode = WriteEnumDef(
                type,
                cTypeName,
                typeDescriptorRegistry
            );

            sb.AppendLine(enumdefCode);
        } else if (type.IsDelegate()) {
            var delegateInvokeMethod = type.GetDelegateInvokeMethod();

            string delegateTypedefCode = WriteDelegateTypeDefs(
                type,
                delegateInvokeMethod
            );
    
            sb.AppendLine(delegateTypedefCode);
        } else {
            string typedefCode = WriteTypeDef(cTypeName);
            
            sb.AppendLine(typedefCode);
        }
        
        return sb.ToString();
    }

    private string WriteTypeDef(string cTypeName)
    {
        return Builder.TypeAliasTypeDef($"{cTypeName}_t", "void*")
            .ToString();
    }

    private string WriteDelegateTypeDefs(
        Type delegateType,
        MethodInfo? delegateInvokeMethod
    )
    {
        var nullabilityContext = new NullabilityInfoContext();
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        string? fullTypeName = delegateType.FullName;

        if (fullTypeName == null) {
            return Builder.SingleLineComment($"Type \"{delegateType.Name}\" was skipped. Reason: It has no full name.")
                .ToString();
        }
        
        string cTypeName = delegateType.CTypeName();
        
        Type returnType = delegateInvokeMethod?.ReturnType ?? typeof(void);
        var parameterInfos = delegateInvokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>();
        
        if (returnType.IsByRef) {
            return Builder.SingleLineComment($"TODO: ({cTypeName}) Unsupported delegate type. Reason: Has by ref return type")
                .ToString();
        }
        
        foreach (var parameter in parameterInfos) {
            if (parameter.IsOut) {
                return Builder.SingleLineComment($"TODO: ({cTypeName}) Unsupported delegate type. Reason: Has out parameters")
                    .ToString();
            }
            
            if (parameter.IsIn) {
                return Builder.SingleLineComment($"TODO: ({cTypeName}) Unsupported delegate type. Reason: Has in parameters")
                    .ToString();
            }

            if (!ExperimentalFeatureFlags.EnableByRefParametersInDelegates) {
                Type parameterType = parameter.ParameterType;
                
                if (parameterType.IsByRef) {
                    return Builder.SingleLineComment($"TODO: ({cTypeName}) Unsupported delegate type. Reason: Has by ref parameters")
                        .ToString();
                }
            }
        }

        CCodeBuilder sb = new();
        
        sb.AppendLine(WriteTypeDef(cTypeName));

        string contextTypeName = "void*";
        string cFunctionTypeName = $"{cTypeName}_CFunction_t";
        string cDestructorFunctionTypeName = $"{cTypeName}_CDestructorFunction_t";
        
        sb.AppendLine($"typedef void (*{cDestructorFunctionTypeName})({contextTypeName} context);");
        sb.AppendLine();

        string cReturnTypeName;

        if (returnType.IsVoid()) {
            cReturnTypeName = "void";
        } else {
            TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
            cReturnTypeName = returnTypeDescriptor.GetTypeName(CodeLanguage.C, true);            
        }

        sb.AppendLine($"typedef {cReturnTypeName} (*{cFunctionTypeName})(");

        List<string> parameters = new();

        foreach (var parameter in parameterInfos) {
            string parameterName = parameter.Name ?? throw new Exception("Delegate parameter has no name");
        
            Type parameterType = parameter.ParameterType;
            
            bool isOutParameter = parameter.IsOut;
            bool isInParameter = parameter.IsIn;
                
            bool isByRefParameter = parameterType.IsByRef;

            if (isByRefParameter) {
                parameterType = parameterType.GetNonByRefType();
            }
            
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
            
            bool isNotNull = false;

            if (parameterType.IsReferenceType()) {
                var parameterNullabilityInfo = nullabilityContext.Create(parameter);

                if (parameterNullabilityInfo.ReadState == parameterNullabilityInfo.WriteState) {
                    isNotNull = parameterNullabilityInfo.ReadState == NullabilityState.NotNull;
                }
            }

            Nullability parameterNullability = isNotNull
                ? Nullability.NonNullable
                : Nullability.NotSpecified;

            string parameterTypeName = parameterTypeDescriptor.GetTypeName(
                CodeLanguage.C,
                true,
                parameterNullability,
                Nullability.NotSpecified,
                isOutParameter,
                isByRefParameter,
                isInParameter
            );
                
            parameters.Add($"{parameterTypeName} {parameterName}");
        }

        string parametersString = string.Join(",\n", parameters);

        string contextParameter = $"{contextTypeName} context";

        if (!string.IsNullOrEmpty(parametersString)) {
            parametersString = contextParameter + ",\n" + parametersString;
        } else {
            parametersString = contextParameter;
        }
    
        sb.Append(parametersString
            .IndentAllLines(1));

        sb.AppendLine();
        sb.AppendLine(");");

        string delegateTypeDefCode = sb.ToString();

        return delegateTypeDefCode;
    }

    private string WriteEnumDef(
        Type type,
        string cTypeName,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        CCodeBuilder sb = new();

        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);

        Type underlyingType = type.GetEnumUnderlyingType();
        TypeDescriptor underlyingTypeDescriptor = underlyingType.GetTypeDescriptor(typeDescriptorRegistry);

        string underlyingTypeName = underlyingTypeDescriptor.GetTypeName(CodeLanguage.C, false);
        
        bool isFlagsEnum = type.IsDefined(typeof(FlagsAttribute), false);

        List<string> clangAttributes = new() {
            "__attribute__((enum_extensibility(open)))"
        };

        if (isFlagsEnum) {
            clangAttributes.Add("__attribute__((flag_enum))");
        }

        string clangAttributesString = string.Join(' ', clangAttributes);

        sb.AppendLine($"typedef enum {clangAttributesString}: {underlyingTypeName} {{");

        var caseNames = type.GetEnumNames();
        var values = type.GetEnumValuesAsUnderlyingType() ?? throw new Exception("No enum values");

        if (caseNames.Length != values.Length) {
            throw new Exception("The number of case names in an enum must match the number of values");
        }

        List<string> enumCases = new();

        for (int i = 0; i < caseNames.Length; i++) {
            string caseName = caseNames[i];
            var value = values.GetValue(i) ?? throw new Exception("No enum value for case");
            
            enumCases.Add($"\t{cTypeName}_{caseName} = {value.ToString()}");
        }

        string enumCasesString = string.Join(",\n", enumCases);

        sb.AppendLine(enumCasesString);

        string cEnumTypeName = typeDescriptor.GetTypeName(CodeLanguage.C, false);
        
        sb.AppendLine($"}} {cEnumTypeName};");
        
        return sb.ToString();
    }

    public string WriteMembers(Type type, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        if (type.IsPointer ||
            type.IsByRef ||
            type.IsGenericParameter ||
            type.IsGenericMethodParameter ||
            type.IsGenericTypeParameter ||
            type.IsConstructedGenericType) {
            // No need to generate C code for those kinds of types

            return string.Empty;
        }
        
        var cSharpMembers = cSharpUnmanagedResult.GeneratedTypes[type];

        CCodeBuilder sb = new();

        string fullTypeName = type.GetFullNameOrName();

        bool isDelegate = type.IsDelegate();

        sb.AppendLine(
            Builder.PragmaMark()
                .Separator()
                .Comment($"BEGIN APIs of {fullTypeName}")
                .ToString()
        );

        if (isDelegate) {
            TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
            string cTypeName =  typeDescriptor.GetTypeName(CodeLanguage.C, false);
            string cMemberNamePrefix = type.CTypeName();
            
            WriteDelegateTypeMembers(
                typeDescriptor,
                fullTypeName,
                cTypeName,
                cMemberNamePrefix,
                sb,
                state,
                typeDescriptorRegistry
            );
        }

        HashSet<MemberInfo> generatedMembers = new();

        foreach (var cSharpMember in cSharpMembers) {
            var member = cSharpMember.Member;

            if (member is not null &&
                generatedMembers.Contains(member)) {
                continue;
            }
            
            var memberKind = cSharpMember.MemberKind;
            var memberType = member?.MemberType;

            ICSyntaxWriter? syntaxWriter = GetSyntaxWriter(
                memberKind,
                memberType ?? MemberTypes.Custom
            );
            
            if (syntaxWriter == null) {
                if (Settings.EmitUnsupported) {
                    sb.AppendLine(Builder.SingleLineComment($"TODO: Unsupported Member Type \"{memberType}\"").ToString());
                }
                    
                continue;
            }

            object? target;

            if (syntaxWriter is IDestructorSyntaxWriter) {
                target = type;
            } else if (syntaxWriter is ITypeOfSyntaxWriter) {
                target = type;
            } else {
                target = member;
            }

            if (target == null) {
                throw new Exception("No target");
            }

            string memberCode = syntaxWriter.Write(target, state, configuration);

            sb.AppendLine(memberCode);

            if (member is not null) {
                generatedMembers.Add(member);
            }
        }

        sb.AppendLine(
            Builder.PragmaMark()
                .Separator()
                .Comment($"END APIs of {fullTypeName}")
                .ToString()
        );

        return sb.ToString();
    }

    private void WriteDelegateTypeMembers(
        TypeDescriptor typeDescriptor,
        string fullTypeName,
        string cTypeName,
        string cMemberNamePrefix,
        CCodeBuilder sb,
        State state,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        var invokeMethod = typeDescriptor.ManagedType.GetDelegateInvokeMethod();

        WriteDelegateTypeMembers(
            typeDescriptor,
            fullTypeName,
            cTypeName,
            cMemberNamePrefix,
            invokeMethod,
            sb,
            state,
            typeDescriptorRegistry
        );
    }

    private void WriteDelegateTypeMembers(
        TypeDescriptor typeDescriptor,
        string fullTypeName,
        string cTypeName,
        string cMemberNamePrefix,
        MethodInfo? invokeMethod,
        CCodeBuilder sb,
        State state,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        var parameterInfos = invokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>();
        var returnType = invokeMethod?.ReturnType ?? typeof(void);
        
        WriteDelegateTypeMembers(
            typeDescriptor,
            fullTypeName,
            cTypeName,
            cMemberNamePrefix,
            parameterInfos,
            returnType,
            sb,
            state,
            typeDescriptorRegistry
        );
    }
    
    private void WriteDelegateTypeMembers(
        TypeDescriptor typeDescriptor,
        string fullTypeName,
        string cTypeName,
        string cMemberNamePrefix,
        ParameterInfo[] parameterInfos,
        Type returnType,
        CCodeBuilder sb,
        State state,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        // TODO: Generics
        
        Type type = typeDescriptor.ManagedType;
        TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);

        if (returnType.IsByRef) {
            sb.AppendLine(Builder.SingleLineComment($"TODO: ({cTypeName}) Unsupported delegate type. Reason: Has by ref return type").ToString());

            return;
        }
        
        foreach (var parameter in parameterInfos) {
            if (parameter.IsOut) {
                sb.AppendLine(Builder.SingleLineComment($"TODO: ({cTypeName}) Unsupported delegate type. Reason: Has out parameters").ToString());
                
                return;
            }
            
            if (parameter.IsIn) {
                sb.AppendLine(Builder.SingleLineComment($"TODO: ({cTypeName}) Unsupported delegate type. Reason: Has in parameters").ToString());
                
                return;
            }

            Type parameterType = parameter.ParameterType;

            if (!ExperimentalFeatureFlags.EnableByRefParametersInDelegates) {
                if (parameterType.IsByRef) {
                    sb.AppendLine(Builder.SingleLineComment($"TODO: ({cTypeName}) Unsupported delegate type. Reason: Has by ref parameters").ToString());
                    
                    return;
                }
            }
            
            if (parameterType.IsGenericParameter ||
                parameterType.IsGenericMethodParameter) {
                sb.AppendLine(Builder.SingleLineComment($"TODO: ({cTypeName}) Unsupported delegate type. Reason: Has generic parameters").ToString());
                
                return;
            }
        }
        
        string contextType = "const void*";
        string functionType = $"{cMemberNamePrefix}_CFunction_t";
        string destrutorFunctionType = $"{cMemberNamePrefix}_CDestructorFunction_t";

        #region Create
        var nonNullAttr = Nullability.NonNullable.GetClangAttribute();
        
        sb.AppendLine($"{cTypeName} {nonNullAttr} /* {fullTypeName} */");
        sb.AppendLine($"{cMemberNamePrefix}_Create(");
        sb.AppendLine($"\t{contextType} context,");
        sb.AppendLine($"\t{functionType} function,");
        sb.AppendLine($"\t{destrutorFunctionType} destructorFunction");
        sb.AppendLine(");");
        #endregion Create

        sb.AppendLine();

        #region Invoke
        string returnTypeName = returnType.IsVoid()
            ? "void"
            : returnTypeDescriptor.GetTypeName(CodeLanguage.C, true);

        string parameters = CMethodSyntaxWriter.WriteParameters(
            MemberKind.Automatic,
            null,
            Nullability.NotSpecified,
            false,
            true,
            type,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            typeDescriptorRegistry
        );
        
        if (!string.IsNullOrEmpty(parameters)) {
            parameters = ", " + parameters;
        }

        parameters += ", System_Exception_t* /* System.Exception */ outException";
        
        sb.AppendLine($"{returnTypeName}");
        sb.AppendLine($"{cMemberNamePrefix}_Invoke(");
        sb.AppendLine($"\t{cTypeName} /* {fullTypeName} */ self{parameters}");
        sb.AppendLine(");");
        #endregion Invoke
        
        sb.AppendLine();
        
        #region Context Get
        sb.AppendLine($"{contextType}");
        sb.AppendLine($"{cMemberNamePrefix}_Context_Get(");
        sb.AppendLine($"\t{cTypeName} /* {fullTypeName} */ self");
        sb.AppendLine(");");
        #endregion Context Get

        sb.AppendLine();
        
        #region CFunction Get
        sb.AppendLine($"{functionType}");
        sb.AppendLine($"{cMemberNamePrefix}_CFunction_Get(");
        sb.AppendLine($"\t{cTypeName} /* {fullTypeName} */ self");
        sb.AppendLine(");");
        #endregion CFunction Get
        
        sb.AppendLine();

        #region CDestructorFunction Get
        sb.AppendLine($"{destrutorFunctionType}");
        sb.AppendLine($"{cMemberNamePrefix}_CDestructorFunction_Get(");
        sb.AppendLine($"\t{cTypeName} /* {fullTypeName} */ self");
        sb.AppendLine(");");
        #endregion CDestructorFunction Get

        sb.AppendLine();

        // TODO: Add to State
    }

    private ICSyntaxWriter? GetSyntaxWriter(
        MemberKind memberKind,
        MemberTypes memberType
    )
    {
        if (memberKind == MemberKind.Destructor) {
            return m_destructorSyntaxWriter;
        } else if (memberKind == MemberKind.TypeOf) {
            return m_typeOfSyntaxWriter;
        }

        m_syntaxWriters.TryGetValue(
            memberType,
            out ICSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
}