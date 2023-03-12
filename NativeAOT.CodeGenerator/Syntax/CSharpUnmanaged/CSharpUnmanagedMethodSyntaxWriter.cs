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
        StringBuilder sb = new();
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        Type declaringType = method.DeclaringType ?? throw new Exception("No declaring type");;

        string methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_{method.Name}";
                    
        Type returnType = method.ReturnType;
        TypeDescriptor typeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
        string unmanagedReturnTypeName = typeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
        string unmanagedReturnTypeNameWithComment = $"{unmanagedReturnTypeName} /* {returnType.GetFullNameOrName()} */";

        string parameters = WriteParameters(method, typeDescriptorRegistry);
        
        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{methodNameC}\")]");
        sb.AppendLine($"internal static {unmanagedReturnTypeNameWithComment} {methodNameC}({parameters})");
        sb.AppendLine("{");
        sb.AppendLine("\t// TODO: Implementation");
        sb.AppendLine("}");

        return sb.ToString();
    }

    private string WriteParameters(MethodInfo method, TypeDescriptorRegistry typeDescriptorRegistry)
    {
        const bool mayThrow = true;
        
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