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
        
        string fullTypeName = declaringType.GetFullNameOrName();
        string fullTypeNameC = fullTypeName.CTypeName();
        
        string methodNameC;

        switch (memberKind) {
            case MemberKind.Automatic:
                throw new Exception("MemberKind may not be Automatic here");
            case MemberKind.Method:
                methodNameC = $"{fullTypeNameC}_{methodName}";
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
                methodNameC = $"{fullTypeNameC}_{methodName}_Get";
                break;
            case MemberKind.PropertySetter:
            case MemberKind.FieldSetter:
                methodNameC = $"{fullTypeNameC}_{methodName}_Set";
                break;
            case MemberKind.EventHandlerAdder:
                methodNameC = $"{fullTypeNameC}_{methodName}_Add";
                break;
            case MemberKind.EventHandlerRemover:
                methodNameC = $"{fullTypeNameC}_{methodName}_Remove";
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
            false,
            typeDescriptorRegistry
        );
        
        TypeDescriptor returnOrSetterOrEventHandlerTypeDescriptor = returnOrSetterOrEventHandlerType.GetTypeDescriptor(typeDescriptorRegistry);
        string unmanagedReturnOrSetterOrEventHandlerTypeName = returnOrSetterOrEventHandlerTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
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
            typeDescriptorRegistry,
            out List<string> convertedParameterNames
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
            returnValuePrefix = $"{returnOrSetterOrEventHandlerType.GetFullNameOrName()} {returnValueName} = ";
        }

        string methodNameForInvocation;

        if (memberKind == MemberKind.Constructor) {
            methodNameForInvocation = string.Empty;
        } else if (memberKind == MemberKind.Destructor) {
            methodNameForInvocation = "InteropUtils.FreeIfAllocated(__self)";
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

        if (invocationNeedsParentheses) {
            methodInvocationSuffix = ")";
        } else if (memberKind == MemberKind.PropertySetter ||
                   memberKind == MemberKind.FieldSetter) {
            string valueParamterName = "__value";

            string? setterTypeConversion = returnOrSetterOrEventHandlerTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharpUnmanaged,
                CodeLanguage.CSharp
            );

            string fullSetterTypeConversion = setterTypeConversion != null
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
        
        sb.AppendLine($"{implPrefix}{returnValuePrefix}{methodTarget}{methodNameForInvocation}{methodInvocationPrefix}{convertedParameterNamesString}{methodInvocationSuffix};");

        string? convertedReturnValueName = null;

        if (isReturning) {
            string? returnValueTypeConversion = returnOrSetterOrEventHandlerTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharp,
                CodeLanguage.CSharpUnmanaged
            );

            if (returnValueTypeConversion != null) {
                string fullReturnValueTypeConversion = string.Format(returnValueTypeConversion, returnValueName);
                
                convertedReturnValueName = "_returnValueNative";
                
                sb.AppendLine($"{implPrefix}{returnOrSetterOrEventHandlerTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true)} {convertedReturnValueName} = {fullReturnValueTypeConversion};");
            } else {
                convertedReturnValueName = returnValueName;
            }
        }

        if (mayThrow) {
            sb.AppendLine("""

        if (__outException is not null) {
            *__outException = null;
        }

""");

            foreach (var parameter in parameters) {
                Type parameterType = parameter.ParameterType;

                bool isOutParameter = parameter.IsOut;
                bool isByRefParameter = parameterType.IsByRef;
                
                if (!isOutParameter &&
                    !isByRefParameter) {
                    continue;
                }

                parameterType = parameterType.GetNonByRefType();
                
                string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");
                string convertedParameterName = $"{parameterName}Converted";

                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

                string? parameterTypeConversion = parameterTypeDescriptor.GetTypeConversion(
                    CodeLanguage.CSharp,
                    CodeLanguage.CSharpUnmanaged
                );

                string parameterModifier = isOutParameter 
                    ? "out"
                    : "ref";

                if (!convertedParameterNames.Contains($"{parameterModifier} {convertedParameterName}")) {
                    convertedParameterName = parameterName;
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
                string returnValue = returnOrSetterOrEventHandlerTypeDescriptor.GetDefaultValue()
                                     ?? $"default({returnOrSetterOrEventHandlerType.GetFullNameOrName()})";

                sb.AppendLine($"{implPrefix}return {returnValue};");
            }
            
            sb.AppendLine("\t}");
        } else {
            if (isReturning) {
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
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
        
        StringBuilder sb = new();
        string parameterName = "__self";
        convertedSelfParameterName = parameterName;
                
        string? typeConversion = typeDescriptor.GetTypeConversion(CodeLanguage.CSharpUnmanaged, CodeLanguage.CSharp);
            
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
        TypeDescriptorRegistry typeDescriptorRegistry,
        out List<string> convertedParameterNames
    )
    {
        StringBuilder sb = new();
        convertedParameterNames = new();
        
        foreach (var parameter in parameters) {
            string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");
            
            Type parameterType = parameter.ParameterType;
            bool isOutParameter = parameter.IsOut;
            bool isByRefParameter = parameterType.IsByRef;

            if (!isByRefParameter &&
                isOutParameter) {
                throw new Exception("Parameter is out but not by ref, that's impossible");
            } else if (isByRefParameter) {
                parameterType = parameterType.GetNonByRefType();
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

                sb.AppendLine($"\t{typeName} {convertedParameterName};");
                
                convertedParameterName = $"out {convertedParameterName}";
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

                convertedParameterName = $"ref {convertedParameterName}";
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

                if (isOutParameter) {
                    convertedParameterName = $"out {convertedParameterName}";
                } else if (isByRefParameter) {
                    convertedParameterName = $"ref {convertedParameterName}";
                }
            } else {
                if (isOutParameter) {
                    convertedParameterName = $"out {parameterName}";
                } else if (isByRefParameter) {
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