using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedMethodSyntaxWriter: ICSharpUnmanagedSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object)
    {
        return Write((MethodInfo)@object);
    }
    
    public string Write(MethodInfo method)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        const bool mayThrow = true;
        const bool isConstructor = false;

        bool isStaticMethod = method.IsStatic;
        string methodName = method.Name;

        Type declaringType = method.DeclaringType ?? throw new Exception("No declaring type");;
        Type returnType = method.ReturnType;
        IEnumerable<ParameterInfo> parameters = method.GetParameters();

        string methodCode = WriteMethod(
            methodName,
            isStaticMethod,
            isConstructor,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            typeDescriptorRegistry
        );

        return methodCode;
    }

    protected string WriteMethod(
        string methodName,
        bool isStaticMethod,
        bool isConstructor,
        bool mayThrow,
        Type declaringType,
        Type returnType,
        IEnumerable<ParameterInfo> parameters,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        string methodNameC;

        if (isConstructor) {
            methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_Create";
        } else {
            methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_{methodName}";
        }
        
        TypeDescriptor declaringTypeDescriptor = declaringType.GetTypeDescriptor(typeDescriptorRegistry);
        
        TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
        string unmanagedReturnTypeName = returnTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
        string unmanagedReturnTypeNameWithComment = $"{unmanagedReturnTypeName} /* {returnType.GetFullNameOrName()} */";

        string methodSignatureParameters = WriteParameters(
            mayThrow,
            isStaticMethod,
            declaringType,
            parameters,
            typeDescriptorRegistry
        );
        
        StringBuilder sb = new();
        
        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{methodNameC}\")]");
        sb.AppendLine($"internal static {unmanagedReturnTypeNameWithComment} {methodNameC}({methodSignatureParameters})");
        sb.AppendLine("{");

        string? convertedSelfParameterName = null;

        if (!isStaticMethod) {
            string selfConversionCode = WriteSelfConversion(
                declaringType,
                declaringTypeDescriptor,
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
            ? (isConstructor ? "new " : string.Empty) + declaringType.GetFullNameOrName()
            : convertedSelfParameterName ?? string.Empty;

        string convertedParameterNamesString = string.Join(", ", convertedParameterNames);

        bool isReturning = !returnTypeDescriptor.IsVoid;

        string returnValuePrefix = string.Empty;
        string returnValueName = "returnValue";

        if (isReturning) {
            returnValuePrefix = $"{returnType.GetFullNameOrName()} {returnValueName} = ";
        }

        string methodNameForInvocation = isConstructor
            ? string.Empty
            : $".{methodName}";
        
        sb.AppendLine($"{implPrefix}{returnValuePrefix}{methodTarget}{methodNameForInvocation}({convertedParameterNamesString})");

        string? convertedReturnValueName = null;

        if (isReturning) {
            string? returnValueTypeConversion = returnTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharp,
                CodeLanguage.CSharpUnmanaged
            );

            if (returnValueTypeConversion != null) {
                string fullReturnValueTypeConversion = string.Format(returnValueTypeConversion, returnValueName);
                
                convertedReturnValueName = "returnValueNative";
                
                sb.AppendLine($"{implPrefix}{returnTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true)} {convertedReturnValueName} = {fullReturnValueTypeConversion};");
            } else {
                convertedReturnValueName = returnValueName;
            }
        }

        if (mayThrow) {
            sb.AppendLine("""

        if (outException is not null) {
            *outException = null;
        }

""");
            
            if (isReturning) {
                sb.AppendLine($"{implPrefix}return {convertedReturnValueName};");
            }
            
            sb.AppendLine("""
    } catch (Exception exception) {
        if (outException is not null) {
            void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                
            *outException = exceptionHandleAddress;
        }

""");

            if (isReturning) {
                string returnValue = returnTypeDescriptor.GetReturnValueOnException()
                                     ?? $"default({returnType.GetFullNameOrName()})";

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
        TypeDescriptor typeDescriptor,
        out string convertedSelfParameterName
    )
    {
        StringBuilder sb = new();
        string parameterName = "self";
        convertedSelfParameterName = parameterName;
                
        string? typeConversion = typeDescriptor.GetTypeConversion(CodeLanguage.CSharpUnmanaged, CodeLanguage.CSharp);
            
        if (typeConversion != null) {
            string convertedParameterName = $"{parameterName}DotNet";
                
            string fullTypeConversion = string.Format(typeConversion, parameterName);

            bool isSelfPointer = typeDescriptor.RequiresNativePointer;
            string throwPart = isSelfPointer ? $" ?? throw new ArgumentNullException(nameof({parameterName}))" : string.Empty;
            string typeConversionCode = $"{type.GetFullNameOrName()} {convertedParameterName} = {fullTypeConversion}{throwPart};";

            sb.AppendLine($"\t{typeConversionCode}");

            convertedSelfParameterName = convertedParameterName;
        }

        return sb.ToString();
    }

    protected string WriteParameters(
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
            string parameterString = $"{declaringTypeName} /* {declaringType.GetFullNameOrName()} */ self";

            parameterList.Add(parameterString);
        }

        foreach (var parameter in parameters) {
            Type parameterType = parameter.ParameterType;
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
            string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);

            string parameterString = $"{unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */ {parameter.Name}";
            parameterList.Add(parameterString);
        }

        if (mayThrow) {
            Type exceptionType = typeof(Exception);
            TypeDescriptor outExceptionTypeDescriptor = exceptionType.GetTypeDescriptor(typeDescriptorRegistry);
            string outExceptionTypeName = outExceptionTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true, true);

            string outExceptionParameterString = $"{outExceptionTypeName} /* {exceptionType.GetFullNameOrName()} */ outException"; 
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