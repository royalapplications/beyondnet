using System.Reflection;
using System.Text;
using NativeAOT.Core;

namespace NativeAOT.CodeGenerator;

public class ManagedCodeGenerator
{
    private readonly Type m_type;

    public ManagedCodeGenerator(Type type)
    {
        m_type = type;
    }

    public void Generate(StringBuilder sb)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = new();
        
        string? fullTypeName = m_type.FullName;

        if (fullTypeName == null) {
            sb.AppendLine($"// Type \"{m_type.Name}\" was skipped. Reason: It has no full name");
            
            return;
        }
        
        string cTypeName = fullTypeName.Replace(".", "_");

        sb.AppendLine($"internal static unsafe class {cTypeName}");
        sb.AppendLine("{");

        var memberCollector = new MemberCollector(m_type);
        var members = memberCollector.Collect(out Dictionary<MemberInfo, string> unsupportedMembers);
        
        foreach (var kvp in unsupportedMembers) {
            MemberInfo member = kvp.Key;
            string reason = kvp.Value;

            string memberName = member.Name;

            sb.AppendLine($"// Unsupported Member \"{memberName}\": {reason}");
            sb.AppendLine();
        }

        foreach (var member in members) {
            var memberType = member.MemberType;

            switch (memberType) {
                case MemberTypes.Constructor:
                    string constructorNameC = "Create";
                    
                    sb.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{constructorNameC}\")]");
                    sb.AppendLine($"\tinternal static void* {constructorNameC}()");
                    sb.AppendLine("\t{");
                    sb.AppendLine($"\t\t{m_type.FullName} instance = new();");
                    sb.AppendLine("\t\treturn instance.AllocateGCHandleAndGetAddress();");
                    sb.AppendLine("\t}");
                    
                    break;
                case MemberTypes.Method:
                    MethodInfo methodInfo = (MethodInfo)member;
                    
                    string methodNameC = methodInfo.Name;
                    
                    Type returnType = methodInfo.ReturnType;
                    TypeDescriptor? typeDescriptor = typeDescriptorRegistry.GetTypeDescriptor(returnType);
                    
                    sb.AppendLine($"\t// TODO (Method): {methodNameC}");

                    if (typeDescriptor != null) {
                        sb.AppendLine($"\t// Unmanaged C# Return Type: {typeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged)}");
                    }

                    break;
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)member;
                    
                    string propertyNameC = propertyInfo.Name;
                    
                    sb.AppendLine($"\t// TODO (Property): {propertyNameC}");
                    
                    break;
                case MemberTypes.Field:
                    FieldInfo fieldInfo = (FieldInfo)member;
                    
                    string fieldNameC = fieldInfo.Name;
                    
                    sb.AppendLine($"\t// TODO (Field): {fieldNameC}");
                    
                    break;
                case MemberTypes.Event:
                    EventInfo eventInfo = (EventInfo)member;
                    
                    string eventNameC = eventInfo.Name;
                    
                    sb.AppendLine($"\t// TODO (Event): {eventNameC}");
                    
                    break;
                default:
                    sb.AppendLine($"\t// TODO: Unsupported Member Type \"{memberType}\"");
                    
                    break;
            }
        }
        
        sb.AppendLine("}");
    }
}