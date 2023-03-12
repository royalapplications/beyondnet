using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface IPropertySyntaxWriter: ISyntaxWriter
{
    string Write(PropertyInfo property);
}