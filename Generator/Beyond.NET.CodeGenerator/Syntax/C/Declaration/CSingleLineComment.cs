namespace Beyond.NET.CodeGenerator.Syntax.C.Declaration;

public struct CSingleLineComment
{
    public string Comment { get; }

    public CSingleLineComment(string comment)
    {
        Comment = comment;
    }

    public override string ToString()
    {
        return $"// {Comment}";
    }
}