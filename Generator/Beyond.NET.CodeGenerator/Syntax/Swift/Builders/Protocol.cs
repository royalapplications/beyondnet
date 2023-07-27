using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Protocol
{
    private readonly string m_name;

    private string? m_baseTypeName = null;
    private string? m_protocolConformance = null;
    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private string? m_implementation = null;
    
    public Protocol(
        string name
    )
    {
        m_name = name;
    }
    
    #region BaseTypeName
    public Protocol BaseTypeName(string? baseTypeName = null)
    {
        m_baseTypeName = baseTypeName;

        return this;
    }
    #endregion BaseTypeName

    #region ProtocolConformance
    public Protocol ProtocolConformance(string? protocolConformance = null)
    {
        m_protocolConformance = protocolConformance;

        return this;
    }
    #endregion ProtocolConformance
    
    #region Visibility
    public Protocol Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;
        
        return this;
    }
    
    public Protocol Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public Protocol Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }
    
    public Protocol Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }
    
    public Protocol Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }
    
    public Protocol FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility
    
    #region Implementation
    public Protocol Implementation(string? implementation = null)
    {
        m_implementation = implementation;

        return this;
    }
    #endregion Implementation
    
    #region Build
    public SwiftProtocolDeclaration Build()
    {
        return new(
            m_name,
            m_baseTypeName,
            m_protocolConformance,
            m_visibility,
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