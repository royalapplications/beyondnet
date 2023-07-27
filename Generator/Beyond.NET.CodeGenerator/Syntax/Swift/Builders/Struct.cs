using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Struct
{
    private readonly string m_name;

    private string? m_protocolConformance = null;
    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private string? m_implementation = null;
    
    public Struct(
        string name
    )
    {
        m_name = name;
    }

    #region ProtocolConformance
    public Struct ProtocolConformance(string? protocolConformance = null)
    {
        m_protocolConformance = protocolConformance;

        return this;
    }
    #endregion ProtocolConformance
    
    #region Visibility
    public Struct Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;
        
        return this;
    }
    
    public Struct Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public Struct Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }
    
    public Struct Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }
    
    public Struct Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }
    
    public Struct FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility
    
    #region Implementation
    public Struct Implementation(string? implementation = null)
    {
        m_implementation = implementation;

        return this;
    }
    #endregion Implementation
    
    #region Build
    public SwiftStructDeclaration Build()
    {
        return new(
            m_name,
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