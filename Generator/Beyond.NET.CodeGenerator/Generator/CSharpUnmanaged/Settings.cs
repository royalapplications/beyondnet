namespace Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;

public class Settings: Generator.Settings
{
    public string NamespaceForGeneratedCode { get; }

    public Settings(string namespaceForGeneratedCode)
    {
        NamespaceForGeneratedCode = namespaceForGeneratedCode ?? throw new ArgumentNullException(nameof(namespaceForGeneratedCode));
    }
}