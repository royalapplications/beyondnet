using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Builders;

public struct FunctionType
{
    private string? m_parameters = null;
    private string? m_returnTypeName = null;

    public FunctionType()
    {
    }

    #region Parameters
    public FunctionType Parameters(string? parameters = null)
    {
        m_parameters = parameters;

        return this;
    }
    #endregion Parameters

    #region ReturnTypeName
    public FunctionType ReturnTypeName(string? returnTypeName = null)
    {
        m_returnTypeName = returnTypeName;

        return this;
    }
    #endregion ReturnTypeName

    #region Build
    public KotlinFunctionTypeDeclaration Build()
    {
        return new(
            m_parameters,
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
