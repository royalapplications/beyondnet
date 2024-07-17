namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor VoidTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(void),
                null,
                new() {
                    { CodeLanguage.CSharpUnmanaged, "void" },
                    { CodeLanguage.C, "void" },
                    { CodeLanguage.Swift, "Void" },
                    { CodeLanguage.Kotlin, "void" },
                    { CodeLanguage.KotlinJNA, "void" }
                }
            );
        
            return descriptor;
        }
    }
}