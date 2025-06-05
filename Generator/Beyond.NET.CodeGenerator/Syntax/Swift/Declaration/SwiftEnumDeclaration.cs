using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftEnumDeclaration
{
    public string Name { get; }
    public string? RawTypeName { get; }
    public string? ProtocolConformance { get; }
    public SwiftVisibilities Visibility { get; }
    public string? Implementation { get; }

    public SwiftEnumDeclaration(
        string name,
        string? rawTypeName,
        string? protocolConformance,
        SwiftVisibilities visibility,
        string? implementation
    )
    {
        Name = name;
        RawTypeName = rawTypeName;
        ProtocolConformance = protocolConformance;
        Visibility = visibility;
        Implementation = implementation;
    }

    public override string ToString()
    {
        const string @enum = "enum";

        string visibilityString = Visibility.ToSwiftSyntaxString();

        string rawTypeOrProtocolConformanceDecl = !string.IsNullOrEmpty(RawTypeName)
            ? $": {RawTypeName}"
            : string.Empty;

        if (!string.IsNullOrEmpty(ProtocolConformance)) {
            rawTypeOrProtocolConformanceDecl += !string.IsNullOrEmpty(rawTypeOrProtocolConformanceDecl)
                ? $", {ProtocolConformance}"
                : $": {ProtocolConformance}";
        }

        string[] signatureComponents = new[] {
            visibilityString,
            @enum,
            $"{Name}{rawTypeOrProtocolConformanceDecl}"
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
