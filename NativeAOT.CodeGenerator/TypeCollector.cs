using System.Reflection;
using NativeAOT.Core;

namespace NativeAOT.CodeGenerator;

public class TypeCollector
{
    private readonly Assembly m_assembly;
    
    public TypeCollector(Assembly assembly)
    {
        m_assembly = assembly;
    }

    public HashSet<Type> Collect()
    {
        var assemblyTypes = m_assembly.ExportedTypes;

        HashSet<Type> collectedTypes = new();
        
        foreach (var type in assemblyTypes) {
            Collect(type, collectedTypes);
        }

        return collectedTypes;
    }

    private void Collect(Type type, HashSet<Type> collectedTypes)
    {
        if (!type.IsVisible) {
            return;
        }

        bool added = collectedTypes.Add(type);

        if (!added) {
            // Already added, so skip this type
            
            return;
        }

        Type? baseType = type.BaseType;

        if (baseType != null) {
            Collect(baseType, collectedTypes);
        }

        BindingFlags getMembersFlags = BindingFlags.Public | 
                                       BindingFlags.DeclaredOnly |
                                       BindingFlags.Instance |
                                       BindingFlags.Static;

        var memberInfos = type.GetMembers(getMembersFlags);

        foreach (var memberInfo in memberInfos) {
            Collect(memberInfo, collectedTypes);
        }
    }

    private void Collect(MemberInfo memberInfo, HashSet<Type> collectedTypes)
    {
        switch (memberInfo.MemberType) {
            case MemberTypes.Constructor:
                CollectConstructor((ConstructorInfo)memberInfo, collectedTypes);
                
                break;
            case MemberTypes.Method:
                CollectMethod((MethodInfo)memberInfo, collectedTypes);
                
                break;
            case MemberTypes.Property:
                CollectProperty((PropertyInfo)memberInfo, collectedTypes);
                
                break;
            case MemberTypes.Field:
                CollectField((FieldInfo)memberInfo, collectedTypes);
                
                break;
            case MemberTypes.Event:
                CollectEvent((EventInfo)memberInfo, collectedTypes);
                
                break;
        }
    }

    private void CollectConstructor(ConstructorInfo constructorInfo, HashSet<Type> collectedTypes)
    {
        var parameterInfos = constructorInfo.GetParameters();

        foreach (var parameterInfo in parameterInfos) {
            CollectParameter(parameterInfo, collectedTypes);
        }
    }
    
    private void CollectMethod(MethodInfo methodInfo, HashSet<Type> collectedTypes)
    {
        Type returnType = methodInfo.ReturnType;

        Collect(returnType, collectedTypes);
        
        var parameterInfos = methodInfo.GetParameters();

        foreach (var parameterInfo in parameterInfos) {
            CollectParameter(parameterInfo, collectedTypes);
        }
    }
    
    private void CollectProperty(PropertyInfo propertyInfo, HashSet<Type> collectedTypes)
    {
        Type propertyType = propertyInfo.PropertyType;

        Collect(propertyType, collectedTypes);
    }
    
    private void CollectField(FieldInfo fieldInfo, HashSet<Type> collectedTypes)
    {
        Type fieldType = fieldInfo.FieldType;

        Collect(fieldType, collectedTypes);
    }
    
    private void CollectEvent(EventInfo eventInfo, HashSet<Type> collectedTypes)
    {
        Type? eventHandlerType = eventInfo.EventHandlerType;

        if (eventHandlerType != null) {
            Collect(eventHandlerType, collectedTypes);
        }
    }

    private void CollectParameter(ParameterInfo parameterInfo, HashSet<Type> collectedTypes)
    {
        Type parameterType = parameterInfo.ParameterType;

        Collect(parameterType, collectedTypes);
    }
}