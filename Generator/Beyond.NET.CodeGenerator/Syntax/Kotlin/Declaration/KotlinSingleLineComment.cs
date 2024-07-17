namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinSingleLineComment
{
    public string Comment { get; }

    public KotlinSingleLineComment(string comment)
    {
        Comment = comment;
    }

    public override string ToString()
    {
        return $"// {Comment}";
    }
}