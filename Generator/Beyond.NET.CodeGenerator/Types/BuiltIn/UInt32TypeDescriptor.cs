namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor UInt32TypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(uint),
                "0",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "uint" },
                    { CodeLanguage.C, "uint32_t" },
                    { CodeLanguage.Swift, "UInt32" }
                }
            );
        
            return descriptor;
        }
    }
}