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
                    { CodeLanguage.Swift, "UInt" } // TODO: This should be UnsafeMutableRawPointer? but then we have issues with overlapping overloads of methods that just differ by parameters that are either IntPtr or UIntPtr in Swift
                }
            );
        
            return descriptor;
        }
    }
}