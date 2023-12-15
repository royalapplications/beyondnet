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
    /// This calls [NSString stringByResolvingSymlinksInPath] and [NSURL getResourceValue:forKey:error:] under the hood which is only available on Apple platforms.
    /// </summary>
    public static string ResolveSymlinksAndGetCanonicalPath_AppleOnly(this string path)
    {
        string? resolvedPath = null;
		
        using (NSString unresolvedPathNS = new(path)) {
            using (NSString? resolvedPathNS = unresolvedPathNS.StringByResolvingSymlinksInPath) {
                if (resolvedPathNS is not null) {
                    using (NSURL url = new(resolvedPathNS)) {
                        using (var canonicalPathKey = NSURL.NSURLCanonicalPathKey) {
                            var canonicalPathKeyStr = canonicalPathKey.UTF8String;
    
                            if (!string.IsNullOrEmpty(canonicalPathKeyStr)) {
                                using (NSString? canonicalPath = url.GetResourceValue(canonicalPathKey)) {
                                    if (canonicalPath is not null) {
                                        resolvedPath = canonicalPath.UTF8String;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(resolvedPath)) {
            resolvedPath = path;
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
    
    public static string PrefixAllLines(this string text, string prefixText)
    {
        if (string.IsNullOrEmpty(prefixText) ||
            string.IsNullOrEmpty(text)) {
            return text;
        }
        
        string newLine = Environment.NewLine;
        
        var lines = text.Split(newLine);
        List<string> prefixedLines = new();

        foreach (var line in lines) {
            string prefixedLine = prefixText + line;
            
            prefixedLines.Add(prefixedLine);
        }

        string prefixedText = string.Join(newLine, prefixedLines);

        return prefixedText;
    }
}