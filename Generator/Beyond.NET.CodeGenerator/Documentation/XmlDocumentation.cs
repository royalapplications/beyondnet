using System.Reflection;
using System.Xml;

namespace Beyond.NET.CodeGenerator;

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
                    
                    var memberContent = new XmlDocumentationContent(memberNode);
                    
                    members[memberType] = memberContent;
                }
            }
        } catch (Exception ex) {
            // TODO: Logging
            Console.WriteLine($"Error: {ex}");
        }

        return members;
    }
    #endregion Parsing

    #region Extracting
    internal XmlDocumentationContent? GetDocumentation(XmlDocumentationMemberIdentifier memberIdentifier)
    {
        if (!memberIdentifier.IsValid) {
            return null;
        }

        if (m_members.TryGetValue(memberIdentifier, out XmlDocumentationContent content)) {
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
        foreach (var documentation in XmlDocumentationStore.Shared.Documentations) {
            var typeDocu = documentation.GetDocumentation(new(type));

            if (typeDocu is not null) {
                return typeDocu;
            }
        }
        
        return null;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this FieldInfo fieldInfo)
    {
        foreach (var documentation in XmlDocumentationStore.Shared.Documentations) {
            var typeDocu = documentation.GetDocumentation(new(fieldInfo));

            if (typeDocu is not null) {
                return typeDocu;
            }
        }
        
        return null;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this PropertyInfo propertyInfo)
    {
        foreach (var documentation in XmlDocumentationStore.Shared.Documentations) {
            var typeDocu = documentation.GetDocumentation(new(propertyInfo));

            if (typeDocu is not null) {
                return typeDocu;
            }
        }
        
        return null;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this EventInfo eventInfo)
    {
        foreach (var documentation in XmlDocumentationStore.Shared.Documentations) {
            var typeDocu = documentation.GetDocumentation(new(eventInfo));

            if (typeDocu is not null) {
                return typeDocu;
            }
        }
        
        return null;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this MethodInfo methodInfo)
    {
        foreach (var documentation in XmlDocumentationStore.Shared.Documentations) {
            var typeDocu = documentation.GetDocumentation(new(methodInfo));

            if (typeDocu is not null) {
                return typeDocu;
            }
        }
        
        return null;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this ConstructorInfo constructorInfo)
    {
        foreach (var documentation in XmlDocumentationStore.Shared.Documentations) {
            var typeDocu = documentation.GetDocumentation(new(constructorInfo));

            if (typeDocu is not null) {
                return typeDocu;
            }
        }
        
        return null;
    }
}