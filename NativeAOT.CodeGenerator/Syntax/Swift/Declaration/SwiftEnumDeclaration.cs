using NativeAOT.CodeGenerator.Extensions;

namespace NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftEnumDeclaration
{
    public string Name { get; }
    public string? UnderlyingTypeName { get; }
    public SwiftVisibilities Visibility { get; }
    public string? Implementation { get; }

    public SwiftEnumDeclaration(
        string name,
        string? underlyingTypeName,
        SwiftVisibilities visibility,
        string? implementation
    )
    {
        Name = name;
        UnderlyingTypeName = underlyingTypeName;
        Visibility = visibility;
        Implementation = implementation;
    }

    public override string ToString()
    {
        const string @enum = "enum";
        
        string visibilityString = Visibility.ToSwiftSyntaxString();

        string underlyingTypeDecl = !string.IsNullOrEmpty(UnderlyingTypeName)
            ? $": {UnderlyingTypeName}"
            : string.Empty; 
        
        string[] signatureComponents = new[] {
            visibilityString,
            @enum,
            $"{Name}{underlyingTypeDecl}"
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