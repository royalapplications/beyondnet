namespace NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

public enum SwiftVisibilities
{
    Private,
    Internal,
    Public,
    Open
}

public static class SwiftVisibilities_Extensions
{
    public static string ToSwiftSyntaxString(this SwiftVisibilities swiftVisibility)
    {
        switch (swiftVisibility) {
            case SwiftVisibilities.Private:
                return "private";
            case SwiftVisibilities.Internal:
                return "internal";
            case SwiftVisibilities.Public:
                return "public";
            case SwiftVisibilities.Open:
                return "open";
        }

        throw new Exception($"Unknown Swift Visibility: {swiftVisibility}");
    }
}