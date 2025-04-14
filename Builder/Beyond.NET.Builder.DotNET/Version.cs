namespace Beyond.NET.Builder.DotNET;

public class Version
{
    public static string GetVersion()
    {
        var result = App.DotNETApp.Launch(new[] {
            "--version"
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

    public static string GetMajorAndMinorVersion()
    {
        string version = GetVersion();

        if (string.IsNullOrEmpty(version)) {
            return string.Empty;
        }

        var versionSplit = version.Split(
            '.',
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
        );

        if (versionSplit.Length < 2) {
            return string.Empty;
        }

        string majorVersion = versionSplit[0];
        string minorVersion = versionSplit[1];

        string majorAndMinorVersion = $"{majorVersion}.{minorVersion}";

        return majorAndMinorVersion;
    }
}