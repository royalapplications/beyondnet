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
        name = name.Replace("+", ".");
        
        return name;
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
}