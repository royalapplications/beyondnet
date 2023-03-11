using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface FieldSyntaxWriter: SyntaxWriter
{
    string Write(FieldInfo field);
}