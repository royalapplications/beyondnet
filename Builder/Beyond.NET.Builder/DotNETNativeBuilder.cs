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
        
        #region macOS
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.MacOS_ARM64);
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.MacOS_X64);
        #endregion macOS

        #region iOS
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_ARM64);
        #endregion iOS

        #region iOS Simulator
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_SIMULATOR_ARM64);
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_SIMULATOR_X64);
        #endregion iOS Simulator
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