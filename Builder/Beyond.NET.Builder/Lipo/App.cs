using Beyond.NET.Builder.Helpers;

namespace Beyond.NET.Builder.Lipo;

public class App
{
    private const string LipoPath = "/usr/bin/lipo";
    internal static readonly CLIApp LipoApp = new(LipoPath);
    
    private const string ARGUMENT_CREATE = "-create";
    private const string ARGUMENT_OUTPUT = "-output";
    
    public static void Create(
        string[] libraryPaths,
        string outputPath
    )
    {
        List<string> args = new(new [] {
            ARGUMENT_CREATE
        });
        
        args.AddRange(libraryPaths);
        
        args.AddRange(new [] {
            ARGUMENT_OUTPUT,
            outputPath
        });

        var result = LipoApp.Launch(args.ToArray());
        
        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw failure;
        }
    }
}