namespace NativeAOT.CodeGenerator.Types.BuiltIn;

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
                    { CodeLanguage.C, "int" },
                    { CodeLanguage.Swift, "Int" }
                }
            );
        
            return descriptor;
        }
    }
}