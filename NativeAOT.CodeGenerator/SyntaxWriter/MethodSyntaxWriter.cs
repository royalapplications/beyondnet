using System.Reflection;

namespace NativeAOT.CodeGenerator;

public interface MethodSyntaxWriter: SyntaxWriter
{
    string Write(MethodInfo method);
}