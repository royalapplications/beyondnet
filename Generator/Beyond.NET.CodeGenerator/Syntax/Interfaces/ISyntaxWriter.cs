namespace Beyond.NET.CodeGenerator.Syntax;

public interface ISyntaxWriter
{
    string Write(object @object, State state, ISyntaxWriterConfiguration? configuration);
}