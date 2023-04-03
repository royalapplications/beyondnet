using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Generator.C;
using NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;
using NativeAOT.CodeGenerator.SourceCode;

namespace NativeAOT.CodeGenerator.CLI;

internal class CodeGeneratorDriver
{
    internal string AssemblyPath { get; }
    
    internal string? CSharpUnmanagedOutputPath { get; }
    internal string? COutputPath { get; }
    
    internal Type[] IncludedTypes { get; }
    internal Type[] ExcludedTypes { get; }
    
    internal CodeGeneratorDriver(Configuration configuration)
    {
        AssemblyPath = configuration.AssemblyPath;
        
        CSharpUnmanagedOutputPath = configuration.CSharpUnmanagedOutputPath;
        COutputPath = configuration.COutputPath;
        
        IncludedTypes = TypesFromTypeNames(configuration.IncludedTypeNames ?? Array.Empty<string>());
        ExcludedTypes = TypesFromTypeNames(configuration.ExcludedTypeNames ?? Array.Empty<string>());
    }

    private static Type[] TypesFromTypeNames(IEnumerable<string> typeNames)
    {
        List<Type> types = new();

        foreach (string typeName in typeNames) {
            Type? type = Type.GetType(typeName, true);

            if (type != null) {
                types.Add(type);
            }
        }
        
        return types.ToArray();
    }

    internal void Generate()
    {
        Assembly assembly;
        
        using (AssemblyLoader assemblyLoader = new()) {
            assembly = assemblyLoader.LoadFrom(AssemblyPath);
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
            CSharpUnmanagedOutputPath
        );
        
        WriteCodeToFileOrPrintToConsole(
            "C",
            cCode,
            COutputPath
        );
    }
    
    private void WriteCodeToFileOrPrintToConsole(
        string languageName,
        string code,
        string? outputPath
    )
    {
        if (string.IsNullOrEmpty(outputPath)) {
            return;
        }
        
        outputPath = Path.GetFullPath(outputPath);
            
        File.WriteAllText(outputPath, code);
    }
    
    private HashSet<Type> CollectTypes(
        Assembly assembly,
        out Dictionary<Type, string> unsupportedTypes
    )
    {
        TypeCollector typeCollector = new(
            assembly,
            IncludedTypes,
            ExcludedTypes
        );

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