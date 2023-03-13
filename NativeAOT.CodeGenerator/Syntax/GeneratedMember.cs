using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public class GeneratedMember
{
    private Dictionary<CodeLanguage, string> m_generatedNames = new();
    
    public MemberInfo Member { get; }

    public GeneratedMember(MemberInfo member)
    {
        Member = member ?? throw new ArgumentNullException(nameof(member));
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