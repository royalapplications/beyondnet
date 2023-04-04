namespace NativeAOT.CodeGenerator.Types.BuiltIn;

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
                    { CodeLanguage.C, "unsigned long" },
                    { CodeLanguage.Swift, "UInt" }
                }
            );
        
            return descriptor;
        }
    }
}