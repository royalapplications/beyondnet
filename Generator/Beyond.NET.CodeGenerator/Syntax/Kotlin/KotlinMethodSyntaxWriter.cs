using System.Reflection;

using Beyond.NET.CodeGenerator.Collectors;
using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.Kotlin;
using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public class KotlinMethodSyntaxWriter: IKotlinSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((MethodInfo)@object, state, configuration);
    }

    public string Write(MethodInfo method, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedMember(method) ?? throw new Exception("No C# generated member");
        GeneratedMember cGeneratedMember = cResult.GetGeneratedMember(method) ?? throw new Exception("No C generated member");

        bool mayThrow = cSharpGeneratedMember.MayThrow;
        const MemberKind methodKind = MemberKind.Method;

        bool isStaticMethod = method.IsStatic;

        Type declaringType = method.DeclaringType ?? throw new Exception("No declaring type");
        Type returnType = method.ReturnType;
        IEnumerable<ParameterInfo> parameters = method.GetParameters();

        string methodCode = WriteMethod(
            cSharpGeneratedMember,
            cGeneratedMember,
            method,
            methodKind,
            isStaticMethod,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            configuration,
            true,
            typeDescriptorRegistry,
            state,
            method,
            out _
        );

        return methodCode;
    }

    protected string WriteMethod(
        GeneratedMember cSharpGeneratedMember,
        GeneratedMember cMember,
        MemberInfo? memberInfo,
        MemberKind memberKind,
        bool isStaticMethod,
        bool mayThrow,
        Type declaringType,
        Type returnOrSetterOrEventHandlerType,
        IEnumerable<ParameterInfo> parameters,
        ISyntaxWriterConfiguration? syntaxWriterConfiguration,
        bool addToState,
        TypeDescriptorRegistry typeDescriptorRegistry,
        State state,
        MemberInfo? originatingMemberInfo,
        out string generatedName
    )
    {
        var kotlinConfiguration = (syntaxWriterConfiguration as KotlinSyntaxWriterConfiguration)!;
        var generationPhase = kotlinConfiguration.GenerationPhase;

        if (generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.JNA) {
            return WriteJNAMethod(
                cSharpGeneratedMember,
                cMember,
                memberInfo,
                memberKind,
                isStaticMethod,
                mayThrow,
                declaringType,
                returnOrSetterOrEventHandlerType,
                parameters,
                kotlinConfiguration,
                addToState,
                typeDescriptorRegistry,
                state,
                originatingMemberInfo,
                out generatedName
            );
        } else if (generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.KotlinBindings) {
            return WriteKotlinMethod(
                cSharpGeneratedMember,
                cMember,
                memberInfo,
                memberKind,
                isStaticMethod,
                mayThrow,
                declaringType,
                returnOrSetterOrEventHandlerType,
                parameters,
                kotlinConfiguration,
                addToState,
                typeDescriptorRegistry,
                state,
                originatingMemberInfo,
                out generatedName
            );
        }

        throw new Exception($"Unknown generation phase: {generationPhase}");
    }

    #region JNA
    protected string WriteJNAMethod(
        GeneratedMember cSharpGeneratedMember,
        GeneratedMember cMember,
        MemberInfo? memberInfo,
        MemberKind memberKind,
        bool isStaticMethod,
        bool mayThrow,
        Type declaringType,
        Type returnOrSetterOrEventHandlerType,
        IEnumerable<ParameterInfo> parameters,
        KotlinSyntaxWriterConfiguration syntaxWriterConfiguration,
        bool addToState,
        TypeDescriptorRegistry typeDescriptorRegistry,
        State state,
        MemberInfo? originatingMemberInfo,
        out string generatedName
    )
    {
        if (memberInfo == null &&
            memberKind != MemberKind.Destructor &&
            memberKind != MemberKind.TypeOf) {
            throw new Exception("memberInfo may only be null when memberKind is Destructor");
        }
        
        MethodBase? methodBase = memberInfo as MethodBase;

        bool isGenericType = declaringType.IsGenericType ||
                             declaringType.IsGenericTypeDefinition;
        
        Type[] genericTypeArguments = Array.Empty<Type>();
        int numberOfGenericTypeArguments = 0;

        if (isGenericType) {
            genericTypeArguments = declaringType.GetGenericArguments();
            numberOfGenericTypeArguments = genericTypeArguments.Length;
        }
        
        bool isGeneric = false;
        Type[] genericMethodArguments = Array.Empty<Type>();
        int numberOfGenericMethodArguments = 0;

        if (methodBase is not null) {
            isGeneric = isGenericType ||
                        methodBase.IsGenericMethod ||
                        methodBase.IsConstructedGenericMethod ||
                        methodBase.IsGenericMethodDefinition ||
                        methodBase.ContainsGenericParameters;

            if (isGeneric) {
                try {
                    genericMethodArguments = methodBase.GetGenericArguments();
                } catch {
                    genericMethodArguments = Array.Empty<Type>();
                }
                
                numberOfGenericMethodArguments = genericMethodArguments.Length;
            }
        } else if (isGenericType &&
                   memberKind != MemberKind.Destructor &&
                   memberKind != MemberKind.TypeOf) {
            isGeneric = true;
        }

        if (isGenericType &&
            numberOfGenericTypeArguments <= 0) {
            throw new Exception("Generic Type without generic arguments");
        }

        if (isGeneric &&
            !isGenericType &&
            numberOfGenericMethodArguments <= 0) {
            throw new Exception("Generic Method without generic arguments");
        }
        
        List<Type> tempCombinedGenericArguments = new();
        
        tempCombinedGenericArguments.AddRange(genericTypeArguments);
        tempCombinedGenericArguments.AddRange(genericMethodArguments);

        Type[] combinedGenericArguments = tempCombinedGenericArguments.ToArray();

        if (isGeneric) {
            foreach (var parameter in parameters) {
                bool isOutOrInOrByRef = parameter.IsOut ||
                                        parameter.IsIn ||
                                        parameter.ParameterType.IsByRef;

                if (isOutOrInOrByRef) {
                    Type nonByRefParameterType = parameter.ParameterType.GetNonByRefType();

                    if (nonByRefParameterType.IsArray) {
                        generatedName = string.Empty;
                        
                        return Builder.SingleLineComment("TODO: Generic Methods with out/in/ref parameters that are arrays are not supported").ToString();    
                    }
                }
            }
        }
        
        string methodNameC = cSharpGeneratedMember.GetGeneratedName(CodeLanguage.CSharpUnmanaged) ?? throw new Exception("No native name");

        if (addToState) {
            state.AddGeneratedMember(
                memberKind,
                memberInfo,
                mayThrow,
                methodNameC,
                CodeLanguage.KotlinJNA
            );
        }

        bool isNullableValueTypeReturnType = returnOrSetterOrEventHandlerType.IsNullableValueType(out Type? nullableValueReturnType);
        
        bool isGenericReturnType = !isNullableValueTypeReturnType &&
                                   (
                                       returnOrSetterOrEventHandlerType.IsGenericParameter ||
                                       returnOrSetterOrEventHandlerType.IsGenericMethodParameter ||
                                       returnOrSetterOrEventHandlerType.IsGenericType ||
                                       returnOrSetterOrEventHandlerType.IsGenericTypeDefinition ||
                                       returnOrSetterOrEventHandlerType.IsGenericTypeParameter ||
                                       returnOrSetterOrEventHandlerType.IsConstructedGenericType
                                    );

        bool isConstructedGenericReturnType = !isNullableValueTypeReturnType &&
                                              returnOrSetterOrEventHandlerType.IsConstructedGenericType;
        
        bool isGenericArrayReturnType = false;

        if (!isGenericReturnType &&
            returnOrSetterOrEventHandlerType.IsArray) {
            Type? arrayElementType = returnOrSetterOrEventHandlerType.GetElementType();

            if (arrayElementType is not null) {
                bool isGenericArray = arrayElementType.IsGenericParameter ||
                                      arrayElementType.IsGenericMethodParameter ||
                                      arrayElementType.IsGenericType ||
                                      arrayElementType.IsGenericTypeDefinition ||
                                      arrayElementType.IsGenericTypeParameter ||
                                      arrayElementType.IsConstructedGenericType;

                isGenericReturnType = isGenericArray;
                isGenericArrayReturnType = isGenericArray;
            }
        }

        if (isGenericReturnType) {
            if (isGenericArrayReturnType) {
                returnOrSetterOrEventHandlerType = typeof(Array);
            } else if (!isConstructedGenericReturnType) {
                returnOrSetterOrEventHandlerType = typeof(object);
            }
        }

        var nullabilityInfoContext = new NullabilityInfoContext();
        Nullability returnOrSetterTypeNullability = Nullability.NotSpecified;

        if (memberKind == MemberKind.TypeOf) {
            returnOrSetterTypeNullability = Nullability.NonNullable;
        } else if (isNullableValueTypeReturnType) {
            returnOrSetterTypeNullability = Nullability.Nullable;
        } else if (returnOrSetterOrEventHandlerType.IsReferenceType() &&
                   !returnOrSetterOrEventHandlerType.IsByRefValueType(out bool nonByRefTypeIsStruct) &&
                   !nonByRefTypeIsStruct) {
            if (memberInfo is MethodInfo methodInfo) {
                ParameterInfo returnOrSetterValueParameter;

                if (memberKind == MemberKind.PropertySetter ||
                    memberKind == MemberKind.EventHandlerAdder ||
                    memberKind == MemberKind.EventHandlerRemover) {
                    returnOrSetterValueParameter = methodInfo.GetParameters().LastOrDefault() ?? methodInfo.ReturnParameter;
                } else {
                    returnOrSetterValueParameter = methodInfo.ReturnParameter;
                }
                
                var nullabilityInfo = nullabilityInfoContext.Create(returnOrSetterValueParameter);

                if (memberKind == MemberKind.PropertyGetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.PropertySetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.EventHandlerAdder ||
                           memberKind == MemberKind.EventHandlerRemover) {
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                } else { // Method
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                }
            } else if (memberInfo is ConstructorInfo) {
                // Constructors in C# are never expected to return null
                returnOrSetterTypeNullability = Nullability.NonNullable;
            } else if (memberInfo is FieldInfo fieldInfo) {
                var nullabilityInfo = nullabilityInfoContext.Create(fieldInfo);

                if (memberKind == MemberKind.FieldGetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.FieldSetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else { // Hmm, not sure this can ever happen
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                }
            }
        }

        bool returnOrSetterOrEventHandlerTypeIsByRef;

        if (isNullableValueTypeReturnType) {
            returnOrSetterOrEventHandlerTypeIsByRef = true;
            returnOrSetterOrEventHandlerType = nullableValueReturnType ?? returnOrSetterOrEventHandlerType;
        } else {
            returnOrSetterOrEventHandlerTypeIsByRef = returnOrSetterOrEventHandlerType.IsByRef;

            if (returnOrSetterOrEventHandlerTypeIsByRef) {
                returnOrSetterOrEventHandlerType = returnOrSetterOrEventHandlerType.GetNonByRefType();
            }   
        }
        
        TypeDescriptor returnOrSetterTypeDescriptor = returnOrSetterOrEventHandlerType.GetTypeDescriptor(typeDescriptorRegistry);

        string? kotlinJNAReturnOrSetterTypeName;

        if (returnOrSetterTypeDescriptor.IsVoid) {
            kotlinJNAReturnOrSetterTypeName = null;
        } else {
            kotlinJNAReturnOrSetterTypeName = returnOrSetterTypeDescriptor.GetTypeName(
                CodeLanguage.KotlinJNA, 
                true,
                returnOrSetterTypeNullability,
                Nullability.NotSpecified,
                false,
                returnOrSetterOrEventHandlerTypeIsByRef,
                false
            );   
        }
        
        string? kotlinJNAReturnOrSetterTypeNameWithComment;
        Type? setterType;
        
        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            kotlinJNAReturnOrSetterTypeNameWithComment = null;
            setterType = returnOrSetterOrEventHandlerType;
        } else {
            if (!string.IsNullOrEmpty(kotlinJNAReturnOrSetterTypeName)) {
                kotlinJNAReturnOrSetterTypeNameWithComment = $"{kotlinJNAReturnOrSetterTypeName} /* {returnOrSetterOrEventHandlerType.GetFullNameOrName()} */";
            } else {
                kotlinJNAReturnOrSetterTypeNameWithComment = null;
            }
            
            setterType = null;
        }

        Nullability finalSetterOrEventHandlerTypeNullability;

        if (isNullableValueTypeReturnType) {
            finalSetterOrEventHandlerTypeNullability = Nullability.Nullable;
        } else if (returnOrSetterTypeNullability == Nullability.NonNullable) {
            finalSetterOrEventHandlerTypeNullability = Nullability.NonNullable;
        } else {
            finalSetterOrEventHandlerTypeNullability = Nullability.NotSpecified;
        }

        string methodSignatureParameters = WriteJNAParameters(
            memberKind,
            setterType,
            finalSetterOrEventHandlerTypeNullability,
            mayThrow,
            isStaticMethod,
            declaringType,
            parameters,
            isGeneric,
            combinedGenericArguments,
            typeDescriptorRegistry,
            CodeLanguage.KotlinJNA
        );
        
        KotlinCodeBuilder sb = new();

        // if (string.IsNullOrEmpty(methodSignatureParameters)) {
        //     methodSignatureParameters = "void";
        // }

        var fun = Builder.Fun(methodNameC)
            .External()
            .Parameters(methodSignatureParameters)
            .ReturnTypeName(kotlinJNAReturnOrSetterTypeNameWithComment);

        sb.AppendLine(fun.ToString());

        generatedName = methodNameC;
        
        return sb.ToString();
    }
    
    internal static string WriteJNAParameters(
        MemberKind memberKind,
        Type? setterOrEventHandlerType,
        Nullability setterOrEventHandlerTypeNullability,
        bool mayThrow,
        bool isStatic,
        Type declaringType,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericArguments,
        TypeDescriptorRegistry typeDescriptorRegistry,
        CodeLanguage targetLanguage
    )
    {
        var nullabilityContext = new NullabilityInfoContext();
        
        List<KotlinFunSignatureParameter> parameterList = new();

        if (!isStatic) {
            TypeDescriptor declaringTypeDescriptor = declaringType.GetTypeDescriptor(typeDescriptorRegistry);
            
            string declaringTypeName = declaringTypeDescriptor.GetTypeName(targetLanguage, true);
            
            string selfParameterName = "self";
            var selfParameter = new KotlinFunSignatureParameter(selfParameterName, $"{declaringTypeName} /* {declaringType.GetFullNameOrName()} */");

            parameterList.Add(selfParameter);
        }
        
        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            string nativeSystemTypeTypeName = systemTypeTypeDescriptor.GetTypeName(targetLanguage, true);
            
            foreach (var genericArgumentType in genericArguments) {
                string parameterName = genericArgumentType.Name
                    .EscapedKotlinName();
            
                var kotlinParameter = new KotlinFunSignatureParameter(parameterName, $"{nativeSystemTypeTypeName} /* {systemTypeTypeName} */");
            
                parameterList.Add(kotlinParameter);
            }
        }
        
        foreach (var parameter in parameters) {
            bool isOutParameter = parameter.IsOut;
            bool isInParameter = parameter.IsIn;
                
            Type parameterType = parameter.ParameterType;
                
            bool isByRefParameter = parameterType.IsByRef;

            if (isByRefParameter) {
                parameterType = parameterType.GetNonByRefType();
            }
                
            bool isGenericParameterType = parameterType.IsGenericParameter || parameterType.IsGenericMethodParameter;
                
            if (isGenericParameterType) {
                parameterType = typeof(object);
            }
                
            bool isGenericArrayParameterType = false;
            Type? arrayType = parameterType.GetElementType();
                        
            if (parameterType.IsArray &&
                arrayType is not null &&
                (arrayType.IsGenericParameter || arrayType.IsGenericMethodParameter)) {
                isGenericArrayParameterType = true;
            }

            if (isGenericArrayParameterType) {
                parameterType = typeof(Array);
            }

            bool isNotNull = false;

            if (!isGeneric &&
                !isGenericParameterType &&
                !isGenericArrayParameterType &&
                parameterType.IsReferenceType()) {
                var parameterNullabilityInfo = nullabilityContext.Create(parameter);

                if (parameterNullabilityInfo.ReadState == parameterNullabilityInfo.WriteState) {
                    isNotNull = parameterNullabilityInfo.ReadState == NullabilityState.NotNull;
                }
            }
            
            Nullability parameterNullability = isNotNull
                ? Nullability.NonNullable
                : Nullability.NotSpecified;
            
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
                
            string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(
                targetLanguage,
                true,
                parameterNullability,
                Nullability.NotSpecified,
                isOutParameter,
                isByRefParameter,
                isInParameter
            );
            
            var parameterName = parameter.Name
                ?.EscapedKotlinName() ?? throw new Exception("Parameter without a name");

            var kotlinParameter = new KotlinFunSignatureParameter(parameterName, $"{unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */");
            
            parameterList.Add(kotlinParameter);
        }

        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            if (setterOrEventHandlerType == null) {
                throw new Exception("Setter or Event Handler Type may not be null");
            }
            
            TypeDescriptor setterOrEventHandlerTypeDescriptor = setterOrEventHandlerType.GetTypeDescriptor(typeDescriptorRegistry);
            
            string cSetterOrEventHandlerTypeName = setterOrEventHandlerTypeDescriptor.GetTypeName(
                targetLanguage,
                true,
                setterOrEventHandlerTypeNullability
            );
    
            var kotlinParameter = new KotlinFunSignatureParameter("value", $"{cSetterOrEventHandlerTypeName} /* {setterOrEventHandlerType.GetFullNameOrName()} */");
            
            parameterList.Add(kotlinParameter);
        }

        if (mayThrow) {
            Type exceptionType = typeof(Exception);
            TypeDescriptor outExceptionTypeDescriptor = exceptionType.GetTypeDescriptor(typeDescriptorRegistry);
            
            string outExceptionTypeName = outExceptionTypeDescriptor.GetTypeName(
                targetLanguage,
                true,
                Nullability.NotSpecified,
                Nullability.NotSpecified,
                true,
                true,
                false
            );
            
            string outExceptionParameterName = "outException";
            var outExceptionParameter = new KotlinFunSignatureParameter(outExceptionParameterName, $"{outExceptionTypeName} /* {exceptionType.GetFullNameOrName()} */");
 
            parameterList.Add(outExceptionParameter);
        }

        var funSigParams = new KotlinFunSignatureParameters(parameterList);

        string parametersString = funSigParams.ToString();

        return parametersString;
    }
    #endregion JNA

    #region Kotlin
    protected string WriteKotlinMethod(
        GeneratedMember cSharpGeneratedMember,
        GeneratedMember cMember,
        MemberInfo? memberInfo,
        MemberKind memberKind,
        bool isStaticMethod,
        bool mayThrow,
        Type declaringType,
        Type returnOrSetterOrEventHandlerType,
        IEnumerable<ParameterInfo> parameters,
        KotlinSyntaxWriterConfiguration syntaxWriterConfiguration,
        bool addToState,
        TypeDescriptorRegistry typeDescriptorRegistry,
        State state,
        MemberInfo? originatingMemberInfo,
        out string generatedName
    )
    {
        #region Preparation
        if (memberInfo == null &&
            memberKind != MemberKind.Destructor &&
            memberKind != MemberKind.TypeOf) {
            throw new Exception("memberInfo may only be null when memberKind is Destructor");
        }

        #region TODO: Unsupported Stuff
        if (returnOrSetterOrEventHandlerType.IsByRef) {
            generatedName = string.Empty;
            
            return $"// TODO: Method with by ref return or setter or event handler type ({cMember.GetGeneratedName(CodeLanguage.C)})";
        }
        #endregion TODO: Unsupported Stuff

        // TODO: Interfaces
        // var interfaceGenerationPhase = (syntaxWriterConfiguration as SwiftSyntaxWriterConfiguration)?.InterfaceGenerationPhase ?? SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.NoInterface;
        
        MethodBase? methodBase = memberInfo as MethodBase;
        MethodInfo? methodInfo = methodBase as MethodInfo;

        bool isGenericType = declaringType.IsGenericType ||
                             declaringType.IsGenericTypeDefinition;
        
        Type[] genericTypeArguments = Array.Empty<Type>();
        int numberOfGenericTypeArguments = 0;

        if (isGenericType) {
            genericTypeArguments = declaringType.GetGenericArguments();
            numberOfGenericTypeArguments = genericTypeArguments.Length;
        }
        
        bool isGeneric = false;
        Type[] genericMethodArguments = Array.Empty<Type>();
        int numberOfGenericMethodArguments = 0;

        if (methodBase is not null) {
            isGeneric = isGenericType ||
                        methodBase.IsGenericMethod ||
                        methodBase.IsConstructedGenericMethod ||
                        methodBase.IsGenericMethodDefinition ||
                        methodBase.ContainsGenericParameters;

            if (isGeneric) {
                try {
                    genericMethodArguments = methodBase.GetGenericArguments();
                } catch {
                    genericMethodArguments = Array.Empty<Type>();
                }
                
                numberOfGenericMethodArguments = genericMethodArguments.Length;
            }
        } else if (isGenericType &&
                   memberKind != MemberKind.Destructor &&
                   memberKind != MemberKind.TypeOf) {
            isGeneric = true;
        }

        if (isGenericType &&
            numberOfGenericTypeArguments <= 0) {
            throw new Exception("Generic Type without generic arguments");
        }

        if (isGeneric &&
            !isGenericType &&
            numberOfGenericMethodArguments <= 0) {
            throw new Exception("Generic Method without generic arguments");
        }

        List<Type> tempCombinedGenericArguments = new();
        
        tempCombinedGenericArguments.AddRange(genericTypeArguments);
        tempCombinedGenericArguments.AddRange(genericMethodArguments);

        Type[] combinedGenericArguments = tempCombinedGenericArguments.ToArray();

        if (isGeneric) {
            foreach (var parameter in parameters) {
                bool isOutOrInOrByRef = parameter.IsOut ||
                                        parameter.IsIn ||
                                        parameter.ParameterType.IsByRef;

                if (isOutOrInOrByRef) {
                    Type nonByRefParameterType = parameter.ParameterType.GetNonByRefType();

                    if (nonByRefParameterType.IsArray) {
                        generatedName = string.Empty;
                        
                        return "// TODO: Generic Methods with out/in/ref parameters that are arrays are not supported";    
                    }
                }
            }
        }
        
        string cMethodName = cSharpGeneratedMember.GetGeneratedName(CodeLanguage.CSharpUnmanaged) ?? throw new Exception("No native name");

        // TODO
        cMethodName = $"BeyondDotNETSampleNative.{cMethodName}";
        
        // string methodNameKotlin = state.UniqueGeneratedName(
        //     memberKind.KotlinName(memberInfo),
        //     CodeLanguage.Kotlin
        // );

        string methodNameKotlin = memberKind.KotlinName(memberInfo);

        bool treatAsOverridden = false;

        if (methodInfo is not null) {
            bool isActuallyOverridden = methodInfo.IsOverridden(out bool overrideNullabilityIsCompatible);

            if (isActuallyOverridden) {
                if (overrideNullabilityIsCompatible) {
                    treatAsOverridden = true;
                }
            } else {
                // TODO: Is this really Kotlin or Kotlin JNA?!
                bool isShadowed = methodInfo.IsShadowed(
                    CodeLanguage.Kotlin,
                    out bool shadowNullabilityIsCompatible
                );

                if (isShadowed) {
                    if (shadowNullabilityIsCompatible) {
                        treatAsOverridden = true;
                    }
                }
            }
        } else if (memberInfo is FieldInfo fieldInfo) {
            bool isShadowed = fieldInfo.IsShadowed();

            if (isShadowed) {
                treatAsOverridden = true;
            }
        }

        if (addToState) {
            state.AddGeneratedMember(
                memberKind,
                memberInfo,
                mayThrow,
                methodNameKotlin,
                CodeLanguage.Kotlin
            );
        }
        
        bool isGenericReturnType = returnOrSetterOrEventHandlerType.IsGenericParameter ||
                                   returnOrSetterOrEventHandlerType.IsGenericMethodParameter ||
                                   returnOrSetterOrEventHandlerType.IsGenericType ||
                                   returnOrSetterOrEventHandlerType.IsGenericTypeDefinition ||
                                   returnOrSetterOrEventHandlerType.IsGenericTypeParameter ||
                                   returnOrSetterOrEventHandlerType.IsConstructedGenericType;

        bool isConstructedGenericReturnType = returnOrSetterOrEventHandlerType.IsConstructedGenericType;
        
        bool isGenericArrayReturnType = false;

        if (!isGenericReturnType &&
            returnOrSetterOrEventHandlerType.IsArray) {
            Type? arrayElementType = returnOrSetterOrEventHandlerType.GetElementType();

            if (arrayElementType is not null) {
                bool isGenericArray = arrayElementType.IsGenericParameter ||
                                      arrayElementType.IsGenericMethodParameter ||
                                      arrayElementType.IsGenericType ||
                                      arrayElementType.IsGenericTypeDefinition ||
                                      arrayElementType.IsGenericTypeParameter ||
                                      arrayElementType.IsConstructedGenericType;

                isGenericReturnType = isGenericArray;
                isGenericArrayReturnType = isGenericArray;
            }
        }

        if (isGenericReturnType) {
            if (isGenericArrayReturnType) {
                returnOrSetterOrEventHandlerType = typeof(Array);
            } else if (!isConstructedGenericReturnType) {
                returnOrSetterOrEventHandlerType = typeof(object);
            }
        }

        bool returnOrSetterOrEventHandlerTypeIsByRef = returnOrSetterOrEventHandlerType.IsByRef;

        if (returnOrSetterOrEventHandlerTypeIsByRef) {
            returnOrSetterOrEventHandlerType = returnOrSetterOrEventHandlerType.GetNonByRefType();
        }
        
        TypeDescriptor returnOrSetterTypeDescriptor = returnOrSetterOrEventHandlerType.GetTypeDescriptor(typeDescriptorRegistry);
        
        var nullabilityInfoContext = new NullabilityInfoContext();
        var returnOrSetterTypeNullability = Nullability.NotSpecified;
        var returnOrSetterTypeArrayElementNullability = Nullability.NotSpecified; 
        
        if (memberKind == MemberKind.TypeOf) {
            returnOrSetterTypeNullability = Nullability.NonNullable;
        } else if (returnOrSetterOrEventHandlerType.IsNullableValueType(out _)) {
            returnOrSetterTypeNullability = Nullability.Nullable;
        } else if (returnOrSetterOrEventHandlerType.IsReferenceType() &&
                   !returnOrSetterOrEventHandlerType.IsByRefValueType(out bool nonByRefTypeIsStruct) &&
                   !nonByRefTypeIsStruct) {
            if (methodInfo is not null) {
                ParameterInfo returnOrSetterValueParameter;

                if (memberKind == MemberKind.PropertySetter ||
                    memberKind == MemberKind.EventHandlerAdder ||
                    memberKind == MemberKind.EventHandlerRemover) {
                    returnOrSetterValueParameter = methodInfo.GetParameters().LastOrDefault() ?? methodInfo.ReturnParameter;
                } else {
                    returnOrSetterValueParameter = methodInfo.ReturnParameter;
                }
                
                var nullabilityInfo = nullabilityInfoContext.Create(returnOrSetterValueParameter);

                if (memberKind == MemberKind.PropertyGetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;

                    returnOrSetterTypeArrayElementNullability = nullabilityInfo.ElementType?.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.PropertySetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                    
                    returnOrSetterTypeArrayElementNullability = nullabilityInfo.ElementType?.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.EventHandlerAdder ||
                           memberKind == MemberKind.EventHandlerRemover) {
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                } else { // Method
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }

                    var elementTypeNullabilityInfo = nullabilityInfo.ElementType;

                    if (elementTypeNullabilityInfo is not null &&
                        elementTypeNullabilityInfo.ReadState == elementTypeNullabilityInfo.WriteState) {
                        returnOrSetterTypeArrayElementNullability = elementTypeNullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                }
            } else if (memberInfo is ConstructorInfo) {
                // Constructors in C# are never expected to return null
                returnOrSetterTypeNullability = Nullability.NonNullable;
            } else if (memberInfo is FieldInfo fieldInfo) {
                var nullabilityInfo = nullabilityInfoContext.Create(fieldInfo);

                if (memberKind == MemberKind.FieldGetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                    
                    returnOrSetterTypeArrayElementNullability = nullabilityInfo.ElementType?.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.FieldSetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                    
                    returnOrSetterTypeArrayElementNullability = nullabilityInfo.ElementType?.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else { // Hmm, not sure this can ever happen
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                    
                    var elementTypeNullabilityInfo = nullabilityInfo.ElementType;

                    if (elementTypeNullabilityInfo is not null &&
                        elementTypeNullabilityInfo.ReadState == elementTypeNullabilityInfo.WriteState) {
                        returnOrSetterTypeArrayElementNullability = elementTypeNullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                }
            }
        }
        
        // TODO: This generates inout TypeName if the return type is by ref
        string kotlinReturnOrSetterTypeName = returnOrSetterTypeDescriptor.GetTypeName(
            CodeLanguage.Kotlin,
            true,
            returnOrSetterTypeNullability,
            returnOrSetterTypeArrayElementNullability,
            false,
            returnOrSetterOrEventHandlerTypeIsByRef,
            false
        );
        
        string kotlinReturnOrSetterTypeNameWithComment;
        Type? setterType;
        
        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            kotlinReturnOrSetterTypeNameWithComment = string.Empty;
            setterType = returnOrSetterOrEventHandlerType;
        } else {
            kotlinReturnOrSetterTypeNameWithComment = $"{kotlinReturnOrSetterTypeName} /* {returnOrSetterOrEventHandlerType.GetFullNameOrName()} */";
            setterType = null;
        }
        
        generatedName = methodNameKotlin;
        #endregion Preparation

        #region Func Declaration
        string? memberImpl;

        bool needImpl;

        // TODO: Interfaces
        // if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol ||
        //     interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass) {
        //     needImpl = memberKind == MemberKind.Destructor ||
        //                memberKind == MemberKind.TypeOf;
        // } else {
            needImpl = true;
        // }
        
        if (!needImpl) {
            memberImpl = null;
        } else {
            memberImpl = WriteMethodImplementation(
                cSharpGeneratedMember,
                cMember,
                memberInfo,
                memberKind,
                cMethodName,
                isGeneric,
                isStaticMethod,
                mayThrow,
                declaringType,
                returnOrSetterOrEventHandlerType,
                returnOrSetterTypeNullability,
                returnOrSetterTypeArrayElementNullability,
                returnOrSetterTypeDescriptor,
                parameters,
                genericTypeArguments,
                genericMethodArguments,
                syntaxWriterConfiguration,
                typeDescriptorRegistry
            );   
        }
        
        string methodSignatureParameters = WriteParameters(
            memberKind,
            setterType,
            returnOrSetterTypeNullability,
            returnOrSetterTypeArrayElementNullability,
            isStaticMethod,
            declaringType,
            parameters,
            isGeneric,
            combinedGenericArguments,
            false,
            false,
            typeDescriptorRegistry
        );

        string declaration;

        KotlinVisibilities memberVisibility;

        // TODO: Interfaces
        // if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol) {
        //     memberVisibility = SwiftVisibilities.None;
        // } else {
            // TODO: Internal?
            // if (memberKind == MemberKind.Destructor) {
            //     memberVisibility = KotlinVisibilities.Internal;
            // } else {
                memberVisibility = KotlinVisibilities.Public;
            // }
        // }

        if (memberKind == MemberKind.Constructor) {
            // TODO
            declaration = Builder.SingleLineComment("TODO: Constructor").ToString();
            // declaration = Builder.Initializer()
            //     .Convenience()
            //     .Visibility(memberVisibility)
            //     .Parameters(methodSignatureParameters)
            //     .Throws(mayThrow)
            //     .Implementation(memberImpl)
            //     .ToString();
        } else if (memberKind == MemberKind.Destructor) {
            // TODO
            declaration = Builder.SingleLineComment("TODO: Destructor").ToString();
            // declaration = Builder.Func(methodNameKotlin)
            //     .Visibility(memberVisibility)
            //     .Override()
            //     .Parameters(methodSignatureParameters)
            //     .Throws(mayThrow)
            //     .Implementation(memberImpl)
            //     .ToString();
        } else if (memberKind == MemberKind.TypeOf) {
            // TODO
            declaration = Builder.SingleLineComment("TODO: TypeOf").ToString();
            // bool isEnum = declaringType.IsEnum;
            //
            // string propTypeName = !returnOrSetterOrEventHandlerType.IsVoid()
            //     ? kotlinReturnOrSetterTypeNameWithComment
            //     : throw new Exception("A property must have a return type");
            //
            // declaration = Builder.GetOnlyProperty(methodNameKotlin, propTypeName)
            //     .Visibility(memberVisibility)
            //     .TypeAttachmentKind(isEnum 
            //         ? SwiftTypeAttachmentKinds.Static
            //         : SwiftTypeAttachmentKinds.Class)
            //     .Override(!isEnum)
            //     .Throws(mayThrow)
            //     .Implementation(memberImpl)
            //     .ToString();
        } else if (CanBeGeneratedAsGetOnlyProperty(memberKind, isGeneric, parameters.Any())) {
            // TODO
            declaration = Builder.SingleLineComment("TODO: Get Only Property").ToString();
            // string propTypeName = !returnOrSetterOrEventHandlerType.IsVoid()
            //     ? kotlinReturnOrSetterTypeNameWithComment
            //     : throw new Exception("A property must have a return type");
            //
            // declaration = Builder.GetOnlyProperty(methodNameKotlin, propTypeName)
            //     .Visibility(memberVisibility)
            //     .TypeAttachmentKind(isStaticMethod
            //         ? interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol || interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations ? SwiftTypeAttachmentKinds.Static : SwiftTypeAttachmentKinds.Class
            //         : SwiftTypeAttachmentKinds.Instance)
            //     .Override(treatAsOverridden)
            //     .Throws(mayThrow)
            //     .Implementation(memberImpl)
            //     .ToString();
        } else {
            // TODO: Static?
            // TODO: Throws?
            
            declaration = Builder.Fun(methodNameKotlin)
                .Visibility(memberVisibility)
                // .TypeAttachmentKind(isStaticMethod 
                //     ? interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol || interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations ? SwiftTypeAttachmentKinds.Static : SwiftTypeAttachmentKinds.Class
                //     : SwiftTypeAttachmentKinds.Instance)
                .Override(treatAsOverridden)
                .Parameters(methodSignatureParameters)
                // .Throws(mayThrow)
                .ReturnTypeName(!returnOrSetterOrEventHandlerType.IsVoid()
                    ? kotlinReturnOrSetterTypeNameWithComment
                    : null)
                .Implementation(memberImpl)
                .ToString();
        }
        #endregion Func Declaration

        XmlDocumentationMember? xmlDocumentationContent;
        
        if (originatingMemberInfo is FieldInfo originatingFieldInfo) {
            xmlDocumentationContent = originatingFieldInfo.GetDocumentation();
        } else if (originatingMemberInfo is PropertyInfo originatingPropertyInfo) {
            xmlDocumentationContent = originatingPropertyInfo.GetDocumentation();
        } else if (originatingMemberInfo is EventInfo originatingEventInfo) {
            xmlDocumentationContent = originatingEventInfo.GetDocumentation();
        } else if (originatingMemberInfo is ParameterlessStructConstructorInfo) {
            xmlDocumentationContent = null;
        } else if (originatingMemberInfo is ConstructorInfo originatingConstructorInfo) {
            xmlDocumentationContent = originatingConstructorInfo.GetDocumentation();
        } else if (originatingMemberInfo is MethodInfo originatingMethodInfo) {
            xmlDocumentationContent = originatingMethodInfo.GetDocumentation();
        } else {
            xmlDocumentationContent = null;
        }
        
        var declarationComment = xmlDocumentationContent
            ?.GetFormattedDocumentationComment();

        if (declarationComment is null &&
            originatingMemberInfo is ParameterlessStructConstructorInfo) {
            declarationComment = $"/// Initializes a new instance of the {declaringType.GetFullNameOrName()} struct.";
        }

        string declarationWithComment;

        if (!string.IsNullOrEmpty(declarationComment)) {
            declarationWithComment = declarationComment + "\n" + declaration;
        } else {
            declarationWithComment = declaration;
        }
        
        return declarationWithComment;
    }
    
    private static bool CanBeGeneratedAsGetOnlyProperty(
        MemberKind memberKind,
        bool isGeneric,
        bool hasParameters
    )
    {
        return
            (memberKind == MemberKind.PropertyGetter || memberKind == MemberKind.FieldGetter) &&
            !isGeneric &&
            !hasParameters;
    }
    
    private static string WriteMethodImplementation(
        GeneratedMember cSharpGeneratedMember,
        GeneratedMember cMember,
        MemberInfo? memberInfo,
        MemberKind memberKind,
        string cMethodName,
        bool isGeneric,
        bool isStaticMethod,
        bool mayThrow,
        Type declaringType,
        Type returnOrSetterOrEventHandlerType,
        Nullability returnOrSetterOrEventHandlerNullability,
        Nullability returnOrSetterOrEventHandlerArrayElementNullability,
        TypeDescriptor returnOrSetterTypeDescriptor,
        IEnumerable<ParameterInfo> parameters,
        IEnumerable<Type> genericTypeArguments,
        IEnumerable<Type> genericMethodArguments,
        ISyntaxWriterConfiguration? syntaxWriterConfiguration,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        // TODO: This was copied from the Swift version of the same method and modified
        
        KotlinCodeBuilder sbImpl = new();

        bool needsRegularImpl = true;

        if (memberKind == MemberKind.Destructor) {
            needsRegularImpl = false;
            
            sbImpl.AppendLine($"{cMethodName}(this.__handle)");
        } else if (memberKind == MemberKind.TypeOf) {
            needsRegularImpl = false;
            
            string returnTypeConversion = returnOrSetterTypeDescriptor.GetTypeConversion(
                CodeLanguage.KotlinJNA,
                CodeLanguage.Kotlin,
                returnOrSetterOrEventHandlerArrayElementNullability
            ) ?? "{0}";

            string invocation = string.Format(returnTypeConversion, $"{cMethodName}()");

            sbImpl.AppendLine($"return {invocation}");
        }

        if (needsRegularImpl) {
            string parameterConversions = WriteParameterConversions(
                CodeLanguage.Kotlin,
                CodeLanguage.KotlinJNA,
                memberKind,
                returnOrSetterOrEventHandlerType,
                returnOrSetterOrEventHandlerNullability,
                returnOrSetterOrEventHandlerArrayElementNullability,
                parameters,
                isGeneric,
                genericTypeArguments,
                genericMethodArguments,
                typeDescriptorRegistry,
                out List<string> convertedParameterNames,
                out List<string> convertedGenericTypeArgumentNames,
                out List<string> convertedGenericMethodArgumentNames,
                out _
            );

            sbImpl.AppendLine(parameterConversions);
            sbImpl.AppendLine();

            if (mayThrow) {
                string cExceptionVarName = "__exceptionC";
                
                // TODO
                convertedParameterNames.Add(cExceptionVarName);
                
                sbImpl.AppendLine(Builder.Val(cExceptionVarName)
                    .Value("PointerByReference()")
                    .ToString());
                
                sbImpl.AppendLine();
            }

            bool isReturning =
                memberKind != MemberKind.EventHandlerAdder &&
                memberKind != MemberKind.EventHandlerRemover &&
                memberKind != MemberKind.FieldSetter &&
                memberKind != MemberKind.PropertySetter &&
                !returnOrSetterTypeDescriptor.IsVoid;

            string returnValueName = "__returnValueC";
            
            string returnValueCStorage = isReturning
                ? $"val {returnValueName} = "
                : string.Empty;

            List<string> allParameterNames = new();

            if (!isStaticMethod) {
                allParameterNames.Add("this.__handle");
            }
            
            allParameterNames.AddRange(convertedGenericTypeArgumentNames);
            allParameterNames.AddRange(convertedGenericMethodArgumentNames);
            allParameterNames.AddRange(convertedParameterNames);
            
            string allParameterNamesString = string.Join(", ", allParameterNames);
            
            string invocation = $"{returnValueCStorage}{cMethodName}({allParameterNamesString})";
            
            sbImpl.AppendLine(invocation);
            sbImpl.AppendLine();

            string returnCode = string.Empty;

            if (isReturning) {
                if (memberKind == MemberKind.Constructor) {
                    // TODO
                    returnCode = $"this.init(handle: {returnValueName})";
                } else {
                    string? returnTypeConversion = returnOrSetterTypeDescriptor.GetTypeConversion(
                        CodeLanguage.KotlinJNA,
                        CodeLanguage.Kotlin,
                        returnOrSetterOrEventHandlerArrayElementNullability
                    );
    
                    if (!string.IsNullOrEmpty(returnTypeConversion)) {
                        string newReturnValueName = "__returnValue";
                        var conv = string.Format(returnTypeConversion, returnValueName);

                        string prefix;
                        string suffix;

                        Nullability actualNullability = returnOrSetterOrEventHandlerArrayElementNullability != Nullability.NotSpecified
                                ? returnOrSetterOrEventHandlerArrayElementNullability
                                : returnOrSetterTypeDescriptor.Nullability;
                        
                        if (returnOrSetterTypeDescriptor.RequiresNativePointer &&
                            actualNullability != Nullability.NonNullable) {
                            prefix = $"if ({returnValueName}.value !== Pointer.NULL) ";
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
    
                    returnCode = $"return {returnValueName}";
                }
            }

            KotlinCodeBuilder sbByRefParameters = new();

            foreach (var parameter in parameters) {
                Type parameterType = parameter.ParameterType;

                bool isOutParameter = parameter.IsOut;
                bool isInParameter = parameter.IsIn;
                bool isByRefParameter = parameterType.IsByRef;
                
                if (!isOutParameter &&
                    !isInParameter &&
                    !isByRefParameter) {
                    continue;
                }

                parameterType = parameterType.GetNonByRefType();

                if (parameterType.IsGenericParameter ||
                    parameterType.IsGenericMethodParameter) {
                    parameterType = typeof(object);
                }
                
                string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");
                string convertedParameterName = $"{parameterName}C";

                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

                var nullabilityInfoContext = new NullabilityInfoContext();
                var parameterNullability = nullabilityInfoContext.Create(parameter);
                var parameterArrayElementNullabilityState = parameterNullability.ElementType;
                Nullability parameterArrayElementNullability;

                if (parameterArrayElementNullabilityState is not null &&
                    parameterArrayElementNullabilityState.ReadState == parameterArrayElementNullabilityState.WriteState) {
                    parameterArrayElementNullability = parameterArrayElementNullabilityState.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else {
                    parameterArrayElementNullability = Nullability.NotSpecified;
                }

                string? parameterTypeConversion = parameterTypeDescriptor.GetTypeConversion(
                    CodeLanguage.KotlinJNA,
                    CodeLanguage.Kotlin,
                    parameterArrayElementNullability
                );

                if (string.IsNullOrEmpty(parameterTypeConversion)) {
                    continue;
                }

                if (!convertedParameterNames.Contains($"&{convertedParameterName}")) {
                    convertedParameterName = parameterName;
                }

                if (string.IsNullOrEmpty(parameterTypeConversion)) {
                    parameterTypeConversion = convertedParameterName;
                } else {
                    parameterTypeConversion = string.Format(parameterTypeConversion, convertedParameterName);
                }
                
                sbByRefParameters.AppendLine($"{parameterName} = {parameterTypeConversion}");
                sbByRefParameters.AppendLine();
            }

            string byRefParamtersCode = sbByRefParameters.ToString();

            if (!string.IsNullOrEmpty(byRefParamtersCode)) {
                sbImpl.AppendLine(byRefParamtersCode);
            }

            if (mayThrow) {
                // TODO
                sbImpl.AppendLine("""
if (__exceptionC.value !== Pointer.NULL) {
    // TODO
    // throw System_Exception(__exceptionC).toKException()
    throw Exception("TODO: Convert System.Exception to Kotlin Exception")
}
""");

                sbImpl.AppendLine();
            }

            if (isReturning) {
                sbImpl.AppendLine(returnCode);
            }
        }

        string funcImpl = sbImpl.ToString();

        return funcImpl;
    }
    
    internal static string WriteParameters(
        MemberKind memberKind,
        Type? setterOrEventHandlerType,
        Nullability setterOrEventHandlerTypeNullability,
        Nullability setterOrEventHandlerTypeArrayElementNullability,
        bool isStatic,
        Type declaringType,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericArguments,
        bool onlyWriteParameterNames,
        bool writeModifiersForInvocation,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        // TODO: This was copied from the Swift version of the same method and modified
        
        var nullabilityContext = new NullabilityInfoContext();
        
        List<string> parameterList = new();

        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            
            string nativeSystemTypeTypeName = systemTypeTypeDescriptor.GetTypeName(
                CodeLanguage.Kotlin, 
                true,
                Nullability.NonNullable
            );
            
            foreach (var genericArgumentType in genericArguments) {
                string parameterName = genericArgumentType.Name.EscapedKotlinName();

                string parameterString = onlyWriteParameterNames 
                    ? parameterName 
                    : $"{parameterName}: {nativeSystemTypeTypeName} /* {systemTypeTypeName} */";
            
                parameterList.Add(parameterString);
            }
        }
        
        foreach (var parameter in parameters) {
            bool isOutParameter = parameter.IsOut;
            bool isInParameter = parameter.IsIn;
            
            Type parameterType = parameter.ParameterType;
            
            bool isByRefParameter = parameterType.IsByRef;

            if (isByRefParameter) {
                parameterType = parameterType.GetNonByRefType();
            }
            
            bool isGenericParameterType = parameterType.IsGenericParameter || parameterType.IsGenericMethodParameter;
            
            if (isGenericParameterType) {
                parameterType = typeof(object);
            }
            
            bool isGenericArrayParameterType = false;
            Type? arrayType = parameterType.GetElementType();
                    
            if (parameterType.IsArray &&
                arrayType is not null &&
                (arrayType.IsGenericParameter || arrayType.IsGenericMethodParameter)) {
                isGenericArrayParameterType = true;
            }

            if (isGenericArrayParameterType) {
                parameterType = typeof(Array);
            }
            
            bool isNotNull = false;
            bool isArrayElementNotNull = false;

            if (!isGeneric &&
                !isGenericParameterType &&
                !isGenericArrayParameterType &&
                parameterType.IsReferenceType()) {
                var parameterNullability = nullabilityContext.Create(parameter);

                if (parameterNullability.ReadState == parameterNullability.WriteState) {
                    isNotNull = parameterNullability.ReadState == NullabilityState.NotNull;
                }

                var parameterElementTypeNullability = parameterNullability.ElementType;

                if (parameterElementTypeNullability is not null &&
                    parameterElementTypeNullability.ReadState == parameterElementTypeNullability.WriteState) {
                    isArrayElementNotNull = parameterElementTypeNullability.ReadState == NullabilityState.NotNull;
                }
            }
            
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

            string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(
                CodeLanguage.Kotlin,
                true,
                isNotNull ? Nullability.NonNullable : Nullability.NotSpecified,
                isArrayElementNotNull ? Nullability.NonNullable : Nullability.NotSpecified,
                isOutParameter,
                isByRefParameter,
                isInParameter
            );

            string? parameterName = parameter.Name
                ?.EscapedKotlinName();

            if (parameterName is null) {
                throw new Exception("Parameter without a name");
            }

            string parameterString;
            
            if (onlyWriteParameterNames) {
                if (writeModifiersForInvocation) {
                    // TODO
                    parameterString = $"{(isByRefParameter || isOutParameter ? "&" : string.Empty)}{parameterName}";
                } else {
                    parameterString = parameterName;
                }
            } else {
                parameterString = $"{parameterName}: {unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */";    
            }
            
            parameterList.Add(parameterString);
        }

        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            if (setterOrEventHandlerType == null) {
                throw new Exception("Setter or Event Handler Type may not be null");
            }
            
            TypeDescriptor setterOrEventHandlerTypeDescriptor = setterOrEventHandlerType.GetTypeDescriptor(typeDescriptorRegistry);
            
            string cSetterOrEventHandlerTypeName = setterOrEventHandlerTypeDescriptor.GetTypeName(
                CodeLanguage.Kotlin,
                true,
                setterOrEventHandlerTypeNullability,
                setterOrEventHandlerTypeArrayElementNullability
            );

            const string parameterName = "value";

            string parameterString = onlyWriteParameterNames
                ? parameterName
                : $"{parameterName}: {cSetterOrEventHandlerTypeName} /* {setterOrEventHandlerType.GetFullNameOrName()} */";
            
            parameterList.Add(parameterString);
        }

        string parametersString = string.Join(", ", parameterList);

        return parametersString;
    }
    
    internal static string WriteParameterConversions(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage,
        MemberKind memberKind,
        Type? setterOrEventHandlerType,
        Nullability setterOrEventHandlerTypeNullability,
        Nullability setterOrEventHandlerTypeArrayElementNullability,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericTypeArguments,
        IEnumerable<Type> genericMethodArguments,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out List<string> convertedParameterNames,
        out List<string> convertedGenericTypeArgumentNames,
        out List<string> convertedGenericMethodArgumentNames,
        out List<string> parameterTypeBackConversionCodes
    )
    {
        // TODO: This was copied from the Swift version of the same method and modified
        
        string convertedParameterNameSuffix;

        if (sourceLanguage == CodeLanguage.Kotlin &&
            targetLanguage == CodeLanguage.KotlinJNA) {
            convertedParameterNameSuffix = "C";
        } else if (sourceLanguage == CodeLanguage.KotlinJNA &&
                   targetLanguage == CodeLanguage.Kotlin) {
            convertedParameterNameSuffix = "Kotlin";
        } else {
            throw new Exception("Unknown language pair");
        }
        
        KotlinCodeBuilder sb = new();
        
        convertedParameterNames = new();
        convertedGenericTypeArgumentNames = new();
        convertedGenericMethodArgumentNames = new();
        parameterTypeBackConversionCodes = new();

        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            
            string systemTypeTypeConversion = systemTypeTypeDescriptor.GetTypeConversion(
                sourceLanguage,
                targetLanguage,
                Nullability.NotSpecified
            )!;
    
            foreach (var genericArgumentType in genericTypeArguments) {
                string name = genericArgumentType.Name;
                
                string convertedGenericArgumentName = $"{name}{convertedParameterNameSuffix}";
                    
                string fullTypeConversion = string.Format(systemTypeTypeConversion, name);

                string typeConversionCode = Builder.Val(convertedGenericArgumentName)
                    .Value(fullTypeConversion)
                    .ToString();
    
                sb.AppendLine(typeConversionCode);
                
                convertedGenericTypeArgumentNames.Add(convertedGenericArgumentName);
            }
            
            foreach (var genericArgumentType in genericMethodArguments) {
                string name = genericArgumentType.Name;
                
                string convertedGenericArgumentName = $"{name}{convertedParameterNameSuffix}";
                    
                string fullTypeConversion = string.Format(systemTypeTypeConversion, name);

                string typeConversionCode = Builder.Val(convertedGenericArgumentName)
                    .Value(fullTypeConversion)
                    .ToString();
    
                sb.AppendLine(typeConversionCode);
                
                convertedGenericMethodArgumentNames.Add(convertedGenericArgumentName);
            }
        }

        foreach (var parameter in parameters) {
            string? parameterName = parameter.Name
                ?.EscapedKotlinName();
            
            if (parameterName is null) {
                throw new Exception("Parameter without a name");
            }
            
            Type parameterType = parameter.ParameterType;
            bool isOutParameter = parameter.IsOut;
            bool isInParameter = parameter.IsIn;
            
            WriteParameterConversion(
                sourceLanguage,
                targetLanguage,
                parameter,
                Nullability.NotSpecified,
                parameterName,
                parameterType,
                isOutParameter,
                isInParameter,
                isGeneric,
                typeDescriptorRegistry,
                out string? typeConversionCode,
                out string convertedParameterName,
                out string? typeBackConversionCode
            );

            if (!string.IsNullOrEmpty(typeConversionCode)) {
                sb.AppendLine(typeConversionCode);
            }
            
            convertedParameterNames.Add(convertedParameterName);

            if (!string.IsNullOrEmpty(typeBackConversionCode)) {
                parameterTypeBackConversionCodes.Add(typeBackConversionCode);
            }
        }

        if (memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            string parameterName = "value";
            Type parameterType = setterOrEventHandlerType ?? throw new Exception("No setter or event handler type");
            const bool isOutParameter = false;
            const bool isInParameter = false;
            
            WriteParameterConversion(
                sourceLanguage,
                targetLanguage,
                null, // TODO
                setterOrEventHandlerTypeNullability,
                parameterName,
                parameterType,
                isOutParameter,
                isInParameter,
                isGeneric,
                typeDescriptorRegistry,
                out string? typeConversionCode,
                out string convertedParameterName,
                out string? typeBackConversionCode
            );

            if (!string.IsNullOrEmpty(typeConversionCode)) {
                sb.AppendLine(typeConversionCode);
            }
            
            convertedParameterNames.Add(convertedParameterName);
            
            if (!string.IsNullOrEmpty(typeBackConversionCode)) {
                parameterTypeBackConversionCodes.Add(typeBackConversionCode);
            }
        }

        return sb.ToString();
    }
    
    private static void WriteParameterConversion(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage,
        ParameterInfo? parameterInfo,
        Nullability parameterNullability,
        string parameterName,
        Type parameterType,
        bool isOutParameter,
        bool isInParameter,
        bool isGeneric,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string? typeConversionCode,
        out string convertedParameterName,
        out string? typeBackConversionCode
    )
    {
        // TODO: This was copied from the Swift version of the same method and modified
        
        if (string.IsNullOrEmpty(parameterName)) {
            throw new Exception("Parameter has no name");   
        }
        
        var nullabilityContext = new NullabilityInfoContext();
        
        string convertedParameterNameSuffix;

        if (sourceLanguage == CodeLanguage.Kotlin &&
            targetLanguage == CodeLanguage.KotlinJNA) {
            convertedParameterNameSuffix = "C";
        } else if (sourceLanguage == CodeLanguage.KotlinJNA &&
                   targetLanguage == CodeLanguage.Kotlin) {
            convertedParameterNameSuffix = "Kotlin";
        } else {
            throw new Exception("Unknown language pair");
        }
        
        bool isByRefParameter = parameterType.IsByRef;
        bool isArrayType = parameterType.IsArray;
        bool isInOut = isOutParameter || isInParameter || isByRefParameter;

        if (isByRefParameter) {
            parameterType = parameterType.GetNonByRefType();
        }
        
        bool isGenericParameterType = parameterType.IsGenericParameter || parameterType.IsGenericMethodParameter;

        if (!isByRefParameter &&
            isOutParameter) {
            throw new Exception("Parameter is out but not by ref, that's impossible");
        } else if (isGenericParameterType) {
            parameterType = typeof(object);
        } else if (isArrayType) {
            if (isGeneric) {
                Type? arrayType = parameterType.GetElementType();

                if (arrayType is not null &&
                    (arrayType.IsGenericType || arrayType.IsGenericParameter || arrayType.IsGenericMethodParameter)) {
                    parameterType = typeof(Array);
                }
            }
        }
        
        TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

        Nullability parameterArrayElementNullability;
        
        if (parameterInfo is not null) {
            var nullabilityInfoContext = new NullabilityInfoContext();
            var parameterNullabilityA = nullabilityInfoContext.Create(parameterInfo);
            var parameterArrayElementNullabilityState = parameterNullabilityA.ElementType;

            if (parameterArrayElementNullabilityState is not null &&
                parameterArrayElementNullabilityState.ReadState == parameterArrayElementNullabilityState.WriteState) {
                parameterArrayElementNullability = parameterArrayElementNullabilityState.ReadState == NullabilityState.NotNull
                    ? Nullability.NonNullable
                    : Nullability.NotSpecified;
            } else {
                parameterArrayElementNullability = Nullability.NotSpecified;
            }
        } else {
            parameterArrayElementNullability = Nullability.NotSpecified;
        }

        string? typeConversion = parameterTypeDescriptor.GetTypeConversion(
            sourceLanguage,
            targetLanguage,
            parameterArrayElementNullability
        );
        
        if (typeConversion != null) {
            string parameterNameForConversion = parameterName;
            
            if (parameterName.StartsWith("`") &&
                parameterName.EndsWith("`")) {
                parameterNameForConversion = parameterName.Trim('`');
            }
            
            convertedParameterName = $"{parameterNameForConversion}{convertedParameterNameSuffix}";

            string optionalString;
            
            if (sourceLanguage == CodeLanguage.Kotlin &&
                targetLanguage == CodeLanguage.KotlinJNA) {
                // bool isNotNull = false;

                if ((typeDescriptorRegistry.GetTypeDescriptor(parameterType)?.RequiresNativePointer ?? false)) {
                    parameterNullability = Nullability.NonNullable;
                }

                // if (parameterInfo is not null &&
                //     !isGeneric &&
                //     !isGenericParameterType &&
                //     parameterType.IsReferenceType()) {
                //     bool isNotNull = false;
                //     
                //     var parameterNullabilityInfo = nullabilityContext.Create(parameterInfo);
                //     
                //     if (parameterNullabilityInfo.ReadState == parameterNullabilityInfo.WriteState) {
                //         isNotNull = parameterNullabilityInfo.ReadState == NullabilityState.NotNull;
                //     }
                //     
                //     parameterNullability = isNotNull
                //         ? Nullability.NonNullable
                //         : parameterTypeDescriptor.Nullability;
                // }

                if (parameterNullability == Nullability.NotSpecified) {
                    parameterNullability = parameterTypeDescriptor.Nullability;
                }
                
                optionalString = parameterNullability.GetKotlinOptionalitySpecifier();
            } else {
                optionalString = string.Empty;
            }

            string fullTypeConversion = string.Format(typeConversion, $"{parameterName}{optionalString}");

            typeConversionCode = Builder.Val(convertedParameterName)
                .Value(fullTypeConversion)
                .ToString();
            
            if (isInOut) {
                // TODO
                convertedParameterName = $"&{convertedParameterName}";
            }

            typeBackConversionCode = null;
        } else {
            if (sourceLanguage == CodeLanguage.KotlinJNA &&
                targetLanguage == CodeLanguage.Kotlin &&
                isInOut) {
                string kotlinParameterName = $"__{parameterName}Kotlin";
                
                // TODO
                typeConversionCode = $"val {kotlinParameterName} = {parameterName}?.pointee ?? .init()";
                typeBackConversionCode = $"{parameterName}?.pointee = {kotlinParameterName}";
                
                convertedParameterName = $"&{kotlinParameterName}";
            } else {
                typeConversionCode = null;
                typeBackConversionCode = null;
                
                if (isInOut) {
                    // TODO
                    convertedParameterName = $"&{parameterName}";
                } else {
                    convertedParameterName = parameterName;
                }
            }
        }
    }
    #endregion Kotlin
}