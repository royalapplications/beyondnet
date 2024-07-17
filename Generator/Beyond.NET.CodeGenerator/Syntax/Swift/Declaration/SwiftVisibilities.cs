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
    public static string ToSwiftSyntaxString(this SwiftVisibilities visibilty)
    {
        switch (visibilty) {
            case SwiftVisibilities.None:
                return string.Empty;
            case SwiftVisibilities.Private:
                return "private";
            case SwiftVisibilities.FilePrivate:
                return "fileprivate";
            case SwiftVisibilities.Internal:
                return "internal";
            case SwiftVisibilities.Public:
                return "public";
            case SwiftVisibilities.Open:
                return "open";
        }

        throw new Exception($"Unknown Swift Visibility: {visibilty}");
    }
}