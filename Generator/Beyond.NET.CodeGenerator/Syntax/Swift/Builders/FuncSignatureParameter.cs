using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct FuncSignatureParameter
{
    private readonly string m_label;
    private readonly string? m_name;
    private readonly string m_typeName;
    
    public FuncSignatureParameter
    (
        string label,
        string? name,
        string typeName
    )
    {
        m_label = label;
        m_name = name;
        m_typeName = typeName;
    }
    
    public FuncSignatureParameter
    (
        string label,
        string typeName
    )
    {
        m_label = label;
        m_name = null;
        m_typeName = typeName;
    }
    
    #region Build
    public SwiftFuncSignatureParameter Build()
    {
        return new(
            m_label,
            m_name,
            m_typeName
        );
    }

    public override string ToString()
    {
        return Build()
            .ToString();
    }
    #endregion Build
}