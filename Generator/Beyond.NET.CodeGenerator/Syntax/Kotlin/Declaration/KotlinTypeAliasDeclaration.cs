using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public class KotlinTypeAliasDeclaration
{
    public string Name { get; }
    public string TypeName { get; }

    public KotlinTypeAliasDeclaration(
        string name,
        string typeName
    )
    {
        Name = name;
        TypeName = typeName;
    }

    public override string ToString()
    {
        const string typealiasKeyword = "typealias";

        var fullDecl = $"{typealiasKeyword} {Name} = {TypeName}";

        return fullDecl;
    }
}
