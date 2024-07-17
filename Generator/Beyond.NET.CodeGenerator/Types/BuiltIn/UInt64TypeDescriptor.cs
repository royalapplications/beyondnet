namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
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
                }
            );
        
            return descriptor;
        }
    }
}