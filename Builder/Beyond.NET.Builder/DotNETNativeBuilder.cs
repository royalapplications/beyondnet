using Beyond.NET.Builder.Apple.Framework;
using Beyond.NET.Builder.DotNET;
using Beyond.NET.Core;

namespace Beyond.NET.Builder;

public class DotNETNativeBuilder
{
    public record BuildResult(
        string TemporaryDirectoryPath,
        string OutputDirectoryPath
    );
    
    public string TargetFramework { get; }
    public string ProductName { get; }
    public string ProductBundleIdentifier { get; }
    public string TargetAssemblyFilePath { get; }
    public string[] AssemblyReferences { get; }
    public string GeneratedCSharpFilePath { get; }
    public SwiftBuilder.BuildResult? SwiftBuildResult { get; }

    private const string VERBOSITY_LEVEL = Publish.VERBOSITY_LEVEL_NORMAL;
    private const string BUILD_CONFIGURATION = "Release";
    
    private string OutputProductName => ProductName;
    private string OutputProductFileName => $"{OutputProductName}.dylib";
    private string UniversalOutputProductFileName => $"{ProductName}.xcframework";

    public DotNETNativeBuilder(
        string targetFramework,
        string productName,
        string productBundleIdentifier,
        string targetAssemblyFilePath,
        string[] assemblyReferences,
        string generatedCSharpFilePath,
        SwiftBuilder.BuildResult? swiftBuildResult
    )
    {
        TargetFramework = targetFramework;
        ProductName = productName;
        ProductBundleIdentifier = productBundleIdentifier;
        TargetAssemblyFilePath = targetAssemblyFilePath;
        AssemblyReferences = assemblyReferences;
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
            AssemblyReferences,
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

            string appleUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.APPLE_UNIVERSAL}/{publishDirName}";

            string iOSARM64BuildPath = Path.Combine(tempDirectoryPath, iOSARM64BuildDir);
            string iOSARM64FilePath = Path.Combine(iOSARM64BuildPath, OutputProductFileName);

            string iOSSimulatorARM64BuildPath = Path.Combine(tempDirectoryPath, iOSSimulatorARM64BuildDir);
            string iOSSimulatorARM64FilePath = Path.Combine(iOSSimulatorARM64BuildPath, OutputProductFileName);

            string iOSSimulatorX64BuildPath = Path.Combine(tempDirectoryPath, iOSSimulatorX64BuildDir);
            string iOSSimulatorX64FilePath = Path.Combine(iOSSimulatorX64BuildPath, OutputProductFileName);

            string iOSSimulatorUniversalBuildPath = Path.Combine(tempDirectoryPath, iOSSimulatorUniversalBuildDir);
            string iOSSimulatorUniversalFilePath = Path.Combine(iOSSimulatorUniversalBuildPath, OutputProductFileName);

            string macOSARM64BuildPath = Path.Combine(tempDirectoryPath, macOSARM64BuildDir);
            string macOSARM64FilePath = Path.Combine(macOSARM64BuildPath, OutputProductFileName);

            string macOSX64BuildPath = Path.Combine(tempDirectoryPath, macOSX64BuildDir);
            string macOSX64FilePath = Path.Combine(macOSX64BuildPath, OutputProductFileName);

            string macOSUniversalBuildPath = Path.Combine(tempDirectoryPath, macOSUniversalBuildDir);
            string macOSUniversalFilePath = Path.Combine(macOSUniversalBuildPath, OutputProductFileName);

            string appleUniversalBuildPath = Path.Combine(tempDirectoryPath, appleUniversalBuildDir);
            string appleUniversalXCFrameworkFilePath = Path.Combine(appleUniversalBuildPath, UniversalOutputProductFileName);

            string newLibraryId = $"@rpath/{OutputProductFileName}";
            string swiftModuleDirName = $"module.{Apple.XCRun.SwiftC.FileExtensions.SwiftModule}";
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

            Apple.InstallNameTool.App.ChangeId(macOSUniversalFilePath, newLibraryId);
            #endregion macOS
            
