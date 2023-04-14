using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.Swift;

public class SwiftPropertySyntaxWriter: SwiftMethodSyntaxWriter, IPropertySyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((PropertyInfo)@object, state);
    }
    
    public string Write(PropertyInfo property, State state)
    {
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

        IEnumerable<ParameterInfo> parameters = Array.Empty<ParameterInfo>();

        StringBuilder sb = new();

        Type propertyType = property.PropertyType;

        MethodInfo? getterMethod = property.GetGetMethod(false);
        MethodInfo? setterMethod = property.GetSetMethod(false);

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
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(getterCode);
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
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(setterCode);
        }

        return sb.ToString();
    }
}