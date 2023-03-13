using System.Reflection;
using NativeAOT.CodeGenerator.Extensions;

namespace NativeAOT.CodeGenerator.Collectors;

public class TypeCollector
{
    private readonly Assembly m_assembly;
    
    public TypeCollector(Assembly assembly)
    {
        m_assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
    }

    public HashSet<Type> Collect(out Dictionary<Type, string> unsupportedTypes)
    {
        HashSet<Type> collectedTypes = new();
        unsupportedTypes = new();
        
        var assemblyTypes = m_assembly.ExportedTypes;

        foreach (var type in assemblyTypes) {
            CollectType(type, collectedTypes, unsupportedTypes);
        }

        return collectedTypes;
    }

    private void CollectType(Type type, HashSet<Type> collectedTypes, Dictionary<Type, string> unsupportedTypes)
    {
        if (!IsSupportedType(type, out string? unsupportedReason)) {
            unsupportedTypes[type] = unsupportedReason ?? string.Empty;
            
            return;
        }

        bool added = collectedTypes.Add(type);

        if (!added) {
            // Already added, so skip this type
            
            return;
        }

        Type? baseType = type.BaseType;

        if (baseType != null) {
            CollectType(baseType, collectedTypes, unsupportedTypes);
        }

        BindingFlags getMembersFlags = BindingFlags.Public | 
                                       BindingFlags.DeclaredOnly |
                                       BindingFlags.Instance |
                                       BindingFlags.Static;

        var memberInfos = type.GetMembers(getMembersFlags);

        foreach (var memberInfo in memberInfos) {
            CollectMember(memberInfo, collectedTypes, unsupportedTypes);
        }
    }

    private void CollectMember(MemberInfo memberInfo, HashSet<Type> collectedTypes, Dictionary<Type, string> unsupportedTypes)
    {
        switch (memberInfo.MemberType) {
            case MemberTypes.Constructor:
                CollectConstructor((ConstructorInfo)memberInfo, collectedTypes, unsupportedTypes);
                
                break;
            case MemberTypes.Method:
                CollectMethod((MethodInfo)memberInfo, collectedTypes, unsupportedTypes);
                
                break;
            case MemberTypes.Property:
                CollectProperty((PropertyInfo)memberInfo, collectedTypes, unsupportedTypes);
                
                break;
            case MemberTypes.Field:
                CollectField((FieldInfo)memberInfo, collectedTypes, unsupportedTypes);
                
                break;
            case MemberTypes.Event:
                CollectEvent((EventInfo)memberInfo, collectedTypes, unsupportedTypes);
                
                break;
        }
    }

    private void CollectConstructor(ConstructorInfo constructorInfo, HashSet<Type> collectedTypes, Dictionary<Type, string> unsupportedTypes)
    {
        var parameterInfos = constructorInfo.GetParameters();

        foreach (var parameterInfo in parameterInfos) {
            CollectParameter(parameterInfo, collectedTypes, unsupportedTypes);
        }
    }
    
    private void CollectMethod(MethodInfo methodInfo, HashSet<Type> collectedTypes, Dictionary<Type, string> unsupportedTypes)
    {
        Type returnType = methodInfo.ReturnType;

        CollectType(returnType, collectedTypes, unsupportedTypes);
        
        var parameterInfos = methodInfo.GetParameters();

        foreach (var parameterInfo in parameterInfos) {
            CollectParameter(parameterInfo, collectedTypes, unsupportedTypes);
        }
    }
    
    private void CollectProperty(PropertyInfo propertyInfo, HashSet<Type> collectedTypes, Dictionary<Type, string> unsupportedTypes)
    {
        Type propertyType = propertyInfo.PropertyType;

        CollectType(propertyType, collectedTypes, unsupportedTypes);
    }
    
    private void CollectField(FieldInfo fieldInfo, HashSet<Type> collectedTypes, Dictionary<Type, string> unsupportedTypes)
    {
        Type fieldType = fieldInfo.FieldType;

        CollectType(fieldType, collectedTypes, unsupportedTypes);
    }
    
    private void CollectEvent(EventInfo eventInfo, HashSet<Type> collectedTypes, Dictionary<Type, string> unsupportedTypes)
    {
        Type? eventHandlerType = eventInfo.EventHandlerType;

        if (eventHandlerType != null) {
            CollectType(eventHandlerType, collectedTypes, unsupportedTypes);
        }
    }

    private void CollectParameter(ParameterInfo parameterInfo, HashSet<Type> collectedTypes, Dictionary<Type, string> unsupportedTypes)
    {
        Type parameterType = parameterInfo.ParameterType;

        CollectType(parameterType, collectedTypes, unsupportedTypes);
    }

    public static bool IsSupportedType(Type type)
    {
        return IsSupportedType(type, out _);
    }
    
    public static bool IsSupportedType(Type type, out string? unsupportedReason)
    {
        unsupportedReason = null;

        if (!type.IsVisible) {
            unsupportedReason = "Is Not Visible (public)";
            return false;
        }
        
        if (type.IsByRef) {
            unsupportedReason = "Is By Ref Type";
            return false;
        }

        if (type.IsStruct()) {
            unsupportedReason = "Is Struct Type";
            return false;
        }

        if (type.IsArray) {
            unsupportedReason = "Is Array";
            return false;
        }
        
        if (type.IsGenericType) {
            unsupportedReason = "Is Generic Type";
            return false;
        }
        
        if (type.IsGenericParameter) {
            unsupportedReason = "Is Generic Parameter";
            return false;
        }
        
        if (type.IsGenericMethodParameter) {
            unsupportedReason = "Is Generic Method Parameter";
            return false;
        }
        
        if (type.IsGenericTypeDefinition) {
            unsupportedReason = "Is Generic Type Definition";
            return false;
        }
        
        if (type.IsGenericTypeParameter) {
            unsupportedReason = "Is Generic Type Parameter";
            return false;
        }
        
        if (type.IsConstructedGenericType) {
            unsupportedReason = "Is Constructed Generic Type";
            return false;
        }

        if (type.IsPointer) {
            unsupportedReason = "Is Managed Pointer Type";
            return false;
        }

        if (type.IsDelegate()) {
            unsupportedReason = "Is Delegate Type";
            return false;
        }

        return true;
    } 
}