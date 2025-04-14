namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftTypeAliasDeclaration
{
    public string AliasTypeName { get; }
    public SwiftVisibilities Visibility { get; }
    public string OriginalTypeName { get; }

    public SwiftTypeAliasDeclaration(
        string aliasTypeName,
        SwiftVisibilities visibility,
        string originalTypeName
    )
    {
        AliasTypeName = aliasTypeName;
        Visibility = visibility;
        OriginalTypeName = originalTypeName;
    }

    public override string ToString()
    {
        const string typealias = "typealias";
        const string equals = "=";

        string visibilityString = Visibility.ToSwiftSyntaxString();

        string[] signatureComponents = new[] {
            visibilityString,
            typealias,
            AliasTypeName,
            equals,
            OriginalTypeName
        };

        string signature = SwiftFuncSignatureComponents.ComponentsToString(signatureComponents);

        return signature;
    }
}