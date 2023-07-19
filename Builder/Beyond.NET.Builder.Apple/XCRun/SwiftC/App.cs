namespace Beyond.NET.Builder.Apple.XCRun.SwiftC;

public class App
{
    private const string COMMAND_SWIFTC = "swiftc";
    
    public const string ARGUMENT_SDK = "-sdk";
    public const string ARGUMENT_TARGET = "-target";
    public const string ARGUMENT_WORKING_DIRECTORY = "-working-directory";
    public const string ARGUMENT_IMPORT_SEARCH_PATH = "-I";
    public const string ARGUMENT_FRAMEWORK_SEARCH_PATH = "-F";
    public const string FLAG_OPTIMIZATIONS = "-O";
    public const string ARGUMENT_VFSOVERLAY = "-vfsoverlay";
    public const string FLAG_SAVE_TEMPS = "-save-temps";
    public const string FLAG_PARSE_AS_LIBRARY = "-parse-as-library";
    public const string FLAG_ENABLE_TESTING = "-enable-testing";
    public const string FLAG_ENABLE_LIBRARY_EVOLUTION = "-enable-library-evolution";
    public const string FLAG_ENABLE_BARE_SLASH_REGEX = "-enable-bare-slash-regex";
    public const string ARGUMENT_MODULE_NAME = "-module-name";
    public const string ARGUMENT_MODULE_LINK_NAME = "-module-link-name";
    public const string FLAG_STATIC = "-static";
    public const string FLAG_EMIT_DEPENDENCIES = "-emit-dependencies";
    public const string FLAG_EMIT_MODULE = "-emit-module";
    public const string FLAG_EMIT_LIBRARY = "-emit-library";
    public const string FLAG_EMIT_OBJC_HEADER = "-emit-objc-header";
    public const string ARGUMENT_EMIT_OBJC_HEADER_PATH = "-emit-objc-header-path";
    public const string ARGUMENT_EMIT_MODULE_PATH = "-emit-module-path";
    public const string ARGUMENT_EMIT_MODULE_INTERFACE_PATH = "-emit-module-interface-path";
    public const string ARGUMENT_OUTPUT = "-o";
    public const string ARGUMENT_SWIFT_VERSION = "-swift-version";
    public const string FLAG_IMPORT_UNDERLYING_MODULE = "-import-underlying-module";
    public const string FLAG_EXPERIMENTAL_EMIT_MODULE_SEPARATELY = "-experimental-emit-module-separately";
    public const string FLAG_DISABLE_CROSS_MODULE_OPTIMIZATION = "-disable-cmo";
    public const string ARGUMENT_COMPILE = "-c";
    
    public static string Run(
        string workingDirectory,
        string[] arguments
    )
    {
        List<string> finalArguments = new(new[] {
            COMMAND_SWIFTC
        });
        
        finalArguments.AddRange(arguments);
        
        var result = XCRun.App.XCRunApp.Launch(
            finalArguments.ToArray(),
            workingDirectory
        );

        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw failure;
        }

        return result.StandardOut ?? string.Empty;
    }
}