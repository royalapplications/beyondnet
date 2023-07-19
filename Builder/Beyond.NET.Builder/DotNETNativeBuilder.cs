using Beyond.NET.Builder.DotNET;
using Beyond.NET.Core;

namespace Beyond.NET.Builder;

public class DotNETNativeBuilder
{
    public string TargetFramework { get; }
    public string ProductName { get; }
    public string TargetProjectFilePath { get; }
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
        string targetProjectFilePath,
        string generatedCSharpFilePath,
        SwiftBuilder.BuildResult? swiftBuildResult
    )
    {
        TargetFramework = targetFramework;
        ProductName = productName;
        TargetProjectFilePath = targetProjectFilePath;
        GeneratedCSharpFilePath = generatedCSharpFilePath;
        SwiftBuildResult = swiftBuildResult;
    }

    public void Build()
    {
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
            TargetProjectFilePath,
            appleSettings
        );

        string nativeCsProjContents = nativeCsProj.GetCsProjContents();
        
        string sanitizedProductName = ProductName.SanitizedProductNameForTempDirectory();
        string tempDirectoryPrefix = $"BeyondNETBuilderDotNETNativeBuilder_{sanitizedProductName}_";
        
        string tempDirectoryPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix).FullName;

        string nativeCsProjFileName = $"{ProductName}.csproj";
        string nativeCsProjFilePath = Path.Combine(tempDirectoryPath, nativeCsProjFileName);
        
        File.WriteAllText(nativeCsProjFilePath, nativeCsProjContents);

        string generatedCSharpFilePath = GeneratedCSharpFilePath;
        string generatedCSharpFileName = Path.GetFileName(generatedCSharpFilePath);
        
        File.Copy(GeneratedCSharpFilePath, Path.Combine(tempDirectoryPath, generatedCSharpFileName));

        #region Prepare File Paths
        const string binDirName = "bin";
        const string publishDirName = "publish";
        
        // TODO: Check if actually compiling for Apple platforms

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
        string iOSUniversalFilePath = Path.Combine(iOSUniversalBuildPath, OutputProductFileName);

        string macOSARM64BuildPath = Path.Combine(tempDirectoryPath, macOSARM64BuildDir);
        string macOSARM64FilePath = Path.Combine(macOSARM64BuildPath, OutputProductFileName);

        string macOSX64BuildPath = Path.Combine(tempDirectoryPath, macOSX64BuildDir);
        string macOSX64FilePath = Path.Combine(macOSX64BuildPath, OutputProductFileName);

        string macOSUniversalBuildPath = Path.Combine(tempDirectoryPath, macOSUniversalBuildDir);
        string macOSUniversalFilePath = Path.Combine(macOSUniversalBuildPath, OutputProductFileName);

        string appleUniversalBuildPath = Path.Combine(tempDirectoryPath, appleUniversalBuildDir);
        string appleUniversalFilePath = Path.Combine(appleUniversalBuildPath, UniversalOutputProductFileName);

        string libraryId = $"@rpath/{OutputProductFileName}";
        #endregion Prepare File Paths
        
        #region macOS
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.MacOS_ARM64);
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.MacOS_X64);

        Directory.CreateDirectory(macOSUniversalBuildPath);
        
        Apple.Lipo.App.Create(
            new [] {
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
            new [] {
                iOSSimulatorARM64FilePath,
                iOSSimulatorX64FilePath
            },
            iOSSimulatorUniversalFilePath
        );
        
        Apple.InstallNameTool.App.ChangeId(iOSSimulatorUniversalFilePath, libraryId);
        
        Directory.CreateDirectory(iOSUniversalBuildPath);
        
        Apple.Xcodebuild.CreateXCFramework.Run(
            new [] {
                iOSSimulatorUniversalFilePath,
                iOSARM64FilePath
            },
            iOSUniversalFilePath
        );
        #endregion iOS

        #region Apple Universal
        Directory.CreateDirectory(appleUniversalBuildPath);

        Apple.Xcodebuild.CreateXCFramework.Run(
            new [] {
                macOSUniversalFilePath,
                iOSSimulatorUniversalFilePath,
                iOSARM64FilePath
            },
            appleUniversalFilePath
        );
        #endregion Apple Universal
        
        Console.WriteLine("TODO: Done");
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
}