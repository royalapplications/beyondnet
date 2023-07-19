using Beyond.NET.Builder;

namespace Beyond.NET.CodeGenerator.CLI;

static class Program
{
    public static int Main(string[] args)
    {
        // TODO: Just for testing
        // RunBuilderTests();

        string? configFilePath = args.Length == 1
            ? args[0]
            : null;

        if (string.IsNullOrEmpty(configFilePath)) {
            ShowUsage();

            return 1;
        }

        ConfigurationSerializer serializer = new();
        
        Configuration configuration = serializer.DeserializeFromJsonFilePath(configFilePath) 
                                      ?? throw new Exception("Error while parsing configuration file.");

        CodeGeneratorDriver driver = new(configuration);
        
        driver.Generate();

        return 0;
    }

    private static void RunBuilderTests()
    {
        SwiftBuilder.BuilderConfiguration config = new(
            "BeyondDotNETSampleNative",
            "~/Development/DotNET/beyondnet/Samples/Generated/Generated_C.h".ExpandTildeAndGetAbsolutePath(),
            "~/Development/DotNET/beyondnet/Samples/Generated/Generated_Swift.swift".ExpandTildeAndGetAbsolutePath(),
            "13.0",
            "16.0"
        );
        
        SwiftBuilder swiftBuilder = new(config);
        
        var result = swiftBuilder.Build();
        
        Console.WriteLine($"Built in: {result.OutputRootPath}");
    }
    
    private static void ShowUsage()
    {
        var assemblyInfo = typeof(Program).Assembly.GetName();

        const string productName = "Beyond.NET";
        string assemblyName = assemblyInfo.Name ?? "beyondnetgen";
        
        Version? version = assemblyInfo.Version;
        string versionString = version?.ToString() ?? "N/A";
        
        string usageText = $"""
{productName} Version {versionString}
Usage: {assemblyName} <PathToConfig.json>
""";
        
        Console.WriteLine(usageText);    
    }
}