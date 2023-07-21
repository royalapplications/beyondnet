namespace Beyond.NET.Core;

public interface ILogger
{
    void LogDebug(string message);
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message);
}