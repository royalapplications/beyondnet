using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Closure
{
    private string? m_parameters = null;
    private bool m_throws = false;
    private string? m_returnTypeName = null;
    
    public Closure() { }
    
    #region Parameters
    public Closure Parameters(string? parameters = null)
    {
        m_parameters = parameters;

        return this;
    }
    #endregion Parameters
    
    #region Throws
    public Closure Throws(bool throws = true)
    {
        m_throws = throws;

        return this;
    }
    #endregion Throws

    #region ReturnTypeName
    public Closure ReturnTypeName(string? returnTypeName = null)
    {
        m_returnTypeName = returnTypeName;

        return this;
    }
    #endregion ReturnTypeName
    
    #region Build
    public SwiftClosureDeclaration Build()
    {
        return new(
            m_parameters ?? string.Empty,
            m_throws,
            m_returnTypeName
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