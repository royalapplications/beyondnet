using System.Reflection;
using NativeAOT.CodeGenerator.Syntax;

namespace NativeAOT.CodeGenerator.Generator;

public class Result
{
    private Dictionary<Type, IEnumerable<GeneratedMember>> m_generatedTypes = new();

    public Dictionary<Type, IEnumerable<GeneratedMember>> GeneratedTypes
    {
        get {
            return m_generatedTypes;
        }
    }

    public void AddGeneratedType(
        Type type,
        IEnumerable<GeneratedMember> generatedMembers
    )
    {
        m_generatedTypes[type] = generatedMembers;
    }

    public GeneratedMember? GetGeneratedMember(MemberInfo member)
    {
        Type declaringType = member.DeclaringType ?? throw new Exception("No declaring type");

        var generatedMembers = m_generatedTypes[declaringType];

        foreach (var generatedMember in generatedMembers) {
            if (generatedMember.Member == member) {
                return generatedMember;
            }
        }

        return null;
    }
}