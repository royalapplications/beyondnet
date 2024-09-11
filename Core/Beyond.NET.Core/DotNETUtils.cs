using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Beyond.NET.Core;

public class DotNETUtils
{
    public static string GetLatestRefPack(
        string dotnetDir,
        sbyte dotnetVersion
    )
    {
        if (string.IsNullOrWhiteSpace(dotnetDir)) {
            throw new ArgumentOutOfRangeException(nameof(dotnetDir));
        }

        if (dotnetVersion <= 0) {
            throw new ArgumentOutOfRangeException(nameof(dotnetVersion));
        }

        var refDir = Path.Join(
            dotnetDir,
            "packs/Microsoft.NETCore.App.Ref"
        );

        if (!Directory.Exists(refDir)) {
            return string.Empty;
        }

        var refSubdir = $"ref/net{dotnetVersion}.0";

        var latestVersion = new Version(
            dotnetVersion, // Major
            0, // Minor
            0 // Build
        );

        var searchPattern = $"{dotnetVersion}.0.*";
        
        foreach (string dir in Directory.EnumerateDirectories(refDir, searchPattern)) {
            var dirName = Path.GetFileName(dir);

            if (!Version.TryParse(dirName, out Version? version)) {
                continue;
            }

            var refAssemblyDir = Path.Join(dir, refSubdir);

            if (!Directory.Exists(refAssemblyDir)) {
                continue;
            }

            if (!Directory.EnumerateFiles(refAssemblyDir, "*.xml").Any()) {
                continue;
            }

            if (version > latestVersion) {
                latestVersion = version;
            }
        }
        
        // sanity check
        string latestRefDir = Path.Join(refDir, latestVersion.ToString());

        if (!Directory.Exists(latestRefDir)) {
            throw new Exception("Found latest ref dir but it does not exist");
        }
        
        latestRefDir = Path.Join(latestRefDir, refSubdir);
        
        if (!Directory.Exists(latestRefDir)) {
            throw new Exception("Found latest ref dir but it does not exist");
        }

        if (!Directory.EnumerateFiles(latestRefDir, "*.xml").Any()) {
            throw new Exception("Found latest ref dir but it has no xml files");
        }
        
        return latestRefDir;
    }
    
    public static sbyte GetDotNetCoreVersion(string tfm)
    {
        if (string.IsNullOrWhiteSpace(tfm)) {
            throw new ArgumentOutOfRangeException(nameof(tfm));
        }
        
        var rxTfm = new Regex(
            @"\.NETCoreApp,Version=v(?<v>[0-9]+)\.0",
            RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.Singleline
        );

        var match = rxTfm.Match(tfm);

        if (!match.Success) {
            return -1;
        }

        var v = sbyte.TryParse(match.Groups["v"].ValueSpan, out sbyte version) && version > 0 
            ? version 
            : (sbyte)-1;

        return v;
    }

    public static string GetDotnetGlobalInstallLocation()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX) &&
            !RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
            throw new Exception("TODO: platforms other than macOS and Linux are not yet supported");
        } 

        // https://github.com/dotnet/designs/blob/main/accepted/2020/install-locations.md#global-install-to-custom-location
        const string installMarkerFile = "/etc/dotnet/install_location";
        
        var architecture = RuntimeInformation.OSArchitecture;
        string file;
        
        switch (architecture) {
            case Architecture.X64:
                file = installMarkerFile;
                break;
            
            case Architecture.Arm64:
                file = installMarkerFile + "_arm64";
                break;
            
            default:
                throw new Exception($"Unexpected architecture \"{architecture}\"");
        }

        try {
            using var reader = File.OpenText(file);
            
            var location = reader.ReadLine() ?? string.Empty;
            
            var path = Directory.Exists(location)
                ? location 
                : string.Empty;

            return path;
        } catch (Exception ex) {
            throw new Exception($"Cannot determine .NET install location from \"{file}\"", ex);
        }
    }
    
    public static string GetTargetFrameworkName(string assemblyFilePath)
    {
        if (string.IsNullOrWhiteSpace(assemblyFilePath)) {
            throw new ArgumentOutOfRangeException(nameof(assemblyFilePath));
        }

        if (!Path.IsPathFullyQualified(assemblyFilePath)) {
            throw new Exception($"Expected qualified path for \"{assemblyFilePath}\"");
        }
        
        // https://weblog.west-wind.com/posts/2018/Apr/12/Getting-the-NET-Core-Runtime-Version-in-a-Running-Application
        // https://gist.github.com/alexey-gusarov/050dcac7d9bb9f0a1c192142db57c367
        // https://stackoverflow.com/questions/54727685/getting-target-framework-attribute-in-powershell-core

        using var stream = File.OpenRead(assemblyFilePath);
        using var peReader = new PEReader(stream, PEStreamOptions.PrefetchMetadata);
        
        var mdReader = peReader.GetMetadataReader();
        var asm = mdReader.GetAssemblyDefinition();
        var attributeHandles = asm.GetCustomAttributes();

        var attrs = attributeHandles.Select(a => mdReader.GetCustomAttribute(a));
        
        foreach (var attr in attrs) {
            var ctor = mdReader.GetMemberReference((MemberReferenceHandle)attr.Constructor);
            var attrType = mdReader.GetTypeReference((TypeReferenceHandle)ctor.Parent);
            var attrName = mdReader.GetString(attrType.Name);

            if (!(attrName is "TargetFrameworkAttribute")) {
                continue;
            }

            var attrValueBytes = mdReader.GetBlobContent(attr.Value);
            var length = attrValueBytes[2];
            var attrValue = Encoding.UTF8.GetString(attrValueBytes.AsSpan(3, length));
            
            return attrValue;
        }

        return string.Empty;
    }
}