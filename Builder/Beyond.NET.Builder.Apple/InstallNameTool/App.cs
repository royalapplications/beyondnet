using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.InstallNameTool;

public class App
{
    private static string InstallNameToolPath => Which.GetAbsoluteCommandPath("install_name_tool");
    private static CLIApp InstallNameToolApp => new(InstallNameToolPath);
    
    private const string ARGUMENT_ID = "-id";

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