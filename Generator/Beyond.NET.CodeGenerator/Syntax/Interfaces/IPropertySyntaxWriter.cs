using System.Reflection;

namespace Beyond.NET.CodeGenerator.Syntax;

public interface IPropertySyntaxWriter: ISyntaxWriter
{
    string Write(PropertyInfo property, State state, ISyntaxWriterConfiguration? configuration);
}