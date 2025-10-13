namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public enum KotlinVisibilities
{
    None,
    Private,
    Public,
    Open
}

public static class KotlinVisibilities_Extensions
{
    public static string ToKotlinSyntaxString(this KotlinVisibilities visibilty)
    {
        return visibilty switch {
            KotlinVisibilities.None => string.Empty,
            KotlinVisibilities.Private => "private",
            KotlinVisibilities.Public => "public",
            KotlinVisibilities.Open => "open",
            _ => throw new Exception($"Unknown Kotlin Visibility: {visibilty}")
        };
    }
}
