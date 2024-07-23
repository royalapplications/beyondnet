using System.Reflection;

using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.C;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.C;

public class CFieldSyntaxWriter: CMethodSyntaxWriter, IFieldSyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((FieldInfo)@object, state, configuration);
    }

    public string Write(FieldInfo field, State state, ISyntaxWriterConfiguration? configuration)
    {
        const bool addToState = false;
        
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

        CCodeBuilder sb = new();

        if (generatedGetterMember is not null) {
            bool mayThrow = generatedGetterMember.MayThrow;
            
            string code = WriteMethod(
                generatedGetterMember,
                field,
                MemberKind.FieldGetter,
                isStatic,
                mayThrow,
                declaringType,
                fieldType,
                parameters,
                addToState,
                typeDescriptorRegistry,
                state,
                out string generatedName
            );

            sb.AppendLine(code);
            
            state.AddGeneratedMember(
                MemberKind.FieldGetter,
                field,
                mayThrow,
                generatedName,
                CodeLanguage.C
            );
        }

        if (generatedSetterMember is not null) {
            bool mayThrow = generatedSetterMember.MayThrow;
            
            string code = WriteMethod(
                generatedSetterMember,
                field,
                MemberKind.FieldSetter,
                isStatic,
                mayThrow,
                declaringType,
                fieldType,
                parameters,
                addToState,
                typeDescriptorRegistry,
                state,
                out string generatedName
            );

            sb.AppendLine(code);
            
            state.AddGeneratedMember(
                MemberKind.FieldSetter,
                field,
                mayThrow,
                generatedName,
                CodeLanguage.C
            );
        }

        return sb.ToString();
    }
}