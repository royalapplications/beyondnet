namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor DoubleTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(double),
                "-1",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "double" },
                    { CodeLanguage.C, "double" },
                    { CodeLanguage.Swift, "Double" },
                    { CodeLanguage.Kotlin, "Double" },
                    { CodeLanguage.KotlinJNA, "Double" }
                }
            );
        
            return descriptor;
        }
    }
}