using System.Reflection;
using Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedFieldSyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IFieldSyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((FieldInfo)@object, state, configuration);
    }
    
    public string Write(FieldInfo field, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        Type declaringType = field.DeclaringType ?? throw new Exception("No declaring type");
        Type fieldType = field.FieldType;
        
        const bool mayThrow = false;
        const bool addToState = false;
        
        bool isStatic = field.IsStatic;
        
        bool canSet = !field.IsLiteral &&
                      !field.IsInitOnly;
        
        string fieldName = field.Name;

        CSharpCodeBuilder sb = new();

        string fieldGetterCode = WriteMethod(
            field,
            MemberKind.FieldGetter,
            fieldName,
            isStatic,
            mayThrow,
            declaringType,
            fieldType,
            Array.Empty<ParameterInfo>(),
            addToState,
            typeDescriptorRegistry,
            state,
            out string generatedGetterName
        );

        sb.AppendLine(fieldGetterCode);

        state.AddGeneratedMember(
            MemberKind.FieldGetter,
            field,
            mayThrow,
            generatedGetterName,
            CodeLanguage.CSharpUnmanaged
        );

        if (canSet) {
            string fieldSetterCode = WriteMethod(
                field,
                MemberKind.FieldSetter,
                fieldName,
                isStatic,
                mayThrow,
                declaringType,
                fieldType,
                Array.Empty<ParameterInfo>(),
                addToState,
                typeDescriptorRegistry,
                state,
                out string generatedSetterName
            );

            sb.AppendLine(fieldSetterCode);
            
            state.AddGeneratedMember(
                MemberKind.FieldSetter,
                field,
                mayThrow,
                generatedSetterName,
                CodeLanguage.CSharpUnmanaged
            );
        }

        return sb.ToString();
    }
}