using Beyond.NET.Builder.Helpers;

namespace Beyond.NET.Builder.XCRun;

internal class App
{
    private const string XCRunPath = "/usr/bin/xcrun";
    
    internal static readonly CLIApp XCRunApp = new(XCRunPath);
}