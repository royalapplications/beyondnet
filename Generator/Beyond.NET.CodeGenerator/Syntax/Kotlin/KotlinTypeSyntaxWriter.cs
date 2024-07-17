using System.Reflection;
using System.Text;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

using Settings = Beyond.NET.CodeGenerator.Generator.Kotlin.Settings;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public partial class KotlinTypeSyntaxWriter: IKotlinSyntaxWriter, ITypeSyntaxWriter
{
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, IKotlinSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new KotlinConstructorSyntaxWriter() },
        { MemberTypes.Property, new KotlinPropertySyntaxWriter() },
        { MemberTypes.Method, new KotlinMethodSyntaxWriter() },
        { MemberTypes.Field, new KotlinFieldSyntaxWriter() },
        { MemberTypes.Event, new KotlinEventSyntaxWriter() }
    };
    
    private KotlinDestructorSyntaxWriter m_destructorSyntaxWriter = new();
    private KotlinTypeOfSyntaxWriter m_typeOfSyntaxWriter = new();
    
    public KotlinTypeSyntaxWriter(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((Type)@object, state, configuration);
    }

    public string Write(
        Type type,
        State state,
        ISyntaxWriterConfiguration? configuration
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        var kotlinConfiguration = (configuration as KotlinSyntaxWriterConfiguration)!; 
        var generationPhase = kotlinConfiguration.GenerationPhase;
        
        if (state.CSharpUnmanagedResult is null) {
            throw new Exception("No CSharpUnmanagedResult provided");
        }

        if (state.CResult is null) {
            throw new Exception("No CResult provided");
        }

        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            return Builder.SingleLineComment($"Type \"{type.Name}\" was skipped. Reason: It has no full name.").ToString();
        }

        // TODO
        if (type.IsGenericInAnyWay(true))
        {
            return Builder.SingleLineComment($"Type \"{type.Name}\" was skipped. Reason: It is generic somehow.").ToString();
        }
        
        StringBuilder sb = new();

        if (type.IsEnum &&
            generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.KotlinBindings)
        {
            string enumTypeCode = WriteEnumType(
                type,
                typeDescriptorRegistry
            );

            sb.AppendLine(enumTypeCode);
        }
        else if (generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.JNA)
        {
            var typeCode = WriteJNAType(
                type,
                state,
                kotlinConfiguration
            );

            sb.AppendLine(typeCode);
        }
        
        return sb.ToString();
    }
    
    private string WriteEnumType(
        Type type,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
        
        // string cEnumTypeName = typeDescriptor.GetTypeName(CodeLanguage.C, false);
        string kotlinEnumTypeName = typeDescriptor.GetTypeName(CodeLanguage.Kotlin, false);

        Type underlyingType = type.GetEnumUnderlyingType();
        TypeDescriptor underlyingTypeDescriptor = underlyingType.GetTypeDescriptor(typeDescriptorRegistry);

        string underlyingTypeName = underlyingTypeDescriptor.GetTypeName(CodeLanguage.KotlinJNA, false);
        
        bool isFlagsEnum = type.IsDefined(typeof(FlagsAttribute), false);
        
        StringBuilder sb = new();

        sb.AppendLine($"public enum class {kotlinEnumTypeName}(val rawValue: {underlyingTypeName}) {{");
        
        var caseNames = type.GetEnumNames();
        var values = type.GetEnumValuesAsUnderlyingType() ?? throw new Exception("No enum values");

        if (caseNames.Length != values.Length) {
            throw new Exception("The number of case names in an enum must match the number of values");
        }

        List<string> enumCases = new();

        for (int i = 0; i < caseNames.Length; i++) {
            string caseName = caseNames[i];
            var value = values.GetValue(i) ?? throw new Exception("No enum value for case");
            var valueType = value.GetType();

            if (valueType == typeof(int) ||
                valueType == typeof(sbyte) ||
                valueType == typeof(byte) ||
                valueType == typeof(ushort) ||
                valueType == typeof(uint) ||
                valueType == typeof(ulong))
                enumCases.Add($"\t{caseName}({value})");
            // else if (valueType == typeof(byte) ||
            //          valueType == typeof(ushort) ||
            //          valueType == typeof(uint) ||
            //          valueType == typeof(ulong))
            //     enumCases.Add($"\t{caseName}({value}u)");
            else if (valueType == typeof(double))
                enumCases.Add($"\t{caseName}({value}.toDouble())");
            else if (valueType == typeof(float))
                enumCases.Add($"\t{caseName}({value}.toFloat())");
            else
                throw new Exception($"Unknown underlying enum type: {underlyingType}");
        }

        string enumCasesString = string.Join(",\n", enumCases);

        sb.AppendLine(enumCasesString);

        sb.AppendLine("}");

        return sb.ToString();
    }

    private string WriteJNAType(
        Type type,
        State state,
        KotlinSyntaxWriterConfiguration configuration
    )
    {
        return WriteJNAMembers(
            type,
            state,
            configuration
        );
    }

    private string WriteJNAMembers(
        Type type,
        State state,
        KotlinSyntaxWriterConfiguration configuration
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        if (type.IsPointer ||
            type.IsByRef ||
            type.IsGenericParameter ||
            type.IsGenericMethodParameter ||
            type.IsGenericTypeParameter ||
            type.IsConstructedGenericType) {
            // No need to generate Kotlin code for those kinds of types

            return string.Empty;
        }
        
        var cSharpMembers = cSharpUnmanagedResult.GeneratedTypes[type];
        var cMembers = cResult.GeneratedTypes[type];
        
        HashSet<MemberInfo> generatedMembers = new();
        
        StringBuilder sbMembers = new();

        foreach (var cSharpMember in cSharpMembers) {
            var member = cSharpMember.Member;

            if (member is not null &&
                generatedMembers.Contains(member)) {
                continue;
            }
            
            var memberKind = cSharpMember.MemberKind;
            var memberType = member?.MemberType;

            IKotlinSyntaxWriter? syntaxWriter = GetSyntaxWriter(
                memberKind,
                memberType ?? MemberTypes.Custom
            );
            
            if (syntaxWriter == null) {
                if (Settings.EmitUnsupported) {
                    sbMembers.AppendLine(Builder.SingleLineComment($"TODO: Unsupported Member Type \"{memberType}\"").ToString());
                }
                    
                continue;
            }

            object? target;

            if (syntaxWriter is IDestructorSyntaxWriter) {
                target = type;
            } else if (syntaxWriter is ITypeOfSyntaxWriter) {
                target = type;
            } else {
                target = member;
            }

            if (target == null) {
                throw new Exception("No target");
            }

            // if ((interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol || interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations) &&
            //     (syntaxWriter is IDestructorSyntaxWriter || syntaxWriter is ITypeOfSyntaxWriter)) {
            //     continue;
            // }
            //
            // if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass &&
            //     syntaxWriter is not IDestructorSyntaxWriter &&
            //     syntaxWriter is not ITypeOfSyntaxWriter) {
            //     continue;
            // }

            string memberCode = syntaxWriter.Write(
                target,
                state,
                configuration
            );

            sbMembers.AppendLine(memberCode);

            if (member is not null) {
                generatedMembers.Add(member);
            }
        }

        string membersCode = sbMembers.ToString()
            .IndentAllLines(1);

        return membersCode;
    }

    private IKotlinSyntaxWriter? GetSyntaxWriter(
        MemberKind memberKind,
        MemberTypes memberType
    )
    {
        if (memberKind == MemberKind.Destructor) {
            return m_destructorSyntaxWriter;
        } else if (memberKind == MemberKind.TypeOf) {
            return m_typeOfSyntaxWriter;
        }

        m_syntaxWriters.TryGetValue(
            memberType,
            out IKotlinSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
}