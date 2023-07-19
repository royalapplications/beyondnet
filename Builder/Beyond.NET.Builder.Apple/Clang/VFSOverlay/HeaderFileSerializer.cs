using System.Text.Json;
using System.Text.Json.Serialization;

namespace Beyond.NET.Builder.Apple.Clang.VFSOverlay;

[JsonSerializable(typeof(HeaderFile))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata, WriteIndented = true)]
internal partial class ConfigurationMetadataOnlyContext : JsonSerializerContext { }

public class HeaderFileSerializer
{
    public string SerializeToJson(HeaderFile headerFile)
    {
        string json = JsonSerializer.Serialize(headerFile, ConfigurationMetadataOnlyContext.Default.HeaderFile);

        return json;
    }
    
    public HeaderFile DeserializeFromJson(string jsonString)
    {
        HeaderFile? headerFile = JsonSerializer.Deserialize(jsonString, ConfigurationMetadataOnlyContext.Default.HeaderFile);

        if (headerFile is null) {
            throw new Exception("An unknown error occured while deserializing JSON.");
        }

        return headerFile;
    }
    
    public HeaderFile DeserializeFromJsonFilePath(string jsonFilePath)
    {
        string jsonString = File.ReadAllText(jsonFilePath);
        HeaderFile headerFile = DeserializeFromJson(jsonString);

        return headerFile;
    }
}