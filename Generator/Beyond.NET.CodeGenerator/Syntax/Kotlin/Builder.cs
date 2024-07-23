using System.Diagnostics.CodeAnalysis;
using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public struct Builder
{
    // public static Builders.GetOnlyProperty GetOnlyProperty(
    //     string name,
    //     string returnType
    // )
    // {
    //     return new(
    //         name,
    //         returnType
    //     );
    // }
    //
    // public static Builders.TypeAlias TypeAlias
    // (
    //     string aliasTypeName,
    //     string originalTypeName
    // )
    // {
    //     return new(
    //         aliasTypeName,
    //         originalTypeName
    //     );
    // }

    public static Builders.SingleLineComment SingleLineComment
    (
        [StringSyntax("Kt")] string comment
    )
    {
        return new(
            comment
        );
    }
    
    // public static Builders.Class Class
    // (
    //     string name
    // )
    // {
    //     return new(
    //         name
    //     );
    // }
    //
    // public static Builders.Struct Struct
    // (
    //     string name
    // )
    // {
    //     return new(
    //         name
    //     );
    // }
    //
    // public static Builders.Enum Enum
    // (
    //     string name
    // )
    // {
    //     return new(
    //         name
    //     );
    // }
    //
    // public static Builders.Extension Extension
    // (
    //     string name
    // )
    // {
    //     return new(
    //         name
    //     );
    // }
    //
    // public static Builders.Protocol Protocol
    // (
    //     string name
    // )
    // {
    //     return new(
    //         name
    //     );
    // }
    
    public static Builders.Fun Fun
    (
        string name
    )
    {
        return new(
            name
        );
    }
    
    // public static Builders.Initializer Initializer()
    // {
    //     return new();
    // }
    //
    // public static Builders.Closure Closure()
    // {
    //     return new();
    // }
    
    public static Builders.FunSignatureParameter FunSignatureParameter
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
    
    public static Builders.FunSignatureParameters FunSignatureParameters()
    {
        return new();
    }
    
    public static Builders.Variable Variable
    (
        KotlinVariableKinds variableKind,
        string name
    )
    {
        return new(
            variableKind,
            name
        );
    }
    
    public static Builders.Variable Val
    (
        string name
    )
    {
        return Variable(
            KotlinVariableKinds.Constant,
            name
        );
    }
    
    public static Builders.Variable Var
    (
        string name
    )
    {
        return Variable(
            KotlinVariableKinds.Variable,
            name
        );
    }
}