using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedFieldSyntaxWriter: FieldSyntaxWriter
{
    public string Write(FieldInfo field)
    {
        StringBuilder sb = new();
        
        string fieldNameC = field.Name;
                    
        sb.AppendLine($"\t// TODO (Field): {fieldNameC}");

        return sb.ToString();
    }
}