namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor Int64TypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(long),
                "-1",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "long" },
                    { CodeLanguage.C, "int64_t" },
                    { CodeLanguage.Swift, "Int64" },
                    { CodeLanguage.Kotlin, "Long" },
                    { CodeLanguage.KotlinJNA, "Long" }
                }
            );
        
            return descriptor;
        }
    }
}