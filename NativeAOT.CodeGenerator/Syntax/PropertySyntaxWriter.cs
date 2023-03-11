using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface PropertySyntaxWriter: SyntaxWriter
{
    string Write(PropertyInfo property);
}