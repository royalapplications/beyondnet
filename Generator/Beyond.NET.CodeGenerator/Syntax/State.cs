using System.Reflection;
using System.Runtime.CompilerServices;
using Beyond.NET.CodeGenerator.Generator;

namespace Beyond.NET.CodeGenerator.Syntax;

public class State
{
    public Settings? Settings { get; init; }
    
    private List<GeneratedMember> m_generatedMembers = new();
    public IEnumerable<GeneratedMember> GeneratedMembers => m_generatedMembers;
    
    private List<Type> m_skippedTypes = new();
    public IEnumerable<Type> SkippedTypes => m_skippedTypes;

    public Result? CSharpUnmanagedResult { get; }
    public Result? CResult { get; }

    public State() { }

    public State(Result cSharpUnmanagedResult)
    {
        CSharpUnmanagedResult = cSharpUnmanagedResult ?? throw new ArgumentNullException(nameof(cSharpUnmanagedResult));
    }
    
    public State(
        Result cSharpUnmanagedResult,
        Result cResult
    )
    {
        CSharpUnmanagedResult = cSharpUnmanagedResult ?? throw new ArgumentNullException(nameof(cSharpUnmanagedResult));
        CResult = cResult ?? throw new ArgumentNullException(nameof(cResult));
    }

    public GeneratedMember AddGeneratedMember(
        MemberKind memberKind,
        MemberInfo? member,
        bool mayThrow
    )
    {
        GeneratedMember generatedMember = new(
            memberKind,
            member,
            mayThrow
        );
        
        m_generatedMembers.Add(generatedMember);

        return generatedMember;
    }

    public GeneratedMember AddGeneratedMember(
        MemberKind memberKind,
        MemberInfo? member,
        bool mayThrow,
        string generatedName,
        CodeLanguage generatedNameLanguage
    )
    {
        GeneratedMember generatedMember = AddGeneratedMember(
            memberKind,
            member,
            mayThrow
        );
        
        generatedMember.SetGeneratedName(generatedName, generatedNameLanguage);
        
        return generatedMember;
    }

    public void AddSkippedType(Type type)
    {
        m_skippedTypes.Add(type);
    }

    public string UniqueGeneratedName(string proposedName, CodeLanguage language)
    {
        var membersThatStartWithProposedName = m_generatedMembers.Where(
            m => m.GetGeneratedName(language)?.StartsWith(proposedName) ?? false
        ).ToArray();

        if (membersThatStartWithProposedName.Length == 0) {
            return proposedName;
        }

        string proposedNameWithOverloadPrefix = proposedName + "_";

        var overloadedMembers = membersThatStartWithProposedName.Where(
            m => m.GetGeneratedName(language)?.StartsWith(proposedNameWithOverloadPrefix) ?? false
        );

        int highestIndex = 0;

        foreach (var member in overloadedMembers) {
            string? memberName = member.GetGeneratedName(language);

            if (memberName == null) {
                continue;
            }

            string textAfterName = memberName.Substring(proposedNameWithOverloadPrefix.Length);

            if (string.IsNullOrEmpty(textAfterName)) {
                continue;
            }

            if (!int.TryParse(textAfterName, out int index)) {
                continue;
            }

            highestIndex = index;
        }

        int newIndex = highestIndex + 1;

        string newName = $"{proposedNameWithOverloadPrefix}{newIndex}";

        return newName;
    }

    public IEnumerable<GeneratedMember> GetGeneratedMembersThatAreExtensions()
    {
        List<GeneratedMember> extensionMembers = new();
        
        foreach (var generatedMember in GeneratedMembers) {
            MethodBase? methodBase = generatedMember.Member as MethodBase;
            
            if (methodBase is null) {
                continue;
            }

            Type? declaringType = methodBase.DeclaringType;

            if (declaringType is null) {
                continue;
            }
            
            if (declaringType.IsGenericType ||
                declaringType.IsNested ||
                !declaringType.IsSealed ||
                !methodBase.IsDefined(typeof(ExtensionAttribute))) {
                continue;
            }

            var parameters = methodBase.GetParameters();

            if (parameters.Length < 1) {
                continue;
            }
            
            extensionMembers.Add(generatedMember);
        }

        return extensionMembers;
    }
}