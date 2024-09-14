using Beyond.NET.Core;

namespace Beyond.NET.Builder;

public class SwiftBuilder
{
    public record BuilderConfiguration(
        BuildTargets Targets,
        string ProductName,
        string BundleIdentifier,
        string GeneratedCHeaderFilePath,
        string GeneratedSwiftFilePath,
        string DeploymentTargetMacOS,
        string DeploymentTargetiOS,
        bool BuildInParallel
    );
    
    public record BuildResult(
        string OutputRootPath,
        
        string LibraryOutputPathFormat,
        string SymbolsOutputPathFormat,
        
        string OutputClangModuleMapFilePath,
        string macOSDeploymentTarget,
        string iOSDeploymentTarget,
        
        PartialCompileResult? MacOSARM64Result,
        PartialCompileResult? MacOSX64Result,
        PartialCompileResult? iOSARM64Result,
        PartialCompileResult? iOSSimulatorARM64Result,
        PartialCompileResult? iOSSimulatorX64Result
    );
    
    public record PartialCompileResult(
        string StandardOutput,
        string LibraryOutputFilePath,
        string ModuleInterfaceOutputFilePath,
        string ModuleOutputFilePath,
        string SymbolsOutputFilePath,
        string? SwiftDocOutputFilePath,
        string? SwiftABIOutputFilePath
    );

    private ILogger Logger => Services.Shared.LoggerService;
    
    public BuilderConfiguration Configuration { get; }

    public SwiftBuilder(BuilderConfiguration configuration)
    {
        Configuration = configuration;
    }

    public BuildResult Build()
    {
        const string swiftVersion = "5";
        
        string productName = Configuration.ProductName;
        string bundleIdentifier = Configuration.BundleIdentifier;

        string deploymentTargetMacOS = Configuration.DeploymentTargetMacOS;
        string deploymentTargetiOS = Configuration.DeploymentTargetiOS;

        string generatedCHeaderFilePath = Configuration.GeneratedCHeaderFilePath;
        string generatedSwiftFilePath = Configuration.GeneratedSwiftFilePath;

        string generatedCHeaderFileName = Path.GetFileName(generatedCHeaderFilePath);
        string generatedSwiftFileName = Path.GetFileName(generatedSwiftFilePath);

        const string moduleMapFileName = "module.modulemap";
        const string clangModuleMapFileName = "module_clang.modulemap";
        const string headerMapFileName = "headers.yaml";

        string sanitizedProductName = productName.SanitizedProductNameForTempDirectory();
        string tempDirectoryPrefix = $"BeyondNETBuilderSwiftBuilder_{sanitizedProductName}_";
        
        Logger.LogDebug("Creating temporary directory for Swift build");
        
        string tempDirectoryPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix).FullName;
        
        Logger.LogDebug($"Temporary directory for Swift build created at \"{tempDirectoryPath}\"");

        string moduleMapFilePath = Path.Combine(tempDirectoryPath, moduleMapFileName);
        string clangModuleMapFilePath = Path.Combine(tempDirectoryPath, clangModuleMapFileName);
        string headerMapFilePath = Path.Combine(tempDirectoryPath, headerMapFileName);

        Apple.Clang.ModuleMap moduleMap = new(productName) {
            Headers = new [] {
                new Apple.Clang.ModuleMap.Header(generatedCHeaderFileName) {
                    Type = Apple.Clang.ModuleMap.Header.Types.Private
                }
            }
        };
        
        Apple.Clang.ModuleMap clangModuleMap = new(productName) {
            IsFramework = true,
            ExportEverything = true
        };

