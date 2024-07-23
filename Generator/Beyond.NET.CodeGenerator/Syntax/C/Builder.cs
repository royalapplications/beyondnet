using System.Diagnostics.CodeAnalysis;
using Beyond.NET.CodeGenerator.Syntax.C.Builders;

namespace Beyond.NET.CodeGenerator.Syntax.C;

public struct Builder
{
    public static SingleLineComment SingleLineComment
    (
        [StringSyntax("C")] string comment
    )
    {
        return new(
            comment
        );
    }
    
    public static TypeAliasTypeDef TypeAliasTypeDef
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

    public static PragmaMark PragmaMark()
    {
        return new();
    }
}