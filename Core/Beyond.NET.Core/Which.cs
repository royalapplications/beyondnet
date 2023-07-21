namespace Beyond.NET.Core;

public class Which
{
    private static readonly CLIApp WhichApp = new("which");

    private static Dictionary<string, string> m_cache = new(); 

    public static string GetAbsoluteCommandPath(string command)
    {
        if (m_cache.TryGetValue(command, out string? cachedResult)) {
            return cachedResult;
        }
        
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

        if (!string.IsNullOrEmpty(trimmedStandardOut)) {
            m_cache[command] = trimmedStandardOut;
        }

        return trimmedStandardOut;
    }
}