using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftProtocolDeclaration
{
    public string Name { get; }
    public string? BaseTypeName { get; }
    public string? ProtocolConformance { get; }
    public SwiftVisibilities Visibility { get; }
    public string? Implementation { get; }

    public SwiftProtocolDeclaration(
        string name,
        string? baseTypeName,
        string? protocolConformance,
        SwiftVisibilities visibility,
        string? implementation
    )
    {
        Name = name;
        BaseTypeName = baseTypeName;
        ProtocolConformance = protocolConformance;
        Visibility = visibility;
        Implementation = implementation;
    }

    public override string ToString()
    {
        const string protocol = "protocol";
        
        string visibilityString = Visibility.ToSwiftSyntaxString();

        string baseTypeAndProtocolConformanceDecl = !string.IsNullOrEmpty(BaseTypeName)
            ? $": {BaseTypeName}"
            : string.Empty;

        if (!string.IsNullOrEmpty(ProtocolConformance)) {
            baseTypeAndProtocolConformanceDecl += !string.IsNullOrEmpty(baseTypeAndProtocolConformanceDecl)
                ? $", {ProtocolConformance}"
                : $": {ProtocolConformance}";
        } 
        
        string[] signatureComponents = [
            visibilityString,
            protocol,
            $"{Name}{baseTypeAndProtocolConformanceDecl}"
        ];

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