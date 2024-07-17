namespace Beyond.NET.CodeGenerator.Types.BuiltIn;

public partial class BuiltInTypeDescriptors
{
    public static TypeDescriptor ReadOnlySpanOfByteTypeDescriptor
    {
        get {
            var descriptor = new TypeDescriptor(
                typeof(ReadOnlySpan<byte>),
                "DNReadOnlySpanOfByte.Empty",
                new() {
                    { CodeLanguage.CSharpUnmanaged, "DNReadOnlySpanOfByte" },
                    { CodeLanguage.C, "DNReadOnlySpanOfByte" },
                    { CodeLanguage.Swift, "Data?" }
                    // TODO: Kotlin
                },
                new() {
                    {
                        new LanguagePair(CodeLanguage.CSharpUnmanaged, CodeLanguage.CSharp),
                        "{0}.CopyDataToManagedReadOnlySpanAndFree()"
                    },
                    {
                        new LanguagePair(CodeLanguage.CSharp, CodeLanguage.CSharpUnmanaged),
                        "{0}.CopyToNativeReadOnlySpanOfByte()"
                    },
                    {
                        new LanguagePair(CodeLanguage.C, CodeLanguage.Swift),
                        "{0}.data()"
                    },
                    {
                        new LanguagePair(CodeLanguage.Swift, CodeLanguage.C),
                        "{0}.readOnlySpanOfByte()"
                    }
                }
            );
        
            return descriptor;
        }
    }
}