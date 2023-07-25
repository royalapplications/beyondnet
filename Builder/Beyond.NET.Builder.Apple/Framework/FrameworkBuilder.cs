using Beyond.NET.Core;

namespace Beyond.NET.Builder.Apple.Framework;

public record FrameworkBuilder
(
    string FrameworkName,
    string LibraryFilePath,
    string OutputDirectoryPath,
    
    string? ModuleMapFilePath,
    string? SwiftModuleDirectoryPath
)
{
    private static ILogger Logger => Services.Shared.LoggerService;
    
    public void Build()
    {
        string bundleName = $"{FrameworkName}.framework"; 
        string outputBundlePath = Path.Combine(OutputDirectoryPath, bundleName);
        string libraryName = FrameworkName;

        const string versionsDirName = "Versions";
        const string versionsADirName = "A";
        const string versionsCurrentDirName = "Current";

        const string resourcesDirName = "Resources";
        const string modulesDirName = "Modules";

        const string infoPlistFileName = "Info.plist";
        const string moduleMapFileName = "module.modulemap";
        
        string versionsDirectoryPath = Path.Combine(outputBundlePath, versionsDirName);
        string versionsADirectoryPath = Path.Combine(versionsDirectoryPath, versionsADirName);
        string versionsCurrentDirectoryPath = Path.Combine(versionsDirectoryPath, versionsCurrentDirName);
        
        string modulesDirectoryPath = Path.Combine(versionsADirectoryPath, modulesDirName);
        string resourcesDirectoryPath = Path.Combine(versionsADirectoryPath, resourcesDirName);
        string infoPlistFilePath = Path.Combine(resourcesDirectoryPath, infoPlistFileName);

        string modulesLinkDirectoryPath = Path.Combine(outputBundlePath, modulesDirName);
        string resourcesLinkDirectoryPath = Path.Combine(outputBundlePath, resourcesDirName);

        string frameworkLibraryFilePath = Path.Combine(versionsADirectoryPath, libraryName);
        string frameworkLibraryFileLinkPath = Path.Combine(outputBundlePath, libraryName);

        string frameworkModuleMapFilePath = Path.Combine(modulesDirectoryPath, moduleMapFileName);

        Logger.LogDebug("Creating Framework Directory Structure");
        Directory.CreateDirectory(outputBundlePath);
        Directory.CreateDirectory(versionsDirectoryPath);
        Directory.CreateDirectory(versionsADirectoryPath);
        Directory.CreateDirectory(modulesDirectoryPath);
        Directory.CreateDirectory(resourcesDirectoryPath);

        Directory.CreateSymbolicLink(versionsCurrentDirectoryPath, versionsADirName);
        Directory.CreateSymbolicLink(modulesLinkDirectoryPath, $"{versionsDirName}/{versionsCurrentDirName}/{modulesDirName}");
        Directory.CreateSymbolicLink(versionsCurrentDirectoryPath, $"{versionsDirName}/{versionsCurrentDirName}/{resourcesDirName}");
        
        Logger.LogDebug("Creating Framework Info.plist");
        string infoPlistContent = GetInfoPlistContent();
        File.WriteAllText(infoPlistFilePath, infoPlistContent);

        if (!string.IsNullOrEmpty(ModuleMapFilePath)) {
			Logger.LogDebug("Copying Framework modulemap");
	        File.Copy(ModuleMapFilePath, frameworkModuleMapFilePath);
        }

        if (!string.IsNullOrEmpty(SwiftModuleDirectoryPath)) {
	        Logger.LogDebug("Copying Framework Swift module");
	        string frameworkSwiftModuleDirectoryPath = Path.Combine(modulesDirectoryPath, Path.GetFileName(SwiftModuleDirectoryPath));
	        
	        FileSystemUtils.CopyDirectory(
		        SwiftModuleDirectoryPath,
		        frameworkSwiftModuleDirectoryPath,
		        true
		    );
        }
        
        Logger.LogDebug("Copying Framework library");
        File.Copy(LibraryFilePath, frameworkLibraryFilePath);
        File.CreateSymbolicLink(frameworkLibraryFileLinkPath, $"{versionsDirName}/{versionsCurrentDirName}/{libraryName}");
    }

    private string GetInfoPlistContent()
    {
	    string extendedTemplate = INFO_PLIST_TEMPLATE
		    .Replace(TOKEN_FRAMEWORK_NAME, FrameworkName);

	    return extendedTemplate;
    }

    private const string TOKEN = "$__BEYOND_TOKEN__$";
    private const string TOKEN_FRAMEWORK_NAME = $"{TOKEN}FrameworkName{TOKEN}";

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
</dict>
</plist>
""";
}