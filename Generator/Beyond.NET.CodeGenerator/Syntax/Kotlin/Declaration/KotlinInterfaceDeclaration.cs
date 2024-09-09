using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public class KotlinInterfaceDeclaration
{
    public string Name { get; }
    public string? BaseTypeName { get; }
    public string? InterfaceConformance { get; }
    public KotlinVisibilities Visibility { get; }
    public string? Implementation { get; }
    
    public KotlinInterfaceDeclaration(
        string name,
        string? baseTypeName,
        string? interfaceConformance,
        KotlinVisibilities visibility,
        string? implementation
    )
    {
        Name = name;
        BaseTypeName = baseTypeName;
        InterfaceConformance = interfaceConformance;
        Visibility = visibility;
        Implementation = implementation;
    }
    
    public override string ToString()
    {
        const string interfaceKeyword = "interface";

        string visibilityString = Visibility.ToKotlinSyntaxString();

        string baseTypeAndInterfaceConformanceDecl = !string.IsNullOrEmpty(BaseTypeName)
            ? $": {BaseTypeName}"
            : string.Empty;

        if (!string.IsNullOrEmpty(InterfaceConformance)) {
            baseTypeAndInterfaceConformanceDecl += !string.IsNullOrEmpty(baseTypeAndInterfaceConformanceDecl)
                ? $", {InterfaceConformance}"
                : $": {InterfaceConformance}";
        } 
        
        string[] signatureComponents = [
            visibilityString,
            interfaceKeyword,
            $"{Name}{baseTypeAndInterfaceConformanceDecl}"
        ];

        string signature = KotlinFunSignatureComponents.ComponentsToString(signatureComponents);

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