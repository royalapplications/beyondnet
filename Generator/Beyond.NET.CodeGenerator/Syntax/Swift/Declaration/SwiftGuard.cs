namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftGuard
{
    public string Condition { get; }
    public string ElseStatement { get; }

    public SwiftGuard(string condition, string elseStatement)
    {
        Condition = !string.IsNullOrEmpty(condition)
            ? condition
            : throw new ArgumentOutOfRangeException(nameof(condition));

        ElseStatement = !string.IsNullOrEmpty(elseStatement)
            ? elseStatement
            : throw new ArgumentOutOfRangeException(nameof(elseStatement));
    }

    public override string ToString()
    {
        return $$"""
               guard {{Condition}} else {
                   {{ElseStatement}}
               }
               """;
    }
}
