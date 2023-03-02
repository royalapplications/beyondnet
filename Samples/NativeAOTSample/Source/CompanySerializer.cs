using System.Text.Json;
using System.Text.Json.Serialization;
using NativeAOT.Core;

namespace NativeAOTSample;

[JsonSerializable(typeof(Company))]
[JsonSerializable(typeof(Person))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata, WriteIndented = true)]
internal partial class CompanyMetadataOnlyContext : JsonSerializerContext { }

[NativeExport]
public class CompanySerializer
{
    [NativeExport]
    public string SerializeToJson(Company company)
    {
        string jsonString = JsonSerializer.Serialize(company, CompanyMetadataOnlyContext.Default.Company);

        return jsonString;

        // return null;
    }

    [NativeExport(Throwing = true)]
    public Company DeserializeFromJson(string jsonString)
    {
        Company? company = JsonSerializer.Deserialize(jsonString, CompanyMetadataOnlyContext.Default.Company);

        if (company == null) {
            throw new Exception("An unknown error occured while deserializing JSON.");
        }

        return company;
    }
}