        Apple.Clang.VFSOverlay.HeaderFile headerFile = new() {
            Version = 0,
            CaseSensitive = false,
            Roots = new [] {
                new Apple.Clang.VFSOverlay.HeaderFileRoot() {
                    Type = Apple.Clang.VFSOverlay.HeaderFile.TYPE_DIRECTORY,
                    Name = ".",
                    Contents = new [] {
                        new Apple.Clang.VFSOverlay.HeaderFileContent() {
                            Type = Apple.Clang.VFSOverlay.HeaderFile.TYPE_FILE,
                            Name = generatedCHeaderFileName,
                            ExternalContents = generatedCHeaderFileName
                        },
                        new Apple.Clang.VFSOverlay.HeaderFileContent() {
                            Type = Apple.Clang.VFSOverlay.HeaderFile.TYPE_FILE,
                            Name = moduleMapFileName,
                            ExternalContents = moduleMapFileName
                        }
                    }
                }
            }
        };

        string moduleMapString = moduleMap.ToString();
        string clangModuleMapString = clangModuleMap.ToString();
        string headerMapString = headerFile.ToString();
        
        Logger.LogDebug($"Writing Swift module map file to \"{moduleMapFilePath}\"");
        File.WriteAllText(moduleMapFilePath, moduleMapString);
        
        Logger.LogDebug($"Writing Clang module map file to \"{clangModuleMapFilePath}\"");
        File.WriteAllText(clangModuleMapFilePath, clangModuleMapString);
        
        Logger.LogDebug($"Writing Header map file to \"{headerMapFilePath}\"");
        File.WriteAllText(headerMapFilePath, headerMapString);
        
        string generatedCHeaderDestinationFilePath = Path.Combine(tempDirectoryPath, generatedCHeaderFileName);
        Logger.LogDebug($"Copying generated C header file to \"{generatedCHeaderDestinationFilePath}\"");
        File.Copy(generatedCHeaderFilePath, generatedCHeaderDestinationFilePath);

        string generatedSwiftDestinationFilePath = Path.Combine(tempDirectoryPath, generatedSwiftFileName);
        Logger.LogDebug($"Copying generated Swift file to \"{generatedSwiftDestinationFilePath}\"");
        File.Copy(generatedSwiftFilePath, generatedSwiftDestinationFilePath);

        string? sdkPathMacOS;

        if (Configuration.Targets.ContainsAnyMacOSTarget()) {
            Logger.LogDebug($"Getting macOS SDK Path");
            sdkPathMacOS = Apple.XCRun.SDK.GetSDKPath(Apple.XCRun.SDK.macOSName);
        } else {
            sdkPathMacOS = null;
        }
        
        string? sdkPathiOS;

        if (Configuration.Targets.HasFlag(BuildTargets.iOSARM64)) {
            Logger.LogDebug($"Getting iOS SDK Path");
            sdkPathiOS = Apple.XCRun.SDK.GetSDKPath(Apple.XCRun.SDK.iOSName);
        } else {
            sdkPathiOS = null;
        }
        
        string? sdkPathiOSSimulator;

        if (Configuration.Targets.ContainsAnyiOSSimulatorTarget()) {
            Logger.LogDebug($"Getting iOS Simulator SDK Path");
            sdkPathiOSSimulator = Apple.XCRun.SDK.GetSDKPath(Apple.XCRun.SDK.iOSSimulatorName);
        } else {
            sdkPathiOSSimulator = null;
        }

        string targetIdentifierARM64 = Apple.XCRun.SwiftC.TargetIdentifier.ARM64;
        string targetIdentifierX64 = Apple.XCRun.SwiftC.TargetIdentifier.x64;

        string targetIdentifierARM64DN = DotNET.TargetIdentifier.ARM64;
        string targetIdentifierX64DN = DotNET.TargetIdentifier.x64;

        string platformIdentifierMacOS = Apple.XCRun.SwiftC.PlatformIdentifier.macOS;
        string platformIdentifierMacOSDN = DotNET.PlatformIdentifier.macOS;

        string platformIdentifieriOS = Apple.XCRun.SwiftC.PlatformIdentifier.iOS;
        string platformIdentifieriOSDN = DotNET.PlatformIdentifier.iOS;

