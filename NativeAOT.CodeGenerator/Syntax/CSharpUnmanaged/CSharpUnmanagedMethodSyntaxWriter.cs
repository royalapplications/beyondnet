using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Types;
using NativeAOT.Core;

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
        
        string methodNameC = method.Name;
                    
        Type returnType = method.ReturnType;
        TypeDescriptor typeDescriptor = typeDescriptorRegistry.GetOrCreateTypeDescriptor(returnType);
        string unmanagedReturnTypeName = typeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);

        string parameters = WriteParameters(method, typeDescriptorRegistry);
        
        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{methodNameC}\")]");
        sb.AppendLine($"internal static {unmanagedReturnTypeName} {methodNameC}({parameters})");
        sb.AppendLine("{");
        sb.AppendLine("\t// TODO: Implementation");
        sb.AppendLine("}");

        return sb.ToString();
    }

    private string WriteParameters(MethodInfo method, TypeDescriptorRegistry typeDescriptorRegistry)
    {
        List<string> parameterList = new();
        
        var parameters = method.GetParameters();

        foreach (var parameter in parameters) {
            Type parameterType = parameter.ParameterType;
            TypeDescriptor parameterTypeDescriptor = typeDescriptorRegistry.GetOrCreateTypeDescriptor(parameterType);
            string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);

            string parameterString = $"{unmanagedParameterTypeName} {parameter.Name}";
            parameterList.Add(parameterString);
        }

        string parametersString = string.Join(", ", parameterList);

        return parametersString;
    }
}