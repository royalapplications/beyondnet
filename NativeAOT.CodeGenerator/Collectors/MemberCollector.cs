using System.Reflection;

namespace NativeAOT.CodeGenerator.Collectors;

public class MemberCollector
{
    private readonly Type m_type;

    public MemberCollector(Type type)
    {
        m_type = type ?? throw new ArgumentNullException(nameof(type));
    }
    
    public HashSet<MemberInfo> Collect(out Dictionary<MemberInfo, string> unsupportedMembers)
    {
        HashSet<MemberInfo> collectedMembers = new();
        unsupportedMembers = new();

        if (TypeCollector.IsSupportedType(m_type)) {
            BindingFlags flags = BindingFlags.Public | 
                                 BindingFlags.DeclaredOnly |
                                 BindingFlags.Instance |
                                 BindingFlags.Static;
            
            var memberInfos = m_type.GetMembers(flags);
            
            foreach (var memberInfo in memberInfos) {
                CollectMember(memberInfo, collectedMembers, unsupportedMembers);
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
        if (constructorInfo.IsGenericMethod ||
            constructorInfo.IsGenericMethodDefinition ||
            constructorInfo.IsConstructedGenericMethod) {
            unsupportedMembers[constructorInfo] = "Is Generic";
            
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
        if (methodInfo.IsGenericMethod ||
            methodInfo.IsGenericMethodDefinition ||
            methodInfo.IsConstructedGenericMethod ||
            methodInfo.ContainsGenericParameters) {
            unsupportedMembers[methodInfo] = "Is Generic";
            
            return;
        }
        
        // This filters out getters/setters and operator overloading methods
        bool isSpecialName = methodInfo.IsSpecialName;

        if (isSpecialName) {
            unsupportedMembers[methodInfo] = "Is Special Name";
            
            return;
        }

        Type returnType = methodInfo.ReturnType;

        if (!TypeCollector.IsSupportedType(returnType)) {
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

        if (!TypeCollector.IsSupportedType(propertyType)) {
            unsupportedMembers[propertyInfo] = "Has unsupported type";
            
            return;
        }

        bool hasIndexParameters = propertyInfo.GetIndexParameters().Length > 0;

        if (hasIndexParameters) {
            unsupportedMembers[propertyInfo] = "Has index parameter(s)";
            
            return;
        }

        collectedMembers.Add(propertyInfo);
    }
    
    private void CollectField(
        FieldInfo fieldInfo,
        HashSet<MemberInfo> collectedMembers,
        Dictionary<MemberInfo, string> unsupportedMembers
    )
    {
        Type fieldType = fieldInfo.FieldType;

        if (!TypeCollector.IsSupportedType(fieldType)) {
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
        } else if (!TypeCollector.IsSupportedType(eventHandlerType)) {
            unsupportedMembers[eventInfo] = "Has unsupported event handler type";
        } else {
            collectedMembers.Add(eventInfo);
        }
    }
    
    private bool ValidateParameter(ParameterInfo parameterInfo)
    {
        var parameterType = parameterInfo.ParameterType;

        if (!TypeCollector.IsSupportedType(parameterType)) {
            return false;
        }

        return true;
    }
}