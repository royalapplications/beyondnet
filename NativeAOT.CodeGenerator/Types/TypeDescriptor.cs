namespace NativeAOT.CodeGenerator.Types;

public class TypeDescriptor
{
    public Type ManagedType { get; }

    public bool IsPrimitive => ManagedType.IsPrimitive;
    public bool IsReferenceType => !IsValueType;
    public bool IsValueType => ManagedType.IsValueType;
    public bool IsEnum => ManagedType.IsEnum;
    public bool IsBool => ManagedType == typeof(bool);
    public bool IsVoid => ManagedType.Name == "void";
    public bool IsPointer => !IsVoid && !IsEnum && !IsPrimitive && !IsBool; 

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

        if (includeModifiers) {
            typeName = AddModifiersToTypeName(typeName, language, isOutParameter);
        }

        return typeName;
    }

    private string AddModifiersToTypeName(string typeName, CodeLanguage language, bool isOutParameter)
    {
        if (!IsPointer &&
            !isOutParameter) {
            return typeName;
        }

        string typeNameWithModifiers;

        switch (language) {
            case CodeLanguage.CSharpUnmanaged:
                if (IsPointer && 
                    isOutParameter) {
                    typeNameWithModifiers = $"{typeName}**";
                } else {
                    typeNameWithModifiers = $"{typeName}*";
                }
                
                break;
            case CodeLanguage.C:
                if (IsPointer && 
                    isOutParameter) {
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
                if (IsPointer) {
                    return "void";
                } else {
                    return ManagedType.FullName ?? ManagedType.Name;
                }
            case CodeLanguage.C:
                string typeName = ManagedType.FullName ?? ManagedType.Name;
                string cTypeName = $"{typeName.Replace('.', '_')}_t"; 
                
                return cTypeName;
            default:
                throw new NotImplementedException();
        }
    }
}