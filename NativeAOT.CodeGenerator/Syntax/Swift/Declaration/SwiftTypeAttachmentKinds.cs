namespace NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

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
        switch (swiftTypeAttachmentKind) {
            case SwiftTypeAttachmentKinds.Instance:
                return string.Empty;
            case SwiftTypeAttachmentKinds.Static:
                return "static";
            case SwiftTypeAttachmentKinds.Class:
                return "class";
        }

        throw new Exception($"Unknown Swift Type Attachment Kind: {swiftTypeAttachmentKind}");
    }
}