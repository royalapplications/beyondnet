namespace NativeAOT.CodeGenerator.CLI;

static class Program
{
    public static int Main(string[] args)
    {
        bool parseSuccess = ArgumentParser.TryParse(
            args,
            out ArgumentParser.Result? result
        );
        
        if (!parseSuccess ||
            result == null) {
            ShowUsage();
            
            return 1;
        }

        string assemblyPath = result.AssemblyPath;
        string? cSharpUnmanagedOutputPath = result.CSharpUnmanagedOutputPath;
        string? cOutputPath = result.COutputPath;

        Configuration configuration = new() {
            AssemblyPath = assemblyPath,
            CSharpUnmanagedOutputPath = cSharpUnmanagedOutputPath,
            COutputPath = cOutputPath,
            IncludedTypeNames = Array.Empty<string>(),
            ExcludedTypeNames = Array.Empty<string>()
        };

        CodeGeneratorDriver driver = new(configuration);
        
        driver.Generate();

        return 0;
    }
    
    private static void ShowUsage()
    {
        string usageText = """
Usage: NativeAOT.CodeGenerator.CLI <PathToAssembly.dll> [<PathToCSharpOutputFile.cs>] [<PathToCHeaderOutputFile.h>]
""";
        
        Console.WriteLine(usageText);    
    }
}