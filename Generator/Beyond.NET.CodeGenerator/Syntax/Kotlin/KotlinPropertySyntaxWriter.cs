using System.Reflection;
using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.Kotlin;
using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;
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

        Type propertyType = property.PropertyType;

        MethodInfo? getterMethod = property.GetGetMethod(false);
        MethodInfo? setterMethod = property.GetPublicAndNonInitSetMethod();

        string? getterCode;
        string? generatedGetterName;
        KotlinPropertyInfo? getterInfo;

        if (getterMethod is not null &&
            cSharpGeneratedMemberGetter is not null &&
            cGeneratedMemberGetter is not null) {
            bool isStaticMethod = getterMethod.IsStatic;

            getterCode = WriteMethod(
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
                out generatedGetterName,
                out getterInfo
            );
        } else {
            getterCode = null;
            generatedGetterName = null;
            getterInfo = null;
        }

        string? setterCode;
        string? generatedSetterName;
        KotlinPropertyInfo? setterInfo;

        if (setterMethod is not null &&
            cSharpGeneratedMemberSetter is not null &&
            cGeneratedMemberSetter is not null) {
            bool isStaticMethod = setterMethod.IsStatic;

            setterCode = WriteMethod(
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
                out generatedSetterName,
                out setterInfo
            );
        } else {
            setterCode = null;
            generatedSetterName = null;
            setterInfo = null;
        }

        KotlinCodeBuilder sb = new();

        if (getterInfo is not null ||
            setterInfo is not null) {
            KotlinComputedPropertyDeclaration prop;

            if (getterInfo is not null) {
                state.AddGeneratedMember(
                    MemberKind.PropertyGetter,
                    property,
                    getterMayThrow,
                    getterInfo.Name,
                    CodeLanguage.Kotlin
                );
            }

            if (setterInfo is not null) {
                state.AddGeneratedMember(
                    MemberKind.PropertySetter,
                    property,
                    setterMayThrow,
                    setterInfo.Name,
                    CodeLanguage.Kotlin
                );
            }

            var propName = getterInfo?.Name ?? setterInfo?.Name;

            if (string.IsNullOrEmpty(propName)) {
                throw new Exception("Property without a name is not valid");
            }

            var propType = getterInfo?.TypeName ?? setterInfo?.TypeName;

            if (string.IsNullOrEmpty(propType)) {
                throw new Exception("Property without a type is not valid");
            }

            var propVisibility = getterInfo?.Visibility ?? setterInfo?.Visibility ?? KotlinVisibilities.Public;
            bool propIsOverride = getterInfo?.IsOverride ?? setterInfo?.IsOverride ?? false;

            prop = new(
                propName,
                propType,
                propVisibility,
                propIsOverride,
                getterInfo?.Implementation,
                getterInfo?.JvmName,
                setterInfo?.Implementation,
                setterInfo?.JvmName
            );

            var declComment = getterInfo?.DeclarationComment ?? setterInfo?.DeclarationComment;

            if (!string.IsNullOrEmpty(declComment)) {
                sb.AppendLine(declComment);
            }

            sb.AppendLine(prop.ToString());
        } else {
            if (getterCode is not null &&
                generatedGetterName is not null) {
                sb.AppendLine(getterCode);

                state.AddGeneratedMember(
                    MemberKind.PropertyGetter,
                    property,
                    getterMayThrow,
                    generatedGetterName,
                    CodeLanguage.Kotlin
                );
            }

            if (setterCode is not null &&
                generatedSetterName is not null) {
                sb.AppendLine(setterCode);

                state.AddGeneratedMember(
                    MemberKind.PropertySetter,
                    property,
                    setterMayThrow,
                    generatedSetterName,
                    CodeLanguage.Kotlin
                );
            }
        }

        return sb.ToString();
    }
}
