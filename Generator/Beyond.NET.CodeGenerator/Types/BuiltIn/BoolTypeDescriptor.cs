namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor BoolTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(bool),
                "0",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "byte" },
                    { CodeLanguage.C, "bool" },
                    { CodeLanguage.Swift, "Bool" },
                    { CodeLanguage.KotlinJNA, "Boolean" },
                    { CodeLanguage.Kotlin, "Boolean" }
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