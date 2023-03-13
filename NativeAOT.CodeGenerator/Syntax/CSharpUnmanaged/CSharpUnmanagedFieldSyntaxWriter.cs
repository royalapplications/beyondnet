using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedFieldSyntaxWriter: ICSharpUnmanagedSyntaxWriter, IFieldSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((FieldInfo)@object, state);
    }
    
    public string Write(FieldInfo field, State state)
    {
        StringBuilder sb = new();
        
        string fieldNameC = field.Name;
                    
        sb.AppendLine($"// TODO (Field): {fieldNameC}");

        return sb.ToString();
    }
}