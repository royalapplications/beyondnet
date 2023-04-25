using NativeAOT.CodeGenerator.Syntax.Swift.Builders;

namespace NativeAOT.CodeGenerator.Syntax.Swift;

public struct Builder
{
    public static GetOnlyProperty GetOnlyProperty(
        string name,
        string returnType
    )
    {
        return new GetOnlyProperty(
            name,
            returnType
        );
    }

    public static TypeAlias TypeAlias
    (
        string aliasTypeName,
        string originalTypeName
    )
    {
        return new TypeAlias(
            aliasTypeName,
            originalTypeName
        );
    }

    public static SingleLineComment SingleLineComment
    (
        string comment
    )
    {
        return new SingleLineComment(
            comment
        );
    }
    
    public static Class Class
    (
        string name
    )
    {
        return new Class(
            name
        );
    }
}