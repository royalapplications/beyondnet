using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Builders;

public struct TypeAlias
{
    private readonly string m_name;
    private readonly string m_typeName;

    public TypeAlias
    (
        string name,
        string typeName
    )
    {
        m_name = name;
        m_typeName = typeName;
    }

    #region Build
    public KotlinTypeAliasDeclaration Build()
    {
        return new(
            m_name,
            m_typeName
        );
    }

    public override string ToString()
    {
        return Build()
            .ToString();
    }

    public string ToIndentedString(int indentationLevel)
    {
        return Build()
            .ToString()
            .IndentAllLines(indentationLevel);
    }
    #endregion Build
}
