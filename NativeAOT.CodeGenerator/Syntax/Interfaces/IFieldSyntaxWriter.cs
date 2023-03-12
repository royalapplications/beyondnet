using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface IFieldSyntaxWriter: ISyntaxWriter
{
    string Write(FieldInfo field);
}