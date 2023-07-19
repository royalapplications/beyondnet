using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.Nm;

public class App
{
    private static string NmPath => Which.GetAbsoluteCommandPath("nm");
    private static CLIApp NmApp => new(NmPath);
    
    private const string FLAG_ONLY_EXTERNAL_SYMBOLS = "-g";
    private const string FLAG_EXCLUDE_UNDEFINED_SYMBOLS = "-U";
    
    public static string GetSymbols(
        string targetPath,
        bool onlyExternalSymbols,
        bool excludeUndefinedSymbols
    )
    {
        List<string> args = new();

        if (onlyExternalSymbols) {
            args.Add(FLAG_ONLY_EXTERNAL_SYMBOLS);
        }
        
        if (excludeUndefinedSymbols) {
            args.Add(FLAG_EXCLUDE_UNDEFINED_SYMBOLS);
        }
        
        args.Add(targetPath);
        
        var result = NmApp.Launch(args.ToArray());
        
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