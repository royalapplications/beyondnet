using System.Diagnostics;
using System.Text;

namespace Beyond.NET.Core;

public class CLIApp
{
    public class Result
    {
        public string Invocation { get; init; }
        public int ExitCode { get; init; }
        public Exception? LaunchException { get; init; }
        
        public string? StandardOut { get; init; }
        public string? StandardError { get; init; }

        public Exception? FailureAsException
        {
            get {
                if (LaunchException is not null) {
                    return LaunchException;
                }

                if (!string.IsNullOrEmpty(StandardError)) {
                    return new Exception(StandardError);
                }

                if (ExitCode != 0) {
                    return new Exception($"Process exited with exit code {ExitCode}");
                }

                return null;
            }
        }

        public Result(
            string invocation,
            int exitCode
        )
        {
            Invocation = invocation;
            ExitCode = exitCode;
        }
        
        public Result(
            string invocation,
            int exitCode,
            string? standardOut,
            string? standardError
        )
        {
            Invocation = invocation;
            ExitCode = exitCode;
            StandardOut = standardOut;
            StandardError = standardError;
        }
        
        public Result(
            string invocation,
            Exception launchException
        )
        {
            Invocation = invocation;
            ExitCode = 1;
            LaunchException = launchException;
        }
    }
    
    public string Command { get; init; }
    public bool TreatNonZeroExitCodeAsGenericError { get; init; } = true;

    public CLIApp(string command)
    {
        Command = command;
    }

    public Result Launch(string[]? arguments)
    {
        string invocationString = MakeInvocationString(arguments);
        var startInfo = MakeStartInfo(arguments);
        
        try {
            using var process = Process.Start(startInfo);

            if (process is null) {
                throw new Exception("An unknown error occurred while starting the process");
            }
        
            using StreamReader stdOutReader = process.StandardOutput;
            using StreamReader stdErrReader = process.StandardError;

            process.WaitForExit();
            
            var standardOut = stdOutReader.ReadToEnd();
            var standardError = stdErrReader.ReadToEnd();

            var exitCode = process.ExitCode;

            if (TreatNonZeroExitCodeAsGenericError &&
                exitCode != 0 &&
                string.IsNullOrEmpty(standardError)) {
                standardError = $"Process exited with exit code {exitCode}";
            }

            return new(
                invocationString,
                exitCode,
                standardOut,
                standardError
            );
        } catch (Exception ex) {
            return new(
                invocationString,
                ex
            );
        }
    }

    private string MakeInvocationString(string[]? arguments)
    {
        arguments ??= Array.Empty<string>();

        StringBuilder sb = new(Command);

        if (arguments.Length > 0) {
            foreach (var argument in arguments) {
                sb.Append($" {argument}");
            }
        }

        string invocationString = sb.ToString();

        return invocationString;
    }

    private ProcessStartInfo MakeStartInfo(string[]? arguments)
    {
        ProcessStartInfo startInfo = new(Command, arguments ?? Array.Empty<string>()) {
            UseShellExecute = false,
            
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        return startInfo;
    }
}