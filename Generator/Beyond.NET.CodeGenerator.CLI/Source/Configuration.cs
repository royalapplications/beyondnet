namespace Beyond.NET.CodeGenerator.CLI;

public struct Configuration
{
    public string AssemblyPath { get; init; }
    
    public string? CSharpUnmanagedOutputPath { get; init; }
    public string? COutputPath { get; init; }
    public string? SwiftOutputPath { get; init; }

    public bool? EmitUnsupported { get; init; }
    public bool? GenerateTypeCheckedDestroyMethods { get; init; }
    public bool? GenerateSwiftNestedTypeAliases { get; init; }
    public bool? EnableGenericsSupport { get; init; }
    
    public string[]? IncludedTypeNames { get; init; }
    public string[]? ExcludedTypeNames { get; init; }
    
    public string[]? AssemblySearchPaths { get; init; }
}