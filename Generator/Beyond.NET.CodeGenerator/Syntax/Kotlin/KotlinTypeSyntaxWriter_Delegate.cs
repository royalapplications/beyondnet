using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator.Kotlin;
using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public partial class KotlinTypeSyntaxWriter
{
    internal class DelegateTypeInfo
    {
        internal Type Type { get; }
        internal TypeDescriptor TypeDescriptor { get; }
        internal string TypeName { get; }
        internal string FullTypeName { get; }
        internal string CTypeName { get; }

        internal string KotlinTypeName { get; }

        internal Type? BaseType { get; }
        internal TypeDescriptor? BaseTypeDescriptor { get; }
        internal string KotlinBaseTypeName { get; }
        internal MethodInfo? BaseTypeDelegateInvokeMethod { get; }

        internal Type ReturnType { get; }
        internal TypeDescriptor ReturnTypeDescriptor { get; }
        internal bool IsReturning { get; }
        internal bool ReturnTypeIsPrimitive { get; }
        internal bool ReturnTypeIsOptional { get; }
        internal bool ReturnTypeIsReadOnlySpanOfByte { get; }

        internal string KotlinReturnTypeName { get; }

        internal ParameterInfo[] ParameterInfos { get; }

        internal MethodInfo? DelegateInvokeMethod { get; }

        internal DelegateTypeInfo(
            CodeLanguage targetLanguage,
            Type type,
            MethodInfo? delegateInvokeMethod,
            TypeDescriptorRegistry typeDescriptorRegistry
        ) : this(
            targetLanguage,
            type,
            delegateInvokeMethod,
            delegateInvokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>(),
            delegateInvokeMethod?.ReturnType ?? typeof(void),
            typeDescriptorRegistry
        )
        {
        }

        internal DelegateTypeInfo(
            CodeLanguage targetLanguage,
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

            KotlinTypeName = TypeDescriptor.GetTypeName(targetLanguage, false);

            ReturnType = returnType;

            if (ReturnType.IsByRef) {
                throw new Exception($"// TODO: ({KotlinTypeName}) Unsupported delegate type. Reason: Has by ref return type");
            }

            if (ReturnType.IsGenericInAnyWay(true)) {
                throw new Exception($"// TODO: ({KotlinTypeName}) Unsupported delegate type. Reason: Has generic return type");
            }

            IsReturning = !ReturnType.IsVoid();

            ReturnTypeDescriptor = ReturnType.GetTypeDescriptor(typeDescriptorRegistry);

            ReturnTypeIsPrimitive = ReturnType.IsPrimitive;
            ReturnTypeIsReadOnlySpanOfByte = ReturnType.IsReadOnlySpanOfByte();
            ReturnTypeIsOptional = ReturnTypeDescriptor.Nullability == Nullability.Nullable;

            if (IsReturning) {
                // TODO: This generates inout TypeName if the return type is by ref
                KotlinReturnTypeName = ReturnTypeDescriptor.GetTypeName(
                    targetLanguage,
                    true,
                    ReturnTypeIsOptional ? Nullability.Nullable : Nullability.NonNullable,
                    Nullability.NotSpecified, // TODO
                    false,
                    false,
                    false
                );
            } else {
                KotlinReturnTypeName = string.Empty;
            }

            foreach (var parameter in parameterInfos) {
                if (parameter.IsOut) {
                    throw new Exception($"// TODO: ({KotlinTypeName}) Unsupported delegate type. Reason: Has out parameters");
                }

                if (parameter.IsIn) {
                    throw new Exception($"// TODO: ({KotlinTypeName}) Unsupported delegate type. Reason: Has in parameters");
                }

                if (parameter.ParameterType.IsGenericInAnyWay(true)) {
                    throw new Exception($"// TODO: ({KotlinTypeName}) Unsupported delegate type. Reason: Has generic parameters");
                }

                if (!ExperimentalFeatureFlags.EnableByRefParametersInDelegates) {
                    Type parameterType = parameter.ParameterType;

                    if (parameterType.IsByRef) {
                        throw new Exception($"// TODO: ({KotlinTypeName}) Unsupported delegate type. Reason: Has by ref parameters");
                    }
                }
            }

            ParameterInfos = parameterInfos;

            BaseType = type.BaseType;
            BaseTypeDescriptor = BaseType?.GetTypeDescriptor(typeDescriptorRegistry);

            KotlinBaseTypeName = BaseTypeDescriptor?.GetTypeName(targetLanguage, false)
                                ?? "DNObject";

            if (BaseType is not null &&
                BaseType.IsDelegate()) {
                BaseTypeDelegateInvokeMethod = BaseType.GetDelegateInvokeMethod();
            } else {
                BaseTypeDelegateInvokeMethod = null;
            }
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

    // NOTE: Entry Point
    private string WriteDelegateType(
        ISyntaxWriterConfiguration? configuration,
        Type type,
        State state
    )
    {
        var kotlinConfiguration = (configuration as KotlinSyntaxWriterConfiguration)!;
        var generationPhase = kotlinConfiguration.GenerationPhase;

        var targetLanguage = generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.JNA
            ? CodeLanguage.KotlinJNA
            : CodeLanguage.Kotlin;

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
                targetLanguage,
                type,
                delegateInvokeMethod,
                typeDescriptorRegistry
            );
        } catch (Exception ex) {
            state.AddSkippedType(type);

            return ex.Message;
        }

        List<string> memberParts = new();

        // TODO: Requires Kotlin support for nested type aliases (see https://kotlinlang.org/docs/type-aliases.html#rules-for-nested-type-aliases)
        // #region Function Type Alias
        // var functionTypeAliasCode = WriteFunctionTypeAlias(
        //     kotlinConfiguration,
        //     type,
        //     typeInfo,
        //     typeInfo.ParameterInfos,
        //     typeInfo.KotlinReturnTypeName,
        //     typeDescriptorRegistry,
        //     out _
        // );
        //
        // memberParts.Add(functionTypeAliasCode);
        // #endregion Function Type Alias

        #region Callback Interface
        if (targetLanguage == CodeLanguage.KotlinJNA) {
            var callbackInterface = WriteCallbackInterface(
                typeInfo,
                typeDescriptorRegistry,
                false,
                out _
            );

            memberParts.Add(callbackInterface);
        }
        #endregion Callback Interface

        #region Destructor Callback Interface
        if (targetLanguage == CodeLanguage.KotlinJNA) {
            var destructorCallbackInterface = WriteCallbackInterface(
                typeInfo,
                typeDescriptorRegistry,
                true,
                out _
            );

            memberParts.Add(destructorCallbackInterface);
        }
        #endregion Destructor Callback Interface

        #region Create Fun (Low Level)
        if (targetLanguage == CodeLanguage.KotlinJNA) {
            var createFunCode = WriteCreateFunLowLevel(
                typeInfo,
                out _
            );

            memberParts.Add(createFunCode);
        }
        #endregion Create Fun (Low Level)

        #region Create Fun (High Level)
        if (targetLanguage == CodeLanguage.KotlinJNA) {
            var createFunHighLevelCode = WriteCreateFunHighLevel(
                kotlinConfiguration,
                typeInfo,
                typeDescriptorRegistry,
                out _
            );

            memberParts.Add(createFunHighLevelCode);
        }
        #endregion Create Fun (High Level)

        #region Context Get Fun
        if (targetLanguage == CodeLanguage.KotlinJNA) {
            var contextGetFunCode = WriteContextGetFun(
                typeInfo,
                out _
            );

            memberParts.Add(contextGetFunCode);
        }
        #endregion Context Get Fun

        #region CFunction Get Fun
        if (targetLanguage == CodeLanguage.KotlinJNA) {
            var cFunctionGetFunCode = WriteCFunctionGetFun(
                typeInfo,
                out _
            );

            memberParts.Add(cFunctionGetFunCode);
        }
        #endregion CFunction Get Fun

        #region CDestructor Get Fun
        if (targetLanguage == CodeLanguage.KotlinJNA) {
            var cDestructorFunctionGetFun = WriteCDestructorFunctionGetFun(
                typeInfo,
                out _
            );

            memberParts.Add(cDestructorFunctionGetFun);
        }
        #endregion CDestructor Get Fun

        #region Invoke Fun
        if (typeInfo.DelegateInvokeMethod is not null) {
            var invokeFun = WriteInvokeFun(
                kotlinConfiguration,
                typeInfo,
                typeDescriptorRegistry,
                out _
            );

            memberParts.Add(invokeFun);
        }
        #endregion Invoke Fun

        #region C Callback Wrapper
        if (targetLanguage == CodeLanguage.KotlinJNA) {
            var cCallbackWrapper = WriteCCallbackWrapper(
                kotlinConfiguration,
                typeInfo,
                typeDescriptorRegistry,
                out string cCallbackWrapperTypeName
            );

            memberParts.Add(cCallbackWrapper);
        }
        #endregion C Callback Wrapper

        #region Init
        if (targetLanguage == CodeLanguage.Kotlin) {
            string initCode = WriteInit(
                typeInfo,
                typeDescriptorRegistry
            );

            memberParts.Add(initCode);
        }
        #endregion Init

        string memberPartsCode = string.Join("\n\n", memberParts);

        KotlinCodeBuilder sb = new();

        sb.AppendLine(memberPartsCode);

        // #region Other Members
        // string membersCode = WriteMembers(
        //     configuration,
        //     type,
        //     state,
        //     false
        // );
        //
        // sb.AppendLine(membersCode);
        // #endregion Other Members
        //
        // string typeDecl = Builder.Class($"{typeInfo.KotlinTypeName} /* {typeInfo.FullTypeName} */")
        //     .BaseTypeName(typeInfo.KotlinBaseTypeName)
        //     .Public()
        //     .Implementation(sb.ToString())
        //     .ToString();
        //
        // var typeDocumentationComment = type.GetDocumentation()
        //     ?.GetFormattedDocumentationComment();
        //
        // KotlinCodeBuilder sbFinal;
        //
        // if (!string.IsNullOrEmpty(typeDocumentationComment)) {
        //     sbFinal = new(typeDocumentationComment + "\n");
        //     sbFinal.AppendLine(typeDecl);
        // } else {
        //     sbFinal = new(typeDecl);
        // }
        //
        // var final = sbFinal.ToString();
        //
        // return final;

        return sb.ToString();
    }

    #region C (JNA) & Kotlin Stuff

    private string WriteInit(
        DelegateTypeInfo typeInfo,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        KotlinCodeBuilder sb = new();

        #region DelegateAdapterHelper
        sb.AppendLine("private object DelegateAdapterHelper {");

        var inputFunctionTypeDecl = WriteFunctionTypeDeclaration(
            CodeLanguage.Kotlin,
            typeInfo.Type,
            typeDescriptorRegistry
        );

        var outputFunctionTypeDecl = WriteFunctionTypeDeclaration(
            CodeLanguage.KotlinJNA,
            typeInfo.Type,
            typeDescriptorRegistry
        );

        string impl;

        // TODO: Is this really a good way to compare this?!
        if (inputFunctionTypeDecl == outputFunctionTypeDecl) {
            impl = "return __callbackToConvert";
        } else {
            #region Preps
            KotlinCodeBuilder sbValue = new();
            #endregion Preps

            #region Parameter List
            var parameterNames = typeInfo.ParameterInfos.Select(p => p.Name ?? throw new Exception("Parameter without name"));
            var parameterNamesJoined = string.Join(", ", parameterNames);

            sbValue.AppendLine("{ " + parameterNamesJoined + " ->");
            #endregion Parameter List

            #region Conversion Code
            var conversions = WriteParameterAndReturnValueConversion(
                true,
                "__callbackToConvert",
                CodeLanguage.KotlinJNA,
                CodeLanguage.Kotlin,
                typeInfo,
                typeDescriptorRegistry
            );

            sbValue.AppendLine(conversions.IndentAllLines(1));

            sbValue.AppendLine("}");
            #endregion Conversion Code

            #region Finish up
            KotlinCodeBuilder sbImpl = new();

            sbImpl.AppendLine(
                Builder.Val("__convertedCallbackAdapter")
                    .TypeName(outputFunctionTypeDecl)
                    .Value(sbValue.ToString())
                    .ToString()
            );

            sbImpl.AppendLine();
            sbImpl.AppendLine("return __convertedCallbackAdapter");

            impl = sbImpl.ToString();
            #endregion Finish up
        }

        var createCallbackAdapterFun = Builder.Fun("createCallbackAdapter")
            .Parameters(Builder.FunSignatureParameter("__callbackToConvert", inputFunctionTypeDecl).ToString())
            .ReturnTypeName(outputFunctionTypeDecl)
            .Implementation(impl)
            .ToIndentedString(1);

        sb.AppendLine(createCallbackAdapterFun);

        sb.AppendLine("}");
        #endregion DelegateAdapterHelper

        sb.AppendLine();

        #region Secondary Constructor
        sb.AppendLine($"constructor(callback: {inputFunctionTypeDecl}) : this(CAPI.{typeInfo.CTypeName}_Create(DelegateAdapterHelper.createCallbackAdapter(callback))) {{ }}");
        #endregion Secondary Constructor

        var code = sb.ToString();

        return code;
    }

    private string WriteFunctionTypeDeclaration(
        CodeLanguage targetLanguage,
        Type type,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        var delegateInvokeMethod = type.GetDelegateInvokeMethod();

        DelegateTypeInfo typeInfo = new(
            targetLanguage,
            type,
            delegateInvokeMethod,
            typeDescriptorRegistry
        );

        return WriteFunctionTypeDeclaration(
            targetLanguage,
            typeInfo,
            typeDescriptorRegistry
        );
    }

    private string WriteFunctionTypeDeclaration(
        CodeLanguage targetLanguage,
        DelegateTypeInfo typeInfo,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        string kotlinFunParameters;
        string returnTypeName;

        if (targetLanguage == CodeLanguage.KotlinJNA) {
            kotlinFunParameters = KotlinMethodSyntaxWriter.WriteJNAParameters(
                MemberKind.Method,
                null,
                Nullability.NotSpecified,
                false,
                true,
                typeInfo.Type,
                typeInfo.ParameterInfos,
                false,
                Array.Empty<Type>(),
                typeDescriptorRegistry,
                targetLanguage,
                false
            );

            returnTypeName = typeInfo.KotlinReturnTypeName;
        } else {
            kotlinFunParameters = KotlinMethodSyntaxWriter.WriteParameters(
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
                false, // TODO: Is this what we want?
                null,
                typeDescriptorRegistry
            );

            returnTypeName = typeInfo.KotlinReturnTypeName;
        }

        var functionTypeDecl = Builder.FunctionType()
            .Parameters(kotlinFunParameters)
            .ReturnTypeName(returnTypeName)
            .Build()
            .ToString();

        return functionTypeDecl;
    }

    // NOTE: Unused and not even generated right now because it requires an experimental Kotlin feature
    private string WriteFunctionTypeAlias(
        KotlinSyntaxWriterConfiguration configuration,
        DelegateTypeInfo typeInfo,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string functionTypeAliasName
    )
    {
        var generationPhase = configuration.GenerationPhase;

        var targetLanguage = generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.JNA
            ? CodeLanguage.KotlinJNA
            : CodeLanguage.Kotlin;

        var cTypeName = typeInfo.CTypeName;

        if (targetLanguage == CodeLanguage.KotlinJNA) {
            var suffix = "_CFunctionType";
            functionTypeAliasName = $"{cTypeName}{suffix}";
        } else {
            // var suffix = "_FunctionType";
            // TODO
            throw new NotSupportedException();
        }

        var functionTypeDecl = WriteFunctionTypeDeclaration(
            targetLanguage,
            typeInfo,
            typeDescriptorRegistry
        );

        var typeAliasDecl = Builder.TypeAlias(
                functionTypeAliasName,
                functionTypeDecl
            )
            .Build()
            .ToString();

        return typeAliasDecl;
    }
    #endregion C (JNA) & Kotlin Stuff

    #region C Functions (JNA-only)
    private string WriteCallbackInterface(
        DelegateTypeInfo typeInfo,
        TypeDescriptorRegistry typeDescriptorRegistry,
        bool isDestructor,
        out string callbackInterfaceTypeName
    )
    {
        var cTypeName = typeInfo.CTypeName;

        var suffix = isDestructor
            ? "_CDestructorFunction_t"
            : "_CFunction_t";

        callbackInterfaceTypeName = $"{cTypeName}{suffix}";

        var callbackImpl = WriteCallbackDeclaration(
            typeInfo,
            typeDescriptorRegistry,
            isDestructor,
            null,
            out _
        );

        var interfaceDecl = new KotlinInterfaceDeclaration(
            callbackInterfaceTypeName,
            "Callback",
            null,
            KotlinVisibilities.None,
            callbackImpl
        );

        return interfaceDecl.ToString();
    }

    private string WriteCallbackDeclaration(
        DelegateTypeInfo typeInfo,
        TypeDescriptorRegistry typeDescriptorRegistry,
        bool isDestructor,
        string? funImplementation,
        out string callbackFunName
    )
    {
        string kotlinFuncParameters;

        if (isDestructor) {
            kotlinFuncParameters = Builder.FunSignatureParameter("context", "Pointer?")
                .Build()
                .ToString();
        } else {
            kotlinFuncParameters = KotlinMethodSyntaxWriter.WriteJNAParameters(
                MemberKind.Method,
                null,
                Nullability.NotSpecified,
                false,
                true,
                typeInfo.Type,
                typeInfo.ParameterInfos,
                false,
                Array.Empty<Type>(),
                typeDescriptorRegistry,
                CodeLanguage.KotlinJNA,
                true
            );
        }

        callbackFunName = isDestructor
            ? "dnDelegateDestroy"
            : "dnDelegateCallback";

        var callbackImpl = Builder.Fun(callbackFunName)
            .Parameters(kotlinFuncParameters)
            .ReturnTypeName(isDestructor ? null : typeInfo.KotlinReturnTypeName)
            .Implementation(funImplementation)
            .Override(!string.IsNullOrEmpty(funImplementation))
            .Build()
            .ToString();

        return callbackImpl;
    }

    private string WriteCreateFunLowLevel(
        DelegateTypeInfo typeInfo,
        out string createCFunctionFuncName
    )
    {
        createCFunctionFuncName = $"{typeInfo.CTypeName}_Create";

        var parameters = Builder.FunSignatureParameters()
            .AddParameter("context", "Pointer?")
            .AddParameter("function", $"{typeInfo.CTypeName}_CFunction_t")
            .AddParameter("destructorFunction", $"{typeInfo.CTypeName}_CDestructorFunction_t")
            .Build()
            .ToString();

        var code = Builder.Fun(createCFunctionFuncName)
            .External()
            .Private()
            .Parameters(parameters)
            .ReturnTypeName(typeInfo.KotlinTypeName)
            .ToString();

        return code;
    }

    private string WriteCreateFunHighLevel(
        KotlinSyntaxWriterConfiguration configuration,
        DelegateTypeInfo typeInfo,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string createCFunctionFuncName
    )
    {
        var generationPhase = configuration.GenerationPhase;

        var targetLanguage = generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.JNA
            ? CodeLanguage.KotlinJNA
            : CodeLanguage.Kotlin;

        var cTypeName = typeInfo.CTypeName;
        var kotlinTypeName = typeInfo.KotlinTypeName;

        createCFunctionFuncName = $"{cTypeName}_Create";

        var callbackFunctionTypeDecl = WriteFunctionTypeDeclaration(
            targetLanguage,
            typeInfo,
            typeDescriptorRegistry
        );

        var parameters = Builder.FunSignatureParameters()
            .AddParameter("callback", callbackFunctionTypeDecl)
            .Build()
            .ToString();

        KotlinCodeBuilder impl = new();

        impl.AppendLine(
            Builder.Val("callbackWrapper")
                .Value($"{cTypeName}_CCallbackWrapper(callback)")
                .ToString()
        );

        impl.AppendLine(
            Builder.Val("delegatePtr")
                .Value($"{cTypeName}_Create(null, callbackWrapper.cFunction, callbackWrapper.cDestructorFunction)")
                .ToString()
        );

        impl.AppendLine("");

        impl.AppendLine("return delegatePtr");

        var code = Builder.Fun(createCFunctionFuncName)
            .Parameters(parameters)
            .ReturnTypeName(kotlinTypeName)
            .Implementation(impl.ToString())
            .ToString();

        return code;
    }

    // NOTE: Unused
    private string WriteContextGetFun(
        DelegateTypeInfo typeInfo,
        out string contextGetCFunctionFuncName
    )
    {
        contextGetCFunctionFuncName = $"{typeInfo.CTypeName}_Context_Get";

        var parameters = Builder.FunSignatureParameters()
            .AddParameter("self", "Pointer")
            .Build()
            .ToString();

        var code = Builder.Fun(contextGetCFunctionFuncName)
            .External()
            .Parameters(parameters)
            .ReturnTypeName("Pointer?")
            .ToString();

        return code;
    }

    // NOTE: Unused
    private string WriteCFunctionGetFun(
        DelegateTypeInfo typeInfo,
        out string cFunctionGetCFunctionFuncName
    )
    {
        cFunctionGetCFunctionFuncName = $"{typeInfo.CTypeName}_CFunction_Get";

        var parameters = Builder.FunSignatureParameters()
            .AddParameter("self", "Pointer")
            .Build()
            .ToString();

        var code = Builder.Fun(cFunctionGetCFunctionFuncName)
            .External()
            .Parameters(parameters)
            .ReturnTypeName($"{typeInfo.CTypeName}_CFunction_t")
            .ToString();

        return code;
    }

    // NOTE: Unused
    private string WriteCDestructorFunctionGetFun(
        DelegateTypeInfo typeInfo,
        out string cCDestructorFunctionFuncName
    )
    {
        cCDestructorFunctionFuncName = $"{typeInfo.CTypeName}_CDestructorFunction_Get";

        var parameters = Builder.FunSignatureParameters()
            .AddParameter("self", "Pointer")
            .Build()
            .ToString();

        var code = Builder.Fun(cCDestructorFunctionFuncName)
            .External()
            .Parameters(parameters)
            .ReturnTypeName($"{typeInfo.CTypeName}_CDestructorFunction_t")
            .ToString();

        return code;
    }

    private string WriteInvokeFun(
        KotlinSyntaxWriterConfiguration configuration,
        DelegateTypeInfo typeInfo,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string invokeFuncName
    )
    {
        var generationPhase = configuration.GenerationPhase;

        var targetLanguage = generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.JNA
            ? CodeLanguage.KotlinJNA
            : CodeLanguage.Kotlin;

        string kotlinFunParameters;

        if (targetLanguage == CodeLanguage.KotlinJNA) {
            invokeFuncName = $"{typeInfo.CTypeName}_Invoke";

            kotlinFunParameters = KotlinMethodSyntaxWriter.WriteJNAParameters(
                MemberKind.Method,
                null,
                Nullability.NotSpecified,
                true,
                false,
                typeInfo.Type,
                typeInfo.ParameterInfos,
                false,
                Array.Empty<Type>(),
                typeDescriptorRegistry,
                CodeLanguage.KotlinJNA,
                false
            );
        } else {
            invokeFuncName = "invoke";

            kotlinFunParameters = KotlinMethodSyntaxWriter.WriteParameters(
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
                false, // TODO: Is this what we want?
                configuration,
                typeDescriptorRegistry
            );
        }

        string code;

        if (targetLanguage == CodeLanguage.KotlinJNA) {
            code = Builder.Fun(invokeFuncName)
                .External()
                .Parameters(kotlinFunParameters)
                .ReturnTypeName(typeInfo.KotlinReturnTypeName)
                .ToString();
        } else {
            var impl = WriteParameterAndReturnValueConversion(
                false,
                $"CAPI.{typeInfo.CTypeName}_Invoke",
                CodeLanguage.Kotlin,
                CodeLanguage.KotlinJNA,
                typeInfo,
                typeDescriptorRegistry
            );

            bool isOverride = typeInfo.DelegateInvokeMethodMatches(typeInfo.BaseTypeDelegateInvokeMethod);

            var funCode = Builder.Fun(invokeFuncName)
                .Open()
                .Override(isOverride)
                .Parameters(kotlinFunParameters)
                .ReturnTypeName(typeInfo.KotlinReturnTypeName)
                .Implementation(impl)
                .ToString();

            code = funCode;
        }

        return code;
    }

    private string WriteParameterAndReturnValueConversion(
        bool forCallback,
        string methodToInvoke,
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage,
        DelegateTypeInfo typeInfo,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        var parameterConversions = KotlinMethodSyntaxWriter.WriteParameterConversions(
            sourceLanguage,
            targetLanguage,
            MemberKind.Method,
            null,
            Nullability.NotSpecified,
            Nullability.NotSpecified,
            typeInfo.ParameterInfos,
            false,
            [],
            [],
            null,
            typeDescriptorRegistry,
            out List<string> convertedParameterNames,
            out _,
            out _,
            out _
        );

        KotlinCodeBuilder sbImpl = new();

        string? thisConvertedVarName;

        if (!forCallback) {
            string exceptionCVarName = "__exceptionC";

            sbImpl.AppendLine(Builder.Val(exceptionCVarName)
                .Value("PointerByReference()")
                .ToString());

            convertedParameterNames.Add(exceptionCVarName);

            sbImpl.AppendLine();

            thisConvertedVarName = "__thisC";

            sbImpl.AppendLine(Builder.Val(thisConvertedVarName)
                .Value("this.getHandleOrNull()")
                .ToString());

            sbImpl.AppendLine();
        } else {
            thisConvertedVarName = null;
        }

        sbImpl.AppendLine(parameterConversions);
        sbImpl.AppendLine();

        if (thisConvertedVarName is not null) {
            convertedParameterNames.Insert(0, thisConvertedVarName);
        }

        string allParameterNamesString = string.Join(", ", convertedParameterNames);

        string returnValueName = "__returnValueC";

        string returnValueStorage = typeInfo.IsReturning
            ? $"val {returnValueName} = "
            : string.Empty;

        string invocation = $"{returnValueStorage}{methodToInvoke}({allParameterNamesString})";

        sbImpl.AppendLine(invocation);
        sbImpl.AppendLine();

        string returnCode = string.Empty;

        if (typeInfo.IsReturning) {
            string? returnTypeConversion = typeInfo.ReturnTypeDescriptor.GetTypeConversion(
                targetLanguage,
                sourceLanguage,
                Nullability.NotSpecified // TODO
            );

            if (!string.IsNullOrEmpty(returnTypeConversion)) {
                string newReturnValueName = "__returnValue";
                var conv = string.Format(returnTypeConversion, returnValueName);

                string prefix;
                string suffix;

                Nullability actualNullability = typeInfo.ReturnTypeIsOptional
                    ? Nullability.Nullable
                    : typeInfo.ReturnTypeDescriptor.Nullability;

                if (typeInfo.ReturnTypeDescriptor.RequiresNativePointer &&
                    actualNullability != Nullability.NonNullable) {
                    prefix = $"if ({returnValueName} != null) ";
                    suffix = " else null";
                } else {
                    prefix = string.Empty;
                    suffix = string.Empty;
                }

                string fullReturnTypeConversion = Builder.Val(newReturnValueName)
                    .Value($"{prefix}{conv}{suffix}")
                    .ToString();

                sbImpl.AppendLine(fullReturnTypeConversion);
                sbImpl.AppendLine();

                returnValueName = newReturnValueName;
            }

            string returnKeyword;

            if (forCallback) {
                returnKeyword = string.Empty;
            } else {
                returnKeyword = "return ";
            }

            returnCode = $"{returnKeyword}{returnValueName}";
        }

        if (!forCallback) {
            sbImpl.AppendLine("""
                              val __exceptionCHandle = __exceptionC.value

                              if (__exceptionCHandle != null) {
                                  throw System_Exception(__exceptionCHandle).toKException()
                              }
                              """);

            sbImpl.AppendLine();
        }

        sbImpl.AppendLine();

        if (typeInfo.IsReturning) {
            sbImpl.AppendLine(returnCode);
        }

        var impl = sbImpl.ToString();

        return impl;
    }

    private string WriteCCallbackWrapper(
        KotlinSyntaxWriterConfiguration configuration,
        DelegateTypeInfo typeInfo,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string callbackWrapperTypeName
    )
    {
        var generationPhase = configuration.GenerationPhase;

        var targetLanguage = generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.JNA
            ? CodeLanguage.KotlinJNA
            : CodeLanguage.Kotlin;

        #region Preps
        const string callbackWrapperSuffix = "_CCallbackWrapper";
        callbackWrapperTypeName = $"{typeInfo.CTypeName}{callbackWrapperSuffix}";

        KotlinCodeBuilder implBuilder = new();
        #endregion Preps

        #region Variables
        implBuilder.AppendLine(
            Builder.Val("cFunction")
                .Value("CFunction(this)")
                .ToString()
        );

        implBuilder.AppendLine(
            Builder.Val("cDestructorFunction")
                .Value("CDestructorFunction(this)")
                .ToString()
        );
        #endregion Variables

        implBuilder.AppendLine();

        #region Companion Object
        implBuilder.AppendLine("companion object {");

        implBuilder.AppendLine(
            Builder.Val("_instances")
                .Value($"CopyOnWriteArrayList<{callbackWrapperTypeName}>()")
                .Private()
                .ToIndentedString(1)
        );

        implBuilder.AppendLine();

        var instanceParameter = Builder.FunSignatureParameter(
            "instance",
            callbackWrapperTypeName
        ).ToString();

        implBuilder.AppendLine(
            Builder.Fun("addInstance")
                .Private()
                .Parameters(instanceParameter)
                .Implementation("_instances.add(instance)")
                .ToIndentedString(1)
        );

        implBuilder.AppendLine();

        implBuilder.AppendLine(
            Builder.Fun("removeInstance")
                .Private()
                .Parameters(instanceParameter)
                .Implementation("_instances.add(instance)")
                .ToIndentedString(1)
        );

        implBuilder.AppendLine("}");
        #endregion Companion Object

        implBuilder.AppendLine();

        #region Init
        implBuilder.AppendLine("init {");
        implBuilder.AppendLine("addInstance(this)".IndentAllLines(1));
        implBuilder.AppendLine("}");
        #endregion Init

        implBuilder.AppendLine();

        #region Destroy
        implBuilder.AppendLine(
            Builder.Fun("destroy")
                .Private()
                .Implementation("removeInstance(this)")
                .ToString()
        );
        #endregion Destroy

        implBuilder.AppendLine();

        #region CFunction
        var cFunctionTypeName = $"{typeInfo.CTypeName}_CFunction_t";

        KotlinCodeBuilder funImpl = new();

        funImpl.AppendLine(
            Builder.Val("parent")
                .Value("WeakReference(parent)")
                .Private()
                .ToString()
        );

        funImpl.AppendLine();

        List<string> parameterNames = new();

        foreach (var parameterInfo in typeInfo.ParameterInfos) {
            var name = parameterInfo.Name ?? throw new Exception("Parameter without name");

            // TODO: Is there a better way to do this?
            if (string.Equals(name, "context", StringComparison.CurrentCultureIgnoreCase)) {
                continue;
            }

            parameterNames.Add(name);
        }

        var parameterNamesJoined = string.Join(", ", parameterNames);
        var callbackInvocationImplPrefix = typeInfo.IsReturning ? "return " : string.Empty;
        var callbackInvocationImpl = $"{callbackInvocationImplPrefix}parent.get()!!.callback({parameterNamesJoined})";

        KotlinCodeBuilder callbackImplBuilder = new();

        callbackImplBuilder.AppendLine("try {");
        callbackImplBuilder.AppendLine($"\t{callbackInvocationImpl}");
        callbackImplBuilder.AppendLine("} catch (t: Throwable) {");
        callbackImplBuilder.AppendLine("\terror(\"Error: Throwing inside .NET delegates is not supported and results in undefined behavior. Thrown Exception: ${t.toString()}\")");
        callbackImplBuilder.AppendLine("}");

        var callbackImpl = callbackImplBuilder.ToString();

        funImpl.AppendLine(
            WriteCallbackDeclaration(
                typeInfo,
                typeDescriptorRegistry,
                false,
                callbackImpl,
                out _
            )
        );

        implBuilder.AppendLine(
            new KotlinClassDeclaration(
                "CFunction",
                null,
                cFunctionTypeName,
                KotlinVisibilities.Private,
                new([new("parent", callbackWrapperTypeName)]),
                [],
                funImpl.ToString()
            ).ToString()
        );
        #endregion CFunction

        implBuilder.AppendLine();

        #region CDestructorFunction
        var cDestructorFunctionTypeName = $"{typeInfo.CTypeName}_CDestructorFunction_t";

        KotlinCodeBuilder destructorImpl = new();

        destructorImpl.AppendLine(
            Builder.Val("parent")
                .Value("WeakReference(parent)")
                .Private()
                .ToString()
        );

        destructorImpl.AppendLine();

        destructorImpl.AppendLine(
            WriteCallbackDeclaration(
                typeInfo,
                typeDescriptorRegistry,
                true,
                "parent.get()?.destroy()",
                out _
            )
        );

        implBuilder.AppendLine(
            new KotlinClassDeclaration(
                "CDestructorFunction",
                null,
                cDestructorFunctionTypeName,
                KotlinVisibilities.Private,
                new([new("parent", callbackWrapperTypeName)]),
                [],
                destructorImpl.ToString()
            ).ToString()
        );
        #endregion CDestructorFunction

        #region Class Decl
        var impl = implBuilder.ToString();

        var functionTypeDecl = WriteFunctionTypeDeclaration(
            targetLanguage,
            typeInfo,
            typeDescriptorRegistry
        );

        var ctorParams = Builder.FunSignatureParameters()
            .AddParameter("val callback", functionTypeDecl)
            .Build();

        var classDecl = new KotlinClassDeclaration(
            callbackWrapperTypeName,
            null,
            null,
            KotlinVisibilities.Private,
            ctorParams,
            [],
            impl
        );

        var classDeclStr = classDecl.ToString();

        return classDeclStr;
        #endregion Class Decl
    }
    #endregion C Functions (JNA-only)
}
