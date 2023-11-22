namespace Beyond.NET.CodeGenerator.Collectors;

public record TypeCollectorSettings(
    bool EnableGenericsSupport,
    Type[] IncludedTypes,
    Type[] ExcludedTypes
);