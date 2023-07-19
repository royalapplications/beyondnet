using Beyond.NET.Builder.DotNET;

namespace Beyond.NET.Builder;

public class DotNETNativeBuilder
{
    public string TargetFramework { get; }
    public string ProductName { get; }
    public string TargetProjectFilePath { get; }
    public SwiftBuilder.BuildResult? SwiftBuildResult { get; }

    public DotNETNativeBuilder(
        string targetFramework,
        string productName,
        string targetProjectFilePath,
        SwiftBuilder.BuildResult? swiftBuildResult
    )
    {
        TargetFramework = targetFramework;
        ProductName = productName;
        TargetProjectFilePath = targetProjectFilePath;
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
        
        Console.WriteLine(nativeCsProjContents);
    }
}