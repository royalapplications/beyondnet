using System.Reflection;
using System.Text;

using Beyond.NET.Builder;
using Beyond.NET.CodeGenerator.Collectors;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.C;
using Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;
using Beyond.NET.CodeGenerator.Generator.Swift;
using Beyond.NET.CodeGenerator.SourceCode;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.CLI;

internal class CodeGeneratorDriver
{
    private ILogger Logger => Services.Shared.LoggerService;
    
    internal Configuration Configuration { get; }

    private AssemblyLoader? m_assemblyLoader;
    private AssemblyLoader AssemblyLoader
    {
        get {
            if (m_assemblyLoader is null) {
                string[] assemblySearchPaths = Configuration.AssemblySearchPaths ?? Array.Empty<string>();

                m_assemblyLoader = new(assemblySearchPaths);
            }

            return m_assemblyLoader;
        }
    }
    
    internal CodeGeneratorDriver(Configuration configuration)
    {
        Configuration = configuration;
    }

    internal void Generate()
    {
        HashSet<string> tempDirPaths = new();
        
        try {
            #region Configuration
            bool emitUnsupported = Configuration.EmitUnsupported ?? false;
            bool generateTypeCheckedDestroyMethods = Configuration.GenerateTypeCheckedDestroyMethods ?? false;
            bool enableGenericsSupport = Configuration.EnableGenericsSupport ?? false;
            bool doNotGenerateSwiftNestedTypeAliases = Configuration.DoNotGenerateSwiftNestedTypeAliases ?? false;
    
            BuildConfiguration? buildConfig = Configuration.Build;
    
            bool buildEnabled = false;
            string? buildTarget = null;
            string? buildProductName = null;
            string? buildProductOutputPath = null;
            string? buildMacOSDeploymentTarget = null;
            string? buildiOSDeploymentTarget = null;
    
            if (buildConfig is not null) {
                buildEnabled = true;
                
                buildTarget = buildConfig.Target;
                
                if (buildTarget != BuildTargets.APPLE_UNIVERSAL) {
                    throw new Exception($"Only \"{BuildTargets.APPLE_UNIVERSAL}\" is currently supported as \"{nameof(buildConfig.Target)}\"");
                }
    
                buildProductName = buildConfig.ProductName;
    
                if (string.IsNullOrEmpty(buildProductName)) {
                    throw new Exception($"A build \"{nameof(BuildConfiguration.ProductName)}\" must be provided");
                }
    
                buildProductOutputPath = buildConfig.ProductOutputPath;
    
                if (string.IsNullOrEmpty(buildProductOutputPath)) {
                    throw new Exception($"A build \"{nameof(BuildConfiguration.ProductOutputPath)}\" must be provided");
                }
    
                buildMacOSDeploymentTarget = buildConfig.MacOSDeploymentTarget;
    
                if (string.IsNullOrEmpty(buildMacOSDeploymentTarget)) {
                    buildMacOSDeploymentTarget = AppleDeploymentTargets.MACOS_DEFAULT;
                }
    
                buildiOSDeploymentTarget = buildConfig.iOSDeploymentTarget;
    
                if (string.IsNullOrEmpty(buildiOSDeploymentTarget)) {
                    buildiOSDeploymentTarget = AppleDeploymentTargets.IOS_DEFAULT;
                }
            }
            #endregion Configuration
    
            #region Load Assembly
            string assemblyPath = Configuration.AssemblyPath.ExpandTildeAndGetAbsolutePath();
            
            Logger.LogInformation($"Loading assembly from \"{assemblyPath}\"");
            
            Assembly assembly = AssemblyLoader.LoadFrom(assemblyPath);
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
            
            Logger.LogInformation($"Collecting types in assembly");
            
            var types = CollectTypes(
                assembly,
                typeCollectorSettings,
                out Dictionary<Type, string> unsupportedTypes
            );
            #endregion Collect Types
    
            #region Generate Code
            #region C# Unmanaged
            const string namespaceForCSharpUnamangedCode = "NativeGeneratedCode";
    
            Logger.LogInformation("Generating C# Code");
            
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
            Logger.LogInformation("Generating C Code");
            
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
            Logger.LogInformation("Generating Swift Code");
            
            var swiftResultObject = GenerateSwiftCode(
                types,
                unsupportedTypes,
                cSharpUnmanagedResult,
                cResult,
                emitUnsupported,
                doNotGenerateSwiftNestedTypeAliases,
                typeCollectorSettings
            );
    
            var swiftResult = swiftResultObject.Result;
            var swiftCode = swiftResultObject.GeneratedCode;
            #endregion Swift
            #endregion Generate Code
            
            #region Write Generated Code Output to Files
            string? cSharpUnmanagedOutputPath = Configuration.CSharpUnmanagedOutputPath?
                .ExpandTildeAndGetAbsolutePath();
            
            string? cOutputPath = Configuration.COutputPath?
                .ExpandTildeAndGetAbsolutePath();
            
            string? swiftOutputPath = Configuration.SwiftOutputPath?
                .ExpandTildeAndGetAbsolutePath();
            
            // If the user doesn't provide output paths for generated code files but build is enabled, we can use temporary paths
            string? temporaryGeneratedCodeDirPath = null;
    
            if (buildEnabled &&
                !string.IsNullOrEmpty(buildProductName) &&
                (string.IsNullOrEmpty(cSharpUnmanagedOutputPath) ||
                 string.IsNullOrEmpty(cOutputPath) ||
                 string.IsNullOrEmpty(swiftOutputPath))) {
                string sanitizedProductName = buildProductName.SanitizedProductNameForTempDirectory();
                string tempDirectoryPrefix = $"BeyondNETCodeGenerator_{sanitizedProductName}_";
    
                temporaryGeneratedCodeDirPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix).FullName;

                tempDirPaths.Add(temporaryGeneratedCodeDirPath);
    
                if (string.IsNullOrEmpty(cSharpUnmanagedOutputPath)) {
                    cSharpUnmanagedOutputPath = Path.Combine(temporaryGeneratedCodeDirPath, "Generated_CS.cs");
                }
                
                if (string.IsNullOrEmpty(cOutputPath)) {
                    cOutputPath = Path.Combine(temporaryGeneratedCodeDirPath, "Generated_C.h");
                }
                
                if (string.IsNullOrEmpty(swiftOutputPath)) {
                    swiftOutputPath = Path.Combine(temporaryGeneratedCodeDirPath, "Generated_Swift.swift");
                }
            }
     
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
    
            WriteCodeToFileOrPrintToConsole(
                "Swift",
                swiftCode,
                swiftOutputPath
            );
            #endregion Write Generated Code Output to Files
    
            #region Build
            if (buildEnabled) {
                if (string.IsNullOrEmpty(buildProductName) ||
                    string.IsNullOrEmpty(buildProductOutputPath) ||
                    string.IsNullOrEmpty(cSharpUnmanagedOutputPath) ||
                    string.IsNullOrEmpty(cOutputPath) ||
                    string.IsNullOrEmpty(swiftOutputPath) ||
                    string.IsNullOrEmpty(buildMacOSDeploymentTarget) ||
                    string.IsNullOrEmpty(buildiOSDeploymentTarget)) {
                    // We checked all of these above so we shouldn't get here but just in case...
                    throw new Exception("Invalid build configuration");
                }
                
                SwiftBuilder.BuilderConfiguration config = new(
                    buildProductName,
                    cOutputPath,
                    swiftOutputPath,
                    buildMacOSDeploymentTarget,
                    buildiOSDeploymentTarget
                );
            
                Logger.LogInformation("Building Swift bindings");
            
                SwiftBuilder swiftBuilder = new(config);
            
                var swiftBuildResult = swiftBuilder.Build();
                
                if (!Directory.Exists(swiftBuildResult.OutputRootPath)) {
                    throw new Exception($"Swift product directory does not exist at \"{swiftBuildResult.OutputRootPath}\"");
                }
                
                tempDirPaths.Add(swiftBuildResult.OutputRootPath);
            
                Logger.LogInformation($"Swift bindings built at \"{swiftBuildResult.OutputRootPath}\"");

                string dnVersion = Builder.DotNET.Version.GetMajorAndMinorVersion();
                string targetFramework = $"net{dnVersion}";
            
                Logger.LogInformation("Building .NET Native stuff");
    
                var dnNativeBuilder = new DotNETNativeBuilder(
                    targetFramework,
                    buildProductName,
                    assemblyPath,
                    cSharpUnmanagedOutputPath,
                    swiftBuildResult
                );
            
                var result = dnNativeBuilder.Build();
                
                if (!Directory.Exists(result.OutputDirectoryPath)) {
                    throw new Exception($"Final product directory does not exist at \"{result.OutputDirectoryPath}\"");
                }
                
                if (Directory.Exists(result.TemporaryDirectoryPath)) {
                    tempDirPaths.Add(result.TemporaryDirectoryPath);
                }

                Logger.LogInformation($"Final product built at \"{result.OutputDirectoryPath}\"");
                Logger.LogInformation($"Copying product to \"{buildProductOutputPath}\"");
                
                CopyDirectory(
                    result.OutputDirectoryPath,
                    buildProductOutputPath,
                    true
                );
            }
            #endregion Build
        } finally {
            bool shouldDeleteTempDirs = !Configuration.DoNotDeleteTemporaryDirectories;
            
            if (shouldDeleteTempDirs) {
                // Clean up temporary directories
                foreach (var tempDirPath in tempDirPaths) {
                    if (Directory.Exists(tempDirPath)) {
                        Logger.LogInformation($"Removing temporary directory \"{tempDirPath}\"");
                    
                        Directory.Delete(tempDirPath, true);
                    }
                }
            }
        }
    }

    private static void CopyDirectory(
        string sourceDir,
        string destinationDir,
        bool recursive
    )
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDir);

        // Check if the source directory exists
        if (!dir.Exists) {
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");
        }

        // Cache directories before we start copying
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (!Directory.Exists(destinationDir)) {
            // Create the destination directory
            Directory.CreateDirectory(destinationDir);
        }

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles()) {
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            
            file.CopyTo(
                targetFilePath,
                true
            );
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive) {
            foreach (DirectoryInfo subDir in dirs) {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                
                CopyDirectory(
                    subDir.FullName,
                    newDestinationDir,
                    true
                );
            }
        }
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

            if (type is not null) {
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
        
        Logger.LogInformation($"Writing generated {languageName} code to \"{outputPath}\"");
    
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
        bool doNotGenerateSwiftNestedTypeAliases,
        TypeCollectorSettings typeCollectorSettings
    )
    {
        SourceCodeWriter writer = new();
        
        Generator.Swift.Settings settings = new() {
            EmitUnsupported = emitUnsupported,
            TypeCollectorSettings = typeCollectorSettings,
            DoNotGenerateSwiftNestedTypeAliases = doNotGenerateSwiftNestedTypeAliases
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