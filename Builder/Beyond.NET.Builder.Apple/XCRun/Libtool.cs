namespace Beyond.NET.Builder.Apple.XCRun;

public static class Libtool
{
    public static string StaticMerge(
        string workingDirectory,
        string[] inputFiles,
        string outputFile,
        bool noWarningForNoSymbols
    )
    {
        List<string> arguments = new() {
            "libtool",
            "-static",
            "-o", outputFile
        };

        foreach (var inputFile in inputFiles) {
            arguments.Add(inputFile);
        }

        if (noWarningForNoSymbols) {
            arguments.Add("-no_warning_for_no_symbols");
        }

        var result = App.XCRunApp.Launch(
            arguments.ToArray(),
            workingDirectory
        );

        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw failure;
        }

        // Trim whitespaces and new lines
        var trimmedStandardOut = result.StandardOut?.Trim(
            ' ',
            '\n'
        ) ?? string.Empty;

        return trimmedStandardOut;
    }
}