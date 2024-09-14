using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinFunDeclaration
{
    public string Name { get; }
    public KotlinVisibilities Visibility { get; }
    public bool IsExternal { get; }
    public bool IsOverride { get; }
    public bool IsOperator { get; }
    public string? ExtendedTypeName { get; }
    public string Parameters { get; }
    public string? ReturnTypeName { get; }
    public HashSet<string>? Attributes { get; }
    public string? Implementation { get; }
    
    public KotlinFunDeclaration(
        string name,
        KotlinVisibilities visibility,
        bool isExternal,
        bool isOverride,
        bool isOperator,
        string? extendedTypeName,
        string parameters,
        string? returnTypeName,
        HashSet<string>? attributes,
        string? implementation
    )
    {
        Name = !string.IsNullOrEmpty(name)
            ? name 
            : throw new ArgumentOutOfRangeException(nameof(name));
        
        Visibility = visibility;
        IsExternal = isExternal;
        IsOverride = isOverride;
        IsOperator = isOperator;
        
        ExtendedTypeName = !string.IsNullOrEmpty(extendedTypeName)
            ? extendedTypeName
            : null;
        
        Parameters = parameters;

        ReturnTypeName = !string.IsNullOrEmpty(returnTypeName)
            ? returnTypeName
            : null;

        Attributes = attributes;
        
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
        
        string operatorString = IsOperator
            ? "operator"
            : string.Empty;

        string extendedTypeNameString = !string.IsNullOrEmpty(ExtendedTypeName)
            ? $"{ExtendedTypeName}."
            : string.Empty;
        
        string returnString = !string.IsNullOrEmpty(ReturnTypeName)
            ? $": {ReturnTypeName}"
            : string.Empty;

        string attributesString;

        if (Attributes is not null) {
            attributesString = string.Join(" ", Attributes);
        } else {
            attributesString = string.Empty;
        }

        string[] signatureComponents = [
            attributesString,
            operatorString,
            visibilityString,
            externalString,
            overrideString,
            fun,
            $"{extendedTypeNameString}{Name}({Parameters})",
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