using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.SourceCode;
using NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

using NativeAOT.Core;

namespace NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;

public class CSharpUnmanagedCodeGenerator: CodeGenerator
{
    public void Generate(IEnumerable<Type> types, Dictionary<Type, string> unsupportedTypes, SourceCodeWriter writer)
    {
        SourceCodeSection headerSection = writer.AddSection("Header");
        SourceCodeSection unsupportedTypesSection = writer.AddSection("Unsupported Types");
        SourceCodeSection apisSection = writer.AddSection("APIs");
        
        string namespaceForGeneratedCode = "GeneratedNativeBindings";

        string header = $"""
using System;
using System.Runtime.InteropServices;
using NativeAOT.Core;

namespace {namespaceForGeneratedCode};

""";

        headerSection.Code.AppendLine(header);

        foreach (var kvp in unsupportedTypes) {
            Type type = kvp.Key;
            string reason = kvp.Value;

            string typeName = type.FullName ?? type.Name;

            unsupportedTypesSection.Code.AppendLine($"// Unsupported Type \"{typeName}\": {reason}");
        }
        
        foreach (var type in types) {
            Generate(type, apisSection.Code);
        }
    }

    private void Generate(Type type, StringBuilder sb)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = new();
        
        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            sb.AppendLine($"// Type \"{type.Name}\" was skipped. Reason: It has no full name");
            
            return;
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
        
        CSharpUnmanagedConstructorSyntaxWriter constructorSyntaxWriter = new();

        foreach (var member in members) {
            var memberType = member.MemberType;

            switch (memberType) {
                case MemberTypes.Constructor:
                    string ctorCode = constructorSyntaxWriter.Write((ConstructorInfo)member);
                    
                    sb.AppendLine(ctorCode);

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
                    
                    sb.AppendLine(methodCode.ToString());

                    break;
                case MemberTypes.Property:
                    StringBuilder propertyCode = new();
                    
                    PropertyInfo propertyInfo = (PropertyInfo)member;
                    string propertyNameC = propertyInfo.Name;

                    propertyCode.AppendLine($"\t// TODO (Property): {propertyNameC}");
                    
                    sb.AppendLine(propertyCode.ToString());
                    
                    break;
                case MemberTypes.Field:
                    StringBuilder fieldCode = new();
                    
                    FieldInfo fieldInfo = (FieldInfo)member;
                    string fieldNameC = fieldInfo.Name;
                    
                    fieldCode.AppendLine($"\t// TODO (Field): {fieldNameC}");
                    
                    sb.AppendLine(fieldCode.ToString());
                    
                    break;
                case MemberTypes.Event:
                    StringBuilder eventCode = new();
                    
                    EventInfo eventInfo = (EventInfo)member;
                    
                    string eventNameC = eventInfo.Name;
                    
                    eventCode.AppendLine($"\t// TODO (Event): {eventNameC}");
                    
                    sb.AppendLine(eventCode.ToString());
                    
                    break;
                default:
                    sb.AppendLine($"\t// TODO: Unsupported Member Type \"{memberType}\"");
                    
                    break;
            }
        }
        
        sb.AppendLine("}");
        sb.AppendLine();
    }
}