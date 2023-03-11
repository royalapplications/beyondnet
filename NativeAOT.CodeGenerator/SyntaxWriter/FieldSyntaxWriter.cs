using System.Reflection;

namespace NativeAOT.CodeGenerator;

public interface FieldSyntaxWriter: SyntaxWriter
{
    string Write(FieldInfo field);
}