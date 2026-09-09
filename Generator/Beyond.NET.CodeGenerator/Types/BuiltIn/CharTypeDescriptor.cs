namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor CharTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(char),
                "(char)0", // TODO: Is this correct?
                new() {
                    { CodeLanguage.CSharpUnmanaged, "ushort" },
                    { CodeLanguage.C, "wchar_t" }, // TODO: Is this correct?
                    { CodeLanguage.Swift, "DNChar" }, // TODO: Is this correct?
                    { CodeLanguage.Kotlin, "Char" },
                    { CodeLanguage.KotlinJNA, "Char" }
                },
                new() {
                    { new LanguagePair(CodeLanguage.CSharpUnmanaged, CodeLanguage.CSharp), "(char){0}" },
                    { new LanguagePair(CodeLanguage.CSharp, CodeLanguage.CSharpUnmanaged), "(ushort){0}" },
                    { new LanguagePair(CodeLanguage.C, CodeLanguage.Swift), "DNChar(cValue: {0})" },
                    { new LanguagePair(CodeLanguage.Swift, CodeLanguage.C), "{0}.cValue" }
                }
            );

            return descriptor;
        }
    }
}