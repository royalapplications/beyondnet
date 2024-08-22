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
                    { CodeLanguage.Swift, "UInt32" },
                    { CodeLanguage.Kotlin, "UInt" },
                    { CodeLanguage.KotlinJNA, "Int" }
                }, new Dictionary<LanguagePair, string>() {
                    {
                        new (CodeLanguage.KotlinJNA, CodeLanguage.Kotlin),
                        "{0}.toUInt()"
                    },
                    {
                        new (CodeLanguage.Kotlin, CodeLanguage.KotlinJNA),
                        "{0}.toInt()"
                    }
                }
            );
        
            return descriptor;
        }
    }
}