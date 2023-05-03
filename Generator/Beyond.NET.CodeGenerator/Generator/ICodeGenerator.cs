using Beyond.NET.CodeGenerator.SourceCode;

namespace Beyond.NET.CodeGenerator.Generator;

public interface ICodeGenerator
{
    Result Generate(
        IEnumerable<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        SourceCodeWriter writer
    );
}