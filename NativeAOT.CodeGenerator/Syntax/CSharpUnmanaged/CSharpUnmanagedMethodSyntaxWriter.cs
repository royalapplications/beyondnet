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
        
        // TODO: Move out
        TypeDescriptorRegistry typeDescriptorRegistry = new();
        
        string methodNameC = method.Name;
                    
        Type returnType = method.ReturnType;
        TypeDescriptor? typeDescriptor = typeDescriptorRegistry.GetTypeDescriptor(returnType);

        sb.AppendLine($"// TODO (Method): {methodNameC}");

        if (typeDescriptor != null) {
            sb.AppendLine($"// Unmanaged C# Return Type: {typeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged)}");
        }

        return sb.ToString();
    }
}