using Beyond.NET.Builder.DotNET;

namespace Beyond.NET.Builder.Android;

public static class AndroidPublish
{
    public static string Run(
        string workingDirectory,
        string runtimeIdentifier,
        string configuration,
        string? verbosityLevel
    )
    {
        using var _ = AndroidNdkEnvironment.PrependBinPathToPath();

        return Publish.Run(
            workingDirectory,
            runtimeIdentifier,
            verbosityLevel,
            new[] {
                "-c",
                configuration,
                "-p:PublishAotUsingRuntimePack=true"
            }
        );
    }
}
