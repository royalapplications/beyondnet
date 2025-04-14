using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Builders;

public struct Func
{
    private readonly string m_name;

    private SwiftVisibilities m_visibility = SwiftVisibilities.None;
    private SwiftTypeAttachmentKinds m_typeAttachmentKind = SwiftTypeAttachmentKinds.Instance;
    private bool m_override = false;
    private bool m_throws = false;
    private string? m_parameters = null;
    private string? m_returnTypeName = null;
    private string? m_implementation = null;

    public Func
    (
        string name
    )
    {
        m_name = name;
    }

    #region Visibility
    public Func Visibility(SwiftVisibilities visibility)
    {
        m_visibility = visibility;

        return this;
    }

    public Func Open()
    {
        return Visibility(SwiftVisibilities.Open);
    }

    public Func Public()
    {
        return Visibility(SwiftVisibilities.Public);
    }

    public Func Internal()
    {
        return Visibility(SwiftVisibilities.Internal);
    }

    public Func Private()
    {
        return Visibility(SwiftVisibilities.Private);
    }

    public Func FilePrivate()
    {
        return Visibility(SwiftVisibilities.FilePrivate);
    }
    #endregion Visibility

    #region TypeAttachmentKind
    public Func TypeAttachmentKind(SwiftTypeAttachmentKinds typeAttachmentKind)
    {
        m_typeAttachmentKind = typeAttachmentKind;

        return this;
    }

    public Func Static()
    {
        return TypeAttachmentKind(SwiftTypeAttachmentKinds.Static);
    }

    public Func Class()
    {
        return TypeAttachmentKind(SwiftTypeAttachmentKinds.Class);
    }
    #endregion TypeAttachmentKind

    #region Override
    public Func Override(bool isOverride = true)
    {
        m_override = isOverride;

        return this;
    }
    #endregion Override

    #region Throws
    public Func Throws(bool throws = true)
    {
        m_throws = throws;

        return this;
    }
    #endregion Throws

    #region Parameters
    public Func Parameters(string? parameters = null)
    {
        m_parameters = parameters;

        return this;
    }
    #endregion Parameters

    #region ReturnTypeName
    public Func ReturnTypeName(string? returnTypeName = null)
    {
        m_returnTypeName = returnTypeName;

        return this;
    }
    #endregion ReturnTypeName

    #region Implementation
    public Func Implementation(string? implementation = null)
    {
        m_implementation = implementation;

        return this;
    }
    #endregion Implementation

    #region Build
    public SwiftFuncDeclaration Build()
    {
        return new(
            m_name,
            m_visibility,
            m_typeAttachmentKind,
            m_override,
            m_parameters ?? string.Empty,
            m_throws,
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