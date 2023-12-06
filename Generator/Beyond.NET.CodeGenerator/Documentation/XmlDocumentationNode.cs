using System.Text;
using System.Xml;

namespace Beyond.NET.CodeGenerator;

internal struct XmlDocumentationNode
{
    private XmlNode MemberNode { get; }

    private XmlNode? SummaryNode => MemberNode.SelectSingleNode(".//summary");
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

    #region Public Accessors
    public string SummaryAsPlainText
    {
        get {
            var node = SummaryNode;

            if (node is null) {
                return string.Empty;
            }
            
            var str = ConvertNodeToPlainText(node)
                .Replace("\r\n", Environment.NewLine)
                .Trim(NEW_LINES);

            return str;
        }
    }

    public string ReturnsAsPlainText
    {
        get {
            var node = ReturnsNode;

            if (node is null) {
                return string.Empty;
            }
            
            var str = ConvertNodeToPlainText(node)
                .Replace("\r\n", Environment.NewLine)
                .Trim(NEW_LINES);

            return str;
        }
    }

    public string[] ParamsAsPlainText
    {
        get {
            var nodes = ParamNodes;

            if (nodes is null ||
                nodes.Count <= 0) {
                return Array.Empty<string>();
            }

            List<string> paramStrings = new();

            foreach (XmlNode node in nodes) {
                var paramName = node.Attributes?["name"]?.Value;

                if (string.IsNullOrEmpty(paramName)) {
                    continue;
                }
                
                var str = ConvertNodeToPlainText(node)
                    .Replace("\r\n", Environment.NewLine)
                    .Trim(NEW_LINES);

                if (string.IsNullOrEmpty(str)) {
                    continue;
                }
                
                var strSplit = str.Split(NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
                var strJoined = string.Join(' ', strSplit);

                var paramStr = $"{paramName}: {strJoined}";
                
                paramStrings.Add(paramStr);
            }

            return paramStrings.ToArray();
        }
    }
    #endregion Public Accessors

    #region Node -> Plain Text
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
            } else if (child.Name == TAG_TYPEPARAMREF) {
                sb.Append(ConvertTypeParamRefNodeToPlainText(child));
            } else if (child.Name == TAG_C) {
                sb.Append(ConvertCNodeToPlainText(child));
            } else if (child.Name == TAG_CODE) {
                sb.Append(ConvertCodeNodeToPlainText(child));
            } else if (child.Name == TAG_XREF) {
                sb.Append(ConvertXrefNodeToPlainText(child));
            } else if (child.Name == TAG_LIST) {
                sb.Append(ConvertListNodeToPlainText(child));
            } else if (child.Name == TAG_LISTHEADER) {
                sb.Append(ConvertListHeaderNodeToPlainText(child));
            } else if (child.Name == TAG_ITEM) {
                sb.Append(ConvertItemNodeToPlainText(child));
            } else if (child.Name == TAG_TERM) {
                sb.Append(ConvertTermNodeToPlainText(child));
            } else if (child.Name == TAG_DESCRIPTION) {
                sb.Append(ConvertDescriptionNodeToPlainText(child));
            } else if (child.Name == TAG_TABLE) {
                sb.Append(ConvertTableNodeToPlainText(child));
            } else if (child.Name == TAG_BR) {
                sb.Append(ConvertBrNodeToPlainText(child));
            } else if (child.Name == TAG_P) {
                sb.Append(ConvertPNodeToPlainText(child));
            } else {
                // throw new NotImplementedException();
                // Console.WriteLine($"Unknown node type: {child.Name}");
                
                sb.Append(ConvertNodeToPlainText(child));
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
        var crefAttr = node.Attributes?["cref"];
        var langwordAttr = node.Attributes?["langword"];
        var hrefAttr = node.Attributes?["href"];

        string? value;
        
        if (crefAttr is not null) {
            value = crefAttr.Value;
        } else if (langwordAttr is not null) {
            value = langwordAttr.Value;
        } else if (hrefAttr is not null) {
            value = hrefAttr.Value;
        } else {
            value = null;
        }

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
    
    private const string TAG_TYPEPARAMREF = "typeparamref";
    private static string ConvertTypeParamRefNodeToPlainText(XmlNode node)
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

    private const string TAG_LIST = "list";
    private static string ConvertListNodeToPlainText(XmlNode node)
    {
        return ConvertNodeToPlainText(node);
    }
    
    private const string TAG_LISTHEADER = "listheader";
    private static string ConvertListHeaderNodeToPlainText(XmlNode node)
    {
        return ConvertNodeToPlainText(node);
    }
    
    private const string TAG_ITEM = "item";
    private static string ConvertItemNodeToPlainText(XmlNode node)
    {
        return ConvertNodeToPlainText(node);
    }
    
    private const string TAG_TERM = "term";
    private static string ConvertTermNodeToPlainText(XmlNode node)
    {
        return ConvertNodeToPlainText(node);
    }
    
    private const string TAG_DESCRIPTION = "description";
    private static string ConvertDescriptionNodeToPlainText(XmlNode node)
    {
        return ConvertNodeToPlainText(node);
    }
    
    private const string TAG_TABLE = "table";
    private static string ConvertTableNodeToPlainText(XmlNode node)
    {
        return ConvertNodeToPlainText(node);
    }
    
    private const string TAG_BR = "br";
    private static string ConvertBrNodeToPlainText(XmlNode node)
    {
        return Environment.NewLine;
    }
    
    private const string TAG_P = "p";
    private static string ConvertPNodeToPlainText(XmlNode node)
    {
        return node.InnerText;
    }
    #endregion Node -> Plain Text
}