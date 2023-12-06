using System.Reflection;

namespace Beyond.NET.CodeGenerator;

public class XmlDocumentationStore
{
    private static readonly XmlDocumentationStore m_shared = new();
    public static XmlDocumentationStore Shared { get; } = m_shared;

    private readonly HashSet<Assembly> m_assemblies = new();
    private readonly HashSet<XmlDocumentation> m_documentations = new();

    internal HashSet<XmlDocumentation> Documentations => m_documentations;

    public void ParseDocumentation(Assembly assembly)
    {
        if (!m_assemblies.Add(assembly)) {
            return;
        }
        
        var assemblyFilePath = assembly.Location;

        if (!File.Exists(assemblyFilePath)) {
            return;
        }

        var xmlDocumentationFilePath = GetXmlDocumentationFilePath(assemblyFilePath);

        if (string.IsNullOrEmpty(xmlDocumentationFilePath)) {
            return;
        }

        var documentation = new XmlDocumentation();
        documentation.PopulateMembersFromXmlDocumentationPath(xmlDocumentationFilePath);

        m_documentations.Add(documentation);
    }
    
    public void ParseDocumentation(string xmlDocumentationFilePath)
    {
        if (string.IsNullOrEmpty(xmlDocumentationFilePath)) {
            return;
        }

        var documentation = new XmlDocumentation();
        documentation.PopulateMembersFromXmlDocumentationPath(xmlDocumentationFilePath);

        m_documentations.Add(documentation);
    }
    
    public void ParseDocumentation(IEnumerable<string> xmlDocumentationFilePaths)
    {
        foreach (var xmlDocumentationFilePath in xmlDocumentationFilePaths) {
            ParseDocumentation(xmlDocumentationFilePath);
        }
    }

    public void ParseSystemDocumentation()
    {
        var xmlDocumentationFilePaths = GetSystemXmlDocumentationFilePaths();

        ParseDocumentation(xmlDocumentationFilePaths);
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

        if (!File.Exists(xmlFilePath)) {
            return null;
        }

        return xmlFilePath;
    }

    private static IEnumerable<string> GetSystemXmlDocumentationFilePaths()
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