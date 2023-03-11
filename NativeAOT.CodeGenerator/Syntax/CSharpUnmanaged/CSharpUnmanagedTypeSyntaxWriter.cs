using System.Reflection;
using System.Text;
using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.Extensions;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedTypeSyntaxWriter: ICSharpUnmanagedSyntaxWriter, ITypeSyntaxWriter
{
    private readonly Dictionary<MemberTypes, ICSharpUnmanagedSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new CSharpUnmanagedConstructorSyntaxWriter() },
        { MemberTypes.Property, new CSharpUnmanagedPropertySyntaxWriter() },
        { MemberTypes.Method, new CSharpUnmanagedMethodSyntaxWriter() },
        { MemberTypes.Field, new CSharpUnmanagedFieldSyntaxWriter() },
        { MemberTypes.Event, new CSharpUnmanagedEventSyntaxWriter() }
    };
    
    public string Write(object @object)
    {
        return Write((Type)@object);
    }
    
    public string Write(Type type)
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
        
        foreach (var kvp in unsupportedMembers) {
            MemberInfo member = kvp.Key;
            string reason = kvp.Value;

            string memberName = member.Name;

            sb.AppendLine($"\t// Unsupported Member \"{memberName}\": {reason}");
            sb.AppendLine();
        }
        
        foreach (var member in members) {
            var memberType = member.MemberType;

            ICSharpUnmanagedSyntaxWriter? syntaxWriter = GetSyntaxWriter(memberType);

            if (syntaxWriter == null) {
                sb.AppendLine($"\t// TODO: Unsupported Member Type \"{memberType}\"");
                    
                continue;
            }

            string memberCode = syntaxWriter.Write(member)
                .IndentAllLines(1);

            sb.AppendLine(memberCode);
        }
        
        sb.AppendLine("}");
        sb.AppendLine();

        return sb.ToString();
    }

    private ICSharpUnmanagedSyntaxWriter? GetSyntaxWriter(MemberTypes memberType)
    {
        m_syntaxWriters.TryGetValue(memberType, out ICSharpUnmanagedSyntaxWriter? syntaxWriter);

        return syntaxWriter;
    }
}