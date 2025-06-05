using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Guard
{
    private readonly string m_condition;
    private readonly string m_elseStatement;

    public Guard(
        string condition,
        string elseStatement
    )
    {
        m_condition = condition;
        m_elseStatement = elseStatement;
    }

    #region Build
    public SwiftGuard Build()
    {
        return new(
            m_condition,
            m_elseStatement
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
