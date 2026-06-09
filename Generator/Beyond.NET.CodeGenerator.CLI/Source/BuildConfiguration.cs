namespace Beyond.NET.CodeGenerator.CLI;

public record BuildConfiguration
(
    string? Target,
    string[]? Targets,

    string? ProductName,
    string? ProductBundleIdentifier,
    string? ProductOutputPath,

    string? MacOSDeploymentTarget,
    string? iOSDeploymentTarget,

    bool DisableParallelBuild,
    bool DisableStripDotNETSymbols,

    string[]? NoWarn
)
{
    /// <summary>
    /// Gets all build targets, supporting both single Target and multiple Targets properties.
    /// </summary>
    public string[] GetAllTargets()
    {
        var targets = new List<string>();

        if (!string.IsNullOrEmpty(Target))
        {
            targets.Add(Target);
        }

        if (Targets != null && Targets.Length > 0)
        {
            targets.AddRange(Targets);
        }

        return targets.Distinct().ToArray();
    }
};

internal static class BuildTargets
{
    public const string APPLE_UNIVERSAL = "apple-universal";
    public const string MACOS_UNIVERSAL = "macos-universal";
    public const string IOS_UNIVERSAL = "ios-universal";
    public const string ANDROID_ARM64 = "android-arm64";
}

internal static class AppleDeploymentTargets
{
    public const string MACOS_DEFAULT = "13.0";
    public const string IOS_DEFAULT = "16.0";
}
