using System.Text;

namespace Beyond.NET.Builder.DotNET;

public class NativeCsProj
{
    public record AppleSpecificSettings
    (
        string MinMacOSVersion,
        string MiniOSVersion,

        string? SwiftLibraryFilePathFormat,
        string? SymbolsFilePathFormat,
        
        string? ModuleMapFilePath
    );
    
    public string TargetFramework { get; }
    public string ProductName { get; }
    public string TargetAssemblyFilePath { get; }
    public string[] AssemblyReferences { get; }
    public AppleSpecificSettings? AppleSettings { get; }
    
    private string OutputProductName => ProductName;

    public NativeCsProj(
        string targetFramework,
        string productName,
        string targetAssemblyFilePath,
        string[] assemblyReferences,
        AppleSpecificSettings? appleSettings
    )
    {
        TargetFramework = targetFramework;
        ProductName = productName;
        TargetAssemblyFilePath = targetAssemblyFilePath;
        AssemblyReferences = assemblyReferences;
        AppleSettings = appleSettings;
    }

    public string GetCsProjContents()
    {
        // TODO: Disabled for now to get faster build times
        const string nullable = "disable";
        
        const string icudtFileName = "icudt.dat";

        string swiftLibraryFilePathFormat = AppleSettings?.SwiftLibraryFilePathFormat ?? string.Empty;
        string symbolsFilePathFormat = AppleSettings?.SymbolsFilePathFormat ?? string.Empty;
        string moduleMapFilePath = AppleSettings?.ModuleMapFilePath ?? string.Empty;
        
        string minMacOSVersion = AppleSettings?.MinMacOSVersion ?? "1.0";
        string miniOSVersion = AppleSettings?.MiniOSVersion ?? "1.0";
        
        bool mixInSwift = !string.IsNullOrEmpty(swiftLibraryFilePathFormat) &&
                          !string.IsNullOrEmpty(moduleMapFilePath) &&
                          !string.IsNullOrEmpty(symbolsFilePathFormat);

        string swiftLibraryFilePath;
        string symbolsFilePath;
        
        if (mixInSwift) {
            swiftLibraryFilePath = string.Format(swiftLibraryFilePathFormat, "$(RuntimeIdentifier)");
            symbolsFilePath = string.Format(symbolsFilePathFormat, "$(RuntimeIdentifier)");
        } else {
            swiftLibraryFilePath = string.Empty;
            symbolsFilePath = string.Empty;
        }

        StringBuilder assemblyReferencesXmlSb = new();
        
        foreach (var assemblyReference in AssemblyReferences) {
          assemblyReferencesXmlSb.AppendLine($"<Reference Include=\"{assemblyReference}\" />");
        }

        string assemblyReferencesXml = assemblyReferencesXmlSb.ToString();

        string expandedTemplate = CSPROJ_TEMPLATE
            .Replace(TOKEN_ASSEMBLY_NAME, OutputProductName)
            .Replace(TOKEN_TARGET_FRAMEWORK, TargetFramework)
            .Replace(TOKEN_NULLABLE, nullable)
            .Replace(TOKEN_TARGET_ASSEMBLY_FILE_PATH, TargetAssemblyFilePath)
            .Replace(TOKEN_ASSEMBLY_REFERENCES, assemblyReferencesXml)
            .Replace(TOKEN_MIX_IN_SWIFT, mixInSwift ? "true" : "false")
            .Replace(TOKEN_MIN_MACOS_VERSION, minMacOSVersion)
            .Replace(TOKEN_MIN_IOS_VERSION, miniOSVersion)
            .Replace(TOKEN_SWIFT_LIBRARY_FILE_PATH, swiftLibraryFilePath)
            .Replace(TOKEN_SYMBOLS_FILE_PATH, symbolsFilePath)
            .Replace(TOKEN_MODULE_MAP_FILE_PATH, moduleMapFilePath)
            .Replace(TOKEN_ICUDT_FILE_PATH, icudtFileName)
          ;

        return expandedTemplate;
    }

