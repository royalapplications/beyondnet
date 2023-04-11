using System.Reflection;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Extensions;

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
        string name = type.FullName ?? type.Name;

        // TODO: Is this correct? Why is there even a "+" in some types (ie. System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable+ConfiguredValueTaskAwaiter)
        name = name
            .Replace("+", ".");
        
        return name;
    }
    
    internal static string CTypeName(this Type type)
    {
        string fullTypeName = type.GetFullNameOrName();

        string cTypeName = fullTypeName
            .Replace(".", "_")
            .Replace("+", "_")
            .Replace("&", string.Empty)
            .Replace("[]", "_Array");

        bool isGeneric = type.IsGenericType ||
                         type.IsGenericTypeDefinition;

        if (isGeneric) {
            int backtickIndex = cTypeName.IndexOf('`');

            if (backtickIndex <= 0) {
                throw new Exception($"A generic type \"{fullTypeName}\" without a backtick in its name is weird");
            }

            cTypeName = cTypeName.Substring(0, backtickIndex);
        
            int genericArgsCount = type.GetGenericArguments().Length;

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
}