using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface MethodSyntaxWriter: SyntaxWriter
{
    string Write(MethodInfo method);
}