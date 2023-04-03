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

        CodeGeneratorDriver driver = new(
            assemblyPath,
            Array.Empty<Type>(),
            Array.Empty<Type>(),
            cSharpUnmanagedOutputPath,
            cOutputPath
        );
        
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