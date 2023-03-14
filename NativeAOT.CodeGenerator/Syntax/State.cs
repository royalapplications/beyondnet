using System.Reflection;
using NativeAOT.CodeGenerator.Generator;

namespace NativeAOT.CodeGenerator.Syntax;

public class State
{
    private List<GeneratedMember> m_generatedMembers = new();

    public IEnumerable<GeneratedMember> GeneratedMembers
    {
        get {
            return m_generatedMembers;
        }
    }
    
    public Result? CSharpUnmanagedResult { get; }

    public State() { }

    public State(Result cSharpUnmanagedResult)
    {
        CSharpUnmanagedResult = cSharpUnmanagedResult ?? throw new ArgumentNullException(nameof(cSharpUnmanagedResult));
    }

    public GeneratedMember AddGeneratedMember(MemberInfo member)
    {
        GeneratedMember generatedMember = new(member);
        
        m_generatedMembers.Add(generatedMember);

        return generatedMember;
    }

    public GeneratedMember AddGeneratedMember(
        MemberInfo member,
        string generatedName,
        CodeLanguage generatedNameLanguage
    )
    {
        GeneratedMember generatedMember = AddGeneratedMember(member);
        
        generatedMember.SetGeneratedName(generatedName, generatedNameLanguage);
        
        return generatedMember;
    }

    public string UniqueGeneratedName(string proposedName, CodeLanguage language)
    {
        var membersThatStartWithProposedName = m_generatedMembers.Where(
            m => m.GetGeneratedName(language)?.StartsWith(proposedName) ?? false
        ).ToArray();

        if (membersThatStartWithProposedName.Length == 0) {
            return proposedName;
        }

        int highestIndex = 0;

        foreach (var member in membersThatStartWithProposedName) {
            string? memberName = member.GetGeneratedName(language);

            if (memberName == null) {
                continue;
            }

            string textAfterName = memberName.Substring(proposedName.Length);

            if (string.IsNullOrEmpty(textAfterName)) {
                continue;
            }

            if (!int.TryParse(textAfterName, out int index)) {
                continue;
            }

            highestIndex = index;
        }

        int newIndex = highestIndex + 1;

        string newName = $"{proposedName}{newIndex}";

        return newName;
    }
}