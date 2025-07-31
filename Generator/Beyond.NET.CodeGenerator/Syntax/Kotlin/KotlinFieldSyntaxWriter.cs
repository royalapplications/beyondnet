using System.Reflection;

using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.Kotlin;
using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;
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

        bool getterMayThrow = cSharpGeneratedGetterMember?.MayThrow ?? true;
        bool setterMayThrow = cSharpGeneratedSetterMember?.MayThrow ?? true;

        bool isStatic = field.IsStatic;
        Type declaringType = field.DeclaringType ?? throw new Exception("No declaring type");
        IEnumerable<ParameterInfo> parameters = Array.Empty<ParameterInfo>();
        Type fieldType = field.FieldType;

        string? getterCode;
        string? generatedGetterName;
        KotlinPropertyInfo? getterInfo;

        if (cSharpGeneratedGetterMember is not null &&
            cGeneratedGetterMember is not null) {
            getterCode = WriteMethod(
                cSharpGeneratedGetterMember,
                cGeneratedGetterMember,
                field,
                MemberKind.FieldGetter,
                isStatic,
                getterMayThrow,
                declaringType,
                fieldType,
                parameters,
                configuration,
                addToState,
                typeDescriptorRegistry,
                state,
                field,
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

        if (cSharpGeneratedSetterMember is not null &&
            cGeneratedSetterMember is not null) {
            setterCode = WriteMethod(
                cSharpGeneratedSetterMember,
                cGeneratedSetterMember,
                field,
                MemberKind.FieldSetter,
                isStatic,
                setterMayThrow,
                declaringType,
                fieldType,
                parameters,
                configuration,
                addToState,
                typeDescriptorRegistry,
                state,
                field,
                out generatedSetterName,
                out setterInfo
            );
        } else {
            setterCode = null;
            generatedSetterName = null;
            setterInfo = null;
        }

        KotlinCodeBuilder sb = new();

        if (getterInfo is not null) {
            KotlinComputedPropertyDeclaration prop;

            state.AddGeneratedMember(
                MemberKind.FieldGetter,
                field,
                getterMayThrow,
                getterInfo.Name,
                CodeLanguage.Kotlin
            );

            if (setterInfo is not null) {
                prop = new(
                    getterInfo.Name,
                    getterInfo.TypeName,
                    getterInfo.Visibility,
                    getterInfo.IsOverride,
                    getterInfo.Implementation,
                    getterInfo.JvmName,
                    setterInfo.Implementation,
                    setterInfo.JvmName
                );

                state.AddGeneratedMember(
                    MemberKind.FieldSetter,
                    field,
                    setterMayThrow,
                    setterInfo.Name,
                    CodeLanguage.Kotlin
                );
            } else {
                prop = new(
                    getterInfo.Name,
                    getterInfo.TypeName,
                    getterInfo.Visibility,
                    getterInfo.IsOverride,
                    getterInfo.Implementation,
                    getterInfo.JvmName,
                    null,
                    null
                );
            }

            if (!string.IsNullOrEmpty(getterInfo.DeclarationComment)) {
                sb.AppendLine(getterInfo.DeclarationComment);
            }

            sb.AppendLine(prop.ToString());
        } else {
            if (getterCode is not null &&
                generatedGetterName is not null) {
                sb.AppendLine(getterCode);

                state.AddGeneratedMember(
                    MemberKind.FieldGetter,
                    field,
                    getterMayThrow,
                    generatedGetterName,
                    CodeLanguage.Kotlin
                );
            }

            if (setterCode is not null &&
                generatedSetterName is not null) {
                sb.AppendLine(setterCode);

                state.AddGeneratedMember(
                    MemberKind.FieldSetter,
                    field,
                    setterMayThrow,
                    generatedSetterName,
                    CodeLanguage.Kotlin
                );
            }
        }

        return sb.ToString();
    }
}
