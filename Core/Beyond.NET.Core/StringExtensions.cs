namespace Beyond.NET.Core;

public static class StringExtensions
{
    public static string ExpandTildeAndGetAbsolutePath(this string path)
    {
        string absoluteExpandedPath = path
            .ExpandTildeInPath()
            .GetAbsolutePath();

        return absoluteExpandedPath;
    }
    
    public static string ExpandTildeInPath(this string path)
    {
        string expandedPath = path.Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

        return expandedPath;
    }

    public static string GetAbsolutePath(this string path)
    {
        string absolutePath = Path.GetFullPath(path);

        return absolutePath;
    }
    
    public static string SanitizedProductNameForTempDirectory(this string productName)
    {
        return productName
            .Replace(' ', '_')
            .Replace('.', '_');
    }
    
    public static string IndentAllLines(this string text, int indentCount)
    {
        if (indentCount < 1) {
            return text;
        }
        
        string newLine = Environment.NewLine;
        
        string indentPrefix = string.Empty;

        for (int i = 0; i < indentCount; i++) {
            indentPrefix += "\t";
        }
        
        var lines = text.Split(newLine);
        List<string> indentedLines = new();

        foreach (var line in lines) {
            string indentedLine = indentPrefix + line;
            
            indentedLines.Add(indentedLine);
        }

        string indentedText = string.Join(newLine, indentedLines);

        return indentedText;
    }
}