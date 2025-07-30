using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinFunctionTypeDeclaration
{
    public string? Parameters { get; }
    public string? ReturnTypeName { get; }

    public KotlinFunctionTypeDeclaration(
        string? parameters,
        string? returnTypeName
    )
    {
        Parameters = parameters;
        ReturnTypeName = returnTypeName;
    }

    public override string ToString()
    {
        const string parameterListPrefix = "(";
        const string parameterListSuffix = ")";
        const string returnTypePrefix = "->";
        const string voidReturnType = "Unit";

        string parameters = Parameters ?? string.Empty;
        string returnTypeName = string.IsNullOrEmpty(ReturnTypeName) ? voidReturnType : ReturnTypeName;

        var decl = $"{parameterListPrefix}{parameters}{parameterListSuffix} {returnTypePrefix} {returnTypeName}";

        return decl;
    }
}
