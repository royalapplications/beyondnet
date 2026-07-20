using Beyond.NET.Core;

namespace Beyond.NET.Builder.Android;

public static class AndroidPublish
{
    private const string BUILD_SCRIPT_NAME = "build_android.sh";

    public static string Run(
        string workingDirectory,
        string runtimeIdentifier,
        string configuration,
        string? verbosityLevel
    )
    {
        // Get the path to the build script
        string scriptDirectory = Path.GetDirectoryName(typeof(AndroidPublish).Assembly.Location)!;
        string scriptPath = Path.Combine(scriptDirectory, BUILD_SCRIPT_NAME);

        if (!File.Exists(scriptPath))
        {
            throw new FileNotFoundException($"Android build script not found at: {scriptPath}");
        }

        // Build arguments for the script
        List<string> args = new()
        {
            scriptPath,
            workingDirectory,
            runtimeIdentifier,
            configuration
        };

        if (!string.IsNullOrEmpty(verbosityLevel))
        {
            args.Add(verbosityLevel);
        }

        // Execute the build script using bash
        var bashApp = new CLIApp("/bin/bash");
        var result = bashApp.Launch(
            args.ToArray(),
            workingDirectory
        );

        Exception? failure = result.FailureAsException;

        if (failure is not null)
        {
            throw failure;
        }

        return result.StandardOut ?? string.Empty;
    }
}

