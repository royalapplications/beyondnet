using NativeAOT.CodeGenerator.Collectors;

namespace NativeAOT.CodeGenerator.Generator;

public abstract class Settings
{
    public bool EmitUnsupported { get; init; }
    public bool GenerateTypeCheckedDestroyMethods { get; init; }
    public TypeCollectorSettings? TypeCollectorSettings { get; init; }
}