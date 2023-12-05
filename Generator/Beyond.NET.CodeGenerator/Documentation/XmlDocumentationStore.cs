using System.Reflection;

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
            // Console.WriteLine($"TODO: XML Documentation found for Assembly named {assembly.GetName().Name} at {xmlFilePath}");
            
            string[] xmlFilePaths = new [] { xmlFilePath };

            return xmlFilePaths;   
        } else {
            // Console.WriteLine($"TODO: No XML Documentation file found for Assembly named {assembly.GetName().Name}");
            
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