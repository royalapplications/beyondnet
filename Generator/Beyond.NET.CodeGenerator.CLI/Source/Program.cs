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
        string productName = "BeyondDotNETSampleNative";
        
        string targetCsProjFilePath = "~/Development/DotNET/beyondnet/Samples/Beyond.NET.Sample.Managed/Beyond.NET.Sample.Managed.csproj"
            .ExpandTildeAndGetAbsolutePath();
        
        string generatedCSharpFilePath = "~/Development/DotNET/beyondnet/Samples/Generated/Generated_CS.cs"
            .ExpandTildeAndGetAbsolutePath();
        
        string generatedCHeaderFilePath = "~/Development/DotNET/beyondnet/Samples/Generated/Generated_C.h"
            .ExpandTildeAndGetAbsolutePath();
        
        string generatedSwiftFilePath = "~/Development/DotNET/beyondnet/Samples/Generated/Generated_Swift.swift"
            .ExpandTildeAndGetAbsolutePath();
        
        string macOSDeploymentTarget = "13.0";
        string iOSDeploymentTarget = "16.0";
        
        SwiftBuilder.BuilderConfiguration config = new(
            productName,
            generatedCHeaderFilePath,
            generatedSwiftFilePath,
            macOSDeploymentTarget,
            iOSDeploymentTarget
        );
        
        Console.WriteLine("Building Swift bindings...");
        
        SwiftBuilder swiftBuilder = new(config);
        
        var swiftBuildResult = swiftBuilder.Build();
        
        Console.WriteLine($"Swift bindings built in: {swiftBuildResult.OutputRootPath}");

        string dnVersion = Builder.DotNET.Version.GetMajorAndMinorVersion();
        string targetFramework = $"net{dnVersion}";
        
        Console.WriteLine("Building .NET Native stuff...");

        var dnNativeBuilder = new DotNETNativeBuilder(
            targetFramework,
            productName,
            targetCsProjFilePath,
            generatedCSharpFilePath,
            swiftBuildResult
        );
        
        var result = dnNativeBuilder.Build();
        
        Console.WriteLine($"Final product built in: {result.OutputDirectoryPath}");
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