namespace Beyond.NET.Core;

public static class FileSystemUtils
{
    public static void CopyDirectoryContents(
        string sourceDirectoryPath,
        string destinationDirectoryPath,
        bool recursive
    )
    {
        CopyDirectoryContents(
            sourceDirectoryPath,
            destinationDirectoryPath,
            recursive,
            true
        );
    }

    private static void CopyDirectoryContents(
        string sourceDirectoryPath,
        string destinationDirectoryPath,
        bool recursive,
        bool isFirstLevel
    )
    {
        // Get information about the source directory
        var sourceDirectory = new DirectoryInfo(sourceDirectoryPath);

        // Check if the source directory exists
        if (!sourceDirectory.Exists) {
            throw new DirectoryNotFoundException($"Source directory not found: {sourceDirectory.FullName}");
        }

        // Cache directories before we start copying
        var sourceDirectories = sourceDirectory.GetDirectories();

        if (!Directory.Exists(destinationDirectoryPath)) {
            // Destination directory does not exist, create it
            Directory.CreateDirectory(destinationDirectoryPath);
        }

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo sourceFile in sourceDirectory.GetFiles()) {
            var targetFilePath = Path.Combine(destinationDirectoryPath, sourceFile.Name);

            var linkTarget = sourceFile.LinkTarget;

            if (linkTarget is not null) { // Link
                var targetFile = new FileInfo(targetFilePath);

                if (targetFile.Exists) {
                    // Target file already exists, delete it
                    targetFile.Delete();
                }

                // Re-create link
                targetFile.CreateAsSymbolicLink(linkTarget);
            } else { // Regular file
                // Copy file
                sourceFile.CopyTo(
                    targetFilePath,
                    true
                );
            }
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive) {
            foreach (DirectoryInfo sourceSubDirectory in sourceDirectories) {
                var destinationSubDirectoryPath = Path.Combine(destinationDirectoryPath, sourceSubDirectory.Name);

                var targetSubDirectory = new DirectoryInfo(destinationSubDirectoryPath);

                var linkTarget = sourceSubDirectory.LinkTarget;

                if (linkTarget is not null) { // Link
                    if (targetSubDirectory.Exists) {
                        // Target directory already exists, delete it
                        targetSubDirectory.Delete();
                    }

                    // Re-create link
                    targetSubDirectory.CreateAsSymbolicLink(linkTarget);
                } else { // Regular directory
                    if (isFirstLevel &&
                        targetSubDirectory.Exists) {
                        // On the first level, delete any existing directories
                        targetSubDirectory.Delete(true);
                    }

                    // Copy directory
                    CopyDirectoryContents(
                        sourceSubDirectory.FullName,
                        destinationSubDirectoryPath,
                        true,
                        false
                    );
                }
            }
        }
    }
}