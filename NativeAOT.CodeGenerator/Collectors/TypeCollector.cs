using System.Reflection;

using NativeAOT.CodeGenerator.Extensions;

namespace NativeAOT.CodeGenerator.Collectors;

public class TypeCollector
{
    private readonly Assembly m_assembly;

    private static readonly Type[] INCLUDED_TYPES = new [] {
        typeof(System.Object),
        typeof(System.Type),
        typeof(System.String),
        typeof(System.Runtime.InteropServices.Marshal),
        typeof(System.Exception),
        typeof(System.NullReferenceException),
        typeof(System.AggregateException),
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
        typeof(System.Byte),
        typeof(System.UInt16),
        typeof(System.UInt32),
        typeof(System.UInt64),
        typeof(System.Delegate),
        typeof(System.MulticastDelegate),
        typeof(System.Enum),
        typeof(System.Array),
    };
    
    private static readonly Type[] UNSUPPORTED_TYPES = new [] {
        Type.GetType("System.Runtime.Serialization.DeserializationToken")!,
        typeof(System.TypedReference),
        Type.GetType("System.Char&")!,
        typeof(System.Runtime.CompilerServices.DefaultInterpolatedStringHandler),
        typeof(System.Runtime.InteropServices.ComTypes.ITypeInfo),
        typeof(System.Runtime.InteropServices.ComTypes.ITypeLib),
        typeof(System.Runtime.InteropServices.ComTypes.ITypeLib2),
        typeof(System.Runtime.InteropServices.ComTypes.ITypeComp),
    };

    private readonly Type[] m_includedTypes;
    private readonly Type[] m_excludedTypes;
    
    public TypeCollector(
        Assembly assembly,
        Type[] includedTypes,
        Type[] excludedTypes
    )
    {
        List<Type> whitelist = new(INCLUDED_TYPES);
        whitelist.AddRange(includedTypes);
        
        List<Type> blacklist = new(UNSUPPORTED_TYPES);
        blacklist.AddRange(excludedTypes);

        m_includedTypes = whitelist.ToArray();
        m_excludedTypes = blacklist.ToArray();
        
        m_assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
    }

    public HashSet<Type> Collect(out Dictionary<Type, string> unsupportedTypes)
    {
        HashSet<Type> collectedTypes = new();
        unsupportedTypes = new();

        List<Type> typesToCollect = new(m_includedTypes);
        
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
        if (m_excludedTypes.Contains(type)) {
            unsupportedTypes[type] = "Excluded";
            
            return;
        }
        
        if (!IsSupportedType(type, out string? unsupportedReason)) {
            unsupportedTypes[type] = unsupportedReason ?? string.Empty;
            
            return;
        }

        if (type.IsByRef) {
            Type nonByRefType = type.GetNonByRefType();

            if (nonByRefType != type) {
                CollectType(
                    nonByRefType,
                    collectedTypes,
                    unsupportedTypes
                );
            }
        }

        bool added = collectedTypes.Add(type);

        if (!added) {
            // Already added, so skip this type
            
            return;
        }

        Type? baseType = type.BaseType;

        if (baseType != null) {
            CollectType(
                baseType,
                collectedTypes,
                unsupportedTypes
            );
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
        
        if (type.IsByRef) {
            Type nonByRefType = type.GetNonByRefType();

            if (nonByRefType != type) {
                bool isNonByRefTypeSupported = IsSupportedType(nonByRefType, out string? innerUnsupportedReason);

                if (!isNonByRefTypeSupported) {
                    unsupportedReason = innerUnsupportedReason;
                    
                    return false;
                }
            }
        }

        if (!type.IsVisible) {
            unsupportedReason = "Is Not Visible (public)";
            return false;
        }

        if (type.IsGenericType) {
            unsupportedReason = "Is Generic Type";
            return false;
        }
        
        if (type.IsGenericTypeDefinition) {
            unsupportedReason = "Is Generic Type Definition";
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