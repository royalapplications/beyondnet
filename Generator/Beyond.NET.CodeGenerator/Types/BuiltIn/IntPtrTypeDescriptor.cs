namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor NintTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(nint),
                "nint.Zero",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "nint" },
                    { CodeLanguage.C, "void*" },
                    { CodeLanguage.Swift, "UnsafeMutableRawPointer?" },
                    { CodeLanguage.Kotlin, "Pointer" },
                    { CodeLanguage.KotlinJNA, "Pointer" }
                }
            );
        
            return descriptor;
        }
    }
}