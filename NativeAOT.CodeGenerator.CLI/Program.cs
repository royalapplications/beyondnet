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

        var types = typeCollector.Collect(out Dictionary<Type, string> unsupportedTypes);
        
        string namespaceForGeneratedCode = $"GeneratedNativeBindings";

        SourceCodeWriter writer = new();

        string header = $"""
using System;
using System.Runtime.InteropServices;
using NativeAOT.Core;

namespace {namespaceForGeneratedCode};

""";
        
        writer.Write(header, "Header");

        foreach (var kvp in unsupportedTypes) {
            Type type = kvp.Key;
            string reason = kvp.Value;

            string typeName = type.FullName ?? type.Name;
            
            writer.Write(
                $"// Unsupported Type \"{typeName}\": {reason}\n", 
                "Unsupported Types"
            );
        }

        foreach (var type in types) {
            var managedCodeGenerator = new ManagedCodeGenerator(type);

            managedCodeGenerator.Generate(writer);
        }

        StringBuilder sb = new();

        foreach (var section in writer.Sections) {
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
    
    static void ShowUsage()
    {
        Console.WriteLine("Usage: NativeAOT.CodeGenerator.CLI <PathToAssembly.dll> [<PathToOutput.cs>]");    
    }
}