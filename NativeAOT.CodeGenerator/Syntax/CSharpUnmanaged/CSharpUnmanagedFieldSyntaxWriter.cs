using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedFieldSyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IFieldSyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((FieldInfo)@object, state);
    }
    
    public string Write(FieldInfo field, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        Type declaringType = field.DeclaringType ?? throw new Exception("No declaring type");
        Type fieldType = field.FieldType;
        
        const bool mayThrow = true;
        bool isStatic = field.IsStatic;
        
        bool canSet = !field.IsLiteral &&
                      !field.IsInitOnly;
        
        string fieldName = field.Name;

        StringBuilder sb = new();

        string fieldGetterCode = WriteMethod(
            field,
            MemberKind.FieldGetter,
            fieldName,
            isStatic,
            mayThrow,
            declaringType,
            fieldType,
            Array.Empty<ParameterInfo>(),
            typeDescriptorRegistry,
            state
        );

        sb.AppendLine(fieldGetterCode);

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
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(fieldSetterCode);
        }

        return sb.ToString();
    }
}