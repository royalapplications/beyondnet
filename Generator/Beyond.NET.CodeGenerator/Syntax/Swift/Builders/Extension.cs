using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Extension
{
    private readonly string m_name;

    private string? m_protocolConformance = null;
    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private string? m_implementation = null;

    public Extension(
        string name
    )
    {
        m_name = name;
    }

    #region ProtocolConformance
    public Extension ProtocolConformance(string? protocolConformance = null)
    {
        m_protocolConformance = protocolConformance;

        return this;
    }
    #endregion ProtocolConformance

    #region Visibility
    public Extension Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;

        return this;
    }

    public Extension Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public Extension Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }

    public Extension Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }

    public Extension Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }

    public Extension FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility

    #region Implementation
    public Extension Implementation(string? implementation = null)
    {
        m_implementation = implementation;

        return this;
    }
    #endregion Implementation

    #region Build
    public SwiftExtensionDeclaration Build()
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