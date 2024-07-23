namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinVariableDeclaration
{
    public KotlinVariableKinds VariableKind { get; }
    public string Name { get; }
    public string? TypeName { get; } = null;
    public string? Value { get; } = null;
    public KotlinVisibilities Visibility { get; } = KotlinVisibilities.None;

    public KotlinVariableDeclaration(
        KotlinVariableKinds variableKind,
        string name,
        string? typeName,
        string? value,
        KotlinVisibilities visibility
    )
    {
        VariableKind = variableKind;
        Name = name;
        TypeName = typeName;
        Value = value;
        Visibility = visibility;

        if (string.IsNullOrEmpty(Value) &&
            string.IsNullOrEmpty(TypeName)) {
            throw new Exception("Either Value or TypeName must be specified");
        }
    }

    public override string ToString()
    {
        string variableKindString;

        switch (VariableKind) {
            case KotlinVariableKinds.Constant:
                variableKindString = "val";
                break;
            case KotlinVariableKinds.Variable:
                variableKindString = "var";
                break;
            default:
                throw new Exception("Unknown Variable Kind");
        }
        
        string visibilityString = Visibility.ToKotlinSyntaxString();

        string nameAndTypeName = Name;

        if (!string.IsNullOrEmpty(TypeName)) {
            nameAndTypeName += $": {TypeName}";
        }

        string valueAssignment = !string.IsNullOrEmpty(Value)
            ? $"= {Value}"
            : string.Empty;
        
        string[] signatureComponents = [
            visibilityString,
            variableKindString,
            nameAndTypeName,
            valueAssignment
        ];

        string decl = KotlinFunSignatureComponents.ComponentsToString(signatureComponents);
        
        return decl;
    }
}