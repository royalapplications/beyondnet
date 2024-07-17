using System.Text;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinFunSignatureComponents
{
    public static string ComponentsToString(IEnumerable<string> components)
    {
        const string space = " ";

        StringBuilder sb = new();

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