using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

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
    public SwiftSingleLineComment Build()
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