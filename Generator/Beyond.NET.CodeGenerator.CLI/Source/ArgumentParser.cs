using System.Diagnostics.CodeAnalysis;

namespace Beyond.NET.CodeGenerator.CLI;

internal class ArgumentParser
{
    internal class Result
    {
        internal string AssemblyPath { get; }
        
        internal string? CSharpUnmanagedOutputPath { get; }
        internal string? COutputPath { get; }

        internal Result(
            string assemblyPath,
            string? cSharpUnmanagedOutputPath,
            string? cOutputPath
        )
        {
            AssemblyPath = assemblyPath ?? throw new ArgumentNullException(nameof(assemblyPath));
            CSharpUnmanagedOutputPath = cSharpUnmanagedOutputPath;
            COutputPath = cOutputPath;
        }
    }

    internal static bool TryParse(
        string[] args, 
        [NotNullWhen(true)] out Result? result
    )
    {
        result = null;
        
        const string cSharpFileExtension = ".cs";
        const string cHeaderFileExtension = ".h";

        string? cSharpUnmanagedOutputPath = null;
        string? cOutputPath = null;
        
        if (args.Length <= 0) {
            return false;
        }

        string assemblyPath = args[0].Trim();

        if (string.IsNullOrWhiteSpace(assemblyPath)) {
            return false;
        }

        if (args.Length > 1) {
            for (int i = 1; i < args.Length; i++) {
                string path = args[i].Trim();
    
                if (path.EndsWith(cSharpFileExtension, StringComparison.InvariantCultureIgnoreCase)) {
                    cSharpUnmanagedOutputPath = path;
                } else if (path.EndsWith(cHeaderFileExtension, StringComparison.InvariantCultureIgnoreCase)) {
                    cOutputPath = path;
                } else {
                    return false;
                }
            }
        }

        result = new(
            assemblyPath,
            cSharpUnmanagedOutputPath,
            cOutputPath
        );

        return true;
    }
}