using Beyond.NET.Builder.DotNET;
using Beyond.NET.Core;

namespace Beyond.NET.Builder;

public class DotNETNativeBuilder
{
    public string TargetFramework { get; }
    public string ProductName { get; }
    public string TargetProjectFilePath { get; }
    public string GeneratedCSharpFilePath { get; }
    public SwiftBuilder.BuildResult? SwiftBuildResult { get; }

    private const string VERBOSITY_LEVEL = Publish.VERBOSITY_LEVEL_NORMAL;
    private const string BUILD_CONFIGURATION = "Release";
    
    private string OutputProductName => $"lib{ProductName}";
    private string OutputProductFileName => $"{OutputProductName}.dylib";
    private string UniversalOutputProductFileName => $"{ProductName}.xcframework";

    public DotNETNativeBuilder(
        string targetFramework,
        string productName,
        string targetProjectFilePath,
        string generatedCSharpFilePath,
        SwiftBuilder.BuildResult? swiftBuildResult
    )
    {
        TargetFramework = targetFramework;
        ProductName = productName;
        TargetProjectFilePath = targetProjectFilePath;
        GeneratedCSharpFilePath = generatedCSharpFilePath;
        SwiftBuildResult = swiftBuildResult;
    }

    public void Build()
    {
        NativeCsProj.AppleSpecificSettings? appleSettings;

        var swiftBuildResult = SwiftBuildResult;

        if (swiftBuildResult is not null) {
            appleSettings = new(
                swiftBuildResult.macOSDeploymentTarget,
                swiftBuildResult.iOSDeploymentTarget,
                swiftBuildResult.LibraryOutputPathFormat,
                swiftBuildResult.SymbolsOutputPathFormat,
                swiftBuildResult.OutputClangModuleMapFilePath
            );
        } else {
            appleSettings = null;
        }

        var nativeCsProj = new NativeCsProj(
            TargetFramework,
            ProductName,
            TargetProjectFilePath,
            appleSettings
        );

        string nativeCsProjContents = nativeCsProj.GetCsProjContents();
        
        string sanitizedProductName = ProductName.SanitizedProductNameForTempDirectory();
        string tempDirectoryPrefix = $"BeyondNETBuilderDotNETNativeBuilder_{sanitizedProductName}_";
        
        string tempDirectoryPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix).FullName;

        string nativeCsProjFileName = $"{ProductName}.csproj";
        string nativeCsProjFilePath = Path.Combine(tempDirectoryPath, nativeCsProjFileName);
        
        File.WriteAllText(nativeCsProjFilePath, nativeCsProjContents);

        string generatedCSharpFilePath = GeneratedCSharpFilePath;
        string generatedCSharpFileName = Path.GetFileName(generatedCSharpFilePath);
        
        File.Copy(GeneratedCSharpFilePath, Path.Combine(tempDirectoryPath, generatedCSharpFileName));

        #region Prepare File Paths
        const string binDirName = "bin";
        const string publishDirName = "publish";
        
        // TODO: Check if actually compiling for Apple platforms

