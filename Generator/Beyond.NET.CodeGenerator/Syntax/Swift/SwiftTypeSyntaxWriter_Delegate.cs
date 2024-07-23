using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator.Swift;
using Beyond.NET.Core;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.Swift;

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
        internal bool ReturnTypeIsReadOnlySpanOfByte { get; }
        internal string SwiftReturnTypeName { get; }
        
        internal ParameterInfo[] ParameterInfos { get; }
        
        internal MethodInfo? DelegateInvokeMethod { get; }

        internal DelegateTypeInfo(
            Type type,
            MethodInfo? delegateInvokeMethod,
            TypeDescriptorRegistry typeDescriptorRegistry
        ) : this(
            type,
            delegateInvokeMethod,
            delegateInvokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>(),
            delegateInvokeMethod?.ReturnType ?? typeof(void),
            typeDescriptorRegistry
        )
        {
        }
        
        internal DelegateTypeInfo(
            Type type,
            MethodInfo? delegateInvokeMethod,
            ParameterInfo[] parameterInfos,
            Type returnType,
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
            
            ReturnType = returnType;
    
            if (ReturnType.IsByRef) {
                throw new Exception($"// TODO: ({SwiftTypeName}) Unsupported delegate type. Reason: Has by ref return type");
            }
            
            IsReturning = !ReturnType.IsVoid();
    
            ReturnTypeDescriptor = ReturnType.GetTypeDescriptor(typeDescriptorRegistry);
            
            ReturnTypeIsPrimitive = ReturnType.IsPrimitive;
            ReturnTypeIsReadOnlySpanOfByte = ReturnType.IsReadOnlySpanOfByte();
            ReturnTypeIsOptional = ReturnTypeDescriptor.Nullability == Nullability.Nullable;
            
            // TODO: This generates inout TypeName if the return type is by ref
            SwiftReturnTypeName = ReturnTypeDescriptor.GetTypeName(
                CodeLanguage.Swift,
                true,
                ReturnTypeIsOptional ? Nullability.Nullable : Nullability.NonNullable,
                Nullability.NotSpecified, // TODO
                false,
                false,
                false
            );
            
            foreach (var parameter in parameterInfos) {
                if (parameter.IsOut) {
                    throw new Exception($"// TODO: ({SwiftTypeName}) Unsupported delegate type. Reason: Has out parameters");
                }
            
                if (parameter.IsIn) {
                    throw new Exception($"// TODO: ({SwiftTypeName}) Unsupported delegate type. Reason: Has in parameters");
                }
                
                if (!ExperimentalFeatureFlags.EnableByRefParametersInDelegates) {
                    Type parameterType = parameter.ParameterType;
                    
                    if (parameterType.IsByRef) {
                        throw new Exception($"// TODO: ({SwiftTypeName}) Unsupported delegate type. Reason: Has by ref parameters");
                    }
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
        ISyntaxWriterConfiguration? configuration,
        Type type,
        State state
    )
    {
        var delegateInvokeMethod = type.GetDelegateInvokeMethod();
        
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
            state.AddSkippedType(type);
            
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
            typeInfo.ReturnTypeIsReadOnlySpanOfByte,
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

        string memberPartsCode = string.Join("\n\n", memberParts);
        
        SwiftCodeBuilder sb = new();
        
        sb.AppendLine(memberPartsCode);

        #region Other Members
        string membersCode = WriteMembers(
            configuration,
            type,
            state,
            false
        );
                
        sb.AppendLine(membersCode);
        #endregion Other Members

        string typeDecl = Builder.Class($"{typeInfo.SwiftTypeName} /* {typeInfo.FullTypeName} */")
            .BaseTypeName(typeInfo.SwiftBaseTypeName)
            .Public()
            .Implementation(sb.ToString())
            .ToString();
        
        var typeDocumentationComment = type.GetDocumentation()
            ?.GetFormattedDocumentationComment();

        SwiftCodeBuilder sbFinal;
        
        if (!string.IsNullOrEmpty(typeDocumentationComment)) {
            sbFinal = new(typeDocumentationComment + "\n");
            sbFinal.AppendLine(typeDecl);
        } else {
            sbFinal = new(typeDecl);
        }

        var final = sbFinal.ToString();

        return final;
    }

    private string WriteTypeNames(
        string typeName,
        string fullTypeName
    )
    {
        string typeNameDecl = Builder.GetOnlyProperty("typeName", "String")
            .Public()
            .Class()
            .Override()
            .Implementation($"\"{typeName}\"")
            .ToString();

        string fullTypeNameDecl = Builder.GetOnlyProperty("fullTypeName", "String")
            .Public()
            .Class()
            .Override()
            .Implementation($"\"{fullTypeName}\"")
            .ToString();
        
        string code = $"{typeNameDecl}\n\n{fullTypeNameDecl}";

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
        SwiftCodeBuilder sb = new();
        
        string swiftFuncParameters = SwiftMethodSyntaxWriter.WriteParameters(
            MemberKind.Method,
            null,
            Nullability.NotSpecified,
            Nullability.NotSpecified,
            false,
            type,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            false,
            false,
            typeDescriptorRegistry
        );

        string swiftClosureDecl = Builder.Closure()
            .Parameters(swiftFuncParameters)
            .ReturnTypeName(swiftReturnTypeName)
            .ToString();

        closureTypeTypeAliasName = "ClosureType";

        string typeAliasDeclaration = Builder.TypeAlias(closureTypeTypeAliasName, swiftClosureDecl)
            .Public()
            .ToString();

        sb.AppendLine(typeAliasDeclaration);

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
        bool returnValueIsReadOnlySpanOfByte,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string createCFunctionFuncName
    )
    {
        string cFunctionParameters = SwiftMethodSyntaxWriter.WriteParameters(
            MemberKind.Method,
            null,
            Nullability.NotSpecified,
            Nullability.NotSpecified,
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

        SwiftCodeBuilder sb = new();
        
        sb.AppendLine($"return {{ {cFunctionParameters} in");
        sb.AppendLine($"\tguard let {innerContextParameterName} else {{ fatalError(\"{fatalErrorMessageIfNoContext}\") }}");
        sb.AppendLine();
        
        sb.AppendLine(Builder.Let(innerSwiftContextVarName)
            .Value($"NativeBox<{closureTypeTypeAliasName}>.fromPointer({innerContextParameterName})")
            .ToIndentedString(1));
        
        sb.AppendLine(Builder.Let(innerClosureVarName)
            .Value($"{innerSwiftContextVarName}.value")
            .ToIndentedString(1));
        
        sb.AppendLine();

        string parameterConversionsToSwift = SwiftMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.C,
            CodeLanguage.Swift,
            MemberKind.Method,
            null,
            Nullability.NotSpecified,
            Nullability.NotSpecified,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            Array.Empty<Type>(),
            typeDescriptorRegistry,
            out List<string> convertedParameterNamesToSwift,
            out _,
            out _,
            out List<string> parameterBackConversionCodes
        );

        sb.AppendLine(parameterConversionsToSwift
            .IndentAllLines(1));
        
        string returnValueName = "__returnValueSwift";
            
        string returnValueStorage = isReturning
            ? $"let {returnValueName} = "
            : string.Empty;
        
        string allParameterNamesString = string.Join(", ", convertedParameterNamesToSwift);
        
        string invocation = $"{returnValueStorage}{innerClosureVarName}({allParameterNamesString})";
        
        sb.AppendLine($"\t{invocation}");
        sb.AppendLine();

        if (parameterBackConversionCodes.Count > 0) {
            foreach (var backConversionCode in parameterBackConversionCodes) {
                var indentedBackConversionCode = backConversionCode
                    .IndentAllLines(1);

                sb.AppendLine(indentedBackConversionCode);
            }
            
            sb.AppendLine();
        }
        
        string returnCode = string.Empty;

        if (isReturning) {
            string? returnTypeConversion = returnTypeDescriptor.GetTypeConversion(
                CodeLanguage.Swift,
                CodeLanguage.C,
                Nullability.NotSpecified // TODO
            );
    
            if (!string.IsNullOrEmpty(returnTypeConversion)) {
                string newReturnValueName = "__returnValue";

                string returnTypeOptionalString = returnTypeIsOptional
                    ? "?"
                    : string.Empty;

                string fullReturnTypeConversion = Builder.Let(newReturnValueName)
                    .Value(string.Format(returnTypeConversion, $"{returnValueName}{returnTypeOptionalString}"))
                    .ToIndentedString(1);
    
                sb.AppendLine(fullReturnTypeConversion);
                
                if (!returnTypeIsPrimitive &&
                    !returnValueIsReadOnlySpanOfByte) {
                    string nullabilitySpecifier = returnTypeIsOptional
                        ? "?"
                        : string.Empty;
                    
                    sb.AppendLine($"\t{returnValueName}{nullabilitySpecifier}.__destroyMode = .skip // Will be destroyed by .NET");
                }
                
                sb.AppendLine();
                        
                returnValueName = newReturnValueName;
            }
    
            returnCode = $"return {returnValueName}";
        }

        if (isReturning) {
            sb.AppendLine($"\t{returnCode}");
        }
        
        sb.AppendLine("}");

        string code = Builder.Func(createCFunctionFuncName)
            .Private()
            .Static()
            .ReturnTypeName($"{cTypeName}_CFunction_t")
            .Implementation(sb.ToString())
            .ToString();

        return code;
    }

    private string WriteCreateCDestructorFunction(
        string cTypeName,
        string closureTypeTypeAliasName,
        out string createCDestructorFunctionFuncName
    )
    {
        SwiftCodeBuilder sb = new();

        string innerContextParameterName = "__innerContext";
        string fatalErrorMessageIfNoContext = "Context is nil";
        
        createCDestructorFunctionFuncName = "__createCDestructorFunction";

        sb.AppendLine($"return {{ {innerContextParameterName} in");
        sb.AppendLine($"\tguard let {innerContextParameterName} else {{ fatalError(\"{fatalErrorMessageIfNoContext}\") }}");
        sb.AppendLine();
        sb.AppendLine($"\tNativeBox<{closureTypeTypeAliasName}>.release({innerContextParameterName})");
        sb.AppendLine("}");

        string code = Builder.Func(createCDestructorFunctionFuncName)
            .Private()
            .Static()
            .ReturnTypeName($"{cTypeName}_CDestructorFunction_t")
            .Implementation(sb.ToString())
            .ToString();

        return code;
    }

    private string WriteInit(
        string cTypeName,
        string closureTypeTypeAliasName,
        string createCFunctionFuncName,
        string createCDestructorFunctionFuncName
    )
    {
        SwiftCodeBuilder sb = new();

        sb.AppendLine(Builder.Let("__cFunction")
            .Value($"Self.{createCFunctionFuncName}()").ToString());
        
        sb.AppendLine(Builder.Let("__cDestructorFunction")
            .Value($"Self.{createCDestructorFunctionFuncName}()").ToString());
        
        sb.AppendLine();
        
        sb.AppendLine(Builder.Let("__outerSwiftContext")
            .Value("NativeBox(__closure)").ToString());
        
        sb.AppendLine(Builder.Let("__outerContext")
            .Value("__outerSwiftContext.retainedPointer()").ToString());
        
        sb.AppendLine();

        string letDelegateC = Builder.Let("__delegateC")
            .Value($"{cTypeName}_Create(__outerContext, __cFunction, __cDestructorFunction)")
            .ToString();
        
        sb.AppendLine(letDelegateC);
        sb.AppendLine();
        sb.AppendLine("self.init(handle: __delegateC)");

        string initDecl = Builder.Initializer()
            .Public()
            .Convenience()
            .Parameters(Builder.FuncSignatureParameter("_", "__closure", $"@escaping {closureTypeTypeAliasName}").ToString())
            .Implementation(sb.ToString())
            .ToString();

        string code = initDecl;

        return code;
    }

    private string WriteInvoke(
        DelegateTypeInfo typeInfo,
        MethodInfo? baseTypeDelegateInvokeMethod,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        SwiftCodeBuilder sb = new();
        
        string swiftFuncParameters = SwiftMethodSyntaxWriter.WriteParameters(
            MemberKind.Method,
            null,
            Nullability.NotSpecified,
            Nullability.NotSpecified,
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

        string swiftFuncDecl = Builder.Func("invoke")
            .Public()
            .Override(isOverride)
            .Parameters(swiftFuncParameters)
            .Throws()
            .ReturnTypeName(typeInfo.SwiftReturnTypeName)
            .ToString();

        sb.AppendLine($"{swiftFuncDecl} {{");

        string exceptionCVarName = "__exceptionC";

        sb.AppendLine(Builder.Var(exceptionCVarName)
            .TypeName("System_Exception_t?")
            .ToIndentedString(1));
        
        sb.AppendLine();

        string selfConvertedVarName = "__selfC";

        sb.AppendLine(Builder.Let(selfConvertedVarName)
            .Value("self.__handle")
            .ToIndentedString(1));
        
        sb.AppendLine();

        string parameterConversions = SwiftMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.Swift,
            CodeLanguage.C,
            MemberKind.Method,
            null,
            Nullability.NotSpecified,
            Nullability.NotSpecified,
            typeInfo.ParameterInfos,
            false,
            Array.Empty<Type>(),
            Array.Empty<Type>(),
            typeDescriptorRegistry,
            out List<string> convertedParameterNamesToC,
            out _,
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
                CodeLanguage.Swift,
                Nullability.NotSpecified // TODO
            );
        
            if (!string.IsNullOrEmpty(returnTypeConversion)) {
                string newReturnValueName = "__returnValue";
        
                string fullReturnTypeConversion = Builder.Let(newReturnValueName)
                    .Value(string.Format(returnTypeConversion, $"{returnValueName}"))
                    .ToIndentedString(1);
        
                sb.AppendLine(fullReturnTypeConversion);
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