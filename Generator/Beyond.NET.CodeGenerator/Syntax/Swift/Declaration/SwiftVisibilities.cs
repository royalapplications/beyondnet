namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public enum SwiftVisibilities
{
    None,
    Private,
    FilePrivate,
    Internal,
    Public,
    Open
}

public static class SwiftVisibilities_Extensions
{
    public static string ToSwiftSyntaxString(this SwiftVisibilities visibility)
    {
        return visibility switch
        {
            SwiftVisibilities.None => string.Empty,
            SwiftVisibilities.Private => "private",
            SwiftVisibilities.FilePrivate => "fileprivate",
            SwiftVisibilities.Internal => "internal",
            SwiftVisibilities.Public => "public",
            SwiftVisibilities.Open => "open",
            _ => throw new Exception($"Unknown Swift Visibility: {visibility}")
        };
    }
}
