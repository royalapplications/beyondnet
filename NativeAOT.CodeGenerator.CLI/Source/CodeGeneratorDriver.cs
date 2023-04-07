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
    internal Configuration Configuration { get; }
    
    internal CodeGeneratorDriver(Configuration configuration)
    {
        Configuration = configuration;
    }

    internal void Generate()
    {
        string assemblyPath = Configuration.AssemblyPath.ExpandTildeAndGetAbsolutePath();
        
        Assembly assembly;
        
        using (AssemblyLoader assemblyLoader = new()) {
            assembly = assemblyLoader.LoadFrom(assemblyPath);
        }

        Type[] includedTypes = TypesFromTypeNames(
            Configuration.IncludedTypeNames ?? Array.Empty<string>(),
            assembly
        );
        
        Type[] excludedTypes = TypesFromTypeNames(
            Configuration.ExcludedTypeNames ?? Array.Empty<string>(),
            assembly
        );
        
        var types = CollectTypes(
            assembly,
            includedTypes,
            excludedTypes,
            out Dictionary<Type, string> unsupportedTypes
        );

        bool emitUnsupported = Configuration.EmitUnsupported ?? false;
        bool generateTypeCheckedDestroyMethods = Configuration.GenerateTypeCheckedDestroyMethods ?? false;

        const string namespaceForCSharpUnamangedCode = "NativeGeneratedCode";

        var cSharpUnmanagedResultObject = GenerateCSharpUnmanagedCode(
            types,
            unsupportedTypes,
            namespaceForCSharpUnamangedCode,
            emitUnsupported,
            generateTypeCheckedDestroyMethods
        );

        var cSharpUnmanagedResult = cSharpUnmanagedResultObject.Result;
        var cSharpUnmanagedCode = cSharpUnmanagedResultObject.GeneratedCode;
        
        var cResultObject = GenerateCCode(
            types,
            unsupportedTypes,
            cSharpUnmanagedResult,
            emitUnsupported
        );

        var cResult = cResultObject.Result;
        var cCode = cResultObject.GeneratedCode;
        
        string? cSharpUnmanagedOutputPath = Configuration.CSharpUnmanagedOutputPath?
            .ExpandTildeAndGetAbsolutePath();

        WriteCodeToFileOrPrintToConsole(
            "C#",
            cSharpUnmanagedCode,
            cSharpUnmanagedOutputPath
        );
        
        string? cOutputPath = Configuration.COutputPath?
            .ExpandTildeAndGetAbsolutePath();
        
        WriteCodeToFileOrPrintToConsole(
            "C",
            cCode,
            cOutputPath
        );
    }
    
    private static Type[] TypesFromTypeNames(
        IEnumerable<string> typeNames,
        Assembly assembly
    )
    {
        List<Type> types = new();

        foreach (string typeName in typeNames) {
            Type? type;
            
            try {
                type = assembly.GetType(typeName, true);
            } catch {
                type = Type.GetType(typeName, true);
            }

            if (type != null) {
                types.Add(type);
            }
        }
        
        return types.ToArray();
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
    
        File.WriteAllText(outputPath, code);
    }
    
    private HashSet<Type> CollectTypes(
        Assembly assembly,
        Type[] includedTypes,
        Type[] excludedTypes,
        out Dictionary<Type, string> unsupportedTypes
    )
    {
        TypeCollector typeCollector = new(
            assembly,
            includedTypes,
            excludedTypes
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
        string namespaceForGeneratedCode,
        bool emitUnsupported,
        bool generateTypeCheckedDestroyMethods
    )
    {
        SourceCodeWriter writer = new();

        Generator.CSharpUnmanaged.Settings settings = new(namespaceForGeneratedCode) {
            EmitUnsupported = emitUnsupported,
            GenerateTypeCheckedDestroyMethods = generateTypeCheckedDestroyMethods
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
        Result cSharpUnmanagedResult,
        bool emitUnsupported
    )
    {
        SourceCodeWriter writer = new();
        
        Generator.C.Settings settings = new() {
            EmitUnsupported = emitUnsupported
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