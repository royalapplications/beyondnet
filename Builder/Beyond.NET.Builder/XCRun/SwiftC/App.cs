using Beyond.NET.Builder.Helpers;

namespace Beyond.NET.Builder.XCRun.SwiftC;

public class App
{
    public const string ARGUMENT_SDK = "-sdk";
    public const string ARGUMENT_TARGET = "-target";
    public const string ARGUMENT_WORKING_DIRECTORY = "-working-directory";
    public const string ARGUMENT_IMPORT_SEARCH_PATH = "-I";
    public const string ARGUMENT_FRAMEWORK_SEARCH_PATH = "-F";
    public const string FLAG_OPTIMIZATIONS = "-O";
    public const string ARGUMENT_CLANGARG = "-Xcc";
    public const string ARGUMENT_LINKERARG = "-Xlinker";
    public const string FLAG_SAVE_TEMPS = "-save-temps";
    public const string FLAG_PARSE_AS_LIBRARY = "-parse-as-library";
    public const string FLAG_ENABLE_LIBRARY_EVOLUTION = "-enable-library-evolution";
    public const string ARGUMENT_MODULE_NAME = "-module-name";
    public const string FLAG_EMIT_DEPENDENCIES = "-emit-dependencies";
    public const string FLAG_EMIT_MODULE = "-emit-module";
    public const string FLAG_EMIT_OBJC_HEADER = "-emit-objc-header";
    public const string ARGUMENT_EMIT_OBJC_HEADER_PATH = "-emit-objc-header-path";
    public const string ARGUMENT_EMIT_MODULE_PATH = "-emit-module-path";
    public const string ARGUMENT_EMIT_MODULE_INTERFACE_PATH = "-emit-module-interface-path";
    public const string ARGUMENT_OUTPUT = "-o";
    public const string ARGUMENT_SWIFT_VERSION = "-swift-version";
    public const string FLAG_IMPORT_UNDERLYING_MODULE = "-import-underlying-module";
    public const string FLAG_EXPERIMENTAL_EMIT_MODULE_SEPARATELY = "-experimental-emit-module-separately";
    public const string FLAG_DISABLE_CMO = "-disable-cmo";
    public const string ARGUMENT_COMPILE = "-c";
    
    public static CLIApp.Result Run(string[] arguments)
    {
        var result = XCRun.App.XCRunApp.Launch(arguments);

        return result;
    }
}