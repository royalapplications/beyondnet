namespace NativeAOT.CodeGenerator;

public class TypeDescriptorRegistry
{
    public TypeDescriptorRegistry()
    {
        var builtInDescriptors = GetBuiltInTypeDescriptors();

        foreach (var typeDescriptor in builtInDescriptors) {
            AddTypeDescriptor(typeDescriptor);
        }
    }

    private Dictionary<Type, TypeDescriptor> Descriptors { get; } = new();

    public void AddTypeDescriptor(TypeDescriptor typeDescriptor)
    {
        Descriptors[typeDescriptor.ManagedType] = typeDescriptor;
    }

    public TypeDescriptor? GetTypeDescriptor(Type managedType)
    {
        if (Descriptors.TryGetValue(managedType, out TypeDescriptor? typeDescriptor)) {
            return typeDescriptor;
        }

        return null;
    }

    private static HashSet<TypeDescriptor> GetBuiltInTypeDescriptors()
    {
        HashSet<TypeDescriptor> descriptors = new() {
            BuiltInTypeDescriptors.VoidTypeDescriptor,
            BuiltInTypeDescriptors.Int32TypeDescriptor,
            BuiltInTypeDescriptors.StringTypeDescriptor
        };

        return descriptors;
    }
}