using Beyond.NET.Builder.DotNET;
using Beyond.NET.Core;

namespace Beyond.NET.Builder;

public class DotNETNativeBuilder
{
    public record BuildResult(
        string OutputDirectoryPath
    );
    
    public string TargetFramework { get; }
    public string ProductName { get; }
    public string TargetAssemblyFilePath { get; }
    public string GeneratedCSharpFilePath { get; }
    public SwiftBuilder.BuildResult? SwiftBuildResult { get; }

    private const string VERBOSITY_LEVEL = Publish.VERBOSITY_LEVEL_NORMAL;
    private const string BUILD_CONFIGURATION = "Release";
    
    private string OutputProductName => $"lib{ProductName}";
    private string OutputProductFileName => $"{OutputProductName}.dylib";
    private string UniversalOutputProductFileName => $"{ProductName}.xcframework";

    public DotNETNativeBuilder(
        string targetFramework,
        string productName,
        string targetAssemblyFilePath,
        string generatedCSharpFilePath,
        SwiftBuilder.BuildResult? swiftBuildResult
    )
    {
        TargetFramework = targetFramework;
        ProductName = productName;
        TargetAssemblyFilePath = targetAssemblyFilePath;
        GeneratedCSharpFilePath = generatedCSharpFilePath;
        SwiftBuildResult = swiftBuildResult;
    }

    public BuildResult Build()
    {
        #region Generate CSProj
        NativeCsProj.AppleSpecificSettings? appleSettings;

        var swiftBuildResult = SwiftBuildResult;

        if (swiftBuildResult is not null) {
            appleSettings = new(
                swiftBuildResult.macOSDeploymentTarget,
                swiftBuildResult.iOSDeploymentTarget,
                swiftBuildResult.LibraryOutputPathFormat,
                swiftBuildResult.SymbolsOutputPathFormat,
                swiftBuildResult.OutputClangModuleMapFilePath
            );
        } else {
            appleSettings = null;
        }

        var nativeCsProj = new NativeCsProj(
            TargetFramework,
            ProductName,
            TargetAssemblyFilePath,
            appleSettings
        );

        string nativeCsProjContents = nativeCsProj.GetCsProjContents();
        #endregion Generate CSProj

        #region Create Temp Dir
        string sanitizedProductName = ProductName.SanitizedProductNameForTempDirectory();
        string tempDirectoryPrefix = $"BeyondNETBuilderDotNETNativeBuilder_{sanitizedProductName}_";

        string tempDirectoryPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix).FullName;
        #endregion Create Temp Dir

        #region Copy Material to Temp Dir
        string nativeCsProjFileName = $"{ProductName}.csproj";
        string nativeCsProjFilePath = Path.Combine(tempDirectoryPath, nativeCsProjFileName);

        File.WriteAllText(nativeCsProjFilePath, nativeCsProjContents);

        string generatedCSharpFilePath = GeneratedCSharpFilePath;
        string generatedCSharpFileName = Path.GetFileName(generatedCSharpFilePath);

        File.Copy(GeneratedCSharpFilePath, Path.Combine(tempDirectoryPath, generatedCSharpFileName));
        #endregion Copy Material to Temp Dir
        
