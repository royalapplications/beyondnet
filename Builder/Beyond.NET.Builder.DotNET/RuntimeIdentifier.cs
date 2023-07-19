namespace Beyond.NET.Builder.DotNET;

public static class RuntimeIdentifier
{
    public const string MacOS_ARM64 = $"{PlatformIdentifier.macOS}-{TargetIdentifier.ARM64}";
    public const string MacOS_X64 = $"{PlatformIdentifier.macOS}-{TargetIdentifier.x64}";
    
    /// <summary>
    /// Made up, not part of .NET!
    /// </summary>
    public const string MacOS_UNIVERSAL = $"{PlatformIdentifier.macOS}-{TargetIdentifier.UNIVERSAL}";
    
    public const string iOS_ARM64 = $"{PlatformIdentifier.iOS}-{TargetIdentifier.ARM64}";
    public const string iOS_SIMULATOR_ARM64 = $"{PlatformIdentifier.iOSSimulator}-{TargetIdentifier.ARM64}";
    public const string iOS_SIMULATOR_X64 = $"{PlatformIdentifier.iOSSimulator}-{TargetIdentifier.x64}";
    
    /// <summary>
    /// Made up, not part of .NET!
    /// </summary>
    public const string iOS_SIMULATOR_UNIVERSAL = $"{PlatformIdentifier.iOSSimulator}-{TargetIdentifier.UNIVERSAL}";
    
    /// <summary>
    /// Made up, not part of .NET!
    /// </summary>
    public const string iOS_UNIVERSAL = $"{PlatformIdentifier.iOS}-{TargetIdentifier.UNIVERSAL}";
    
    /// <summary>
    /// Made up, not part of .NET!
    /// </summary>
    public const string APPLE_UNIVERSAL = $"{PlatformIdentifier.Apple}-{TargetIdentifier.UNIVERSAL}";
}