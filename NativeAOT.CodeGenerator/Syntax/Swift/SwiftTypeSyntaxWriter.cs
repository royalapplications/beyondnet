using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;

using Settings = NativeAOT.CodeGenerator.Generator.Swift.Settings;

namespace NativeAOT.CodeGenerator.Syntax.Swift;

public class SwiftTypeSyntaxWriter: ISwiftSyntaxWriter, ITypeSyntaxWriter
{
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, ISwiftSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new SwiftConstructorSyntaxWriter() },
        { MemberTypes.Property, new SwiftPropertySyntaxWriter() },
        { MemberTypes.Method, new SwiftMethodSyntaxWriter() },
        { MemberTypes.Field, new SwiftFieldSyntaxWriter() },
        { MemberTypes.Event, new SwiftEventSyntaxWriter() }
    };
    
    private SwiftDestructorSyntaxWriter m_destructorSyntaxWriter = new();
    private SwiftTypeOfSyntaxWriter m_typeOfSyntaxWriter = new();
    
    public SwiftTypeSyntaxWriter(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }

    public string Write(Type type, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        if (type.IsPrimitive ||
            type.IsPointer ||
            type.IsByRef) {
            // No need to generate Swift code for those kinds of types

            return string.Empty;
        }

        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            return $"// Type \"{type.Name}\" was skipped. Reason: It has no full name.";
        }
        
        string cTypeName = type.CTypeName();

        StringBuilder sb = new();

        bool writeMembers = !type.IsEnum;
        bool writeTypeDefinition = true;

        if (type.IsEnum) {
            string enumdefCode = WriteEnumDef(
                type,
                cTypeName,
                typeDescriptorRegistry
            );

            sb.AppendLine(enumdefCode);
        } else if (type.IsDelegate()) {
            writeTypeDefinition = false;
            
            var delegateInvokeMethod = type.GetDelegateInvokeMethod();

            string delegateTypedefCode = WriteDelegateTypeDefs(
                type,
                delegateInvokeMethod
            );
    
            sb.AppendLine(delegateTypedefCode);
            
            // TODO: Delegates are not yet supported
            writeMembers = false;
        }

        if (writeMembers) {
            string membersCode = WriteMembers(
                type,
                state,
                writeTypeDefinition
            );
                
            sb.AppendLine(membersCode);
        }
        
        return sb.ToString();
    }

    // TODO
    private string WriteDelegateTypeDefs(
        Type delegateType,
        MethodInfo? delegateInvokeMethod
    )
    {
        return $"// TODO: Delegate Type Defition ({delegateType.GetFullNameOrName()})";
        
        // TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        //
        // string? fullTypeName = delegateType.FullName;
        //
        // if (fullTypeName == null) {
        //     return $"// Type \"{delegateType.Name}\" was skipped. Reason: It has no full name.";
        // }
        //
        // string cTypeName = delegateType.CTypeName();
        //
        // Type returnType = delegateInvokeMethod?.ReturnType ?? typeof(void);
        // var parameterInfos = delegateInvokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>();
        //
        // if (returnType.IsByRef) {
        //     return $"// TODO: ({cTypeName}) Unsupported delegate type. Reason: Has by ref return type";
        // }
        //
        // foreach (var parameter in parameterInfos) {
        //     if (parameter.IsOut ||
        //         parameter.ParameterType.IsByRef) {
        //         return $"// TODO: ({cTypeName}) Unsupported delegate type. Reason: Has by ref or out parameters";
        //     }
        // }
        //
        // StringBuilder sb = new();
        //
        // sb.AppendLine(WriteTypeDef(cTypeName));
        //
        // string contextTypeName = "void*";
        // string cFunctionTypeName = $"{cTypeName}_CFunction_t";
        // string cDestructorFunctionTypeName = $"{cTypeName}_CDestructorFunction_t";
        //
        // sb.AppendLine($"typedef void (*{cDestructorFunctionTypeName})({contextTypeName} context);");
        // sb.AppendLine();
        //
        // string cReturnTypeName;
        //
        // if (returnType.IsVoid()) {
        //     cReturnTypeName = "void";
        // } else {
        //     TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
        //     cReturnTypeName = returnTypeDescriptor.GetTypeName(CodeLanguage.C, true);            
        // }
        //
        // sb.AppendLine($"typedef {cReturnTypeName} (*{cFunctionTypeName})(");
        //
        // List<string> parameters = new();
        //
        // foreach (var parameter in parameterInfos) {
        //     string parameterName = parameter.Name ?? throw new Exception("Delegate parameter has no name");
        //
        //     Type parameterType = parameter.ParameterType;
        //     TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
        //
        //     string parameterTypeName = parameterTypeDescriptor.GetTypeName(CodeLanguage.C, true);
        //
        //     parameters.Add($"{parameterTypeName} {parameterName}");
        // }
        //
        // string parametersString = string.Join(",\n", parameters);
        //
        // string contextParameter = $"{contextTypeName} context";
        //
        // if (!string.IsNullOrEmpty(parametersString)) {
        //     parametersString = contextParameter + ",\n" + parametersString;
        // } else {
        //     parametersString = contextParameter + "\n";
        // }
        //
        // sb.Append(parametersString
        //     .IndentAllLines(1));
        //
        // sb.AppendLine(");");
        //
        // string delegateTypeDefCode = sb.ToString();
        //
        // return delegateTypeDefCode;
    }

    private string WriteEnumDef(
        Type type,
        string cTypeName,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        StringBuilder sb = new();

        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);

        Type underlyingType = type.GetEnumUnderlyingType();
        TypeDescriptor underlyingTypeDescriptor = underlyingType.GetTypeDescriptor(typeDescriptorRegistry);

        string underlyingSwiftTypeName = underlyingTypeDescriptor.GetTypeName(CodeLanguage.Swift, false);
        string cEnumTypeName = typeDescriptor.GetTypeName(CodeLanguage.C, false);
        string swiftEnumTypeName = typeDescriptor.GetTypeName(CodeLanguage.Swift, false);
        
        bool isFlagsEnum = type.IsDefined(typeof(FlagsAttribute), false);

        if (isFlagsEnum) {
            sb.AppendLine($"public struct {swiftEnumTypeName}: OptionSet {{");

            sb.AppendLine($"\tpublic typealias RawValue = {underlyingSwiftTypeName}");
            sb.AppendLine("\tpublic let rawValue: RawValue");
            sb.AppendLine();
            sb.AppendLine("\tpublic init(rawValue: RawValue) {");
            sb.AppendLine("\t\tself.rawValue = rawValue");
            sb.AppendLine("\t}");
            sb.AppendLine();
        } else {
            sb.AppendLine($"public enum {swiftEnumTypeName}: {underlyingSwiftTypeName} {{");
        }

        string initUnwrap = isFlagsEnum 
            ? string.Empty
            : "!";
        
        sb.AppendLine($"\tinit(cValue: {cEnumTypeName}) {{");
        sb.AppendLine($"\t\tself.init(rawValue: cValue.rawValue){initUnwrap}");
        sb.AppendLine("\t}");

        sb.AppendLine();
        sb.AppendLine($"\tvar cValue: {cEnumTypeName} {{ {cEnumTypeName}(rawValue: rawValue){initUnwrap} }}");
        sb.AppendLine();
        
        var caseNames = type.GetEnumNames();
        var values = type.GetEnumValuesAsUnderlyingType() ?? throw new Exception("No enum values");

        if (caseNames.Length != values.Length) {
            throw new Exception("The number of case names in an enum must match the number of values");
        }

        Dictionary<object, string> valueToNameMapping = new();

        for (int i = 0; i < values.Length; i++) {
            object value = values.GetValue(i) ?? throw new ArgumentNullException(nameof(value));
            string name = caseNames[i];

            if (valueToNameMapping.ContainsKey(value)) {
                continue;
            }

            valueToNameMapping[value] = name;
        }

        Dictionary<string, string> duplicateCases = new();
        List<object> uniqueValues = new();

        for (int i = 0; i < values.Length; i++) {
            object value = values.GetValue(i) ?? throw new ArgumentNullException(nameof(value));
            string name = caseNames[i];

            if (uniqueValues.Contains(value)) {
                string originalName = valueToNameMapping[value];

                duplicateCases[name] = originalName;
            } else {
                uniqueValues.Add(value);
            }
        }

        List<string> enumCases = new();

        for (int i = 0; i < caseNames.Length; i++) {
            string caseName = caseNames[i];
            string swiftCaseName = caseName.ToSwiftEnumCaseName();

            var value = values.GetValue(i) ?? throw new Exception("No enum value for case");

            string caseCode;

            if (isFlagsEnum) {
                string caseCodeDecl = $"public static let {swiftCaseName} = {swiftEnumTypeName}";
                
                if (value.Equals(0)) {
                    caseCode = $"{caseCodeDecl}([])";
                } else {
                    caseCode = $"{caseCodeDecl}(rawValue: {value})";
                }
            } else {
                if (duplicateCases.TryGetValue(caseName, out string? caseNameWithEquivalentValue)) {
                    string swiftCaseNameWithEquivalentValue = caseNameWithEquivalentValue.ToSwiftEnumCaseName();
                    
                    caseCode = $"public static let {swiftCaseName} = {swiftEnumTypeName}.{swiftCaseNameWithEquivalentValue}";
                } else {
                    caseCode = $"case {swiftCaseName} = {value}";
                }
            }
            
            enumCases.Add($"\t{caseCode}");
        }

        string enumCasesString = string.Join('\n', enumCases);

        sb.AppendLine(enumCasesString);
        sb.AppendLine("}");
        
        return sb.ToString();
    }

    // TODO
    public string WriteMembers(
        Type type,
        State state,
        bool writeTypeDefinition
    )
    {
        // return $"// TODO: Members ({type.GetFullNameOrName()})";
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        if (type.IsPrimitive ||
            type.IsPointer ||
            type.IsByRef ||
            type.IsGenericParameter ||
            type.IsGenericMethodParameter ||
            type.IsGenericTypeParameter ||
            type.IsConstructedGenericType) {
            // No need to generate Swift code for those kinds of types

            return string.Empty;
        }

        bool isAbstract = type.IsAbstract;
        
        var cSharpMembers = cSharpUnmanagedResult.GeneratedTypes[type];
        var cMembers = cResult.GeneratedTypes[type];

        StringBuilder sb = new();

        string typeName = type.Name;
        string fullTypeName = type.GetFullNameOrName();
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
        string cTypeName = typeDescriptor.GetTypeName(CodeLanguage.C, false);
        string swiftTypeName =  typeDescriptor.GetTypeName(CodeLanguage.Swift, false);
        
        bool isDelegate = type.IsDelegate();

        if (writeTypeDefinition) {
            Type? baseType = type.BaseType;
            TypeDescriptor? baseTypeDescriptor = baseType?.GetTypeDescriptor(typeDescriptorRegistry);

            string swiftBaseTypeName = baseTypeDescriptor?.GetTypeName(CodeLanguage.Swift, false)
                                       ?? "DNObject";
            
            sb.AppendLine($"public class {swiftTypeName} /* {fullTypeName} */: {swiftBaseTypeName} {{");
            sb.AppendLine($"public override class var typeName: String {{ \"{typeName}\" }}");
            sb.AppendLine($"public override class var fullTypeName: String {{ \"{fullTypeName}\" }}");
        }

        // if (isDelegate) {
        //     string cTypeName =  typeDescriptor.GetTypeName(CodeLanguage.Swift, false);
        //     string cMemberNamePrefix = type.CTypeName();
        //     
        //     WriteDelegateTypeMembers(
        //         typeDescriptor,
        //         fullTypeName,
        //         cTypeName,
        //         cMemberNamePrefix,
        //         sb,
        //         state,
        //         typeDescriptorRegistry
        //     );
        // }

        HashSet<MemberInfo> generatedMembers = new();

        StringBuilder sbMembers = new();

        foreach (var cSharpMember in cSharpMembers) {
            var member = cSharpMember.Member;

            if (member is not null &&
                generatedMembers.Contains(member)) {
                continue;
            }
            
            var memberKind = cSharpMember.MemberKind;
            var memberType = member?.MemberType;

            ISwiftSyntaxWriter? syntaxWriter = GetSyntaxWriter(
                memberKind,
                memberType ?? MemberTypes.Custom
            );
            
            if (syntaxWriter == null) {
                if (Settings.EmitUnsupported) {
                    sbMembers.AppendLine($"// TODO: Unsupported Member Type \"{memberType}\"");
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

            string memberCode = syntaxWriter.Write(target, state);

            sbMembers.AppendLine(memberCode);

            if (member is not null) {
                generatedMembers.Add(member);
            }
        }

        string membersCode = sbMembers
            .ToString()
            .IndentAllLines(1); 

        sb.AppendLine(membersCode);

        if (writeTypeDefinition) {
            sb.AppendLine("}");
        }

        return sb.ToString();
    }

    // TODO
    private void WriteDelegateTypeMembers(
        TypeDescriptor typeDescriptor,
        string fullTypeName,
        string cTypeName,
        string cMemberNamePrefix,
        StringBuilder sb,
        State state,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        sb.AppendLine("// TODO: Delegate Members");
        
        return;
        
        // TODO: Generics
        
        Type type = typeDescriptor.ManagedType;
        
        MethodInfo? invokeMethod = typeDescriptor.ManagedType.GetDelegateInvokeMethod();
        Type returnType = invokeMethod?.ReturnType ?? typeof(void);
        TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
        
        var parameterInfos = invokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>();

        if (returnType.IsByRef) {
            sb.AppendLine($"// TODO: ({cTypeName}) Unsupported delegate type. Reason: Has by ref return type");

            return;
        }
        
        foreach (var parameter in parameterInfos) {
            if (parameter.IsOut) {
                sb.AppendLine($"// TODO: ({cTypeName}) Unsupported delegate type. Reason: Has out parameters");
                
                return;
            }

            Type parameterType = parameter.ParameterType;
            
            if (parameterType.IsByRef) {
                sb.AppendLine($"// TODO: ({cTypeName}) Unsupported delegate type. Reason: Has by ref parameters");
                
                return;
            }
            
            if (parameterType.IsGenericParameter ||
                parameterType.IsGenericMethodParameter) {
                sb.AppendLine($"// TODO: ({cTypeName}) Unsupported delegate type. Reason: Has generic parameters");
                
                return;
            }
        }
        
        string contextType = "const void*";
        string functionType = $"{cMemberNamePrefix}_CFunction_t";
        string destrutorFunctionType = $"{cMemberNamePrefix}_CDestructorFunction_t";

        #region Create
        sb.AppendLine($"{cTypeName} /* {fullTypeName} */");
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

        string parameters = SwiftMethodSyntaxWriter.WriteParameters(
            MemberKind.Automatic,
            null,
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

    private ISwiftSyntaxWriter? GetSyntaxWriter(
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
            out ISwiftSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
}