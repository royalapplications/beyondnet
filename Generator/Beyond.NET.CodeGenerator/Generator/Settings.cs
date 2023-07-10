using Beyond.NET.CodeGenerator.Collectors;

namespace Beyond.NET.CodeGenerator.Generator;

public abstract class Settings
{
    public bool EmitUnsupported { get; init; }
    public bool GenerateTypeCheckedDestroyMethods { get; init; }
    public bool GenerateSwiftNestedTypeAliases { get; init; }
    public TypeCollectorSettings? TypeCollectorSettings { get; init; }
}