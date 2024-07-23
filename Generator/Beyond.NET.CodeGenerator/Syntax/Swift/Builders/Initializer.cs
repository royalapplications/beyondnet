using System.Diagnostics.CodeAnalysis;
using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Initializer
{
    private bool m_convenience = false;
    private bool m_failable = false;
    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private string? m_parameters = null;
    private bool m_throws = false;
    private string? m_implementation = null;
    
    public Initializer() { }

    #region Convenience
    public Initializer Convenience(bool convenience = true)
    {
        m_convenience = convenience;

        return this;
    }
    #endregion Convenience
    
    #region Failable
    public Initializer Failable(bool failable = true)
    {
        m_failable = failable;

        return this;
    }
    #endregion Failable
    
    #region Visibility
    public Initializer Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;
        
        return this;
    }
    
    public Initializer Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public Initializer Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }
    
    public Initializer Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }
    
    public Initializer Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }
    
    public Initializer FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility

    #region Parameters
    public Initializer Parameters(string? parameters = null)
    {
        m_parameters = parameters;

        return this;
    }
    #endregion Parameters
    
    #region Throws
    public Initializer Throws(bool throws = true)
    {
        m_throws = throws;

        return this;
    }
    #endregion Throws
    
    #region Implementation
    public Initializer Implementation([StringSyntax("Swift")] string? implementation = null)
    {
        m_implementation = implementation;

        return this;
    }
    #endregion Implementation
    
    #region Build
    public SwiftInitDeclaration Build()
    {
        return new(
            m_convenience,
            m_failable,
            m_visibility,
            m_parameters ?? string.Empty,
            m_throws,
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