    private const string TOKEN = "$__BEYOND_TOKEN__$";
    
    private const string TOKEN_ASSEMBLY_NAME = $"{TOKEN}AssemblyName{TOKEN}";
    private const string TOKEN_TARGET_FRAMEWORK = $"{TOKEN}TargetFramework{TOKEN}";
    private const string TOKEN_NULLABLE = $"{TOKEN}Nullable{TOKEN}";
    private const string TOKEN_TARGET_ASSEMBLY_FILE_PATH = $"{TOKEN}TargetAssemblyFilePath{TOKEN}";
    private const string TOKEN_ASSEMBLY_REFERENCES = $"{TOKEN}AssemblyReferences{TOKEN}";
    
    private const string TOKEN_MIX_IN_SWIFT = $"{TOKEN}MixInSwift{TOKEN}";
    private const string TOKEN_MIN_MACOS_VERSION = $"{TOKEN}MinMacOSVersion{TOKEN}";
    private const string TOKEN_MIN_IOS_VERSION = $"{TOKEN}MiniOSVersion{TOKEN}";
    private const string TOKEN_SWIFT_LIBRARY_FILE_PATH = $"{TOKEN}SwiftLibraryFilePath{TOKEN}";
    private const string TOKEN_SYMBOLS_FILE_PATH = $"{TOKEN}SymbolsFilePath{TOKEN}";
    private const string TOKEN_MODULE_MAP_FILE_PATH = $"{TOKEN}ModuleMapFilePath{TOKEN}";
    private const string TOKEN_ICUDT_FILE_PATH = $"{TOKEN}IcudtFilePath{TOKEN}";

