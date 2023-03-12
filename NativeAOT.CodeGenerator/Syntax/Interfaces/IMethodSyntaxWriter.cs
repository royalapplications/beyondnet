using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface IMethodSyntaxWriter: ISyntaxWriter
{
    string Write(MethodInfo method);
}