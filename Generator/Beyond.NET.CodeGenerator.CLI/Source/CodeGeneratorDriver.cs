using System.Reflection;
using System.Text;

using Beyond.NET.Builder;
using Beyond.NET.CodeGenerator.Collectors;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.C;
using Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;
using Beyond.NET.CodeGenerator.Generator.Kotlin;
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
            var loader = m_assemblyLoader;
            
            if (loader is null) {
                string[] assemblySearchPaths = Configuration.AssemblySearchPaths ?? Array.Empty<string>();

                loader = new(assemblySearchPaths);
                
                loader.AssemblyResolved += AssemblyLoader_OnAssemblyResolved;
                loader.GetDocumentation += AssemblyLoader_OnGetDocumentation;
                
                m_assemblyLoader = loader;
            }

            return loader;
        }
    }

    private readonly Dictionary<Assembly, string> m_resolvedAssemblyPaths = new();

    internal CodeGeneratorDriver(Configuration configuration)
    {
        Configuration = configuration;
    }

    internal void Generate()
    {
        HashSet<string> tempDirPaths = new();

        string? finalBuildOutputPath = null;
        
        try {
            #region Configuration
            string assemblyPath = Configuration.AssemblyPath
                .ExpandTildeAndGetAbsolutePath();

            string? kotlinPackageName = Configuration.KotlinPackageName;
            string? kotlinNativeLibraryName = Configuration.KotlinNativeLibraryName;
            
            bool emitUnsupported = Configuration.EmitUnsupported ?? false;
            bool generateTypeCheckedDestroyMethods = Configuration.GenerateTypeCheckedDestroyMethods ?? false;
            bool enableGenericsSupport = Configuration.EnableGenericsSupport ?? false;
            bool doNotGenerateSwiftNestedTypeAliases = Configuration.DoNotGenerateSwiftNestedTypeAliases ?? false;
            bool doNotGenerateDocumentation = Configuration.DoNotGenerateDocumentation ?? false;
    
            BuildConfiguration? buildConfig = Configuration.Build;
    
            bool buildEnabled = false;
            string? buildTarget = null;
            string? buildProductName = null;
            string? buildProductBundleIdentifier = null;
            string? buildProductOutputPath = null;
            string? buildMacOSDeploymentTarget = null;
            string? buildiOSDeploymentTarget = null;
            bool disableParallelBuild = false;
            bool disableStripDotNETSymbols = false;
    
            if (buildConfig is not null) {
                buildEnabled = true;
                
                buildTarget = buildConfig.Target;
                
                if (buildTarget != BuildTargets.APPLE_UNIVERSAL &&
                    buildTarget != BuildTargets.MACOS_UNIVERSAL &&
                    buildTarget != BuildTargets.IOS_UNIVERSAL) {
                    throw new Exception($"Only \"{BuildTargets.APPLE_UNIVERSAL}\", \"{BuildTargets.MACOS_UNIVERSAL}\" and \"{BuildTargets.IOS_UNIVERSAL}\" are currently supported as \"{nameof(buildConfig.Target)}\"");
                }

                var potentialProductName = buildConfig.ProductName;
                string productName;
                
                string assemblyName = Path.GetFileNameWithoutExtension(assemblyPath)
                    .Replace('.', '_');

                if (string.IsNullOrEmpty(potentialProductName)) {
                    // If no product name is provided, let's generate one
                    productName = assemblyName;
                } else {
                    productName = potentialProductName;
                }
    
                buildProductName = productName
                    .Replace('.', '_');

                if (buildProductName == assemblyName &&
                    !buildProductName.EndsWith("Kit")) {
                    // If the product name matches the assembly name, suffix it with "Kit"
                    buildProductName = $"{buildProductName}Kit";
                }
    
                if (string.IsNullOrEmpty(buildProductName)) {
                    throw new Exception($"A build \"{nameof(BuildConfiguration.ProductName)}\" must be provided");
                }

                buildProductBundleIdentifier = buildConfig.ProductBundleIdentifier;

                if (string.IsNullOrEmpty(buildProductBundleIdentifier)) {
                    // In case no bundle identifier is specified, generate one
                    buildProductBundleIdentifier = $"com.mycompany.{buildProductName.ToLower()}";
                }

                var potentialOutputPath = buildConfig.ProductOutputPath;
                string? outputPath;

                if (string.IsNullOrEmpty(potentialOutputPath)) {
                    // If no product output path is provided, use the target assembly's directory path
                    var assemblyDirectoryPath = Path.GetDirectoryName(assemblyPath);

                    outputPath = assemblyDirectoryPath;
                } else {
                    outputPath = potentialOutputPath
                        .ExpandTildeAndGetAbsolutePath();
                }
    
                buildProductOutputPath = outputPath;
    
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

                disableParallelBuild = buildConfig.DisableParallelBuild;
                disableStripDotNETSymbols = buildConfig.DisableStripDotNETSymbols;
            }
            #endregion Configuration

            #region Gather System Information
            var dotNetVersion = Builder.DotNET.Version.GetMajorAndMinorVersion();
            var dotNetTargetFramework = $"net{dotNetVersion}";
            #endregion Gather System Information
            
            #region Load Assembly
            Logger.LogInformation($"Loading assembly from \"{assemblyPath}\"");
            
            Assembly assembly = AssemblyLoader.LoadFrom(assemblyPath);
            #endregion Load Assembly
            
            #region Add System Documentation
            if (!doNotGenerateDocumentation) {
                var systemReferenceAssembliesDirectoryPath = string.Empty;
                
                try {
                    var dotnetDir = DotNETUtils.GetDotnetGlobalInstallLocation();

                    if (string.IsNullOrWhiteSpace(dotnetDir)) {
                        throw new Exception("Cannot determine .NET root directory");
                    }
                    
                    Logger.LogDebug($".NET Core root directory: \"{dotnetDir}\"");
                    
                    var tfm = DotNETUtils.GetTargetFrameworkName(assemblyPath);

                    if (string.IsNullOrWhiteSpace(tfm)) {
                        throw new Exception($"Cannot determine TFM for \"{assemblyPath}\"");
                    }
                    
                    Logger.LogDebug($"TFM for \"{assemblyPath}\": {tfm}");
        
                    var version = DotNETUtils.GetDotNetCoreVersion(tfm);

                    if (version <= 0) {
                        throw new Exception($"Cannot determine .NET version for \"{assemblyPath}\"");
                    }
                    
                    Logger.LogDebug($".NET Core version for \"{assemblyPath}\": {version}");

                    var latestRefDir = DotNETUtils.GetLatestRefPack(dotnetDir, version);

                    if (string.IsNullOrWhiteSpace(latestRefDir)) {
                        throw new Exception($"Cannot find latest reference pack for .NET {version}.x at \"{latestRefDir}\"");
                    }
                    
                    Logger.LogDebug($"Reference pack for .NET {version}.x found at \"{latestRefDir}\"");

                    systemReferenceAssembliesDirectoryPath = latestRefDir;
                } catch (Exception ex) {
                    Logger.LogError($"An error occurred while trying to locate the latest .NET Core Reference Pack: {ex}");
                }
                
                if (string.IsNullOrEmpty(systemReferenceAssembliesDirectoryPath) ||
                    !Directory.Exists(systemReferenceAssembliesDirectoryPath)) {
                    // Fall back to hard coded path
                    // TODO: update to `net9.0` after RTM
                    systemReferenceAssembliesDirectoryPath = $"/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/8.0.0/ref/net8.0";
                    
                    Logger.LogWarning($"Failed to gather path to system reference assemblies - falling back to hard coded path \"{systemReferenceAssembliesDirectoryPath}\"");
                } else {
                    Logger.LogInformation($"Found path to system reference assemblies at \"{systemReferenceAssembliesDirectoryPath}\"");
                }
                
                XmlDocumentationStore.Shared.ParseSystemDocumentation(systemReferenceAssembliesDirectoryPath);
            }
            #endregion Add System Documentation
    
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

            #region Kotlin
            Logger.LogInformation("Generating Kotlin Code");
            
            if (string.IsNullOrEmpty(kotlinPackageName)) {
                // In case no kotlin package name is specified, generate one
                string assemblyName = Path.GetFileNameWithoutExtension(assemblyPath)
                    .Replace('.', '_');
                
                kotlinPackageName = $"com.mycompany.{assemblyName.ToLower()}";
            }

            if (string.IsNullOrEmpty(kotlinNativeLibraryName)) {
                var fallback = "BeyondDotNETSampleNative";
                
                Logger.LogWarning($"Kotlin native library name is empty, using fallback \"{fallback}\" instead. This is very likely not what you want when targeting Kotlin!");
                
                kotlinNativeLibraryName = fallback;
            }
            
            var kotlinResultObject = GenerateKotlinCode(
                types,
                unsupportedTypes,
                cSharpUnmanagedResult,
                cResult,
                emitUnsupported,
                typeCollectorSettings,
                kotlinPackageName,
                kotlinNativeLibraryName
            );
    
            var kotlinResult = kotlinResultObject.Result;
            var kotlinCode = kotlinResultObject.GeneratedCode;
            #endregion Kotlin
            #endregion Generate Code
            
            #region Write Generated Code Output to Files
            string? cSharpUnmanagedOutputPath = Configuration.CSharpUnmanagedOutputPath?
                .ExpandTildeAndGetAbsolutePath();
            
            string? cOutputPath = Configuration.COutputPath?
                .ExpandTildeAndGetAbsolutePath();
            
            string? swiftOutputPath = Configuration.SwiftOutputPath?
                .ExpandTildeAndGetAbsolutePath();
            
            string? kotlinOutputPath = Configuration.KotlinOutputPath?
                .ExpandTildeAndGetAbsolutePath();
            
            // If the user doesn't provide output paths for generated code files but build is enabled, we can use temporary paths
            string? temporaryGeneratedCodeDirPath = null;
    
            // TODO: This assumes that we're always building for Apple platforms
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
                
                if (string.IsNullOrEmpty(kotlinOutputPath)) {
                    kotlinOutputPath = Path.Combine(temporaryGeneratedCodeDirPath, "Generated_Kotlin.kt");
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
            
            WriteCodeToFileOrPrintToConsole(
                "Kotlin",
                kotlinCode,
                kotlinOutputPath
            );
            #endregion Write Generated Code Output to Files
    
            #region Build
            if (buildEnabled) {
                // TODO: This assumes that we're always building for Apple platforms
                if (string.IsNullOrEmpty(buildProductName) ||
                    string.IsNullOrEmpty(buildProductBundleIdentifier) ||
                    string.IsNullOrEmpty(buildProductOutputPath) ||
                    string.IsNullOrEmpty(cSharpUnmanagedOutputPath) ||
                    string.IsNullOrEmpty(cOutputPath) ||
                    string.IsNullOrEmpty(swiftOutputPath) ||
                    string.IsNullOrEmpty(buildMacOSDeploymentTarget) ||
                    string.IsNullOrEmpty(buildiOSDeploymentTarget)) {
                    // We checked all of these above so we shouldn't get here but just in case...
                    throw new Exception("Invalid build configuration");
                }

                Beyond.NET.Builder.BuildTargets builderBuildTargets;

                if (buildTarget == BuildTargets.APPLE_UNIVERSAL) {
                    builderBuildTargets = Beyond.NET.Builder.BuildTargets.AppleUniversal;
                } else if (buildTarget == BuildTargets.MACOS_UNIVERSAL) {
                    builderBuildTargets = Beyond.NET.Builder.BuildTargets.MacOSUniversal;
                } else if (buildTarget == BuildTargets.IOS_UNIVERSAL) {
                    builderBuildTargets = Beyond.NET.Builder.BuildTargets.iOSUniversal;
                } else {
                    throw new Exception($"Build Target \"{buildTarget}\" is not a supported target for the SwiftBuilder");
                }
                
                SwiftBuilder.BuilderConfiguration config = new(
                    builderBuildTargets,
                    buildProductName,
                    buildProductBundleIdentifier,
                    cOutputPath,
                    swiftOutputPath,
                    buildMacOSDeploymentTarget,
                    buildiOSDeploymentTarget,
                    !disableParallelBuild
                );
            
                Logger.LogInformation("Building Swift bindings");
            
                SwiftBuilder swiftBuilder = new(config);
            
                var swiftBuildResult = swiftBuilder.Build();
                
                if (!Directory.Exists(swiftBuildResult.OutputRootPath)) {
                    throw new Exception($"Swift product directory does not exist at \"{swiftBuildResult.OutputRootPath}\"");
                }
                
                tempDirPaths.Add(swiftBuildResult.OutputRootPath);
            
                Logger.LogInformation($"Swift bindings built at \"{swiftBuildResult.OutputRootPath}\"");
            
                Logger.LogInformation("Building .NET Native stuff");

                string[] assemblyReferences = m_resolvedAssemblyPaths
                    .Values
                    .ToArray();

                var dnNativeBuilder = new DotNETNativeBuilder(
                    builderBuildTargets,
                    dotNetTargetFramework,
                    buildProductName,
                    buildProductBundleIdentifier,
                    assemblyPath,
                    assemblyReferences,
                    !disableStripDotNETSymbols,
                    cSharpUnmanagedOutputPath,
                    !disableParallelBuild,
                    swiftBuildResult
                );
            
                var result = dnNativeBuilder.Build();
                
                if (!Directory.Exists(result.OutputDirectoryPath)) {
                    throw new Exception($"Final product directory does not exist at \"{result.OutputDirectoryPath}\"");
                }

                foreach (var tempDirPath in result.TemporaryDirectoryPaths) {
                    if (Directory.Exists(tempDirPath)) {
                        tempDirPaths.Add(tempDirPath);
                    }
                }

                Logger.LogInformation($"Final product built at \"{result.OutputDirectoryPath}\"");
                Logger.LogInformation($"Copying product to \"{buildProductOutputPath}\"");
                
                FileSystemUtils.CopyDirectoryContents(
                    result.OutputDirectoryPath,
                    buildProductOutputPath,
                    true
                );

                finalBuildOutputPath = buildProductOutputPath;
            }
            #endregion Build
        } finally {
            bool doNotDeleteTemporaryDirectories = Configuration.DoNotDeleteTemporaryDirectories ?? false; 
            bool shouldDeleteTempDirs = !doNotDeleteTemporaryDirectories;
            
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

        if (!string.IsNullOrEmpty(finalBuildOutputPath)) {
            Logger.LogInformation("Code Generation and Build completed successfully");
            Logger.LogInformation($"Build Output has been written to \"{finalBuildOutputPath}\"");
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
        
        SwiftCodeGenerator codeGenerator = new(
            settings,
            cSharpUnmanagedResult,
            cResult
        );
        
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
    
    private static CodeGeneratorResult GenerateKotlinCode(
        HashSet<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        Result cSharpUnmanagedResult,
        Result cResult,
        bool emitUnsupported,
        TypeCollectorSettings typeCollectorSettings,
        string kotlinPackageName,
        string kotlinNativeLibraryName
    )
    {
        SourceCodeWriter writer = new();
        
        Generator.Kotlin.Settings settings = new(kotlinPackageName, kotlinNativeLibraryName) {
            EmitUnsupported = emitUnsupported,
            TypeCollectorSettings = typeCollectorSettings
        };
        
        KotlinCodeGenerator codeGenerator = new(
            settings,
            cSharpUnmanagedResult,
            cResult
        );
        
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
    
    private void AssemblyLoader_OnAssemblyResolved(
        object? sender,
        AssemblyEventArgs e
    )
    {
        m_resolvedAssemblyPaths[e.Assembly] = e.AssemblyPath;
        
        Logger.LogDebug($"Assembly \"{e.Assembly.GetName().FullName}\" resolved at path \"{e.AssemblyPath}\"");
    }
    
    private void AssemblyLoader_OnGetDocumentation(
        object? sender,
        AssemblyEventArgs e
    )
    {
        bool doNotGenerateDocumentation = Configuration.DoNotGenerateDocumentation ?? false;
        
        if (doNotGenerateDocumentation) {
            return;
        }
        
        XmlDocumentationStore.Shared.ParseDocumentation(e.Assembly);
    }
}