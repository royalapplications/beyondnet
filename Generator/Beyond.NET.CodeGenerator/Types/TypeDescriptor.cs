using System.Diagnostics.CodeAnalysis;
using Beyond.NET.CodeGenerator.Extensions;

namespace Beyond.NET.CodeGenerator.Types;

public class TypeDescriptor
{
    public Type ManagedType { get; }

    public bool IsPrimitive => ManagedType.IsPrimitive;
    public bool IsReferenceType => ManagedType.IsReferenceType() && !IsReadOnlyStructOfByte;
    public bool IsStruct => ManagedType.IsStruct() || IsReadOnlyStructOfByte;
    public bool IsValueType => ManagedType.IsValueType || IsReadOnlyStructOfByte;
    public bool IsEnum => ManagedType.IsEnum;
    public bool IsBool => ManagedType.IsBoolean();
    public bool IsVoid => ManagedType.IsVoid();
    public bool IsDelegate => ManagedType.IsDelegate();
    public bool IsManagedPointer => ManagedType.IsPointer;
    public bool RequiresNativePointer => !IsVoid && !IsEnum && !IsPrimitive && !IsBool && !IsReadOnlyStructOfByte;
    public bool IsReadOnlyStructOfByte => ManagedType.IsReadOnlySpanOfByte();

    public bool IsNullableValueType([NotNullWhen(true)] out Type? valueType)
    {
        return ManagedType.IsNullableValueType(out valueType);
    }

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
            true
        );
    }
    
    public string GetTypeName(
        CodeLanguage language,
        bool includeModifiers,
        bool isOptional
    )
    {
        return GetTypeName(
            language,
            includeModifiers,
            isOptional,
            false,
            false,
            false
        );
    }
    
    public string GetTypeName(
        CodeLanguage language,
        bool includeModifiers,
        bool isOptional,
        bool isOutParameter,
        bool isByRefParameter,
        bool isInParameter
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
                isOptional,
                isOutParameter,
                isByRefParameter,
                isInParameter
            );
        }

        return typeName;
    }

    private string AddModifiersToTypeName(
        string typeName,
        CodeLanguage language,
        bool isOptional,
        bool isOutParameter,
        bool isByRefParameter,
        bool isInParameter
    )
    {
        if (!RequiresNativePointer &&
            !isOutParameter &&
            !isByRefParameter &&
            !isInParameter) {
            return typeName;
        }

        string typeNameWithModifiers;

        switch (language) {
            case CodeLanguage.CSharp:
                if (isOutParameter) {
                    return $"out {typeName}";
                } else if (isInParameter) {
                    return typeName; // $"in {typeName}";
                } else if (isByRefParameter) {
                    return $"ref {typeName}";
                } else {
                    return typeName;
                }
            case CodeLanguage.CSharpUnmanaged:
                if (RequiresNativePointer && 
                    (isOutParameter || isByRefParameter || isInParameter)) {
                    typeNameWithModifiers = $"{typeName}**";
                } else {
                    typeNameWithModifiers = $"{typeName}*";
                }
                
                break;
            case CodeLanguage.C:
                if (isOutParameter || isByRefParameter || isInParameter) {
                    typeNameWithModifiers = $"{typeName}*";
                } else {
                    typeNameWithModifiers = $"{typeName}";
                }
                
                break;
            case CodeLanguage.Swift:
                if (isOutParameter || isByRefParameter || isInParameter) {
                    typeNameWithModifiers = $"inout {typeName}";
                } else {
                    typeNameWithModifiers = $"{typeName}";
                }

                if (isOptional) {
                    typeNameWithModifiers += "?";
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
            case CodeLanguage.Swift:
                string swiftTypeName = ManagedType.CTypeName();
                
                return swiftTypeName;
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
        Type type = ManagedType;

        if (sourceLanguage == CodeLanguage.CSharpUnmanaged &&
            targetLanguage == CodeLanguage.CSharp) {
            if (!RequiresNativePointer) {
                return null;
            }
            
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
            if (!RequiresNativePointer) {
                return null;
            }
            
            string conversion;

            if (type.IsDelegate()) {
                string cTypeName = type.CTypeName();
                
                conversion = $"new {cTypeName}({{0}}).AllocateGCHandleAndGetAddress()";
            } else {
                if (IsNullableValueType(out Type? valueType)) {
                    conversion = "{0}.HasValue ? {0}.Value.AllocateGCHandleAndGetAddress() : default";
                } else {
                    conversion = "{0}.AllocateGCHandleAndGetAddress()";
                }
            }

            return conversion;
        } else if (sourceLanguage == CodeLanguage.C &&
                   targetLanguage == CodeLanguage.Swift) {
            string swiftTypeName = GetTypeName(
                CodeLanguage.Swift,
                false
            );

            if (IsEnum) {
                return $"{swiftTypeName}(cValue: {{0}})";
            } else if (RequiresNativePointer) {
                return $"{swiftTypeName}(handle: {{0}})";
            } else {
                return null;
            }
        } else if (sourceLanguage == CodeLanguage.Swift &&
                   targetLanguage == CodeLanguage.C) {
            if (IsEnum) {
                return "{0}.cValue";
            } else if (RequiresNativePointer) {
                return "{0}.__handle";
            } else {
                return null;
            }
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