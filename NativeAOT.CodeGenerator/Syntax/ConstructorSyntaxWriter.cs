using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface ConstructorSyntaxWriter: SyntaxWriter
{
    string Write(ConstructorInfo constructor);
}