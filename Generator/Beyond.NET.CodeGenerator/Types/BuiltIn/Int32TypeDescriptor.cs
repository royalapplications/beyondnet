namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor Int32TypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(int),
                "-1",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "int" },
                    { CodeLanguage.C, "int32_t" },
                    { CodeLanguage.Swift, "Int32" },
                    { CodeLanguage.Kotlin, "Int" },
                    { CodeLanguage.KotlinJNA, "Int" }
                }
            );
        
            return descriptor;
        }
    }
}