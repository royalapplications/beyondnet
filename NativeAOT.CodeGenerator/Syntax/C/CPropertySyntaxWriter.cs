using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CPropertySyntaxWriter: ICSyntaxWriter, IPropertySyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((PropertyInfo)@object, state);
    }
    
    public string Write(PropertyInfo property, State state)
    {
        StringBuilder sb = new();
        
        string propertyNameC = property.Name;

        sb.AppendLine($"// TODO (Property): {propertyNameC}");

        return sb.ToString();
    }
}