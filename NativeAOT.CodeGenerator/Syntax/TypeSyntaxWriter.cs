namespace NativeAOT.CodeGenerator.Syntax;

public interface TypeSyntaxWriter: SyntaxWriter
{
    string Write(Type type);
}