using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct TypeAlias
{
    private readonly string m_aliasTypeName;
    private readonly string m_originalTypeName;

    private SwiftVisibilities m_visibility = SwiftVisibilities.None;

    public TypeAlias(
        string aliasTypeName,
        string originalTypeName
    )
    {
        m_aliasTypeName = aliasTypeName;
        m_originalTypeName = originalTypeName;
    }

    #region Visibility
    public TypeAlias Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;

        return this;
    }

    public TypeAlias Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public TypeAlias Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }

    public TypeAlias Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }

    public TypeAlias Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }

    public TypeAlias FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility

    #region Build
    public SwiftTypeAliasDeclaration Build()
    {
        return new(
            m_aliasTypeName,
            m_visibility,
            m_originalTypeName
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