using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedTypeSyntaxWriter: ICSharpUnmanagedSyntaxWriter, ITypeSyntaxWriter
{
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, ICSharpUnmanagedSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new CSharpUnmanagedConstructorSyntaxWriter() },
        { MemberTypes.Property, new CSharpUnmanagedPropertySyntaxWriter() },
        { MemberTypes.Method, new CSharpUnmanagedMethodSyntaxWriter() },
        { MemberTypes.Field, new CSharpUnmanagedFieldSyntaxWriter() },
        { MemberTypes.Event, new CSharpUnmanagedEventSyntaxWriter() }
    };

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
        
        // TODO: Move out
        string cTypeName = fullTypeName.Replace(".", "_");
        
        StringBuilder sb = new();
        
        sb.AppendLine($"internal static unsafe class {cTypeName}");
        sb.AppendLine("{");

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

            ICSharpUnmanagedSyntaxWriter? syntaxWriter = GetSyntaxWriter(memberType);

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
        
        sb.AppendLine("}");
        sb.AppendLine();

        return sb.ToString();
    }

    private ICSharpUnmanagedSyntaxWriter? GetSyntaxWriter(MemberTypes memberType)
    {
        m_syntaxWriters.TryGetValue(
            memberType,
            out ICSharpUnmanagedSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
}