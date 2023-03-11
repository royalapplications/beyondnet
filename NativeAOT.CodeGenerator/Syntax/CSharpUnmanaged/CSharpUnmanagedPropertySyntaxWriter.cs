using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedPropertySyntaxWriter: PropertySyntaxWriter
{
    public string Write(PropertyInfo property)
    {
        StringBuilder sb = new();
        
        string propertyNameC = property.Name;

        sb.AppendLine($"\t// TODO (Property): {propertyNameC}");

        return sb.ToString();
    }
}