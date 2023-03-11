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

    public void Generate(SourceCodeWriter writer)
    {
        const string sectionName = "APIs";
        
        TypeDescriptorRegistry typeDescriptorRegistry = new();
        
        string? fullTypeName = m_type.FullName;

        if (fullTypeName == null) {
            writer.Write($"// Type \"{m_type.Name}\" was skipped. Reason: It has no full name", sectionName);
            
            return;
        }
        
        string cTypeName = fullTypeName.Replace(".", "_");

        writer.Write($"internal static unsafe class {cTypeName}", sectionName);
        writer.Write("{", sectionName);

        var memberCollector = new MemberCollector(m_type);
        var members = memberCollector.Collect(out Dictionary<MemberInfo, string> unsupportedMembers);
        
        foreach (var kvp in unsupportedMembers) {
            MemberInfo member = kvp.Key;
            string reason = kvp.Value;

            string memberName = member.Name;

            writer.Write($"// Unsupported Member \"{memberName}\": {reason}", sectionName);
            writer.Write("", sectionName);
        }

        foreach (var member in members) {
            var memberType = member.MemberType;

            switch (memberType) {
                case MemberTypes.Constructor:
                    StringBuilder ctorCode = new();
                    
                    string constructorNameC = "Create";

                    ctorCode.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{constructorNameC}\")]");
                    ctorCode.AppendLine($"\tinternal static void* {constructorNameC}()");
                    ctorCode.AppendLine("\t{");
                    ctorCode.AppendLine($"\t\t{m_type.FullName} instance = new();");
                    ctorCode.AppendLine("\t\treturn instance.AllocateGCHandleAndGetAddress();");
                    ctorCode.AppendLine("\t}");
                    
                    writer.Write(ctorCode.ToString(), sectionName);

                    break;
                case MemberTypes.Method:
                    StringBuilder methodCode = new();
                    
                    MethodInfo methodInfo = (MethodInfo)member;
                    
                    string methodNameC = methodInfo.Name;
                    
                    Type returnType = methodInfo.ReturnType;
                    TypeDescriptor? typeDescriptor = typeDescriptorRegistry.GetTypeDescriptor(returnType);

                    methodCode.AppendLine($"\t// TODO (Method): {methodNameC}");

                    if (typeDescriptor != null) {
                        methodCode.AppendLine($"\t// Unmanaged C# Return Type: {typeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged)}");
                    }
                    
                    writer.Write(methodCode.ToString(), sectionName);

                    break;
                case MemberTypes.Property:
                    StringBuilder propertyCode = new();
                    
                    PropertyInfo propertyInfo = (PropertyInfo)member;
                    string propertyNameC = propertyInfo.Name;

                    propertyCode.AppendLine($"\t// TODO (Property): {propertyNameC}");
                    
                    writer.Write(propertyCode.ToString(), sectionName);
                    
                    break;
                case MemberTypes.Field:
                    StringBuilder fieldCode = new();
                    
                    FieldInfo fieldInfo = (FieldInfo)member;
                    string fieldNameC = fieldInfo.Name;
                    
                    fieldCode.AppendLine($"\t// TODO (Field): {fieldNameC}");
                    
                    writer.Write(fieldCode.ToString(), sectionName);
                    
                    break;
                case MemberTypes.Event:
                    StringBuilder eventCode = new();
                    
                    EventInfo eventInfo = (EventInfo)member;
                    
                    string eventNameC = eventInfo.Name;
                    
                    eventCode.AppendLine($"\t// TODO (Event): {eventNameC}");
                    
                    writer.Write(eventCode.ToString(), sectionName);
                    
                    break;
                default:
                    writer.Write($"\t// TODO: Unsupported Member Type \"{memberType}\"", sectionName);
                    
                    break;
            }
        }
        
        writer.Write("}", sectionName);
    }
}