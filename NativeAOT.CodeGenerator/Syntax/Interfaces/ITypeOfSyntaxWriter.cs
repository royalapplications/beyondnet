namespace NativeAOT.CodeGenerator.Syntax;

public interface ITypeOfSyntaxWriter: ISyntaxWriter
{
    string Write(Type type, State state, ISyntaxWriterConfiguration? configuration);
}