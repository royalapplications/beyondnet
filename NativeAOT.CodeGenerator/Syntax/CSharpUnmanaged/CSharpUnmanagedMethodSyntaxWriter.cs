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
        const MethodKind methodKind = MethodKind.Normal;

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
        MemberInfo memberInfo,
        MethodKind methodKind,
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
        string methodNameC;

        switch (methodKind) {
            case MethodKind.Normal:
                methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_{methodName}";
                break;
            case MethodKind.Constructor:
                methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_Create";
                break;
            case MethodKind.PropertyGetter:
                methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_{methodName}_Get";
                break;
            case MethodKind.PropertySetter:
                methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_{methodName}_Set";
                break;
            default:
                throw new Exception("Unknown method kind");
        }

        methodNameC = state.UniqueGeneratedName(methodNameC, CodeLanguage.CSharpUnmanaged);
        
        state.AddGeneratedMember(
            memberInfo,
            mayThrow,
            methodNameC,
            CodeLanguage.CSharpUnmanaged
        );

        Type? setterType = methodKind == MethodKind.PropertySetter 
            ? returnOrSetterType
            : null;

        string methodSignatureParameters = WriteParameters(
            methodKind,
            setterType,
            mayThrow,
            isStaticMethod,
            declaringType,
            parameters,
            typeDescriptorRegistry
        );
        
        TypeDescriptor returnOrSetterTypeDescriptor = returnOrSetterType.GetTypeDescriptor(typeDescriptorRegistry);
        string unmanagedReturnOrSetterTypeName = returnOrSetterTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
        
        string unmanagedReturnOrSetterTypeNameWithComment = methodKind != MethodKind.PropertySetter 
            ? $"{unmanagedReturnOrSetterTypeName} /* {returnOrSetterType.GetFullNameOrName()} */" 
            : "void /* System.Void */";
        
        StringBuilder sb = new();
        
        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{methodNameC}\")]");
        sb.AppendLine($"internal static {unmanagedReturnOrSetterTypeNameWithComment} {methodNameC}({methodSignatureParameters})");
        sb.AppendLine("{");

        string? convertedSelfParameterName = null;

        if (!isStaticMethod) {
            string selfConversionCode = WriteSelfConversion(
                declaringType,
                typeDescriptorRegistry,
                out convertedSelfParameterName
            );

            sb.AppendLine(selfConversionCode);
        }

        string parameterConversions = WriteParameterConversions(
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
        
        string methodTarget = isStaticMethod
            ? (methodKind == MethodKind.Constructor ? "new " : string.Empty) + declaringType.GetFullNameOrName()
            : convertedSelfParameterName ?? string.Empty;

        string convertedParameterNamesString = string.Join(", ", convertedParameterNames);

        bool isReturning = methodKind != MethodKind.PropertySetter &&
                           !returnOrSetterTypeDescriptor.IsVoid;

        string returnValuePrefix = string.Empty;
        string returnValueName = "__returnValue";

        if (isReturning) {
            returnValuePrefix = $"{returnOrSetterType.GetFullNameOrName()} {returnValueName} = ";
        }

        string methodNameForInvocation = methodKind == MethodKind.Constructor
            ? string.Empty
            : $".{methodName}";

        bool invocationNeedsParentheses = methodKind != MethodKind.PropertyGetter &&
                                          methodKind != MethodKind.PropertySetter;

        string methodInvocationPrefix = invocationNeedsParentheses 
            ? "("
            : string.Empty;

        string methodInvocationSuffix;

        if (invocationNeedsParentheses) {
            methodInvocationSuffix = ")";
        } else if (methodKind == MethodKind.PropertySetter) {
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
            string convertedParameterName = $"{parameterName}DotNet";
                
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

    protected string WriteParameters(
        MethodKind methodKind,
        Type? setterType,
        bool mayThrow,
        bool isStatic,
        Type declaringType,
        IEnumerable<ParameterInfo> parameters,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        List<string> parameterList = new();

        if (!isStatic) {
            TypeDescriptor declaringTypeDescriptor = declaringType.GetTypeDescriptor(typeDescriptorRegistry);
            string declaringTypeName = declaringTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
            string selfParameterName = "__self";
            string parameterString = $"{declaringTypeName} /* {declaringType.GetFullNameOrName()} */ {selfParameterName}";

            parameterList.Add(parameterString);
        }

        if (methodKind == MethodKind.PropertySetter) {
            if (setterType == null) {
                throw new Exception("Setter Type may not be null");
            }
            
            TypeDescriptor setterTypeDescriptor = setterType.GetTypeDescriptor(typeDescriptorRegistry);
            string unmanagedSetterTypeName = setterTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
    
            string parameterString = $"{unmanagedSetterTypeName} /* {setterType.GetFullNameOrName()} */ __value";
            parameterList.Add(parameterString);
        } else {
            foreach (var parameter in parameters) {
                Type parameterType = parameter.ParameterType;
                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
                string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
    
                string parameterString = $"{unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */ {parameter.Name}";
                parameterList.Add(parameterString);
            }
        }

        if (mayThrow) {
            Type exceptionType = typeof(Exception);
            TypeDescriptor outExceptionTypeDescriptor = exceptionType.GetTypeDescriptor(typeDescriptorRegistry);
            string outExceptionTypeName = outExceptionTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true, true);
            string outExceptionParameterName = "__outException";

            string outExceptionParameterString = $"{outExceptionTypeName} /* {exceptionType.GetFullNameOrName()} */ {outExceptionParameterName}"; 
            parameterList.Add(outExceptionParameterString);
        }

        string parametersString = string.Join(", ", parameterList);

        return parametersString;
    }

    protected string WriteParameterConversions(
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
                CodeLanguage.CSharpUnmanaged, 
                CodeLanguage.CSharp
            );
            
            if (typeConversion != null) {
                string convertedParameterName = $"{parameterName}DotNet";
                
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