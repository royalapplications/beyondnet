using System.Text;
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

    private static string ConvertNodeToPlainText(XmlNode node)
    {
        var childs = node.ChildNodes;

        StringBuilder sb = new();

        foreach (XmlNode child in childs) {
            if (child is XmlText textChild) {
                sb.Append(ConvertTextNodeToPlainText(textChild));
            } else if (child.Name == TAG_SEE) {
                sb.Append(ConvertSeeNodeToPlainText(child));
            } else if (child.Name == TAG_PARA) {
                sb.Append(ConvertParaNodeToPlainText(child));
            } else if (child.Name == TAG_PARAMREF) {
                sb.Append(ConvertParamRefNodeToPlainText(child));
            } else if (child.Name == TAG_C) {
                sb.Append(ConvertCNodeToPlainText(child));
            } else if (child.Name == TAG_CODE) {
                sb.Append(ConvertCodeNodeToPlainText(child));
            } else if (child.Name == TAG_XREF) {
                sb.Append(ConvertXrefNodeToPlainText(child));
            } else {
                throw new NotImplementedException();
            }
        }

        var str = sb.ToString();

        return str;
    }

    private static string ConvertTextNodeToPlainText(XmlText textNode)
    {
        var text = textNode.InnerText;

        return text;
    }

    private const string TAG_SEE = "see";
    private static string ConvertSeeNodeToPlainText(XmlNode node)
    {
        var attr = node.Attributes?["cref"];

        if (attr is null) {
            return string.Empty;
        }

        var value = attr.Value;

        if (string.IsNullOrEmpty(value)) {
            return string.Empty;
        }

        var identifier = new XmlDocumentationMemberIdentifier(value);
        var plainText = identifier.ToStringWithoutIdentifier();

        return plainText;
    }
    
    private const string TAG_PARAMREF = "paramref";
    private static string ConvertParamRefNodeToPlainText(XmlNode node)
    {
        var attr = node.Attributes?["name"];

        if (attr is null) {
            return string.Empty;
        }

        var value = attr.Value;

        if (string.IsNullOrEmpty(value)) {
            return string.Empty;
        }

        return value;
    }

    private const string TAG_PARA = "para";
    private static string ConvertParaNodeToPlainText(XmlNode node)
    {
        return ConvertNodeToPlainText(node);
    }
    
    private const string TAG_C = "c";
    private static string ConvertCNodeToPlainText(XmlNode node)
    {
        return node.InnerText;
    }
    
    private const string TAG_CODE = "code";
    private static string ConvertCodeNodeToPlainText(XmlNode node)
    {
        return node.InnerText;
    }
    
    private const string TAG_XREF = "xref";
    private static string ConvertXrefNodeToPlainText(XmlNode node)
    {
        var attr = node.Attributes?["uid"];

        if (attr is null) {
            return string.Empty;
        }

        var value = attr.Value;

        if (string.IsNullOrEmpty(value)) {
            return string.Empty;
        }

        return value;
    }

    public string SummaryAsPlainText
    {
        get {
            var node = SummaryNode;

            if (node is null) {
                return string.Empty;
            }
            
            var str = ConvertNodeToPlainText(node)
                .Trim(NEW_LINES);

            return str;
        }
    }
}