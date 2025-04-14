using System.Reflection;

using Beyond.NET.CodeGenerator.Syntax;

namespace Beyond.NET.CodeGenerator.Generator;

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

    public GeneratedMember? GetGeneratedDestructor(Type type)
    {
        var destructors = GetGeneratedMembers(
            type,
            MemberKind.Destructor
        );

        if (destructors.Count() > 1) {
            throw new Exception("More than one destructor");
        }

        return destructors.FirstOrDefault();
    }

    public GeneratedMember? GetGeneratedTypeOf(Type type)
    {
        var typeOfs = GetGeneratedMembers(
            type,
            MemberKind.TypeOf
        );

        if (typeOfs.Count() > 1) {
            throw new Exception("More than one typeOf");
        }

        return typeOfs.FirstOrDefault();
    }

    public IEnumerable<GeneratedMember> GetGeneratedMembers(
        Type type,
        MemberKind memberKind
    )
    {
        var generatedMembers = m_generatedTypes[type];

        List<GeneratedMember> members = new();

        foreach (var generatedMember in generatedMembers) {
            if (generatedMember.MemberKind == memberKind) {
                members.Add(generatedMember);
            }
        }

        return members;
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

    public GeneratedMember? GetGeneratedMember(
        MemberInfo member,
        MemberKind memberKind
    )
    {
        Type declaringType = member.DeclaringType ?? throw new Exception("No declaring type");

        var generatedMembers = m_generatedTypes[declaringType];

        foreach (var generatedMember in generatedMembers) {
            if (generatedMember.Member == member &&
                generatedMember.MemberKind == memberKind) {
                return generatedMember;
            }
        }

        return null;
    }

    public IEnumerable<Type> GetTypesInNamespace(string @namespace)
    {
        IEnumerable<Type> typesInNamespace = GeneratedTypes.Keys.Where(t => t.Namespace == @namespace);

        return typesInNamespace;
    }

    public NamespaceNode GetNamespaceTreeOfGeneratedTypes()
    {
        var types = GeneratedTypes.Keys;

        var tree = NamespaceNode.FromTypes(types);

        return tree;
    }
}