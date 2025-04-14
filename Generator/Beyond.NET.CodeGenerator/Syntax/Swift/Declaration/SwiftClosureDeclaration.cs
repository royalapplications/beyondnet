namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftClosureDeclaration
{
    public string Parameters { get; }
    public bool Throws { get; }
    public string? ReturnTypeName { get; }

    public SwiftClosureDeclaration(
        string parameters,
        bool throws,
        string? returnTypeName
    )
    {
        Parameters = parameters;
        Throws = throws;

        ReturnTypeName = !string.IsNullOrEmpty(returnTypeName)
            ? returnTypeName
            : null;
    }

    public override string ToString()
    {
        string throwsString = Throws
            ? "throws"
            : string.Empty;

        string returnString = !string.IsNullOrEmpty(ReturnTypeName)
            ? $"-> {ReturnTypeName}"
            : string.Empty;

        string[] signatureComponents = new[] {
            $"({Parameters})",
            throwsString,
            returnString
        };

        string signature = SwiftFuncSignatureComponents.ComponentsToString(signatureComponents);

        return signature;
    }
}