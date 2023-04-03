namespace NativeAOT.CodeGenerator.CLI;

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
}