using Beyond.NET.CodeGenerator.Generator.Swift;

namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftFuncSignatureComponents
{
    public static string ComponentsToString(IEnumerable<string> components)
    {
        const string space = " ";

        SwiftCodeBuilder sb = new();

        foreach (string component in components) {
            if (string.IsNullOrEmpty(component)) {
                continue;
            }

            sb.Append(component + space);
        }

        string str = sb
            .ToString()
            .Trim();

        return str;
    }
}