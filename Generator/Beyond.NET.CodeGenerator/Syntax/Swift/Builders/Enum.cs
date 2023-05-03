using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Enum
{
    private readonly string m_name;

    private string? m_rawTypeName = null;
    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private string? m_implementation = null;

    public Enum
    (
        string name
    )
    {
        m_name = name;
    }

    #region RawTypeName
    public Enum RawTypeName(string? rawTypeName = null)
    {
        m_rawTypeName = rawTypeName;

        return this;
    }
    #endregion RawTypeName
    
    #region Visibility
    public Enum Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;
        
        return this;
    }
    
    public Enum Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public Enum Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }
    
    public Enum Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }
    
    public Enum Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }
    
    public Enum FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility
    
    #region Implementation
    public Enum Implementation(string? implementation = null)
    {
        m_implementation = implementation;

        return this;
    }
    #endregion Implementation
    
    #region Build
    public SwiftEnumDeclaration Build()
    {
        return new(
            m_name,
            m_rawTypeName,
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