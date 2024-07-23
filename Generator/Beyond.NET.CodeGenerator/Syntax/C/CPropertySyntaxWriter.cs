using System.Reflection;
using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.C;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.C;

public class CPropertySyntaxWriter: CMethodSyntaxWriter, IPropertySyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((PropertyInfo)@object, state, configuration);
    }
    
    public string Write(PropertyInfo property, State state, ISyntaxWriterConfiguration? configuration)
    {
        const bool addToState = false;
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");

        GeneratedMember? generatedMemberGetter = cSharpUnmanagedResult.GetGeneratedMember(property, MemberKind.PropertyGetter);
        GeneratedMember? generatedMemberSetter = cSharpUnmanagedResult.GetGeneratedMember(property, MemberKind.PropertySetter);

        if (generatedMemberGetter is null &&
            generatedMemberSetter is null) {
            throw new Exception("No generated C# Unmanaged Member");
        }

        bool getterMayThrow = generatedMemberGetter?.MayThrow ?? true;
        bool setterMayThrow = generatedMemberSetter?.MayThrow ?? true; 
        
        Type declaringType = property.DeclaringType ?? throw new Exception("No declaring type");

        IEnumerable<ParameterInfo> parameters = property.GetIndexParameters();

        CCodeBuilder sb = new();

        Type propertyType = property.PropertyType;

        MethodInfo? getterMethod = property.GetGetMethod(false);
        MethodInfo? setterMethod = property.GetPublicAndNonInitSetMethod();

        if (getterMethod is not null &&
            generatedMemberGetter is not null) {
            bool isStaticMethod = getterMethod.IsStatic;
            
            string getterCode = WriteMethod(
                generatedMemberGetter,
                getterMethod,
                MemberKind.PropertyGetter,
                isStaticMethod,
                getterMayThrow,
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
                getterMayThrow,
                generatedName,
                CodeLanguage.C
            );
        }
        
        if (setterMethod is not null &&
            generatedMemberSetter is not null) {
            bool isStaticMethod = setterMethod.IsStatic;
            
            string setterCode = WriteMethod(
                generatedMemberSetter,
                setterMethod,
                MemberKind.PropertySetter,
                isStaticMethod,
                setterMayThrow,
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
                getterMayThrow,
                generatedName,
                CodeLanguage.C
            );
        }

        return sb.ToString();
    }
}