using Beyond.NET.Core;

namespace Beyond.NET.Builder.Android;

internal static class AndroidNdkEnvironment
{
    private sealed class PathScope : IDisposable
    {
        private readonly string? _originalPath;

        public PathScope(string? originalPath)
        {
            _originalPath = originalPath;
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable("PATH", _originalPath);
        }
    }

    public static IDisposable PrependBinPathToPath()
    {
        string binPath = ResolveBinPath();
        string? originalPath = Environment.GetEnvironmentVariable("PATH");
        string updatedPath = string.IsNullOrEmpty(originalPath)
            ? binPath
            : $"{binPath}{Path.PathSeparator}{originalPath}";

        Environment.SetEnvironmentVariable("PATH", updatedPath);

        return new PathScope(originalPath);
    }

    public static string ResolveBinPath()
    {
        string? binPath = Environment.GetEnvironmentVariable("ANDROID_NDK_BIN_PATH");

        if (!string.IsNullOrEmpty(binPath) && Directory.Exists(binPath)) {
            return binPath;
        }

        string? ndkHome = Environment.GetEnvironmentVariable("ANDROID_NDK_HOME");

        if (string.IsNullOrEmpty(ndkHome) || !Directory.Exists(ndkHome)) {
            throw new InvalidOperationException(
                "Android NDK bin path could not be resolved. Set ANDROID_NDK_BIN_PATH to the NDK llvm bin directory, " +
                "or set ANDROID_NDK_HOME to your Android NDK installation."
            );
        }

        string hostTag = ResolveHostTag(ndkHome);
        binPath = Path.Combine(ndkHome, "toolchains", "llvm", "prebuilt", hostTag, "bin");

        if (!Directory.Exists(binPath)) {
            throw new DirectoryNotFoundException($"Android NDK bin path not found at \"{binPath}\"");
        }

        return binPath;
    }

    private static string ResolveHostTag(string ndkHome)
    {
        string ndkBinCommonScriptPath = Path.Combine(ndkHome, "build", "tools", "ndk_bin_common.sh");

        if (!File.Exists(ndkBinCommonScriptPath)) {
            throw new FileNotFoundException($"Android NDK helper script not found at \"{ndkBinCommonScriptPath}\"");
        }

        var bashApp = new CLIApp("/bin/bash");
        var result = bashApp.Launch(
            new[] {
                "-c",
                $"source \"{ndkBinCommonScriptPath}\" && echo \"$HOST_TAG\""
            }
        );

        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw new InvalidOperationException("Failed to resolve Android NDK host tag from ndk_bin_common.sh", failure);
        }

        string hostTag = (result.StandardOut ?? string.Empty).Trim();

        if (string.IsNullOrEmpty(hostTag)) {
            throw new InvalidOperationException("Android NDK host tag resolved from ndk_bin_common.sh was empty");
        }

        return hostTag;
    }
}
