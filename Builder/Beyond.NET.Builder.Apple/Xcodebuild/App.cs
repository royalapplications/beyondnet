using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.Xcodebuild;

public class App
{
    private static string XcodeBuildPath => Which.GetAbsoluteCommandPath("xcodebuild");
    internal static CLIApp XcodeBuildApp => new(XcodeBuildPath);
}