using System.Reflection;
using Beyond.NET.CodeGenerator.Extensions;

namespace Beyond.NET.CodeGenerator.Collectors;

public class MemberCollector
{
    private readonly Type m_type;
    private readonly TypeCollector m_typeCollector;

    private static readonly Dictionary<Type, string[]> TYPES_TO_UNSUPPORTED_MEMBER_NAMES_MAPPING = new() {
        { typeof(System.Runtime.InteropServices.Marshal), new [] {
            "WriteInt16"
        } }
    };

    private readonly string[]? m_excludedMemberNames;

    public MemberCollector(
        Type type,
        TypeCollector typeCollector
    )
    {
        m_type = type ?? throw new ArgumentNullException(nameof(type));
        m_typeCollector = typeCollector ?? throw new ArgumentNullException(nameof(typeCollector));

        TYPES_TO_UNSUPPORTED_MEMBER_NAMES_MAPPING.TryGetValue(type, out string[]? unsupportedMemberNames);
        
        m_excludedMemberNames = unsupportedMemberNames;
    }
    
    public HashSet<MemberInfo> Collect(
        out Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        HashSet<MemberInfo> collectedMembers = new();
        unsupportedMembers = new();

        if (m_typeCollector.IsSupportedType(m_type)) {
            bool isStruct = m_type.IsStruct();
            bool foundParameterlessStructConstructor = false;
            
            BindingFlags flags = BindingFlags.Public | 
                                 BindingFlags.DeclaredOnly |
                                 BindingFlags.Instance |
                                 BindingFlags.Static;

            var memberInfos = m_type.GetMembers(flags);
            
            foreach (var memberInfo in memberInfos) {
                string memberName = memberInfo.Name;

                if (m_excludedMemberNames?.Contains(memberName) ?? false) {
                    unsupportedMembers[memberInfo] = "Excluded";
                    
                    continue;
                }

                if (memberInfo.IsObsoleteWithError()) {
                    unsupportedMembers[memberInfo] = "Obsolete with Error";
                    
                    continue;
                }
                
                // See https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record#nondestructive-mutation
                if (memberInfo.IsVirtualCloneMethod()) {
                    unsupportedMembers[memberInfo] = "Virtual Record Clone Method";
                    
                    continue;
                }
                
                CollectMember(
                    memberInfo,
                    collectedMembers,
                    unsupportedMembers
                );

                if (isStruct &&
                    memberInfo is ConstructorInfo constructorInfo &&
                    constructorInfo.GetParameters().Length == 0) {
                    foundParameterlessStructConstructor = true;
                }
            }

            if (isStruct &&
                !foundParameterlessStructConstructor) {
                // Must create a "fake" parameterless constructor

                var parameterlessStructConstructor = new ParameterlessStructConstructorInfo(m_type);
                
                CollectMember(
                    parameterlessStructConstructor,
                    collectedMembers,
                    unsupportedMembers
                );
            }
        }

        return collectedMembers;
    }

    private void CollectMember(
        MemberInfo memberInfo, 
        HashSet<MemberInfo> collectedMembers,
        Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        var memberType = memberInfo.MemberType;
        
        switch (memberType) {
            case MemberTypes.Constructor:
                CollectConstructor((ConstructorInfo)memberInfo, collectedMembers, unsupportedMembers);
                
                break;
            case MemberTypes.Method:
                CollectMethod((MethodInfo)memberInfo, collectedMembers, unsupportedMembers);
                
                break;
            case MemberTypes.Property:
                CollectProperty((PropertyInfo)memberInfo, collectedMembers, unsupportedMembers);
                
                break;
            case MemberTypes.Field:
                CollectField((FieldInfo)memberInfo, collectedMembers, unsupportedMembers);
                
                break;
            case MemberTypes.Event:
                CollectEvent((EventInfo)memberInfo, collectedMembers, unsupportedMembers);
                
                break;
            default:
                unsupportedMembers[memberInfo] = $"Unsupported member type: {memberType}";
                
                break;
        }
    }
    
