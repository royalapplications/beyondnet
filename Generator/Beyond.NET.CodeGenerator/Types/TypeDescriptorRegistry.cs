namespace Beyond.NET.CodeGenerator.Types;

public class TypeDescriptorRegistry
{
    public static TypeDescriptorRegistry Shared { get; } = new();

    private TypeDescriptorRegistry()
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

    public TypeDescriptor GetOrCreateTypeDescriptor(Type managedType)
    {
        TypeDescriptor? typeDescriptor = GetTypeDescriptor(managedType);

        if (typeDescriptor == null) {
            // TODO
            typeDescriptor = new(managedType);

            Descriptors[managedType] = typeDescriptor;
        }

        return typeDescriptor;
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
            BuiltIn.BuiltInTypeDescriptors.VoidTypeDescriptor,

            BuiltIn.BuiltInTypeDescriptors.BoolTypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.CharTypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.NintTypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.NuintTypeDescriptor,

            BuiltIn.BuiltInTypeDescriptors.Int8TypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.UInt8TypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.Int16TypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.UInt16TypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.Int32TypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.UInt32TypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.Int64TypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.UInt64TypeDescriptor,

            BuiltIn.BuiltInTypeDescriptors.FloatTypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.DoubleTypeDescriptor,

            BuiltIn.BuiltInTypeDescriptors.ReadOnlySpanOfByteTypeDescriptor,

            // BuiltIn.BuiltInTypeDescriptors.StringTypeDescriptor
        };

        return descriptors;
    }
}