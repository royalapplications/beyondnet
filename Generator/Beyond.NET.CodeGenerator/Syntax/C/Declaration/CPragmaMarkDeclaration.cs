using Beyond.NET.CodeGenerator.Generator.C;

namespace Beyond.NET.CodeGenerator.Syntax.C.Declaration;

public struct CPragmaMarkDeclaration
{
    public bool IsSeparator { get; }
    public string? Comment { get; }

    public CPragmaMarkDeclaration(
        bool isSeparator,
        string? comment
    )
    {
        IsSeparator = isSeparator;
        Comment = comment;
    }
    
    public override string ToString()
    {
        var sb = new CCodeBuilder("#pragma mark");

        if (IsSeparator) {
            sb.Append(" -");
        }

        if (!string.IsNullOrEmpty(Comment)) {
            sb.Append(' ');
            sb.Append(Comment);
        }

        var str = sb.ToString();
        
        return str;
    }
}