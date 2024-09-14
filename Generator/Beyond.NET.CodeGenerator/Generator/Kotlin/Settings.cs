namespace Beyond.NET.CodeGenerator.Generator.Kotlin;

public class Settings(
    string kotlinPackageName,
    string kotlinNativeLibraryName
) : Generator.Settings
{
    public string KotlinPackageName { get; init; } = kotlinPackageName;
    public string KotlinNativeLibraryName { get; init; } = kotlinNativeLibraryName;
}