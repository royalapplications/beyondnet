using Beyond.NET.CodeGenerator.Syntax.C.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.C.Builders;

public struct PragmaMark
{
    private bool m_isSeparator;
    private string? m_comment;
    
    public PragmaMark() { }

    #region Separator
    public PragmaMark Separator()
    {
        m_isSeparator = true;

        return this;
    }
    #endregion Separator

    #region Comment
    public PragmaMark Comment(string comment)
    {
        m_comment = comment;

        return this;
    }
    #endregion Comment
    
    #region Build
    public CPragmaMarkDeclaration Build()
    {
        return new(
            m_isSeparator,
            m_comment
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