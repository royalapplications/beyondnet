using System.Diagnostics.CodeAnalysis;
using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Builders;

public struct Variable
{
    private readonly KotlinVariableKinds m_variableKind;
    private readonly string m_name;

    private string? m_typeName = null;
    private string? m_value = null;
    private KotlinVisibilities m_visibility = KotlinVisibilities.None;

    public Variable(
        KotlinVariableKinds variableKind,
        string name
    )
    {
        m_variableKind = variableKind;
        m_name = name;
    }

    #region TypeName
    public Variable TypeName([StringSyntax("Kt")] string? typeName)
    {
        m_typeName = typeName;

        return this;
    }
    #endregion TypeName
    
    #region Value
    public Variable Value([StringSyntax("Kt")]string? value)
    {
        m_value = value;

        return this;
    }
    #endregion Value
    
    #region Visibility
    public Variable Visibility(KotlinVisibilities visibility)
    {
        m_visibility = visibility;
        
        return this;
    }
    
    public Variable Open()
    {
        return Visibility(KotlinVisibilities.Open);
    }

    public Variable Public()
    {
        return Visibility(KotlinVisibilities.Public);
    }
    
    // public Variable Internal()
    // {
    //     return Visibility(KotlinVisibilities.Internal);
    // }
    
    public Variable Private()
    {
        return Visibility(KotlinVisibilities.Private);
    }
    #endregion Visibility
    
    #region Build
    public KotlinVariableDeclaration Build()
    {
        return new(
            m_variableKind,
            m_name,
            m_typeName,
            m_value,
            m_visibility
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