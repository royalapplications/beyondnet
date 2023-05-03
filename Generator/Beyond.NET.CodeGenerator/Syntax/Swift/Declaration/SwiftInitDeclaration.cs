using Beyond.NET.CodeGenerator.Extensions;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftInitDeclaration
{
    public bool IsConvenience { get; }
    public bool IsFailable { get; }
    public SwiftVisibilities Visibility { get; }
    public string Parameters { get; }
    public bool Throws { get; }
    public string? Implementation { get; }
    
    public SwiftInitDeclaration(
        bool isConvenience,
        bool isFailable,
        SwiftVisibilities visibility,
        string parameters,
        bool throws,
        string? implementation
    )
    {
        IsConvenience = isConvenience;
        IsFailable = isFailable;
        Visibility = visibility;
        Parameters = parameters;
        Throws = throws;
        
        Implementation = !string.IsNullOrEmpty(implementation)
            ? implementation
            : null;
    }
    
    public override string ToString()
    {
        string convenienceString = IsConvenience
            ? "convenience"
            : string.Empty;
        
        const string name = "init";
        
        string isFailableString = IsFailable
            ? "?"
            : string.Empty;
        
        string visibilityString = Visibility.ToSwiftSyntaxString();
        
        string throwsString = Throws
            ? "throws"
            : string.Empty;
        
        string[] signatureComponents = new[] {
            visibilityString,
            convenienceString,
            $"{name}{isFailableString}({Parameters})",
            throwsString
        };

        string signature = SwiftFuncSignatureComponents.ComponentsToString(signatureComponents);
        
        string fullInit;
        
        if (!string.IsNullOrEmpty(Implementation)) {
            string indentedImpl = Implementation.IndentAllLines(1);
            
            fullInit = $"{signature} {{\n{indentedImpl}\n}}";
        } else {
            fullInit = signature;
        }

        return fullInit;
    }
}