        // string platformIdentifieriOSSimulator = "apple-ios-simulator";
        string platformIdentifieriOSSimulatorDN = DotNET.PlatformIdentifier.iOSSimulator;

        string platformSuffixiOSSimulator = Apple.XCRun.SwiftC.PlatformIdentifier.SimulatorSuffix;

        string outputPathRoot = Path.Combine(tempDirectoryPath, "bin");
        string outputPathApple = Path.Combine(outputPathRoot, "apple");

        string? outputPathMacOSARM64 = Configuration.Targets.HasFlag(BuildTargets.MacOSARM64)
            ? Path.Combine(outputPathApple, $"{platformIdentifierMacOSDN}-{targetIdentifierARM64DN}") 
            : null;
        
        string? outputPathMacOSX64 = Configuration.Targets.HasFlag(BuildTargets.MacOSX64)
            ? Path.Combine(outputPathApple, $"{platformIdentifierMacOSDN}-{targetIdentifierX64DN}")
            : null;

        string? outputPathiOSARM64 = Configuration.Targets.HasFlag(BuildTargets.iOSARM64)
            ? Path.Combine(outputPathApple, $"{platformIdentifieriOSDN}-{targetIdentifierARM64DN}")
            : null;

        string? outputPathiOSSimulatorARM64 = Configuration.Targets.HasFlag(BuildTargets.iOSSimulatorARM64)
            ? Path.Combine(outputPathApple, $"{platformIdentifieriOSSimulatorDN}-{targetIdentifierARM64DN}")
            : null;

        string? outputPathiOSSimulatorX64 = Configuration.Targets.HasFlag(BuildTargets.iOSSimulatorX64)
            ? Path.Combine(outputPathApple, $"{platformIdentifieriOSSimulatorDN}-{targetIdentifierX64DN}")
            : null;

        if (outputPathMacOSARM64 is not null) {
            Logger.LogDebug($"Creating macOS ARM64 Output Path at \"{outputPathMacOSARM64}\"");
            Directory.CreateDirectory(outputPathMacOSARM64);
        }

        if (outputPathMacOSX64 is not null) {
            Logger.LogDebug($"Creating macOS x64 Output Path at \"{outputPathMacOSX64}\"");
            Directory.CreateDirectory(outputPathMacOSX64);
        }

        if (outputPathiOSARM64 is not null) {
            Logger.LogDebug($"Creating iOS ARM64 Output Path at \"{outputPathMacOSX64}\"");
            Directory.CreateDirectory(outputPathiOSARM64);
        }

        if (outputPathiOSSimulatorARM64 is not null) {
            Logger.LogDebug($"Creating iOS Simulator ARM64 Output Path at \"{outputPathMacOSX64}\"");
            Directory.CreateDirectory(outputPathiOSSimulatorARM64);
        }

        if (outputPathiOSSimulatorX64 is not null) {
            Logger.LogDebug($"Creating iOS Simulator x64 Output Path at \"{outputPathMacOSX64}\"");
            Directory.CreateDirectory(outputPathiOSSimulatorX64);
        }

        SwiftCompiler compiler = new(
            tempDirectoryPath,
            swiftVersion,
            productName,
            bundleIdentifier,
            generatedSwiftFileName,
            generatedCHeaderFileName,
            headerMapFileName
        );
        
        #region macOS
        Func<PartialCompileResult>? macOSARM64Func;

        if (Configuration.Targets.HasFlag(BuildTargets.MacOSARM64)) {
            macOSARM64Func = () => compiler.Compile(
                sdkPathMacOS!,
                targetIdentifierARM64,
                platformIdentifierMacOS,
                string.Empty,
                deploymentTargetMacOS,
                outputPathMacOSARM64!
            );
        } else {
            macOSARM64Func = null;
        }
        
        Func<PartialCompileResult>? macOSX64Func;

