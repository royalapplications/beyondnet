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
        var returnOrSetterTypeNullability = Nullability.NotSpecified;
        var returnOrSetterTypeArrayElementNullability = Nullability.NotSpecified;

        if (memberKind == MemberKind.TypeOf) {
            returnOrSetterTypeNullability = Nullability.NonNullable;
        } else if (returnOrSetterOrEventHandlerType.IsNullableValueType(out _)) {
            returnOrSetterTypeNullability = Nullability.Nullable;
        } else if (returnOrSetterOrEventHandlerType.IsReferenceType() &&
                  !returnOrSetterOrEventHandlerType.IsByRefValueType(out bool nonByRefTypeIsStruct) &&
                  !nonByRefTypeIsStruct) {
            if (memberInfo is MethodInfo methodInfo) {
                ParameterInfo returnOrSetterValueParameter;

                if (memberKind == MemberKind.PropertySetter ||
                    memberKind == MemberKind.EventHandlerAdder ||
                    memberKind == MemberKind.EventHandlerRemover) {
                    returnOrSetterValueParameter =
                        methodInfo.GetParameters().LastOrDefault() ?? methodInfo.ReturnParameter;
                } else {
                    returnOrSetterValueParameter = methodInfo.ReturnParameter;
                }

                var nullabilityInfo = nullabilityInfoContext.Create(returnOrSetterValueParameter);

                if (memberKind == MemberKind.PropertyGetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;

                    returnOrSetterTypeArrayElementNullability =
                        nullabilityInfo.ElementType?.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.PropertySetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;

                    returnOrSetterTypeArrayElementNullability =
                        nullabilityInfo.ElementType?.WriteState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.EventHandlerAdder ||
                          memberKind == MemberKind.EventHandlerRemover) {
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                } else {
                    // Method
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }

                    var elementTypeNullabilityInfo = nullabilityInfo.ElementType;

                    if (elementTypeNullabilityInfo is not null &&
                        elementTypeNullabilityInfo.ReadState == elementTypeNullabilityInfo.WriteState) {
                        returnOrSetterTypeArrayElementNullability =
                            elementTypeNullabilityInfo.ReadState == NullabilityState.NotNull
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

                    returnOrSetterTypeArrayElementNullability =
                        nullabilityInfo.ElementType?.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.FieldSetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;

                    returnOrSetterTypeArrayElementNullability =
                        nullabilityInfo.ElementType?.WriteState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                } else {
                    // Hmm, not sure this can ever happen
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }

                    var elementTypeNullabilityInfo = nullabilityInfo.ElementType;

                    if (elementTypeNullabilityInfo is not null &&
                        elementTypeNullabilityInfo.ReadState == elementTypeNullabilityInfo.WriteState) {
                        returnOrSetterTypeArrayElementNullability =
                            elementTypeNullabilityInfo.ReadState == NullabilityState.NotNull
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
                returnOrSetterTypeArrayElementNullability,
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
        
        MethodBase? methodBase = memberInfo as MethodBase;
        MethodInfo? methodInfo = methodBase as MethodInfo;

        #region TODO: Unsupported Stuff
        if (declaringType == typeof(System.CharEnumerator) &&
            methodBase is not null &&
            methodBase.Name == "get_Current") {
            generatedName = string.Empty;
            
            return "// TODO: System.CharEnumerator.Current getter causes issues in Kotlin's type system because it overrides the method with the same name in System.Collections.IEnumerator which has a return value of System.Object. But this one here has a return value of char which is a primitive and not an System.Object as far as Kotlin is concerned.";
        }
        
        if (returnOrSetterOrEventHandlerType.IsByRef) {
            generatedName = string.Empty;
            
            return $"// TODO: Method with by ref return or setter or event handler type ({cMember.GetGeneratedName(CodeLanguage.C)})";
        }
        
        if (returnOrSetterOrEventHandlerType.IsGenericInAnyWay(true)) {
            generatedName = string.Empty;
            
            return $"// TODO: Method with generic return or setter or event handler type ({cMember.GetGeneratedName(CodeLanguage.C)})";
        }

        if (methodInfo?.ContainsGenericParameters ?? false) {
            generatedName = string.Empty;
            
            return $"// TODO: Method with generic parameters ({cMember.GetGeneratedName(CodeLanguage.C)})";
        }

        // TODO: Out/by ref value type parameters are currently not supported
        foreach (var parameter in parameters) {
            var parameterType = parameter.ParameterType;
            
            if (parameterType.IsGenericInAnyWay(true)) {
                generatedName = string.Empty;
                
                return $"// TODO: Method with generic parameter ({cMember.GetGeneratedName(CodeLanguage.C)})";
            }

            var isByRefParameter = parameter.IsOut ||
                                   parameter.IsIn ||
                                   parameterType.IsByRef ||
                                   parameterType.IsByRefLike ||
                                   parameterType.IsByRefValueType(out _); 
            
            if (isByRefParameter) {
                var nonByRefType = parameterType.GetNonByRefType();
                
                if (nonByRefType.IsEnum ||
                    nonByRefType.IsPointer ||
                    nonByRefType == typeof(IntPtr) ||
                    nonByRefType == typeof(UIntPtr)) {
                    generatedName = string.Empty;
                    
                    return $"// TODO: Method with out or in or by ref enum/IntPtr/UIntPtr/Pointer type parameter ({cMember.GetGeneratedName(CodeLanguage.C)})";
                }
                
                bool isNullableValueType = nonByRefType.IsNullableValueType(out Type? nullableValueType);

                // Only nullable structs, not primitives or enums are currently supported
                bool isNullableStruct = isNullableValueType &&
                                        (nullableValueType?.IsStruct() ?? false);

                if (nonByRefType.IsGenericInAnyWay(true) &&
                    !isNullableStruct) {
                    generatedName = string.Empty;
                    
                    return $"// TODO: Method with out or in or by ref generic type parameter ({cMember.GetGeneratedName(CodeLanguage.C)})";
                }
            }
        }
        #endregion TODO: Unsupported Stuff

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
        
        const string jnaClassName = KotlinTypeSyntaxWriter.JNA_CLASS_NAME;
        
        string cMethodName = $"{jnaClassName}.{cSharpGeneratedMember.GetGeneratedName(CodeLanguage.CSharpUnmanaged)}" ?? throw new Exception("No native name");

        // string methodNameKotlin = state.UniqueGeneratedName(
        //     memberKind.KotlinName(memberInfo),
        //     CodeLanguage.Kotlin
        // );

        string methodNameKotlin = memberKind.KotlinName(memberInfo);

        bool treatAsOverridden = false;

        if (methodInfo is not null &&
            !methodInfo.IsStatic) {
            // Kotlin doesn't seem to care about nullability in overrides
            bool isActuallyOverridden = methodInfo.IsOverridden(out _ /* bool overrideNullabilityIsCompatible */);

            if (isActuallyOverridden) {
                // if (overrideNullabilityIsCompatible) {
                    treatAsOverridden = true;
                // }
            } else {
                // Kotlin doesn't seem to care about nullability in overrides
                bool isShadowed = methodInfo.IsShadowed(
                    CodeLanguage.Kotlin,
                    out bool _ /* shadowNullabilityIsCompatible */
                );

                if (isShadowed) {
                    // if (shadowNullabilityIsCompatible) {
                        treatAsOverridden = true;
                    // }
                }
            }
        } else if (memberInfo is FieldInfo fieldInfo &&
                   !fieldInfo.IsStatic) {
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
            syntaxWriterConfiguration,
            typeDescriptorRegistry
        );

        string declaration;

        KotlinVisibilities memberVisibility;

        // TODO: Internal?
        // if (memberKind == MemberKind.Destructor) {
        //     memberVisibility = KotlinVisibilities.Internal;
        // } else {
        if (memberInfo?.IsStatic() ?? false) {
            memberVisibility = KotlinVisibilities.Public;
        } else {
            memberVisibility = KotlinVisibilities.Open;
        }

        if (memberKind == MemberKind.Constructor) {
            declaration = Builder.Fun("invoke")
                .Visibility(KotlinVisibilities.Public)
                .Operator()
                .Parameters(methodSignatureParameters)
                // .Throws(mayThrow)
                .ReturnTypeName(!returnOrSetterOrEventHandlerType.IsVoid()
                    ? kotlinReturnOrSetterTypeNameWithComment
                    : null)
                .Implementation(memberImpl)
                .ToString();
        } else if (memberKind == MemberKind.Destructor) {
            declaration = Builder.Fun(methodNameKotlin)
                .Visibility(memberVisibility)
                .Override()
                .Parameters(methodSignatureParameters)
                .Implementation(memberImpl)
                .ToString();
        } else if (memberKind == MemberKind.TypeOf) {
            // TODO: Enums
            bool isEnum = declaringType.IsEnum;

            if (isEnum) {
                return "// TODO: typeOf for enums";
            }
            
            string typeOfTypeName = !returnOrSetterOrEventHandlerType.IsVoid()
                ? kotlinReturnOrSetterTypeNameWithComment
                : throw new Exception("A typeof declaration must have a return type");

            // TODO: Render as get-only property
            declaration = Builder.Fun("typeOf")
                .Attribute("@JvmStatic")
                .Visibility(KotlinVisibilities.Public)
                //     .Throws(mayThrow)
                .ReturnTypeName(typeOfTypeName)
                .Implementation(memberImpl)
                .ToString();
            
            // TODO: Properties
            /* } else if (CanBeGeneratedAsGetOnlyProperty(memberKind, isGeneric, parameters.Any())) {
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
            } else if (memberKind == MemberKind.FieldGetter ||
                       memberKind == MemberKind.FieldSetter ||
                       memberKind == MemberKind.PropertyGetter ||
                       memberKind == MemberKind.PropertySetter) {
            */
        } else {
            // TODO: Static?
            // TODO: Throws?
            
            var extensionMethodType = syntaxWriterConfiguration.ExtensionMethodType;
            string? extensionMethodTypeName;

            if (extensionMethodType is not null) {
                var kind = syntaxWriterConfiguration.ExtensionMethodKind;
                var descr = extensionMethodType.GetTypeDescriptor(typeDescriptorRegistry);
                var name = descr.GetTypeName(CodeLanguage.Kotlin, false);

                if (kind == KotlinSyntaxWriterConfiguration.ExtensionMethodKinds.Optional) {
                    name += "?";
                }

                extensionMethodTypeName = name;
            }else {
                extensionMethodTypeName = null;
            }
            
            declaration = Builder.Fun(methodNameKotlin)
                .Visibility(memberVisibility)
                // .TypeAttachmentKind(isStaticMethod 
                //     ? interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol || interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations ? SwiftTypeAttachmentKinds.Static : SwiftTypeAttachmentKinds.Class
                //     : SwiftTypeAttachmentKinds.Instance)
                .Override(treatAsOverridden)
                .Parameters(methodSignatureParameters)
                // .Throws(mayThrow)
                .ExtendedTypeName(extensionMethodTypeName)
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
        KotlinSyntaxWriterConfiguration? syntaxWriterConfiguration,
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
                syntaxWriterConfiguration,
                typeDescriptorRegistry,
                out List<string> convertedParameterNames,
                out List<string> convertedGenericTypeArgumentNames,
                out List<string> convertedGenericMethodArgumentNames,
                out List<string> parameterBackConversions
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
            
            if (mayThrow) {
                sbImpl.AppendLine("""
                                  val __exceptionCHandle = __exceptionC.value
                                   
                                  if (__exceptionCHandle != null) {
                                      throw System_Exception(__exceptionCHandle).toKException()
                                  }
                                  """);

                sbImpl.AppendLine();
            }

            string returnCode = string.Empty;

            if (isReturning) {
                if (memberKind == MemberKind.Constructor) {
                    TypeDescriptor declaringTypeDescriptor = declaringType.GetTypeDescriptor(typeDescriptorRegistry);
                    string declaringTypeName = declaringTypeDescriptor.GetTypeName(CodeLanguage.Kotlin, false);
                    
                    returnCode = $"return {declaringTypeName}({returnValueName})";
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

                        Nullability actualNullability = returnOrSetterOrEventHandlerNullability != Nullability.NotSpecified
                                ? returnOrSetterOrEventHandlerNullability
                                : returnOrSetterTypeDescriptor.Nullability;
                        
                        if (returnOrSetterTypeDescriptor.RequiresNativePointer &&
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
    
                    returnCode = $"return {returnValueName}";
                }
            }

            KotlinCodeBuilder sbByRefParameters = new();

            foreach (var parameter in parameters) {
                Type parameterType = parameter.ParameterType;

                bool isOutParameter = parameter.IsOut;
                bool isInParameter = parameter.IsIn;
                bool isByRefParameter = parameterType.IsByRef;

                var isInOutParameter = isOutParameter || isInParameter || isByRefParameter;
                
                if (!isInOutParameter) {
                    continue;
                }

                bool isGenericParameterType = parameterType.IsGenericParameter || parameterType.IsGenericMethodParameter;
                
                parameterType = parameterType.GetNonByRefType();

                if (isGenericParameterType) {
                    parameterType = typeof(object);
                }
                
                string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");
                string convertedParameterName = $"__{parameterName}JNAByRef";

                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

                var nullabilityInfoContext = new NullabilityInfoContext();
                var paraNullability = nullabilityInfoContext.Create(parameter);
                var parameterArrayElementNullabilityState = paraNullability.ElementType;
                Nullability parameterArrayElementNullability;

                if (parameterArrayElementNullabilityState is not null &&
                    parameterArrayElementNullabilityState.ReadState == parameterArrayElementNullabilityState.WriteState) {
                    parameterArrayElementNullability = parameterArrayElementNullabilityState.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else {
                    parameterArrayElementNullability = Nullability.NotSpecified;
                }

                Nullability parameterNullability = Nullability.NotSpecified; 
                
                if (!isGeneric &&
                    !isGenericParameterType &&
                    parameterType.IsReferenceType()) {
                    bool isNotNull = false;
                        
                    var parameterNullabilityInfo = nullabilityInfoContext.Create(parameter);
                        
                    if (parameterNullabilityInfo.ReadState == parameterNullabilityInfo.WriteState) {
                        isNotNull = parameterNullabilityInfo.ReadState == NullabilityState.NotNull;
                    }
                        
                    parameterNullability = isNotNull
                        ? Nullability.NonNullable
                        : parameterTypeDescriptor.Nullability;
                }
    
                if (parameterNullability == Nullability.NotSpecified) {
                    parameterNullability = parameterTypeDescriptor.Nullability;
                }
                
                string? parameterTypeConversion = parameterTypeDescriptor.GetTypeConversion(
                    CodeLanguage.KotlinJNA,
                    CodeLanguage.Kotlin,
                    parameterArrayElementNullability
                );

                // TODO
                if (!convertedParameterNames.Contains(convertedParameterName)) {
                    convertedParameterName = parameterName;
                }

                string refValueGetter = $"{convertedParameterName}.value";

                if (string.IsNullOrEmpty(parameterTypeConversion)) {
                    parameterTypeConversion = refValueGetter;
                } else {
                    var resolvedConversion = string.Format(parameterTypeConversion, refValueGetter);

                    if (parameterNullability != Nullability.Nullable &&
                        (parameterType.IsPrimitive || parameterType.IsEnum)) {
                        // TODO: This is a bad hack
                        parameterNullability = Nullability.NonNullable;
                    }
                    
                    if (parameterNullability == Nullability.NonNullable) {
                        parameterTypeConversion = resolvedConversion;
                    } else {
                        parameterTypeConversion = $"if ({refValueGetter} != null) {resolvedConversion} else null";
                    }
                }

                sbByRefParameters.AppendLine($"{parameterName}.value = {parameterTypeConversion}");
                sbByRefParameters.AppendLine();
            }

            string byRefParamtersCode = sbByRefParameters.ToString();

            if (!string.IsNullOrEmpty(byRefParamtersCode)) {
                sbImpl.AppendLine(byRefParamtersCode);
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
        KotlinSyntaxWriterConfiguration? syntaxWriterConfiguration,
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

        var isExtensionMethod = syntaxWriterConfiguration?.IsExtensionMethod ?? false;
        var firstParameter = true;
        
        foreach (var parameter in parameters) {
            if (firstParameter) {
                firstParameter = false;
            
                if (isExtensionMethod) {
                    continue;
                }
            }
            
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

            string? parameterName = parameter.Name;

            parameterName = parameterName?.EscapedKotlinName();

            if (parameterName is null) {
                throw new Exception("Parameter without a name");
            }

            string parameterString;
            
            if (onlyWriteParameterNames) {
                if (writeModifiersForInvocation) {
                    // TODO
                    parameterString = parameterName;
                    // parameterString = $"{(isByRefParameter || isOutParameter ? "&" : string.Empty)}{parameterName}";
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
        KotlinSyntaxWriterConfiguration? syntaxWriterConfiguration,
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
        
        var isExtensionMethod = syntaxWriterConfiguration?.IsExtensionMethod ?? false;
        var isFirstParameter = true;

        foreach (var parameter in parameters) {
            string extensionMethodTargetParameter = string.Empty;
            
            if (isFirstParameter) {
                isFirstParameter = false;

                if (isExtensionMethod) {
                    extensionMethodTargetParameter = "this";
                }
            }
            
            string? parameterName = !string.IsNullOrEmpty(extensionMethodTargetParameter) 
                ? extensionMethodTargetParameter 
                : parameter.Name?.EscapedKotlinName();
            
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
        
        string parameterNameForConversion = parameterName;
            
        if (parameterName.StartsWith("`") &&
            parameterName.EndsWith("`")) {
            parameterNameForConversion = parameterName.Trim('`');
        }
        
        string? typeConversion = parameterTypeDescriptor.GetTypeConversion(
            sourceLanguage,
            targetLanguage,
            parameterArrayElementNullability
        );
        
        string optionalString;
                
        if (sourceLanguage == CodeLanguage.Kotlin &&
            targetLanguage == CodeLanguage.KotlinJNA) {
            // bool isNotNull = false;
    
            if ((typeDescriptorRegistry.GetTypeDescriptor(parameterType)?.RequiresNativePointer ?? false)) {
                parameterNullability = Nullability.NonNullable;
            }
    
            if (parameterInfo is not null &&
                !isGeneric &&
                !isGenericParameterType &&
                parameterType.IsReferenceType()) {
                bool isNotNull = false;
                        
                var parameterNullabilityInfo = nullabilityContext.Create(parameterInfo);
                        
                if (parameterNullabilityInfo.ReadState == parameterNullabilityInfo.WriteState) {
                    isNotNull = parameterNullabilityInfo.ReadState == NullabilityState.NotNull;
                }
                        
                parameterNullability = isNotNull
                    ? Nullability.NonNullable
                    : parameterTypeDescriptor.Nullability;
            }
    
            if (parameterNullability == Nullability.NotSpecified) {
                parameterNullability = parameterTypeDescriptor.Nullability;
            }
                    
            optionalString = parameterNullability.GetKotlinOptionalitySpecifier();
        } else {
            optionalString = string.Empty;
        }

        if (isInOut) {
            convertedParameterName = $"__{parameterName}JNAByRef";
            typeConversionCode = $"val {convertedParameterName} = {parameterName}.toJNARef()";
            typeBackConversionCode = null;
        } else {
            if (typeConversion != null) {
                convertedParameterName = $"{parameterNameForConversion}{convertedParameterNameSuffix}";
    
                string fullTypeConversion = string.Format(typeConversion, $"{parameterName}{optionalString}");
    
                typeConversionCode = Builder.Val(convertedParameterName)
                    .Value(fullTypeConversion)
                    .ToString();
                
                typeBackConversionCode = null;
            } else {
                typeConversionCode = null;
                typeBackConversionCode = null;

                convertedParameterName = parameterName;
            }
        }
    }
    #endregion Kotlin
}