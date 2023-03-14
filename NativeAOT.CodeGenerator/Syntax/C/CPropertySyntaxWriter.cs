using System.Reflection;
using System.Text;
using NativeAOT.CodeGenerator.Generator;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CPropertySyntaxWriter: ICSyntaxWriter, IPropertySyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((PropertyInfo)@object, state);
    }
    
    public string Write(PropertyInfo property, State state)
    {
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        StringBuilder sb = new();
        
        string propertyNameC = property.Name;

        sb.AppendLine($"// TODO (Property): {propertyNameC}");

        return sb.ToString();
    }
}