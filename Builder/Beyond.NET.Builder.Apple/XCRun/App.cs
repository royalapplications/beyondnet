using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.XCRun;

internal class App
{
    private static string XCRunPath => Which.GetAbsoluteCommandPath("xcrun");
    internal static CLIApp XCRunApp => new(XCRunPath);
}