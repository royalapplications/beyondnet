using System.Xml;

namespace Beyond.NET.CodeGenerator;

internal struct XmlDocumentationNode
{
    private XmlNode MemberNode { get; }
    
    internal XmlNode? SummaryNode => MemberNode.SelectSingleNode(".//summary");
    internal XmlNode? ReturnsNode => MemberNode.SelectSingleNode(".//returns");
    internal XmlNodeList? ParamNodes => MemberNode.SelectNodes(".//param");
    internal XmlNodeList? ExceptionNodes => MemberNode.SelectNodes(".//exception");
    
    private static readonly char[] NEW_LINES = new char[] { '\r', '\n' };
    private static readonly char[] NEW_LINES_AND_BLANKS = new char[] { '\r', '\n', ' ' };
    
    internal XmlDocumentationNode(XmlNode xmlMemberNode)
    {
        MemberNode = xmlMemberNode;
    }

    public override string ToString()
    {
        return MemberNode.InnerXml;
    }

    public string SummaryAsPlainText
    {
        get {
            var node = SummaryNode;

            if (node is null) {
                return string.Empty;
            }
            
            var text = node.InnerXml.Trim(NEW_LINES_AND_BLANKS);
            var lines = text.Split(NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length <= 0) {
                return string.Empty;
            }

            var processedLines = new List<string>();
            
            foreach (var summaryLine in lines) {
                var trimmedSummaryLine = summaryLine.Trim();

                if (string.IsNullOrEmpty(trimmedSummaryLine)) {
                    continue;
                }
                
                processedLines.Add(trimmedSummaryLine);
            }

            if (processedLines.Count <= 0) {
                return string.Empty;
            }

            var plainText = string.Join(Environment.NewLine, processedLines);

            return plainText;
        }
    }
}