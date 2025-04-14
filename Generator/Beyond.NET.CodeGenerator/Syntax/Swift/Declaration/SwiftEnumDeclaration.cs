using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftEnumDeclaration
{
    public string Name { get; }
    public string? RawTypeName { get; }
    public SwiftVisibilities Visibility { get; }
    public string? Implementation { get; }

    public SwiftEnumDeclaration(
        string name,
        string? rawTypeName,
        SwiftVisibilities visibility,
        string? implementation
    )
    {
        Name = name;
        RawTypeName = rawTypeName;
        Visibility = visibility;
        Implementation = implementation;
    }

    public override string ToString()
    {
        const string @enum = "enum";

        string visibilityString = Visibility.ToSwiftSyntaxString();

        string rawTypeDecl = !string.IsNullOrEmpty(RawTypeName)
            ? $": {RawTypeName}"
            : string.Empty;

        string[] signatureComponents = new[] {
            visibilityString,
            @enum,
            $"{Name}{rawTypeDecl}"
        };

        string signature = SwiftFuncSignatureComponents.ComponentsToString(signatureComponents);

        string fullDecl;

        if (!string.IsNullOrEmpty(Implementation)) {
            string indentedImpl = Implementation.IndentAllLines(1);

            fullDecl = $"{signature} {{\n{indentedImpl}\n}}";
        } else {
            fullDecl = signature;
        }

        return fullDecl;
    }
}