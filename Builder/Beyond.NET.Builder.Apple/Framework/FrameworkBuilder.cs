using Beyond.NET.Builder.Apple.XCRun.SwiftC;
using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.Framework;

public record FrameworkBuilder
(
    string FrameworkName,
    string FrameworkBundleIdentifier,
    string LibraryFilePath,
    string OutputDirectoryPath,
    
    bool BuildForMacOS,
    
    string? ModuleMapFilePath,
    string? SwiftModuleDirectoryPath
)
{
	public record Result(string FrameworkOutputDirectoryPath);
	
    private static ILogger Logger => Services.Shared.LoggerService;
    
    public Result Build()
    {
	    string bundleName = $"{FrameworkName}.framework"; 
        string outputBundlePath = Path.Combine(OutputDirectoryPath, bundleName);
        string libraryName = FrameworkName;

	    Logger.LogDebug($"Creating framework \"{FrameworkName}\" at \"{outputBundlePath}\"");
	    
        const string versionsDirName = "Versions";
        const string versionsADirName = "A";
        const string versionsCurrentDirName = "Current";

        const string resourcesDirName = "Resources";
        const string modulesDirName = "Modules";

        const string infoPlistFileName = "Info.plist";
        const string moduleMapFileName = "module.modulemap";
        string swiftModuleDirName = $"{FrameworkName}.{FileExtensions.SwiftModule}";
        
        // Only relevant for macOS
        string versionsDirectoryPath = Path.Combine(outputBundlePath, versionsDirName);
        
        // Only relevant for macOS
        string versionsADirectoryPath = Path.Combine(versionsDirectoryPath, versionsADirName);
        
        // Only relevant for macOS
        string versionsCurrentDirectoryPath = Path.Combine(versionsDirectoryPath, versionsCurrentDirName);
        
        string modulesDirectoryPath;

        if (BuildForMacOS) {
			modulesDirectoryPath = Path.Combine(versionsADirectoryPath, modulesDirName);
        } else {
	        modulesDirectoryPath = Path.Combine(outputBundlePath, modulesDirName);
        }
        
        // Only relevant for macOS
        string resourcesDirectoryPath = Path.Combine(versionsADirectoryPath, resourcesDirName);

        string infoPlistFilePath;

        if (BuildForMacOS) {
			infoPlistFilePath = Path.Combine(resourcesDirectoryPath, infoPlistFileName);
        } else {
	        infoPlistFilePath = Path.Combine(outputBundlePath, infoPlistFileName);
        }

        // Only relevant for macOS
        string modulesLinkDirectoryPath = Path.Combine(outputBundlePath, modulesDirName);
        
        // Only relevant for macOS
        string resourcesLinkDirectoryPath = Path.Combine(outputBundlePath, resourcesDirName);

        string frameworkLibraryFilePath;

        if (BuildForMacOS) {
			frameworkLibraryFilePath = Path.Combine(versionsADirectoryPath, libraryName);
        } else {
	        frameworkLibraryFilePath = Path.Combine(outputBundlePath, libraryName);
        }
        
        // Only relevant for macOS
        string frameworkLibraryFileLinkPath = Path.Combine(outputBundlePath, libraryName);

        string frameworkModuleMapFilePath = Path.Combine(modulesDirectoryPath, moduleMapFileName);
        string frameworkSwiftModuleDirectoryPath = Path.Combine(modulesDirectoryPath, swiftModuleDirName);

        string newFrameworkLibraryID;

        if (BuildForMacOS) {
	        newFrameworkLibraryID = $"@rpath/{bundleName}/Versions/A/{libraryName}";
        } else {
			newFrameworkLibraryID = $"@rpath/{bundleName}/{libraryName}";
        }

        Logger.LogDebug("Creating Framework Directory Structure");
        
        Directory.CreateDirectory(outputBundlePath);
        
        if (BuildForMacOS) {
	        Directory.CreateDirectory(versionsDirectoryPath);
	        Directory.CreateDirectory(versionsADirectoryPath);
	        Directory.CreateDirectory(modulesDirectoryPath);
	        Directory.CreateDirectory(resourcesDirectoryPath);

	        Directory.CreateSymbolicLink(versionsCurrentDirectoryPath, versionsADirName);
	        Directory.CreateSymbolicLink(modulesLinkDirectoryPath, $"{versionsDirName}/{versionsCurrentDirName}/{modulesDirName}");
	        Directory.CreateSymbolicLink(resourcesLinkDirectoryPath, $"{versionsDirName}/{versionsCurrentDirName}/{resourcesDirName}");   
        } else { // iOS-like
	        Directory.CreateDirectory(modulesDirectoryPath);
        }

        Logger.LogDebug("Creating Framework Info.plist");
        string infoPlistContent = GetInfoPlistContent();
        File.WriteAllText(infoPlistFilePath, infoPlistContent);

        if (!string.IsNullOrEmpty(ModuleMapFilePath)) {
			Logger.LogDebug("Copying Framework modulemap");
	        File.Copy(ModuleMapFilePath, frameworkModuleMapFilePath);
        }

        if (!string.IsNullOrEmpty(SwiftModuleDirectoryPath)) {
	        Logger.LogDebug("Copying Framework Swift module");
	        FileSystemUtils.CopyDirectoryContents(
		        SwiftModuleDirectoryPath,
		        frameworkSwiftModuleDirectoryPath,
		        true
		    );
        }
        
        Logger.LogDebug("Copying Framework library");
        File.Copy(LibraryFilePath, frameworkLibraryFilePath);

        if (BuildForMacOS) {
			File.CreateSymbolicLink(frameworkLibraryFileLinkPath, $"{versionsDirName}/{versionsCurrentDirName}/{libraryName}");
        }
        
        Logger.LogDebug($"Changing Framework library ID to \"{newFrameworkLibraryID}\"");
        
        InstallNameTool.App.ChangeId(
	        frameworkLibraryFilePath,
	        newFrameworkLibraryID
	    );

        return new(outputBundlePath);
    }

    private string GetInfoPlistContent()
    {
	    string extendedTemplate = INFO_PLIST_TEMPLATE
		    .Replace(TOKEN_FRAMEWORK_NAME, FrameworkName)
		    .Replace(TOKEN_BUNDLE_IDENTIFIER, FrameworkBundleIdentifier);

	    return extendedTemplate;
    }

    private const string TOKEN = "$__BEYOND_TOKEN__$";
    private const string TOKEN_FRAMEWORK_NAME = $"{TOKEN}FrameworkName{TOKEN}";
    private const string TOKEN_BUNDLE_IDENTIFIER = $"{TOKEN}BundleIdentifier{TOKEN}";

    private const string INFO_PLIST_TEMPLATE = $"""
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
	<key>CFBundleExecutable</key>
	<string>{TOKEN_FRAMEWORK_NAME}</string>
	<key>CFBundleInfoDictionaryVersion</key>
	<string>6.0</string>
	<key>CFBundleName</key>
	<string>{TOKEN_FRAMEWORK_NAME}</string>
	<key>CFBundleIdentifier</key>
	<string>{TOKEN_BUNDLE_IDENTIFIER}</string>
</dict>
</plist>
""";
}