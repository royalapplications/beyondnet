using System.Reflection;

namespace Beyond.NET.CodeGenerator.Syntax;

public interface IMethodSyntaxWriter: ISyntaxWriter
{
    string Write(MethodInfo method, State state, ISyntaxWriterConfiguration? configuration);
}