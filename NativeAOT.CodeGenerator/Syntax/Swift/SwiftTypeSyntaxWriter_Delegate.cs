using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Syntax.Swift.Declaration;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.Swift;

public partial class SwiftTypeSyntaxWriter
{
    internal class DelegateTypeInfo
    {
        internal Type Type { get; }
        internal TypeDescriptor TypeDescriptor { get; }
        internal string TypeName { get; }
        internal string FullTypeName { get; }
        internal string CTypeName { get; }
        internal string SwiftTypeName { get; }
        
        internal Type? BaseType { get; }
        internal TypeDescriptor? BaseTypeDescriptor { get; }
        internal string SwiftBaseTypeName { get; }
        
        internal Type ReturnType { get; }
        internal TypeDescriptor ReturnTypeDescriptor { get; }
        internal bool IsReturning { get; }
        internal bool ReturnTypeIsPrimitive { get; }
        internal bool ReturnTypeIsOptional { get; }
        internal string SwiftReturnTypeName { get; }
        
        internal ParameterInfo[] ParameterInfos { get; }
        
        internal MethodInfo? DelegateInvokeMethod { get; }

        internal DelegateTypeInfo(
            Type type,
            MethodInfo? delegateInvokeMethod,
            TypeDescriptorRegistry typeDescriptorRegistry
        )
        {
            DelegateInvokeMethod = delegateInvokeMethod;
            
            Type = type;
            TypeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);

            string? fullTypeName = type.FullName;
            
            if (string.IsNullOrEmpty(fullTypeName)) {
                throw new Exception($"// Type \"{type.Name}\" was skipped. Reason: It has no full name.");
            }

            FullTypeName = fullTypeName;
            TypeName = type.GetFullNameOrName();
            CTypeName = type.CTypeName();
            SwiftTypeName = TypeDescriptor.GetTypeName(CodeLanguage.Swift, false);
            
            ReturnType = delegateInvokeMethod?.ReturnType ?? typeof(void);
    
            if (ReturnType.IsByRef) {
                throw new Exception($"// TODO: ({SwiftTypeName}) Unsupported delegate type. Reason: Has by ref return type");
            }
            
            IsReturning = !ReturnType.IsVoid();
    
            ReturnTypeDescriptor = ReturnType.GetTypeDescriptor(typeDescriptorRegistry);
            
            ReturnTypeIsPrimitive = ReturnType.IsPrimitive;
            ReturnTypeIsOptional = !ReturnTypeIsPrimitive;
            
            // TODO: This generates inout TypeName if the return type is by ref
            SwiftReturnTypeName = ReturnTypeDescriptor.GetTypeName(
                CodeLanguage.Swift,
                true,
                ReturnTypeIsOptional,
                false,
                false
            );
            
            var parameterInfos = delegateInvokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>();
            
            foreach (var parameter in parameterInfos) {
                if (parameter.IsOut ||
                    parameter.ParameterType.IsByRef) {
                    throw new Exception($"// TODO: ({SwiftTypeName}) Unsupported delegate type. Reason: Has by ref or out parameters");
                }
            }

            ParameterInfos = parameterInfos;
            
            BaseType = type.BaseType;
            BaseTypeDescriptor = BaseType?.GetTypeDescriptor(typeDescriptorRegistry);
    
            SwiftBaseTypeName = BaseTypeDescriptor?.GetTypeName(CodeLanguage.Swift, false)
                                ?? "DNObject";
        }

