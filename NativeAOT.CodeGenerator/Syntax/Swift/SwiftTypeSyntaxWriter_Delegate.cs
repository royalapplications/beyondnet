using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Syntax.Swift.Declaration;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.Swift;

public partial class SwiftTypeSyntaxWriter
{
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
        
        bool returnTypeIsPrimitive = returnType.IsPrimitive;
        bool returnTypeIsOptional = !returnTypeIsPrimitive;
        
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
        string typeNamesCode = WriteTypeNames(
            typeName,
            fullTypeName
        );

        sb.AppendLine(typeNamesCode.IndentAllLines(1));
        sb.AppendLine();
        #endregion Type Names
        
        #region Closure Type Alias
        string closureTypeAliasCode = WriteClosureTypeAlias(
            type,
            parameterInfos,
            swiftReturnTypeName,
            typeDescriptorRegistry,
            out string closureTypeTypeAliasName
        );

        sb.AppendLine(closureTypeAliasCode.IndentAllLines(1));
        sb.AppendLine();
        #endregion Closure Type Alias

        #region Create C Function
        string createCFunctionCode = WriteCreateCFunction(
            type,
            parameterInfos,
            cTypeName,
            closureTypeTypeAliasName,
            isReturning,
            returnTypeDescriptor,
            returnTypeIsOptional,
            returnTypeIsPrimitive,
            typeDescriptorRegistry,
            out string createCFunctionFuncName
        );

        sb.AppendLine(createCFunctionCode.IndentAllLines(1));
        sb.AppendLine();
        #endregion Create C Function

        #region Create C Destructor Function
        string createCDestructorFunctionCode = WriteCreateCDestructorFunction(
            cTypeName,
            closureTypeTypeAliasName,
            out string createCDestructorFunctionFuncName
        );

        sb.AppendLine(createCDestructorFunctionCode.IndentAllLines(1));
        sb.AppendLine();
        #endregion Create C Destructor Function

        #region Init
        string initCode = WriteInit(
            cTypeName,
            closureTypeTypeAliasName,
            createCFunctionFuncName,
            createCDestructorFunctionFuncName
        );

        sb.AppendLine(initCode.IndentAllLines(1));
        sb.AppendLine();
        #endregion Init

        #region Invoke
        string invokeCode = WriteInvoke(
            type,
            parameterInfos,
            swiftReturnTypeName,
            returnTypeDescriptor,
            returnTypeIsOptional,
            returnTypeIsPrimitive,
            typeDescriptorRegistry
        );

        sb.AppendLine(invokeCode.IndentAllLines(1));
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

