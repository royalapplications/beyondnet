using Beyond.NET.Core;
using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Class
{
    private readonly string m_name;

    private string? m_baseTypeName = null;
    private string? m_protocolConformance = null;
    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private string? m_implementation = null;

    public Class(
        string name
    )
    {
        m_name = name;
    }

    #region Visibility
    public Class Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;

        return this;
    }

    public Class Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public Class Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }

    public Class Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }

    public Class Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }

    public Class FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility

    #region BaseTypeName
    public Class BaseTypeName(string? baseTypeName = null)
    {
        m_baseTypeName = baseTypeName;

        return this;
    }
    #endregion BaseTypeName

    #region ProtocolConformance
    public Class ProtocolConformance(string? protocolConformance = null)
    {
        m_protocolConformance = protocolConformance;

        return this;
    }
    #endregion ProtocolConformance

    #region Implementation
    public Class Implementation(string? implementation = null)
    {
        m_implementation = implementation;

        return this;
    }
    #endregion Implementation

    #region Build
    public SwiftClassDeclaration Build()
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