        string macOSARM64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.MacOS_ARM64}/{publishDirName}";
        string macOSX64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.MacOS_X64}/{publishDirName}";
        string macOSUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.MacOS_UNIVERSAL}/{publishDirName}";

        string iOSARM64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_ARM64}/{publishDirName}";
        string iOSSimulatorARM64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_SIMULATOR_ARM64}/{publishDirName}";
        string iOSSimulatorX64BuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_SIMULATOR_X64}/{publishDirName}";
        string iOSSimulatorUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_SIMULATOR_UNIVERSAL}/{publishDirName}";
        string iOSUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_UNIVERSAL}/{publishDirName}";

        string appleUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.APPLE_UNIVERSAL}/{publishDirName}";

        string iOSARM64BuildPath = Path.Combine(tempDirectoryPath, iOSARM64BuildDir);
        string iOSARM64FilePath = Path.Combine(iOSARM64BuildPath, OutputProductFileName);

        string iOSSimulatorARM64BuildPath = Path.Combine(tempDirectoryPath, iOSSimulatorARM64BuildDir); 
        string iOSSimulatorARM64FilePath = Path.Combine(iOSSimulatorARM64BuildPath, OutputProductFileName);

        string iOSSimulatorX64BuildPath = Path.Combine(tempDirectoryPath, iOSSimulatorX64BuildDir);
        string iOSSimulatorX64FilePath = Path.Combine(iOSSimulatorX64BuildPath, OutputProductFileName);
        
        string iOSSimulatorUniversalBuildPath = Path.Combine(tempDirectoryPath, iOSSimulatorUniversalBuildDir);
        string iOSSimulatorUniversalFilePath = Path.Combine(iOSSimulatorUniversalBuildPath, OutputProductFileName);

        string iOSUniversalBuildPath = Path.Combine(tempDirectoryPath, iOSUniversalBuildDir);
        string iOSUniversalXCFrameworkFilePath = Path.Combine(iOSUniversalBuildPath, UniversalOutputProductFileName);

        string macOSARM64BuildPath = Path.Combine(tempDirectoryPath, macOSARM64BuildDir);
        string macOSARM64FilePath = Path.Combine(macOSARM64BuildPath, OutputProductFileName);

        string macOSX64BuildPath = Path.Combine(tempDirectoryPath, macOSX64BuildDir);
        string macOSX64FilePath = Path.Combine(macOSX64BuildPath, OutputProductFileName);

        string macOSUniversalBuildPath = Path.Combine(tempDirectoryPath, macOSUniversalBuildDir);
        string macOSUniversalFilePath = Path.Combine(macOSUniversalBuildPath, OutputProductFileName);

        string appleUniversalBuildPath = Path.Combine(tempDirectoryPath, appleUniversalBuildDir);
        string appleUniversalXCFrameworkFilePath = Path.Combine(appleUniversalBuildPath, UniversalOutputProductFileName);

        string libraryId = $"@rpath/{OutputProductFileName}";
        #endregion Prepare File Paths
        
        #region macOS
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.MacOS_ARM64);
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.MacOS_X64);

        Directory.CreateDirectory(macOSUniversalBuildPath);
        
        Apple.Lipo.App.Create(
            new [] {
                macOSARM64FilePath,
                macOSX64FilePath
            },
            macOSUniversalFilePath
        );
        
        Apple.InstallNameTool.App.ChangeId(macOSUniversalFilePath, libraryId);
        #endregion macOS

        #region iOS
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_ARM64);
        Apple.InstallNameTool.App.ChangeId(iOSARM64FilePath, libraryId);
        
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_SIMULATOR_ARM64);
        Apple.InstallNameTool.App.ChangeId(iOSSimulatorARM64FilePath, libraryId);
        
        DotNETPublish(tempDirectoryPath, RuntimeIdentifier.iOS_SIMULATOR_X64);
        Apple.InstallNameTool.App.ChangeId(iOSSimulatorX64FilePath, libraryId);
        
        Directory.CreateDirectory(iOSSimulatorUniversalBuildPath);
        
        Apple.Lipo.App.Create(
            new [] {
                iOSSimulatorARM64FilePath,
                iOSSimulatorX64FilePath
            },
            iOSSimulatorUniversalFilePath
        );
        
        Apple.InstallNameTool.App.ChangeId(iOSSimulatorUniversalFilePath, libraryId);
        
        Directory.CreateDirectory(iOSUniversalBuildPath);
        
        Apple.Xcodebuild.CreateXCFramework.Run(
            new [] {
                iOSSimulatorUniversalFilePath,
                iOSARM64FilePath
            },
            iOSUniversalXCFrameworkFilePath
        );
        #endregion iOS

        #region Apple Universal
        Directory.CreateDirectory(appleUniversalBuildPath);

        Apple.Xcodebuild.CreateXCFramework.Run(
            new [] {
                macOSUniversalFilePath,
                iOSSimulatorUniversalFilePath,
                iOSARM64FilePath
            },
            appleUniversalXCFrameworkFilePath
        );
        #endregion Apple Universal
        
        // TODO: Copy Swift module into Apple Universal XCFramework
        Console.WriteLine("TODO: Copy Swift module into Apple Universal XCFramework");

        /*
# Copy Swift module into xcframework

SWIFTINTERFACE_FILE_EXTENSION="swiftinterface"
SWIFTMODULE_FILE_EXTENSION="swiftmodule"
SWIFTDOC_FILE_EXTENSION="swiftdoc"
SWIFTABI_FILE_EXTENSION="abi.json"

GENERATED_APPLE_PATH="../Generated/bin/apple"
GENERATED_MODULEMAP_PATH="../Generated/module_clang.modulemap"
MODULEMAP_FILE_NAME="module.modulemap"

swift_copy_modulemap_files() {
	local RUNTIME_IDENTIFIER=$1
	local TARGET_DOUBLE=$2
	local TARGET_SWIFTMODULE_PATH=$3

	echo "Copying module.modulemap"
	cp "${GENERATED_MODULEMAP_PATH}" "${TARGET_SWIFTMODULE_PATH}/${MODULEMAP_FILE_NAME}"

	echo "Copying Swiftinterface"
	cp "${GENERATED_APPLE_PATH}/${RUNTIME_IDENTIFIER}/${TARGET_DOUBLE}.${SWIFTINTERFACE_FILE_EXTENSION}" "${TARGET_SWIFTMODULE_PATH}"

	echo "Copying Swiftmodule"
	cp "${GENERATED_APPLE_PATH}/${RUNTIME_IDENTIFIER}/${TARGET_DOUBLE}.${SWIFTMODULE_FILE_EXTENSION}" "${TARGET_SWIFTMODULE_PATH}"

	echo "Copying Swiftdoc"
	cp "${GENERATED_APPLE_PATH}/${RUNTIME_IDENTIFIER}/${TARGET_DOUBLE}.${SWIFTDOC_FILE_EXTENSION}" "${TARGET_SWIFTMODULE_PATH}"

	echo "Copying Swiftabi"
	cp "${GENERATED_APPLE_PATH}/${RUNTIME_IDENTIFIER}/${TARGET_DOUBLE}.${SWIFTABI_FILE_EXTENSION}" "${TARGET_SWIFTMODULE_PATH}"
}


# macOS
APPLE_MACOS_UNIVERSAL_SWIFTMODULE_PATH="${APPLE_UNIVERSAL_FILE_PATH}/macos-arm64_x86_64/${PRODUCT_NAME}.${SWIFTMODULE_FILE_EXTENSION}"

echo "Making Swiftmodule directory for macOS Universal"
mkdir "${APPLE_MACOS_UNIVERSAL_SWIFTMODULE_PATH}"

swift_copy_modulemap_files "${RUNTIME_IDENTIFIER_MACOS_ARM64}" "arm64-apple-macos" "${APPLE_MACOS_UNIVERSAL_SWIFTMODULE_PATH}"
swift_copy_modulemap_files "${RUNTIME_IDENTIFIER_MACOS_X64}" "x86_64-apple-macos" "${APPLE_MACOS_UNIVERSAL_SWIFTMODULE_PATH}"


# iOS
APPLE_IOS_ARM64_SWIFTMODULE_PATH="${APPLE_UNIVERSAL_FILE_PATH}/ios-arm64/${PRODUCT_NAME}.${SWIFTMODULE_FILE_EXTENSION}"

echo "Making Swiftmodule directory for iOS ARM64"
mkdir "${APPLE_IOS_ARM64_SWIFTMODULE_PATH}"

swift_copy_modulemap_files "${RUNTIME_IDENTIFIER_IOS_ARM64}" "arm64-apple-ios" "${APPLE_IOS_ARM64_SWIFTMODULE_PATH}"


# iOS Simulator
APPLE_IOSSIMULATOR_UNIVERSAL_SWIFTMODULE_PATH="${APPLE_UNIVERSAL_FILE_PATH}/ios-arm64_x86_64-simulator/${PRODUCT_NAME}.${SWIFTMODULE_FILE_EXTENSION}"

echo "Making Swiftmodule directory for iOS Simulator Universal"
mkdir "${APPLE_IOSSIMULATOR_UNIVERSAL_SWIFTMODULE_PATH}"

swift_copy_modulemap_files "${RUNTIME_IDENTIFIER_IOS_SIMULATOR_ARM64}" "arm64-apple-ios" "${APPLE_IOSSIMULATOR_UNIVERSAL_SWIFTMODULE_PATH}"
swift_copy_modulemap_files "${RUNTIME_IDENTIFIER_IOS_SIMULATOR_X64}" "x86_64-apple-ios" "${APPLE_IOSSIMULATOR_UNIVERSAL_SWIFTMODULE_PATH}"
         */
    }

    private void DotNETPublish(
        string workingDirectory,
        string runtimeIdentifier
    )
    {
        Publish.Run(
            workingDirectory,
            runtimeIdentifier,
            VERBOSITY_LEVEL,
            null
        );
    }
}