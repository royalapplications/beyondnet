namespace NativeAOT.CodeGenerator.Syntax;

public interface ITypeSyntaxWriter: ISyntaxWriter
{
    string Write(Type type, State state, ISyntaxWriterConfiguration? configuration);
}