namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor NuintTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(nuint),
                "nuint.Zero",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "nuint" },
                    { CodeLanguage.C, "uintptr_t" }, // TODO: This should be void* but then we have issues with overlapping overloads of methods that just differ by parameters that are either IntPtr or UIntPtr in Swift
                    { CodeLanguage.Swift, "UInt" }, // TODO: This should be UnsafeMutableRawPointer? but then we have issues with overlapping overloads of methods that just differ by parameters that are either IntPtr or UIntPtr in Swift
                    { CodeLanguage.Kotlin, "UPointer" }, // TODO: Is this correct?
                    { CodeLanguage.KotlinJNA, "Pointer" } // TODO: Is this correct?
                },
                new Dictionary<LanguagePair, string>() {
                    {
                        new (CodeLanguage.KotlinJNA, CodeLanguage.Kotlin),
                        "{0}.toUPointer()"
                    },
                    {
                        new (CodeLanguage.Kotlin, CodeLanguage.KotlinJNA),
                        "{0}.toPointer()"
                    }
                }
            );
        
            return descriptor;
        }
    }
}