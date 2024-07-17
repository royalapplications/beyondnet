namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinFunSignatureParameter
{
    public string Name { get; }
    public string TypeName { get; }
    
    public KotlinFunSignatureParameter(
        string name,
        string typeName
    )
    {
        Name = name;
        TypeName = typeName;
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(TypeName)) {
            throw new ArgumentOutOfRangeException(nameof(TypeName));
        }
        
        if (string.IsNullOrEmpty(Name)) {
            throw new ArgumentOutOfRangeException(nameof(Name));
        }

        string nameAndColon = $"{Name}:";
        
        string parameter = KotlinFunSignatureComponents.ComponentsToString(new[] {
            nameAndColon,
            TypeName
        });

        return parameter;
    }
}