    private string WriteTypeNames(
        string typeName,
        string fullTypeName
    )
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"public override class var typeName: String {{ \"{typeName}\" }}");
        sb.AppendLine($"public override class var fullTypeName: String {{ \"{fullTypeName}\" }}");

        string code = sb.ToString();

        return code;
    }

    private string WriteClosureTypeAlias(
        Type type,
        ParameterInfo[] parameterInfos,
        string swiftReturnTypeName,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string closureTypeTypeAliasName
    )
    {
        string swiftFuncParameters = SwiftMethodSyntaxWriter.WriteParameters(
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
            swiftFuncParameters,
            false,
            swiftReturnTypeName
        );

        closureTypeTypeAliasName = "ClosureType";

        SwiftTypeAliasDeclaration typeAliasDeclaration = new(
            closureTypeTypeAliasName,
            SwiftVisibilities.Public,
            swiftClosureDecl.ToString()
        );

        string code = typeAliasDeclaration.ToString();

        return code;
    }

    private string WriteCreateCFunction(
        Type type,
        ParameterInfo[] parameterInfos,
        string cTypeName,
        string closureTypeTypeAliasName,
        bool isReturning,
        TypeDescriptor returnTypeDescriptor,
        bool returnTypeIsOptional,
        bool returnTypeIsPrimitive,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string createCFunctionFuncName
    )
    {
        StringBuilder sb = new();
        
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
        createCFunctionFuncName = "__createCFunction";
        string innerClosureVarName = "__innerClosure";

        sb.AppendLine($"private static func {createCFunctionFuncName}() -> {cTypeName}_CFunction_t {{");
        sb.AppendLine($"\treturn {{ {cFunctionParameters} in");
        sb.AppendLine($"\t\tguard let {innerContextParameterName} else {{ fatalError(\"{fatalErrorMessageIfNoContext}\") }}");
        sb.AppendLine();
        sb.AppendLine($"\t\tlet {innerSwiftContextVarName} = NativeBox<{closureTypeTypeAliasName}>.fromPointer({innerContextParameterName})");
        sb.AppendLine($"\t\tlet {innerClosureVarName} = {innerSwiftContextVarName}.value");
        sb.AppendLine();

        string parameterConversionsToSwift = SwiftMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.C,
            CodeLanguage.Swift,
            MemberKind.Method,
            null,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            Array.Empty<Type>(),
            typeDescriptorRegistry,
            out List<string> convertedParameterNamesToSwift,
            out _,
            out _
        );

        sb.AppendLine(parameterConversionsToSwift
            .IndentAllLines(2));
        
        string returnValueName = "__returnValueSwift";
            
        string returnValueStorage = isReturning
            ? $"let {returnValueName} = "
            : string.Empty;
        
        string allParameterNamesString = string.Join(", ", convertedParameterNamesToSwift);
        
        string invocation = $"{returnValueStorage}{innerClosureVarName}({allParameterNamesString})";
        
        sb.AppendLine($"\t\t{invocation}");
        sb.AppendLine();
        
        string returnCode = string.Empty;

        if (isReturning) {
            string? returnTypeConversion = returnTypeDescriptor.GetTypeConversion(
                CodeLanguage.Swift,
                CodeLanguage.C
            );
    
            if (!string.IsNullOrEmpty(returnTypeConversion)) {
                string newReturnValueName = "__returnValue";

                string returnTypeOptionalString = returnTypeIsOptional
                    ? "?"
                    : string.Empty;

                string fullReturnTypeConversion = $"let {newReturnValueName} = {string.Format(returnTypeConversion, $"{returnValueName}{returnTypeOptionalString}")}";
    
                sb.AppendLine($"\t\t{fullReturnTypeConversion}");
                
                if (!returnTypeIsPrimitive) {
                    sb.AppendLine($"\t\t{returnValueName}?.__skipDestroy = true // Will be destroyed by .NET");
                }
                
                sb.AppendLine();
                        
                returnValueName = newReturnValueName;
            }
    
            returnCode = $"return {returnValueName}";
        }

        if (isReturning) {
            sb.AppendLine($"\t\t{returnCode}");
        }
        
        sb.AppendLine("\t}");
        sb.AppendLine("}");

        string code = sb.ToString();

        return code;
    }

    private string WriteCreateCDestructorFunction(
        string cTypeName,
        string closureTypeTypeAliasName,
        out string createCDestructorFunctionFuncName
    )
    {
        StringBuilder sb = new();

        string innerContextParameterName = "__innerContext";
        string fatalErrorMessageIfNoContext = "Context is nil";
        
        createCDestructorFunctionFuncName = "__createCDestructorFunction";
        
        sb.AppendLine($"private static func {createCDestructorFunctionFuncName}() -> {cTypeName}_CDestructorFunction_t {{");
        sb.AppendLine($"\treturn {{ {innerContextParameterName} in");
        sb.AppendLine($"\t\tguard let {innerContextParameterName} else {{ fatalError(\"{fatalErrorMessageIfNoContext}\") }}");
        sb.AppendLine();
        sb.AppendLine($"\t\tNativeBox<{closureTypeTypeAliasName}>.release({innerContextParameterName})");
        sb.AppendLine("\t}");
        sb.AppendLine("}");

        string code = sb.ToString();

        return code;
    }

    private string WriteInit(
        string cTypeName,
        string closureTypeTypeAliasName,
        string createCFunctionFuncName,
        string createCDestructorFunctionFuncName
    )
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"public convenience init?(_ __closure: @escaping {closureTypeTypeAliasName}) {{");
        sb.AppendLine($"\tlet __cFunction = Self.{createCFunctionFuncName}()");
        sb.AppendLine($"\tlet __cDestructorFunction = Self.{createCDestructorFunctionFuncName}()");
        sb.AppendLine();
        sb.AppendLine("\tlet __outerSwiftContext = NativeBox(__closure)");
        sb.AppendLine("\tlet __outerContext = __outerSwiftContext.retainedPointer()");
        sb.AppendLine();
        sb.AppendLine($"\tguard let __delegateC = {cTypeName}_Create(__outerContext, __cFunction, __cDestructorFunction) else {{ return nil }}");
        sb.AppendLine();
        sb.AppendLine("\tself.init(handle: __delegateC)");
        sb.AppendLine("}");

        string code = sb.ToString();

        return code;
    }

    private string WriteInvoke(
        Type type,
        ParameterInfo[] parameterInfos,
        string swiftReturnTypeName,
        TypeDescriptor returnTypeDescriptor,
        bool returnTypeIsOptional,
        bool returnTypeIsPrimitive,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        StringBuilder sb = new();
        
        string swiftFuncParameters = SwiftMethodSyntaxWriter.WriteParameters(
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
        
        SwiftFuncDeclaration swiftFuncDecl = new(
            "invoke",
            SwiftVisibilities.Public,
            SwiftTypeAttachmentKinds.Instance,
            false,
            swiftFuncParameters,
            true,
            swiftReturnTypeName
        );

        sb.AppendLine($"{swiftFuncDecl} {{");

        sb.AppendLine("\tvar __exceptionC: System_Exception_t?");
        sb.AppendLine();

        string selfConvertedVarName = "__selfC";

        sb.AppendLine($"\tlet {selfConvertedVarName} = self.__handle");
        sb.AppendLine();

        string parameterConversionsToC = SwiftMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.Swift,
            CodeLanguage.C,
            MemberKind.Method,
            null,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            Array.Empty<Type>(),
            typeDescriptorRegistry,
            out List<string> convertedParameterNamesToC,
            out _,
            out _
        );

        sb.AppendLine(parameterConversionsToC
            .IndentAllLines(1));
        
        // TODO
        // string returnValueName = "__returnValueSwift";
        //     
        // string returnValueStorage = isReturning
        //     ? $"let {returnValueName} = "
        //     : string.Empty;
        //
        // string allParameterNamesString = string.Join(", ", convertedParameterNamesToC);
        //
        // string invocation = $"{returnValueStorage}{innerClosureVarName}({allParameterNamesString})";
        //
        // sb.AppendLine($"\t\t\t{invocation}");
        // sb.AppendLine();
        //
        // string returnCode = string.Empty;
        //
        // if (isReturning) {
        //     string? returnTypeConversion = returnTypeDescriptor.GetTypeConversion(
        //         CodeLanguage.Swift,
        //         CodeLanguage.C
        //     );
        //
        //     if (!string.IsNullOrEmpty(returnTypeConversion)) {
        //         string newReturnValueName = "__returnValue";
        //
        //         string returnTypeOptionalString = returnTypeIsOptional
        //             ? "?"
        //             : string.Empty;
        //
        //         string fullReturnTypeConversion = $"let {newReturnValueName} = {string.Format(returnTypeConversion, $"{returnValueName}{returnTypeOptionalString}")}";
        //
        //         sb.AppendLine($"\t\t\t{fullReturnTypeConversion}");
        //         
        //         if (!returnTypeIsPrimitive) {
        //             sb.AppendLine($"\t\t\t{returnValueName}?.__skipDestroy = true // Will be destroyed by .NET");
        //         }
        //         
        //         sb.AppendLine();
        //                 
        //         returnValueName = newReturnValueName;
        //     }
        //
        //     returnCode = $"return {returnValueName}";
        // }
        //
        // if (isReturning) {
        //     sb.AppendLine($"\t\t\t{returnCode}");
        // }
        
        sb.AppendLine("}");

        string code = sb.ToString();

        return code;
    }
}