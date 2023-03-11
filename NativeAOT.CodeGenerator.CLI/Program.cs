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

        SourceCodeWriter cSharpUnmanagedWriter = GenerateCSharpUnmanagedCode(types, unsupportedTypes);

        StringBuilder sb = new();

        foreach (var section in cSharpUnmanagedWriter.Sections) {
            sb.AppendLine($"// <{section.Name}>");
            sb.AppendLine(section.Code.ToString());
            sb.AppendLine($"// </{section.Name}>");
        }

        string outputString = sb.ToString();

        if (!string.IsNullOrEmpty(outputPath)) {
            File.WriteAllText(outputPath, outputString);
        } else {
            Console.WriteLine(outputString);
        }

        return 0;
    }

    private static SourceCodeWriter GenerateCSharpUnmanagedCode(HashSet<Type> types, Dictionary<Type, string> unsupportedTypes)
    {
        SourceCodeWriter writer = new();
        CSharpUnmanagedCodeGenerator cSharpUnmanagedCodeGenerator = new();
        
        cSharpUnmanagedCodeGenerator.Generate(types, unsupportedTypes, writer);

        return writer;
    }
    
    private static void ShowUsage()
    {
        Console.WriteLine("Usage: NativeAOT.CodeGenerator.CLI <PathToAssembly.dll> [<PathToOutput.cs>]");    
    }
}