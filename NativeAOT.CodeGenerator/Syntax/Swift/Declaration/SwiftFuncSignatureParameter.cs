namespace NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftFuncSignatureParameter
{
    public string Label { get; }
    public string? Name { get; }
    public string TypeName { get; }
    
    public SwiftFuncSignatureParameter(
        string label,
        string? name,
        string typeName
    )
    {
        Label = label;
        Name = name;
        TypeName = typeName;
    }
    
    public SwiftFuncSignatureParameter(
        string label,
        string typeName
    )
    {
        Label = label;
        Name = null;
        TypeName = typeName;
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(TypeName)) {
            throw new ArgumentOutOfRangeException(nameof(TypeName));
        }
        
        string label = !string.IsNullOrEmpty(Label)
            ? Label
            : "_";

        string name = Name ?? string.Empty;

        bool labelAndNameAreEqual = label == name;

        if (labelAndNameAreEqual &&
            label == "_") {
            throw new Exception("Either Label or Name or both must be supplied");
        }
        
        string labelAndName;

        if (!string.IsNullOrEmpty(name) &&
            !labelAndNameAreEqual) {
            labelAndName = $"{label} {name}";
        } else {
            labelAndName = label;
        }

        string labelAndNameAndColon = $"{labelAndName}:";
        
        string parameter = SwiftFuncSignatureComponents.ComponentsToString(new[] {
            labelAndNameAndColon,
            TypeName
        });

        return parameter;
    }
}