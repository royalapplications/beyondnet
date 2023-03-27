using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public class GeneratedMember
{
    private Dictionary<CodeLanguage, string> m_generatedNames = new();
    
    public MemberKind MemberKind { get; }
    public MemberInfo? Member { get; }
    public bool MayThrow { get; }

    public GeneratedMember(
        MemberKind memberKind,
        MemberInfo? member,
        bool mayThrow
    )
    {
        if (member == null &&
            memberKind == MemberKind.Automatic) {
            throw new Exception("Either member must be not null or memberKind must be something other than Automatic");
        }
        
        MemberKind = memberKind;
        Member = member;
        MayThrow = mayThrow;
    }

    public void SetGeneratedName(string name, CodeLanguage language)
    {
        m_generatedNames[language] = name;
    }

    public string? GetGeneratedName(CodeLanguage language)
    {
        m_generatedNames.TryGetValue(
            language,
            out string? generatedName
        );

        return generatedName;
    }
}