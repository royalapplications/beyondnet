using NativeAOT.Core;

namespace NativeAOT.CodeGenerator.Types;

public class TypeDescriptor
{
    public Type ManagedType { get; }

    public bool IsReferenceType => !IsValueType;
    public bool IsValueType => ManagedType.IsValueType;
    public bool IsVoid => ManagedType.Name == "void";

    private readonly Dictionary<CodeLanguage, string> m_typeNames;

    public TypeDescriptor(Type managedType) : this(managedType, null) { }
    
    public TypeDescriptor(Type managedType, Dictionary<CodeLanguage, string>? typeNames)
    {
        ManagedType = managedType;


        if (typeNames == null) {
            typeNames = new();
        }

        if (!typeNames.ContainsKey(CodeLanguage.CSharp)) {
            string csharpTypeName = managedType.FullName ?? managedType.Name;
            
            typeNames[CodeLanguage.CSharp] = csharpTypeName;
        }

        m_typeNames = typeNames;
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