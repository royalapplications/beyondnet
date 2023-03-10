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

    public string Generate()
    {
        TypeDescriptorRegistry typeDescriptorRegistry = new();
        
        StringBuilder sb = new();

        bool shouldSkip = false;
        string? skipReason = null;

        if (m_type.IsGenericType) {
            shouldSkip = true;
            skipReason = "Is Generic";
        } else if (m_type.IsArray) {
            shouldSkip = true;
            skipReason = "Is Array";
        }

        if (shouldSkip) {
            sb.AppendLine($"// Type \"{m_type.Name}\" was skipped. Reason: {skipReason ?? "N/A"}");
            
            return sb.ToString();
        }

        string generatedNamespace = $"{m_type.Namespace}.NativeBindings";
        string? fullTypeName = m_type.FullName;

        if (fullTypeName == null) {
            sb.AppendLine($"// Type \"{m_type.Name}\" was skipped. Reason: It has no full name");
            
            return sb.ToString();
        }
        
        string cTypeName = fullTypeName.Replace(".", "_");

        sb.AppendLine($"""
using System;
using System.Runtime.InteropServices;
using NativeAOT.Core;

namespace {generatedNamespace};

""");

        sb.AppendLine($"internal static unsafe class {cTypeName}");
        sb.AppendLine("{");
        
        BindingFlags getMembersFlags = BindingFlags.Public | 
                                       BindingFlags.DeclaredOnly |
                                       BindingFlags.Instance |
                                       BindingFlags.Static;

        foreach (var memberInfo in m_type.GetMembers(getMembersFlags)) {
            var memberType = memberInfo.MemberType;

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
                    MethodInfo methodInfo = (MethodInfo)memberInfo;
                    
                    string methodNameC = methodInfo.Name;
                    
                    Type returnType = methodInfo.ReturnType;
                    TypeDescriptor? typeDescriptor = typeDescriptorRegistry.GetTypeDescriptor(returnType);
                    
                    sb.AppendLine($"\t// TODO (Method): {methodNameC}");

                    if (typeDescriptor != null) {
                        sb.AppendLine($"\t// Unmanaged C# Return Type: {typeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged)}");
                    }

                    break;
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
                    
                    string propertyNameC = propertyInfo.Name;
                    
                    sb.AppendLine($"\t// TODO (Property): {propertyNameC}");
                    
                    break;
                case MemberTypes.Field:
                    FieldInfo fieldInfo = (FieldInfo)memberInfo;
                    
                    string fieldNameC = fieldInfo.Name;
                    
                    sb.AppendLine($"\t// TODO (Field): {fieldNameC}");
                    
                    break;
                case MemberTypes.Event:
                    EventInfo eventInfo = (EventInfo)memberInfo;
                    
                    string eventNameC = eventInfo.Name;
                    
                    sb.AppendLine($"\t// TODO (Event): {eventNameC}");
                    
                    break;
                default:
                    sb.AppendLine($"\t// TODO: Unsupported Member Type \"{memberType}\"");
                    
                    break;
            }
        }
        
        sb.AppendLine("}");
        
        return sb.ToString();
    }
}