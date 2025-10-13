namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public enum SwiftTypeAttachmentKinds
{
    Instance,
    Static,
    Class
}

public static class SwiftTypeAttachmentKinds_Extensions
{
    public static string ToSwiftSyntaxString(this SwiftTypeAttachmentKinds swiftTypeAttachmentKind)
    {
        return swiftTypeAttachmentKind switch {
            SwiftTypeAttachmentKinds.Instance => string.Empty,
            SwiftTypeAttachmentKinds.Static => "static",
            SwiftTypeAttachmentKinds.Class => "class",
            _ => throw new Exception($"Unknown Swift Type Attachment Kind: {swiftTypeAttachmentKind}")
        };
    }
}
