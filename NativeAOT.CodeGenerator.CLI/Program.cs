using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.CLI;

static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length <= 0) {
            ShowUsage();
            
            return 1;
        }

        string assemblyPath = args[0];

        if (string.IsNullOrWhiteSpace(assemblyPath)) {
            ShowUsage();

            return 1;
        }

        string? outputPath;
        
        if (args.Length > 1) {
            outputPath = args[1];
        } else {
            outputPath = null;
        }

        Assembly assembly = Assembly.LoadFrom(assemblyPath);

        TypeCollector typeCollector = new(assembly);

        var publicTypes = typeCollector.Collect();

        StringBuilder sb = new();

        foreach (var exportedType in publicTypes) {
            var managedCodeGenerator = new ManagedCodeGenerator(exportedType);

            string generatedManagedCodeForExportedType = managedCodeGenerator.Generate();

            if (!string.IsNullOrEmpty(generatedManagedCodeForExportedType)) {
                sb.AppendLine("---");
                sb.AppendLine(generatedManagedCodeForExportedType);
                sb.AppendLine("---");
            }
        }

        string outputString = sb.ToString();

        if (!string.IsNullOrEmpty(outputPath)) {
            File.WriteAllText(outputPath, outputString);
        } else {
            Console.WriteLine(outputString);
        }

        return 0;
    }
    
    static void ShowUsage()
    {
        Console.WriteLine("Usage: NativeAOT.CodeGenerator.CLI <PathToAssembly.dll> [<PathToOutput.cs>]");    
    }
}