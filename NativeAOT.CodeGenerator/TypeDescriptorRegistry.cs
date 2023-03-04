using NativeAOT.Core;

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
            VoidTypeDescriptor,
            Int32TypeDescriptor,
            StringTypeDescriptor
        };

        return descriptors;
    }

    #region Built-in Type Descriptors
    #region void
    private static TypeDescriptor VoidTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(typeof(void));
            descriptor.SetTypeName("void", CodeLanguage.CSharpUnmanaged);
            descriptor.SetTypeName("void", CodeLanguage.C);
            descriptor.SetTypeName("Void", CodeLanguage.Swift);
    
            return descriptor;
        }
    }
    #endregion void
    
    #region Int32
    private static TypeDescriptor Int32TypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(typeof(int));
            descriptor.SetTypeName("int", CodeLanguage.CSharpUnmanaged);
            descriptor.SetTypeName("int32_t", CodeLanguage.C);
            descriptor.SetTypeName("Int32", CodeLanguage.Swift);
    
            return descriptor;
        }
    }
    #endregion Int32
    
    #region String
    private static TypeDescriptor StringTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(typeof(string));
            descriptor.SetTypeName("byte*", CodeLanguage.CSharpUnmanaged);
            descriptor.SetTypeName("char*", CodeLanguage.C);
            descriptor.SetTypeName("String", CodeLanguage.Swift);
    
            return descriptor;
        }
    }
    #endregion String
    #endregion Built-in Type Descriptors
}