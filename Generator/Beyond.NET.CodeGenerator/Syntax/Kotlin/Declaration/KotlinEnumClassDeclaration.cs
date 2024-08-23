using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinEnumClassDeclaration
{
    public string Name { get; }
    public KotlinVisibilities Visibility { get; }
    public string UnderlyingTypeName { get; }
    public string? Implementation { get; }

    public KotlinEnumClassDeclaration(
        string name,
        KotlinVisibilities visibility,
        string underlyingTypeName,
        string? implementation
    )
    {
        Name = !string.IsNullOrEmpty(name)
            ? name 
            : throw new ArgumentOutOfRangeException(nameof(name));
        
        Visibility = visibility;
        
        UnderlyingTypeName = !string.IsNullOrEmpty(underlyingTypeName)
            ? underlyingTypeName 
            : throw new ArgumentOutOfRangeException(nameof(underlyingTypeName));

        Implementation = implementation;
    }
    
    public override string ToString()
    {
        const string enumClass = "enum class";
        
        var visibilityString = Visibility.ToKotlinSyntaxString();
        var underlyingTypeString = $"(val rawValue: {UnderlyingTypeName})";
        var nameAndUnderlyingTypeString = $"{Name}{underlyingTypeString}";

        string[] signatureComponents = [
            visibilityString,
            enumClass,
            nameAndUnderlyingTypeString
        ];

        string signature = KotlinFunSignatureComponents.ComponentsToString(signatureComponents);

        string fullFunc;
        
        if (!string.IsNullOrEmpty(Implementation)) {
            string indentedImplementation = Implementation.IndentAllLines(1);
            
            fullFunc = $"{signature} {{\n{indentedImplementation}\n}}";
        } else {
            fullFunc = signature;
        }

        return fullFunc;
    }
}