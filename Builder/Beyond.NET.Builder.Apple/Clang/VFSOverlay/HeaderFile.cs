using System.Text.Json.Serialization;

namespace Beyond.NET.Builder.Apple.Clang.VFSOverlay;

public class HeaderFile
{
    [JsonPropertyName("version")]
    public int Version { get; set; }
    
    [JsonPropertyName("case-sensitive")]
    public bool CaseSensitive { get; set; }
    
    [JsonPropertyName("roots")]
    public HeaderFileRoot[]? Roots { get; set; }
}

public class HeaderFileRoot
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [JsonPropertyName("contents")]
    public HeaderFileContent[]? Contents { get; set; }
}

public class HeaderFileContent
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [JsonPropertyName("external-contents")]
    public string? ExternalContents { get; set; }
}