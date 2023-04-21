namespace NativeAOT.CodeGenerator.Syntax;

public interface IDestructorSyntaxWriter: ISyntaxWriter
{
    string Write(Type type, State state, ISyntaxWriterConfiguration? configuration);
}