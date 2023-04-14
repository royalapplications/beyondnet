using NativeAOT.CodeGenerator.Extensions;

namespace NativeAOT.CodeGenerator.Types;

public class TypeDescriptor
{
    public Type ManagedType { get; }

    public bool IsPrimitive => ManagedType.IsPrimitive;
    public bool IsReferenceType => ManagedType.IsReferenceType();
    public bool IsStruct => ManagedType.IsStruct();
    public bool IsValueType => ManagedType.IsValueType;
    public bool IsEnum => ManagedType.IsEnum;
    public bool IsBool => ManagedType.IsBoolean();
    public bool IsVoid => ManagedType.IsVoid();
    public bool IsDelegate => ManagedType.IsDelegate();
    public bool IsManagedPointer => ManagedType.IsPointer;
    public bool RequiresNativePointer => !IsVoid && !IsEnum && !IsPrimitive && !IsBool; 

    private readonly Dictionary<CodeLanguage, string> m_typeNames;
    private readonly Dictionary<LanguagePair, string> m_typeConversions;
    private readonly string? m_defaultValue;
    private readonly Dictionary<CodeLanguage, string> m_destructors;

    public TypeDescriptor(
        Type managedType,
        string? defaultValue = null,
        Dictionary<CodeLanguage, string>? typeNames = null,
        Dictionary<LanguagePair, string>? typeConversions = null,
        Dictionary<CodeLanguage, string>? destructors = null
    )
    {
        ManagedType = managedType;

        m_defaultValue = defaultValue;
        
        if (typeNames == null) {
            typeNames = new();
        }

        if (!typeNames.ContainsKey(CodeLanguage.CSharp)) {
            string csharpTypeName = managedType.GetFullNameOrName();
            
            typeNames[CodeLanguage.CSharp] = csharpTypeName;
        }

        m_typeNames = typeNames;

        if (typeConversions == null) {
            typeConversions = new();
        }
        
        m_typeConversions = typeConversions;

        if (destructors == null) {
            destructors = new();
        }

        m_destructors = destructors;
    }

    public void SetTypeName(string typeName, CodeLanguage language)
    {
        m_typeNames[language] = typeName;
    }

    public string GetTypeName(
        CodeLanguage language,
        bool includeModifiers
    )
    {
        return GetTypeName(
            language,
            includeModifiers,
            false,
            false
        );
    }
    
    public string GetTypeName(
        CodeLanguage language,
        bool includeModifiers,
        bool isOutParameter,
        bool isByRefParameter
    )
    {
        string typeName;
        
        if (m_typeNames.TryGetValue(language, out string? tempTypeName)) {
            typeName = tempTypeName;
        } else {
            typeName = GenerateTypeName(language);

            m_typeNames[language] = typeName;
        }

        if (includeModifiers) {
            typeName = AddModifiersToTypeName(
                typeName,
                language,
                isOutParameter,
                isByRefParameter
            );
        }

        return typeName;
    }

    private string AddModifiersToTypeName(
        string typeName,
        CodeLanguage language,
        bool isOutParameter,
        bool isByRefParameter
    )
    {
        if (!RequiresNativePointer &&
            !isOutParameter &&
            !isByRefParameter) {
            return typeName;
        }

        string typeNameWithModifiers;

        switch (language) {
            case CodeLanguage.CSharp:
                if (isOutParameter) {
                    return $"out {typeName}";
                } else if (isByRefParameter) {
                    return $"ref {typeName}";
                } else {
                    return typeName;
                }
            case CodeLanguage.CSharpUnmanaged:
                if (RequiresNativePointer && 
                    (isOutParameter || isByRefParameter)) {
                    typeNameWithModifiers = $"{typeName}**";
                } else {
                    typeNameWithModifiers = $"{typeName}*";
                }
                
                break;
            case CodeLanguage.C:
                if (isOutParameter || isByRefParameter) {
                    typeNameWithModifiers = $"{typeName}*";
                } else {
                    typeNameWithModifiers = $"{typeName}";
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
                if (RequiresNativePointer) {
                    return "void";
                } else {
                    return ManagedType.GetFullNameOrName();
                }
            case CodeLanguage.C:
                string cTypeName = ManagedType.CTypeName();
                string constructedCTypeName;
                
                if (IsReferenceType ||
                    IsStruct) {
                    constructedCTypeName = $"{cTypeName}_t";    
                } else if (IsEnum) {
                    constructedCTypeName = $"{cTypeName}_t";
                } else {
                    throw new Exception("Unknown kind of type");
                }

                return constructedCTypeName;
            default:
                throw new NotImplementedException();
        }
    }

    public string? GetTypeConversion(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage
    )
    {
        LanguagePair languagePair = new(sourceLanguage, targetLanguage);
        
        m_typeConversions.TryGetValue(
            languagePair,
            out string? typeConversion
        );

        if (typeConversion == null) {
            typeConversion = GenerateTypeConversion(sourceLanguage, targetLanguage);

            if (typeConversion != null) {
                m_typeConversions[languagePair] = typeConversion;
            }
        }

        return typeConversion;
    }
    
    private string? GenerateTypeConversion(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage
    )
    {
        if (!RequiresNativePointer) {
            return null;
        }

        Type type = ManagedType;

        if (sourceLanguage == CodeLanguage.CSharpUnmanaged &&
            targetLanguage == CodeLanguage.CSharp) {
            string conversion;

            if (type.IsDelegate()) {
                string cTypeName = type.CTypeName();

                conversion = "InteropUtils.GetInstance<" + cTypeName + ">({0})?.Trampoline";
            } else {
                string typeName = type.GetFullNameOrName();

                conversion = "InteropUtils.GetInstance<" + typeName + ">({0})";
            }

            return conversion;
        } else if (sourceLanguage == CodeLanguage.CSharp &&
                   targetLanguage == CodeLanguage.CSharpUnmanaged) {
            string conversion;

            if (type.IsDelegate()) {
                string cTypeName = type.CTypeName();
                
                conversion = $"new {cTypeName}({{0}}).AllocateGCHandleAndGetAddress()";
            } else {
                conversion = "{0}.AllocateGCHandleAndGetAddress()";
            }

            return conversion;
        } else {
            throw new Exception("Unknown language pair");
        }
    }

    public string? GetDestructor(CodeLanguage language)
    {
        string? destructor;
        
        if (m_destructors.TryGetValue(language, out string? tempDestructor)) {
            destructor = tempDestructor;
        } else {
            destructor = GenerateDestructor(language);

            if (destructor != null) {
                m_destructors[language] = destructor;
            }
        }

        return destructor;
    }

    private string? GenerateDestructor(CodeLanguage language)
    {
        if (!RequiresNativePointer) {
            return string.Empty;
        }

        switch (language) {
            case CodeLanguage.CSharpUnmanaged:
                return "InteropUtils.FreeIfAllocated({0})";
        }

        return null;
    }

    public string? GetDefaultValue()
    {
        if (m_defaultValue != null) {
            return m_defaultValue;
        }

        if (RequiresNativePointer) {
            return "null";
        }

        return null;
    }
}