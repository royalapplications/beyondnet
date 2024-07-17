namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor FloatTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(float),
                "-1",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "float" },
                    { CodeLanguage.C, "float" },
                    { CodeLanguage.Swift, "Float" },
                    { CodeLanguage.Kotlin, "Float" },
                    { CodeLanguage.KotlinJNA, "Float" }
                }
            );
        
            return descriptor;
        }
    }
}