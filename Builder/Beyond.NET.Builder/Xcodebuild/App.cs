using Beyond.NET.Builder.Helpers;

namespace Beyond.NET.Builder.Xcodebuild;

public class App
{
    private const string XcodeBuildPath = "/usr/bin/xcodebuild";
    
    internal static readonly CLIApp XcodeBuildApp = new(XcodeBuildPath);
}