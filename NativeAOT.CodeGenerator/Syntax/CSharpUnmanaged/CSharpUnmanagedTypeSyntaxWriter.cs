using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

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
    
    public string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }
    
    public string Write(Type type, State state)
    {
        if (type.IsPrimitive ||
            type.IsEnum ||
            type.IsPointer ||
            type.IsByRef) {
            // No need to generate C# Unmanaged code for those kinds of types

            return string.Empty;
        }
        
        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            return $"// Type \"{type.Name}\" was skipped. Reason: It has no full name.";
        }
        
        string cTypeName = fullTypeName.CTypeName();

        bool isDelegate = type.IsDelegate();
        
        MethodInfo? delegateInvokeMethod = isDelegate
            ? type.GetDelegateInvokeMethod()
            : null;
        
        StringBuilder sb = new();
        
        sb.AppendLine($"internal unsafe class {cTypeName}");
        sb.AppendLine("{");

        if (type.IsDelegate()) {
            WriteDelegateType(
                type,
                fullTypeName,
                cTypeName,
                delegateInvokeMethod!,
                sb,
                state
            );
        } else {
            WriteRegularType(
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
        Type type,
        StringBuilder sb,
        State state
    )
    {
        var memberCollector = new MemberCollector(type);
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

            string memberCode = syntaxWriter.Write(member, state)
                .IndentAllLines(1);

            sb.AppendLine(memberCode);
        }
        
        WriteTypeOf(
            type,
            sb,
            state
        );
        
        WriteDestructor(
            type,
            sb,
            state
        );
    }

    private void WriteTypeOf(
        Type type,
        StringBuilder sb,
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

        string code = syntaxWriter.Write(type, state)
            .IndentAllLines(1);

        sb.AppendLine(code);
    }
    
    private void WriteDestructor(
        Type type,
        StringBuilder sb,
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

        string code = syntaxWriter.Write(type, state)
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