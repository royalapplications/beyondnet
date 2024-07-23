using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;

readonly struct CSharpCodeBuilder
{
    readonly StringBuilder m_sb;

    public CSharpCodeBuilder() : this(new StringBuilder())
    { }

    internal CSharpCodeBuilder([StringSyntax("C#")] string? value = null) : this(new StringBuilder(value))
    { }
    
    private CSharpCodeBuilder(StringBuilder sb) => m_sb = sb;

    internal CSharpCodeBuilder Append(char c) => new(m_sb.Append(c));
    
    internal CSharpCodeBuilder Append([StringSyntax("C#")] string value) => new(m_sb.Append(value));
    
    internal CSharpCodeBuilder AppendLine([StringSyntax("C#")] string? value = null) => new(m_sb.AppendLine(value));

    public override string ToString() => m_sb.ToString();
}