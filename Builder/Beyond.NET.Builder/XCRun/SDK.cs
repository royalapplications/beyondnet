using System.Diagnostics;

namespace Beyond.NET.Builder.XCRun;

public static class SDK
{
    public const string macOSName = "macosx";
    public const string iOSName = "iphoneos";
    public const string iOSSimulatorName = "iphonesimulator";
    
    public static string GetSDKPath(string name)
    {
        ProcessStartInfo startInfo = new(Constants.XCRunPath, new[] {
            "--sdk", name,
            "--show-sdk-path"
        }) {
            UseShellExecute = false,
            
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        
        using var process = Process.Start(startInfo);

        if (process is null) {
            throw new Exception("Failed to start process");
        }
        
        using StreamReader stdOutReader = process.StandardOutput;
        using StreamReader stdErrReader = process.StandardError;

        var stdOut = stdOutReader.ReadToEnd();
        var stdErr = stdErrReader.ReadToEnd();

        var exitCode = process.ExitCode;

        if (!string.IsNullOrEmpty(stdErr) ||
            exitCode != 0) {
            if (string.IsNullOrEmpty(stdErr)) {
                stdErr = $"Process exited with exit code {exitCode}";
            }

            throw new Exception(stdErr);
        }

        // Trim whitespaces and new lines
        var trimmedStdOut = stdOut.Trim(
            ' ',
            '\n'
        );

        return trimmedStdOut;
    }
}