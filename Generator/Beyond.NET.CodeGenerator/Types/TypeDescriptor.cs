using System.Diagnostics.CodeAnalysis;
using Beyond.NET.CodeGenerator.Extensions;

namespace Beyond.NET.CodeGenerator.Types;

public enum Nullability
{
    NotSpecified,
    Nullable,
    NonNullable
}

public static class Nullability_Extensions
{
    public const string ClangAttributeNullable = "_Nullable";
    public const string ClangAttributeNonNull = "_Nonnull";
    
    public static string GetClangAttribute(this Nullability nullability)
    {
        switch (nullability) {
            case Nullability.Nullable:
                return ClangAttributeNullable;
            case Nullability.NonNullable:
                return ClangAttributeNonNull;
            default:
                return string.Empty; 
        }
    }
    
    public static string GetSwiftOptionalitySpecifier(this Nullability nullability)
    {
        switch (nullability) {
            case Nullability.Nullable:
                return "?";
            default:
                return string.Empty; 
        }
    }
    
    public static string GetKotlinOptionalitySpecifier(this Nullability nullability)
    {
        switch (nullability) {
            case Nullability.Nullable:
                return "?";
            default:
                return string.Empty; 
        }
    }
}

public class TypeDescriptor
{
    public static string SwiftDotNETInterfaceImplementationSuffix = "_DNInterface";
    public static string KotlinDotNETInterfaceImplementationSuffix = "_DNInterface";
    
    public Type ManagedType { get; }

    public bool IsPrimitive => ManagedType.IsPrimitive;
    public bool IsReferenceType => ManagedType.IsReferenceType() && !IsReadOnlyStructOfByte;
    public bool IsStruct => ManagedType.IsStruct() || IsReadOnlyStructOfByte;
    public bool IsValueType => ManagedType.IsValueType || IsReadOnlyStructOfByte;
    public bool IsInterface => ManagedType.IsInterface;
    public bool IsEnum => ManagedType.IsEnum;
    public bool IsBool => ManagedType.IsBoolean();
    public bool IsArray => ManagedType.IsArray;
    public bool IsVoid => ManagedType.IsVoid();
    public bool IsDelegate => ManagedType.IsDelegate();
    public bool IsManagedPointer => ManagedType.IsPointer;
    public bool RequiresNativePointer => !IsVoid && !IsEnum && !IsPrimitive && !IsBool && !IsReadOnlyStructOfByte;
    public bool IsReadOnlyStructOfByte => ManagedType.IsReadOnlySpanOfByte();
    
    public bool IsNullableValueType([NotNullWhen(true)] out Type? valueType)
    {
        return ManagedType.IsNullableValueType(out valueType);
    }