        if (Configuration.Targets.HasFlag(BuildTargets.MacOSX64)) {
            macOSX64Func = () => compiler.Compile(
                sdkPathMacOS!,
                targetIdentifierX64,
                platformIdentifierMacOS,
                string.Empty,
                deploymentTargetMacOS,
                outputPathMacOSX64!
            );
        } else {
            macOSX64Func = null;
        }
        #endregion macOS

        #region iOS
        Func<PartialCompileResult>? iOSARM64Func;

        if (Configuration.Targets.HasFlag(BuildTargets.iOSARM64)) {
            iOSARM64Func = () => compiler.Compile(
                sdkPathiOS!,
                targetIdentifierARM64,
                platformIdentifieriOS,
                string.Empty,
                deploymentTargetiOS,
                outputPathiOSARM64!
            );
        } else {
            iOSARM64Func = null;
        }
        #endregion iOS

        #region iOS Simulator
        Func<PartialCompileResult>? iOSSimulatorARM64Func;

        if (Configuration.Targets.HasFlag(BuildTargets.iOSSimulatorARM64)) {
            iOSSimulatorARM64Func = () => compiler.Compile(
                sdkPathiOSSimulator!,
                targetIdentifierARM64,
                platformIdentifieriOS,
                platformSuffixiOSSimulator,
                deploymentTargetiOS,
                outputPathiOSSimulatorARM64!
            );
        } else {
            iOSSimulatorARM64Func = null;
        }
        
        Func<PartialCompileResult>? iOSSimulatorX64Func;

        if (Configuration.Targets.HasFlag(BuildTargets.iOSSimulatorX64)) {
            iOSSimulatorX64Func = () => compiler.Compile(
                sdkPathiOSSimulator!,
                targetIdentifierX64,
                platformIdentifieriOS,
                platformSuffixiOSSimulator,
                deploymentTargetiOS,
                outputPathiOSSimulatorX64!
            );
        } else {
            iOSSimulatorX64Func = null;
        }
        #endregion iOS Simulator
        
        string libraryOutputPathFormat = Path.Combine(
            outputPathApple,
            "{0}", // Runtime Identifier
            $"{productName}.a"
        );
        
        string symbolsOutputPathFormat = Path.Combine(
            outputPathApple,
            "{0}", // Runtime Identifier
            $"{productName}.export"
        );

        PartialCompileResult? macOSARM64Result;
        PartialCompileResult? macOSX64Result;
        PartialCompileResult? iOSARM64Result;
        PartialCompileResult? iOSSimulatorARM64Result;
        PartialCompileResult? iOSSimulatorX64Result;

        if (Configuration.BuildInParallel) {
            List<Task> tasks = new();

            Task<PartialCompileResult>? macOSARM64Task = null;
            if (macOSARM64Func is not null) {
                macOSARM64Task = Task.Run(macOSARM64Func);
                tasks.Add(macOSARM64Task);
            }
            
            Task<PartialCompileResult>? macOSX64Task = null;
            if (macOSX64Func is not null) {
                macOSX64Task = Task.Run(macOSX64Func);
                tasks.Add(macOSX64Task);
            }
            
            Task<PartialCompileResult>? iOSARM64Task = null;
            if (iOSARM64Func is not null) {
                iOSARM64Task = Task.Run(iOSARM64Func);
                tasks.Add(iOSARM64Task);
            }
            
            Task<PartialCompileResult>? iOSSimulatorARM64Task = null;
            if (iOSSimulatorARM64Func is not null) {
                iOSSimulatorARM64Task = Task.Run(iOSSimulatorARM64Func); 
                tasks.Add(iOSSimulatorARM64Task);
            }
            
            Task<PartialCompileResult>? iOSSimulatorX64Task = null;
            if (iOSSimulatorX64Func is not null) {
                iOSSimulatorX64Task = Task.Run(iOSSimulatorX64Func); 
                tasks.Add(iOSSimulatorX64Task);
            }
            
            Logger.LogDebug($"Waiting for {tasks.Count} Swift compilation tasks to complete in parallel");

            Task.WaitAll(tasks.ToArray());
            
            macOSARM64Result = macOSARM64Task?.Result;
            macOSX64Result = macOSX64Task?.Result;
            iOSARM64Result = iOSARM64Task?.Result;
            iOSSimulatorARM64Result = iOSSimulatorARM64Task?.Result;
            iOSSimulatorX64Result = iOSSimulatorX64Task?.Result;
        } else {
           macOSARM64Result = macOSARM64Func?.Invoke();
           macOSX64Result = macOSX64Func?.Invoke();
           iOSARM64Result = iOSARM64Func?.Invoke();
           iOSSimulatorARM64Result = iOSSimulatorARM64Func?.Invoke();
           iOSSimulatorX64Result = iOSSimulatorX64Func?.Invoke();
        }

