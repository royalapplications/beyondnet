namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    // TODO: The Kotlin bindings are incorrect. See https://www.mkammerer.de/blog/jna-and-unsigned-integers/
    public static TypeDescriptor UInt16TypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(ushort),
                "0",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "ushort" },
                    { CodeLanguage.C, "uint16_t" },
                    { CodeLanguage.Swift, "UInt16" },
                    { CodeLanguage.Kotlin, "UShort" },
                    { CodeLanguage.KotlinJNA, "Short" }
                }, new Dictionary<LanguagePair, string>() {
                    {
                        new (CodeLanguage.KotlinJNA, CodeLanguage.Kotlin),
                        "{0}.toUShort()"
                    },
                    {
                        new (CodeLanguage.Kotlin, CodeLanguage.KotlinJNA),
                        "{0}.toShort()"
                    }
                }
            );
        
            return descriptor;
        }
    }
}