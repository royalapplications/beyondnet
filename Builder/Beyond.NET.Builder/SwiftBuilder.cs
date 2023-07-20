using Beyond.NET.Core;

namespace Beyond.NET.Builder;

public class SwiftBuilder
{
    public record BuilderConfiguration(
        string ProductName,
        string GeneratedCHeaderFilePath,
        string GeneratedSwiftFilePath,
        string DeploymentTargetMacOS,
        string DeploymentTargetiOS
    );
    
    public record BuildResult(
        string OutputRootPath,
        
        string LibraryOutputPathFormat,
        string SymbolsOutputPathFormat,
        
        string OutputClangModuleMapFilePath,
        string macOSDeploymentTarget,
        string iOSDeploymentTarget,
        
        PartialCompileResult MacOSARM64Result,
        PartialCompileResult MacOSX64Result,
        PartialCompileResult iOSARM64Result,
        PartialCompileResult iOSSimulatorARM64Result,
        PartialCompileResult iOSSimulatorX64Result
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
    
    public BuilderConfiguration Configuration { get; }

    public SwiftBuilder(BuilderConfiguration configuration)
    {
        Configuration = configuration;
    }

    public BuildResult Build()
    {
        const string swiftVersion = "5";
        
        string productName = Configuration.ProductName;

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
        
        string tempDirectoryPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix).FullName;

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

        File.WriteAllText(moduleMapFilePath, moduleMapString);
        File.WriteAllText(clangModuleMapFilePath, clangModuleMapString);
        File.WriteAllText(headerMapFilePath, headerMapString);
        
        File.Copy(generatedCHeaderFilePath, Path.Combine(tempDirectoryPath, generatedCHeaderFileName));
        File.Copy(generatedSwiftFilePath, Path.Combine(tempDirectoryPath, generatedSwiftFileName));

        var sdkPathMacOS = Apple.XCRun.SDK.GetSDKPath(Apple.XCRun.SDK.macOSName);
        var sdkPathiOS = Apple.XCRun.SDK.GetSDKPath(Apple.XCRun.SDK.iOSName);
        var sdkPathiOSSimulator = Apple.XCRun.SDK.GetSDKPath(Apple.XCRun.SDK.iOSSimulatorName);

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

        string platformSuffixiOSSimulator = Apple.XCRun.SwiftC.PlatformIdentifier.iOSSimulatorSuffix;

        string outputPathRoot = Path.Combine(tempDirectoryPath, "bin");
        string outputPathApple = Path.Combine(outputPathRoot, "apple");

        string outputPathMacOSARM64 = Path.Combine(outputPathApple, $"{platformIdentifierMacOSDN}-{targetIdentifierARM64DN}");
        string outputPathMacOSX64 = Path.Combine(outputPathApple, $"{platformIdentifierMacOSDN}-{targetIdentifierX64DN}");

        string outputPathiOSARM64 = Path.Combine(outputPathApple, $"{platformIdentifieriOSDN}-{targetIdentifierARM64DN}");
        string outputPathiOSSimulatorARM64 = Path.Combine(outputPathApple, $"{platformIdentifieriOSSimulatorDN}-{targetIdentifierARM64DN}");
        string outputPathiOSSimulatorX64 = Path.Combine(outputPathApple, $"{platformIdentifieriOSSimulatorDN}-{targetIdentifierX64DN}");

        Directory.CreateDirectory(outputPathMacOSARM64);
        Directory.CreateDirectory(outputPathMacOSX64);

        Directory.CreateDirectory(outputPathiOSARM64);

        Directory.CreateDirectory(outputPathiOSSimulatorARM64);
        Directory.CreateDirectory(outputPathiOSSimulatorX64);

        SwiftCompiler compiler = new(
            tempDirectoryPath,
            swiftVersion,
            productName,
            generatedSwiftFileName,
            headerMapFileName
        );

        #region macOS
        var macOSARM64Result = compiler.Compile(
            sdkPathMacOS,
            targetIdentifierARM64,
            platformIdentifierMacOS,
            string.Empty,
            deploymentTargetMacOS,
            outputPathMacOSARM64
        );
        
        var macOSX64Result = compiler.Compile(
            sdkPathMacOS,
            targetIdentifierX64,
            platformIdentifierMacOS,
            string.Empty,
            deploymentTargetMacOS,
            outputPathMacOSX64
        );
        #endregion macOS

        #region iOS
        var iOSARM64Result = compiler.Compile(
            sdkPathiOS,
            targetIdentifierARM64,
            platformIdentifieriOS,
            string.Empty,
            deploymentTargetiOS,
            outputPathiOSARM64
        );
        #endregion iOS

        #region iOS Simulator
        var iOSSimulatorARM64Result = compiler.Compile(
            sdkPathiOSSimulator,
            targetIdentifierARM64,
            platformIdentifieriOS,
            platformSuffixiOSSimulator,
            deploymentTargetiOS,
            outputPathiOSSimulatorARM64
        );
        
        var iOSSimulatorX64Result = compiler.Compile(
            sdkPathiOSSimulator,
            targetIdentifierX64,
            platformIdentifieriOS,
            platformSuffixiOSSimulator,
            deploymentTargetiOS,
            outputPathiOSSimulatorX64
        );
        #endregion iOS Simulator
        
        string libraryOutputPathFormat = Path.Combine(
            outputPathApple,
            "{0}", // Runtime Identifier
            $"lib{productName}.a"
        );
        
        string symbolsOutputPathFormat = Path.Combine(
            outputPathApple,
            "{0}", // Runtime Identifier
            $"lib{productName}.export"
        );

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
        string SwiftFileName,
        string HeaderMapFileName
    )
    {
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
                SwiftFileName,
                HeaderMapFileName,
                outputPath
            );
        }

        private static PartialCompileResult Compile(
            string workingDirectory,
            string swiftVersion,
            string sdk,
            string targetIdentifier,
            string platformIdentifier,
            string platformSuffix,
            string deploymentTarget,
            string productName,
            string swiftFileName,
            string headerMapFileName,
            string outputPath
        )
        {
            string targetDouble = Apple.XCRun.SwiftC.TargetDouble.Make(
                targetIdentifier,
                platformIdentifier
            );

            string targetTriple = Apple.XCRun.SwiftC.TargetTriple.Make(
                targetIdentifier,
                platformIdentifier,
                deploymentTarget,
                platformSuffix
            );
            
            string outputProductName = $"lib{productName}";

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
                Apple.XCRun.SwiftC.App.ARGUMENT_SWIFT_VERSION, swiftVersion,
                Apple.XCRun.SwiftC.App.FLAG_DISABLE_CROSS_MODULE_OPTIMIZATION,
                Apple.XCRun.SwiftC.App.FLAG_ENABLE_TESTING,
                Apple.XCRun.SwiftC.App.FLAG_ENABLE_LIBRARY_EVOLUTION,
                Apple.XCRun.SwiftC.App.FLAG_PARSE_AS_LIBRARY,
                Apple.XCRun.SwiftC.App.ARGUMENT_MODULE_NAME, productName,
                Apple.XCRun.SwiftC.App.ARGUMENT_MODULE_LINK_NAME, productName,
                Apple.XCRun.SwiftC.App.FLAG_IMPORT_UNDERLYING_MODULE,
                Apple.XCRun.SwiftC.App.FLAG_STATIC,
                Apple.XCRun.SwiftC.App.FLAG_EMIT_LIBRARY,
                Apple.XCRun.SwiftC.App.FLAG_EMIT_MODULE,
                Apple.XCRun.SwiftC.App.ARGUMENT_EMIT_MODULE_INTERFACE_PATH, moduleInterfaceOutputFilePath,
                Apple.XCRun.SwiftC.App.ARGUMENT_EMIT_MODULE_PATH, moduleOutputFilePath,
                Apple.XCRun.SwiftC.App.ARGUMENT_OUTPUT, libraryOutputFilePath,
                Apple.XCRun.SwiftC.App.ARGUMENT_COMPILE, swiftFileName
            });

            string standardOutput = Apple.XCRun.SwiftC.App.Run(
                workingDirectory,
                args.ToArray()
            );

            var symbols = Apple.Nm.App.GetRelevantSymbols(libraryOutputFilePath);
            var symbolsString = string.Join('\n', symbols);
            
            File.WriteAllText(symbolsOutputFilePath, symbolsString);
 
            string? swiftDocOutputFilePath = Path.Combine(outputPath, $"{targetDouble}.{Apple.XCRun.SwiftC.FileExtensions.SwiftDoc}");

            if (!File.Exists(swiftDocOutputFilePath)) {
                swiftDocOutputFilePath = null;
            }
            
            string? swiftABIOutputFilePath = Path.Combine(outputPath, $"{targetDouble}.{Apple.XCRun.SwiftC.FileExtensions.SwiftABI}");

            if (!File.Exists(swiftABIOutputFilePath)) {
                swiftABIOutputFilePath = null;
            }

            var result = new PartialCompileResult(
                standardOutput,
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