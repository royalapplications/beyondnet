using System.Reflection;
using System.Text;
using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;
using Settings = NativeAOT.CodeGenerator.Generator.C.Settings;

namespace NativeAOT.CodeGenerator.Syntax.C;

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
    
    public string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }

    public string Write(Type type, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        if (type.IsPrimitive ||
            type.IsPointer ||
            type.IsByRef) {
            // No need to generate C code for those kinds of types

            return string.Empty;
        }

        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            return $"// Type \"{type.Name}\" was skipped. Reason: It has no full name.";
        }
        
        string cTypeName = fullTypeName.CTypeName();

        StringBuilder sb = new();

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
        return $"typedef void* {cTypeName}_t;";
    }

    private string WriteDelegateTypeDefs(
        Type delegateType,
        MethodInfo? delegateInvokeMethod
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        string fullTypeName = delegateType.GetFullNameOrName();
        string cTypeName = fullTypeName.CTypeName();

        StringBuilder sb = new();

        sb.AppendLine(WriteTypeDef(cTypeName));

        string contextTypeName = "void*";
        string cFunctionTypeName = $"{cTypeName}_CFunction_t";
        string cDestructorFunctionTypeName = $"{cTypeName}_CDestructorFunction_t";
        
        sb.AppendLine($"typedef void (*{cDestructorFunctionTypeName})({contextTypeName} context);");
        sb.AppendLine();

        Type returnType = delegateInvokeMethod?.ReturnType ?? typeof(void);

        string cReturnTypeName;

        if (returnType.IsVoid()) {
            cReturnTypeName = "void";
        } else {
            TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
            cReturnTypeName = returnTypeDescriptor.GetTypeName(CodeLanguage.C, true);            
        }

        sb.AppendLine($"typedef {cReturnTypeName} (*{cFunctionTypeName})(");

        List<string> parameters = new();

        var parameterInfos = delegateInvokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>();

        foreach (var parameter in parameterInfos) {
            string parameterName = parameter.Name ?? throw new Exception("Delegate parameter has no name");
        
            Type parameterType = parameter.ParameterType;
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

            string parameterTypeName = parameterTypeDescriptor.GetTypeName(CodeLanguage.C, true);

            parameters.Add($"{parameterTypeName} {parameterName}");
        }

        string parametersString = string.Join(",\n", parameters);

        string contextParameter = $"{contextTypeName} context";

        if (!string.IsNullOrEmpty(parametersString)) {
            parametersString = contextParameter + ",\n" + parametersString;
        } else {
            parametersString = contextParameter + "\n";
        }
    
        sb.Append(parametersString
            .IndentAllLines(1));
    
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
        StringBuilder sb = new();

        Type underlyingType = type.GetEnumUnderlyingType();
        TypeDescriptor underlyingTypeDescriptor = underlyingType.GetTypeDescriptor(typeDescriptorRegistry);

        string underlyingTypeName = underlyingTypeDescriptor.GetTypeName(CodeLanguage.C, false);

        sb.AppendLine($"typedef enum __attribute__((enum_extensibility(closed))): {underlyingTypeName} {{");

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
        
        sb.AppendLine($"}} {cTypeName};");
        
        return sb.ToString();
    }

    public string WriteMembers(Type type, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        if (type.IsPrimitive ||
            type.IsPointer ||
            type.IsByRef) {
            // No need to generate C code for those kinds of types

            return string.Empty;
        }
        
        var cSharpMembers = cSharpUnmanagedResult.GeneratedTypes[type];

        StringBuilder sb = new();

        string fullTypeName = type.GetFullNameOrName();
        
        bool isDelegate = type.IsDelegate();

        sb.AppendLine($"#pragma mark - BEGIN APIs of {fullTypeName}");

        if (isDelegate) {
            TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
            string cTypeName =  typeDescriptor.GetTypeName(CodeLanguage.C, false);
            string cMemberNamePrefix = fullTypeName.CTypeName();
            
            WriteDelegateTypeMembers(
                fullTypeName,
                cTypeName,
                cMemberNamePrefix,
                sb,
                state
            );
        }

        foreach (var cSharpMember in cSharpMembers) {
            var member = cSharpMember.Member;
            var memberKind = cSharpMember.MemberKind;
            var memberType = member?.MemberType;

            ICSyntaxWriter? syntaxWriter = GetSyntaxWriter(
                memberKind,
                memberType ?? MemberTypes.Custom
            );
            
            if (syntaxWriter == null) {
                if (Settings.EmitUnsupported) {
                    sb.AppendLine($"// TODO: Unsupported Member Type \"{memberType}\"");
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

            string memberCode;
            
            if (member is FieldInfo fieldInfo &&
                syntaxWriter is CFieldSyntaxWriter fieldSyntaxWriter) {
                memberCode = fieldSyntaxWriter.Write(fieldInfo, memberKind, state);
            } else {
                memberCode = syntaxWriter.Write(target, state);
            }

            sb.AppendLine(memberCode);
        }

        sb.AppendLine($"#pragma mark - END APIs of {fullTypeName}");

        return sb.ToString();
    }

    private void WriteDelegateTypeMembers(
        string fullTypeName,
        string cTypeName,
        string cMemberNamePrefix,
        StringBuilder sb,
        State state
    )
    {
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