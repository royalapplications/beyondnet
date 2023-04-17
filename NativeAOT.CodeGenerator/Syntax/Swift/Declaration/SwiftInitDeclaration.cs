namespace NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftInitDeclaration
{
    public bool IsConvenience { get; }
    public bool IsFailable { get; }
    public SwiftVisibilities Visibility { get; }
    public string Parameters { get; }
    public bool Throws { get; }
    
    public SwiftInitDeclaration(
        bool isConvenience,
        bool isFailable,
        SwiftVisibilities visibility,
        string parameters,
        bool throws
    )
    {
        IsConvenience = isConvenience;
        IsFailable = isFailable;
        Visibility = visibility;
        Parameters = parameters;
        Throws = throws;
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

        return signature;
    }
}