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
        bool parseSuccess = ParseArguments(
            args,
            out string? assemblyPath,
            out string? cSharpUnmanagedOutputPath,
            out string? cOutputPath
        );

        if (!parseSuccess ||
            assemblyPath == null) {
            ShowUsage();

            return 1;
        }
        
        AppDomain.CurrentDomain.AssemblyResolve += AppDomain_OnAssemblyResolve;

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

    private static bool ParseArguments(
        string[] args,
        out string? assemblyPath,
        out string? cSharpUnmanagedOutputPath,
        out string? cOutputPath
    )
    {
        const string cSharpFileExtension = ".cs";
        const string cHeaderFileExtension = ".h";
        
        assemblyPath = null;
        cSharpUnmanagedOutputPath = null;
        cOutputPath = null;
        
        if (args.Length <= 0) {
            return false;
        }

        assemblyPath = args[0].Trim();

        if (string.IsNullOrWhiteSpace(assemblyPath)) {
            return false;
        }

        if (args.Length < 2) {
            return true;
        }

        for (int i = 1; i < args.Length; i++) {
            string path = args[i].Trim();

            if (path.EndsWith(cSharpFileExtension, StringComparison.InvariantCultureIgnoreCase)) {
                cSharpUnmanagedOutputPath = path;
            } else if (path.EndsWith(cHeaderFileExtension, StringComparison.InvariantCultureIgnoreCase)) {
                cOutputPath = path;
            } else {
                return false;
            }
        }

        return true;
    }
    
    private static void ShowUsage()
    {
        string usageText = """
Usage: NativeAOT.CodeGenerator.CLI <PathToAssembly.dll> [<PathToCSharpOutputFile.cs>] [<PathToCHeaderOutputFile.h>]
""";
        
        Console.WriteLine(usageText);    
    }

    private static Assembly? AppDomain_OnAssemblyResolve(object? sender, ResolveEventArgs args)
    {
        string assemblyFullName = args.Name;
        AssemblyName assemblyNameObject = new(assemblyFullName);
        string? assemblyName = assemblyNameObject.Name;

        if (string.IsNullOrEmpty(assemblyName)) {
            return null;
        }

        if (!assemblyName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase)) {
            assemblyName += ".dll";
        }
        
        try {
            return Assembly.LoadFrom(assemblyName);
        } catch {
            var searchPaths = GetAssemblySearchPaths();

            foreach (var searchPath in searchPaths) {
                string potentialAssemblyPath = Path.Combine(
                    searchPath,
                    assemblyName
                );

                try {
                    return Assembly.LoadFrom(potentialAssemblyPath);
                } catch {
                    // ignored
                }
            }
        }

        return null;
    }

    private static IEnumerable<string> GetAssemblySearchPaths()
    {
        List<string> searchPaths = new() {
            Environment.CurrentDirectory
        };

        string? processPath = Environment.ProcessPath;

        if (!string.IsNullOrEmpty(processPath)) {
            string? processDirectoryPath = Path.GetDirectoryName(processPath);
    
            if (!string.IsNullOrEmpty(processDirectoryPath)) {
                if (!searchPaths.Contains(processDirectoryPath)) {
                    searchPaths.Add(processDirectoryPath);
                }
            }
        }

        return searchPaths;
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
}