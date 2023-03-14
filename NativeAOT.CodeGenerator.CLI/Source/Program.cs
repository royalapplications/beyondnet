using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;
using NativeAOT.CodeGenerator.Generator.C;
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

        string? cSharpUnmanagedOutputPath;
        
        if (args.Length > 1) {
            cSharpUnmanagedOutputPath = args[1];
        } else {
            cSharpUnmanagedOutputPath = null;
        }
        
        string? cOutputPath;
        
        if (args.Length > 2) {
            cOutputPath = args[2];
        } else {
            cOutputPath = null;
        }

        Assembly assembly = Assembly.LoadFrom(assemblyPath);

        TypeCollector typeCollector = new(assembly);

        var types = typeCollector.Collect(out Dictionary<Type, string> unsupportedTypes);

        const string namespaceForCSharpUnamangedCode = "NativeGeneratedCode";

        var cSharpUnmanagedResultObject = GenerateCSharpUnmanagedCode(
            types,
            unsupportedTypes,
            namespaceForCSharpUnamangedCode
        );

        var cSharpUnmanagedResult = cSharpUnmanagedResultObject.Item1;
        var cSharpUnmanagedCode = cSharpUnmanagedResultObject.Item2;
        
        var cResultObject = GenerateCCode(
            types,
            unsupportedTypes,
            cSharpUnmanagedResult
        );

        var cResult = cResultObject.Item1;
        var cCode = cResultObject.Item2;

        if (!string.IsNullOrEmpty(cSharpUnmanagedOutputPath)) {
            File.WriteAllText(cSharpUnmanagedOutputPath, cSharpUnmanagedCode);
        } else {
            Console.WriteLine("--- C# BEGIN ---");
            Console.WriteLine(cSharpUnmanagedCode);
            Console.WriteLine("--- C# END ---");
        }

        if (!string.IsNullOrEmpty(cOutputPath)) {
            File.WriteAllText(cOutputPath, cCode);
        } else {
            Console.WriteLine("--- C BEGIN ---");
            Console.WriteLine(cCode);
            Console.WriteLine("--- C END ---");
        }

        return 0;
    }

    private static Tuple<Result, string> GenerateCSharpUnmanagedCode(
        HashSet<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        string namespaceForGeneratedCode
    )
    {
        SourceCodeWriter writer = new();
        
        Generator.CSharpUnmanaged.Settings settings = new(namespaceForGeneratedCode) {
            EmitUnsupported = false
        };
        
        CSharpUnmanagedCodeGenerator codeGenerator = new(settings);
        
        Result result = codeGenerator.Generate(
            types,
            unsupportedTypes,
            writer
        );
        
        StringBuilder sb = new();

        int generatedTypesCount = result.GeneratedTypes.Count;
        int generatedMembersCount = 0;

        foreach (var generatedMembers in result.GeneratedTypes.Values) {
            generatedMembersCount += generatedMembers.Count();
        }

        sb.AppendLine($"// Number of generated types: {generatedTypesCount}");
        sb.AppendLine($"// Number of generated members: {generatedMembersCount}");
        sb.AppendLine();

        foreach (var section in writer.Sections) {
            sb.AppendLine($"// <{section.Name}>");
            sb.AppendLine(section.Code.ToString());
            sb.AppendLine($"// </{section.Name}>");
        }

        return new(result, sb.ToString());
    }
    
    private static Tuple<Result, string> GenerateCCode(
        HashSet<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        Result cSharpUnmanagedResult
    )
    {
        SourceCodeWriter writer = new();
        
        Generator.C.Settings settings = new() {
            EmitUnsupported = false
        };
        
        CCodeGenerator codeGenerator = new(settings, cSharpUnmanagedResult);
        
        Result result = codeGenerator.Generate(
            types,
            unsupportedTypes,
            writer
        );
        
        StringBuilder sb = new();

        int generatedTypesCount = result.GeneratedTypes.Count;
        int generatedMembersCount = 0;

        foreach (var generatedMembers in result.GeneratedTypes.Values) {
            generatedMembersCount += generatedMembers.Count();
        }

        sb.AppendLine($"// Number of generated types: {generatedTypesCount}");
        sb.AppendLine($"// Number of generated members: {generatedMembersCount}");
        sb.AppendLine();

        foreach (var section in writer.Sections) {
            sb.AppendLine($"#pragma mark - BEGIN {section.Name}");
            sb.AppendLine(section.Code.ToString());
            sb.AppendLine($"#pragma mark - END {section.Name}");
        }

        return new(result, sb.ToString());
    }
    
    private static void ShowUsage()
    {
        Console.WriteLine("Usage: NativeAOT.CodeGenerator.CLI <PathToAssembly.dll> [<PathToOutput.cs>]");    
    }
}