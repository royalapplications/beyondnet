namespace Beyond.NET.Builder.SwiftC;

public static class TargetIdentifier
{
    public const string ARM64 = "arm64";
    public const string x64 = "x86_64";
}

public static class PlatformIdentifier
{
    public const string macOS = "apple-macos";
    public const string iOS = "apple-ios";
    
    public const string iOSSimulatorSuffix = "-simulator";
}

public static class FileExtensions
{
    public const string SwiftModule = "swiftmodule";
    public const string SwiftInterface = "swiftinterface";
}