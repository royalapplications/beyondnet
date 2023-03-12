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
        const bool mayThrow = true;
        
        StringBuilder sb = new();
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        Type declaringType = method.DeclaringType ?? throw new Exception("No declaring type");;
        TypeDescriptor declaringTypeDescriptor = declaringType.GetTypeDescriptor(typeDescriptorRegistry);

        string methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_{method.Name}";
                    
        Type returnType = method.ReturnType;
        TypeDescriptor typeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
        string unmanagedReturnTypeName = typeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
        string unmanagedReturnTypeNameWithComment = $"{unmanagedReturnTypeName} /* {returnType.GetFullNameOrName()} */";

        string methodSignatureParameters = WriteParameters(method, mayThrow, typeDescriptorRegistry);
        
        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{methodNameC}\")]");
        sb.AppendLine($"internal static {unmanagedReturnTypeNameWithComment} {methodNameC}({methodSignatureParameters})");
        sb.AppendLine("{");

        string? convertedSelfParameterName = null;

        if (!method.IsStatic) {
            string parameterName = "self";
            convertedSelfParameterName = parameterName;
                
            string? typeConversion = declaringTypeDescriptor.GetTypeConversion(CodeLanguage.CSharpUnmanaged, CodeLanguage.CSharp);
            
            if (typeConversion != null) {
                string convertedParameterName = $"{parameterName}DotNet";
                
                string fullTypeConversion = string.Format(typeConversion, parameterName);

                bool isSelfPointer = declaringTypeDescriptor.IsPointer;
                string throwPart = isSelfPointer ? $" ?? throw new ArgumentNullException(nameof({parameterName}))" : string.Empty;
                string typeConversionCode = $"{declaringType.GetFullNameOrName()} {convertedParameterName} = {fullTypeConversion}{throwPart};";

                sb.AppendLine($"\t{typeConversionCode}");

                convertedSelfParameterName = convertedParameterName;
            }
        }

        var parameters = method.GetParameters();
        List<string> convertedParameterNames = new();
        
        foreach (var parameter in parameters) {
            string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");
            
            Type parameterType = parameter.ParameterType;
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

            string? typeConversion = parameterTypeDescriptor.GetTypeConversion(CodeLanguage.CSharpUnmanaged, CodeLanguage.CSharp);
            
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

        sb.AppendLine();

        if (mayThrow) {
            sb.AppendLine("""
    try {
""");
        }

        string implPrefix = mayThrow ? "\t\t" : "\t";
        
        string methodTarget = method.IsStatic
            ? declaringType.GetFullNameOrName()
            : convertedSelfParameterName ?? string.Empty;

        string convertedParameterNamesString = string.Join(", ", convertedParameterNames);
        
        sb.AppendLine($"{implPrefix}{methodTarget}.{method.Name}({convertedParameterNamesString})");

        if (mayThrow) {
            sb.AppendLine("""

        if (outException is not null) {
            *outException = null;
        }
    } catch (Exception exception) {
        if (outException is not null) {
            void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                
            *outException = exceptionHandleAddress;
        }
    }
""");
        }

        sb.AppendLine("}");

        return sb.ToString();
    }

    private string WriteParameters(MethodInfo method, bool mayThrow, TypeDescriptorRegistry typeDescriptorRegistry)
    {
        List<string> parameterList = new();

        if (!method.IsStatic) {
            Type? declaringType = method.DeclaringType;

            if (declaringType == null) {
                throw new Exception("No declaring type");
            }

            TypeDescriptor declaringTypeDescriptor = declaringType.GetTypeDescriptor(typeDescriptorRegistry);
            string declaringTypeName = declaringTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
            string parameterString = $"{declaringTypeName} /* {declaringType.GetFullNameOrName()} */ self";

            parameterList.Add(parameterString);
        }
        
        var parameters = method.GetParameters();

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
}