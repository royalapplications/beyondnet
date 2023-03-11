using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedFieldSyntaxWriter: ICSharpUnmanagedSyntaxWriter, IFieldSyntaxWriter
{
    public string Write(object @object)
    {
        return Write((FieldInfo)@object);
    }
    
    public string Write(FieldInfo field)
    {
        StringBuilder sb = new();
        
        string fieldNameC = field.Name;
                    
        sb.AppendLine($"// TODO (Field): {fieldNameC}");

        return sb.ToString();
    }
}