using NativeAOT.CodeGenerator.SourceCode;

namespace NativeAOT.CodeGenerator.Generator;

public interface ICodeGenerator
{
    void Generate(
        IEnumerable<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        SourceCodeWriter writer
    );
}