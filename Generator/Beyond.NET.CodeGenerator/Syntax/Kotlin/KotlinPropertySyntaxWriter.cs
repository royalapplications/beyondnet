using System.Reflection;
using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.Kotlin;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public class KotlinPropertySyntaxWriter: KotlinMethodSyntaxWriter, IPropertySyntaxWriter
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
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");

        GeneratedMember? cSharpGeneratedMemberGetter = cSharpUnmanagedResult.GetGeneratedMember(property, MemberKind.PropertyGetter);
        GeneratedMember? cGeneratedMemberGetter = cResult.GetGeneratedMember(property, MemberKind.PropertyGetter);
        
        GeneratedMember? cSharpGeneratedMemberSetter = cSharpUnmanagedResult.GetGeneratedMember(property, MemberKind.PropertySetter);
        GeneratedMember? cGeneratedMemberSetter = cResult.GetGeneratedMember(property, MemberKind.PropertySetter);

        if (cSharpGeneratedMemberGetter is null &&
            cSharpGeneratedMemberSetter is null) {
            throw new Exception("No generated C# Unmanaged Member");
        }
        
        if (cGeneratedMemberGetter is null &&
            cGeneratedMemberSetter is null) {
            throw new Exception("No generated C Member");
        }

        bool getterMayThrow = cSharpGeneratedMemberGetter?.MayThrow ?? true;
        bool setterMayThrow = cSharpGeneratedMemberSetter?.MayThrow ?? true; 
        
        Type declaringType = property.DeclaringType ?? throw new Exception("No declaring type");

        IEnumerable<ParameterInfo> parameters = property.GetIndexParameters();

        KotlinCodeBuilder sb = new();

        Type propertyType = property.PropertyType;

        MethodInfo? getterMethod = property.GetGetMethod(false);
        MethodInfo? setterMethod = property.GetPublicAndNonInitSetMethod();

        if (getterMethod is not null &&
            cSharpGeneratedMemberGetter is not null &&
            cGeneratedMemberGetter is not null) {
            bool isStaticMethod = getterMethod.IsStatic;
            
            string getterCode = WriteMethod(
                cSharpGeneratedMemberGetter,
                cGeneratedMemberGetter,
                getterMethod,
                MemberKind.PropertyGetter,
                isStaticMethod,
                getterMayThrow,
                declaringType,
                propertyType,
                parameters,
                configuration,
                addToState,
                typeDescriptorRegistry,
                state,
                property,
                out string generatedName
            );

            sb.AppendLine(getterCode);
            
            state.AddGeneratedMember(
                MemberKind.PropertyGetter,
                property,
                getterMayThrow,
                generatedName,
                CodeLanguage.Kotlin
            );
        }
        
        if (setterMethod is not null &&
            cSharpGeneratedMemberSetter is not null &&
            cGeneratedMemberSetter is not null) {
            bool isStaticMethod = setterMethod.IsStatic;
            
            string setterCode = WriteMethod(
                cSharpGeneratedMemberSetter,
                cGeneratedMemberSetter,
                setterMethod,
                MemberKind.PropertySetter,
                isStaticMethod,
                setterMayThrow,
                declaringType,
                propertyType,
                parameters,
                configuration,
                addToState,
                typeDescriptorRegistry,
                state,
                property,
                out string generatedName
            );

            sb.AppendLine(setterCode);
            
            state.AddGeneratedMember(
                MemberKind.PropertySetter,
                property,
                getterMayThrow,
                generatedName,
                CodeLanguage.Kotlin
            );
        }

        return sb.ToString();
    }
}