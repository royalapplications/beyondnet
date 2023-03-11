using System.Reflection;

namespace NativeAOT.CodeGenerator;

public interface ConstructorSyntaxWriter: SyntaxWriter
{
    string Write(ConstructorInfo constructor);
}