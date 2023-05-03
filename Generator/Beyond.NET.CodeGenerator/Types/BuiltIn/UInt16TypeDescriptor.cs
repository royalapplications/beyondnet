namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor UInt16TypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(ushort),
                "0",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "ushort" },
                    { CodeLanguage.C, "uint16_t" },
                    { CodeLanguage.Swift, "UInt16" }
                }
            );
        
            return descriptor;
        }
    }
}