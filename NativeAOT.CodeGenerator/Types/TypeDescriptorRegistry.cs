namespace NativeAOT.CodeGenerator.Types;

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
            BuiltIn.BuiltInTypeDescriptors.Int32TypeDescriptor,
            BuiltIn.BuiltInTypeDescriptors.StringTypeDescriptor
        };

        return descriptors;
    }
}