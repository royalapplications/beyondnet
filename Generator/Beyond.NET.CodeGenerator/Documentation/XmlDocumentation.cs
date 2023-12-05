using System.Reflection;
using System.Xml;

namespace Beyond.NET.CodeGenerator;

internal class XmlDocumentationStore
{
    private static readonly XmlDocumentationStore m_shared = new();
    internal static XmlDocumentationStore Shared { get; } = m_shared;

    private Dictionary<Assembly, XmlDocumentation?> AssemblyDocumentations { get; } = new();

    internal XmlDocumentation? GetDocumentation(Assembly assembly)
    {
        if (AssemblyDocumentations.TryGetValue(assembly, out XmlDocumentation? existingDocumentation)) {
            return existingDocumentation;
        }

        var newDocumentation = XmlDocumentation.FromAssembly(assembly);

        AssemblyDocumentations[assembly] = newDocumentation;

        return newDocumentation;
    }
}

// TODO: Right now this only gets documentation from the targeted assembly but we must also make it so it's able to add documentation from multiple Assemblies when they are loaded by the AssemblyLoader's AppDomain_OnAssemblyResolve method.
public class XmlDocumentation
{
    private readonly Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationContent> m_members;

    private XmlDocumentation(Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationContent> members)
    {
        m_members = members ?? throw new ArgumentNullException(nameof(members));
    }

    #region Parsing
    internal static XmlDocumentation? FromAssembly(Assembly assembly)
    {
        string assemblyFilePath = assembly.Location;

        var docu = FromAssemblyPath(assemblyFilePath);

        return docu;
    }
    
    internal static XmlDocumentation? FromAssemblyPath(string assemblyFilePath)
    {
        if (!File.Exists(assemblyFilePath)) {
            return null;
        }
        
        var xmlFilePath = GetXmlDocumentationFilePath(assemblyFilePath);
        
        if (!File.Exists(xmlFilePath)) {
            return null;
        }

        var content = File.ReadAllText(xmlFilePath);
        var docu = ParseXmlDocumentation(content);

        return docu;
    }
    
    private static string? GetXmlDocumentationFilePath(string assemblyFilePath)
    {
        var assemblyName = Path.GetFileNameWithoutExtension(assemblyFilePath);
        var assemblyDir = Path.GetDirectoryName(assemblyFilePath);

        if (string.IsNullOrEmpty(assemblyDir)) {
            return null;
        }
        
        var xmlFileName = assemblyName + ".xml"; 
        
        var xmlFilePath = Path.Combine(
            assemblyDir,
            xmlFileName
        );

        return xmlFilePath;
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
    internal XmlDocumentationContent? GetTypeDocumentation(Type type)
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

public static class XmlDocumentation_Extensions
{
    public static XmlDocumentationContent? GetDocumentation(this Type type)
    {
        var docu = XmlDocumentationStore.Shared.GetDocumentation(type.Assembly);
        var typeDocu = docu?.GetTypeDocumentation(type);

        return typeDocu;
    }
}