using NativeAOT.CodeGenerator.SourceCode;

namespace NativeAOT.CodeGenerator.Generator;

public interface ICodeGenerator
{
    Result Generate(
        IEnumerable<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        SourceCodeWriter writer
    );
}