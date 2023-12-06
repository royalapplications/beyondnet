using System.Reflection;

namespace Beyond.NET.CodeGenerator;

public static class XmlDocumentationReflectionExtensions
{
    private static XmlDocumentationStore Store => XmlDocumentationStore.Shared;
    
    public static XmlDocumentationMember? GetDocumentation(this Type type) 
        => Store.GetDocumentation(new(type));
    
    public static XmlDocumentationMember? GetDocumentation(this FieldInfo fieldInfo)
        => Store.GetDocumentation(new(fieldInfo));
    
    public static XmlDocumentationMember? GetDocumentation(this PropertyInfo propertyInfo)
        => Store.GetDocumentation(new(propertyInfo));
    
    public static XmlDocumentationMember? GetDocumentation(this EventInfo eventInfo)
        => Store.GetDocumentation(new(eventInfo));
    
    public static XmlDocumentationMember? GetDocumentation(this MethodInfo methodInfo)
        => Store.GetDocumentation(new(methodInfo));
    
    public static XmlDocumentationMember? GetDocumentation(this ConstructorInfo constructorInfo)
        => Store.GetDocumentation(new(constructorInfo));
}