    public Nullability Nullability
    {
        get {
            if ((RequiresNativePointer && !IsStruct) ||
                IsNullableValueType(out _)) {
                return Nullability.Nullable;
            } else if (IsStruct) {
                return Nullability.NonNullable;
            }

            return Nullability.NotSpecified;
        }
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
        bool includeModifiers,
        Nullability nullability = Nullability.NotSpecified,
        Nullability arrayElementNullability = Nullability.NotSpecified,
        bool isOutParameter = false,
        bool isByRefParameter = false,
        bool isInParameter = false
    )
    {
        string typeName;

        bool isSwift = language == CodeLanguage.Swift;
        bool isKotlin = language == CodeLanguage.Kotlin;

        bool isAllowedToCache = !((isSwift || isKotlin) &&
                                  IsArray);
        
        // Cannot cache array types because of element nullability
        if (isAllowedToCache &&
            m_typeNames.TryGetValue(language, out string? tempTypeName)) {
            typeName = tempTypeName;
        } else {
            typeName = GenerateTypeName(
                language,
                arrayElementNullability
            );

            if (isAllowedToCache) {
                m_typeNames[language] = typeName;
            }
        }

        if (includeModifiers) {
            typeName = AddModifiersToTypeName(
                typeName,
                language,
                nullability,
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
        Nullability nullability,
        bool isOutParameter,
        bool isByRefParameter,
        bool isInParameter
    )
    {
        if (nullability == Nullability.NotSpecified) {
            nullability = Nullability;
        }
        
        if (!RequiresNativePointer &&
            !isOutParameter &&
            !isByRefParameter &&
            !isInParameter) {
            return typeName;
        }

        string typeNameWithModifiers;

        switch (language) {
            case CodeLanguage.CSharp:
            {
                if (isOutParameter) {
                    return $"out {typeName}";
                } else if (isInParameter) {
                    return typeName; // $"in {typeName}";
                } else if (isByRefParameter) {
                    return $"ref {typeName}";
                } else {
                    return typeName;
                }
            }
            case CodeLanguage.CSharpUnmanaged:
            {
                if (RequiresNativePointer &&
                    (isOutParameter || isByRefParameter || isInParameter)) {
                    typeNameWithModifiers = $"{typeName}**";
                } else {
                    typeNameWithModifiers = $"{typeName}*";
                }

                break;
            }
            case CodeLanguage.C:
            {
                string cNullabilitySpecifier = nullability.GetClangAttribute();

                if (isOutParameter || isByRefParameter || isInParameter) {
                    string innerTypeName = typeName;

                    if (!string.IsNullOrEmpty(cNullabilitySpecifier) &&
                        !IsReadOnlyStructOfByte) {
                        innerTypeName += " " + cNullabilitySpecifier;
                    }

                    typeNameWithModifiers = $"{innerTypeName}*";
                } else {
                    typeNameWithModifiers = $"{typeName}";
                }

                if (!string.IsNullOrEmpty(cNullabilitySpecifier)) {
                    typeNameWithModifiers += " " + cNullabilitySpecifier;
                }

                break;
            }
            case CodeLanguage.Swift:
            {
                if (isOutParameter || isByRefParameter || isInParameter) {
                    typeNameWithModifiers = $"inout {typeName}";
                } else {
                    typeNameWithModifiers = $"{typeName}";
                }

                string swiftNullabilitySpecifier = nullability.GetSwiftOptionalitySpecifier();

                if (!string.IsNullOrEmpty(swiftNullabilitySpecifier)) {
                    typeNameWithModifiers += swiftNullabilitySpecifier;
                }

                break;
            }
            case CodeLanguage.KotlinJNA:
            {
                if (isOutParameter || isByRefParameter || isInParameter) {
                    string nonByRefTypeName;

                    var nonByRefTypeDescriptor = ManagedType.GetNonByRefType().GetTypeDescriptor();

                    if (nonByRefTypeDescriptor.IsPrimitive) {
                        nonByRefTypeName = nonByRefTypeDescriptor.GetTypeName(CodeLanguage.Kotlin, false);                        
                    } else {
                        nonByRefTypeName = typeName;
                    }
                    
                    typeNameWithModifiers = $"{nonByRefTypeName}ByReference";
                } else {
                    typeNameWithModifiers = $"{typeName}";
                }

                string kotlinNullabilitySpecifier;

                // if (RequiresNativePointer || IsReadOnlyStructOfByte) {
                //     kotlinNullabilitySpecifier = string.Empty;
                // } else {
                    kotlinNullabilitySpecifier = nullability.GetKotlinOptionalitySpecifier();
                // }

                if (!string.IsNullOrEmpty(kotlinNullabilitySpecifier)) {
                    typeNameWithModifiers += kotlinNullabilitySpecifier;
                }

                break;
            }
            case CodeLanguage.Kotlin:
            {
                string kotlinNullabilitySpecifier = nullability.GetKotlinOptionalitySpecifier();
                
                if (isOutParameter || isByRefParameter || isInParameter) {
                    var kotlinTypeName = GetTypeName(CodeLanguage.Kotlin, false);
                    string refTypeName;

                    if (IsPrimitive) {
                        if (IsEnum) {
                            throw new NotSupportedException("By ref enums are currently not supported when generating Kotlin code");
                        } else {
                            refTypeName = $"{kotlinTypeName}Ref";
                        }
                    } else {
                        refTypeName = $"ObjectRef<{kotlinTypeName}{kotlinNullabilitySpecifier}>";
                    }

                    typeNameWithModifiers = refTypeName;
                } else {
                    typeNameWithModifiers = $"{typeName}{kotlinNullabilitySpecifier}";
                }

                break;
            }
            default:
                throw new NotImplementedException();
        }

        return typeNameWithModifiers;
    }

    private string GenerateTypeName(
        CodeLanguage language,
        Nullability arrayElementNullability
    )
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
            case CodeLanguage.Swift: {
                bool isArray = ManagedType.IsArray;

                Type? elementType = isArray
                    ? ManagedType.GetElementType()
                    : null;

                string swiftTypeName;

                if (isArray &&
                    elementType is not null)
                {
                    // TODO: Shouldn't this go through TypeDescriptor?
                    var swiftElementTypeName = elementType.CTypeName();
                    var rank = ManagedType.GetArrayRank();

                    if (arrayElementNullability == Nullability.NotSpecified)
                    {
                        arrayElementNullability = Nullability.Nullable;
                    }

                    string arrayTypeName;

                    if (rank == 1)
                    {
                        // Single-dimensional array
                        arrayTypeName = arrayElementNullability == Nullability.NonNullable
                            ? "DNArray"
                            : "DNNullableArray";
                    }
                    else if (rank > 1)
                    {
                        // Multidimensional array
                        arrayTypeName = arrayElementNullability == Nullability.NonNullable
                            ? "DNMultidimensionalArray"
                            : "DNNullableMultidimensionalArray";
                    }
                    else
                    {
                        throw new Exception($"An array rank of {rank} doesn't really make sense, right?");
                    }

                    swiftTypeName = $"{arrayTypeName}<{swiftElementTypeName}>";
                }
                else
                {
                    swiftTypeName = ManagedType.CTypeName();
                }

                return swiftTypeName;
            }
            case CodeLanguage.KotlinJNA: {
                if (RequiresNativePointer || IsReadOnlyStructOfByte)
                {
                    return "Pointer";
                }
                else if (IsEnum)
                {
                    var enumUnderlyingType = ManagedType.GetEnumUnderlyingType();
                    var enumUnderlyingTypeDescriptor = enumUnderlyingType.GetTypeDescriptor(TypeDescriptorRegistry.Shared);
                    var enumUnderlyingTypeName = enumUnderlyingTypeDescriptor.GetTypeName(CodeLanguage.KotlinJNA, false);

                    return enumUnderlyingTypeName;
                }
                else
                {
                    return ManagedType.CTypeName();
                }
            }
            case CodeLanguage.Kotlin: {
                bool isArray = ManagedType.IsArray;

                Type? elementType = isArray
                    ? ManagedType.GetElementType()
                    : null;
                
                string kotlinTypeName;

                if (isArray &&
                    elementType is not null)
                {
                    // TODO: Shouldn't this go through TypeDescriptor?
                    var kotlinElementTypeName = elementType.CTypeName();
                    var rank = ManagedType.GetArrayRank();

                    if (arrayElementNullability == Nullability.NotSpecified)
                    {
                        arrayElementNullability = Nullability.Nullable;
                    }

                    string arrayTypeName;

                    if (rank == 1)
                    {
                        // Single-dimensional array
                        arrayTypeName = arrayElementNullability == Nullability.NonNullable
                            ? "DNArray"
                            : "DNNullableArray";
                    }
                    else if (rank > 1)
                    {
                        // Multidimensional array
                        // TODO: Support multi-dimensional arrays
                        return ManagedType.CTypeName();
                        
                        // arrayTypeName = arrayElementNullability == Nullability.NonNullable
                        //     ? "DNMultidimensionalArray"
                        //     : "DNNullableMultidimensionalArray";
                    }
                    else
                    {
                        throw new Exception($"An array rank of {rank} doesn't really make sense, right?");
                    }

                    kotlinTypeName = $"{arrayTypeName}<{kotlinElementTypeName}>";
                }
                else
                {
                    kotlinTypeName = ManagedType.CTypeName();
                }

                return kotlinTypeName;
            }
            default:
                throw new NotImplementedException();
        }
    }

