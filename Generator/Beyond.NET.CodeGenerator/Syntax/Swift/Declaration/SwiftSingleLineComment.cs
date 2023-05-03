namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftSingleLineComment
{
    public string Comment { get; }

    public SwiftSingleLineComment(string comment)
    {
        Comment = comment;
    }

    public override string ToString()
    {
        return $"// {Comment}";
    }
}