        if (swiftBuildResult is not null) {
            #region Build for Apple Universal
            #region Prepare File Paths
            const string binDirName = "bin";
            const string publishDirName = "publish";

            string macOSARM64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.MacOS_ARM64}/{publishDirName}";
            string macOSX64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.MacOS_X64}/{publishDirName}";
            string macOSUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.MacOS_UNIVERSAL}/{publishDirName}";

            string iOSARM64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_ARM64}/{publishDirName}";
            string iOSSimulatorARM64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_SIMULATOR_ARM64}/{publishDirName}";
            string iOSSimulatorX64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_SIMULATOR_X64}/{publishDirName}";
            string iOSSimulatorUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_SIMULATOR_UNIVERSAL}/{publishDirName}";
            string iOSUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_UNIVERSAL}/{publishDirName}";

            string appleUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.APPLE_UNIVERSAL}/{publishDirName}";

            string iOSARM64BuildPath = Path.Combine(tempDirectoryPath, iOSARM64BuildDir);
            string iOSARM64FilePath = Path.Combine(iOSARM64BuildPath, OutputProductFileName);

            string iOSSimulatorARM64BuildPath = Path.Combine(tempDirectoryPath, iOSSimulatorARM64BuildDir);
            string iOSSimulatorARM64FilePath = Path.Combine(iOSSimulatorARM64BuildPath, OutputProductFileName);

            string iOSSimulatorX64BuildPath = Path.Combine(tempDirectoryPath, iOSSimulatorX64BuildDir);
            string iOSSimulatorX64FilePath = Path.Combine(iOSSimulatorX64BuildPath, OutputProductFileName);

            string iOSSimulatorUniversalBuildPath = Path.Combine(tempDirectoryPath, iOSSimulatorUniversalBuildDir);
            string iOSSimulatorUniversalFilePath = Path.Combine(iOSSimulatorUniversalBuildPath, OutputProductFileName);

            string iOSUniversalBuildPath = Path.Combine(tempDirectoryPath, iOSUniversalBuildDir);
            string iOSUniversalXCFrameworkFilePath = Path.Combine(iOSUniversalBuildPath, UniversalOutputProductFileName);

            string macOSARM64BuildPath = Path.Combine(tempDirectoryPath, macOSARM64BuildDir);
            string macOSARM64FilePath = Path.Combine(macOSARM64BuildPath, OutputProductFileName);

            string macOSX64BuildPath = Path.Combine(tempDirectoryPath, macOSX64BuildDir);
            string macOSX64FilePath = Path.Combine(macOSX64BuildPath, OutputProductFileName);

            string macOSUniversalBuildPath = Path.Combine(tempDirectoryPath, macOSUniversalBuildDir);
            string macOSUniversalFilePath = Path.Combine(macOSUniversalBuildPath, OutputProductFileName);

            string appleUniversalBuildPath = Path.Combine(tempDirectoryPath, appleUniversalBuildDir);
            string appleUniversalXCFrameworkFilePath = Path.Combine(appleUniversalBuildPath, UniversalOutputProductFileName);

            string libraryId = $"@rpath/{OutputProductFileName}";
            #endregion Prepare File Paths

            #region dotnet publish
            #region macOS
            DotNETPublish(tempDirectoryPath, RuntimeIdentifier.MacOS_ARM64);
            DotNETPublish(tempDirectoryPath, RuntimeIdentifier.MacOS_X64);

            Directory.CreateDirectory(macOSUniversalBuildPath);

            Apple.Lipo.App.Create(
                new[] {
                    macOSARM64FilePath,
                    macOSX64FilePath
                },
                macOSUniversalFilePath
            );

            Apple.InstallNameTool.App.ChangeId(macOSUniversalFilePath, libraryId);
            #endregion macOS
            
            #region iOS
            DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_ARM64);
            Apple.InstallNameTool.App.ChangeId(iOSARM64FilePath, libraryId);

            DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_SIMULATOR_ARM64);
            Apple.InstallNameTool.App.ChangeId(iOSSimulatorARM64FilePath, libraryId);

            DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_SIMULATOR_X64);
            Apple.InstallNameTool.App.ChangeId(iOSSimulatorX64FilePath, libraryId);

            Directory.CreateDirectory(iOSSimulatorUniversalBuildPath);

            Apple.Lipo.App.Create(
                new[] {
                    iOSSimulatorARM64FilePath,
                    iOSSimulatorX64FilePath
                },
                iOSSimulatorUniversalFilePath
            );

            Apple.InstallNameTool.App.ChangeId(iOSSimulatorUniversalFilePath, libraryId);

            Directory.CreateDirectory(iOSUniversalBuildPath);

            Apple.Xcodebuild.CreateXCFramework.Run(
                new[] {
                    iOSSimulatorUniversalFilePath,
                    iOSARM64FilePath
                },
                iOSUniversalXCFrameworkFilePath
            );
            #endregion iOS

            #region Apple Universal
            Directory.CreateDirectory(appleUniversalBuildPath);

            Apple.Xcodebuild.CreateXCFramework.Run(
                new[] {
                    macOSUniversalFilePath,
                    iOSSimulatorUniversalFilePath,
                    iOSARM64FilePath
                },
                appleUniversalXCFrameworkFilePath
            );
            #endregion Apple Universal
            #endregion dotnet publish

            #region Copy Swift module into Apple Universal XCFramework
            #region macOS
            var macOSUniversalSwiftModuleDirPath = $"{appleUniversalXCFrameworkFilePath}/macos-arm64_x86_64/{ProductName}.{Apple.XCRun.SwiftC.FileExtensions.SwiftModule}";
            
            CreateSwiftModule(
                swiftBuildResult,
                swiftBuildResult.MacOSARM64Result,
                macOSUniversalSwiftModuleDirPath
            );
            
            CreateSwiftModule(
                swiftBuildResult,
                swiftBuildResult.MacOSX64Result,
                macOSUniversalSwiftModuleDirPath
            );
            #endregion macOS

            #region iOS
            var iOSARM64SwiftModuleDirPath = $"{appleUniversalXCFrameworkFilePath}/ios-arm64/{ProductName}.{Apple.XCRun.SwiftC.FileExtensions.SwiftModule}";

            CreateSwiftModule(
                swiftBuildResult,
                swiftBuildResult.iOSARM64Result,
                iOSARM64SwiftModuleDirPath
            );

            var iOSSimulatorUniversalSwiftModuleDirPath = $"{appleUniversalXCFrameworkFilePath}/ios-arm64_x86_64-simulator/{ProductName}.{Apple.XCRun.SwiftC.FileExtensions.SwiftModule}";
            
            CreateSwiftModule(
                swiftBuildResult,
                swiftBuildResult.iOSSimulatorARM64Result,
                iOSSimulatorUniversalSwiftModuleDirPath
            );
            
            CreateSwiftModule(
                swiftBuildResult,
                swiftBuildResult.iOSSimulatorX64Result,
                iOSSimulatorUniversalSwiftModuleDirPath
            );
            #endregion iOS
            #endregion Copy Swift module into Apple Universal XCFramework

            return new(
                appleUniversalBuildPath
            );
            #endregion Build for Apple Universal
        } else {
            // TODO: Currently only apple universal builds are supported
            throw new Exception("No swift build result");
        }
    }

    private void DotNETPublish(
        string workingDirectory,
        string runtimeIdentifier
    )
    {
        Publish.Run(
            workingDirectory,
            runtimeIdentifier,
            VERBOSITY_LEVEL,
            null
        );
    }

    private void CreateSwiftModule(
        SwiftBuilder.BuildResult buildResult,
        SwiftBuilder.PartialCompileResult partialCompileResult,
        string targetSwiftModuleDirPath
    )
    {
        if (!Directory.Exists(targetSwiftModuleDirPath)) {
            Directory.CreateDirectory(targetSwiftModuleDirPath);
        }

        string targetModuleMapFilePath = Path.Combine(targetSwiftModuleDirPath, "module.modulemap");

        if (!File.Exists(targetModuleMapFilePath)) {
            // Copy .modulemap
            File.Copy(
                buildResult.OutputClangModuleMapFilePath,
                targetModuleMapFilePath
            );
        }
            
        // Copy .swiftinterface
        File.Copy(
            partialCompileResult.ModuleInterfaceOutputFilePath,
            Path.Combine(targetSwiftModuleDirPath, Path.GetFileName(partialCompileResult.ModuleInterfaceOutputFilePath))
        );
            
        // Copy .swiftmodule
        File.Copy(
            partialCompileResult.ModuleOutputFilePath,
            Path.Combine(targetSwiftModuleDirPath, Path.GetFileName(partialCompileResult.ModuleOutputFilePath))
        );
            
        // Copy .swiftdoc
        if (!string.IsNullOrEmpty(partialCompileResult.SwiftDocOutputFilePath)) {
            File.Copy(
                partialCompileResult.SwiftDocOutputFilePath,
                Path.Combine(targetSwiftModuleDirPath, Path.GetFileName(partialCompileResult.SwiftDocOutputFilePath))
            );   
        }

        // Copy .abi.json
        if (!string.IsNullOrEmpty(partialCompileResult.SwiftABIOutputFilePath)) {
            File.Copy(
                partialCompileResult.SwiftABIOutputFilePath,
                Path.Combine(targetSwiftModuleDirPath, Path.GetFileName(partialCompileResult.SwiftABIOutputFilePath))
            );   
        }
    }
}