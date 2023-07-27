using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftGetOnlyPropertyDeclaration
{
    public string Name { get; }
    public SwiftVisibilities Visibility { get; }
    public SwiftTypeAttachmentKinds TypeAttachmentKind { get; }
    public bool IsOverride { get; }
    public bool Throws { get; }
    public string TypeName { get; }
    public string? Implementation { get; }
    
    public SwiftGetOnlyPropertyDeclaration(
        string name,
        SwiftVisibilities visibility,
        SwiftTypeAttachmentKinds typeAttachmentKind,
        bool isOverride,
        bool throws,
        string typeName,
        string? implementation
    )
    {
        Name = !string.IsNullOrEmpty(name)
            ? name 
            : throw new ArgumentOutOfRangeException(nameof(name));
        
        Visibility = visibility;
        TypeAttachmentKind = typeAttachmentKind;
        IsOverride = isOverride;
        Throws = throws;
        TypeName = typeName;
        Implementation = implementation;
    }
    
    public override string ToString()
    {
        const string var = "var";
        const string get = "get";
        string newLine = Environment.NewLine;
        
        string visibilityString = Visibility.ToSwiftSyntaxString();
        string typeAttachmentKindString = TypeAttachmentKind.ToSwiftSyntaxString();

        string overrideString = IsOverride
            ? "override"
            : string.Empty;

        string throwsString = Throws
            ? "throws"
            : string.Empty;

        string? implementation = Implementation;
        bool hasImplementation = !string.IsNullOrEmpty(implementation);

        if (hasImplementation) {
            implementation = implementation?.IndentAllLines(1);
        }

        const string openingBrace = "{";
        const string closingBrace = "}";
        
        string[] signatureComponents = new[] {
            visibilityString,
            overrideString,
            typeAttachmentKindString,
            var,
            $"{Name}: {TypeName}",
            openingBrace,
            get,
            throwsString,
            hasImplementation 
                ? openingBrace
                : closingBrace
        };

        string signature = SwiftFuncSignatureComponents.ComponentsToString(signatureComponents);
        string decl = signature;
        
        if (hasImplementation) {
            decl += $"{newLine}{implementation}{newLine}{closingBrace}{closingBrace}";
        }
        
        return decl;
    }
}