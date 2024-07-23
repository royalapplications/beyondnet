using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Beyond.NET.CodeGenerator.Generator.Swift;

readonly struct SwiftCodeBuilder
{
    readonly StringBuilder m_sb;

    public SwiftCodeBuilder() : this(new StringBuilder())
    { }

    internal SwiftCodeBuilder([StringSyntax("Swift")] string? value = null) : this(new StringBuilder(value))
    { }
    
    private SwiftCodeBuilder(StringBuilder sb) => m_sb = sb;
    
    internal SwiftCodeBuilder Append(char c) => new(m_sb.Append(c));
    
    internal SwiftCodeBuilder Append([StringSyntax("Swift")] string value) => new(m_sb.Append(value));
    
    internal SwiftCodeBuilder AppendLine([StringSyntax("Swift")] string? value = null) => new(m_sb.AppendLine(value));
    
    public override string ToString() => m_sb.ToString();
}