using System.Reflection;
using System.Xml;

using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator;

public class XmlDocumentationStore
{
    private ILogger Logger => Services.Shared.LoggerService;
    
    private static readonly Lazy<XmlDocumentationStore> m_shared = new(() => new XmlDocumentationStore());
    public static XmlDocumentationStore Shared => m_shared.Value;

    private readonly HashSet<Assembly> m_assemblies = new();
    private readonly Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationMember> m_members = new();

    // TODO: How to get this dynamically and in a platform-specific way?!
    private string SystemReferenceAssembliesDirectoryPath
        => "/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/8.0.0/ref/net8.0";

    #region Parse Entry Points
    public void ParseDocumentation(Assembly assembly)
    {
        var assemblyName = assembly.GetName().Name ?? "N/A";
        
        if (!m_assemblies.Add(assembly)) {
            Logger.LogDebug($"Skipping parsing documentation for assembly \"{assemblyName}\" because it has already been parsed");
            
            return;
        }
        
        var assemblyFilePath = assembly.Location;
        
        Logger.LogDebug($"Going to parse documentation for assembly \"{assemblyName}\" at \"{assemblyFilePath}\"");

        if (!File.Exists(assemblyFilePath)) {
            Logger.LogDebug($"Assembly does not exist on disk at \"{assemblyFilePath}\" so we cannot parse documentation");
            
            return;
        }

        var xmlDocumentationFilePath = GetXmlDocumentationFilePath(assemblyFilePath);

        if (string.IsNullOrEmpty(xmlDocumentationFilePath)) {
            Logger.LogDebug($"XML documentation file for assembly \"{assemblyName}\" was not found");
            
            return;
        }
        
        Logger.LogDebug($"XML documentation file for assembly \"{assemblyName}\" was found at \"{xmlDocumentationFilePath}\"");
        
        ParseDocumentation(xmlDocumentationFilePath);
    }
    
    public void ParseSystemDocumentation()
    {
        var xmlDocumentationFilePaths = GetSystemXmlDocumentationFilePaths();

        ParseDocumentation(xmlDocumentationFilePaths);
    }

    private void ParseDocumentation(IEnumerable<string> xmlDocumentationFilePaths)
    {
        foreach (var xmlDocumentationFilePath in xmlDocumentationFilePaths) {
            ParseDocumentation(xmlDocumentationFilePath);
        }
    }
    #endregion Parse Entry Points

    #region Parse
    private void ParseDocumentation(string xmlDocumentationFilePath)
    {
        if (string.IsNullOrEmpty(xmlDocumentationFilePath)) {
            return;
        }
        
        Logger.LogInformation($"Parsing XML documentation file at \"{xmlDocumentationFilePath}\"");

        var content = File.ReadAllText(xmlDocumentationFilePath);
        var newMembers = GetXmlDocumentationMembers(content);

        foreach (var newMember in newMembers) {
            m_members[newMember.Key] = newMember.Value;
        }
    }

    private Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationMember> GetXmlDocumentationMembers(string xmlDocumentationContent)
    {
        Dictionary<XmlDocumentationMemberIdentifier, XmlDocumentationMember> members = new();

        try {
            XmlDocument doc = new();
            
            doc.LoadXml(xmlDocumentationContent);
            var root = doc.DocumentElement;
            var memberNodes = root?.SelectNodes("//doc/members/member");

            if (memberNodes is not null) {
                foreach (XmlNode memberNode in memberNodes) {
                    var rawName = memberNode.Attributes?["name"]?.Value;

                    if (string.IsNullOrEmpty(rawName)) {
                        continue;
                    }
                    
                    var memberType = new XmlDocumentationMemberIdentifier(rawName);
                    
                    var memberContent = new XmlDocumentationMember(memberNode);
                    
                    members[memberType] = memberContent;
                }
            }
        } catch (Exception ex) {
            Logger.LogError($"An error occurred while parsing XML documentation: {ex}");
        }

        return members;
    }
    #endregion Parse
    
    #region Extracting
    internal XmlDocumentationMember? GetDocumentation(XmlDocumentationMemberIdentifier memberIdentifier)
    {
        if (!memberIdentifier.IsValid) {
            return null;
        }

        if (m_members.TryGetValue(memberIdentifier, out XmlDocumentationMember content)) {
            return content;
        } else {
            return null;
        }
    }
    #endregion Extracting

    #region Private Helpers
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

        if (!File.Exists(xmlFilePath)) {
            return null;
        }

        return xmlFilePath;
    }

    private IEnumerable<string> GetSystemXmlDocumentationFilePaths()
    {
        var refPath = SystemReferenceAssembliesDirectoryPath;

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
    #endregion Private Helpers
}