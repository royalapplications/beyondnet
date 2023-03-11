using NativeAOT.Core;

namespace NativeAOT.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor Int32TypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(typeof(int));
            descriptor.SetTypeName("int", CodeLanguage.CSharpUnmanaged);
            descriptor.SetTypeName("int32_t", CodeLanguage.C);
            descriptor.SetTypeName("Int32", CodeLanguage.Swift);
        
            return descriptor;
        }
    }
}