using Beyond.NET.Core;

namespace Beyond.NET.Builder.DotNET;

internal class App
{
    private static string DotNETPath => Which.GetAbsoluteCommandPath("dotnet");
    internal static CLIApp DotNETApp => new(DotNETPath);
}