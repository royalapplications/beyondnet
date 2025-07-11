namespace Beyond.NET.Core;

public struct ConsoleLogger: ILogger
{
    public void LogDebug(string message)
    {
        Log(message, LoggingLevel.Debug);
    }

    public void LogInformation(string message)
    {
        Log(message, LoggingLevel.Information);
    }

    public void LogWarning(string message)
    {
        Log(message, LoggingLevel.Warning);
    }

    public void LogError(string message)
    {
        Log(message, LoggingLevel.Error);
    }

    internal enum LoggingLevel
    {
        Debug,
        Information,
        Warning,
        Error
    }

    private string GetFullMessage(
        string message,
        LoggingLevel loggingLevel
    )
    {
        string prefix = loggingLevel.GetLogMessagePrefix();
        string fullMessage = $"{prefix} {message}";

        return fullMessage;
    }

    private void Log(
        string message,
        LoggingLevel loggingLevel
    )
    {
        string fullMessage = GetFullMessage(
            message,
            loggingLevel
        );

        Console.WriteLine(fullMessage);
    }
}

internal static class LoggingLevelExtensions
{
    internal static string GetLogMessagePrefix(this ConsoleLogger.LoggingLevel loggingLevel)
    {
        switch (loggingLevel) {
            case ConsoleLogger.LoggingLevel.Debug:
                return "[Debug]";
            case ConsoleLogger.LoggingLevel.Information:
                return "[Info]";
            case ConsoleLogger.LoggingLevel.Warning:
                return "[Warn]";
            case ConsoleLogger.LoggingLevel.Error:
                return "[Error]";
            default:
                throw new Exception("Unknown Log Level");
        }
    }
}