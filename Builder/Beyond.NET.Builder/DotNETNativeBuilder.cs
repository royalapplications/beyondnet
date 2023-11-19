using Beyond.NET.Builder.Apple.Framework;
using Beyond.NET.Builder.DotNET;
using Beyond.NET.Core;

namespace Beyond.NET.Builder;

public class DotNETNativeBuilder
{
    public record BuildResult(
        IEnumerable<string> TemporaryDirectoryPaths,
        string OutputDirectoryPath
    );
    
    public BuildTargets Targets { get; }
    public string TargetFramework { get; }
    public string ProductName { get; }
    public string ProductBundleIdentifier { get; }
    public string TargetAssemblyFilePath { get; }
    public string[] AssemblyReferences { get; }
    public bool StripSymbols { get; }
    public string GeneratedCSharpFilePath { get; }
    public bool BuildInParallel { get; }
    public SwiftBuilder.BuildResult? SwiftBuildResult { get; }

    private const string VERBOSITY_LEVEL = Publish.VERBOSITY_LEVEL_NORMAL;
    private const string BUILD_CONFIGURATION = "Release";

    private ILogger Logger => Services.Shared.LoggerService;
    
    private string OutputProductName => ProductName;
    private string OutputProductFileName => $"{OutputProductName}.dylib";
    private string UniversalOutputProductFileName => $"{ProductName}.xcframework";

    public DotNETNativeBuilder(
        BuildTargets targets,
        string targetFramework,
        string productName,
        string productBundleIdentifier,
        string targetAssemblyFilePath,
        string[] assemblyReferences,
        bool stripSymbols,
        string generatedCSharpFilePath,
        bool buildInParallel,
        SwiftBuilder.BuildResult? swiftBuildResult
    )
    {
        Targets = targets;
        TargetFramework = targetFramework;
        ProductName = productName;
        ProductBundleIdentifier = productBundleIdentifier;
        TargetAssemblyFilePath = targetAssemblyFilePath;
        AssemblyReferences = assemblyReferences;
        StripSymbols = stripSymbols;
        GeneratedCSharpFilePath = generatedCSharpFilePath;
        BuildInParallel = buildInParallel;
        SwiftBuildResult = swiftBuildResult;
    }

    public BuildResult Build()
    {
        #region Generate CSProj
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
            TargetAssemblyFilePath,
            AssemblyReferences,
            StripSymbols,
            appleSettings
        );
        
        Logger.LogDebug("Generating csproj content for NativeAOT build");

        string nativeCsProjContents = nativeCsProj.GetCsProjContents();
        #endregion Generate CSProj

        #region Create Temp Dir
        var tempDirPaths = new List<string>();
        
        string sanitizedProductName = ProductName.SanitizedProductNameForTempDirectory();
        string tempDirectoryPrefix = $"BeyondNETBuilderDotNETNativeBuilder_{sanitizedProductName}_";

