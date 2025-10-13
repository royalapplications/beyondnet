namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftVariableDeclaration
{
    public SwiftVariableKinds VariableKind { get; }
    public string Name { get; }
    public string? TypeName { get; } = null;
    public string? Value { get; } = null;
    public SwiftVisibilities Visibility { get; } = SwiftVisibilities.None;
    public SwiftTypeAttachmentKinds TypeAttachmentKind { get; } = SwiftTypeAttachmentKinds.Instance;

    public SwiftVariableDeclaration(
        SwiftVariableKinds variableKind,
        string name,
        string? typeName,
        string? value,
        SwiftVisibilities visibility,
        SwiftTypeAttachmentKinds typeAttachmentKind
    )
    {
        VariableKind = variableKind;
        Name = name;
        TypeName = typeName;
        Value = value;
        Visibility = visibility;
        TypeAttachmentKind = typeAttachmentKind;

        if (string.IsNullOrEmpty(Value) &&
            string.IsNullOrEmpty(TypeName)) {
            throw new Exception("Either Value or TypeName must be specified");
        }
    }

    public override string ToString()
    {
        string variableKindString = VariableKind switch {
            SwiftVariableKinds.Constant => "let",
            SwiftVariableKinds.Variable => "var",
            _ => throw new Exception("Unknown Variable Kind")
        };

        string visibilityString = Visibility.ToSwiftSyntaxString();
        string typeAttachmentKindString = TypeAttachmentKind.ToSwiftSyntaxString();

        string nameAndTypeName = Name;

        if (!string.IsNullOrEmpty(TypeName)) {
            nameAndTypeName += $": {TypeName}";
        }

        string valueAssignment = !string.IsNullOrEmpty(Value)
            ? $"= {Value}"
            : string.Empty;

        string[] signatureComponents = [
            visibilityString,
            typeAttachmentKindString,
            variableKindString,
            nameAndTypeName,
            valueAssignment
        ];

        string decl = SwiftFuncSignatureComponents.ComponentsToString(signatureComponents);

        return decl;
    }
}
