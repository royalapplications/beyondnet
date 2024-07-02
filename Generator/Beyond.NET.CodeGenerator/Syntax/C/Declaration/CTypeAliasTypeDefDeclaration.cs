namespace Beyond.NET.CodeGenerator.Syntax.C.Declaration;

public struct CTypeAliasTypeDefDeclaration
{
    public string AliasTypeName { get; }
    public string OriginalTypeName { get; }

    public CTypeAliasTypeDefDeclaration(
        string aliasTypeName,
        string originalTypeName
    )
    {
        AliasTypeName = aliasTypeName;
        OriginalTypeName = originalTypeName;
    }

    public override string ToString()
    {
        const string typedef = "typedef";
        
        string[] components = [
            typedef,
            OriginalTypeName,
            AliasTypeName
        ];

        string signature = string.Join(' ', components) + ";";

        return signature;
    }
}