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
}