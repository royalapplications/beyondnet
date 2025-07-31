namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public record KotlinPropertyInfo(
    string Name,
    string TypeName,
    KotlinVisibilities Visibility,
    bool IsOverride,
    string Implementation,
    string JvmName,
    string? DeclarationComment
);
