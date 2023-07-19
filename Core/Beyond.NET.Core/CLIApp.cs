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
        return Launch(
            arguments,
            null
        );
    }
    
    public Result Launch(
        string[]? arguments,
        string? workingDirectory
    )
    {
        int timeout = int.MaxValue;
        
        string invocationString = MakeInvocationString(arguments);
        
        var startInfo = MakeStartInfo(
            arguments,
            workingDirectory
        );

        try {
            using var process = new Process() {
                StartInfo = startInfo
            };

            if (process is null) {
                throw new Exception("An unknown error occurred while starting the process");
            }

            StringBuilder sbStdOut = new();
            StringBuilder sbStdErr = new();

            using var stdOutWaitHandle = new AutoResetEvent(false);
            using var stdErrWaitHandle = new AutoResetEvent(false);
            
            process.OutputDataReceived += (_, e) => {
                if (e.Data is null) {
                    stdOutWaitHandle.Set();
                } else {
                    sbStdOut.AppendLine(e.Data);
                }
            };

            process.ErrorDataReceived += (_, e) => {
                if (e.Data is null) {
                    stdErrWaitHandle.Set();
                } else {
                    sbStdErr.AppendLine(e.Data);
                }
            };

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            Exception? timeoutException = null;
            int exitCode = -1;

            if (process.WaitForExit(timeout) &&
                stdOutWaitHandle.WaitOne(timeout) &&
                stdErrWaitHandle.WaitOne(timeout)) {
                exitCode = process.ExitCode;
            } else {
                timeoutException = new TimeoutException();
            }

            string stdOut = sbStdOut.ToString();
            string stdErr = sbStdErr.ToString();
            
            if (timeoutException is null &&
                TreatNonZeroExitCodeAsGenericError &&
                exitCode != 0 &&
                string.IsNullOrEmpty(stdErr)) {
                stdErr = $"Process exited with exit code {exitCode}";
            }

            return new(
                invocationString,
                exitCode,
                stdOut,
                stdErr
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

    private ProcessStartInfo MakeStartInfo(
        string[]? arguments,
        string? workingDirectory
    )
    {
        ProcessStartInfo startInfo = new(Command, arguments ?? Array.Empty<string>()) {
            UseShellExecute = false,
            CreateNoWindow = true,

            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        if (!string.IsNullOrEmpty(workingDirectory)) {
            startInfo.WorkingDirectory = workingDirectory;
        }

        return startInfo;
    }
}