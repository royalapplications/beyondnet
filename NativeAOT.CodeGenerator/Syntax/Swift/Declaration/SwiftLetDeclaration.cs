namespace NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftLetDeclaration
{
    public string Name { get; }
    public string? TypeName { get; } = null;
    public string? Value { get; } = null;
    public SwiftVisibilities Visibility { get; } = SwiftVisibilities.None;
    public SwiftTypeAttachmentKinds TypeAttachmentKind { get; } = SwiftTypeAttachmentKinds.Instance;

    public SwiftLetDeclaration(
        string name,
        string? typeName,
        string? value,
        SwiftVisibilities visibility,
        SwiftTypeAttachmentKinds typeAttachmentKind
    )
    {
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
        const string let = "let";
        
        string visibilityString = Visibility.ToSwiftSyntaxString();
        string typeAttachmentKindString = TypeAttachmentKind.ToSwiftSyntaxString();

        string nameAndTypeName = Name;

        if (!string.IsNullOrEmpty(TypeName)) {
            nameAndTypeName += $": {TypeName}";
        }

        string valueAssignment = !string.IsNullOrEmpty(Value)
            ? $"= {Value}"
            : string.Empty;
        
        string[] signatureComponents = new[] {
            visibilityString,
            typeAttachmentKindString,
            let,
            nameAndTypeName,
            valueAssignment
        };

        string decl = SwiftFuncSignatureComponents.ComponentsToString(signatureComponents);
        
        return decl;
    }
}