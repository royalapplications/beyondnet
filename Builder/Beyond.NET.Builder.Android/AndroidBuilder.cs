using Beyond.NET.Core;

namespace Beyond.NET.Builder.Android;

public class AndroidBuilder
{
    public record BuildResult(
        string? AndroidARM64LibraryPath,
        string OutputDirectoryPath
    );

    public string ProductName { get; }
    public string OutputDirectory { get; }
    public bool BuildAndroidARM64 { get; }

    private ILogger Logger => Services.Shared.LoggerService;

    private string OutputProductName => ProductName;
    private string OutputLibraryFileName => $"lib{OutputProductName}.so";

    public AndroidBuilder(
        string productName,
        string outputDirectory,
        bool buildAndroidARM64
    )
    {
        ProductName = productName;
        OutputDirectory = outputDirectory;
        BuildAndroidARM64 = buildAndroidARM64;
    }

    public BuildResult Build(
        string? androidARM64BuildPath
    )
    {
        Logger.LogInformation("Building Android libraries");

        string? androidARM64LibraryPath = null;

        if (androidARM64BuildPath is not null && BuildAndroidARM64)
        {
            androidARM64LibraryPath = Path.Combine(androidARM64BuildPath, OutputLibraryFileName);

            if (!File.Exists(androidARM64LibraryPath))
            {
                var libraryWithoutLibName = OutputLibraryFileName.Replace("lib", "");
                androidARM64LibraryPath = Path.Combine(androidARM64BuildPath, libraryWithoutLibName);
            }

            if (!File.Exists(androidARM64LibraryPath))
            {
                throw new FileNotFoundException($"Android library not found at {androidARM64BuildPath}");
            }

            Logger.LogInformation($"Android library found at: {androidARM64LibraryPath}");
        }

        // Create output directory structure for Android
        string androidOutputPath = Path.Combine(OutputDirectory, "android");
        Directory.CreateDirectory(androidOutputPath);

        if (androidARM64LibraryPath is not null)
        {
            string arm64OutputDir = Path.Combine(androidOutputPath, "arm64-v8a");
            Directory.CreateDirectory(arm64OutputDir);
            string destPath = Path.Combine(arm64OutputDir, OutputLibraryFileName);
            File.Copy(androidARM64LibraryPath, destPath, true);
            Logger.LogInformation($"Copied library to: {destPath}");
        }

        if (androidARM64BuildPath is not null && BuildAndroidARM64)
        {
            string debugSymbolsOutputPath = Path.Combine(androidOutputPath, "android-debug-symbols");
            Directory.CreateDirectory(debugSymbolsOutputPath);

            Logger.LogInformation($"Copying all files from {androidARM64BuildPath} to {debugSymbolsOutputPath}");
            CopyAllFiles(androidARM64BuildPath, debugSymbolsOutputPath);

            Logger.LogInformation($"Copied debug symbols to: {debugSymbolsOutputPath}");
        }

        Logger.LogInformation($"Android build completed. Output directory: {androidOutputPath}");

        return new BuildResult(
            androidARM64LibraryPath,
            androidOutputPath
        );
    }

    private void CopyAllFiles(string sourceDirectory, string destinationDirectory)
    {
        if (!Directory.Exists(sourceDirectory))
        {
            throw new DirectoryNotFoundException($"Source directory not found: {sourceDirectory}");
        }

        Directory.CreateDirectory(destinationDirectory);

        foreach (string filePath in Directory.GetFiles(sourceDirectory))
        {
            string fileName = Path.GetFileName(filePath);
            string destFilePath = Path.Combine(destinationDirectory, fileName);

            File.Copy(filePath, destFilePath, overwrite: true);
            Logger.LogDebug($"Copied: {fileName}");
        }

        foreach (string dirPath in Directory.GetDirectories(sourceDirectory))
        {
            string dirName = Path.GetFileName(dirPath);
            string destDirPath = Path.Combine(destinationDirectory, dirName);

            CopyAllFiles(dirPath, destDirPath);
        }
    }
}

