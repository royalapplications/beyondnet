using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Builders;

public struct Fun
{
    private readonly string m_name;

    private KotlinVisibilities m_visibility = KotlinVisibilities.None;
    private bool m_external = false;
    private bool m_override = false;
    private string? m_parameters = null;
    private string? m_returnTypeName = null;
    private string? m_implementation = null;

    public Fun
    (
        string name
    )
    {
        m_name = name;
    }
    
    #region Visibility
    public Fun Visibility(KotlinVisibilities visibility)
    {
        m_visibility = visibility;
        
        return this;
    }
    
    public Fun Open()
    {
        return Visibility(KotlinVisibilities.Open);
    }

    public Fun Public()
    {
        return Visibility(KotlinVisibilities.Public);
    }
    
    // TODO
    // public Fun Internal()
    // {
    //     return Visibility(KotlinVisibilities.Internal);
    // }
    
    public Fun Private()
    {
        return Visibility(KotlinVisibilities.Private);
    }
    #endregion Visibility
    
    #region External
    public Fun External(bool isExternal = true)
    {
        m_external = isExternal;

        return this;
    }
    #endregion External
    
    #region Override
    public Fun Override(bool isOverride = true)
    {
        m_override = isOverride;

        return this;
    }
    #endregion Override

    #region Parameters
    public Fun Parameters(string? parameters = null)
    {
        m_parameters = parameters;

        return this;
    }
    #endregion Parameters
    
    #region ReturnTypeName
    public Fun ReturnTypeName(string? returnTypeName = null)
    {
        m_returnTypeName = returnTypeName;

        return this;
    }
    #endregion ReturnTypeName
    
    #region Implementation
    public Fun Implementation(string? implementation = null)
    {
        m_implementation = implementation;

        return this;
    }
    #endregion Implementation
    
    #region Build
    public KotlinFunDeclaration Build()
    {
        return new(
            m_name,
            m_visibility,
            m_external,
            m_override,
            m_parameters ?? string.Empty,
            m_returnTypeName,
            m_implementation
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