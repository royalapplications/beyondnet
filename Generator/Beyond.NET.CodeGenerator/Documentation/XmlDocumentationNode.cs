using System.Xml;

namespace Beyond.NET.CodeGenerator;

internal struct XmlDocumentationNode
{
    private XmlNode MemberNode { get; }
    
    internal XmlNode? SummaryNode => MemberNode.SelectSingleNode(".//summary");
    internal XmlNode? ReturnsNode => MemberNode.SelectSingleNode(".//returns");
    internal XmlNodeList? ParamNodes => MemberNode.SelectNodes(".//param");
    internal XmlNodeList? ExceptionNodes => MemberNode.SelectNodes(".//exception");
    
    internal XmlDocumentationNode(XmlNode xmlMemberNode)
    {
        MemberNode = xmlMemberNode;
    }

    public override string ToString()
    {
        return MemberNode.InnerXml;
    }
}