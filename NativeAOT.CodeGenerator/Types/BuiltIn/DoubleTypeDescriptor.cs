namespace NativeAOT.CodeGenerator.Types.BuiltIn;

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
                    { CodeLanguage.C, "double" }, // TODO: is that correct?
                    { CodeLanguage.Swift, "double" } // TODO: is that correct?
                }
            );
        
            return descriptor;
        }
    }
}