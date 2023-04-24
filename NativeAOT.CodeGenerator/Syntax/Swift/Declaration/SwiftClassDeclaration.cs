using NativeAOT.CodeGenerator.Extensions;

namespace NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftClassDeclaration
{
    public string Name { get; }
    public string? BaseTypeName { get; }
    public SwiftVisibilities Visibility { get; }
    public string? Implementation { get; }

    public SwiftClassDeclaration(
        string name,
        string? baseTypeName,
        SwiftVisibilities visibility,
        string? implementation
    )
    {
        Name = name;
        BaseTypeName = baseTypeName;
        Visibility = visibility;
        Implementation = implementation;
    }

    public override string ToString()
    {
        const string @class = "class";
        
        string visibilityString = Visibility.ToSwiftSyntaxString();

        string baseTypeDecl = !string.IsNullOrEmpty(BaseTypeName)
            ? $": {BaseTypeName}"
            : string.Empty; 
        
        string[] signatureComponents = new[] {
            visibilityString,
            @class,
            $"{Name}{baseTypeDecl}"
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