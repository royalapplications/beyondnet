using System.Xml;

namespace Beyond.NET.CodeGenerator;

// TODO: Right now this only gets documentation from the targeted assembly but we must also make it so it's able to add documentation from multiple Assemblies when they are loaded by the AssemblyLoader's AppDomain_OnAssemblyResolve method.
public class XmlDocumentation
{
    private readonly Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationContent> m_members;

    private XmlDocumentation(Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationContent> members)
    {
        m_members = members ?? throw new ArgumentNullException(nameof(members));
    }

    #region Parsing
    public static XmlDocumentation? FromAssemblyPath(string assemblyPath)
    {
        var assemblyName = Path.GetFileNameWithoutExtension(assemblyPath);
        var assemblyDir = Path.GetDirectoryName(assemblyPath);

        if (string.IsNullOrEmpty(assemblyDir)) {
            return null;
        }
        
        var xmlFileName = assemblyName + ".xml"; 
        
        var xmlFilePath = Path.Combine(
            assemblyDir,
            xmlFileName
        );
        
        if (!File.Exists(xmlFilePath)) {
            return null;
        }

        var content = File.ReadAllText(xmlFilePath);
        var docu = ParseXmlDocumentation(content);

        return docu;
    }

    private static XmlDocumentation ParseXmlDocumentation(string xmlDocumentationContent)
    {
        Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationContent> members = new();
        
        using XmlReader xmlReader = XmlReader.Create(new StringReader(xmlDocumentationContent));
        
        while (xmlReader.Read()) {
            if (xmlReader.NodeType == XmlNodeType.Element &&
                xmlReader.Name == "member") {
                var rawName = xmlReader["name"];

                if (string.IsNullOrEmpty(rawName)) {
                    continue;
                }

                var memberType = new XmlDocumentationMemberIdentifier(rawName);
                var memberXml = xmlReader.ReadInnerXml();
                var memberContent = new XmlDocumentationContent(memberXml);

                members[memberType] = memberContent;
            }
        }

        var docu = new XmlDocumentation(members);

        return docu;
    }
    #endregion Parsing

    #region Extracting
    public XmlDocumentationContent? GetTypeDocumentation(Type type)
    {
        var rawName = $"T:{type.FullName}";
        var identifier = new XmlDocumentationMemberIdentifier(rawName);

        if (m_members.TryGetValue(identifier, out XmlDocumentationContent content)) {
            return content;
        } else {
            return null;
        }
    }
    #endregion Extracting
}