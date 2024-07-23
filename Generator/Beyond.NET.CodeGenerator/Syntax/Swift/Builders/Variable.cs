using System.Diagnostics.CodeAnalysis;
using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Variable
{
    private readonly SwiftVariableKinds m_variableKind;
    private readonly string m_name;

    private string? m_typeName = null;
    private string? m_value = null;
    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private SwiftTypeAttachmentKinds m_typeAttachmentKind = SwiftTypeAttachmentKinds.Instance;

    public Variable(
        SwiftVariableKinds variableKind,
        string name
    )
    {
        m_variableKind = variableKind;
        m_name = name;
    }

    #region TypeName
    public Variable TypeName([StringSyntax("Swift")] string? typeName)
    {
        m_typeName = typeName;

        return this;
    }
    #endregion TypeName
    
    #region Value
    public Variable Value([StringSyntax("Swift")]string? value)
    {
        m_value = value;

        return this;
    }
    #endregion Value
    
    #region Visibility
    public Variable Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;
        
        return this;
    }
    
    public Variable Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public Variable Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }
    
    public Variable Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }
    
    public Variable Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }
    
    public Variable FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility
    
    #region TypeAttachmentKind
    public Variable TypeAttachmentKind(SwiftTypeAttachmentKinds typeAttachmentKind)
    {
        m_typeAttachmentKind = typeAttachmentKind;

        return this;
    }

    public Variable Static()
    {
        return TypeAttachmentKind(SwiftTypeAttachmentKinds.Static);
    }
    
    public Variable Class()
    {
        return TypeAttachmentKind(SwiftTypeAttachmentKinds.Class);
    }
    #endregion TypeAttachmentKind
    
    #region Build
    public SwiftVariableDeclaration Build()
    {
        return new(
            m_variableKind,
            m_name,
            m_typeName,
            m_value,
            m_visibility,
            m_typeAttachmentKind
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