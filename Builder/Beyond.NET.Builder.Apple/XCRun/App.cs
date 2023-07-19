using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.XCRun;

internal class App
{
    private const string XCRunPath = "/usr/bin/xcrun";
    
    internal static readonly CLIApp XCRunApp = new(XCRunPath);
}