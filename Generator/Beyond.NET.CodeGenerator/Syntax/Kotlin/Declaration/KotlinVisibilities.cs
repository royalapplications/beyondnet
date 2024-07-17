namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public enum KotlinVisibilities
{
    None,
    Private,
    Public
}

public static class KotlinVisibilities_Extensions
{
    public static string ToKotlinSyntaxString(this KotlinVisibilities visibilty)
    {
        switch (visibilty) {
            case KotlinVisibilities.None:
                return string.Empty;
            case KotlinVisibilities.Private:
                return "private";
            case KotlinVisibilities.Public:
                return "public";
        }

        throw new Exception($"Unknown Kotlin Visibility: {visibilty}");
    }
}