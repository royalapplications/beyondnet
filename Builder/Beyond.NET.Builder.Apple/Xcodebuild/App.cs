using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.Xcodebuild;

public class App
{
    private const string XcodeBuildPath = "/usr/bin/xcodebuild";
    
    internal static readonly CLIApp XcodeBuildApp = new(XcodeBuildPath);
}