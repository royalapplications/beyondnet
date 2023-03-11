using NativeAOT.Core;

namespace NativeAOT.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor StringTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(typeof(string));
            descriptor.SetTypeName("byte", CodeLanguage.CSharpUnmanaged);
            descriptor.SetTypeName("char", CodeLanguage.C);
            descriptor.SetTypeName("String", CodeLanguage.Swift);
    
            return descriptor;
        }
    }
}