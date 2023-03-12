namespace NativeAOT.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor StringTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(string),
                new() {
                    { CodeLanguage.CSharpUnmanaged, "byte" },
                    { CodeLanguage.C, "char" },
                    { CodeLanguage.Swift, "String" }
                },
                new() {
                    { 
                        new(CodeLanguage.CSharp, CodeLanguage.CSharpUnmanaged), 
                        "{0}.CopyToCString()"
                    }, {
                        new(CodeLanguage.CSharpUnmanaged, CodeLanguage.CSharp), 
                        "{0}.ToDotNetString()"
                    }
                }
            );
    
            return descriptor;
        }
    }
}