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
        bool parseSuccess = ArgumentParser.TryParse(args, out ArgumentParser.Result? result);
        
        if (!parseSuccess ||
            result == null) {
            ShowUsage();
            
            return 1;
        }

        string assemblyPath = result.AssemblyPath;
        string? cSharpUnmanagedOutputPath = result.CSharpUnmanagedOutputPath;
        string? cOutputPath = result.COutputPath;

        Assembly assembly;
        
        using (AssemblyLoader assemblyLoader = new()) {
            assembly = assemblyLoader.LoadFrom(assemblyPath);
        }
        
        var types = CollectTypes(
            assembly,
            out Dictionary<Type, string> unsupportedTypes
        );

        const string namespaceForCSharpUnamangedCode = "NativeGeneratedCode";

        var cSharpUnmanagedResultObject = GenerateCSharpUnmanagedCode(
            types,
            unsupportedTypes,
            namespaceForCSharpUnamangedCode
        );

        var cSharpUnmanagedResult = cSharpUnmanagedResultObject.Result;
        var cSharpUnmanagedCode = cSharpUnmanagedResultObject.GeneratedCode;
        
        var cResultObject = GenerateCCode(
            types,
            unsupportedTypes,
            cSharpUnmanagedResult
        );

        var cResult = cResultObject.Result;
        var cCode = cResultObject.GeneratedCode;

        WriteCodeToFileOrPrintToConsole(
            "C#",
            cSharpUnmanagedCode,
            cSharpUnmanagedOutputPath
        );
        
        WriteCodeToFileOrPrintToConsole(
            "C",
            cCode,
            cOutputPath
        );

        return 0;
    }

    private static void WriteCodeToFileOrPrintToConsole(
        string languageName,
        string code,
        string? outputPath
    )
    {
        if (!string.IsNullOrEmpty(outputPath)) {
            File.WriteAllText(outputPath, code);
        } else {
            Console.WriteLine($"--- {languageName} BEGIN ---");
            Console.WriteLine(code);
            Console.WriteLine($"--- {languageName} END ---");
        }
    }
    
    private static void ShowUsage()
    {
        string usageText = """
Usage: NativeAOT.CodeGenerator.CLI <PathToAssembly.dll> [<PathToCSharpOutputFile.cs>] [<PathToCHeaderOutputFile.h>]
""";
        
        Console.WriteLine(usageText);    
    }

    private static HashSet<Type> CollectTypes(
        Assembly assembly,
        out Dictionary<Type, string> unsupportedTypes
    )
    {
        TypeCollector typeCollector = new(assembly);

        var types = typeCollector.Collect(out unsupportedTypes);

        return types;
    }

    private struct CodeGeneratorResult
    {
        internal Result Result { get; }
        internal string GeneratedCode { get; }

        internal CodeGeneratorResult(Result result, string generatedCode)
        {
            Result = result;
            GeneratedCode = generatedCode;
        }
    }

    private static CodeGeneratorResult GenerateCSharpUnmanagedCode(
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
    
    private static CodeGeneratorResult GenerateCCode(
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
}