            #region iOS
            DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_ARM64);
            Apple.InstallNameTool.App.ChangeId(iOSARM64FilePath, newLibraryId);

            DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_SIMULATOR_ARM64);
            Apple.InstallNameTool.App.ChangeId(iOSSimulatorARM64FilePath, newLibraryId);

            DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_SIMULATOR_X64);
            Apple.InstallNameTool.App.ChangeId(iOSSimulatorX64FilePath, newLibraryId);

            Directory.CreateDirectory(iOSSimulatorUniversalBuildPath);

            Apple.Lipo.App.Create(
                new[] {
                    iOSSimulatorARM64FilePath,
                    iOSSimulatorX64FilePath
                },
                iOSSimulatorUniversalFilePath
            );

            Apple.InstallNameTool.App.ChangeId(iOSSimulatorUniversalFilePath, newLibraryId);
            #endregion iOS
            #endregion dotnet publish
            
            #region Create Frameworks for Platforms
            #region macOS
            var macOSUniversalSwiftModuleDirPath = $"{macOSUniversalBuildPath}/{swiftModuleDirName}";
            
            CreateSwiftModule(
                swiftBuildResult.MacOSARM64Result,
                macOSUniversalSwiftModuleDirPath
            );
            
            CreateSwiftModule(
                swiftBuildResult.MacOSX64Result,
                macOSUniversalSwiftModuleDirPath
            );
            
            var macOSUniversalFrameworkBuilder = new FrameworkBuilder(
                ProductName,
                ProductBundleIdentifier,
                macOSUniversalFilePath,
                macOSUniversalBuildPath,
                true,
                swiftBuildResult.OutputClangModuleMapFilePath,
                macOSUniversalSwiftModuleDirPath
            );

            var macOSUniversalFrameworkBundlePath = macOSUniversalFrameworkBuilder.Build()
                .FrameworkOutputDirectoryPath;
            #endregion macOS

            #region iOS
            var iOSARM64SwiftModuleDirPath = $"{iOSARM64BuildPath}/{swiftModuleDirName}";

            CreateSwiftModule(
                swiftBuildResult.iOSARM64Result,
                iOSARM64SwiftModuleDirPath
            );
            
            var iOSARM64FrameworkBuilder = new FrameworkBuilder(
                ProductName,
                ProductBundleIdentifier,
                iOSARM64FilePath,
                iOSARM64BuildPath,
                false,
                swiftBuildResult.OutputClangModuleMapFilePath,
                iOSARM64SwiftModuleDirPath
            );

            var iOSARM64FrameworkBundlePath = iOSARM64FrameworkBuilder.Build()
                .FrameworkOutputDirectoryPath;

            var iOSSimulatorUniversalSwiftModuleDirPath = $"{iOSSimulatorUniversalBuildPath}/{swiftModuleDirName}";
            
            CreateSwiftModule(
                swiftBuildResult.iOSSimulatorARM64Result,
                iOSSimulatorUniversalSwiftModuleDirPath
            );
            
            CreateSwiftModule(
                swiftBuildResult.iOSSimulatorX64Result,
                iOSSimulatorUniversalSwiftModuleDirPath
            );
            
            var iOSSimulatorUniversalFrameworkBuilder = new FrameworkBuilder(
                ProductName,
                ProductBundleIdentifier,
                iOSSimulatorUniversalFilePath,
                iOSSimulatorUniversalBuildPath,
                false,
                swiftBuildResult.OutputClangModuleMapFilePath,
                iOSSimulatorUniversalSwiftModuleDirPath
            );

            var iOSSimulatorUniversalFrameworkBundlePath = iOSSimulatorUniversalFrameworkBuilder.Build()
                .FrameworkOutputDirectoryPath;
            #endregion iOS
            #endregion Create Frameworks for Platforms
            
            #region Apple Universal
            Directory.CreateDirectory(appleUniversalBuildPath);

            Apple.Xcodebuild.CreateXCFramework.Run(
                new[] {
                    macOSUniversalFrameworkBundlePath,
                    iOSSimulatorUniversalFrameworkBundlePath,
                    iOSARM64FrameworkBundlePath
                },
                null,
                appleUniversalXCFrameworkFilePath
            );
            #endregion Apple Universal

            return new(
                tempDirectoryPath,
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
        SwiftBuilder.PartialCompileResult partialCompileResult,
        string targetSwiftModuleDirPath
    )
    {
        if (!Directory.Exists(targetSwiftModuleDirPath)) {
            Directory.CreateDirectory(targetSwiftModuleDirPath);
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