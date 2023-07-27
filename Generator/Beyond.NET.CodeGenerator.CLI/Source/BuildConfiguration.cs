namespace Beyond.NET.CodeGenerator.CLI;

public record BuildConfiguration
(
    string Target,
    
    string ProductName,
    string? ProductBundleIdentifier,
    string ProductOutputPath,
    
    string? MacOSDeploymentTarget,
    string? iOSDeploymentTarget
);

internal static class BuildTargets
{
    public const string APPLE_UNIVERSAL = "apple-universal";
}

internal static class AppleDeploymentTargets
{
    public const string MACOS_DEFAULT = "13.0";
    public const string IOS_DEFAULT = "16.0";
}