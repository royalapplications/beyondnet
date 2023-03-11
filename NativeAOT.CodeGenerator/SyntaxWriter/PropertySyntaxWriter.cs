using System.Reflection;

namespace NativeAOT.CodeGenerator;

public interface PropertySyntaxWriter: SyntaxWriter
{
    string Write(PropertyInfo property);
}