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
            typeDescriptorRegistry,
            state
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
        Type returnOrSetterType,
        IEnumerable<ParameterInfo> parameters,
        TypeDescriptorRegistry typeDescriptorRegistry,
        State state
    )
    {
        if (memberInfo == null &&
            memberKind != MemberKind.Destructor) {
            throw new Exception("memberInfo may only be null when memberKind is Destructor");
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
            case MemberKind.PropertyGetter:
            case MemberKind.FieldGetter:
                methodNameC = $"{fullTypeNameC}_{methodName}_Get";
                break;
            case MemberKind.PropertySetter:
            case MemberKind.FieldSetter:
                methodNameC = $"{fullTypeNameC}_{methodName}_Set";
                break;
            default:
                throw new Exception("Unknown method kind");
        }

        methodNameC = state.UniqueGeneratedName(methodNameC, CodeLanguage.CSharpUnmanaged);
        
        state.AddGeneratedMember(
            memberKind,
            memberInfo,
            mayThrow,
            methodNameC,
            CodeLanguage.CSharpUnmanaged
        );

        Type? setterType;

        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter) {
            setterType = returnOrSetterType;
        } else {
            setterType = null;
        }

        string methodSignatureParameters = WriteParameters(
            CodeLanguage.CSharpUnmanaged,
            memberKind,
            setterType,
            mayThrow,
            isStaticMethod,
            declaringType,
            parameters,
            false,
            typeDescriptorRegistry
        );
        
        TypeDescriptor returnOrSetterTypeDescriptor = returnOrSetterType.GetTypeDescriptor(typeDescriptorRegistry);
        string unmanagedReturnOrSetterTypeName = returnOrSetterTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
        string unmanagedReturnOrSetterTypeNameWithComment;

        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter) {
            unmanagedReturnOrSetterTypeNameWithComment = "void /* System.Void */";
        } else {
            unmanagedReturnOrSetterTypeNameWithComment = $"{unmanagedReturnOrSetterTypeName} /* {returnOrSetterType.GetFullNameOrName()} */";
        }
        
        StringBuilder sb = new();
        
        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{methodNameC}\")]");
        sb.AppendLine($"internal static {unmanagedReturnOrSetterTypeNameWithComment} {methodNameC}({methodSignatureParameters})");
        sb.AppendLine("{");

        string? convertedSelfParameterName = null;

        if (!isStaticMethod &&
            memberKind != MemberKind.Destructor) {
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

            methodTarget += declaringType.GetFullNameOrName();
        } else {
            methodTarget = convertedSelfParameterName ?? string.Empty;
        }

        string convertedParameterNamesString = string.Join(", ", convertedParameterNames);

        bool isReturning = memberKind != MemberKind.PropertySetter &&
                           memberKind != MemberKind.FieldSetter &&
                           !returnOrSetterTypeDescriptor.IsVoid;

        string returnValuePrefix = string.Empty;
        string returnValueName = "__returnValue";

        if (isReturning) {
            returnValuePrefix = $"{returnOrSetterType.GetFullNameOrName()} {returnValueName} = ";
        }

        string methodNameForInvocation;

        if (memberKind == MemberKind.Constructor) {
            methodNameForInvocation = string.Empty;
        } else if (memberKind == MemberKind.Destructor) {
            methodNameForInvocation = "InteropUtils.FreeIfAllocated(__self)";
        } else {
            methodNameForInvocation = $".{methodName}";
        }

        bool invocationNeedsParentheses = memberKind != MemberKind.PropertyGetter &&
                                          memberKind != MemberKind.PropertySetter &&
                                          memberKind != MemberKind.FieldGetter &&
                                          memberKind != MemberKind.FieldSetter &&
                                          memberKind != MemberKind.Destructor;

        string methodInvocationPrefix = invocationNeedsParentheses 
            ? "("
            : string.Empty;

        string methodInvocationSuffix;

        if (invocationNeedsParentheses) {
            methodInvocationSuffix = ")";
        } else if (memberKind == MemberKind.PropertySetter ||
                   memberKind == MemberKind.FieldSetter) {
            string valueParamterName = "__value";

            string? setterTypeConversion = returnOrSetterTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharpUnmanaged,
                CodeLanguage.CSharp
            );

            string fullSetterTypeConversion = setterTypeConversion != null
                ? string.Format(setterTypeConversion, valueParamterName)
                : valueParamterName;

            methodInvocationSuffix = $" = {fullSetterTypeConversion}";
        } else {
            methodInvocationSuffix = string.Empty;
        }
        
        sb.AppendLine($"{implPrefix}{returnValuePrefix}{methodTarget}{methodNameForInvocation}{methodInvocationPrefix}{convertedParameterNamesString}{methodInvocationSuffix};");

        string? convertedReturnValueName = null;

        if (isReturning) {
            string? returnValueTypeConversion = returnOrSetterTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharp,
                CodeLanguage.CSharpUnmanaged
            );

            if (returnValueTypeConversion != null) {
                string fullReturnValueTypeConversion = string.Format(returnValueTypeConversion, returnValueName);
                
                convertedReturnValueName = "_returnValueNative";
                
                sb.AppendLine($"{implPrefix}{returnOrSetterTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true)} {convertedReturnValueName} = {fullReturnValueTypeConversion};");
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

            if (isReturning) {
                string returnValue = returnOrSetterTypeDescriptor.GetReturnValueOnException()
                                     ?? $"default({returnOrSetterType.GetFullNameOrName()})";

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
        Type? setterType,
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
            memberKind == MemberKind.FieldSetter) {
            if (setterType == null) {
                throw new Exception("Setter Type may not be null");
            }
            
            TypeDescriptor setterTypeDescriptor = setterType.GetTypeDescriptor(typeDescriptorRegistry);
            string unmanagedSetterTypeName = setterTypeDescriptor.GetTypeName(targetLanguage, true);
    
            string parameterString = $"{unmanagedSetterTypeName} /* {setterType.GetFullNameOrName()} */ {parameterNamePrefix}__value{parameterNameSuffix}";
            parameterList.Add(parameterString);
        } else if (memberKind != MemberKind.Destructor) {
            foreach (var parameter in parameters) {
                Type parameterType = parameter.ParameterType;
                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
                string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(targetLanguage, true);
    
                string parameterString = $"{unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */ {parameterNamePrefix}{parameter.Name}{parameterNameSuffix}";
                parameterList.Add(parameterString);
            }
        }

        if (mayThrow) {
            Type exceptionType = typeof(Exception);
            TypeDescriptor outExceptionTypeDescriptor = exceptionType.GetTypeDescriptor(typeDescriptorRegistry);
            string outExceptionTypeName = outExceptionTypeDescriptor.GetTypeName(targetLanguage, true, true);
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
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

            string? typeConversion = parameterTypeDescriptor.GetTypeConversion(
                sourceLanguage,
                targetLanguage
            );
            
            if (typeConversion != null) {
                string convertedParameterName = $"{parameterName}Converted";
                
                string fullTypeConversion = string.Format(typeConversion, parameterName);
                string typeConversionCode = $"{parameterType.GetFullNameOrName()} {convertedParameterName} = {fullTypeConversion};";

                sb.AppendLine($"\t{typeConversionCode}");
                
                convertedParameterNames.Add(convertedParameterName);
            } else {
                convertedParameterNames.Add(parameterName);
            }
        }

        return sb.ToString();
    }
}