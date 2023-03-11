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

        StringBuilder sb = new();
        
        sb.AppendLine($"""
using System;
using System.Runtime.InteropServices;
using NativeAOT.Core;

namespace {namespaceForGeneratedCode};

""");

        foreach (var kvp in unsupportedTypes) {
            Type type = kvp.Key;
            string reason = kvp.Value;

            string typeName = type.FullName ?? type.Name;

            sb.AppendLine($"// Unsupported Type \"{typeName}\": {reason}");
            sb.AppendLine();
        }

        foreach (var type in types) {
            var managedCodeGenerator = new ManagedCodeGenerator(type);

            managedCodeGenerator.Generate(sb);
            sb.AppendLine();
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