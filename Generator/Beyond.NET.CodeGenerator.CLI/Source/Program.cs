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
        Console.WriteLine($"Major and Minor .NET Version: {Builder.DotNET.Version.GetMajorAndMinorVersion()}");
        
        Console.WriteLine($"Target Triple: {Builder.Apple.XCRun.SwiftC.TargetTriple.Make(
            Builder.Apple.XCRun.SwiftC.TargetIdentifier.ARM64,
            Builder.Apple.XCRun.SwiftC.PlatformIdentifier.iOS,
            "16.0",
            Builder.Apple.XCRun.SwiftC.PlatformIdentifier.iOSSimulatorSuffix
        )}");
        
        string macOSSDKPath = Builder.Apple.XCRun.SDK.GetSDKPath(Builder.Apple.XCRun.SDK.macOSName);
        Console.WriteLine($"macOS SDK Path: {macOSSDKPath}");

        Builder.Apple.Clang.VFSOverlay.HeaderFile headerFile = new() {
            Version = 0,
            CaseSensitive = false,
            Roots = new [] {
                new Builder.Apple.Clang.VFSOverlay.HeaderFileRoot() {
                    Name = ".",
                    Type = "directory",
                    Contents = new [] {
                        new Builder.Apple.Clang.VFSOverlay.HeaderFileContent() {
                            Name = "Generated_C.h",
                            Type = "file",
                            ExternalContents = "Generated_C.h"
                        },
                        new Builder.Apple.Clang.VFSOverlay.HeaderFileContent() {
                            Name = "module.modulemap",
                            Type = "file",
                            ExternalContents = "module.modulemap"
                        }
                    }
                }
            }
        };

        var serializer = new Builder.Apple.Clang.VFSOverlay.HeaderFileSerializer();
        string json = serializer.SerializeToJson(headerFile);
        
        Console.WriteLine($"Clang VFS Overlay Header file:\n{json}");

        var moduleMap = new Builder.Apple.Clang.ModuleMap("BeyondDotNETSampleNative") {
            Headers = new [] {
                new Builder.Apple.Clang.ModuleMap.Header("Generated_C.h") {
                    Type = Builder.Apple.Clang.ModuleMap.Header.Types.Private
                }
            }
        };
        
        Console.WriteLine($"Clang Module Map:\n{moduleMap}");
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