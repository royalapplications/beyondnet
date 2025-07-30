using System.Xml;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator;

public struct XmlDocumentationMember
{
    private XmlDocumentationNode Node { get; }

    internal XmlDocumentationMember(XmlNode xmlMemberNode)
    {
        Node = new(xmlMemberNode);
    }

    public override string ToString()
    {
        return Node.ToString();
    }

    public string GetFormattedDocumentationComment(CodeLanguage language)
    {
        if (language == CodeLanguage.Kotlin) {
            return GetFormattedDocumentationCommentForKotlin();
        }

        return GetFormattedDocumentationCommentWithTripleSlashes();
    }

    private string GetFormattedDocumentationCommentWithTripleSlashes()
    {
        const string commentPrefix = "/// ";

        List<string> lines = new();

        var summary = Node.SummaryAsPlainText
            .PrefixAllLines(commentPrefix);
        var summaryLines = summary.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var summaryLine in summaryLines) {
            lines.Add(summaryLine);
        }

        var paramStrings = Node.ParamsAsPlainText;

        foreach (var paramString in paramStrings) {
            lines.Add($"{commentPrefix}- Parameter {paramString}");
        }

        var exceptionStrings = Node.ExceptionsAsPlainText;

        foreach (var exceptionString in exceptionStrings) {
            lines.Add($"{commentPrefix}- Throws: {exceptionString}");
        }

        var returns = Node.ReturnsAsPlainText;
        var returnsAsSingleLine = string.Join(' ', returns.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));

        if (!string.IsNullOrEmpty(returnsAsSingleLine)) {
            lines.Add($"{commentPrefix}- Returns: {returnsAsSingleLine}");
        }

        if (lines.Count <= 0) {
            return string.Empty;
        }

        var linesJoined = string.Join('\n', lines);

        return linesJoined;
    }

    private string GetFormattedDocumentationCommentForKotlin()
    {
        const string commentPrefix = "/**";
        const string commentSuffix = " */";
        const string linePrefix = " * ";

        List<string> lines = new([commentPrefix]);

        var summary = Node.SummaryAsPlainText
            .PrefixAllLines(linePrefix);
        var summaryLines = summary.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var summaryLine in summaryLines) {
            lines.Add(summaryLine);
        }

        var paramStrings = Node.ParamsAsPlainText;

        foreach (var paramString in paramStrings) {
            lines.Add($"{linePrefix}@param {paramString}");
        }

        var exceptionStrings = Node.ExceptionsAsPlainText;

        foreach (var exceptionString in exceptionStrings) {
            lines.Add($"{linePrefix}@throws {exceptionString}");
        }

        var returns = Node.ReturnsAsPlainText;
        var returnsAsSingleLine = string.Join(' ', returns.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));

        if (!string.IsNullOrEmpty(returnsAsSingleLine)) {
            lines.Add($"{linePrefix}@return {returnsAsSingleLine}");
        }

        if (lines.Count <= 1) {
            return string.Empty;
        }

        lines.Add(commentSuffix);

        var linesJoined = string.Join('\n', lines);

        return linesJoined;
    }
}
