using System.Reflection;
using System.Text;

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
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        GeneratedMember? generatedGetterMember = cSharpUnmanagedResult.GetGeneratedMember(field, MemberKind.FieldGetter);
        GeneratedMember? generatedSetterMember = cSharpUnmanagedResult.GetGeneratedMember(field, MemberKind.FieldSetter);

        if (generatedGetterMember is null &&
            generatedSetterMember is null) {
            throw new Exception("No C# generated member");
        }

        bool isStatic = field.IsStatic;
        Type declaringType = field.DeclaringType ?? throw new Exception("No declaring type");
        IEnumerable<ParameterInfo> parameters = Array.Empty<ParameterInfo>();
        Type fieldType = field.FieldType;

        StringBuilder sb = new();

        if (generatedGetterMember is not null) {
            string code = WriteMethod(
                generatedGetterMember,
                field,
                MemberKind.FieldGetter,
                isStatic,
                generatedGetterMember.MayThrow,
                declaringType,
                fieldType,
                parameters,
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(code);
        }

        if (generatedSetterMember is not null) {
            string code = WriteMethod(
                generatedSetterMember,
                field,
                MemberKind.FieldSetter,
                isStatic,
                generatedSetterMember.MayThrow,
                declaringType,
                fieldType,
                parameters,
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(code);
        }

        return sb.ToString();
    }
}