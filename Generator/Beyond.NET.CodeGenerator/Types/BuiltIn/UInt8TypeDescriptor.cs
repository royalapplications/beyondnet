namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor UInt8TypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(byte),
                "0",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "byte" },
                    { CodeLanguage.C, "uint8_t" },
                    { CodeLanguage.Swift, "UInt8" },
                    { CodeLanguage.Kotlin, "UByte" },
                    { CodeLanguage.KotlinJNA, "Byte" }
                }
            );
        
            return descriptor;
        }
    }
}