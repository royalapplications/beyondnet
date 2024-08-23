namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    // TODO: The Kotlin bindings are incorrect. See https://www.mkammerer.de/blog/jna-and-unsigned-integers/
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
                },
                new Dictionary<LanguagePair, string>() {
                    {
                        new (CodeLanguage.KotlinJNA, CodeLanguage.Kotlin),
                        "{0}.toUByte()"
                    },
                    {
                        new (CodeLanguage.Kotlin, CodeLanguage.KotlinJNA),
                        "{0}.toByte()"
                    }
                }
            );
        
            return descriptor;
        }
    }
}