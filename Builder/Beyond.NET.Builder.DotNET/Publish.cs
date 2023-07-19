namespace Beyond.NET.Builder.DotNET;

public class Publish
{
    private const string FLAG_PUBLISH = "publish";
    private const string ARGUMENT_RUNTIME_IDENTIFIER = "-r";
    private const string ARGUMENT_VERBOSITY_LEVEL = "-v";

    public const string VERBOSITY_LEVEL_NORMAL = "normal";
    
    public const string RUNTIME_IDENTIFIER_MACOS_X64 = "osx-x64";
    public const string RUNTIME_IDENTIFIER_MACOS_ARM64 = "osx-arm64";

    public const string RUNTIME_IDENTIFIER_IOS_ARM64 = "ios-arm64";
    public const string RUNTIME_IDENTIFIER_IOS_SIMULATOR_ARM64 = "iossimulator-arm64";
    public const string RUNTIME_IDENTIFIER_IOS_SIMULATOR_X64 = "iossimulator-x64";
    
    public static string Run(
        string workingDirectory,
        string runtimeIdentifier,
        string? verbosityLevel,
        string[]? additionalArguments
    )
    {
        List<string> args = new(new [] {
            FLAG_PUBLISH
        });
        
        args.AddRange(new [] {
            ARGUMENT_RUNTIME_IDENTIFIER,
            runtimeIdentifier
        });

        if (!string.IsNullOrEmpty(verbosityLevel)) {
            args.AddRange(new [] {
                ARGUMENT_VERBOSITY_LEVEL,
                verbosityLevel
            });
        }

        if (additionalArguments is not null &&
            additionalArguments.Length > 0) {
            args.AddRange(additionalArguments);
        }

        var result = App.DotNETApp.Launch(
            args.ToArray(),
            workingDirectory
        );
        
        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw failure;
        }

        return result.StandardOut ?? string.Empty;
    }
}