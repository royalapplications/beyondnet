namespace Beyond.NET.CodeGenerator.Collectors;

public record class TypeCollectorSettings(
    bool EnableGenericsSupport,
    Type[] IncludedTypes,
    Type[] ExcludedTypes
) { }