    private const string CSPROJ_TEMPLATE = $"""
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <AssemblyName>{TOKEN_ASSEMBLY_NAME}</AssemblyName>
    <OutputType>Library</OutputType>
    <TargetFramework>{TOKEN_TARGET_FRAMEWORK}</TargetFramework>
    <LangVersion>latest</LangVersion>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    <PublishTrimmed>true</PublishTrimmed>
    <PublishAot>true</PublishAot>
    <DisableFastUpToDateCheck>true</DisableFastUpToDateCheck>
    <EnablePreviewFeatures>true</EnablePreviewFeatures>

    <!-- TODO: Disabled for now to get faster build times -->
    <Nullable>{TOKEN_NULLABLE}</Nullable>
    
    <!-- Seems to not be required as long as we're providing the RuntimeIdentifier when calling dotnet publish/build -->
    <!-- <RuntimeIdentifiers>osx-x64;osx-arm64;linux-x64;ios-arm64;iossimulator-arm64;iossimulator-x64</RuntimeIdentifiers> -->

    <MixInSwift>{TOKEN_MIX_IN_SWIFT}</MixInSwift>
  </PropertyGroup>

  <PropertyGroup>
    <MacOSMinVersion>{TOKEN_MIN_MACOS_VERSION}</MacOSMinVersion>
    <iOSMinVersion>{TOKEN_MIN_IOS_VERSION}</iOSMinVersion>
  </PropertyGroup>
  
  <!-- Compiler Customization -->

  <!-- Merge exported Symbols List -->
  <Target Name="MergeExportedSymbolsList" 
          BeforeTargets="LinkNative"
          Condition="$(MixInSwift) And ($(RuntimeIdentifier.Contains('osx')) Or $(RuntimeIdentifier.Contains('ios')))">
    <Exec Command="echo '\n' >> '$(ExportsFile)'; cat '{TOKEN_SYMBOLS_FILE_PATH}' >> '$(ExportsFile)'" />
  </Target>

  <!-- Include pre-compiled Swift stuff -->
  <Choose>
    <When Condition="$(MixInSwift) And ($(RuntimeIdentifier.Contains('osx')) Or $(RuntimeIdentifier.Contains('ios')))">
      <ItemGroup>
        <LinkerArg Include="-Wl,-force_load,{TOKEN_SWIFT_LIBRARY_FILE_PATH}" />
        <LinkerArg Include="-fmodules -fmodule-map-file={TOKEN_MODULE_MAP_FILE_PATH}" />
      </ItemGroup>
    </When>
  </Choose>

  <!-- Set min macOS version -->
  <Choose>
    <When Condition="$(RuntimeIdentifier.Contains('osx'))">
      <ItemGroup>
        <LinkerArg Include="-mmacosx-version-min=$(MacOSMinVersion)" />
      </ItemGroup>
    </When>
  </Choose>

  <!-- Culture stuff for iOS Support -->
  <Choose>
    <When Condition="$(RuntimeIdentifier.Contains('ios'))">
      <!-- If globalization is not required, you can enable this instead of providing an icudt.dat file on iOS -->
      <!-- <PropertyGroup>
        <InvariantGlobalization>true</InvariantGlobalization>
      </PropertyGroup> -->

      <!-- When this is set, an icudt.dat must be placed in the resulting bundle -->
      <ItemGroup>
        <RuntimeHostConfigurationOption Include="ICU_DAT_FILE_PATH" Value="{TOKEN_ICUDT_FILE_PATH}" />
      </ItemGroup>
    </When>
  </Choose>

  <Choose>
    <When Condition="$(RuntimeIdentifier.Contains('ios'))">
      <ItemGroup>
        <!-- Link to Swift on iOS (it's automatic when targeting macOS) -->
        <LinkerArg Include="-L/usr/lib/swift" />
      </ItemGroup>
    </When>
  </Choose>

  <!-- TODO: Temporary workarounds for iOS/iOS Simulator support -->
  <Choose>
    <When Condition="$(RuntimeIdentifier.Contains('ios'))">
      <PropertyGroup>
        <PublishAotUsingRuntimePack>true</PublishAotUsingRuntimePack>
        <_IsAppleMobileLibraryMode>false</_IsAppleMobileLibraryMode>
      </PropertyGroup>
    </When>
  </Choose>

  <Choose>
    <When Condition="$(RuntimeIdentifier.Contains('iossimulator'))">
      <ItemGroup>
        <!-- TODO: Temporary workaround for iOS Simulator support -->
        <LinkerArg Include="-isysroot /Applications/Xcode.app/Contents/Developer/Platforms/iPhoneSimulator.platform/Developer/SDKs/iPhoneSimulator.sdk" />

        <!-- Set min iOS version -->
        <LinkerArg Include="-mios-simulator-version-min=$(iOSMinVersion)" />
      </ItemGroup>
    </When>
  </Choose>

  <!-- TODO: Temporary workarounds for iOS support -->
  <Choose>
    <When Condition="$(RuntimeIdentifier.Contains('ios-'))">
      <ItemGroup>
        <!-- TODO: Temporary workaround for iOS support -->
        <LinkerArg Include="-isysroot /Applications/Xcode.app/Contents/Developer/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk" />

        <!-- Set min iOS version -->
        <LinkerArg Include="-mios-version-min=$(iOSMinVersion)" />
      </ItemGroup>
    </When>
  </Choose>

  <!-- Strip Symbols in Release Builds -->
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <StripSymbols>true</StripSymbols>
  </PropertyGroup>

  <!-- Item Excludes -->
  <PropertyGroup>
    <DefaultItemExcludes>$(DefaultItemExcludes);.gitignore;*.sln.DotSettings;</DefaultItemExcludes>
  </PropertyGroup>

  <!-- Target Assembly Reference -->
  <ItemGroup>
    <Reference Include="{TOKEN_TARGET_ASSEMBLY_FILE_PATH}" />
  </ItemGroup>

  <!-- Assembly References -->
  <ItemGroup>
    {TOKEN_ASSEMBLY_REFERENCES}
  </ItemGroup>

</Project>
""";
}