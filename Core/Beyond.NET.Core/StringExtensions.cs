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

    /// <summary>
    /// This calls [NSString stringByResolvingSymlinksInPath] under the hood which is only available on Apple platforms.
    /// It's not used at the moment but could be used by Beyond.NET.Builder.Apple.
    /// </summary>
    public static string ResolveSymlinks_AppleOnly(this string path)
    {
        string resolvedPath;
		
        using (NSString unresolvedPathNS = new(path)) {
            using (NSString? resolvedPathNS = unresolvedPathNS.StringByResolvingSymlinksInPath) {
                if (resolvedPathNS is null) {
                    throw new Exception("Failed to resolve path");
                }

                resolvedPath = resolvedPathNS.UTF8String ?? string.Empty;
            }
        }

        return resolvedPath;
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