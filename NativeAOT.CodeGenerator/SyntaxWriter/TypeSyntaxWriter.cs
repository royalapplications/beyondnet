namespace NativeAOT.CodeGenerator;

public interface TypeSyntaxWriter: SyntaxWriter
{
    string Write(Type type);
}