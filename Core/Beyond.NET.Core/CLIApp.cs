using System.Diagnostics;
using System.Text;

namespace Beyond.NET.Core;

public class CLIApp
{
    public class Result
    {
        public string Invocation { get; }
        public string? WorkingDirectory { get; }
        public int ExitCode { get; }
        public Exception? LaunchException { get; init; }
        
        public string? StandardOut { get; init; }
        public string? StandardError { get; init; }

        public Exception? FailureAsException
        {
            get {
                Exception innerException;
                
                if (LaunchException is not null) {
                    innerException = LaunchException;
                } else if (!string.IsNullOrEmpty(StandardError)) {
                    innerException = new Exception(StandardError);
                } else if (ExitCode != 0) {
                    if (!string.IsNullOrEmpty(StandardOut)) {
                        innerException = new Exception(StandardOut);    
                    } else {
                        innerException = new Exception("Unknown Error");
                    }
                } else {
                    return null;
                }

                string msg;

                if (string.IsNullOrEmpty(WorkingDirectory)) {
                    msg = $"The Command \"{Invocation}\" exited with exit code {ExitCode}";
                } else {
                    msg = $"The Command \"{Invocation}\" in directory \"{WorkingDirectory}\" exited with exit code {ExitCode}";
                }

                var ex = new Exception(
                    msg, 
                    innerException
                );

                return ex;
            }
        }

        public Result(
            string invocation,
            string? workingDirectory,
            int exitCode
        )
        {
            Invocation = invocation;
            WorkingDirectory = workingDirectory;
            ExitCode = exitCode;
        }
        
        public Result(
            string invocation,
            string? workingDirectory,
            int exitCode,
            string? standardOut,
            string? standardError
        )
        {
            Invocation = invocation;
            WorkingDirectory = workingDirectory;
            ExitCode = exitCode;
            StandardOut = standardOut;
            StandardError = standardError;
        }
        
        public Result(
            string invocation,
            string? workingDirectory,
            Exception launchException
        )
        {
            Invocation = invocation;
            WorkingDirectory = workingDirectory;
            ExitCode = 1;
            LaunchException = launchException;
        }
    }

    private ILogger Logger => Services.Shared.LoggerService;
    
    public string Command { get; init; }

    public CLIApp(string command)
    {
        Command = command;
    }
    
    public Result Launch(
        string[]? arguments,
        string? workingDirectory = null
    )
    {
        int timeout = int.MaxValue;
        
        var startInfo = MakeStartInfo(
            arguments,
            workingDirectory,
            out string invocationString
        );
        
        string logMsg;

        if (!string.IsNullOrEmpty(workingDirectory)) {
            logMsg = $"Running command \"{invocationString}\" in directory \"{workingDirectory}\"";
        } else {
            logMsg = $"Running command \"{invocationString}\"";
        }
        
        Logger.LogDebug(logMsg);

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

            if (timeoutException is not null) {
                stdErr = timeoutException.ToString();
            }

            return new(
                invocationString,
                workingDirectory,
                exitCode,
                stdOut,
                stdErr
            );
        } catch (Exception ex) {
            return new(
                invocationString,
                workingDirectory,
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
        string? workingDirectory,
        out string invocationString
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

        invocationString = MakeInvocationString(arguments);

        return startInfo;
    }
}