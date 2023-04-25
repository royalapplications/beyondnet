using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

namespace NativeAOT.CodeGenerator.Syntax.Swift.Builders;

public struct Let
{
    private readonly string m_name;

    private string? m_typeName = null;
    private string? m_value = null;
    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private SwiftTypeAttachmentKinds m_typeAttachmentKind = SwiftTypeAttachmentKinds.Instance;

    public Let(
        string name
    )
    {
        m_name = name;
    }

    #region TypeName
    public Let TypeName(string? typeName)
    {
        m_typeName = typeName;

        return this;
    }
    #endregion TypeName
    
    #region Value
    public Let Value(string? value)
    {
        m_value = value;

        return this;
    }
    #endregion Value
    
    #region Visibility
    public Let Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;
        
        return this;
    }
    
    public Let Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public Let Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }
    
    public Let Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }
    
    public Let Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }
    
    public Let FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility
    
    #region TypeAttachmentKind
    public Let TypeAttachmentKind(SwiftTypeAttachmentKinds typeAttachmentKind)
    {
        m_typeAttachmentKind = typeAttachmentKind;

        return this;
    }

    public Let Static()
    {
        return TypeAttachmentKind(SwiftTypeAttachmentKinds.Static);
    }
    
    public Let Class()
    {
        return TypeAttachmentKind(SwiftTypeAttachmentKinds.Class);
    }
    #endregion TypeAttachmentKind
    
    #region Build
    public SwiftLetDeclaration Build()
    {
        return new(
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