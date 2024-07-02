using Beyond.NET.CodeGenerator.Syntax.C.Declaration;

namespace Beyond.NET.CodeGenerator.Syntax.C;

public struct Builder
{
    public static Builders.SingleLineComment SingleLineComment
    (
        string comment
    )
    {
        return new(
            comment
        );
    }
    
    public static Builders.TypeAliasTypeDef TypeAliasTypeDef
    (
        string aliasTypeName,
        string originalTypeName
    )
    {
        return new(
            aliasTypeName,
            originalTypeName
        );
    }
}