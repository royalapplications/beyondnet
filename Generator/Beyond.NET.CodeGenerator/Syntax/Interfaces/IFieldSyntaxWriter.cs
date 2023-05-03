using System.Reflection;

namespace Beyond.NET.CodeGenerator.Syntax;

public interface IFieldSyntaxWriter: ISyntaxWriter
{
    string Write(FieldInfo field, State state, ISyntaxWriterConfiguration? configuration);
}