        string tempDirectoryPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix).FullName;
        Logger.LogDebug($"Created temp directory for .NET NativeAOT project at \"{tempDirectoryPath}\"");
        
        tempDirPaths.Add(tempDirectoryPath);
        #endregion Create Temp Dir

        #region Copy Material to Temp Dir
        string nativeCsProjFileName = $"{ProductName}.csproj";
        string nativeCsProjFilePath = Path.Combine(tempDirectoryPath, nativeCsProjFileName);
        
        Logger.LogDebug($"Writing csproj to \"{nativeCsProjFilePath}\"");
        File.WriteAllText(nativeCsProjFilePath, nativeCsProjContents);

        string generatedCSharpFilePath = GeneratedCSharpFilePath;
        string generatedCSharpFileName = Path.GetFileName(generatedCSharpFilePath);

        string generatedCSharpDestinationFilePath = Path.Combine(tempDirectoryPath, generatedCSharpFileName);

        Logger.LogDebug($"Copying generated C# file to \"{generatedCSharpDestinationFilePath}\"");
        File.Copy(GeneratedCSharpFilePath, generatedCSharpDestinationFilePath);
        #endregion Copy Material to Temp Dir
        
        if (swiftBuildResult is not null) {
            #region Build for Apple
            #region Prepare File Paths
            const string binDirName = "bin";
            const string publishDirName = "publish";

            string? macOSARM64BuildDir = Targets.HasFlag(BuildTargets.MacOSArm64) ? $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.MacOS_ARM64}/{publishDirName}" : null;
            string? macOSX64BuildDir = Targets.HasFlag(BuildTargets.MacOSX64) ? $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.MacOS_X64}/{publishDirName}" : null;
            string? macOSUniversalBuildDir = Targets.HasFlag(BuildTargets.MacOSUniversal) ? $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.MacOS_UNIVERSAL}/{publishDirName}" : null;
            string? iOSARM64BuildDir = Targets.HasFlag(BuildTargets.iOSArm64) ? $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_ARM64}/{publishDirName}" : null;
            string? iOSSimulatorARM64BuildDir = Targets.HasFlag(BuildTargets.iOSSimulatorArm64) ? $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_SIMULATOR_ARM64}/{publishDirName}" : null;
            string? iOSSimulatorX64BuildDir = Targets.HasFlag(BuildTargets.iOSSimulatorX64) ? $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_SIMULATOR_X64}/{publishDirName}" : null;
            string? iOSSimulatorUniversalBuildDir = Targets.HasFlag(BuildTargets.iOSSimulatorUniversal) ? $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.iOS_SIMULATOR_UNIVERSAL}/{publishDirName}" : null;
            string appleUniversalBuildDir = $"{binDirName}/{BUILD_CONFIGURATION}/{TargetFramework}/{RuntimeIdentifier.APPLE_UNIVERSAL}/{publishDirName}";

            string iOSARM64TempPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix + "_iOS_ARM64").FullName;
            tempDirPaths.Add(iOSARM64TempPath);
            Logger.LogDebug($"Created temp directory for .NET NativeAOT project at \"{iOSARM64TempPath}\"");
            string? iOSARM64BuildPath = Targets.HasFlag(BuildTargets.iOSArm64) ? Path.Combine(iOSARM64TempPath, iOSARM64BuildDir!) : null;
            string? iOSARM64FilePath = Targets.HasFlag(BuildTargets.iOSArm64) ? Path.Combine(iOSARM64BuildPath!, OutputProductFileName) : null;

            string iOSSimulatorARM64TempPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix + "_iOSSimulator_ARM64").FullName;
            tempDirPaths.Add(iOSSimulatorARM64TempPath);
            Logger.LogDebug($"Created temp directory for .NET NativeAOT project at \"{iOSSimulatorARM64TempPath}\"");
            string? iOSSimulatorARM64BuildPath = Targets.HasFlag(BuildTargets.iOSSimulatorArm64) ? Path.Combine(iOSSimulatorARM64TempPath, iOSSimulatorARM64BuildDir!) : null;
            string? iOSSimulatorARM64FilePath = Targets.HasFlag(BuildTargets.iOSSimulatorArm64) ? Path.Combine(iOSSimulatorARM64BuildPath!, OutputProductFileName) : null;

            string iOSSimulatorX64TempPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix + "_iOSSimulator_X64").FullName;
            tempDirPaths.Add(iOSSimulatorX64TempPath);
            Logger.LogDebug($"Created temp directory for .NET NativeAOT project at \"{iOSSimulatorX64TempPath}\"");
            string? iOSSimulatorX64BuildPath = Targets.HasFlag(BuildTargets.iOSSimulatorX64) ? Path.Combine(iOSSimulatorX64TempPath, iOSSimulatorX64BuildDir!) : null;
            string? iOSSimulatorX64FilePath = Targets.HasFlag(BuildTargets.iOSSimulatorX64) ? Path.Combine(iOSSimulatorX64BuildPath!, OutputProductFileName) : null;

            string iOSSimulatorUniversalTempPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix + "_iOSSimulator_Universal").FullName;
            tempDirPaths.Add(iOSSimulatorUniversalTempPath);
            Logger.LogDebug($"Created temp directory for .NET NativeAOT project at \"{iOSSimulatorUniversalTempPath}\"");
            string? iOSSimulatorUniversalBuildPath = Targets.HasFlag(BuildTargets.iOSSimulatorUniversal) ? Path.Combine(iOSSimulatorUniversalTempPath, iOSSimulatorUniversalBuildDir!) : null;
            string? iOSSimulatorUniversalFilePath = Targets.HasFlag(BuildTargets.iOSSimulatorUniversal) ? Path.Combine(iOSSimulatorUniversalBuildPath!, OutputProductFileName) : null;

            string macOSARM64TempPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix + "_macOS_ARM64").FullName;
            tempDirPaths.Add(macOSARM64TempPath);
            Logger.LogDebug($"Created temp directory for .NET NativeAOT project at \"{macOSARM64TempPath}\"");
            string? macOSARM64BuildPath = Targets.HasFlag(BuildTargets.MacOSArm64) ? Path.Combine(macOSARM64TempPath, macOSARM64BuildDir!) : null;
            string? macOSARM64FilePath = Targets.HasFlag(BuildTargets.MacOSArm64) ? Path.Combine(macOSARM64BuildPath!, OutputProductFileName) : null;

            string macOSX64TempPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix + "_macOS_X64").FullName;
            tempDirPaths.Add(macOSX64TempPath);
            Logger.LogDebug($"Created temp directory for .NET NativeAOT project at \"{macOSX64TempPath}\"");
            string? macOSX64BuildPath = Targets.HasFlag(BuildTargets.MacOSX64) ? Path.Combine(macOSX64TempPath, macOSX64BuildDir!) : null;
            string? macOSX64FilePath = Targets.HasFlag(BuildTargets.MacOSX64) ? Path.Combine(macOSX64BuildPath!, OutputProductFileName) : null;

            string macOSUniversalTempPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix + "_macOS_Universal").FullName;
            tempDirPaths.Add(macOSUniversalTempPath);
            Logger.LogDebug($"Created temp directory for .NET NativeAOT project at \"{macOSUniversalTempPath}\"");
            string? macOSUniversalBuildPath = Targets.HasFlag(BuildTargets.MacOSUniversal) ? Path.Combine(macOSUniversalTempPath, macOSUniversalBuildDir!) : null;
            string? macOSUniversalFilePath = Targets.HasFlag(BuildTargets.MacOSUniversal) ? Path.Combine(macOSUniversalBuildPath!, OutputProductFileName) : null;

            string appleUniversalTempPath = Directory.CreateTempSubdirectory(tempDirectoryPrefix + "_Apple_Universal").FullName;
            tempDirPaths.Add(appleUniversalTempPath);
            Logger.LogDebug($"Created temp directory for .NET NativeAOT project at \"{appleUniversalTempPath}\"");
            string appleUniversalBuildPath = Path.Combine(appleUniversalTempPath, appleUniversalBuildDir!);
            string appleUniversalXCFrameworkFilePath = Path.Combine(appleUniversalBuildPath!, UniversalOutputProductFileName);

            string newLibraryId = $"@rpath/{OutputProductFileName}";
            string swiftModuleDirName = $"module.{Apple.XCRun.SwiftC.FileExtensions.SwiftModule}";
            #endregion Prepare File Paths

            #region dotnet publish
            #region macOS
            Action? macOSARM64Func = null;
            if (Targets.HasFlag(BuildTargets.MacOSArm64)) {
                FileSystemUtils.CopyDirectoryContents(
                    tempDirectoryPath,
                    macOSARM64TempPath,
                    true
                );
                
                macOSARM64Func = () => DotNETPublish(macOSARM64TempPath, RuntimeIdentifier.MacOS_ARM64);
            }

            Action? macOSX64Func = null;
            if (Targets.HasFlag(BuildTargets.MacOSX64)) {
                FileSystemUtils.CopyDirectoryContents(
                    tempDirectoryPath,
                    macOSX64TempPath,
                    true
                );
                
                macOSX64Func = () => DotNETPublish(macOSX64TempPath, RuntimeIdentifier.MacOS_X64);
            }

            if (BuildInParallel) {
                List<Task> tasks = new();

                Task? macOSARM64Task = null;
                if (macOSARM64Func is not null) {
                    macOSARM64Task = Task.Run(macOSARM64Func);
                    tasks.Add(macOSARM64Task);
                }
                
                Task? macOSX64Task = null;
                if (macOSX64Func is not null) {
                    macOSX64Task = Task.Run(macOSX64Func);
                    tasks.Add(macOSX64Task);
                }
                
                Logger.LogDebug($"Waiting for {tasks.Count} .NET compilation tasks to complete in parallel");
                
                Task.WaitAll(tasks.ToArray());
            } else {
                macOSARM64Func?.Invoke();
                macOSX64Func?.Invoke();
            }

            if (Targets.HasFlag(BuildTargets.MacOSUniversal)) {
                Logger.LogDebug($"Creating directory for macOS Universal build at \"{macOSUniversalBuildPath}\"");
                Directory.CreateDirectory(macOSUniversalBuildPath!);
    
                Logger.LogDebug($"Merging macOS ARM64 build at \"{macOSARM64FilePath}\" and macOS x64 build at \"{macOSX64FilePath}\" into macOS Universal build at \"{macOSUniversalFilePath}\"");
                
                Apple.Lipo.App.Create(
                    new[] {
                        macOSARM64FilePath!,
                        macOSX64FilePath!
                    },
                    macOSUniversalFilePath!
                );
    
                Logger.LogDebug($"Changing library ID of macOS Universal build at \"{macOSUniversalFilePath}\" to \"{newLibraryId}\"");
                Apple.InstallNameTool.App.ChangeId(macOSUniversalFilePath!, newLibraryId);
            }
            #endregion macOS
            
            #region iOS
            Action? iOSARM64Func = null;
            if (Targets.HasFlag(BuildTargets.iOSArm64)) {
                iOSARM64Func = () => {
                    FileSystemUtils.CopyDirectoryContents(
                        tempDirectoryPath,
                        iOSARM64TempPath,
                        true
                    );
                    
                    DotNETPublish(iOSARM64TempPath, RuntimeIdentifier.iOS_ARM64);
                    Logger.LogDebug($"Changing library ID of iOS ARM64 build at \"{iOSARM64FilePath}\" to \"{newLibraryId}\"");
                    Apple.InstallNameTool.App.ChangeId(iOSARM64FilePath!, newLibraryId);
                };
            }

            Action? iOSSimulatorARM64Func = null;
            if (Targets.HasFlag(BuildTargets.iOSSimulatorArm64)) {
                iOSSimulatorARM64Func = () => {
                    FileSystemUtils.CopyDirectoryContents(
                        tempDirectoryPath,
                        iOSSimulatorARM64TempPath,
                        true
                    );
                    
                    DotNETPublish(iOSSimulatorARM64TempPath, RuntimeIdentifier.iOS_SIMULATOR_ARM64);
                    Logger.LogDebug($"Changing library ID of iOS Simulator ARM64 build at \"{iOSSimulatorARM64FilePath}\" to \"{newLibraryId}\"");
                    Apple.InstallNameTool.App.ChangeId(iOSSimulatorARM64FilePath!, newLibraryId);
                };
            }

            Action? iOSSimulatorX64Func = null;
            if (Targets.HasFlag(BuildTargets.iOSSimulatorX64)) {
                iOSSimulatorX64Func = () => {
                    FileSystemUtils.CopyDirectoryContents(
                        tempDirectoryPath,
                        iOSSimulatorX64TempPath,
                        true
                    );
                    
                    DotNETPublish(iOSSimulatorX64TempPath, RuntimeIdentifier.iOS_SIMULATOR_X64);
                    Logger.LogDebug($"Changing library ID of iOS Simulator x64 build at \"{iOSSimulatorX64FilePath}\" to \"{newLibraryId}\"");
                    Apple.InstallNameTool.App.ChangeId(iOSSimulatorX64FilePath!, newLibraryId);
                };
            }
            
            if (BuildInParallel) {
                List<Task> tasks = new();

                Task? iOSARM64Task = null;
                if (iOSARM64Func is not null) {
                    iOSARM64Task = Task.Run(iOSARM64Func);
                    tasks.Add(iOSARM64Task);
                }
                
                Task? iOSSimulatorARM64Task = null;
                if (iOSSimulatorARM64Func is not null) {
                    iOSSimulatorARM64Task = Task.Run(iOSSimulatorARM64Func);
                    tasks.Add(iOSSimulatorARM64Task);
                }
                
                Task? iOSSimulatorX64Task = null;
                if (iOSSimulatorX64Func is not null) {
                    iOSSimulatorX64Task = Task.Run(iOSSimulatorX64Func);
                    tasks.Add(iOSSimulatorX64Task);
                }
                
                Logger.LogDebug($"Waiting for {tasks.Count} .NET compilation tasks to complete in parallel");
                
                Task.WaitAll(tasks.ToArray());
            } else {
                iOSARM64Func?.Invoke();
                iOSSimulatorARM64Func?.Invoke();
                iOSSimulatorX64Func?.Invoke();
            }

            if (Targets.HasFlag(BuildTargets.iOSSimulatorUniversal)) {
                Logger.LogDebug($"Creating directory for iOS Simulator Universal build at \"{iOSSimulatorUniversalBuildPath}\"");
                Directory.CreateDirectory(iOSSimulatorUniversalBuildPath!);
    
                Logger.LogDebug($"Merging iOS Simulator ARM64 build at \"{iOSSimulatorARM64FilePath}\" and iOS Simulator x64 build at \"{iOSSimulatorX64FilePath}\" into iOS Simulator Universal build at \"{iOSSimulatorUniversalFilePath}\"");
                
                Apple.Lipo.App.Create(
                    new[] {
                        iOSSimulatorARM64FilePath!,
                        iOSSimulatorX64FilePath!
                    },
                    iOSSimulatorUniversalFilePath!
                );
    
                Logger.LogDebug($"Changing library ID of iOS Simulator Universal build at \"{iOSSimulatorUniversalFilePath}\" to \"{newLibraryId}\"");
                Apple.InstallNameTool.App.ChangeId(iOSSimulatorUniversalFilePath!, newLibraryId);
            }
            #endregion iOS
            #endregion dotnet publish
            
            #region Create Frameworks for Platforms
            #region macOS
            string? macOSUniversalFrameworkBundlePath;
            
            if (Targets.HasFlag(BuildTargets.MacOSUniversal)) {
                var macOSUniversalSwiftModuleDirPath = $"{macOSUniversalBuildPath}/{swiftModuleDirName}";
                
                CreateSwiftModule(
                    swiftBuildResult.MacOSARM64Result!,
                    macOSUniversalSwiftModuleDirPath
                );
                
                CreateSwiftModule(
                    swiftBuildResult.MacOSX64Result!,
                    macOSUniversalSwiftModuleDirPath
                );
                
                var macOSUniversalFrameworkBuilder = new FrameworkBuilder(
                    ProductName,
                    ProductBundleIdentifier,
                    macOSUniversalFilePath!,
                    macOSUniversalBuildPath!,
                    true,
                    swiftBuildResult.OutputClangModuleMapFilePath,
                    macOSUniversalSwiftModuleDirPath
                );
    
                macOSUniversalFrameworkBundlePath = macOSUniversalFrameworkBuilder.Build()
                    .FrameworkOutputDirectoryPath;
            } else {
                macOSUniversalFrameworkBundlePath = null;
            }
            #endregion macOS

            #region iOS
            string? iOSARM64FrameworkBundlePath;
            string? iOSSimulatorUniversalFrameworkBundlePath;
            
            if (Targets.HasFlag(BuildTargets.iOSUniversal)) {
                var iOSARM64SwiftModuleDirPath = $"{iOSARM64BuildPath}/{swiftModuleDirName}";
    
                CreateSwiftModule(
                    swiftBuildResult.iOSARM64Result!,
                    iOSARM64SwiftModuleDirPath
                );
                
                var iOSARM64FrameworkBuilder = new FrameworkBuilder(
                    ProductName,
                    ProductBundleIdentifier,
                    iOSARM64FilePath!,
                    iOSARM64BuildPath!,
                    false,
                    swiftBuildResult.OutputClangModuleMapFilePath,
                    iOSARM64SwiftModuleDirPath
                );
    
                iOSARM64FrameworkBundlePath = iOSARM64FrameworkBuilder.Build()
                    .FrameworkOutputDirectoryPath;
    
                var iOSSimulatorUniversalSwiftModuleDirPath = $"{iOSSimulatorUniversalBuildPath}/{swiftModuleDirName}";
                
                CreateSwiftModule(
                    swiftBuildResult.iOSSimulatorARM64Result!,
                    iOSSimulatorUniversalSwiftModuleDirPath
                );
                
                CreateSwiftModule(
                    swiftBuildResult.iOSSimulatorX64Result!,
                    iOSSimulatorUniversalSwiftModuleDirPath
                );
                
                var iOSSimulatorUniversalFrameworkBuilder = new FrameworkBuilder(
                    ProductName,
                    ProductBundleIdentifier,
                    iOSSimulatorUniversalFilePath!,
                    iOSSimulatorUniversalBuildPath!,
                    false,
                    swiftBuildResult.OutputClangModuleMapFilePath,
                    iOSSimulatorUniversalSwiftModuleDirPath
                );
    
                iOSSimulatorUniversalFrameworkBundlePath = iOSSimulatorUniversalFrameworkBuilder.Build()
                    .FrameworkOutputDirectoryPath;
            } else {
                iOSARM64FrameworkBundlePath = null;
                iOSSimulatorUniversalFrameworkBundlePath = null;
            }
            #endregion iOS
            #endregion Create Frameworks for Platforms
            
            #region Apple Universal
            Logger.LogDebug($"Creating directory for Apple Universal build at \"{appleUniversalBuildPath}\"");
            Directory.CreateDirectory(appleUniversalBuildPath!);
    
            Logger.LogDebug($"Building Apple Universal XCFramework at \"{appleUniversalXCFrameworkFilePath}\"");

            List<string> frameworks = new();

            if (macOSUniversalFrameworkBundlePath is not null) {
                frameworks.Add(macOSUniversalFrameworkBundlePath);
            }
            
            if (iOSSimulatorUniversalFrameworkBundlePath is not null) {
                frameworks.Add(iOSSimulatorUniversalFrameworkBundlePath);
            }
            
            if (iOSARM64FrameworkBundlePath is not null) {
                frameworks.Add(iOSARM64FrameworkBundlePath);
            }
            
            Apple.Xcodebuild.CreateXCFramework.Run(
                frameworks.ToArray(),
                null,
                appleUniversalXCFrameworkFilePath!
            );
            
            string outputDirectoryPath = appleUniversalBuildPath!;
            #endregion Apple Universal

            return new(
                tempDirPaths,
                outputDirectoryPath
            );
            #endregion Build for Apple
        } else {
            // TODO: Currently only apple builds are supported
            throw new Exception("No swift build result");
        }
    }

    private void DotNETPublish(
        string workingDirectory,
        string runtimeIdentifier
    )
    {
        Logger.LogInformation($"Compiling .NET NativeAOT project in \"{workingDirectory}\" with runtime identifier \"{runtimeIdentifier}\"");
        
        Publish.Run(
            workingDirectory,
            runtimeIdentifier,
            VERBOSITY_LEVEL,
            null
        );
    }

    private void CreateSwiftModule(
        SwiftBuilder.PartialCompileResult partialCompileResult,
        string targetSwiftModuleDirPath
    )
    {
        if (!Directory.Exists(targetSwiftModuleDirPath)) {
            Logger.LogDebug($"Creating swiftmodule directory at \"{targetSwiftModuleDirPath}\"");
            Directory.CreateDirectory(targetSwiftModuleDirPath);
        }
        
        // Copy .swiftinterface
        string swiftinterfaceDestinationFilePath = Path.Combine(targetSwiftModuleDirPath, Path.GetFileName(partialCompileResult.ModuleInterfaceOutputFilePath));
        Logger.LogDebug($"Copying .swiftinterface file to \"{swiftinterfaceDestinationFilePath}\"");
        
        File.Copy(
            partialCompileResult.ModuleInterfaceOutputFilePath,
            swiftinterfaceDestinationFilePath
        );
            
        // Copy .swiftmodule
        string swiftmoduleDestinationFilePath = Path.Combine(targetSwiftModuleDirPath, Path.GetFileName(partialCompileResult.ModuleOutputFilePath));
        Logger.LogDebug($"Copying .swiftmodule file to \"{swiftmoduleDestinationFilePath}\"");
        
        File.Copy(
            partialCompileResult.ModuleOutputFilePath,
            swiftmoduleDestinationFilePath
        );
            
        // Copy .swiftdoc
        if (!string.IsNullOrEmpty(partialCompileResult.SwiftDocOutputFilePath)) {
            string swiftDocDestinationFilePath = Path.Combine(targetSwiftModuleDirPath, Path.GetFileName(partialCompileResult.SwiftDocOutputFilePath));
            Logger.LogDebug($"Copying .swiftdoc file to \"{swiftDocDestinationFilePath}\"");
            
            File.Copy(
                partialCompileResult.SwiftDocOutputFilePath,
                swiftDocDestinationFilePath
            );   
        }

        // Copy .abi.json
        if (!string.IsNullOrEmpty(partialCompileResult.SwiftABIOutputFilePath)) {
            string swiftAbiDestinationFilePath = Path.Combine(targetSwiftModuleDirPath, Path.GetFileName(partialCompileResult.SwiftABIOutputFilePath));
            Logger.LogDebug($"Copying .abi.json file to \"{swiftAbiDestinationFilePath}\"");
            
            File.Copy(
                partialCompileResult.SwiftABIOutputFilePath,
                swiftAbiDestinationFilePath
            );   
        }
    }
}