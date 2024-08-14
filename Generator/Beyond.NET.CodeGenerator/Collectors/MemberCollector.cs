using System.Reflection;
using System.Text;

using Beyond.NET.CodeGenerator.Extensions;

namespace Beyond.NET.CodeGenerator.Collectors;

public class MemberCollector
{
    private readonly Type m_type;
    private readonly TypeCollector m_typeCollector;
    private readonly bool m_enableGenericsSupport;

    private static readonly Dictionary<Type, string[]> TYPES_TO_UNSUPPORTED_MEMBER_NAMES_MAPPING = new() {
        {
            typeof(System.Runtime.InteropServices.Marshal), [
                "WriteInt16"
            ]
        },
        {
            typeof(System.Reflection.Assembly), [
                // TODO: There's a bug in .NET 8.0.400 where Assembly.SetEntryAssembly seems to be exposed in the reference assemblies but not in the implementation assemblies. For now, we just exclude that method. 
                "SetEntryAssembly"
            ]
        }
    };

    private readonly string[]? m_excludedMemberNames;

    public MemberCollector(
        Type type,
        TypeCollector typeCollector
    )
    {
        m_type = type ?? throw new ArgumentNullException(nameof(type));
        m_typeCollector = typeCollector ?? throw new ArgumentNullException(nameof(typeCollector));
        m_enableGenericsSupport = m_typeCollector.EnableGenericsSupport;

        TYPES_TO_UNSUPPORTED_MEMBER_NAMES_MAPPING.TryGetValue(
            type,
            out string[]? unsupportedMemberNames
        );
        
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
            bool isInterface = m_type.IsInterface;
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

                if (isInterface)
                {
                    if (memberInfo is MethodInfo { IsAbstract: true, IsStatic: true })
                    {
                        unsupportedMembers[memberInfo] = "Static abstract method in interface";
                        
                        continue;
                    }

                    if (memberInfo is PropertyInfo propertyInfo &&
                        (propertyInfo.GetMethod is { IsAbstract: true, IsStatic: true } ||
                         propertyInfo.SetMethod is { IsAbstract: true, IsStatic: true }))
                    {
                        unsupportedMembers[memberInfo] = "Static abstract property in interface";

                        continue;
                    }
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
        List<Tuple<ParameterInfo, string?>> unsupportedReasons = new();

        foreach (var parameterInfo in parameterInfos) {
            if (!ValidateParameter(parameterInfo, out string? unsupportedReason)) {
                allParametersSupported = false;
                unsupportedReasons.Add(new(parameterInfo, unsupportedReason));
                
                break;
            }
        }

        if (allParametersSupported) {
            collectedMembers.Add(constructorInfo);
        } else {
            var sb = new StringBuilder("Has unsupported parameter(s): ");

            foreach (var unsupportedReason in unsupportedReasons) {
                var parameterInfo = unsupportedReason.Item1;
                var reason = unsupportedReason.Item2 ?? "Unknown Reason";

                sb.Append($"{parameterInfo.Name ?? "N/A"}: {reason}; ");
            }
            
            unsupportedMembers[constructorInfo] = sb.ToString();
        }
    }

    private void CollectMethod(
        MethodInfo methodInfo,
        HashSet<MemberInfo> collectedMembers,
        Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        if (!m_enableGenericsSupport &&
            methodInfo.IsGenericMethod) {
            unsupportedMembers[methodInfo] = "Is Generic Method";
            
            return;
        }
        
        // This filters out getters/setters and operator overloading methods
        bool isSpecialName = methodInfo.IsSpecialName;

        if (isSpecialName) {
            unsupportedMembers[methodInfo] = "Is Special Name";
            
            return;
        }

        Type returnType = methodInfo.ReturnType;

        if (!m_typeCollector.IsSupportedType(returnType, out string? unsupportedTypeReason)) {
            unsupportedMembers[methodInfo] = $"Has unsupported return type: {unsupportedTypeReason}";
            
            return;
        }

        var parameterInfos = methodInfo.GetParameters();
        
        bool allParametersSupported = true;
        List<Tuple<ParameterInfo, string?>> unsupportedReasons = new();

        foreach (var parameterInfo in parameterInfos) {
            if (!ValidateParameter(parameterInfo, out string? unsupportedReason)) {
                allParametersSupported = false;
                unsupportedReasons.Add(new(parameterInfo, unsupportedReason));
                
                break;
            }
        }
        
        if (allParametersSupported) {
            collectedMembers.Add(methodInfo);
        } else {
            var sb = new StringBuilder("Has unsupported parameter(s): ");

            foreach (var unsupportedReason in unsupportedReasons) {
                var parameterInfo = unsupportedReason.Item1;
                var reason = unsupportedReason.Item2 ?? "Unknown Reason";

                sb.Append($"{parameterInfo.Name ?? "N/A"}: {reason}; ");
            }
            
            unsupportedMembers[methodInfo] = sb.ToString();
        }
    }
    
    private void CollectProperty(
        PropertyInfo propertyInfo,
        HashSet<MemberInfo> collectedMembers,
        Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        Type propertyType = propertyInfo.PropertyType;

        if (!m_typeCollector.IsSupportedType(propertyType, out string? unsupportedTypeReason)) {
            unsupportedMembers[propertyInfo] = $"Has unsupported type: {unsupportedTypeReason}";
            
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

        if (!m_typeCollector.IsSupportedType(fieldType, out string? unsupportedReason)) {
            unsupportedMembers[fieldType] = $"Has unsupported type: {unsupportedReason ?? "Unknown Reason"}";
            
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
        } else if (!m_typeCollector.IsSupportedType(eventHandlerType, out string? unsupportedReason)) {
            unsupportedMembers[eventInfo] = $"Has unsupported event handler type: {unsupportedReason ?? "Unknown Reason"}";
        } else {
            collectedMembers.Add(eventInfo);
        }
    }
    
    private bool ValidateParameter(
        ParameterInfo parameterInfo, 
        out string? unsupportedReason
    )
    {
        var parameterType = parameterInfo.ParameterType;

        if (!m_typeCollector.IsSupportedType(parameterType, out unsupportedReason)) {
            return false;
        }

        return true;
    }
}