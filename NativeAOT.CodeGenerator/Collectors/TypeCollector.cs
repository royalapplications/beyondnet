using System.Reflection;

using NativeAOT.CodeGenerator.Extensions;

namespace NativeAOT.CodeGenerator.Collectors;

public class TypeCollector
{
    private readonly Assembly m_assembly;

    private static readonly Type[] INCLUDED_TYPES = new [] {
        typeof(System.Object),
        typeof(System.Type),
        typeof(System.Exception),
        typeof(System.Boolean),
        typeof(System.Char),
        typeof(System.Double),
        typeof(System.Decimal),
        typeof(System.SByte),
        typeof(System.Int16),
        typeof(System.Int32),
        typeof(System.Int64),
        typeof(System.IntPtr),
        typeof(System.UIntPtr),
        typeof(System.String),
        typeof(System.Byte),
        typeof(System.UInt16),
        typeof(System.UInt32),
        typeof(System.UInt64),
        typeof(System.Delegate),
        typeof(System.MulticastDelegate),
        typeof(System.Convert),
        typeof(System.Math),
        typeof(System.GC),
        typeof(System.Enum),
        typeof(System.Array),
        typeof(System.Threading.Thread),
        typeof(System.Threading.Timer),
        typeof(System.AppDomain),
        typeof(System.Reflection.PortableExecutableKinds),
        typeof(System.Reflection.ImageFileMachine),
    };
    
    private static readonly Type[] UNSUPPORTED_TYPES = new [] {
        Type.GetType("System.Runtime.Serialization.DeserializationToken")!,
        Type.GetType("System.TypedReference")!,
        Type.GetType("System.Char&")!
    };

    private readonly Type[] m_typeWhitelist;
    private readonly Type[] m_typeBlacklist;
    
    public TypeCollector(
        Assembly assembly,
        Type[] typeWhitelist,
        Type[] typeBlacklist
    )
    {
        List<Type> whitelist = new(INCLUDED_TYPES);
        whitelist.AddRange(typeWhitelist);
        
        List<Type> blacklist = new(UNSUPPORTED_TYPES);
        blacklist.AddRange(typeBlacklist);

        m_typeWhitelist = whitelist.ToArray();
        m_typeBlacklist = blacklist.ToArray();
        
        m_assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
    }

    public HashSet<Type> Collect(out Dictionary<Type, string> unsupportedTypes)
    {
        HashSet<Type> collectedTypes = new();
        unsupportedTypes = new();

        List<Type> typesToCollect = new(m_typeWhitelist);
        
        var assemblyTypes = m_assembly.ExportedTypes;
        
        typesToCollect.AddRange(assemblyTypes);

        foreach (var type in typesToCollect) {
            CollectType(type, collectedTypes, unsupportedTypes);
        }

        return collectedTypes;
    }

    private void CollectType(
        Type type,
        HashSet<Type> collectedTypes,
        Dictionary<Type, string> unsupportedTypes
    )
    {
        if (m_typeBlacklist.Contains(type)) {
            unsupportedTypes[type] = "Blacklisted";
            
            return;
        }
        
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

        bool isDelegate = type.IsDelegate();

        if (isDelegate) {
            CollectDelegate(
                type,
                collectedTypes,
                unsupportedTypes
            );
        } else {
            BindingFlags getMembersFlags = BindingFlags.Public | 
                                           BindingFlags.DeclaredOnly |
                                           BindingFlags.Instance |
                                           BindingFlags.Static;
    
            var memberInfos = type.GetMembers(getMembersFlags);
    
            foreach (var memberInfo in memberInfos) {
                CollectMember(
                    memberInfo,
                    collectedTypes,
                    unsupportedTypes
                );
            }
        }
    }

    private void CollectMember(
        MemberInfo memberInfo,
        HashSet<Type> collectedTypes,
        Dictionary<Type, string> unsupportedTypes
    )
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

    private void CollectConstructor(
        ConstructorInfo constructorInfo,
        HashSet<Type> collectedTypes, 
        Dictionary<Type, string> unsupportedTypes
    )
    {
        var parameterInfos = constructorInfo.GetParameters();

        foreach (var parameterInfo in parameterInfos) {
            CollectParameter(parameterInfo, collectedTypes, unsupportedTypes);
        }
    }
    
    private void CollectMethod(
        MethodInfo methodInfo,
        HashSet<Type> collectedTypes,
        Dictionary<Type, string> unsupportedTypes
    )
    {
        Type returnType = methodInfo.ReturnType;

        if (returnType.IsByRef) {
            unsupportedTypes[returnType] = "Is by ref";

            return;
        }

        CollectType(returnType, collectedTypes, unsupportedTypes);
        
        var parameterInfos = methodInfo.GetParameters();

        foreach (var parameterInfo in parameterInfos) {
            CollectParameter(parameterInfo, collectedTypes, unsupportedTypes);
        }
    }

    private void CollectDelegate(
        Type delegateType,
        HashSet<Type> collectedTypes,
        Dictionary<Type, string> unsupportedTypes
    )
    {
        MethodInfo? invokeMethod = delegateType.GetDelegateInvokeMethod();

        if (invokeMethod is null) {
            return;
        }
        
        CollectMethod(
            invokeMethod,
            collectedTypes,
            unsupportedTypes
        );
    }
    
    private void CollectProperty(
        PropertyInfo propertyInfo,
        HashSet<Type> collectedTypes,
        Dictionary<Type, string> unsupportedTypes
    )
    {
        Type propertyType = propertyInfo.PropertyType;

        CollectType(propertyType, collectedTypes, unsupportedTypes);
    }
    
    private void CollectField(
        FieldInfo fieldInfo,
        HashSet<Type> collectedTypes,
        Dictionary<Type, string> unsupportedTypes
    )
    {
        Type fieldType = fieldInfo.FieldType;

        CollectType(fieldType, collectedTypes, unsupportedTypes);
    }
    
    private void CollectEvent(
        EventInfo eventInfo,
        HashSet<Type> collectedTypes,
        Dictionary<Type, string> unsupportedTypes
    )
    {
        Type? eventHandlerType = eventInfo.EventHandlerType;

        if (eventHandlerType != null) {
            CollectType(eventHandlerType, collectedTypes, unsupportedTypes);
        }
    }

    private void CollectParameter(
        ParameterInfo parameterInfo,
        HashSet<Type> collectedTypes,
        Dictionary<Type, string> unsupportedTypes
    )
    {
        Type parameterType = parameterInfo.ParameterType;

        CollectType(parameterType, collectedTypes, unsupportedTypes);
    }

    public static bool IsSupportedType(Type type)
    {
        return IsSupportedType(type, out _);
    }
    
    public static bool IsSupportedType(
        Type type,
        out string? unsupportedReason
    )
    {
        unsupportedReason = null;

        if (!type.IsVisible) {
            unsupportedReason = "Is Not Visible (public)";
            return false;
        }

        // TODO: WIP
        // if (type.IsByRef) {
        //     unsupportedReason = "Is By Ref Type";
        //     return false;
        // }
        
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

        if (UNSUPPORTED_TYPES.Contains(type)) {
            unsupportedReason = "Is unsupported Type";
            return false;
        }

        return true;
    } 
}