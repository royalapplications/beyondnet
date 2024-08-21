namespace Beyond.NET.CodeGenerator.Generator.Kotlin;

public class Settings(string kotlinPackageName) : Generator.Settings
{
    public string KotlinPackageName { get; init; } = kotlinPackageName;
}