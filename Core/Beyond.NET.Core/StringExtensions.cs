namespace Beyond.NET.Core;

public static class StringExtensions
{
    public static string SanitizedProductNameForTempDirectory(this string productName)
    {
        return productName
            .Replace(' ', '_')
            .Replace('.', '_');
    }
}