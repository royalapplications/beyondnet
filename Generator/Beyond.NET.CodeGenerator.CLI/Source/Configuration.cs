namespace Beyond.NET.CodeGenerator.CLI;

public record Configuration(
    string AssemblyPath,
    
    BuildConfiguration? Build,
    
    string? CSharpUnmanagedOutputPath,
    string? COutputPath,
    string? SwiftOutputPath,
    
    bool? EmitUnsupported,
    bool? GenerateTypeCheckedDestroyMethods,
    bool? DoNotGenerateSwiftNestedTypeAliases,
    bool? EnableGenericsSupport,
    
    string[]? IncludedTypeNames,
    string[]? ExcludedTypeNames,
    
    string[]? AssemblySearchPaths,
    
    bool DoNotDeleteTemporaryDirectories
);