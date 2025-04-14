using System.Text.Json;
using System.Text.Json.Serialization;

namespace Beyond.NET.CodeGenerator.CLI;

[JsonSerializable(typeof(Configuration))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata, WriteIndented = true)]
internal partial class ConfigurationMetadataOnlyContext : JsonSerializerContext { }

internal class ConfigurationSerializer
{
    internal Configuration? DeserializeFromJson(string jsonString)
    {
        Configuration? configuration = JsonSerializer.Deserialize(jsonString, ConfigurationMetadataOnlyContext.Default.Configuration);

        if (configuration == null) {
            throw new Exception("An unknown error occured while deserializing JSON.");
        }

        return configuration;
    }

    internal Configuration? DeserializeFromJsonFilePath(string jsonFilePath)
    {
        string jsonString = File.ReadAllText(jsonFilePath);
        Configuration? configuration = DeserializeFromJson(jsonString);

        return configuration;
    }
}