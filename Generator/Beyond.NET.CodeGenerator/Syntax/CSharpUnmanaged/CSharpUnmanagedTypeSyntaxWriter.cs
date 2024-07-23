using System.Reflection;

using Beyond.NET.CodeGenerator.Collectors;
using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.CSharpUnmanaged;

public partial class CSharpUnmanagedTypeSyntaxWriter: ICSharpUnmanagedSyntaxWriter, ITypeSyntaxWriter
{
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, ICSharpUnmanagedSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new CSharpUnmanagedConstructorSyntaxWriter() },
        { MemberTypes.Property, new CSharpUnmanagedPropertySyntaxWriter() },
        { MemberTypes.Method, new CSharpUnmanagedMethodSyntaxWriter() },
        { MemberTypes.Field, new CSharpUnmanagedFieldSyntaxWriter() },
        { MemberTypes.Event, new CSharpUnmanagedEventSyntaxWriter() }
    };

    private CSharpUnmanagedDestructorSyntaxWriter m_destructorSyntaxWriter = new();
    private CSharpUnmanagedTypeOfSyntaxWriter m_typeOfSyntaxWriter = new();

    public CSharpUnmanagedTypeSyntaxWriter(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((Type)@object, state, configuration);
    }
    
    public string Write(Type type, State state, ISyntaxWriterConfiguration? configuration)
    {
        if (type.IsPointer ||
            type.IsByRef ||
            type.IsArray ||
            type.IsConstructedGenericType) {
            // No need to generate C# Unmanaged code for those kinds of types

            return string.Empty;
        }
        
        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            return $"// Type \"{type.Name}\" was skipped. Reason: It has no full name.";
        }
        
        string cTypeName = type.CTypeName();

        bool isDelegate = type.IsDelegate();
        
        MethodInfo? delegateInvokeMethod = isDelegate
            ? type.GetDelegateInvokeMethod()
            : null;
        
        CSharpCodeBuilder sb = new();
        
        sb.AppendLine($"internal unsafe class {cTypeName}");
        sb.AppendLine("{");

        if (type.IsDelegate()) {
            WriteDelegateType(
                configuration,
                type,
                fullTypeName,
                cTypeName,
                delegateInvokeMethod!,
                sb,
                state
            );
        } else if (type.IsPrimitive) {
            WritePrimitiveType(
                configuration,
                type,
                sb,
                state
            );
        } else if (type.IsEnum) {
            WriteEnumType(
                configuration,
                type,
                sb,
                state
            );
        } else {
            WriteRegularType(
                configuration,
                type,
                sb,
                state
            );
        }

        sb.AppendLine("}");
        sb.AppendLine();

        return sb.ToString();
    }

    private void WriteRegularType(
        ISyntaxWriterConfiguration? configuration,
        Type type,
        CSharpCodeBuilder sb,
        State state
    )
    {
        TypeCollectorSettings typeCollectorSettings = (configuration as CSharpUnmanagedSyntaxWriterConfiguration)!.TypeCollectorSettings;

        var typeCollector = new TypeCollector(
            null,
            typeCollectorSettings
        );
        
        var memberCollector = new MemberCollector(type, typeCollector);
        var members = memberCollector.Collect(out Dictionary<MemberInfo, string> unsupportedMembers);

        if (Settings.EmitUnsupported) {
            foreach (var kvp in unsupportedMembers) {
                MemberInfo member = kvp.Key;
                string reason = kvp.Value;
    
                string memberName = member.Name;
    
                sb.AppendLine($"\t// Unsupported Member \"{memberName}\": {reason}");
                sb.AppendLine();
            }
        }
        
        foreach (var member in members) {
            var memberType = member.MemberType;

            ICSharpUnmanagedSyntaxWriter? syntaxWriter = GetSyntaxWriter(
                MemberKind.Automatic,
                memberType
            );

            if (syntaxWriter == null) {
                if (Settings.EmitUnsupported) {
                    sb.AppendLine($"\t// TODO: Unsupported Member Type \"{memberType}\"");
                }
                    
                continue;
            }

            string memberCode = syntaxWriter.Write(member, state, configuration)
                .IndentAllLines(1);

            sb.AppendLine(memberCode);
        }
        
        WriteTypeOf(
            configuration,
            type,
            sb,
            state
        );
        
        WriteDestructor(
            configuration,
            type,
            sb,
            state
        );
    }

    private void WriteEnumType(
        ISyntaxWriterConfiguration? configuration,
        Type type,
        CSharpCodeBuilder sb,
        State state
    )
    {
        WriteTypeOf(
            configuration,
            type,
            sb,
            state
        );
    }

    private void WritePrimitiveType(
        ISyntaxWriterConfiguration? configuration,
        Type type,
        CSharpCodeBuilder sb,
        State state
    )
    {
        WriteTypeOf(
            configuration,
            type,
            sb,
            state
        );
    }

    private void WriteTypeOf(
        ISyntaxWriterConfiguration? configuration,
        Type type,
        CSharpCodeBuilder sb,
        State state
    )
    {
        ICSharpUnmanagedSyntaxWriter? syntaxWriter = GetSyntaxWriter(
            MemberKind.TypeOf,
            MemberTypes.Custom
        );

        if (syntaxWriter == null) {
            throw new Exception("No typeOf syntax writer");
        }

        string code = syntaxWriter.Write(type, state, configuration)
            .IndentAllLines(1);

        sb.AppendLine(code);
    }
    
    private void WriteDestructor(
        ISyntaxWriterConfiguration? configuration,
        Type type,
        CSharpCodeBuilder sb,
        State state
    )
    {
        ICSharpUnmanagedSyntaxWriter? syntaxWriter = GetSyntaxWriter(
            MemberKind.Destructor,
            MemberTypes.Custom
        );

        if (syntaxWriter == null) {
            throw new Exception("No destructor syntax writer");
        }

        string code = syntaxWriter.Write(type, state, configuration)
            .IndentAllLines(1);

        sb.AppendLine(code);
    }

    private ICSharpUnmanagedSyntaxWriter? GetSyntaxWriter(
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
            out ICSharpUnmanagedSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
}