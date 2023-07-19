using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.InstallNameTool;

public class App
{
    private const string InstallNameToolPath = "/usr/bin/install_name_tool";
    private const string ARGUMENT_ID = "-id";
    
    internal static readonly CLIApp InstallNameToolApp = new(InstallNameToolPath);

    public static void ChangeId(
        string targetPath,
        string newId
    )
    {
        string[] args = new[] {
            ARGUMENT_ID,
            newId,
            targetPath
        };

        var result = InstallNameToolApp.Launch(args);
        
        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw failure;
        }
    }
}