using NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

namespace NativeAOT.CodeGenerator.Syntax.Swift.Builders;

public struct GetOnlyProperty
{
    private readonly string m_name;
    private readonly string m_typeName;
    
    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private SwiftTypeAttachmentKinds m_typeAttachmentKind = SwiftTypeAttachmentKinds.Instance;
    private bool m_override = false;
    private bool m_throws = false;
    private string? m_implementation = null;
    
    public GetOnlyProperty(
        string name,
        string typeName
    )
    {
        m_name = name;
        m_typeName = typeName;
    }

    #region Visibility
    public GetOnlyProperty Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;
        
        return this;
    }
    
    public GetOnlyProperty Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public GetOnlyProperty Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }
    
    public GetOnlyProperty Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }
    
    public GetOnlyProperty Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }
    
    public GetOnlyProperty FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility

    #region TypeAttachmentKind
    public GetOnlyProperty TypeAttachmentKind(SwiftTypeAttachmentKinds typeAttachmentKind)
    {
        m_typeAttachmentKind = typeAttachmentKind;

        return this;
    }

    public GetOnlyProperty Static()
    {
        return TypeAttachmentKind(SwiftTypeAttachmentKinds.Static);
    }
    
    public GetOnlyProperty Class()
    {
        return TypeAttachmentKind(SwiftTypeAttachmentKinds.Class);
    }
    #endregion TypeAttachmentKind

    #region Override
    public GetOnlyProperty Override(bool isOverride = true)
    {
        m_override = isOverride;

        return this;
    }
    #endregion Override

    #region Throws
    public GetOnlyProperty Throws(bool throws = true)
    {
        m_throws = throws;

        return this;
    }
    #endregion Throws

    #region Implementation
    public GetOnlyProperty Implementation(string? implementation = null)
    {
        m_implementation = implementation;

        return this;
    }
    #endregion Implementation

    #region Build
    public SwiftGetOnlyPropertyDeclaration Build()
    {
        return new(
            m_name,
            m_visibility,
            m_typeAttachmentKind,
            m_override,
            m_throws,
            m_typeName,
            m_implementation
        );
    }

    public override string ToString()
    {
        return Build()
            .ToString();
    }
    #endregion Build
}