namespace Beyond.NET.CodeGenerator.Extensions;

internal static class StringExtensions
{
    // ref. https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/
    private static readonly HashSet<string> CSHARP_KEYWORDS =
    [
        "abstract",
        "as",
        "base",
        "bool",
        "break",
        "byte",
        "case",
        "catch",
        "char",
        "checked",
        "class",
        "const",
        "continue",
        "decimal",
        "default",
        "delegate",
        "do",
        "double",
        "else",
        "enum",
        "event",
        "explicit",
        "extern",
        "false",
        "finally",
        "fixed",
        "float",
        "for",
        "foreach",
        "goto",
        "if",
        "implicit",
        "in",
        "int",
        "interface",
        "internal",
        "is",
        "lock",
        "long",
        "namespace",
        "new",
        "null",
        "object",
        "operator",
        "out",
        "override",
        "params",
        "private",
        "protected",
        "public",
        "readonly",
        "ref",
        "return",
        "sbyte",
        "sealed",
        "short",
        "sizeof",
        "stackalloc",
        "static",
        "string",
        "struct",
        "switch",
        "this",
        "throw",
        "true",
        "try",
        "typeof",
        "uint",
        "ulong",
        "unchecked",
        "unsafe",
        "ushort",
        "using",
        "virtual",
        "void",
        "volatile",
        "while",
    ];

    private static readonly HashSet<string> SWIFT_KEYWORDS =
    [
        "class",
        "struct",
        "import",
        "default",
        "associatedtype",
        "init",
        "deinit",
        "enum",
        "extension",
        "static",
        "open",
        "public",
        "internal",
        "private",
        "fileprivate",
        "func",
        "inout",
        "let",
        "var",
        "operator",
        "protocol",
        "subscript",
        "typealias",
        "break",
        "case",
        "continue",
        "defer",
        "do",
        "else",
        "fallthrough",
        "for",
        "guard",
        "if",
        "in",
        "repeat",
        "return",
        "switch",
        "where",
        "while",
        "as",
        "catch",
        "true",
        "false",
        "is",
        "nil",
        "rethrows",
        "super",
        "self",
        "Self",
        "throw",
        "throws",
        "associativity",
        "convenience",
        "dynamic",
        "didSet",
        "final",
        "get",
        "infix",
        "indirect",
        "lazy",
        "mutating",
        "nonmutating",
        "override",
        "precedence",
        "required",
        "weak",
        "willSet",
        "unowned"
    ];

    // https://kotlinlang.org/docs/keyword-reference.html#hard-keywords
    private static readonly HashSet<string> KOTLIN_KEYWORDS =
    [
        "abstract", // modifier
        "actual", // modifier
        "annotation", // modifier
        "as",
        "break",
        "by", // soft
        "catch", // soft
        "class",
        "companion", // modifier
        "const", // modifier
        "constructor", // soft
        "continue",
        "crossinline", // modifier
        "data", // modifier
        "delegate", // soft
        "do",
        "dynamic", // soft
        "else",
        "enum", // modifier
        "expect", // modifier
        "external", // modifier
        "false",
        "field", // soft, special
        "file", // soft
        "final", // modifier
        "finally", // soft
        "for",
        "fun",
        "get", // soft
        "if",
        "import", // soft
        "in",
        "infix", // modifier
        "init", // soft
        "inline", // modifier
        "inner", // modifier
        "interface",
        "internal", // modifier
        "is",
        "it", // special
        "lateinit", // modifier
        "noinline", // modifier
        "null",
        "object",
        "open", // modifier
        "operator", // modifier
        "out", // modifier
        "override", // modifier
        "package",
        "param", // soft
        "private", // modifier
        "property", // soft
        "protected", // modifier
        "public", // modifier
        "receiver", // soft
        "reified", // modifier
        "return",
        "sealed", // modifier
        "set", // soft
        "setparam", // soft
        "super",
        "suspend", // modifier
        "tailrec", // modifier
        "this",
        "throw",
        "true",
        "try",
        "typealias",
        "typeof",
        "val",
        "value", // soft
        "var",
        "vararg", // modifier
        "when",
        "where", // soft
        "while",
    ];

    private static readonly HashSet<string> SWIFT_RESERVED_TYPE_NAMES =
    [
        "Type"
    ];

    internal static string ToSwiftEnumCaseName(this string cSharpCaseName)
    {
        if (string.IsNullOrEmpty(cSharpCaseName)) {
            return cSharpCaseName;
        }

        string swiftCaseName = cSharpCaseName.FirstCharToLower();

        if (swiftCaseName.IsSwiftKeyword()) {
            swiftCaseName = $"`{swiftCaseName}`";
        }

        return swiftCaseName;
    }

    internal static string FirstCharToLower(this string input)
    {
        if (string.IsNullOrEmpty(input)) {
            return string.Empty;
        }

        char[] stringArray = input.ToCharArray();

        if (char.IsUpper(stringArray[0])) {
            stringArray[0] = char.ToLower(stringArray[0]);
        }

        return new string(stringArray);
    }

    internal static bool IsCSharpKeyword(this string input)
    {
        return CSHARP_KEYWORDS.Contains(input);
    }

    internal static string EscapedCSharpName(this string input)
    {
        if (!input.IsCSharpKeyword()) {
            return input;
        }

        string output = $"@{input}";

        return output;
    }

    internal static bool IsSwiftKeyword(this string input)
    {
        return SWIFT_KEYWORDS.Contains(input);
    }

    internal static bool IsReservedSwiftTypeName(this string input)
    {
        return SWIFT_RESERVED_TYPE_NAMES.Contains(input);
    }

    internal static string EscapedSwiftName(this string input)
    {
        if (!input.IsSwiftKeyword()) {
            return input;
        }

        string output = $"`{input}`";

        return output;
    }

    internal static string EscapedSwiftTypeAliasTypeName(this string input)
    {
        if (!input.IsReservedSwiftTypeName()) {
            return input;
        }

        string output = $"`{input}`";

        return output;
    }

    internal static bool IsKotlinKeyword(this string input)
    {
        return KOTLIN_KEYWORDS.Contains(input);
    }

    internal static string EscapedKotlinName(this string input)
    {
        if (!input.IsKotlinKeyword()) {
            return input;
        }

        string output = $"`{input}`";

        return output;
    }
}
