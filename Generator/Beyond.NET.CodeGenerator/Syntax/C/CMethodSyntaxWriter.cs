using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.C;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.C;

public class CMethodSyntaxWriter: ICSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((MethodInfo)@object, state, configuration);
    }

    public string Write(MethodInfo method, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedMember(method) ?? throw new Exception("No C# generated member");

        bool mayThrow = cSharpGeneratedMember.MayThrow;
        const MemberKind methodKind = MemberKind.Method;

        bool isStaticMethod = method.IsStatic;

        Type declaringType = method.DeclaringType ?? throw new Exception("No declaring type");
        Type returnType = method.ReturnType;
        IEnumerable<ParameterInfo> parameters = method.GetParameters();

        string methodCode = WriteMethod(
            cSharpGeneratedMember,
            method,
            methodKind,
            isStaticMethod,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            true,
            typeDescriptorRegistry,
            state,
            out _
        );

        return methodCode;
    }

    protected string WriteMethod(
        GeneratedMember cSharpGeneratedMember,
        MemberInfo? memberInfo,
        MemberKind memberKind,
        bool isStaticMethod,
        bool mayThrow,
        Type declaringType,
        Type returnOrSetterOrEventHandlerType,
        IEnumerable<ParameterInfo> parameters,
        bool addToState,
        TypeDescriptorRegistry typeDescriptorRegistry,
        State state,
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
                CodeLanguage.C
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
        
        string cReturnOrSetterTypeName = returnOrSetterTypeDescriptor.GetTypeName(
            CodeLanguage.C, 
            true,
            returnOrSetterTypeNullability,
            Nullability.NotSpecified,
            false,
            returnOrSetterOrEventHandlerTypeIsByRef,
            false
        );
        
        string cReturnOrSetterTypeNameWithComment;
        Type? setterType;
        
        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            cReturnOrSetterTypeNameWithComment = "void /* System.Void */";
            setterType = returnOrSetterOrEventHandlerType;
        } else {
            cReturnOrSetterTypeNameWithComment = $"{cReturnOrSetterTypeName} /* {returnOrSetterOrEventHandlerType.GetFullNameOrName()} */";
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

        string methodSignatureParameters = WriteParameters(
            memberKind,
            setterType,
            finalSetterOrEventHandlerTypeNullability,
            mayThrow,
            isStaticMethod,
            declaringType,
            parameters,
            isGeneric,
            combinedGenericArguments,
            typeDescriptorRegistry
        );
        
        CCodeBuilder sb = new();

        if (string.IsNullOrEmpty(methodSignatureParameters)) {
            methodSignatureParameters = "void";
        }
        
        sb.AppendLine($"{cReturnOrSetterTypeNameWithComment}\n{methodNameC}(\n\t{methodSignatureParameters}\n);");

        generatedName = methodNameC;
        
        return sb.ToString();
    }
    
    internal static string WriteParameters(
        MemberKind memberKind,
        Type? setterOrEventHandlerType,
        Nullability setterOrEventHandlerTypeNullability,
        bool mayThrow,
        bool isStatic,
        Type declaringType,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericArguments,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        var nullabilityContext = new NullabilityInfoContext();
        
        List<string> parameterList = new();

        if (!isStatic) {
            TypeDescriptor declaringTypeDescriptor = declaringType.GetTypeDescriptor(typeDescriptorRegistry);
            
            string declaringTypeName = declaringTypeDescriptor.GetTypeName(CodeLanguage.C, true);
            
            string selfParameterName = "self";
            string parameterString = $"{declaringTypeName} /* {declaringType.GetFullNameOrName()} */ {selfParameterName}";

            parameterList.Add(parameterString);
        }
        
        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            string nativeSystemTypeTypeName = systemTypeTypeDescriptor.GetTypeName(CodeLanguage.C, true);
            
            foreach (var genericArgumentType in genericArguments) {
                string parameterName = genericArgumentType.Name;
            
                string parameterString = $"{nativeSystemTypeTypeName} /* {systemTypeTypeName} */ {parameterName}";
            
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
                CodeLanguage.C,
                true,
                parameterNullability,
                Nullability.NotSpecified,
                isOutParameter,
                isByRefParameter,
                isInParameter
            );

            string parameterString = $"{unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */ {parameter.Name}";
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
                CodeLanguage.C,
                true,
                setterOrEventHandlerTypeNullability
            );
    
            string parameterString = $"{cSetterOrEventHandlerTypeName} /* {setterOrEventHandlerType.GetFullNameOrName()} */ value";
            parameterList.Add(parameterString);
        }

        if (mayThrow) {
            Type exceptionType = typeof(Exception);
            TypeDescriptor outExceptionTypeDescriptor = exceptionType.GetTypeDescriptor(typeDescriptorRegistry);
            
            string outExceptionTypeName = outExceptionTypeDescriptor.GetTypeName(
                CodeLanguage.C,
                true,
                Nullability.NotSpecified,
                Nullability.NotSpecified,
                true,
                true,
                false
            );
            
            string outExceptionParameterName = "outException";

            string outExceptionParameterString = $"{outExceptionTypeName} /* {exceptionType.GetFullNameOrName()} */ {outExceptionParameterName}"; 
            parameterList.Add(outExceptionParameterString);
        }

        string parametersString = string.Join(",\n\t", parameterList);

        return parametersString;
    }
}