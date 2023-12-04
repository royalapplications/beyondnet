using System.Text.RegularExpressions;

namespace Beyond.NET.CodeGenerator;

public struct XmlDocumentationContent
{
    public string RawXmlContent { get; }

    public string TrimmedXmlContent => RawXmlContent.Trim('\r', '\n', ' ');

    public XmlDocumentationContent(string rawXmlContent)
    {
        RawXmlContent = rawXmlContent ?? throw new ArgumentNullException(nameof(rawXmlContent));
    }

    public override string ToString()
    {
        return RawXmlContent;
    }

    public string GetFormattedDocumentationComment(
        bool removeXmlTags = true,
        string commentPrefix = "/// "
    )
    {
        var trimmedContent = TrimmedXmlContent;
        
        var split = trimmedContent.Split(
            new [] { '\r', '\n' },
            StringSplitOptions.RemoveEmptyEntries
        );

        if (split.Length <= 0) {
            return string.Empty;
        }

        List<string> lines = new();

        foreach (var line in split) {
            string xmlRemovedLine;

            if (removeXmlTags) {
                xmlRemovedLine = Regex.Replace(
                    line,
                    "<[^>]+>",
                    " "
                );
            } else {
                xmlRemovedLine = line;
            }
            
            var trimmedLine = xmlRemovedLine.Trim();

            if (string.IsNullOrEmpty(trimmedLine)) {
                continue;
            }
            
            string commentLine = $"{commentPrefix}{trimmedLine}";
            
            lines.Add(commentLine);
        }

        string fullComment = string.Join('\n', lines);

        return fullComment;
    }
}