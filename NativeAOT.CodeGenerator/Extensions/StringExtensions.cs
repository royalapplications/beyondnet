using System.Text;

namespace NativeAOT.CodeGenerator.Extensions;

internal static class StringExtensions
{
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
}