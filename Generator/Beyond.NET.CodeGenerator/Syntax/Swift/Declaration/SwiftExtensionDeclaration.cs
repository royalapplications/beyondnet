using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftExtensionDeclaration
{
    public string Name { get; }
    public string? ProtocolConformance { get; }
    public SwiftVisibilities Visibility { get; }
    public string? Implementation { get; }

    public SwiftExtensionDeclaration(
        string name,
        string? protocolConformance,
        SwiftVisibilities visibility,
        string? implementation
    )
    {
        Name = name;
        ProtocolConformance = protocolConformance;
        Visibility = visibility;
        Implementation = implementation;
    }

    public override string ToString()
    {
        const string extension = "extension";

        string visibilityString = Visibility.ToSwiftSyntaxString();

        string protocolConformanceDecl = !string.IsNullOrEmpty(ProtocolConformance)
            ? $": {ProtocolConformance}"
            : string.Empty;

        string[] signatureComponents = new[] {
            visibilityString,
            extension,
            $"{Name}{protocolConformanceDecl}"
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