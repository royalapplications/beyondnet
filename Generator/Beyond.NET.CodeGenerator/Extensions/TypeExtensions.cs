using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Extensions;

internal static class TypeExtensions
{
    internal static TypeDescriptor GetTypeDescriptor(this Type type)
    {
        return type.GetTypeDescriptor(TypeDescriptorRegistry.Shared);
    }
    
    internal static TypeDescriptor GetTypeDescriptor(this Type type, TypeDescriptorRegistry typeDescriptorRegistry)
    {
        return typeDescriptorRegistry.GetOrCreateTypeDescriptor(type);
    }

    internal static string GetFullNameOrName(this Type type)
    {
        string? fullName = type.FullName;
        string name = fullName ?? type.Name;

        // TODO: Nested type handling is not good (ie. System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable+ConfiguredValueTaskAwaiter)
        // if (string.IsNullOrEmpty(fullName) &&
        //     type.IsNested) {
        //     Type? declaringType = type.DeclaringType;
        //
        //     if (declaringType is null) {
        //         throw new Exception("Nested type without declaring type");
        //     }
        //
        //     name = $"{declaringType.GetFullNameOrName()}.{name}";
        // }

        name = name
            .Replace("+", ".");
        
        bool isGeneric = type.IsGenericType ||
                         type.IsGenericTypeDefinition;

        bool isConstructedGeneric = type.IsConstructedGenericType;

        if (isGeneric) {
            int backtickIndex = name.IndexOf('`');

            if (backtickIndex <= 0) {
                throw new Exception($"A generic type \"{name}\" without a backtick in its name");
            }

            name = name.Substring(0, backtickIndex);

            Type[] genericArguments = type.GetGenericArguments();
            int numberOfGenericArguments = genericArguments.Length;

            if (numberOfGenericArguments <= 0) {
                throw new Exception($"A generic type \"{name}\" without generic arguments");
            }

            name += "<";

            int index = 0;
            
            foreach (Type genericArgument in genericArguments) {
                if (index > 0) {
                    name += ",";
                }
                
                if (isConstructedGeneric) {
                    string argumentTypeName = genericArgument.GetFullNameOrName();

                    name += argumentTypeName;
                }

                index++;
            }
            
            name += ">";
        }

        return name;
    }
    
    internal static string CTypeName(this Type type)
    {
        if (type.IsNullableValueType(out Type? valueType)) {
            type = valueType;
        }
        
        string fullTypeName = type.GetFullNameOrName();

        string cTypeName = fullTypeName
            .Replace(".", "_")
            .Replace("+", "_")
            .Replace("&", string.Empty)
            .Replace("[]", "_Array");

        if (cTypeName.Contains("[") &&
            cTypeName.Contains("]") &&
            cTypeName.Contains(",")) { // Multi-dimensional array
            var arrayStartIdx = cTypeName.IndexOf('[');
            var arrayEndIdx = cTypeName.LastIndexOf(']');

            var arrayDimensionsString = cTypeName.Substring(
                arrayStartIdx + 1,
                arrayEndIdx - arrayStartIdx - 1
            );

            if (arrayDimensionsString.Any(s => s != ',')) {
                throw new Exception("Failed to parse array type's dimensions");
            }

            var arrayDimensions = arrayDimensionsString.Length + 1;

            cTypeName = cTypeName
                .Replace($"[{arrayDimensionsString}]", $"_Array_D{arrayDimensions}");
        }

        bool isGeneric = type.IsGenericType ||
                         type.IsGenericTypeDefinition;

        if (isGeneric) {
            int genericArgsCount = type.GetGenericArguments().Length;
            
            int backtickIndex = cTypeName.IndexOf('`');

            if (backtickIndex > 0) {
                cTypeName = cTypeName.Substring(0, backtickIndex);
            }

            int lessThanIndex = cTypeName.IndexOf('<');

            if (lessThanIndex > 0) {
                cTypeName = cTypeName.Substring(0, lessThanIndex);
            }
            
            cTypeName += "_A" + genericArgsCount;
        }

        return cTypeName;
    }

