using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Beyond.NET.CodeGenerator.Generator.C;

readonly struct CCodeBuilder
{
    readonly StringBuilder m_sb;

    public CCodeBuilder() : this(new StringBuilder())
    { }

    internal CCodeBuilder([StringSyntax("C")] string? value = null) : this(new StringBuilder(value))
    { }
    
    private CCodeBuilder(StringBuilder sb) => m_sb = sb;

    internal CCodeBuilder Append(char c) => new(m_sb.Append(c));
    
    internal CCodeBuilder Append([StringSyntax("C")] string value) => new(m_sb.Append(value));
    
    internal CCodeBuilder AppendLine([StringSyntax("C")] string? value = null) => new(m_sb.AppendLine(value));
    
    public override string ToString() => m_sb.ToString();
}