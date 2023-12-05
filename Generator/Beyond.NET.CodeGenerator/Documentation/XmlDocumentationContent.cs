using System.Xml;

namespace Beyond.NET.CodeGenerator;

public struct XmlDocumentationContent
{
    public XmlNode Node { get; }
    
    public XmlDocumentationContent(XmlNode xmlNode)
    {
        Node = xmlNode;
    }

    public override string ToString()
    {
        return Node.InnerXml;
    }

    public string GetFormattedDocumentationComment(string commentPrefix = "/// ")
    {
        char[] newLines = new char[] { '\r', '\n' };
        char[] newLinesAndBlanks = new char[] { '\r', '\n', ' ' };
        
        var summaryNode = Node.SelectSingleNode(".//summary");
        var summaryText = summaryNode?.InnerXml.Trim(newLinesAndBlanks);
        var summaryLines = summaryText?.Split(newLines, StringSplitOptions.RemoveEmptyEntries);
        
        var returnsNode = Node.SelectSingleNode(".//returns");
        var returnsText = returnsNode?.InnerXml.Trim(newLinesAndBlanks);
        var returnsLines = returnsText?.Split(newLines, StringSplitOptions.RemoveEmptyEntries);

        var paramNodes = Node.SelectNodes(".//param");
        var exceptionNodes = Node.SelectNodes(".//exception");

        List<string> lines = new();

        if (summaryLines is not null &&
            summaryLines.Length > 0) {
            foreach (var summaryLine in summaryLines) {
                var trimmedSummaryLine = summaryLine.Trim();

                if (string.IsNullOrEmpty(trimmedSummaryLine)) {
                    continue;
                }
                
                lines.Add($"{commentPrefix}{trimmedSummaryLine}");
            }
        }

        if (paramNodes is not null &&
            paramNodes.Count > 0) {
            bool isFirstParam = true;
            
            foreach (XmlNode paramNode in paramNodes) {
                var paramName = paramNode.Attributes?["name"]?.Value;

                if (string.IsNullOrEmpty(paramName)) {
                    continue;
                }
                
                var paramNodeText = paramNode.InnerXml.Trim(newLinesAndBlanks);
                var paramNodeLines = paramNodeText.Split(newLines, StringSplitOptions.RemoveEmptyEntries);

                if (paramNodeLines.Length <= 0) {
                    continue;
                }

                var paramNodeLinesJoined = string.Join("; ", paramNodeLines);

                if (!string.IsNullOrEmpty(paramNodeLinesJoined)) {
                    if (isFirstParam &&
                        lines.Count > 0) {
                        lines.Add(commentPrefix);
                    }
                    
                    lines.Add($"{commentPrefix}- Parameter {paramName}: {paramNodeLinesJoined}");

                    isFirstParam = false;
                }
            }
        }
        
        if (exceptionNodes is not null &&
            exceptionNodes.Count > 0) {
            bool isFirstEx = true;
            
            foreach (XmlNode exceptionNode in exceptionNodes) {
                var exceptionType = exceptionNode.Attributes?["cref"]?.Value ?? "N/A";

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
        
        if (returnsLines is not null &&
            returnsLines.Length > 0) {
            var returnsLinesJoined = string.Join("; ", returnsLines);

            if (!string.IsNullOrEmpty(returnsLinesJoined)) {
                if (lines.Count > 0) {
                    lines.Add(commentPrefix);
                }
                    
                lines.Add($"{commentPrefix}- Returns: {returnsLinesJoined}");
            }
        }

        if (lines.Count <= 0) {
            return string.Empty;
        }

        var linesJoined = string.Join('\n', lines);
        
        return linesJoined;
    }
}