using System.Reflection;
using System.Text;
using NativeAOT.CodeGenerator.Generator;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CFieldSyntaxWriter: ICSyntaxWriter, IFieldSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((FieldInfo)@object, state);
    }
    
    public string Write(FieldInfo field, State state)
    {
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        StringBuilder sb = new();
        
        string fieldNameC = field.Name;
                    
        sb.AppendLine($"// TODO (Field): {fieldNameC}");

        return sb.ToString();
    }
}