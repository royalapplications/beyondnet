namespace Beyond.NET.Builder.Xcodebuild;

public class CreateXCFramework
{
    private const string ARGUMENT_CREATE_XCFRAMEWORK = "-create-xcframework";
    private const string ARGUMENT_LIBRARY = "-library";
    private const string ARGUMENT_OUTPUT = "-output";
    
    public static void Run(string[] libraries, string outputPath)
    {
        List<string> arguments = new(new[] {
            ARGUMENT_CREATE_XCFRAMEWORK
        });

        foreach (var library in libraries) {
            if (string.IsNullOrEmpty(library)) {
                continue;
            }
            
            arguments.AddRange(new [] {
                ARGUMENT_LIBRARY,
                library
            });
        }
        
        arguments.AddRange(new [] {
            ARGUMENT_OUTPUT,
            outputPath
        });
        
        var result = App.XcodeBuildApp.Launch(arguments.ToArray());
        
        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw failure;
        }
    }
}