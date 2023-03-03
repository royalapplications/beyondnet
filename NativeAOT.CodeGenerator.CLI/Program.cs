using System.Reflection;

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

        Assembly assembly = Assembly.LoadFrom(assemblyPath);

        ManagedTypeCollector managedTypeCollector = new(assembly);

        var exportedTypes = managedTypeCollector.CollectExportedTypes();

        foreach (var exportedType in exportedTypes) {
            var managedCodeGenerator = new ManagedCodeGenerator(exportedType);

            string generatedManagedCodeForExportedType = managedCodeGenerator.Generate();
            
            Console.WriteLine("---");
            Console.WriteLine(generatedManagedCodeForExportedType);
            Console.WriteLine("---");
        }

        return 0;
    }
    
    static void ShowUsage()
    {
        Console.WriteLine("Usage: NativeAOT.CodeGenerator.CLI <PathToAssembly.dll>");    
    }
}