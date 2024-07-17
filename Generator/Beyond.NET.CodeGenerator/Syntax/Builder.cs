namespace Beyond.NET.CodeGenerator.Syntax;

public struct Builder
{
    public static Swift.Builder Swift { get; } = new();
    public static Kotlin.Builder Kotlin { get; } = new();
}