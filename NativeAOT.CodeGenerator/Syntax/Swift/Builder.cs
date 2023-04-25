namespace NativeAOT.CodeGenerator.Syntax.Swift;

public struct Builder
{
    public static Builders.GetOnlyProperty GetOnlyProperty(
        string name,
        string returnType
    )
    {
        return new(
            name,
            returnType
        );
    }

    public static Builders.TypeAlias TypeAlias
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

    public static Builders.SingleLineComment SingleLineComment
    (
        string comment
    )
    {
        return new(
            comment
        );
    }
    
    public static Builders.Class Class
    (
        string name
    )
    {
        return new(
            name
        );
    }
    
    public static Builders.Struct Struct
    (
        string name
    )
    {
        return new(
            name
        );
    }
    
    public static Builders.Enum Enum
    (
        string name
    )
    {
        return new(
            name
        );
    }
    
    public static Builders.Func Func
    (
        string name
    )
    {
        return new(
            name
        );
    }
    
    public static Builders.Initializer Initializer()
    {
        return new();
    }
    
    public static Builders.Closure Closure()
    {
        return new();
    }
    
    public static Builders.FuncSignatureParameter FuncSignatureParameter
    (
        string label,
        string? name,
        string typeName
    )
    {
        return new(
            label,
            name,
            typeName
        );
    }
    
    public static Builders.FuncSignatureParameter FuncSignatureParameter
    (
        string label,
        string typeName
    )
    {
        return new(
            label,
            typeName
        );
    }
    
    public static Builders.FuncSignatureParameters FuncSignatureParameters()
    {
        return new();
    }
    
    public static Builders.Let Let
    (
        string name
    )
    {
        return new(
            name
        );
    }
}