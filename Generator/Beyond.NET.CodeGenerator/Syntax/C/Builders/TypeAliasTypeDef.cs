using Beyond.NET.CodeGenerator.Syntax.C.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.C.Builders;

public struct TypeAliasTypeDef
{
    private readonly string m_aliasTypeName;
    private readonly string m_originalTypeName;
    
    public TypeAliasTypeDef(
        string aliasTypeName,
        string originalTypeName
    )
    {
        m_aliasTypeName = aliasTypeName;
        m_originalTypeName = originalTypeName;
    }
    
    #region Build
    public CTypeAliasTypeDefDeclaration Build()
    {
        return new(
            m_aliasTypeName,
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