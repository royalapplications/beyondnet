using System.Reflection;

namespace Beyond.NET.CodeGenerator.Syntax;

public interface IConstructorSyntaxWriter: ISyntaxWriter
{
    string Write(ConstructorInfo constructor, State state, ISyntaxWriterConfiguration? configuration);
}