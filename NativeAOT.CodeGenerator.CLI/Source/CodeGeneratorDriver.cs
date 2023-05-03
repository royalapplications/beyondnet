using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Generator.C;
using NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;
using NativeAOT.CodeGenerator.Generator.Swift;
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
        #region Configuration
        bool emitUnsupported = Configuration.EmitUnsupported ?? false;
        bool generateTypeCheckedDestroyMethods = Configuration.GenerateTypeCheckedDestroyMethods ?? false;
        bool enableGenericsSupport = Configuration.EnableGenericsSupport ?? false;
        #endregion Configuration
        
        #region Load Assembly
        string assemblyPath = Configuration.AssemblyPath.ExpandTildeAndGetAbsolutePath();
        
        Assembly assembly;
        
        using (AssemblyLoader assemblyLoader = new()) {
            assembly = assemblyLoader.LoadFrom(assemblyPath);
        }
        #endregion Load Assembly

        #region Collect Types
        Type[] includedTypes = TypesFromTypeNames(
            Configuration.IncludedTypeNames ?? Array.Empty<string>(),
            assembly
        );
        
        Type[] excludedTypes = TypesFromTypeNames(
            Configuration.ExcludedTypeNames ?? Array.Empty<string>(),
            assembly
        );

        TypeCollectorSettings typeCollectorSettings = new(
            enableGenericsSupport,
            includedTypes,
            excludedTypes
        );
        
        var types = CollectTypes(
            assembly,
            typeCollectorSettings,
            out Dictionary<Type, string> unsupportedTypes
        );
        #endregion Collect Types

        #region Generate Code
        #region C# Unmanaged
        const string namespaceForCSharpUnamangedCode = "NativeGeneratedCode";

        var cSharpUnmanagedResultObject = GenerateCSharpUnmanagedCode(
            types,
            unsupportedTypes,
            namespaceForCSharpUnamangedCode,
            emitUnsupported,
            generateTypeCheckedDestroyMethods,
            typeCollectorSettings
        );

        var cSharpUnmanagedResult = cSharpUnmanagedResultObject.Result;
        var cSharpUnmanagedCode = cSharpUnmanagedResultObject.GeneratedCode;
        #endregion C# Unmanaged

        #region C
        var cResultObject = GenerateCCode(
            types,
            unsupportedTypes,
            cSharpUnmanagedResult,
            emitUnsupported,
            typeCollectorSettings
        );

        var cResult = cResultObject.Result;
        var cCode = cResultObject.GeneratedCode;
        #endregion C

        #region Swift
        var swiftResultObject = GenerateSwiftCode(
            types,
            unsupportedTypes,
            cSharpUnmanagedResult,
            cResult,
            emitUnsupported,
            typeCollectorSettings
        );

        var swiftResult = swiftResultObject.Result;
        var swiftCode = swiftResultObject.GeneratedCode;
        #endregion Swift
        #endregion Generate Code
        
        #region Write Output to Files
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
        
        string? swiftOutputPath = Configuration.SwiftOutputPath?
            .ExpandTildeAndGetAbsolutePath();
        
        WriteCodeToFileOrPrintToConsole(
            "Swift",
            swiftCode,
            swiftOutputPath
        );
        #endregion Write Output to Files
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

        string? dirPath = Path.GetDirectoryName(outputPath);

        if (!string.IsNullOrEmpty(dirPath) &&
            !Directory.Exists(dirPath)) {
            Directory.CreateDirectory(dirPath);
        }
    
        File.WriteAllText(outputPath, code);
    }
    
    private HashSet<Type> CollectTypes(
        Assembly assembly,
        TypeCollectorSettings settings,
        out Dictionary<Type, string> unsupportedTypes
    )
    {
        TypeCollector typeCollector = new(
            assembly,
            settings
        );

        var types = typeCollector.Collect(out unsupportedTypes);

        types.Remove(typeof(void));

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
        bool generateTypeCheckedDestroyMethods,
        TypeCollectorSettings typeCollectorSettings
    )
    {
        SourceCodeWriter writer = new();

        Generator.CSharpUnmanaged.Settings settings = new(namespaceForGeneratedCode) {
            EmitUnsupported = emitUnsupported,
            GenerateTypeCheckedDestroyMethods = generateTypeCheckedDestroyMethods,
            TypeCollectorSettings = typeCollectorSettings
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
        bool emitUnsupported,
        TypeCollectorSettings typeCollectorSettings
    )
    {
        SourceCodeWriter writer = new();
        
        Generator.C.Settings settings = new() {
            EmitUnsupported = emitUnsupported,
            TypeCollectorSettings = typeCollectorSettings
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
            sb.AppendLine();
        }

        return new(result, sb.ToString());
    }
    
    private static CodeGeneratorResult GenerateSwiftCode(
        HashSet<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        Result cSharpUnmanagedResult,
        Result cResult,
        bool emitUnsupported,
        TypeCollectorSettings typeCollectorSettings
    )
    {
        SourceCodeWriter writer = new();
        
        Generator.Swift.Settings settings = new() {
            EmitUnsupported = emitUnsupported,
            TypeCollectorSettings = typeCollectorSettings
        };
        
        SwiftCodeGenerator codeGenerator = new(settings, cSharpUnmanagedResult, cResult);
        
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
            sb.AppendLine($"// MARK: - BEGIN {section.Name}");
            sb.AppendLine(section.Code.ToString());
            sb.AppendLine($"// MARK: - END {section.Name}");
            sb.AppendLine();
        }

        return new(result, sb.ToString());
    }
}