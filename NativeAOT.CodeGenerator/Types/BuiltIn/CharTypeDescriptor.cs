namespace NativeAOT.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor CharTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(char),
                "(char)0", // TODO: Is this correct?
                new() {
                    { CodeLanguage.CSharpUnmanaged, "char" },
                    { CodeLanguage.C, "uint8_t" }, // TODO: Is this correct?
                    { CodeLanguage.Swift, "UInt8" } // TODO: Is this correct?
                }
            );
        
            return descriptor;
        }
    }
}