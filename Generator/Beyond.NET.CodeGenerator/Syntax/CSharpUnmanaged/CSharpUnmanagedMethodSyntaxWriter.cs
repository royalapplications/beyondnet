using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedMethodSyntaxWriter: ICSharpUnmanagedSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((MethodInfo)@object, state, configuration);
    }
    
    public string Write(MethodInfo method, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        const bool mayThrow = true;
        const MemberKind methodKind = MemberKind.Method;
        const bool addToState = true;

        bool isStaticMethod = method.IsStatic;
        string methodName = method.Name;

        Type declaringType = method.DeclaringType ?? throw new Exception("No declaring type");;
        Type returnType = method.ReturnType;
        IEnumerable<ParameterInfo> parameters = method.GetParameters();
        
        string methodCode = WriteMethod(
            method,
            methodKind,
            methodName,
            isStaticMethod,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            addToState,
            typeDescriptorRegistry,
            state,
            out _
        );

        return methodCode;
    }

    protected string WriteMethod(
        MemberInfo? memberInfo,
        MemberKind memberKind,
        string methodName,
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
            throw new Exception("memberInfo may only be null when memberKind is Destructor or TypeOf");
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
                        
                        return "// TODO: Generic Methods with out/ref parameters that are arrays are not supported";    
                    }
                }
            }
        }
        
        bool isGenericConstructor = isGenericType &&
                                    memberKind == MemberKind.Constructor;

        string fullTypeName = declaringType.GetFullNameOrName();
        string fullTypeNameC = declaringType.CTypeName();

        string methodNameWithGenericArity = methodName;

        if (isGeneric &&
            genericMethodArguments.Length > 0) {
            methodNameWithGenericArity = methodName + "_A" + numberOfGenericMethodArguments;
        }
        
        string methodNameC;

        switch (memberKind) {
            case MemberKind.Automatic:
                throw new Exception("MemberKind may not be Automatic here");
            case MemberKind.Method:
                methodNameC = $"{fullTypeNameC}_{methodNameWithGenericArity}";
                break;
            case MemberKind.Constructor:
                methodNameC = $"{fullTypeNameC}_Create";
                break;
            case MemberKind.Destructor:
                methodNameC = $"{fullTypeNameC}_Destroy";
                break;
            case MemberKind.TypeOf:
                methodNameC = $"{fullTypeNameC}_TypeOf";
                break;
            case MemberKind.PropertyGetter:
            case MemberKind.FieldGetter:
                methodNameC = $"{fullTypeNameC}_{methodNameWithGenericArity}_Get";
                break;
            case MemberKind.PropertySetter:
            case MemberKind.FieldSetter:
                methodNameC = $"{fullTypeNameC}_{methodNameWithGenericArity}_Set";
                break;
            case MemberKind.EventHandlerAdder:
                methodNameC = $"{fullTypeNameC}_{methodNameWithGenericArity}_Add";
                break;
            case MemberKind.EventHandlerRemover:
                methodNameC = $"{fullTypeNameC}_{methodNameWithGenericArity}_Remove";
                break;
            default:
                throw new Exception("Unknown method kind");
        }

        methodNameC = state.UniqueGeneratedName(methodNameC, CodeLanguage.CSharpUnmanaged);

        if (addToState) {
            state.AddGeneratedMember(
                memberKind,
                memberInfo,
                mayThrow,
                methodNameC,
                CodeLanguage.CSharpUnmanaged
            );
        }

        generatedName = methodNameC;

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

        Type originalReturnOrSetterOrEventHandlerType = returnOrSetterOrEventHandlerType;

        if (isGenericReturnType) {
            if (isGenericArrayReturnType) {
                returnOrSetterOrEventHandlerType = typeof(Array);
            } else if (!isConstructedGenericReturnType) {
                returnOrSetterOrEventHandlerType = typeof(object);
            }
        }

        Type? setterOrEventHandlerType;

        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            setterOrEventHandlerType = returnOrSetterOrEventHandlerType;
        } else {
            setterOrEventHandlerType = null;
        }

        string methodSignatureParameters = WriteParameters(
            CodeLanguage.CSharpUnmanaged,
            memberKind,
            setterOrEventHandlerType,
            mayThrow,
            isStaticMethod,
            declaringType,
            parameters,
            isGeneric,
            combinedGenericArguments,
            false,
            typeDescriptorRegistry
        );

        bool isReturnOrSetterOrEventHandlerTypeByRef = returnOrSetterOrEventHandlerType.IsByRef;

        if (isReturnOrSetterOrEventHandlerTypeByRef) {
            returnOrSetterOrEventHandlerType = returnOrSetterOrEventHandlerType.GetNonByRefType();
        }
        
        TypeDescriptor returnOrSetterOrEventHandlerTypeDescriptor = returnOrSetterOrEventHandlerType.GetTypeDescriptor(typeDescriptorRegistry);
        
        string unmanagedReturnOrSetterOrEventHandlerTypeName = returnOrSetterOrEventHandlerTypeDescriptor.GetTypeName(
            CodeLanguage.CSharpUnmanaged,
            true,
            Nullability.Nullable,
            Nullability.NotSpecified,
            false,
            isReturnOrSetterOrEventHandlerTypeByRef,
            false
        );
        
        string unmanagedReturnOrSetterOrEventHandlerTypeNameWithComment;

        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            unmanagedReturnOrSetterOrEventHandlerTypeNameWithComment = "void /* System.Void */";
        } else {
            unmanagedReturnOrSetterOrEventHandlerTypeNameWithComment = $"{unmanagedReturnOrSetterOrEventHandlerTypeName} /* {returnOrSetterOrEventHandlerType.GetFullNameOrName()} */";
        }
        
        CSharpCodeBuilder sb = new();
        
        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{methodNameC}\")]");
        sb.AppendLine($"internal static {unmanagedReturnOrSetterOrEventHandlerTypeNameWithComment} {methodNameC}({methodSignatureParameters})");
        sb.AppendLine("{");

        Type? selfType = null;

        string? convertedSelfParameterName = null;

        if (!isStaticMethod &&
            memberKind != MemberKind.Destructor &&
            memberKind != MemberKind.TypeOf) {
            selfType = declaringType;
            
            string selfConversionCode = WriteSelfConversion(
                declaringType,
                typeDescriptorRegistry,
                out convertedSelfParameterName
            );

            sb.AppendLine(selfConversionCode);
        }

        bool isIndexerSetter = memberKind == MemberKind.PropertySetter &&
                               parameters.Any();

        string parameterConversions = WriteParameterConversions(
            CodeLanguage.CSharpUnmanaged,
            CodeLanguage.CSharp,
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

        sb.AppendLine(parameterConversions);

        if (mayThrow) {
            sb.AppendLine("""
    try {
""");
        }

        string implPrefix = mayThrow 
            ? "\t\t" 
            : "\t";

        string methodTarget;

        if (isStaticMethod) {
            if (memberKind == MemberKind.Constructor) {
                methodTarget = "new ";
            } else {
                methodTarget = string.Empty;
            }

            if (memberKind != MemberKind.TypeOf) {
                methodTarget += declaringType.GetFullNameOrName();
            }
        } else {
            methodTarget = convertedSelfParameterName ?? string.Empty;
        }

        string convertedParameterNamesString = string.Join(", ", convertedParameterNames);

        bool isReturning = memberKind != MemberKind.PropertySetter &&
                           memberKind != MemberKind.FieldSetter &&
                           memberKind != MemberKind.EventHandlerAdder &&
                           memberKind != MemberKind.EventHandlerRemover &&
                           !returnOrSetterOrEventHandlerTypeDescriptor.IsVoid;

        string returnValuePrefix = string.Empty;
        string returnValueName = "__returnValue";

        if (isReturning) {
            string fullReturnTypeName = returnOrSetterOrEventHandlerTypeDescriptor.GetTypeName(
                CodeLanguage.CSharp,
                true,
                Nullability.Nullable,
                Nullability.NotSpecified,
                false,
                isReturnOrSetterOrEventHandlerTypeByRef,
                false
            ).Replace("&", string.Empty);

            string callPrefix = isReturnOrSetterOrEventHandlerTypeByRef
                ? "ref "
                : string.Empty;
            
            returnValuePrefix = $"{fullReturnTypeName} {returnValueName} = {callPrefix}";
        }
        
        bool isProperty = memberKind == MemberKind.PropertyGetter ||
                          memberKind == MemberKind.PropertySetter;

        bool invocationIsIndexer = isProperty &&
                                   parameters.Any();

        string methodNameForInvocation;

        if (memberKind == MemberKind.Constructor) {
            methodNameForInvocation = string.Empty;
        } else if (memberKind == MemberKind.Destructor) {
            bool generateCheckedDestructors = state.Settings?.GenerateTypeCheckedDestroyMethods ?? false;
            
            if (generateCheckedDestructors &&
                !declaringType.IsAbstract &&
                !declaringType.IsGenericType &&
                !declaringType.IsGenericTypeDefinition) {
                string typeName = fullTypeName;
                
                if (declaringType.IsDelegate()) {
                    typeName = fullTypeNameC;
                }
                
                methodNameForInvocation = $"InteropUtils.CheckedFreeIfAllocated<{typeName}>(__self)";
            } else {
                methodNameForInvocation = "InteropUtils.FreeIfAllocated(__self)";
            }
        } else if (memberKind == MemberKind.TypeOf) {
            methodNameForInvocation = $"typeof({declaringType.GetFullNameOrName()})";
        } else if (invocationIsIndexer) {
            methodNameForInvocation = string.Empty;
        } else {
            methodNameForInvocation = $".{methodName}";
        }

        bool invocationNeedsParentheses = memberKind != MemberKind.PropertyGetter &&
                                          memberKind != MemberKind.PropertySetter &&
                                          memberKind != MemberKind.FieldGetter &&
                                          memberKind != MemberKind.FieldSetter &&
                                          memberKind != MemberKind.EventHandlerAdder &&
                                          memberKind != MemberKind.EventHandlerRemover &&
                                          memberKind != MemberKind.Destructor &&
                                          memberKind != MemberKind.TypeOf;

        string methodInvocationPrefix;

        if (invocationNeedsParentheses) {
            methodInvocationPrefix = "(";
        } else if (invocationIsIndexer) {
            methodInvocationPrefix = "[";
        } else {
            methodInvocationPrefix = string.Empty;
        }

        string methodInvocationSuffix;
        string? fullSetterTypeConversion = null;

        if (invocationNeedsParentheses) {
            methodInvocationSuffix = ")";
        } else if (memberKind == MemberKind.PropertySetter ||
                   memberKind == MemberKind.FieldSetter) {
            string valueParamterName = "__value";

            string? setterTypeConversion = returnOrSetterOrEventHandlerTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharpUnmanaged,
                CodeLanguage.CSharp
            );

            fullSetterTypeConversion = setterTypeConversion != null
                ? string.Format(setterTypeConversion, valueParamterName)
                : valueParamterName;

            string indexerSuffix = invocationIsIndexer
                ? "] "
                : string.Empty;

            methodInvocationSuffix = $"{indexerSuffix} = {fullSetterTypeConversion}";
        } else if (memberKind == MemberKind.PropertyGetter &&
                   invocationIsIndexer) {
            methodInvocationSuffix = "]";
        } else if (memberKind == MemberKind.EventHandlerAdder ||
                   memberKind == MemberKind.EventHandlerRemover) {
            string valueParamterName = "__value";
            
            string? eventHandlerTypeConversion = returnOrSetterOrEventHandlerTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharpUnmanaged,
                CodeLanguage.CSharp
            );

            string fullEventHandlerTypeConversion = eventHandlerTypeConversion != null
                ? string.Format(eventHandlerTypeConversion, valueParamterName)
                : valueParamterName;

            if (memberKind == MemberKind.EventHandlerAdder) {
                methodInvocationSuffix = $" += {fullEventHandlerTypeConversion}";
            } else { // Remover
                methodInvocationSuffix = $" -= {fullEventHandlerTypeConversion}";
            }
        } else {
            methodInvocationSuffix = string.Empty;
        }

        // Invocation
        if (isGeneric) {
            bool isNonConstructedGenericType = isGenericType &&
                                               !declaringType.IsConstructedGenericType;
            
            string declaringTypeName = declaringType.GetFullNameOrName();
            
            if (isGenericConstructor) { // Constructor
                sb.AppendLine($"{implPrefix}System.Type __targetTypeForGenericCall = typeof({declaringTypeName});");
            } else { // Method, Property, etc.
                sb.AppendLine($"{implPrefix}System.Type __targetTypeForGenericCall = typeof({declaringTypeName});");
                
                if (isNonConstructedGenericType) {
                    sb.AppendLine($"{implPrefix}const System.String __memberNameForGenericCall = \"{methodName}\";");
                } else {
                    sb.AppendLine($"{implPrefix}System.String __memberNameForGenericCall = nameof({declaringTypeName}.{methodName});");
                }
                
                if (isStaticMethod) {
                    sb.AppendLine($"{implPrefix}System.Object? __methodTargetForGenericCall = null;");
                } else {
                    sb.AppendLine($"{implPrefix}System.Object? __methodTargetForGenericCall = {convertedSelfParameterName};");
                }
    
                sb.AppendLine();
            }

            Type returnType;
            string returnTypeName;
            string typeOfReturnTypeName;
            
            if (isGenericReturnType ||
                isConstructedGenericReturnType) {
                returnType = originalReturnOrSetterOrEventHandlerType;

                bool returnTypeIsByRef = returnType.IsByRef;

                if (returnTypeIsByRef) {
                    returnType = returnType.GetNonByRefType();
                }

                bool returnTypeIsGenericParameter = returnType.IsGenericParameter;
                bool returnTypeIsGenericMethodParameter = returnType.IsGenericMethodParameter;

                bool returnTypeIsGenericParameterType = returnTypeIsGenericParameter ||
                                                        returnTypeIsGenericMethodParameter;

                if (returnTypeIsGenericParameterType) {
                    if (returnTypeIsGenericMethodParameter) {
                        returnTypeName = $"System.Type.MakeGenericMethodParameter({returnType.GenericParameterPosition})";
                    } else {
                        returnTypeName = convertedGenericTypeArgumentNames[returnType.GenericParameterPosition];
                    }

                    if (returnTypeIsByRef) {
                        returnTypeName += ".MakeByRefType()";
                    }

                    if (returnTypeIsGenericMethodParameter ||
                        returnTypeIsByRef) {
                        returnTypeName = "System.Object";
                        typeOfReturnTypeName = "typeof(System.Object)";
                    } else {
                        typeOfReturnTypeName = returnTypeName;
                        
                        if (returnTypeIsGenericParameterType) {
                            returnTypeName = "System.Object";
                        }
                    }
                } else {
                    returnType = returnOrSetterOrEventHandlerType;
                    returnTypeName = returnType.GetFullNameOrName();
                    typeOfReturnTypeName = $"typeof({returnTypeName})";
                }
            } else {
                returnType = returnOrSetterOrEventHandlerType;
                returnTypeName = returnType.GetFullNameOrName();
                typeOfReturnTypeName = $"typeof({returnTypeName})";
            }

            if (convertedParameterNames.Count > 0) {
                string parameterNamesString = convertedParameterNamesString;

                if (memberKind == MemberKind.PropertySetter) {
                    if (!parameterNamesString.TrimEnd().EndsWith(',')) {
                        parameterNamesString += ", ";
                    }

                    parameterNamesString += $"{fullSetterTypeConversion}";
                }
                
                sb.AppendLine($"{implPrefix}System.Object[] __parametersForGenericCall = new System.Object[] {{ {parameterNamesString} }};");

                List<string> parameterTypeNames = new();

                foreach (var parameter in parameters) {
                    Type parameterType = parameter.ParameterType;

                    bool isByRefParameter = parameterType.IsByRef;

                    if (isByRefParameter) {
                        parameterType = parameterType.GetNonByRefType();
                    }

                    bool isGenericParameter = parameterType.IsGenericParameter;
                    bool isGenericMethodParameter = parameterType.IsGenericMethodParameter;

                    bool isGenericParameterType = isGenericParameter ||
                                                  isGenericMethodParameter;

                    if (isGenericParameterType) {
                        string parameterTypeName;
                        
                        if (isGenericMethodParameter) {
                            parameterTypeName = $"System.Type.MakeGenericMethodParameter({parameterType.GenericParameterPosition})";
                        } else {
                            if (isGenericMethodParameter) {
                                parameterTypeName = convertedGenericMethodArgumentNames[parameterType.GenericParameterPosition];
                            } else {
                                parameterTypeName = convertedGenericTypeArgumentNames[parameterType.GenericParameterPosition];
                            }
                        }
                        
                        if (isByRefParameter) {
                            parameterTypeName += ".MakeByRefType()";
                        }

                        parameterTypeNames.Add(parameterTypeName);
                    } else {
                        bool isGenericArrayParameterType = false;
                        Type? arrayType = parameterType.GetElementType();
                        
                        if (parameterType.IsArray &&
                            arrayType is not null &&
                            (arrayType.IsGenericParameter || arrayType.IsGenericMethodParameter)) {
                            isGenericArrayParameterType = true;
                        }

                        if (isGenericArrayParameterType &&
                            arrayType is not null) {
                            // TODO: This is not correct for type generic parameters
                            string parameterTypeName = $"System.Type.MakeGenericMethodParameter({arrayType.GenericParameterPosition}).MakeArrayType()";
                        
                            parameterTypeNames.Add(parameterTypeName);
                        } else {
                            string parameterTypeName = parameterType.GetFullNameOrName();
                            
                            string typeOfCode = $"typeof({parameterTypeName})";
                            
                            if (isByRefParameter) {
                                typeOfCode += ".MakeByRefType()";
                            }

                            parameterTypeNames.Add(typeOfCode);
                        }
                    }
                }

                if (memberKind == MemberKind.PropertySetter) {
                    parameterTypeNames.Add($"typeof({returnOrSetterOrEventHandlerType.GetFullNameOrName()})");
                }

                string parameterTypeNamesString = string.Join(", ", parameterTypeNames);
                
                sb.AppendLine($"{implPrefix}System.Type[] __parameterTypesForGenericCall = new System.Type[] {{ {parameterTypeNamesString} }};");
            } else {
                if (memberKind == MemberKind.PropertySetter) {
                    sb.AppendLine($"{implPrefix}System.Object[] __parametersForGenericCall = new System.Object[] {{ {fullSetterTypeConversion} }};");
                    sb.AppendLine($"{implPrefix}System.Type[] __parameterTypesForGenericCall = new System.Type[] {{ typeof({returnOrSetterOrEventHandlerType.GetFullNameOrName()}) }};");
                } else {
                    sb.AppendLine($"{implPrefix}System.Object[]? __parametersForGenericCall = null;");
                    sb.AppendLine($"{implPrefix}System.Type[] __parameterTypesForGenericCall = System.Type.EmptyTypes;");
                }
            }

            sb.AppendLine();

            if (isNonConstructedGenericType) {
                string convertedGenericTypeArgumentNamesString = string.Join(", ", convertedGenericTypeArgumentNames);
                
                sb.AppendLine($"{implPrefix}System.Type[] __genericParameterTypesForGenericType = new System.Type[] {{ {convertedGenericTypeArgumentNamesString} }};");
                sb.AppendLine();    
            }

            string convertedGenericMethodArgumentNamesString = string.Join(", ", convertedGenericMethodArgumentNames);
            
            sb.AppendLine($"{implPrefix}System.Type[] __genericParameterTypesForGenericCall = new System.Type[] {{ {convertedGenericMethodArgumentNamesString} }};");
            sb.AppendLine();

            if (isNonConstructedGenericType) {
                sb.AppendLine($"{implPrefix}__targetTypeForGenericCall = __targetTypeForGenericCall.MakeGenericType(__genericParameterTypesForGenericType);");
            }
            
            if (!isGenericConstructor) {
                // TODO: More member kinds

                bool hasMethodForGenericCall = true;
                
                if (memberKind == MemberKind.Method) {
                    sb.AppendLine($"{implPrefix}System.Reflection.MethodInfo __methodForGenericCall = __targetTypeForGenericCall.GetMethod(__memberNameForGenericCall, {numberOfGenericMethodArguments}, __parameterTypesForGenericCall) ?? throw new Exception(\"Method {methodName} not found\");");
                } else if (memberKind == MemberKind.PropertyGetter) {
                    sb.AppendLine($"{implPrefix}System.Reflection.MethodInfo __methodForGenericCall = __targetTypeForGenericCall.GetProperty(__memberNameForGenericCall, {typeOfReturnTypeName}).GetGetMethod() ?? throw new Exception(\"Property {methodName} or getter method not found\");");
                } else if (memberKind == MemberKind.PropertySetter) {
                    sb.AppendLine($"{implPrefix}System.Reflection.MethodInfo __methodForGenericCall = __targetTypeForGenericCall.GetProperty(__memberNameForGenericCall, {typeOfReturnTypeName}).GetSetMethod() ?? throw new Exception(\"Property {methodName} or setter method not found\");");
                } else if (memberKind == MemberKind.FieldGetter ||
                           memberKind == MemberKind.FieldSetter) {
                    hasMethodForGenericCall = false;
                    
                    sb.AppendLine($"{implPrefix}System.Reflection.FieldInfo __fieldForGenericCall = __targetTypeForGenericCall.GetField(__memberNameForGenericCall) ?? throw new Exception(\"Field {methodName} not found\");");
                } else {
                    throw new Exception($"Unsupported member kind in generic method or type: {memberKind}");
                }

                if (hasMethodForGenericCall) {
                    if (numberOfGenericMethodArguments > 0) {
                        sb.AppendLine($"{implPrefix}System.Reflection.MethodInfo __genericMethodForGenericCall = __methodForGenericCall.MakeGenericMethod(__genericParameterTypesForGenericCall);");
                    } else {
                        sb.AppendLine($"{implPrefix}System.Reflection.MethodInfo __genericMethodForGenericCall = __methodForGenericCall;");
                    }
                }
            }

            sb.AppendLine();

            if (isReturning) {
                returnValuePrefix += $"({returnTypeName})";
            }

            if (isGenericConstructor) {
                sb.AppendLine($"{implPrefix}{returnValuePrefix}System.Activator.CreateInstance(__targetTypeForGenericCall, __parametersForGenericCall);");
            } else {
                // TODO: More member kinds

                if (memberKind == MemberKind.FieldGetter) {
                    sb.AppendLine($"{implPrefix}{returnValuePrefix}__fieldForGenericCall.GetValue(__methodTargetForGenericCall);");
                } else if (memberKind == MemberKind.FieldSetter) {
                    sb.AppendLine($"{implPrefix}{returnValuePrefix}__fieldForGenericCall.SetValue(__methodTargetForGenericCall, {fullSetterTypeConversion});");
                } else {
                    sb.AppendLine($"{implPrefix}{returnValuePrefix}__genericMethodForGenericCall.Invoke(__methodTargetForGenericCall, __parametersForGenericCall);");
                }
            }
        } else {
            sb.AppendLine($"{implPrefix}{returnValuePrefix}{methodTarget}{methodNameForInvocation}{methodInvocationPrefix}{convertedParameterNamesString}{methodInvocationSuffix};");
        }

        string? convertedReturnValueName = null;
        string? returnValueBoxing = null;

        if (isReturning) {
            string? returnValueTypeConversion = returnOrSetterOrEventHandlerTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharp,
                CodeLanguage.CSharpUnmanaged
            );

            if (returnValueTypeConversion != null) {
                string fullReturnValueTypeConversion = string.Format(returnValueTypeConversion, returnValueName);
                
                convertedReturnValueName = "__returnValueNative";
                
                sb.AppendLine($"{implPrefix}{returnOrSetterOrEventHandlerTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true)} {convertedReturnValueName} = {fullReturnValueTypeConversion};");
            } else {
                convertedReturnValueName = returnValueName;
            }

            if (isReturnOrSetterOrEventHandlerTypeByRef) {
                string boxedReturnValueName = "__returnValueBoxed";
                
                returnValueBoxing = $"{unmanagedReturnOrSetterOrEventHandlerTypeName} {boxedReturnValueName} = ({unmanagedReturnOrSetterOrEventHandlerTypeName})System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof({unmanagedReturnOrSetterOrEventHandlerTypeName})); *{boxedReturnValueName} = {convertedReturnValueName};";

                convertedReturnValueName = boxedReturnValueName;
            }
        }

        if (mayThrow) {
            sb.AppendLine("""

        if (__outException is not null) {
            *__outException = null;
        }

""");

            int parameterIdx = -1;

            foreach (var parameter in parameters) {
                parameterIdx++;
                
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
                string convertedParameterName = $"{parameterName}Converted";

                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

                string? parameterTypeConversion = parameterTypeDescriptor.GetTypeConversion(
                    CodeLanguage.CSharp,
                    CodeLanguage.CSharpUnmanaged
                );

                string parameterPrefix;

                if (isGeneric) {
                    parameterPrefix = string.Empty;
                } else {
                    parameterPrefix = isOutParameter 
                        ? "out "
                        : isInParameter 
                            ? string.Empty
                            : "ref ";                    
                }

                string fullParameterName = $"{parameterPrefix}{convertedParameterName}";

                if (!convertedParameterNames.Contains(fullParameterName)) {
                    convertedParameterName = parameterName;
                }

                if (isGeneric) {
                    sb.AppendLine($"\t\t{fullParameterName} = ({parameterType.GetFullNameOrName()})__parametersForGenericCall[{parameterIdx}];");
                    sb.AppendLine();
                }
                
                if (string.IsNullOrEmpty(parameterTypeConversion)) {
                    parameterTypeConversion = convertedParameterName;
                } else {
                    parameterTypeConversion = string.Format(parameterTypeConversion, convertedParameterName);
                }
                
                sb.AppendLine($"\t\tif ({parameterName} is not null) {{");
                sb.AppendLine($"\t\t\t*{parameterName} = {parameterTypeConversion};");
                sb.AppendLine("\t\t}");
                sb.AppendLine();
            }
            
            if (isReturning) {
                if (!string.IsNullOrEmpty(returnValueBoxing)) {
                    sb.AppendLine($"{implPrefix}{returnValueBoxing}");
                    sb.AppendLine();
                }
                
                sb.AppendLine($"{implPrefix}return {convertedReturnValueName};");
            }
            
            sb.AppendLine("""
    } catch (Exception __exception) {
        if (__outException is not null) {
            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
                
            *__outException = __exceptionHandleAddress;
        }

""");

            foreach (var parameter in parameters) {
                if (!parameter.IsOut) {
                    continue;
                }
                
                string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");

                Type parameterType = parameter.ParameterType.GetNonByRefType();
                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

                string outValue = parameterTypeDescriptor.GetDefaultValue() 
                                  ?? $"default({parameterType.GetFullNameOrName()})";

                sb.AppendLine($"\t\tif ({parameterName} is not null) {{");
                sb.AppendLine($"\t\t\t*{parameterName} = {outValue};");
                sb.AppendLine("\t\t}");
                sb.AppendLine();
            }

            if (isReturning) {
                string returnValue;
                
                if (isReturnOrSetterOrEventHandlerTypeByRef) {
                    returnValue = "null";
                } else {
                    returnValue = returnOrSetterOrEventHandlerTypeDescriptor.GetDefaultValue()
                                  ?? $"default({returnOrSetterOrEventHandlerType.GetFullNameOrName()})";
                }

                sb.AppendLine($"{implPrefix}return {returnValue};");
            }
            
            sb.AppendLine("\t} finally {");

            string mutableStructInstanceReplacement = WriteMutableStructInstanceReplacement(
                selfType,
                "__self",
                convertedSelfParameterName
            );

            if (!string.IsNullOrEmpty(mutableStructInstanceReplacement)) {
                sb.AppendLine(mutableStructInstanceReplacement
                    .IndentAllLines(2));
            }
            
            sb.AppendLine("\t}");
        } else {
            string mutableStructInstanceReplacement = WriteMutableStructInstanceReplacement(
                selfType,
                "__self",
                convertedSelfParameterName
            );

            if (!string.IsNullOrEmpty(mutableStructInstanceReplacement)) {
                sb.AppendLine(mutableStructInstanceReplacement
                    .IndentAllLines(1));
            }
            
            if (isReturning) {
                if (!string.IsNullOrEmpty(returnValueBoxing)) {
                    sb.AppendLine($"\t{returnValueBoxing}");
                    sb.AppendLine();
                }
                
                sb.AppendLine($"\treturn {convertedReturnValueName};");
            }
        }

        sb.AppendLine("}");

        return sb.ToString();
    }

    protected string WriteMutableStructInstanceReplacement(
        Type? type,
        string? parameterName,
        string? convertedParameterName
    )
    {
        if (type is null ||
            !type.IsStruct() ||
            string.IsNullOrEmpty(parameterName) ||
            string.IsNullOrEmpty(convertedParameterName)) {
            return string.Empty;
        }
        
        CSharpCodeBuilder sb = new();

        sb.AppendLine($"if ({parameterName} is not null) {{");
        sb.AppendLine($"\tInteropUtils.ReplaceInstance({parameterName}, {convertedParameterName});");
        sb.AppendLine("}");
        sb.AppendLine();

        return sb.ToString();
    }

    protected string WriteSelfConversion(
        Type type,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string convertedSelfParameterName
    )
    {
        bool isNonConstructedGeneric = (type.IsGenericType || type.IsGenericTypeDefinition) &&
                                       !type.IsConstructedGenericType;
                
        if (isNonConstructedGeneric) {
            type = typeof(System.Object);
        }
        
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
        
        string parameterName = "__self";
        convertedSelfParameterName = parameterName;

        CSharpCodeBuilder sb = new();
                
        string? typeConversion = typeDescriptor.GetTypeConversion(
            CodeLanguage.CSharpUnmanaged, 
            CodeLanguage.CSharp
        );
            
        if (typeConversion != null) {
            string convertedParameterName = $"{parameterName}Converted";
                
            string fullTypeConversion = string.Format(typeConversion, parameterName);

            bool isSelfPointer = typeDescriptor.RequiresNativePointer;

            if (isSelfPointer) {
                sb.AppendLine($"\tif ({parameterName} is null) {{");
                sb.AppendLine($"\t\tthrow new ArgumentNullException(nameof({parameterName}));");
                sb.AppendLine("\t}");
                sb.AppendLine();
            }
            
            string typeConversionCode = $"{type.GetFullNameOrName()} {convertedParameterName} = {fullTypeConversion};";

            sb.AppendLine($"\t{typeConversionCode}");

            convertedSelfParameterName = convertedParameterName;
        }

        return sb.ToString();
    }

    internal static string WriteParameters(
        CodeLanguage targetLanguage,
        MemberKind memberKind,
        Type? setterOrEventHandlerType,
        bool mayThrow,
        bool isStatic,
        Type declaringType,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericArguments,
        bool onlyWriteParameterTypes,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        List<string> parameterList = new();
        
        string parameterNamePrefix = onlyWriteParameterTypes 
            ? "/* "
            : string.Empty;
        
        string parameterNameSuffix = onlyWriteParameterTypes 
            ? " */"
            : string.Empty;

        if (!isStatic) {
            TypeDescriptor declaringTypeDescriptor = declaringType.GetTypeDescriptor(typeDescriptorRegistry);
            string declaringTypeName = declaringTypeDescriptor.GetTypeName(targetLanguage, true);
            string selfParameterName = "__self";
            
            string parameterString = $"{declaringTypeName} /* {declaringType.GetFullNameOrName()} */ {parameterNamePrefix}{selfParameterName}{parameterNameSuffix}";
            
            parameterList.Add(parameterString);
        }
        
        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            string nativeSystemTypeTypeName = systemTypeTypeDescriptor.GetTypeName(targetLanguage, true);
            
            foreach (var genericArgumentType in genericArguments) {
                string parameterName = genericArgumentType.Name;
            
                string parameterString = $"{nativeSystemTypeTypeName} /* {systemTypeTypeName} */ {parameterNamePrefix}{parameterName}{parameterNameSuffix}";
            
                parameterList.Add(parameterString);
            }
        }
        
        if (memberKind != MemberKind.Destructor) {
            foreach (var parameter in parameters) {
                Type parameterType = parameter.ParameterType;

                bool isByRefParameter = parameterType.IsByRef;
                bool isOutParameter = parameter.IsOut;
                bool isInParameter = parameter.IsIn;

                if (isByRefParameter) {
                    parameterType = parameterType.GetNonByRefType();
                }
                
                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
                
                string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(
                    targetLanguage,
                    true,
                    Nullability.Nullable,
                    Nullability.NotSpecified,
                    isOutParameter,
                    isByRefParameter,
                    isInParameter
                );
                
                string parameterString = $"{unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */ {parameterNamePrefix}{parameter.Name}{parameterNameSuffix}";
                parameterList.Add(parameterString);
            }
        }

        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            if (setterOrEventHandlerType == null) {
                throw new Exception("Setter or Event Handler Type may not be null");
            }
            
            TypeDescriptor setterOrEventHandlerTypeDescriptor = setterOrEventHandlerType.GetTypeDescriptor(typeDescriptorRegistry);
            string unmanagedSetterOrEventHandlerTypeName = setterOrEventHandlerTypeDescriptor.GetTypeName(targetLanguage, true);
    
            string parameterString = $"{unmanagedSetterOrEventHandlerTypeName} /* {setterOrEventHandlerType.GetFullNameOrName()} */ {parameterNamePrefix}__value{parameterNameSuffix}";
            parameterList.Add(parameterString);
        }

        if (mayThrow) {
            Type exceptionType = typeof(Exception);
            TypeDescriptor outExceptionTypeDescriptor = exceptionType.GetTypeDescriptor(typeDescriptorRegistry);
            
            string outExceptionTypeName = outExceptionTypeDescriptor.GetTypeName(
                targetLanguage,
                true,
                Nullability.Nullable,
                Nullability.NotSpecified,
                true,
                true,
                false
            );
            
            string outExceptionParameterName = "__outException";

            string outExceptionParameterString = $"{outExceptionTypeName} /* {exceptionType.GetFullNameOrName()} */ {parameterNamePrefix}{outExceptionParameterName}{parameterNameSuffix}"; 
            parameterList.Add(outExceptionParameterString);
        }

        string parametersString = string.Join(", ", parameterList);

        return parametersString;
    }

    internal static string WriteParameterConversions(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericTypeArguments,
        IEnumerable<Type> genericMethodArguments,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out List<string> convertedParameterNames,
        out List<string> convertedGenericTypeArgumentNames,
        out List<string> convertedGenericMethodArgumentNames,
        out List<string> convertedTypeDestructors // Only used in delegates at the moment
    )
    {
        CSharpCodeBuilder sb = new();
        
        convertedParameterNames = new();
        convertedGenericTypeArgumentNames = new();
        convertedGenericMethodArgumentNames = new();
        convertedTypeDestructors = new();

        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            
            string systemTypeTypeConversion = systemTypeTypeDescriptor.GetTypeConversion(
                sourceLanguage,
                targetLanguage
            )!;
    
            foreach (var genericArgumentType in genericTypeArguments) {
                string name = genericArgumentType.Name;
                
                string convertedGenericArgumentName = $"{name}Converted";
                    
                string fullTypeConversion = string.Format(systemTypeTypeConversion, name);
                string typeConversionCode = $"{systemTypeTypeName} {convertedGenericArgumentName} = {fullTypeConversion};";
    
                sb.AppendLine($"\t{typeConversionCode}");
                
                convertedGenericTypeArgumentNames.Add(convertedGenericArgumentName);
            }
            
            foreach (var genericArgumentType in genericMethodArguments) {
                string name = genericArgumentType.Name;
                
                string convertedGenericArgumentName = $"{name}Converted";
                    
                string fullTypeConversion = string.Format(systemTypeTypeConversion, name);
                string typeConversionCode = $"{systemTypeTypeName} {convertedGenericArgumentName} = {fullTypeConversion};";
    
                sb.AppendLine($"\t{typeConversionCode}");
                
                convertedGenericMethodArgumentNames.Add(convertedGenericArgumentName);
            }
        }

        foreach (var parameter in parameters) {
            string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");
            
            Type parameterType = parameter.ParameterType;
            bool isOutParameter = parameter.IsOut;
            bool isInParameter = parameter.IsIn;
            bool isByRefParameter = parameterType.IsByRef;
            bool isArrayType = parameterType.IsArray;

            if (isByRefParameter) {
                parameterType = parameterType.GetNonByRefType();
            }
            
            bool isGenericParameterType = parameterType.IsGenericParameter || parameterType.IsGenericMethodParameter;

            if (isOutParameter &&
                !isByRefParameter) {
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

            string? typeConversion = parameterTypeDescriptor.GetTypeConversion(
                sourceLanguage,
                targetLanguage
            );

            string convertedParameterName;

            if (isOutParameter) {
                convertedParameterName = $"{parameterName}Converted";

                string typeName = parameterType.GetFullNameOrName()
                    .Replace("&", string.Empty);

                string suffix;

                if (isGeneric) {
                    suffix = $" = default({typeName})";
                } else {
                    suffix = string.Empty;
                }

                sb.AppendLine($"\t{typeName} {convertedParameterName}{suffix};");

                if (!isGeneric) {
                    convertedParameterName = $"out {convertedParameterName}";
                }
            } else if (isByRefParameter) {
                convertedParameterName = $"{parameterName}Converted";

                string parameterTypeName = parameterType.GetFullNameOrName()
                    .Replace("&", string.Empty);

                if (sourceLanguage == CodeLanguage.CSharpUnmanaged &&
                    targetLanguage == CodeLanguage.CSharp) {
                    string typeConversionCode;
                
                    if (!string.IsNullOrEmpty(typeConversion)) {
                        string fullTypeConversion = string.Format(typeConversion, $"(*{parameterName})");
                        typeConversionCode = $"{convertedParameterName} = {fullTypeConversion};";
                    } else {
                        typeConversionCode = $"{convertedParameterName} = *{parameterName};";
                    }

                    sb.AppendLine($"\t{parameterTypeName} {convertedParameterName};");

                    sb.AppendLine();
                
                    sb.AppendLine($"\tif ({parameterName} is not null) {{");
                    sb.AppendLine($"\t\t{typeConversionCode}");
                    sb.AppendLine("\t} else {");
                
                    string defaultValue = $"default({parameterType.GetFullNameOrName()})";
                
                    sb.AppendLine($"\t\t{convertedParameterName} = {defaultValue};");
                    sb.AppendLine("\t}");

                    sb.AppendLine();

                    if (!isGeneric) {
                        if (!isInParameter) {
                            convertedParameterName = $"ref {convertedParameterName}";
                        }
                    }
                } else if (sourceLanguage == CodeLanguage.CSharp &&
                           targetLanguage == CodeLanguage.CSharpUnmanaged) {
                    // TODO: Delegates
                
                    if (string.IsNullOrEmpty(typeConversion)) {
                        string convertedParameterTypeName = parameterTypeDescriptor.GetTypeName(
                            CodeLanguage.CSharpUnmanaged,
                            true,
                            Nullability.Nullable,
                            Nullability.NotSpecified,
                            isOutParameter,
                            true,
                            isInParameter
                        );
                    
                        sb.AppendLine($"\t{convertedParameterTypeName} {convertedParameterName} = ({convertedParameterTypeName})System.Runtime.CompilerServices.Unsafe.AsPointer(ref {parameterName});");
                    } else {
                        string convertedParameterTypeName = parameterTypeDescriptor.GetTypeName(
                            CodeLanguage.CSharpUnmanaged,
                            true,
                            Nullability.Nullable,
                            Nullability.NotSpecified,
                            isOutParameter,
                            true,
                            isInParameter
                        );

                        var typeConversionFormat = string.Format(typeConversion, parameterName);
                        sb.AppendLine($"\t{convertedParameterTypeName} {convertedParameterName} = ({convertedParameterTypeName}){typeConversionFormat};");

                        CSharpCodeBuilder sbDestructor = new();

                        var retrieverFormat = parameterTypeDescriptor.GetTypeConversion(
                            CodeLanguage.CSharpUnmanaged,
                            CodeLanguage.CSharp
                        );

                        if (!string.IsNullOrEmpty(retrieverFormat)) {
                            sbDestructor.AppendLine($"\t{parameterName} = {string.Format(retrieverFormat, convertedParameterName)};");
                        }

                        // TODO: I don't know... This kind of looks dangerous
                        var destructorFormat = parameterTypeDescriptor.GetDestructor(CodeLanguage.CSharpUnmanaged);
                        
                        if (!string.IsNullOrEmpty(destructorFormat)) {
                            sbDestructor.AppendLine($"\t{string.Format(destructorFormat, convertedParameterName)};");
                        }

                        var destructor = sbDestructor.ToString();
                        
                        convertedTypeDestructors.Add(destructor);
                    }
                }
            } else if (isInParameter) { // In but not by ref
                convertedParameterName = $"{parameterName}Converted";

                string parameterTypeName = parameterType.GetFullNameOrName()
                    .Replace("&", string.Empty);

                string typeConversionCode;
                
                if (!string.IsNullOrEmpty(typeConversion)) {
                    string fullTypeConversion = string.Format(typeConversion, $"(*{parameterName})");
                    typeConversionCode = $"{convertedParameterName} = {fullTypeConversion};";
                } else {
                    typeConversionCode = $"{convertedParameterName} = *{parameterName};";
                }

                sb.AppendLine($"\t{parameterTypeName} {convertedParameterName};");

                sb.AppendLine();
                
                sb.AppendLine($"\tif ({parameterName} is not null) {{");
                sb.AppendLine($"\t\t{typeConversionCode}");
                sb.AppendLine("\t} else {");
                
                string defaultValue = $"default({parameterType.GetFullNameOrName()})";
                
                sb.AppendLine($"\t\t{convertedParameterName} = {defaultValue};");
                sb.AppendLine("\t}");

                sb.AppendLine();
            } else if (typeConversion != null) {
                string parameterTypeName = parameterTypeDescriptor.GetTypeName(
                    targetLanguage,
                    true,
                    Nullability.Nullable,
                    Nullability.NotSpecified,
                    isOutParameter,
                    isByRefParameter,
                    isInParameter
                );
                
                convertedParameterName = $"{parameterName}Converted";
                
                string fullTypeConversion = string.Format(typeConversion, parameterName);
                string typeConversionCode = $"{parameterTypeName} {convertedParameterName} = {fullTypeConversion};";

                sb.AppendLine($"\t{typeConversionCode}");

                if (!isGeneric) {
                    if (isOutParameter) {
                        convertedParameterName = $"out {convertedParameterName}";
                    } else if (isInParameter) {
                        // convertedParameterName = $"in {convertedParameterName}";
                    } else if (isByRefParameter) {
                        convertedParameterName = $"ref {convertedParameterName}";
                    }
                }
            } else {
                if (!isGeneric &&
                    (isOutParameter || isInParameter)) {
                    if (isOutParameter) {
                        convertedParameterName = $"out {parameterName}";
                    } else { // In
                        convertedParameterName = parameterName;
                    }
                } else if (!isGeneric &&
                           isByRefParameter) {
                    convertedParameterName = $"ref {parameterName}";
                } else {
                    convertedParameterName = parameterName;
                }
            }
            
            convertedParameterNames.Add(convertedParameterName);
        }

        return sb.ToString();
    }
}