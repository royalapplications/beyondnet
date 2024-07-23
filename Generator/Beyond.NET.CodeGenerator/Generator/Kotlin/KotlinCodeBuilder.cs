using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Beyond.NET.CodeGenerator.Generator.Kotlin;

readonly struct KotlinCodeBuilder
{
    readonly StringBuilder m_sb;

    public KotlinCodeBuilder() : this(new StringBuilder())
    { }

    internal KotlinCodeBuilder([StringSyntax("Kt")] string? value = null) : this(new StringBuilder(value))
    { }
    
    private KotlinCodeBuilder(StringBuilder sb) => m_sb = sb;
    
    internal KotlinCodeBuilder Append(char c) => new(m_sb.Append(c));
    
    internal KotlinCodeBuilder Append([StringSyntax("Kt")] string value) => new(m_sb.Append(value));
    
    internal KotlinCodeBuilder AppendLine([StringSyntax("Kt")] string? value = null) => new(m_sb.AppendLine(value));
    
    public override string ToString() => m_sb.ToString();
}