    public string? GetTypeConversion(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage,
        Nullability arrayElementNullability = Nullability.NotSpecified
    )
    {
        LanguagePair languagePair = new(sourceLanguage, targetLanguage);
        
        bool isSwift = sourceLanguage == CodeLanguage.Swift || targetLanguage == CodeLanguage.Swift;
        bool isKotlin = sourceLanguage == CodeLanguage.Kotlin || targetLanguage == CodeLanguage.Kotlin;
        
        bool isAllowedToCache = !((isSwift || isKotlin) &&
                                  IsArray);

        string? typeConversion;

        if (isAllowedToCache) {
            m_typeConversions.TryGetValue(
                languagePair,
                out typeConversion
            );
        } else {
            typeConversion = null;
        }

        if (typeConversion is null) {
            typeConversion = GenerateTypeConversion(
                sourceLanguage,
                targetLanguage,
                arrayElementNullability
            );

            if (isAllowedToCache &&
                typeConversion is not null) {
                m_typeConversions[languagePair] = typeConversion;
            }
        }

        return typeConversion;
    }
    
    private string? GenerateTypeConversion(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage,
        Nullability arrayElementNullability
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
                false,
                Nullability.NotSpecified,
                arrayElementNullability
            );

            if (IsEnum) {
                return $"{swiftTypeName}(cValue: {{0}})";
            } else if (RequiresNativePointer) {
                var suffix = IsInterface 
                    ? SwiftDotNETInterfaceImplementationSuffix
                    : string.Empty;
                
                return $"{swiftTypeName}{suffix}(handle: {{0}})";
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
        } else if (sourceLanguage == CodeLanguage.C &&
                   targetLanguage == CodeLanguage.KotlinJNA) {
            string kotlinTypeName = GetTypeName(
                CodeLanguage.KotlinJNA,
                false,
                Nullability.NotSpecified,
                arrayElementNullability
            );

            if (IsEnum) {
                return $"{kotlinTypeName}({{0}})";
            } else if (RequiresNativePointer) {
                var suffix = IsInterface 
                    ? KotlinDotNETInterfaceImplementationSuffix
                    : string.Empty;
                
                return $"{kotlinTypeName}{suffix}({{0}})";
            } else {
                return null;
            }
        } else if (sourceLanguage == CodeLanguage.KotlinJNA &&
                   targetLanguage == CodeLanguage.Kotlin) {
            string kotlinTypeName = GetTypeName(
                CodeLanguage.Kotlin,
                false,
                Nullability.NotSpecified,
                arrayElementNullability
            );
            
            bool isArray = ManagedType.IsArray;

            Type? elementType = isArray
                ? ManagedType.GetElementType()
                : null;

            int arrayRank = isArray
                ? ManagedType.GetArrayRank() 
                : 0;

            if (IsEnum) {
                return kotlinTypeName + "({0})";
            } else if (isArray &&
                       elementType is not null &&
                       arrayRank == 1 /* Single dimensional array */) {
                // TODO: Support multi-dimensional arrays
                // TODO: Shouldn't this go through TypeDescriptor?
                var kotlinElementTypeName = elementType.CTypeName();
                
                return $"{kotlinTypeName}({{0}}, {kotlinElementTypeName}::class.java)";
            } else if (RequiresNativePointer) {
                var suffix = IsInterface 
                    ? KotlinDotNETInterfaceImplementationSuffix
                    : string.Empty;
                
                return $"{kotlinTypeName}{suffix}({{0}})";
            } else {
                return null;
            }
        } else if (sourceLanguage == CodeLanguage.Kotlin &&
                   targetLanguage == CodeLanguage.KotlinJNA) {
            if (IsEnum) {
                return "{0}.value";
            } else if (RequiresNativePointer) {
                return "{0}.getHandleOrNull()";
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