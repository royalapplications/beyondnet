using System.Text.Json;
using System.Text.Json.Serialization;

namespace NativeAOT.CodeGenerator.CLI;

[JsonSerializable(typeof(Configuration))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata, WriteIndented = true)]
internal partial class ConfigurationMetadataOnlyContext : JsonSerializerContext { }

internal class ConfigurationSerializer
{
    public Configuration? DeserializeFromJson(string jsonString)
    {
        Configuration? configuration = JsonSerializer.Deserialize(jsonString, ConfigurationMetadataOnlyContext.Default.Configuration);

        if (configuration == null) {
            throw new Exception("An unknown error occured while deserializing JSON.");
        }

        return configuration;
    }
}