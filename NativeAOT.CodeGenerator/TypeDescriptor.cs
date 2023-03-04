using NativeAOT.Core;

namespace NativeAOT.CodeGenerator;

public class TypeDescriptor
{
    public Type ManagedType { get; }

    private readonly Dictionary<CodeLanguage, string> m_typeNames;

    public TypeDescriptor(Type managedType)
    {
        ManagedType = managedType;

        string csharpTypeName = managedType.FullName ?? managedType.Name;
        
        m_typeNames = new() {
            { CodeLanguage.CSharp, csharpTypeName }
        };
    }

    public void SetTypeName(string typeName, CodeLanguage language)
    {
        m_typeNames[language] = typeName;
    }

    public string? GetTypeName(CodeLanguage language)
    {
        if (m_typeNames.TryGetValue(language, out string? typeName)) {
            return typeName;
        } else {
            return null;
        }
    }
}