    internal static bool IsReferenceType(this Type type)
    {
        return !type.IsValueType;
    }

    internal static bool IsBoolean(this Type type)
    {
        return type == typeof(bool);
    }
    
    internal static bool IsVoid(this Type type)
    {
        return type == typeof(void);
    }

    internal static bool IsDelegate(this Type type)
    {
        return type.IsAssignableTo(typeof(Delegate));
    }

    internal static bool IsGenericInAnyWay(this Type type, bool includeBaseTypes)
    {
        bool isGeneric = type.IsGenericType || 
                         type.IsConstructedGenericType || 
                         type.IsGenericTypeDefinition || 
                         type.IsGenericParameter;

        if (isGeneric) {
            return true;
        }

        if (!includeBaseTypes) {
            return false;
        }

        Type? baseType = type.BaseType;

        if (baseType is null) {
            return false;
        }

        bool isAnyBaseTypeGeneric = baseType.IsGenericInAnyWay(true);

        return isAnyBaseTypeGeneric;
    }

    internal static bool IsNullableValueType(
        this Type type,
        [NotNullWhen(true)] out Type? valueType
    )
    {
        if (!type.IsGenericType ||
            type.GetGenericTypeDefinition() != typeof(Nullable<>)) {
            valueType = null;
            
            return false;
        }

        valueType = Nullable.GetUnderlyingType(type);

        if (valueType is null) {
            return false;
        }

        return true;
    }

    internal static bool IsReadOnlySpanOfByte(this Type type)
    {
        return type == typeof(ReadOnlySpan<byte>);
    }
    
    internal static bool IsStruct(this Type type)
    {
        return type.IsValueType &&
               !type.IsVoid() &&
               !type.IsEnum &&
               !type.IsPrimitive;
    }

    internal static MethodInfo? GetDelegateInvokeMethod(this Type delegateType)
    {
        const string invokeMethodName = "Invoke";
        
        MethodInfo? invokeMethod = delegateType.GetMethod(invokeMethodName);

        return invokeMethod;
    }

    internal static bool IsByRefValueType(
        this Type type,
        out bool nonByRefTypeIsStruct
    )
    {
        nonByRefTypeIsStruct = false;
        
        if (!type.IsByRef) {
            return false;
        }

        Type nonByRefType = type.GetNonByRefType();
        bool isValueType = nonByRefType.IsValueType;
        nonByRefTypeIsStruct = nonByRefType.IsStruct();

        return isValueType;
    }

    internal static Type GetNonByRefType(this Type type)
    {
        if (!type.IsByRef) {
            return type;
        }

        if (type.IsArray) {
            Type? elementType = type.GetElementType();

            if (elementType is not null) {
                return elementType.MakeArrayType();
            } else {
                return type;
            }
        } else {
            return type.GetElementType() ?? type;
        }
    }

    internal static bool ContainsNonConstructedGenericTypes(this Type type)
    {
        // if (!type.IsGenericType) {
        //     return false;
        // }

        if (type.IsGenericParameter) {
            return true;
        }
        
        Type[] genericArgs = type.GetGenericArguments();

        foreach (Type genericArg in genericArgs) {
            if (genericArg.ContainsNonConstructedGenericTypes()) {
                return true;
            }
        }

        return false;
    }

    internal static bool HasRequiredMembers(this Type type)
    {
        var attr = type.GetCustomAttribute<RequiredMemberAttribute>();
        bool hasIt = attr is not null;

        return hasIt;
    }
    
    internal static bool DoesAnyBaseTypeImplementInterface(
        this Type derivedType,
        Type interfaceType
    )
    {
        if (!interfaceType.IsInterface) {
            throw new ArgumentException("The provided type is not an interface.", nameof(interfaceType));
        }

        // Iterate through the base types
        Type? currentType = derivedType.BaseType;
        
        while (currentType is not null) {
            if (currentType.GetInterfaces().Contains(interfaceType)) {
                return true;
            }
            
            currentType = currentType.BaseType;
        }

        return false;
    }
}