using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftFuncDeclaration
{
    public string Name { get; }
    public SwiftVisibilities Visibility { get; }
    public SwiftTypeAttachmentKinds TypeAttachmentKind { get; }
    public bool IsOverride { get; }
    public string Parameters { get; }
    public bool Throws { get; }
    public string? ReturnTypeName { get; }
    
    public SwiftFuncDeclaration(
        string name,
        SwiftVisibilities visibility,
        SwiftTypeAttachmentKinds typeAttachmentKind,
        bool isOverride,
        string parameters,
        bool throws,
        string? returnTypeName
    )
    {
        Name = !string.IsNullOrEmpty(name)
            ? name 
            : throw new ArgumentOutOfRangeException(nameof(name));
        
        Visibility = visibility;
        TypeAttachmentKind = typeAttachmentKind;
        IsOverride = isOverride;
        Parameters = parameters;
        Throws = throws;

        ReturnTypeName = !string.IsNullOrEmpty(returnTypeName)
            ? returnTypeName 
            : null;
    }

    public override string ToString()
    {
        const string func = "func";
        
        string visibilityString = Visibility.ToSwiftSyntaxString();
        string typeAttachmentKindString = TypeAttachmentKind.ToSwiftSyntaxString();

        string overrideString = IsOverride
            ? "override"
            : string.Empty;

        string throwsString = Throws
            ? "throws"
            : string.Empty;

        string returnString = !string.IsNullOrEmpty(ReturnTypeName)
            ? $"-> {ReturnTypeName}"
            : string.Empty;

        string[] signatureComponents = new[] {
            visibilityString,
            overrideString,
            typeAttachmentKindString,
            func,
            $"{Name}({Parameters})",
            throwsString,
            returnString
        };

        string signature = SwiftFuncSignatureComponents.ComponentsToString(signatureComponents);

        return signature;
    }
}