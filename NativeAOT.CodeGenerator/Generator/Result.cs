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
}