using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface ISyntaxWriter
{
    string Write(object @object);
}