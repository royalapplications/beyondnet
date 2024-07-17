using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinFunDeclaration
{
    public string Name { get; }
    public KotlinVisibilities Visibility { get; }
    public bool IsExternal { get; }
    public bool IsOverride { get; }
    public string Parameters { get; }
    public string? ReturnTypeName { get; }
    public string? Implementation { get; }
    
    public KotlinFunDeclaration(
        string name,
        KotlinVisibilities visibility,
        bool isExternal,
        bool isOverride,
        string parameters,
        string? returnTypeName,
        string? implementation
    )
    {
        Name = !string.IsNullOrEmpty(name)
            ? name 
            : throw new ArgumentOutOfRangeException(nameof(name));
        
        Visibility = visibility;
        IsExternal = isExternal;
        IsOverride = isOverride;
        Parameters = parameters;

        ReturnTypeName = !string.IsNullOrEmpty(returnTypeName)
            ? returnTypeName
            : null;
        
        Implementation = !string.IsNullOrEmpty(implementation)
            ? implementation
            : null;
    }

    public override string ToString()
    {
        const string fun = "fun";
        
        string visibilityString = Visibility.ToKotlinSyntaxString();

        string externalString = IsExternal
            ? "external"
            : string.Empty;
        
        string overrideString = IsOverride
            ? "override"
            : string.Empty;

        string returnString = !string.IsNullOrEmpty(ReturnTypeName)
            ? $": {ReturnTypeName}"
            : string.Empty;

        string[] signatureComponents = [
            visibilityString,
            externalString,
            overrideString,
            fun,
            $"{Name}({Parameters})",
            returnString
        ];

        string signature = KotlinFunSignatureComponents.ComponentsToString(signatureComponents);

        string fullFunc;
        
        if (!string.IsNullOrEmpty(Implementation)) {
            string indentedImpl = Implementation.IndentAllLines(1);
            
            fullFunc = $"{signature} {{\n{indentedImpl}\n}}";
        } else {
            fullFunc = signature;
        }

        return fullFunc;
    }
}