using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedPropertySyntaxWriter: ICSharpUnmanagedSyntaxWriter, IPropertySyntaxWriter
{
    public string Write(object @object)
    {
        return Write((PropertyInfo)@object);
    }
    
    public string Write(PropertyInfo property)
    {
        StringBuilder sb = new();
        
        string propertyNameC = property.Name;

        sb.AppendLine($"// TODO (Property): {propertyNameC}");

        return sb.ToString();
    }
}