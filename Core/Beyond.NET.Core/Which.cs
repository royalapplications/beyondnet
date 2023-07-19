namespace Beyond.NET.Core;

public class Which
{
    private static readonly CLIApp WhichApp = new("which");

    public static string GetAbsoluteCommandPath(string command)
    {
        var result = WhichApp.Launch(new[] {
            command
        });
        
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