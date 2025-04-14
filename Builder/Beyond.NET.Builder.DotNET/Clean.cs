namespace Beyond.NET.Builder.DotNET;

public class Clean
{
    private const string FLAG_CLEAN = "clean";

    public static string Run(
        string workingDirectory,
        string? configuration
    )
    {
        List<string> args = new(new [] {
            FLAG_CLEAN
        });

        if (!string.IsNullOrEmpty(configuration)) {
            string configArg = $"/p:Configuration={configuration}";

            args.Add(configArg);
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