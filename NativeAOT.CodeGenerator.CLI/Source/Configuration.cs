namespace NativeAOT.CodeGenerator.CLI;

public struct Configuration
{
    public string AssemblyPath { get; init; }
    
    public string? CSharpUnmanagedOutputPath { get; init; }
    public string? COutputPath { get; init; }
    
    public string[]? IncludedTypeNames { get; init; }
    public string[]? ExcludedTypeNames { get; init; }
}