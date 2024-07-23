using System.Reflection;
using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedPropertySyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IPropertySyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((PropertyInfo)@object, state, configuration);
    }
    
    public string Write(PropertyInfo property, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        const bool mayThrow = true;
        const bool addToState = false;

        string propertyName = property.Name;

        Type declaringType = property.DeclaringType ?? throw new Exception("No declaring type");;

        IEnumerable<ParameterInfo> parameters = property.GetIndexParameters();

        CSharpCodeBuilder sb = new();

        Type propertyType = property.PropertyType;

        MethodInfo? getterMethod = property.GetGetMethod(false);
        MethodInfo? setterMethod = property.GetPublicAndNonInitSetMethod();

        if (getterMethod is not null) {
            bool isStaticMethod = getterMethod.IsStatic;
            
            string getterCode = WriteMethod(
                getterMethod,
                MemberKind.PropertyGetter,
                propertyName,
                isStaticMethod,
                mayThrow,
                declaringType,
                propertyType,
                parameters,
                addToState,
                typeDescriptorRegistry,
                state,
                out string generatedName
            );

            sb.AppendLine(getterCode);
            
            state.AddGeneratedMember(
                MemberKind.PropertyGetter,
                property,
                mayThrow,
                generatedName,
                CodeLanguage.CSharpUnmanaged
            );
        }
        
        if (setterMethod is not null) {
            bool isStaticMethod = setterMethod.IsStatic;
            
            string setterCode = WriteMethod(
                setterMethod,
                MemberKind.PropertySetter,
                propertyName,
                isStaticMethod,
                mayThrow,
                declaringType,
                propertyType,
                parameters,
                addToState,
                typeDescriptorRegistry,
                state,
                out string generatedName
            );

            sb.AppendLine(setterCode);
            
            state.AddGeneratedMember(
                MemberKind.PropertySetter,
                property,
                mayThrow,
                generatedName,
                CodeLanguage.CSharpUnmanaged
            );
        }

        return sb.ToString();
    }
}