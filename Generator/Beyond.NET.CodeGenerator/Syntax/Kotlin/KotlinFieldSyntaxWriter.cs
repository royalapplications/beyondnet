using System.Reflection;

using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.Kotlin;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public class KotlinFieldSyntaxWriter: KotlinMethodSyntaxWriter, IFieldSyntaxWriter
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
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        GeneratedMember? cSharpGeneratedGetterMember = cSharpUnmanagedResult.GetGeneratedMember(field, MemberKind.FieldGetter);
        GeneratedMember? cGeneratedGetterMember = cResult.GetGeneratedMember(field, MemberKind.FieldGetter);
        
        GeneratedMember? cSharpGeneratedSetterMember = cSharpUnmanagedResult.GetGeneratedMember(field, MemberKind.FieldSetter);
        GeneratedMember? cGeneratedSetterMember = cResult.GetGeneratedMember(field, MemberKind.FieldSetter);

        if (cSharpGeneratedGetterMember is null &&
            cSharpGeneratedSetterMember is null) {
            throw new Exception("No C# generated member");
        }
        
        if (cGeneratedGetterMember is null &&
            cGeneratedSetterMember is null) {
            throw new Exception("No C generated member");
        }

        bool isStatic = field.IsStatic;
        Type declaringType = field.DeclaringType ?? throw new Exception("No declaring type");
        IEnumerable<ParameterInfo> parameters = Array.Empty<ParameterInfo>();
        Type fieldType = field.FieldType;

        KotlinCodeBuilder sb = new();

        if (cSharpGeneratedGetterMember is not null &&
            cGeneratedGetterMember is not null) {
            bool mayThrow = cSharpGeneratedGetterMember.MayThrow;
                
            string code = WriteMethod(
                cSharpGeneratedGetterMember,
                cGeneratedGetterMember,
                field,
                MemberKind.FieldGetter,
                isStatic,
                mayThrow,
                declaringType,
                fieldType,
                parameters,
                configuration,
                addToState,
                typeDescriptorRegistry,
                state,
                field,
                out string generatedName
            );

            sb.AppendLine(code);
            
            state.AddGeneratedMember(
                MemberKind.FieldGetter,
                field,
                mayThrow,
                generatedName,
                CodeLanguage.Kotlin
            );
        }

        if (cSharpGeneratedSetterMember is not null &&
            cGeneratedSetterMember is not null) {
            bool mayThrow = cSharpGeneratedSetterMember.MayThrow;
            
            string code = WriteMethod(
                cSharpGeneratedSetterMember,
                cGeneratedSetterMember,
                field,
                MemberKind.FieldSetter,
                isStatic,
                mayThrow,
                declaringType,
                fieldType,
                parameters,
                configuration,
                addToState,
                typeDescriptorRegistry,
                state,
                field,
                out string generatedName
            );

            sb.AppendLine(code);
            
            state.AddGeneratedMember(
                MemberKind.FieldSetter,
                field,
                mayThrow,
                generatedName,
                CodeLanguage.Kotlin
            );
        }

        return sb.ToString();
    }
}