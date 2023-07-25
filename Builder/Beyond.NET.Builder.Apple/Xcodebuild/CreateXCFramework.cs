namespace Beyond.NET.Builder.Apple.Xcodebuild;

public class CreateXCFramework
{
    private const string ARGUMENT_CREATE_XCFRAMEWORK = "-create-xcframework";
    
    private const string ARGUMENT_FRAMEWORK = "-framework";
    private const string ARGUMENT_LIBRARY = "-library";
    private const string ARGUMENT_OUTPUT = "-output";
    
    public static void Run(
        string[]? frameworks,
        string[]? libraries,
        string outputPath
    )
    {
        List<string> arguments = new(new[] {
            ARGUMENT_CREATE_XCFRAMEWORK
        });

        if (frameworks is not null) {
            foreach (var framework in frameworks) {
                if (string.IsNullOrEmpty(framework)) {
                    continue;
                }
            
                arguments.AddRange(new [] {
                    ARGUMENT_FRAMEWORK,
                    framework
                });
            }   
        }
        
        if (libraries is not null) {
            foreach (var library in libraries) {
                if (string.IsNullOrEmpty(library)) {
                    continue;
                }
            
                arguments.AddRange(new [] {
                    ARGUMENT_LIBRARY,
                    library
                });
            }   
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