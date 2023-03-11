using System.Reflection;
using System.Text;
using NativeAOT.CodeGenerator.Collectors;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedTypeSyntaxWriter: ICSharpUnmanagedSyntaxWriter, ITypeSyntaxWriter
{
    private readonly Dictionary<MemberTypes, ICSharpUnmanagedSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new CSharpUnmanagedConstructorSyntaxWriter() },
        { MemberTypes.Method, new CSharpUnmanagedMethodSyntaxWriter() },
        { MemberTypes.Property, new CSharpUnmanagedPropertySyntaxWriter() },
        { MemberTypes.Field, new CSharpUnmanagedFieldSyntaxWriter() },
        { MemberTypes.Event, new CSharpUnmanagedEventSyntaxWriter() }
    };
    
    public string Write(object @object)
    {
        return Write((Type)@object);
    }
    
    public string Write(Type type)
    {
        StringBuilder sb = new();
        
        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            sb.AppendLine($"// Type \"{type.Name}\" was skipped. Reason: It has no full name");
            
            return sb.ToString();
        }
        
        string cTypeName = fullTypeName.Replace(".", "_");

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

            string memberCode = syntaxWriter.Write(member);
            StringBuilder indentedMemberCode = new();

            foreach (var line in memberCode.Split(Environment.NewLine)) {
                string indentedLine = "\t" + line;

                indentedMemberCode.AppendLine(indentedLine);
            }

            sb.AppendLine(indentedMemberCode.ToString());
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