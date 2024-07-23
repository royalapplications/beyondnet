using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public class KotlinClassDeclaration
{
    public string Name { get; }
    public string? BaseTypeName { get; }
    public string? InterfaceConformance { get; }
    public KotlinVisibilities Visibility { get; }
    public KotlinFunSignatureParameters? PrimaryConstructorParameters { get; }
    public IEnumerable<string> BaseTypePrimaryConstructorParameterNames { get; }
    public string? Implementation { get; }
    
    public KotlinClassDeclaration(
        string name,
        string? baseTypeName,
        string? interfaceConformance,
        KotlinVisibilities visibility,
        KotlinFunSignatureParameters? primaryConstructorParameters,
        IEnumerable<string> baseTypePrimaryConstructorParameterNames,
        string? implementation
    )
    {
        Name = name;
        BaseTypeName = baseTypeName;
        InterfaceConformance = interfaceConformance;
        Visibility = visibility;
        PrimaryConstructorParameters = primaryConstructorParameters;
        BaseTypePrimaryConstructorParameterNames = baseTypePrimaryConstructorParameterNames;
        Implementation = implementation;
    }

    public override string ToString()
    {
        const string @class = "class";
        
        string visibilityString = Visibility.ToKotlinSyntaxString();
        
        var primaryConstructorParametersStr = PrimaryConstructorParameters?.ToString();
        string primaryConstructorStr;

        if (!string.IsNullOrEmpty(primaryConstructorParametersStr)) {
            primaryConstructorStr = $"({primaryConstructorParametersStr})";
        } else {
            primaryConstructorStr = string.Empty;
        }

        bool hasBaseType = !string.IsNullOrEmpty(BaseTypeName);
        string baseTypeWithPrimaryConstructor;

        if (hasBaseType) {
            if (BaseTypePrimaryConstructorParameterNames.Any()) {
                var parameterNames = string.Join(", ", BaseTypePrimaryConstructorParameterNames);

                baseTypeWithPrimaryConstructor = $"{BaseTypeName}({parameterNames})";
            } else {
                baseTypeWithPrimaryConstructor = BaseTypeName!;
            }
        } else {
            baseTypeWithPrimaryConstructor = string.Empty;
        }

        string baseTypeAndInterfaceConformanceDecl = hasBaseType
            ? $": {baseTypeWithPrimaryConstructor}"
            : string.Empty;

        if (!string.IsNullOrEmpty(InterfaceConformance)) {
            baseTypeAndInterfaceConformanceDecl += !string.IsNullOrEmpty(baseTypeAndInterfaceConformanceDecl)
                ? $", {InterfaceConformance}"
                : $": {InterfaceConformance}";
        }
        
        string[] signatureComponents = [
            visibilityString,
            @class,
            $"{Name}{primaryConstructorStr}{baseTypeAndInterfaceConformanceDecl}"
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