        public bool DelegateInvokeMethodMatches(MethodInfo? otherDelegateInvokeMethod)
        {
            MethodInfo? delegateInvokeMethod = DelegateInvokeMethod;

            if (otherDelegateInvokeMethod == delegateInvokeMethod) {
                return true;
            }

            if (delegateInvokeMethod == null ||
                otherDelegateInvokeMethod == null) {
                return false;
            }

            var returnType = delegateInvokeMethod.ReturnType;
            var otherReturnType = otherDelegateInvokeMethod.ReturnType;

            if (returnType != otherReturnType) {
                return false;
            }

            var parameterInfos = delegateInvokeMethod.GetParameters();
            var otherParameterInfos = otherDelegateInvokeMethod.GetParameters();

            if (parameterInfos != otherParameterInfos) {
                return false;
            }

            return true;
        }
    }
    
    private string WriteDelegateTypeDefs(
        Type type,
        MethodInfo? delegateInvokeMethod,
        State state
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        if (state.CSharpUnmanagedResult is null) {
            throw new Exception("No C# unmanaged result");
        }

        if (state.CResult is null) {
            throw new Exception("No C result");
        }

        DelegateTypeInfo typeInfo;

        try {
            typeInfo = new(
                type,
                delegateInvokeMethod,
                typeDescriptorRegistry
            );
        } catch (Exception ex) {
            return ex.Message;
        }
        
        Type? baseType = typeInfo.BaseType;
        MethodInfo? baseTypeDelegateInvokeMethod;

        if (baseType is not null &&
            baseType.IsDelegate()) {
            baseTypeDelegateInvokeMethod = baseType.GetDelegateInvokeMethod();
        } else {
            baseTypeDelegateInvokeMethod = null;
        }
            
        StringBuilder sb = new();
        
        sb.AppendLine($"public class {typeInfo.SwiftTypeName} /* {typeInfo.FullTypeName} */: {typeInfo.SwiftBaseTypeName} {{");

        List<string> memberParts = new();

        #region Type Names
        string typeNamesCode = WriteTypeNames(
            typeInfo.TypeName,
            typeInfo.FullTypeName
        );
        
        memberParts.Add(typeNamesCode);
        #endregion Type Names
        
        #region Closure Type Alias
        string closureTypeAliasCode = WriteClosureTypeAlias(
            type,
            typeInfo.ParameterInfos,
            typeInfo.SwiftReturnTypeName,
            typeDescriptorRegistry,
            out string closureTypeTypeAliasName
        );

        memberParts.Add(closureTypeAliasCode);
        #endregion Closure Type Alias

        #region Create C Function
        string createCFunctionCode = WriteCreateCFunction(
            type,
            typeInfo.ParameterInfos,
            typeInfo.CTypeName,
            closureTypeTypeAliasName,
            typeInfo.IsReturning,
            typeInfo.ReturnTypeDescriptor,
            typeInfo.ReturnTypeIsOptional,
            typeInfo.ReturnTypeIsPrimitive,
            typeDescriptorRegistry,
            out string createCFunctionFuncName
        );

        memberParts.Add(createCFunctionCode);
        #endregion Create C Function

        #region Create C Destructor Function
        string createCDestructorFunctionCode = WriteCreateCDestructorFunction(
            typeInfo.CTypeName,
            closureTypeTypeAliasName,
            out string createCDestructorFunctionFuncName
        );

        memberParts.Add(createCDestructorFunctionCode);
        #endregion Create C Destructor Function

        #region Init
        string initCode = WriteInit(
            typeInfo.CTypeName,
            closureTypeTypeAliasName,
            createCFunctionFuncName,
            createCDestructorFunctionFuncName
        );

        memberParts.Add(initCode);
        #endregion Init

        #region Invoke
        if (typeInfo.DelegateInvokeMethod is not null) {
            string invokeCode = WriteInvoke(
                typeInfo,
                baseTypeDelegateInvokeMethod,
                typeDescriptorRegistry
            );
    
            memberParts.Add(invokeCode);
        }
        #endregion Invoke

        string memberPartsCode = string.Join('\n', memberParts);
        sb.AppendLine(memberPartsCode.IndentAllLines(1));

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

        sb.AppendLine(typeAliasDeclaration.ToString());

        string code = sb.ToString();

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
        DelegateTypeInfo typeInfo,
        MethodInfo? baseTypeDelegateInvokeMethod,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        StringBuilder sb = new();
        
        string swiftFuncParameters = SwiftMethodSyntaxWriter.WriteParameters(
            MemberKind.Method,
            null,
            false,
            typeInfo.Type,
            typeInfo.ParameterInfos,
            false,
            Array.Empty<Type>(),
            false,
            false,
            typeDescriptorRegistry
        );

        bool isOverride = typeInfo.DelegateInvokeMethodMatches(baseTypeDelegateInvokeMethod);

        SwiftFuncDeclaration swiftFuncDecl = new(
            "invoke",
            SwiftVisibilities.Public,
            SwiftTypeAttachmentKinds.Instance,
            isOverride,
            swiftFuncParameters,
            true,
            typeInfo.SwiftReturnTypeName
        );

        sb.AppendLine($"{swiftFuncDecl} {{");

        string exceptionCVarName = "__exceptionC";

        sb.AppendLine($"\tvar {exceptionCVarName}: System_Exception_t?");
        sb.AppendLine();

        string selfConvertedVarName = "__selfC";

        sb.AppendLine($"\tlet {selfConvertedVarName} = self.__handle");
        sb.AppendLine();

        string parameterConversions = SwiftMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.Swift,
            CodeLanguage.C,
            MemberKind.Method,
            null,
            typeInfo.ParameterInfos,
            false,
            Array.Empty<Type>(),
            Array.Empty<Type>(),
            typeDescriptorRegistry,
            out List<string> convertedParameterNamesToC,
            out _,
            out _
        );

        convertedParameterNamesToC.Insert(0, selfConvertedVarName);
        convertedParameterNamesToC.Add($"&{exceptionCVarName}");

        string allParameterNamesString = string.Join(", ", convertedParameterNamesToC);
        
        sb.AppendLine(parameterConversions
            .IndentAllLines(1));
        
        string returnValueName = "__returnValueC";
            
        string returnValueStorage = typeInfo.IsReturning
            ? $"let {returnValueName} = "
            : string.Empty;
        

        string cInvokeMethodName = $"{typeInfo.CTypeName}_Invoke";
        
        string invocation = $"{returnValueStorage}{cInvokeMethodName}({allParameterNamesString})";
        
        sb.AppendLine($"\t{invocation}");
        sb.AppendLine();
        
        string returnCode = string.Empty;
        
        if (typeInfo.IsReturning) {
            string? returnTypeConversion = typeInfo.ReturnTypeDescriptor.GetTypeConversion(
                CodeLanguage.C,
                CodeLanguage.Swift
            );
        
            if (!string.IsNullOrEmpty(returnTypeConversion)) {
                string newReturnValueName = "__returnValue";
        
                string fullReturnTypeConversion = $"let {newReturnValueName} = {string.Format(returnTypeConversion, $"{returnValueName}")}";
        
                sb.AppendLine($"\t{fullReturnTypeConversion}");
                sb.AppendLine();
                        
                returnValueName = newReturnValueName;
            }
        
            returnCode = $"return {returnValueName}";
        }
        
        sb.AppendLine("""
    if let __exceptionC {
        let __exception = System_Exception(handle: __exceptionC)
        let __error = __exception.error
        
        throw __error
    }
""");

        sb.AppendLine();
        
        if (typeInfo.IsReturning) {
            sb.AppendLine($"\t{returnCode}");
        }
        
        sb.AppendLine("}");

        string code = sb.ToString();

        return code;
    }
}