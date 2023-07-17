namespace Beyond.NET.CodeGenerator.CLI;

static class Program
{
    public static int Main(string[] args)
    {
        // TODO: Just for testing
        // string macOSSDKPath = Builder.XCRun.SDK.GetSDKPath(Builder.XCRun.SDK.macOSName);
        // Console.WriteLine($"macOS SDK Path: {macOSSDKPath}");
        
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