        BuildResult result = new(
            tempDirectoryPath,
            libraryOutputPathFormat,
            symbolsOutputPathFormat,
            clangModuleMapFilePath,
            deploymentTargetMacOS,
            deploymentTargetiOS,
            macOSARM64Result,
            macOSX64Result,
            iOSARM64Result,
            iOSSimulatorARM64Result,
            iOSSimulatorX64Result
        );

        return result;
    }

    private record SwiftCompiler(
        string WorkingDirectory,
        string SwiftVersion,
        string ProductName,
        string BundleIdentifier,
        string SwiftFileName,
        string CHeaderFileName,
        string HeaderMapFileName
    )
    {
        private ILogger Logger => Services.Shared.LoggerService;
        
        internal PartialCompileResult Compile(
            string sdk,
            string targetIdentifier,
            string platformIdentifier,
            string platformSuffix,
            string deploymentTarget,
            string outputPath
        )
        {
            return Compile(
                WorkingDirectory,
                SwiftVersion,
                sdk,
                targetIdentifier,
                platformIdentifier,
                platformSuffix,
                deploymentTarget,
                ProductName,
                BundleIdentifier,
                SwiftFileName,
                CHeaderFileName,
                HeaderMapFileName,
                outputPath
            );
        }

        private PartialCompileResult Compile(
            string workingDirectory,
            string swiftVersion,
            string sdk,
            string targetIdentifier,
            string platformIdentifier,
            string platformSuffix,
            string deploymentTarget,
            string productName,
            string bundleIdentifier,
            string swiftFileName,
            string cHeaderFileName,
            string headerMapFileName,
            string outputPath
        )
        {
            string targetDouble = Apple.XCRun.SwiftC.TargetDouble.Make(
                targetIdentifier,
                platformIdentifier,
                platformSuffix
            );

            string targetTriple = Apple.XCRun.SwiftC.TargetTriple.Make(
                targetIdentifier,
                platformIdentifier,
                deploymentTarget,
                platformSuffix
            );

            string outputProductName = productName;

            string objectOutputFilePath = Path.Combine(outputPath, $"{outputProductName}.o");
            string libraryOutputFilePath = Path.Combine(outputPath, $"{outputProductName}.a");

            string moduleInterfaceOutputFilePath = Path.Combine(outputPath, $"{targetDouble}.{Apple.XCRun.SwiftC.FileExtensions.SwiftInterface}");
            string moduleOutputFilePath = Path.Combine(outputPath, $"{targetDouble}.{Apple.XCRun.SwiftC.FileExtensions.SwiftModule}");

            string symbolsOutputFilePath = Path.Combine(outputPath, $"{outputProductName}.export");

            List<string> args = new(new[] {
                Apple.XCRun.SwiftC.App.ARGUMENT_SDK, sdk,
                Apple.XCRun.SwiftC.App.ARGUMENT_TARGET, targetTriple,
                Apple.XCRun.SwiftC.App.ARGUMENT_WORKING_DIRECTORY, ".",
                Apple.XCRun.SwiftC.App.ARGUMENT_IMPORT_SEARCH_PATH, ".",
                Apple.XCRun.SwiftC.App.ARGUMENT_VFSOVERLAY, headerMapFileName,
                Apple.XCRun.SwiftC.App.FLAG_OPTIMIZATIONS,
                Apple.XCRun.SwiftC.App.FLAG_WHOLE_MODULE_OPTIMIZATION,
                Apple.XCRun.SwiftC.App.ARGUMENT_SWIFT_VERSION, swiftVersion,
                Apple.XCRun.SwiftC.App.FLAG_DISABLE_CROSS_MODULE_OPTIMIZATION,
                Apple.XCRun.SwiftC.App.FLAG_ENABLE_TESTING,
                Apple.XCRun.SwiftC.App.FLAG_ENABLE_LIBRARY_EVOLUTION,
                Apple.XCRun.SwiftC.App.FLAG_PARSE_AS_LIBRARY,
                Apple.XCRun.SwiftC.App.ARGUMENT_MODULE_NAME, productName,
                Apple.XCRun.SwiftC.App.ARGUMENT_MODULE_LINK_NAME, productName,
                Apple.XCRun.SwiftC.App.FLAG_IMPORT_UNDERLYING_MODULE,
                // Apple.XCRun.SwiftC.App.FLAG_STATIC,
                // Apple.XCRun.SwiftC.App.FLAG_EMIT_LIBRARY,
                Apple.XCRun.SwiftC.App.FLAG_EMIT_OBJECT,
                Apple.XCRun.SwiftC.App.FLAG_EMIT_MODULE,
                Apple.XCRun.SwiftC.App.ARGUMENT_EMIT_MODULE_INTERFACE_PATH, moduleInterfaceOutputFilePath,
                Apple.XCRun.SwiftC.App.ARGUMENT_EMIT_MODULE_PATH, moduleOutputFilePath,
                Apple.XCRun.SwiftC.App.ARGUMENT_OUTPUT, objectOutputFilePath,
                Apple.XCRun.SwiftC.App.ARGUMENT_COMPILE, swiftFileName
            });
            
            Logger.LogDebug($"Compiling generated Swift code for \"{targetTriple}\"");

            string swiftStandardOutput = Apple.XCRun.SwiftC.App.Run(
                workingDirectory,
                args.ToArray()
            );

            string? swiftDocOutputFilePath = Path.Combine(outputPath, $"{targetDouble}.{Apple.XCRun.SwiftC.FileExtensions.SwiftDoc}");

            if (!File.Exists(swiftDocOutputFilePath)) {
                swiftDocOutputFilePath = null;
            }
            
            string? swiftABIOutputFilePath = Path.Combine(outputPath, $"{targetDouble}.{Apple.XCRun.SwiftC.FileExtensions.SwiftABI}");

            if (!File.Exists(swiftABIOutputFilePath)) {
                swiftABIOutputFilePath = null;
            }
            
            Logger.LogDebug($"Creating static library at \"{libraryOutputFilePath}\" using compiled Swift object file at \"{objectOutputFilePath}\"");
            
            string libToolStandardOutput = Apple.XCRun.Libtool.StaticMerge(
                workingDirectory,
                [ objectOutputFilePath ],
                libraryOutputFilePath,
                true // No warning for no symbols
            );
            
            // Extract symbols
            Logger.LogDebug($"Extracting symbols from \"{libraryOutputFilePath}\"");
            
            var symbols = Apple.Nm.App.GetRelevantSymbols(libraryOutputFilePath);
            var symbolsString = string.Join('\n', symbols);
            
            Logger.LogDebug($"Writing symbols file to \"{symbolsOutputFilePath}\"");
            File.WriteAllText(symbolsOutputFilePath, symbolsString);

            var result = new PartialCompileResult(
                swiftStandardOutput,
                libraryOutputFilePath,
                moduleInterfaceOutputFilePath,
                moduleOutputFilePath,
                symbolsOutputFilePath,
                swiftDocOutputFilePath,
                swiftABIOutputFilePath
            );

            return result;
        }
    }
}