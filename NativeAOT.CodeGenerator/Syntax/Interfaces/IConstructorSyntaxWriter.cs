using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface IConstructorSyntaxWriter: ISyntaxWriter
{
    string Write(ConstructorInfo constructor, State state);
}