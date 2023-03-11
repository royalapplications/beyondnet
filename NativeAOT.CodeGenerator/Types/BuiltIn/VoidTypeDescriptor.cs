using NativeAOT.Core;

namespace NativeAOT.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor VoidTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(typeof(void));
            descriptor.SetTypeName("void", CodeLanguage.CSharpUnmanaged);
            descriptor.SetTypeName("void", CodeLanguage.C);
            descriptor.SetTypeName("Void", CodeLanguage.Swift);
        
            return descriptor;
        }
    }
}