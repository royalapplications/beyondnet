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
        
        var assemblyFilePath = assembly.Location;

        XmlDocumentation? newDocumentation = null;
        
        if (File.Exists(assemblyFilePath)) {
            var xmlDocumentationFilePaths = GetXmlDocumentationFilePaths(
                assembly,
                assemblyFilePath
            );

            foreach (var xmlDocumentationFilePath in xmlDocumentationFilePaths) {
                if (!File.Exists(xmlDocumentationFilePath)) {
                    continue;
                }
                
                newDocumentation ??= new XmlDocumentation();
                newDocumentation.PopulateMembersFromXmlDocumentationPath(xmlDocumentationFilePath);
            }
        }

        AssemblyDocumentations[assembly] = newDocumentation;

        return newDocumentation;
    }
    
    private static IEnumerable<string> GetXmlDocumentationFilePaths(
        Assembly assembly,
        string assemblyFilePath
    )
    {
        var assemblyName = Path.GetFileNameWithoutExtension(assemblyFilePath);
        var assemblyDir = Path.GetDirectoryName(assemblyFilePath);

        if (string.IsNullOrEmpty(assemblyDir)) {
            return Array.Empty<string>();
        }
        
        var xmlFileName = assemblyName + ".xml"; 
        
        var xmlFilePath = Path.Combine(
            assemblyDir,
            xmlFileName
        );

        if (File.Exists(xmlFilePath)) {
            string[] xmlFilePaths = new [] { xmlFilePath };

            return xmlFilePaths;   
        } else {
            bool isSystemPrivateCoreLib = assembly.GetName().Name == "System.Private.CoreLib" &&
                                          assembly.ExportedTypes.Contains(typeof(object));

            if (isSystemPrivateCoreLib) {
                return GetSystemXmlDocumentationFilePaths(
                    assembly,
                    assemblyFilePath
                );
            } else {
                return Array.Empty<string>();
            }
        }
    }

    private static IEnumerable<string> GetSystemXmlDocumentationFilePaths(
        Assembly systemPrivateCoreLibAssembly,
        string assemblyFilePath
    )
    {
        // var name = systemPrivateCoreLibAssembly.GetName();
        // var version = name.Version;
        
        // TODO: How to get this dynamically?!
        var refPath = "/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/8.0.0/ref/net8.0";

        if (!Directory.Exists(refPath)) {
            return Array.Empty<string>();
        }

        var xmlFilePaths = Directory.GetFiles(
            refPath,
            "*.xml",
            SearchOption.AllDirectories
        );

        return xmlFilePaths;
    }
}

public class XmlDocumentation
{
    private readonly Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationContent> m_members;

    internal XmlDocumentation()
    {
        m_members = new();
    }

    #region Parsing
    internal void PopulateMembersFromXmlDocumentationPath(string xmlDocumentationFilePath)
    {
        var content = File.ReadAllText(xmlDocumentationFilePath);
        var newMembers = GetXmlDocumentationMembers(content);

        foreach (var newMember in newMembers) {
            m_members[newMember.Key] = newMember.Value;
        }
    }

    private static Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationContent> GetXmlDocumentationMembers(string xmlDocumentationContent)
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

        return members;
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