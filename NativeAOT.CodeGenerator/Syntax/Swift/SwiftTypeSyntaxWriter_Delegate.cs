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
        sb.AppendLine($"\tpublic override class var typeName: String {{ \"{typeName}\" }}");
        sb.AppendLine($"\tpublic override class var fullTypeName: String {{ \"{fullTypeName}\" }}");
        sb.AppendLine();
        #endregion Type Names
        
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

        #region Closure Type Alias
        string closureTypeAliasCode = WriteClosureTypeAlias(
            swiftFuncParameters,
            swiftReturnTypeName,
            out string closureTypeTypeAliasName
        );

        sb.AppendLine(closureTypeAliasCode.IndentAllLines(1));
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
        sb.AppendLine($"\t\t\tlet {innerSwiftContextVarName} = NativeBox<{closureTypeTypeAliasName}>.fromPointer({innerContextParameterName})");
        sb.AppendLine($"\t\t\tlet {innerClosureVarName} = {innerSwiftContextVarName}.value");
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
            .IndentAllLines(3));
        
        string returnValueName = "__returnValueSwift";
            
        string returnValueStorage = isReturning
            ? $"let {returnValueName} = "
            : string.Empty;
        
        string allParameterNamesString = string.Join(", ", convertedParameterNamesToSwift);
        
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

                string returnTypeOptionalString = returnTypeIsOptional
                    ? "?"
                    : string.Empty;

                string fullReturnTypeConversion = $"let {newReturnValueName} = {string.Format(returnTypeConversion, $"{returnValueName}{returnTypeOptionalString}")}";
    
                sb.AppendLine($"\t\t\t{fullReturnTypeConversion}");
                
                if (!returnTypeIsPrimitive) {
                    sb.AppendLine($"\t\t\t{returnValueName}?.__skipDestroy = true // Will be destroyed by .NET");
                }
                
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
        sb.AppendLine($"\t\t\tNativeBox<{closureTypeTypeAliasName}>.release({innerContextParameterName})");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine();
        #endregion Create C Destructor Function

        #region Init
        sb.AppendLine($"\tpublic convenience init?(_ __closure: @escaping {closureTypeTypeAliasName}) {{");
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
        SwiftFuncDeclaration swiftFuncDecl = new(
            "invoke",
            SwiftVisibilities.Public,
            SwiftTypeAttachmentKinds.Instance,
            false,
            swiftFuncParameters,
            true,
            swiftReturnTypeName
        );

        sb.AppendLine($"\t{swiftFuncDecl} {{");

        sb.AppendLine("\t\tvar __exceptionC: System_Exception_t?");
        sb.AppendLine();

        string selfConvertedVarName = "__selfC";

        sb.AppendLine($"\t\tlet {selfConvertedVarName} = self.__handle");
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
            .IndentAllLines(2));
        
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
        
        sb.AppendLine("\t}");
        
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

    private string WriteClosureTypeAlias(
        string swiftFuncParameters,
        string swiftReturnTypeName,
        out string closureTypeTypeAliasName
    )
    {
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
    )
    {
        return "TODO";
    }
}