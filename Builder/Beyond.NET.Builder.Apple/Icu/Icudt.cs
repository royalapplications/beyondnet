using System.Reflection;

namespace Beyond.NET.Builder.Apple.Icu;

public static class Icudt
{
    public const string FILE_NAME = "icudt";
    public const string FILE_TYPE = "dat";
    
    public const string FULL_FILENAME = $"{FILE_NAME}.{FILE_TYPE}";
    
    // NOTE: This name is hardcoded as "LogicalName" in the csproj
    private const string RESOURCE_NAME = $"Beyond.NET.Builder.Apple.Icu.{FULL_FILENAME}";
    
    public static byte[] GetContent()
    {
        var assembly = Assembly.GetAssembly(typeof(Icudt));

        if (assembly is null) {
            throw new Exception($"Failed to get assembly for {FULL_FILENAME} file");
        }

        using var resourceStream = assembly.GetManifestResourceStream(RESOURCE_NAME);

        if (resourceStream is null) {
            throw new Exception("Failed to get resource stream");
        }

        using var memoryStream = new MemoryStream();
        
        resourceStream.CopyTo(memoryStream);
        var data = memoryStream.ToArray();
        
        return data;
    }
}