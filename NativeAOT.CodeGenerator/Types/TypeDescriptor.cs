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

    public string GetTypeName(CodeLanguage language, bool includeModifiers)
    {
        return GetTypeName(language, includeModifiers, false);
    }
    
    public string GetTypeName(CodeLanguage language, bool includeModifiers, bool isOutParameter)
    {
        string typeName;
        
        if (m_typeNames.TryGetValue(language, out string? tempTypeName)) {
            typeName = tempTypeName;
        } else {
            typeName = GenerateTypeName(language);

            m_typeNames[language] = typeName;
        }

        if (includeModifiers &&
            IsReferenceType &&
            !IsVoid) {
            typeName = AddModifiersToTypeName(typeName, language, isOutParameter);
        }

        return typeName;
    }

    private string AddModifiersToTypeName(string typeName, CodeLanguage language, bool isOutParameter)
    {
        if (!IsReferenceType ||
            IsVoid) {
            return typeName;
        }

        string typeNameWithModifiers;

        switch (language) {
            case CodeLanguage.CSharpUnmanaged:
                if (isOutParameter) {
                    typeNameWithModifiers = $"{typeName}**";
                } else {
                    typeNameWithModifiers = $"{typeName}*";
                }
                
                break;
            case CodeLanguage.C:
                if (isOutParameter) {
                    typeNameWithModifiers = $"{typeName}**";
                } else {
                    typeNameWithModifiers = $"{typeName}*";
                }
                
                break;
            default:
                throw new NotImplementedException();
        }

        return typeNameWithModifiers;
    }

    private string GenerateTypeName(CodeLanguage language)
    {
        switch (language) {
            case CodeLanguage.CSharpUnmanaged:
                if (IsReferenceType) {
                    return "void";
                } else {
                    // TODO: Why?
                    return ManagedType.Name;
                }
            case CodeLanguage.C:
                return $"{ManagedType.Name}_t";
            default:
                throw new NotImplementedException();
        }
    }
}