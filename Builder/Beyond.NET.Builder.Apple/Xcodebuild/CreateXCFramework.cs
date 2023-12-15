using Beyond.NET.Core;

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
                    Temp_XcodeBuild_15_ResolveVarSymlink(framework)
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
                    Temp_XcodeBuild_15_ResolveVarSymlink(library)
                });
            }   
        }

        arguments.AddRange(new [] {
            ARGUMENT_OUTPUT,
            Temp_XcodeBuild_15_ResolveVarSymlink(outputPath)
        });
        
        var result = App.XcodeBuildApp.Launch(arguments.ToArray());
        
        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw failure;
        }
    }

    // `xcodebuild` as of version 15 fails with such a command line:
    //
    // ```shell
    // /usr/bin/xcodebuild -create-xcframework\
    //   -framework /var/folders/<elided>/osx-universal/publish/SampleKit.framework\
    //   -framework /var/folders/<elided>/iossimulator-universal/publish/SampleKit.framework\
    //   -framework /var/folders/<elided>/ios-arm64/publish/SampleKit.framework\
    //   -output /var/folders/<elided>/apple-universal/publish/SampleKit.xcframework
    // ```
    // 
    // It returns exit code 70 and prints an error message similar to:
    //
    // ```
    // error: cannot compute path of binary
    //  'Path(str: "/private/var/folders/<elided>/osx-universal/publish/SampleKit.framework/Versions/A/SampleKit")'
    // relative to that of '/var/folders/<elided>/osx-universal/publish/SampleKit.framework'
    // ```
    // 
    // Note the mismatch between `/private/var` and `/var` -- on OSX,
    // `/var` is a symlink to `/private/var`.
    //
    // `xcodebuild` v15 seems to resolve symlinks for some arguments but not 
    // others and ends up confused. Unfortunately, .NET APIs do not resolve
    // symlinks at the *start* of a path, only for the *last* component. 
    //
    // Hence, this helper resolves the symlink in the initial `/var`
    // path segment explicitly, as a workaround until `xcodebuild` is
    // updated to work correctly with symlinked paths again.
   
    private static string Temp_XcodeBuild_15_ResolveVarSymlink(string path)
    {
        if (string.IsNullOrEmpty(path)) {
            return path;
        }
        
        // Console.WriteLine($"ORIGINAL PATH: {path}");

        var resolvedPath = path.ResolveSymlinksAndGetCanonicalPath_AppleOnly();

        if (string.IsNullOrEmpty(resolvedPath)) {
            return path;
        }
        
        // Console.WriteLine($"RESOLVED PATH: {resolvedPath}");

        return resolvedPath;
    }
    
    // private static string Temp_XcodeBuild_15_ResolveVarSymlink(string path)
    // {
    //     if (string.IsNullOrEmpty(path)) {
    //         return path;
    //     }
    //     
    //     Console.WriteLine($"ORIGINAL PATH: {path}");
    //     
    //     if (!path.StartsWith("/var/", StringComparison.Ordinal)) {
    //         return path;
    //     }
    //     
    //     FileSystemInfo? linkTarget = Directory.ResolveLinkTarget("/var", returnFinalTarget: true);
    //     
    //     if (linkTarget is not null) {
    //         var resolvedPath = linkTarget.FullName + path.Substring("/var".Length);
    //          
    //         Console.WriteLine($"RESOLVED PATH: {resolvedPath}");
    //         
    //         return resolvedPath;
    //     }
    //     
    //     return path;
    // }
}