using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Builders;

public struct FunSignatureParameter
{
    private readonly string m_name;
    private readonly string m_typeName;
    
    public FunSignatureParameter
    (
        string name,
        string typeName
    )
    {
        m_name = name;
        m_typeName = typeName;
    }
    
    #region Build
    public KotlinFunSignatureParameter Build()
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
    #endregion Build
}