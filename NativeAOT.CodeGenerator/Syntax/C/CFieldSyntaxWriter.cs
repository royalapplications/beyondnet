using System.Reflection;

using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CFieldSyntaxWriter: CMethodSyntaxWriter, IFieldSyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((FieldInfo)@object, state);
    }

    public string Write(FieldInfo field, State state)
    {
        return Write(field, MemberKind.Automatic, state);
    }
    
    public string Write(FieldInfo field, MemberKind memberKind, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");

        bool isStatic = field.IsStatic;
        Type declaringType = field.DeclaringType ?? throw new Exception("No declaring type");
        Type fieldType = field.FieldType;
        
        var generatedMember = cSharpUnmanagedResult.GetGeneratedMember(field, memberKind) ?? throw new Exception("No C# unmanaged generated member");

        string memberCode = WriteMethod(
            field,
            memberKind,
            isStatic,
            generatedMember.MayThrow,
            declaringType,
            fieldType,
            Array.Empty<ParameterInfo>(),
            typeDescriptorRegistry,
            state
        );

        return memberCode;
    }
}