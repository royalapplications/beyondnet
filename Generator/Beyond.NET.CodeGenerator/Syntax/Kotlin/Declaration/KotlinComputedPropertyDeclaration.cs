using Beyond.NET.CodeGenerator.Generator.Kotlin;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinComputedPropertyDeclaration
{
    // NOTE: The GetterJvmName/SetterJvmName properties are used to disambiguate from methods with the same name as the property getter/setter in JVM. It doesn't change the name of the property in Kotlin though.
    // The compiler error I encountered was "Platform declaration clash: The following declarations have the same JVM signature ..."

    public string Name { get; }
    public string TypeName { get; }
    public KotlinVisibilities Visibility { get; }
    public bool IsOverride { get; }
    public string GetterImplementation { get; }
    public string? GetterJvmName { get; }
    public string? SetterImplementation { get; }
    public string? SetterJvmName { get; }

    public KotlinComputedPropertyDeclaration(
        string name,
        string typeName,
        KotlinVisibilities visibility,
        bool isOverride,
        string getterImplementation,
        string? getterJvmName,
        string? setterImplementation,
        string? setterJvmName
    )
    {
        Name = !string.IsNullOrEmpty(name)
            ? name
            : throw new ArgumentOutOfRangeException(nameof(name));

        TypeName = !string.IsNullOrEmpty(typeName)
            ? typeName
            : throw new ArgumentOutOfRangeException(nameof(typeName));

        Visibility = visibility;
        IsOverride = isOverride;

        if (string.IsNullOrEmpty(getterImplementation)) {
            throw new Exception("A computed property must have a getter and optionally, a setter");
        }

        GetterImplementation = getterImplementation;
        GetterJvmName = getterJvmName;

        SetterImplementation = setterImplementation;
        SetterJvmName = setterJvmName;
    }

    public override string ToString()
    {
        string? suppressInapplicableJvmNameAttribute;

        if (!string.IsNullOrEmpty(GetterJvmName) ||
            !string.IsNullOrEmpty(SetterJvmName)) {
            suppressInapplicableJvmNameAttribute = "@Suppress(\"INAPPLICABLE_JVM_NAME\")";
        } else {
            suppressInapplicableJvmNameAttribute = null;
        }

        var propDecl = WritePropertyDeclaration();
        var getter = WriteGetter();
        var setter = WriteSetter();

        KotlinCodeBuilder sb = new();

        if (!string.IsNullOrEmpty(suppressInapplicableJvmNameAttribute)) {
            sb.AppendLine(suppressInapplicableJvmNameAttribute);
        }

        sb.AppendLine(propDecl);
        sb.AppendLine(getter.IndentAllLines(1));

        if (!string.IsNullOrEmpty(setter)) {
            sb.AppendLine(setter.IndentAllLines(1));
        }

        var prop = sb.ToString();

        return prop;
    }

    private string WritePropertyDeclaration()
    {
        var visibilityString = Visibility.ToKotlinSyntaxString();
        var overrideString = IsOverride ? "override" : string.Empty;
        var valOrVarString = string.IsNullOrEmpty(SetterImplementation) ? "val" : "var";

        KotlinCodeBuilder sb = new(visibilityString);

        if (!string.IsNullOrEmpty(overrideString)) {
            if (!string.IsNullOrEmpty(visibilityString)) {
                sb.Append(' ');
            }

            sb.Append(overrideString);
        }

        sb.Append($" {valOrVarString}");
        sb.Append($" {Name}");
        sb.Append($": {TypeName}");

        var propDecl = sb.ToString();

        return propDecl;
    }

    private string WriteGetter()
    {
        KotlinCodeBuilder sb = new();

        if (!string.IsNullOrEmpty(GetterJvmName)) {
            var sanitizedJvmName = SanitizeJvmName(GetterJvmName);

            sb.AppendLine($"@JvmName(\"{sanitizedJvmName}\")");
        }

        sb.AppendLine("get() {");
        sb.AppendLine(GetterImplementation.IndentAllLines(1));
        sb.AppendLine("}");

        var getter = sb.ToString();

        return getter;
    }

    private string? WriteSetter()
    {
        var setterImpl = SetterImplementation;

        if (string.IsNullOrEmpty(setterImpl)) {
            return null;
        }

        KotlinCodeBuilder sb = new();

        if (!string.IsNullOrEmpty(SetterJvmName)) {
            var sanitizedJvmName = SanitizeJvmName(SetterJvmName);

            sb.AppendLine($"@JvmName(\"{sanitizedJvmName}\")");
        }

        sb.AppendLine("set(value) {");
        sb.AppendLine(setterImpl.IndentAllLines(1));
        sb.AppendLine("}");

        var setter = sb.ToString();

        return setter;
    }

    private string SanitizeJvmName(string name)
    {
        // Dirty Hack
        return name
            .Replace("`", "__");
    }
}