    private void CollectConstructor(
        ConstructorInfo constructorInfo,
        HashSet<MemberInfo> collectedMembers,
        Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        // TODO: Generics
        if (constructorInfo.IsGenericMethod ||
            constructorInfo.IsGenericMethodDefinition ||
            constructorInfo.IsConstructedGenericMethod) {
            unsupportedMembers[constructorInfo] = "Is Generic";
            
            return;
        }

        var declaringType = constructorInfo.DeclaringType;

        if (declaringType is not null &&
            declaringType.HasRequiredMembers()) {
            unsupportedMembers[constructorInfo] = "Type has required fields or properties";
            
            return;
        }
            
        var parameterInfos = constructorInfo.GetParameters();

        bool allParametersSupported = true;

        foreach (var parameterInfo in parameterInfos) {
            if (!ValidateParameter(parameterInfo)) {
                allParametersSupported = false;
                
                break;
            }
        }

        if (allParametersSupported) {
            collectedMembers.Add(constructorInfo);
        } else {
            unsupportedMembers[constructorInfo] = "Has unsupported parameter(s)";
        }
    }

    private void CollectMethod(
        MethodInfo methodInfo,
        HashSet<MemberInfo> collectedMembers,
        Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        // This filters out getters/setters and operator overloading methods
        bool isSpecialName = methodInfo.IsSpecialName;

        if (isSpecialName) {
            unsupportedMembers[methodInfo] = "Is Special Name";
            
            return;
        }

        Type returnType = methodInfo.ReturnType;

        if (!m_typeCollector.IsSupportedType(returnType)) {
            unsupportedMembers[methodInfo] = "Has unsupported return type";
            
            return;
        }

        var parameterInfos = methodInfo.GetParameters();
        
        bool allParametersSupported = true;

        foreach (var parameterInfo in parameterInfos) {
            if (!ValidateParameter(parameterInfo)) {
                allParametersSupported = false;
                
                break;
            }
        }
        
        if (allParametersSupported) {
            collectedMembers.Add(methodInfo);
        } else {
            unsupportedMembers[methodInfo] = "Has unsupported parameter(s)";
        }
    }
    
    private void CollectProperty(
        PropertyInfo propertyInfo,
        HashSet<MemberInfo> collectedMembers,
        Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        Type propertyType = propertyInfo.PropertyType;

        if (!m_typeCollector.IsSupportedType(propertyType)) {
            unsupportedMembers[propertyInfo] = "Has unsupported type";
            
            return;
        }
        
        // var indexParameters = propertyInfo.GetIndexParameters();
        // bool hasIndexParameters = indexParameters.Length > 0;
        //
        // if (hasIndexParameters) {
        //     unsupportedMembers[propertyInfo] = "Has index parameter(s)";
        //     
        //     return;
        // }

        collectedMembers.Add(propertyInfo);
    }
    
    private void CollectField(
        FieldInfo fieldInfo,
        HashSet<MemberInfo> collectedMembers,
        Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        Type fieldType = fieldInfo.FieldType;

        if (!m_typeCollector.IsSupportedType(fieldType)) {
            unsupportedMembers[fieldType] = "Has unsupported type";
            
            return;
        }

        collectedMembers.Add(fieldInfo);
    }
    
    private void CollectEvent(
        EventInfo eventInfo,
        HashSet<MemberInfo> collectedMembers,
        Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        Type? eventHandlerType = eventInfo.EventHandlerType;

        if (eventHandlerType == null) {
            unsupportedMembers[eventInfo] = "Has no event handler type";
        } else if (!m_typeCollector.IsSupportedType(eventHandlerType)) {
            unsupportedMembers[eventInfo] = "Has unsupported event handler type";
        } else {
            collectedMembers.Add(eventInfo);
        }
    }
    
    private bool ValidateParameter(ParameterInfo parameterInfo)
    {
        var parameterType = parameterInfo.ParameterType;

        if (!m_typeCollector.IsSupportedType(parameterType)) {
            return false;
        }

        return true;
    }
}