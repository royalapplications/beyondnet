namespace Beyond.NET.Builder.Apple.XCRun;

public static class SDK
{
    public const string macOSName = "macosx";
    public const string iOSName = "iphoneos";
    public const string iOSSimulatorName = "iphonesimulator";

    public static string GetSDKPath(string name)
    {
        var result = App.XCRunApp.Launch(new[] {
            "--sdk", name,
            "--show-sdk-path"
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