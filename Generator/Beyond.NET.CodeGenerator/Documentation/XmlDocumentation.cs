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
        Assembly assembly = type.Assembly;
        
        var docu = XmlDocumentationStore.Shared.GetDocumentation(assembly);
        var typeDocu = docu?.GetDocumentation(new(type));

        return typeDocu;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this FieldInfo fieldInfo)
    {
        var assembly = fieldInfo.DeclaringType?.Assembly;

        if (assembly is null) {
            return null;
        }
        
        var docu = XmlDocumentationStore.Shared.GetDocumentation(assembly);
        var memberDocu = docu?.GetDocumentation(new(fieldInfo));

        return memberDocu;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this PropertyInfo propertyInfo)
    {
        var assembly = propertyInfo.DeclaringType?.Assembly;

        if (assembly is null) {
            return null;
        }
        
        var docu = XmlDocumentationStore.Shared.GetDocumentation(assembly);
        var memberDocu = docu?.GetDocumentation(new(propertyInfo));

        return memberDocu;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this EventInfo eventInfo)
    {
        var assembly = eventInfo.DeclaringType?.Assembly;

        if (assembly is null) {
            return null;
        }
        
        var docu = XmlDocumentationStore.Shared.GetDocumentation(assembly);
        var memberDocu = docu?.GetDocumentation(new(eventInfo));

        return memberDocu;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this ConstructorInfo constructorInfo)
    {
        var assembly = constructorInfo.DeclaringType?.Assembly;

        if (assembly is null) {
            return null;
        }
        
        var docu = XmlDocumentationStore.Shared.GetDocumentation(assembly);
        var memberDocu = docu?.GetDocumentation(new(constructorInfo));

        return memberDocu;
    }
    
    public static XmlDocumentationContent? GetDocumentation(this MethodInfo methodInfo)
    {
        var assembly = methodInfo.DeclaringType?.Assembly;

        if (assembly is null) {
            return null;
        }
        
        var docu = XmlDocumentationStore.Shared.GetDocumentation(assembly);
        var memberDocu = docu?.GetDocumentation(new(methodInfo));

        return memberDocu;
    }
}