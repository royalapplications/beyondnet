using System.Reflection;
using System.Text.RegularExpressions;

namespace Beyond.NET.CodeGenerator;

internal struct XmlDocumentationMemberIdentifier
{
    private readonly string m_key;
    internal bool IsValid => !string.IsNullOrEmpty(m_key);

    private const string IDENTIFIER_NAMESPACE = "N";
    private const string IDENTIFIER_TYPE = "T";
    private const string IDENTIFIER_FIELD = "F";
    private const string IDENTIFIER_PROPERTY = "P";
    private const string IDENTIFIER_METHOD = "M";
    private const string IDENTIFIER_EVENT = "E";
    private const string IDENTIFIER_ERRORSTRING = "!";

    internal XmlDocumentationMemberIdentifier(string key)
    {
        m_key = key ?? throw new ArgumentNullException(nameof(key));
    }

    internal XmlDocumentationMemberIdentifier(Type type)
    {
        if (type.IsArray) {
            m_key = string.Empty;
            
            return;
        }
        
        var typeFullName = type.FullName;

        if (string.IsNullOrEmpty(typeFullName)) {
            m_key = string.Empty;
            
            return;
        }
        
        m_key = $"{IDENTIFIER_TYPE}:{XmlDocumentationKeyHelper(typeFullName)}";
    }
    
    internal XmlDocumentationMemberIdentifier(FieldInfo field)
    {
        var type = field.DeclaringType;

        if (type is null) {
            m_key = string.Empty;
            
            return;
        }
        
        var typeFullName = type.FullName;

        if (string.IsNullOrEmpty(typeFullName)) {
            m_key = string.Empty;
            
            return;
        }

        var memberName = field.Name;

        m_key = $"{IDENTIFIER_FIELD}:{XmlDocumentationKeyHelper(typeFullName, memberName)}";
    }
    
    internal XmlDocumentationMemberIdentifier(PropertyInfo property)
    {
        var type = property.DeclaringType;

        if (type is null) {
            m_key = string.Empty;
            
            return;
        }
        
        var typeFullName = type.FullName;

        if (string.IsNullOrEmpty(typeFullName)) {
            m_key = string.Empty;
            
            return;
        }

        var memberName = property.Name;

        m_key = $"{IDENTIFIER_PROPERTY}:{XmlDocumentationKeyHelper(typeFullName, memberName)}";
    }
    
    private static string XmlDocumentationKeyHelper(
        string fullTypeName,
        string? memberName = null
    ) {
        string key = Regex.Replace(
            fullTypeName, 
            @"\[.*\]",
            string.Empty
        ).Replace('+', '.');

        if (!string.IsNullOrEmpty(memberName)) {
            key += "." + memberName;
        }
        
        return key;
    }

    public override string ToString()
    {
        return m_key;
    }
}