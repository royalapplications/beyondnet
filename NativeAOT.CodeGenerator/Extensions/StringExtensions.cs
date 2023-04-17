using System.Text;

namespace NativeAOT.CodeGenerator.Extensions;

internal static class StringExtensions
{
    private static readonly string[] SWIFT_KEYWORDS = new[] {
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
    };
        
    internal static string IndentAllLines(this string text, int indentCount)
    {
        string indentPrefix = string.Empty;

        for (int i = 0; i < indentCount; i++) {
            indentPrefix += "\t";
        }
        
        StringBuilder indentedText = new();

        foreach (var line in text.Split(Environment.NewLine)) {
            string indentedLine = indentPrefix + line;

            indentedText.AppendLine(indentedLine);
        }

        return indentedText.ToString();
    }

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

    internal static bool IsSwiftKeyword(this string input)
    {
        return SWIFT_KEYWORDS.Contains(input);
    }

    internal static string EscapedSwiftName(this string input)
    {
        if (!input.IsSwiftKeyword()) {
            return input;
        }
        
        string output = $"`{input}`";

        return output;
    }
}