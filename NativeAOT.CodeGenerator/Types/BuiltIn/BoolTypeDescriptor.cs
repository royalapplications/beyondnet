namespace NativeAOT.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor BoolTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(bool),
                "false",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "CBool" },
                    { CodeLanguage.C, "CBool" },
                    { CodeLanguage.Swift, "CBool" }
                },
                new() {
                    { 
                        new(CodeLanguage.CSharp, CodeLanguage.CSharpUnmanaged), 
                        "{0}.ToCBool()"
                    }, {
                        new(CodeLanguage.CSharpUnmanaged, CodeLanguage.CSharp), 
                        "{0}.ToBool()"
                    }
                }
            );
        
            return descriptor;
        }
    }
}