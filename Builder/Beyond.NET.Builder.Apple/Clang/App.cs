using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.Clang;

public static class App
{
    private static string ClangPath => Which.GetAbsoluteCommandPath("clang");
    internal static CLIApp ClangApp => new(ClangPath);

    public const string ARGUMENT_TARGET = "-target";
    public const string ARGUMENT_SYSROOT = "-isysroot";

    public const string ARGUMENT_OUTPUT = "-o";
    public const string ARGUMENT_COMPILE = "-c";

    public const string FLAG_OPTIMIZE3 = "-O3";

    public static string Run(
        string workingDirectory,
        string[] arguments
    )
    {
        var result = ClangApp.Launch(
            arguments,
            workingDirectory
        );

        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw failure;
        }

        return result.StandardOut ?? string.Empty;
    }

    public static string Compile(
        string workingDirectory,
        string target,
        string sysRoot,
        bool optimize3,
        string targetFile,
        string outputFile
    )
    {
        List<string> arguments = new() {
            ARGUMENT_TARGET, target,
            ARGUMENT_SYSROOT, sysRoot,
            ARGUMENT_OUTPUT, outputFile
        };

        if (optimize3) {
            arguments.Add(FLAG_OPTIMIZE3);
        }

        arguments.AddRange(new [] {
            ARGUMENT_COMPILE,
            targetFile
        });

        return Run(
            workingDirectory,
            arguments.ToArray()
        );
    }
}