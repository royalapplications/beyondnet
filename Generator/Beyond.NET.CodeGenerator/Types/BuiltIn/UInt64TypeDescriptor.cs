namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    // TODO: The Kotlin bindings are incorrect. See https://www.mkammerer.de/blog/jna-and-unsigned-integers/
    public static TypeDescriptor UInt64TypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(ulong),
                "0",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "ulong" },
                    { CodeLanguage.C, "uint64_t" },
                    { CodeLanguage.Swift, "UInt64" },
                    { CodeLanguage.Kotlin, "ULong" },
                    { CodeLanguage.KotlinJNA, "Long" }
                },
                new Dictionary<LanguagePair, string>() {
                    {
                        new (CodeLanguage.KotlinJNA, CodeLanguage.Kotlin),
                        "{0}.toULong()"
                    },
                    {
                        new (CodeLanguage.Kotlin, CodeLanguage.KotlinJNA),
                        "{0}.toLong()"
                    }
                }
            );
        
            return descriptor;
        }
    }
}