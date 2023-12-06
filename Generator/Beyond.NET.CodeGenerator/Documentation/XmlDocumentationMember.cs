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

    public string GetFormattedDocumentationComment(string commentPrefix = "/// ")
    {
        char[] newLines = new char[] { '\r', '\n' };
        char[] newLinesAndBlanks = new char[] { '\r', '\n', ' ' };
        
        var exceptionNodes = Node.ExceptionNodes;

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
        
        if (exceptionNodes is not null &&
            exceptionNodes.Count > 0) {
            bool isFirstEx = true;
            
            foreach (XmlNode exceptionNode in exceptionNodes) {
                string exceptionType;

                var crefValue = exceptionNode.Attributes?["cref"]?.Value;

                if (!string.IsNullOrEmpty(crefValue)) {
                    exceptionType = new XmlDocumentationMemberIdentifier(crefValue)
                        .ToStringWithoutIdentifier();
                } else {
                    exceptionType = "N/A";
                }

                foreach (var identifier in XmlDocumentationMemberIdentifier.Identifiers) {
                    var identifierPrefix = $"{identifier}:";
                    
                    if (!exceptionType.StartsWith(identifierPrefix) ||
                        exceptionType.Length <= identifierPrefix.Length) {
                        continue;
                    }

                    exceptionType = exceptionType.Substring(identifierPrefix.Length);
                }

                var exceptionNodeText = exceptionNode.InnerXml.Trim(newLinesAndBlanks);
                var exceptionNodeLines = exceptionNodeText.Split(newLines, StringSplitOptions.RemoveEmptyEntries);

                if (exceptionNodeLines.Length <= 0) {
                    continue;
                }

                var exceptionNodeLinesJoined = string.Join("; ", exceptionNodeLines);

                if (!string.IsNullOrEmpty(exceptionNodeLinesJoined)) {
                    if (isFirstEx &&
                        lines.Count > 0) {
                        lines.Add(commentPrefix);
                    }
                    
                    lines.Add($"{commentPrefix}- Throws: {exceptionType} - {exceptionNodeLinesJoined}");

                    isFirstEx = false;
                }
            }
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
}