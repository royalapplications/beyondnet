using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedMethodSyntaxWriter: ICSharpUnmanagedSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((MethodInfo)@object, state);
    }
    
    public string Write(MethodInfo method, State state)
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
                bool isOutOrByRef = parameter.IsOut || parameter.ParameterType.IsByRef;

                if (isOutOrByRef) {
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
            } else {
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
            false,
            isReturnOrSetterOrEventHandlerTypeByRef
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
        
        StringBuilder sb = new();
        
        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{methodNameC}\")]");
        sb.AppendLine($"internal static {unmanagedReturnOrSetterOrEventHandlerTypeNameWithComment} {methodNameC}({methodSignatureParameters})");
        sb.AppendLine("{");

        string? convertedSelfParameterName = null;

        if (!isStaticMethod &&
            memberKind != MemberKind.Destructor &&
            memberKind != MemberKind.TypeOf) {
            string selfConversionCode = WriteSelfConversion(
                declaringType,
                typeDescriptorRegistry,
                out convertedSelfParameterName
            );

            sb.AppendLine(selfConversionCode);
        }

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
            out List<string> convertedGenericMethodArgumentNames
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
                false,
                isReturnOrSetterOrEventHandlerTypeByRef
            ).Replace("&", string.Empty);

            string callPrefix = isReturnOrSetterOrEventHandlerTypeByRef
                ? "ref "
                : string.Empty;
            
            returnValuePrefix = $"{fullReturnTypeName} {returnValueName} = {callPrefix}";
        }

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

        string methodInvocationPrefix = invocationNeedsParentheses 
            ? "("
            : string.Empty;

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

            methodInvocationSuffix = $" = {fullSetterTypeConversion}";
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
            
            if (convertedParameterNames.Count > 0) {
                sb.AppendLine($"{implPrefix}System.Object[] __parametersForGenericCall = new System.Object[] {{ {convertedParameterNamesString} }};");

                List<string> parameterTypeNames = new();

                int parameterIndex = 0;

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
                            string convertedParameterName = convertedParameterNames[parameterIndex];
                            
                            parameterTypeName = $"{convertedParameterName}.GetType()";
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

                    parameterIndex++;
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

            string returnTypeName = returnOrSetterOrEventHandlerType.GetFullNameOrName();

            if (isNonConstructedGenericType) {
                sb.AppendLine($"{implPrefix}__targetTypeForGenericCall = __targetTypeForGenericCall.MakeGenericType(__genericParameterTypesForGenericType);");
            }
            
            if (!isGenericConstructor) {
                // TODO: More member kinds

                bool hasMethodForGenericCall = true;
                
                if (memberKind == MemberKind.Method) {
                    sb.AppendLine($"{implPrefix}System.Reflection.MethodInfo __methodForGenericCall = __targetTypeForGenericCall.GetMethod(__memberNameForGenericCall, {numberOfGenericMethodArguments}, __parameterTypesForGenericCall) ?? throw new Exception(\"Method {methodName} not found\");");
                } else if (memberKind == MemberKind.PropertyGetter) {
                    sb.AppendLine($"{implPrefix}System.Reflection.MethodInfo __methodForGenericCall = __targetTypeForGenericCall.GetProperty(__memberNameForGenericCall, typeof({returnTypeName})).GetGetMethod() ?? throw new Exception(\"Property {methodName} or getter method not found\");");
                } else if (memberKind == MemberKind.PropertySetter) {
                    sb.AppendLine($"{implPrefix}System.Reflection.MethodInfo __methodForGenericCall = __targetTypeForGenericCall.GetProperty(__memberNameForGenericCall, typeof({returnTypeName})).GetSetMethod() ?? throw new Exception(\"Property {methodName} or setter method not found\");");
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
                bool isByRefParameter = parameterType.IsByRef;
                
                if (!isOutParameter &&
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
            
            sb.AppendLine("\t}");
        } else {
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

        StringBuilder sb = new();
                
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
        } else if (memberKind != MemberKind.Destructor) {
            foreach (var parameter in parameters) {
                Type parameterType = parameter.ParameterType;

                bool isByRefParameter = parameterType.IsByRef;
                bool isOutParameter = parameter.IsOut;

                if (isByRefParameter) {
                    parameterType = parameterType.GetNonByRefType();
                }
                
                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
                
                string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(
                    targetLanguage,
                    true,
                    isOutParameter,
                    isByRefParameter
                );
                
                string parameterString = $"{unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */ {parameterNamePrefix}{parameter.Name}{parameterNameSuffix}";
                parameterList.Add(parameterString);
            }
        }

        if (mayThrow) {
            Type exceptionType = typeof(Exception);
            TypeDescriptor outExceptionTypeDescriptor = exceptionType.GetTypeDescriptor(typeDescriptorRegistry);
            
            string outExceptionTypeName = outExceptionTypeDescriptor.GetTypeName(
                targetLanguage,
                true,
                true,
                true
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
        out List<string> convertedGenericMethodArgumentNames
    )
    {
        StringBuilder sb = new();
        
        convertedParameterNames = new();
        convertedGenericTypeArgumentNames = new();
        convertedGenericMethodArgumentNames = new();

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
            bool isByRefParameter = parameterType.IsByRef;
            bool isArrayType = parameterType.IsArray;

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
                    convertedParameterName = $"ref {convertedParameterName}";
                }
            } else if (typeConversion != null) {
                string parameterTypeName = parameterTypeDescriptor.GetTypeName(
                    targetLanguage,
                    true,
                    isOutParameter,
                    isByRefParameter
                );
                
                convertedParameterName = $"{parameterName}Converted";
                
                string fullTypeConversion = string.Format(typeConversion, parameterName);
                string typeConversionCode = $"{parameterTypeName} {convertedParameterName} = {fullTypeConversion};";

                sb.AppendLine($"\t{typeConversionCode}");

                if (!isGeneric) {
                    if (isOutParameter) {
                        convertedParameterName = $"out {convertedParameterName}";
                    } else if (isByRefParameter) {
                        convertedParameterName = $"ref {convertedParameterName}";
                    }
                }
            } else {
                if (!isGeneric &&
                    isOutParameter) {
                    convertedParameterName = $"out {parameterName}";
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