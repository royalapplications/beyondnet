using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public class GeneratedMember
{
    private Dictionary<CodeLanguage, string> m_generatedNames = new();
    
    public MemberInfo Member { get; }
    public bool MayThrow { get; }

    public GeneratedMember(
        MemberInfo member,
        bool mayThrow
    )
    {
        Member = member ?? throw new ArgumentNullException(nameof(member));
        MayThrow = mayThrow;
    }

    public void SetGeneratedName(string name, CodeLanguage language)
    {
        m_generatedNames[language] = name;
    }

    public string? GetGeneratedName(CodeLanguage language)
    {
        m_generatedNames.TryGetValue(language, out string? generatedName);

        return generatedName;
    }
}