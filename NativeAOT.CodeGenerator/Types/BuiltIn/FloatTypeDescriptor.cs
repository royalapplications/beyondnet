namespace NativeAOT.CodeGenerator.Types.BuiltIn;

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
                    { CodeLanguage.C, "float" }, // TODO: is that correct?
                    { CodeLanguage.Swift, "Float" } // TODO: is that correct?
                }
            );
        
            return descriptor;
        }
    }
}