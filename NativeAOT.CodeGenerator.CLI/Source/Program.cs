namespace NativeAOT.CodeGenerator.CLI;

static class Program
{
    public static int Main(string[] args)
    {
        string? configFilePath = args.Length == 1
            ? args[0]
            : null;

        if (string.IsNullOrEmpty(configFilePath)) {
            ShowUsage();

            return 1;
        }

        ConfigurationSerializer serializer = new();
        Configuration configuration = serializer.DeserializeFromJsonFilePath(configFilePath) ?? throw new Exception("Error while parsing configuration file.");

        CodeGeneratorDriver driver = new(configuration);
        
        driver.Generate();

        return 0;
    }
    
    private static void ShowUsage()
    {
        string usageText = """
Usage: NativeAOT.CodeGenerator.CLI <PathToConfig.json>
""";
        
        Console.WriteLine(usageText);    
    }
}