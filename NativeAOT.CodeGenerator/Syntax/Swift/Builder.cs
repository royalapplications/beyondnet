using NativeAOT.CodeGenerator.Syntax.Swift.Builders;

namespace NativeAOT.CodeGenerator.Syntax.Swift;

public struct Builder
{
    public static GetOnlyProperty GetOnlyProperty(
        string name,
        string returnType
    )
    {
        return new(
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
        return new(
            aliasTypeName,
            originalTypeName
        );
    }

    public static SingleLineComment SingleLineComment
    (
        string comment
    )
    {
        return new(
            comment
        );
    }
    
    public static Class Class
    (
        string name
    )
    {
        return new(
            name
        );
    }
    
    public static Struct Struct
    (
        string name
    )
    {
        return new(
            name
        );
    }
    
    public static Initializer Initializer()
    {
        return new();
    }
    
    public static Closure Closure()
    {
        return new();
    }
}