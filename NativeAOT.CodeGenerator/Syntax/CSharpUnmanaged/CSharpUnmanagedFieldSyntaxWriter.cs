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

        bool canSet = !field.IsLiteral &&
                      !field.IsInitOnly;
        
        string fieldNameC = field.Name;
                    
        sb.AppendLine($"// TODO (Field Getter): {fieldNameC}");

        if (canSet) {
            sb.AppendLine($"// TODO (Field Setter): {fieldNameC}");
        }

        return sb.ToString();
    }
}