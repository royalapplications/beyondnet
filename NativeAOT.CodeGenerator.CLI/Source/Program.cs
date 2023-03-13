using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;
using NativeAOT.CodeGenerator.SourceCode;

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

        var types = typeCollector.Collect(out Dictionary<Type, string> unsupportedTypes);

        const string namespaceForCSharpUnamangedCode = "NativeGeneratedCode";

        string cSharpUnmanagedCode = GenerateCSharpUnmanagedCode(
            types,
            unsupportedTypes,
            namespaceForCSharpUnamangedCode
        );

        if (!string.IsNullOrEmpty(outputPath)) {
            File.WriteAllText(outputPath, cSharpUnmanagedCode);
        } else {
            Console.WriteLine(cSharpUnmanagedCode);
        }

        return 0;
    }

    private static string GenerateCSharpUnmanagedCode(
        HashSet<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        string namespaceForGeneratedCode
    )
    {
        SourceCodeWriter writer = new();
        
        Settings settings = new(namespaceForGeneratedCode) {
            EmitUnsupported = false
        };
        
        CSharpUnmanagedCodeGenerator cSharpUnmanagedCodeGenerator = new(settings);
        
        Generator.Result result = cSharpUnmanagedCodeGenerator.Generate(types, unsupportedTypes, writer);
        
        StringBuilder sb = new();

        sb.AppendLine($"// Number of generated types: {result.GeneratedTypes.Count}");
        sb.AppendLine();

        foreach (var section in writer.Sections) {
            sb.AppendLine($"// <{section.Name}>");
            sb.AppendLine(section.Code.ToString());
            sb.AppendLine($"// </{section.Name}>");
        }

        return sb.ToString();
    }
    
    private static void ShowUsage()
    {
        Console.WriteLine("Usage: NativeAOT.CodeGenerator.CLI <PathToAssembly.dll> [<PathToOutput.cs>]");    
    }
}