using Beyond.NET.CodeGenerator.Syntax.C.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.C.Builders;

public struct SingleLineComment
{
    private readonly string m_comment;
    
    public SingleLineComment(
        string comment
    )
    {
        m_comment = comment;
    }
    
    #region Build
    public CSingleLineComment Build()
    {
        return new(
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