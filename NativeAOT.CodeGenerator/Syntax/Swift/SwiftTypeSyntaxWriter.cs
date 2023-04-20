using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Syntax.C;
using NativeAOT.CodeGenerator.Syntax.Swift.Declaration;
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

        if (state.CSharpUnmanagedResult is null) {
            throw new Exception("No CSharpUnmanagedResult provided");
        }

        if (state.CResult is null) {
            throw new Exception("No CResult provided");
        }
        
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
        
        StringBuilder sb = new();

        bool writeMembers = true;
        bool writeTypeDefinition = true;
        bool writeTypeExtension = false;
        
        if (type.IsEnum) {
            writeTypeDefinition = false;
            writeTypeExtension = true;
            
            string enumdefCode = WriteEnumDef(
                type,
                typeDescriptorRegistry
            );

            sb.AppendLine(enumdefCode);
        } else if (type.IsDelegate()) {
            writeTypeDefinition = false;
            writeMembers = false;
            
            var delegateInvokeMethod = type.GetDelegateInvokeMethod();

            string delegateTypedefCode = WriteDelegateTypeDefs(
                type,
                delegateInvokeMethod,
                state
            );
    
            sb.AppendLine(delegateTypedefCode);
        }

        if (writeTypeExtension) {
            string swiftTypeName = type.GetTypeDescriptor(typeDescriptorRegistry)
                .GetTypeName(CodeLanguage.Swift, false);

            sb.AppendLine($"extension {swiftTypeName} {{");
        }

        if (writeMembers) {
            string membersCode = WriteMembers(
                type,
                state,
                writeTypeDefinition
            );
                
            sb.AppendLine(membersCode);
        }
        
        if (writeTypeExtension) {
            sb.AppendLine("}");
        }
        
        return sb.ToString();
    }

    private string WriteDelegateTypeDefs(
        Type type,
        MethodInfo? delegateInvokeMethod,
        State state
    )
    {
        // return $"// TODO: Delegate Type Defition ({delegateType.GetFullNameOrName()})";
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);

        var cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No C# unmanaged result");
        var cResult = state.CResult ?? throw new Exception("No C result");
        
        string? fullTypeName = type.FullName;
        
        if (fullTypeName == null) {
            return $"// Type \"{type.Name}\" was skipped. Reason: It has no full name.";
        }

        string typeName = type.GetFullNameOrName();
        string cTypeName = type.CTypeName();
        string swiftTypeName = typeDescriptor.GetTypeName(CodeLanguage.Swift, false);
        
        Type returnType = delegateInvokeMethod?.ReturnType ?? typeof(void);

        if (returnType.IsByRef) {
            return $"// TODO: ({swiftTypeName}) Unsupported delegate type. Reason: Has by ref return type";
        }
        
        bool isReturning = !returnType.IsVoid();

        TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
        
        const bool returnTypeIsOptional = true;
        
        // TODO: This generates inout TypeName if the return type is by ref
        string swiftReturnTypeName = returnTypeDescriptor.GetTypeName(
            CodeLanguage.Swift,
            true,
            returnTypeIsOptional,
            false,
            false
        );
        
        var parameterInfos = delegateInvokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>();
        
        foreach (var parameter in parameterInfos) {
            if (parameter.IsOut ||
                parameter.ParameterType.IsByRef) {
                return $"// TODO: ({swiftTypeName}) Unsupported delegate type. Reason: Has by ref or out parameters";
            }
        }
        
        StringBuilder sb = new();
        
        Type? baseType = type.BaseType;
        TypeDescriptor? baseTypeDescriptor = baseType?.GetTypeDescriptor(typeDescriptorRegistry);

        string swiftBaseTypeName = baseTypeDescriptor?.GetTypeName(CodeLanguage.Swift, false)
                                   ?? "DNObject";
            
        sb.AppendLine($"public class {swiftTypeName} /* {fullTypeName} */: {swiftBaseTypeName} {{");

        #region Type Names
        sb.AppendLine($"\tpublic override class var typeName: String {{ \"{typeName}\" }}");
        sb.AppendLine($"\tpublic override class var fullTypeName: String {{ \"{fullTypeName}\" }}");
        sb.AppendLine();
        #endregion Type Names

        #region Closure Type Alias
        string swiftClosureParameters = SwiftMethodSyntaxWriter.WriteParameters(
            MemberKind.Method,
            null,
            false,
            type,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            false,
            false,
            typeDescriptorRegistry
        );

        SwiftClosureDeclaration swiftClosureDecl = new(
            swiftClosureParameters,
            false,
            swiftReturnTypeName
        );

        string closureTypeTypealiasName = "ClosureType";

        sb.AppendLine($"\tpublic typealias {closureTypeTypealiasName} = {swiftClosureDecl.ToString()}");
        sb.AppendLine();
        #endregion Closure Type Alias

        #region Create C Function
        string cFunctionParameters = SwiftMethodSyntaxWriter.WriteParameters(
            MemberKind.Method,
            null,
            false,
            type,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            true,
            false,
            typeDescriptorRegistry
        );

        string innerContextParameterName = "__innerContext";

        if (string.IsNullOrEmpty(cFunctionParameters)) {
            cFunctionParameters = innerContextParameterName;
        } else {
            cFunctionParameters = $"{innerContextParameterName}, {cFunctionParameters}";
        }

        string fatalErrorMessageIfNoContext = "Context is nil";
        
        string innerSwiftContextVarName = "__innerSwiftContext";
        string createCFunctionFuncName = "__createCFunction";
        string innerClosureVarName = "__innerClosure";

        sb.AppendLine($"\tprivate static func {createCFunctionFuncName}() -> {cTypeName}_CFunction_t {{");
        sb.AppendLine($"\t\treturn {{ {cFunctionParameters} in");
        sb.AppendLine($"\t\t\tguard let {innerContextParameterName} else {{ fatalError(\"{fatalErrorMessageIfNoContext}\") }}");
        sb.AppendLine();
        sb.AppendLine($"\t\t\tlet {innerSwiftContextVarName} = NativeBox<{closureTypeTypealiasName}>.fromPointer({innerContextParameterName})");
        sb.AppendLine($"\t\t\tlet {innerClosureVarName} = {innerSwiftContextVarName}.value");
        sb.AppendLine();

        string parameterConversions = SwiftMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.C,
            CodeLanguage.Swift,
            MemberKind.Method,
            null,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            Array.Empty<Type>(),
            typeDescriptorRegistry,
            out List<string> convertedParameterNames,
            out _,
            out _
        );

        sb.AppendLine(parameterConversions
            .IndentAllLines(3));
        
        string returnValueName = "__returnValueSwift";
            
        string returnValueStorage = isReturning
            ? $"let {returnValueName} = "
            : string.Empty;
        
        string allParameterNamesString = string.Join(", ", convertedParameterNames);
        
        string invocation = $"{returnValueStorage}{innerClosureVarName}({allParameterNamesString})";
        
        sb.AppendLine($"\t\t\t{invocation}");
        sb.AppendLine();
        
        string returnCode = string.Empty;

        if (isReturning) {
            string? returnTypeConversion = returnTypeDescriptor.GetTypeConversion(
                CodeLanguage.Swift,
                CodeLanguage.C
            );
    
            if (!string.IsNullOrEmpty(returnTypeConversion)) {
                string newReturnValueName = "__returnValue";
                        
                string fullReturnTypeConversion = $"let {newReturnValueName} = {string.Format(returnTypeConversion, $"{returnValueName}?")}";
    
                sb.AppendLine($"\t\t\t{fullReturnTypeConversion}");
                sb.AppendLine();
                        
                returnValueName = newReturnValueName;
            }
    
            returnCode = $"return {returnValueName}";
        }

        if (isReturning) {
            sb.AppendLine($"\t\t\t{returnCode}");
        }
        
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine();
        #endregion Create C Function

        #region Create C Destructor Function
        string createCDestructorFunctionFuncName = "__createCDestructorFunction";
        
        sb.AppendLine($"\tprivate static func {createCDestructorFunctionFuncName}() -> {cTypeName}_CDestructorFunction_t {{");
        sb.AppendLine($"\t\treturn {{ {innerContextParameterName} in");
        sb.AppendLine($"\t\t\tguard let {innerContextParameterName} else {{ fatalError(\"{fatalErrorMessageIfNoContext}\") }}");
        sb.AppendLine();
        sb.AppendLine($"\t\t\tNativeBox<{closureTypeTypealiasName}>.release({innerContextParameterName})");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine();
        #endregion Create C Destructor Function

        #region Init
        sb.AppendLine($"\tpublic convenience init?(_ __closure: @escaping {closureTypeTypealiasName}) {{");
        sb.AppendLine($"\t\tlet __cFunction = Self.{createCFunctionFuncName}()");
        sb.AppendLine($"\t\tlet __cDestructorFunction = Self.{createCDestructorFunctionFuncName}()");
        sb.AppendLine();
        sb.AppendLine("\t\tlet __outerSwiftContext = NativeBox(__closure)");
        sb.AppendLine("\t\tlet __outerContext = __outerSwiftContext.retainedPointer()");
        sb.AppendLine();
        sb.AppendLine($"\t\tguard let __delegateC = {cTypeName}_Create(__outerContext, __cFunction, __cDestructorFunction) else {{ return nil }}");
        sb.AppendLine();
        sb.AppendLine("\t\tself.init(handle: __delegateC)");
        sb.AppendLine("\t}");
        sb.AppendLine();
        #endregion Init

        #region Invoke
        // TODO
        sb.AppendLine("\t// TODO: invoke");
        sb.AppendLine();
        #endregion Invoke

        #region Other Members
        string membersCode = WriteMembers(
            type,
            state,
            false
        );
                
        sb.AppendLine(membersCode);
        #endregion Other Members
        
        sb.AppendLine("}");

        string code = sb.ToString();

        return code;
    }

    private string WriteEnumDef(
        Type type,
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

    public string WriteMembers(
        Type type,
        State state,
        bool writeTypeDefinition
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");

        if (state.CResult is null) {
            throw new Exception("No CResult provided");
        }
        
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

        // bool isAbstract = type.IsAbstract;
        
        var cSharpMembers = cSharpUnmanagedResult.GeneratedTypes[type];
        // var cMembers = cResult.GeneratedTypes[type];

        StringBuilder sb = new();

        string typeName = type.Name;
        string fullTypeName = type.GetFullNameOrName();
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
        string swiftTypeName =  typeDescriptor.GetTypeName(CodeLanguage.Swift, false);
        
        bool isDelegate = type.IsDelegate();

        if (writeTypeDefinition) {
            Type? baseType = type.BaseType;
            TypeDescriptor? baseTypeDescriptor = baseType?.GetTypeDescriptor(typeDescriptorRegistry);

            string swiftBaseTypeName = baseTypeDescriptor?.GetTypeName(CodeLanguage.Swift, false)
                                       ?? "DNObject";
            
            sb.AppendLine($"public class {swiftTypeName} /* {fullTypeName} */: {swiftBaseTypeName} {{");
            sb.AppendLine($"\tpublic override class var typeName: String {{ \"{typeName}\" }}");
            sb.AppendLine($"\tpublic override class var fullTypeName: String {{ \"{fullTypeName}\" }}");
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
            false,
            false,
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

    public string WriteTypeExtensionMethods(
        Type extendedType,
        List<GeneratedMember> generatedMembers
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        string codeForOptional = GetTypeExtensionsCode(
            extendedType,
            true,
            generatedMembers,
            typeDescriptorRegistry
        );
            
        string codeForNonOptional = GetTypeExtensionsCode(
            extendedType,
            false,
            generatedMembers,
            typeDescriptorRegistry
        );

        StringBuilder sb = new();

        sb.AppendLine(codeForOptional);
        sb.AppendLine(codeForNonOptional);

        string code = sb.ToString();

        return code;
    }
    
    private string GetTypeExtensionsCode(
        Type extendedType,
        bool isExtendedTypeOptional,
        List<GeneratedMember> generatedMembers,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        if (generatedMembers.Count <= 0) {
            return string.Empty;
        }
            
        StringBuilder sb = new();

        TypeDescriptor extendedTypeDescriptor = extendedType.GetTypeDescriptor(typeDescriptorRegistry);
        string extendedTypeSwiftName = extendedTypeDescriptor.GetTypeName(CodeLanguage.Swift, false);

        string extendedTypeOptionality = isExtendedTypeOptional
            ? "?"
            : string.Empty;
        
        string typeExtensionDecl = $"extension {extendedTypeSwiftName}{extendedTypeOptionality} {{";
        sb.AppendLine(typeExtensionDecl);

        StringBuilder sbMembers = new();
        
        foreach (GeneratedMember swiftGeneratedMember in generatedMembers) {
            string extensionMethod = SwiftMethodSyntaxWriter.WriteExtensionMethod(
                swiftGeneratedMember,
                typeDescriptorRegistry
            );

            sbMembers.AppendLine(extensionMethod);
        }

        sb.AppendLine(sbMembers
            .ToString()
            .IndentAllLines(1));

        sb.AppendLine("}");
        
        string code = sb.ToString();
        
        return code;
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