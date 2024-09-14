namespace Beyond.NET.CodeGenerator.CLI;

public record Configuration(
    string AssemblyPath,
    
    BuildConfiguration? Build,
    
    string? CSharpUnmanagedOutputPath,
    string? COutputPath,
    string? SwiftOutputPath,
    string? KotlinOutputPath,
    
    string? KotlinPackageName,
    string? KotlinNativeLibraryName,
    
    bool? EmitUnsupported,
    bool? GenerateTypeCheckedDestroyMethods,
    bool? EnableGenericsSupport,
    bool? DoNotGenerateSwiftNestedTypeAliases,
    bool? DoNotGenerateDocumentation,
    bool? DoNotDeleteTemporaryDirectories,
    
    string[]? IncludedTypeNames,
    string[]? ExcludedTypeNames,
    
    string[]? AssemblySearchPaths
);