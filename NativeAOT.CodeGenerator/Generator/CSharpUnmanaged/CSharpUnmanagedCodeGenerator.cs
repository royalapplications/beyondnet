using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.SourceCode;
using NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

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
        CSharpUnmanagedMethodSyntaxWriter methodSyntaxWriter = new();
        CSharpUnmanagedPropertySyntaxWriter propertySyntaxWriter = new();
        CSharpUnmanagedFieldSyntaxWriter fieldSyntaxWriter = new();
        CSharpUnmanagedEventSyntaxWriter eventSyntaxWriter = new();

        foreach (var member in members) {
            var memberType = member.MemberType;

            switch (memberType) {
                case MemberTypes.Constructor:
                    string ctorCode = constructorSyntaxWriter.Write((ConstructorInfo)member);
                    
                    sb.AppendLine(ctorCode);

                    break;
                case MemberTypes.Method:
                    string methodCode = methodSyntaxWriter.Write((MethodInfo)member);
                    
                    sb.AppendLine(methodCode);

                    break;
                case MemberTypes.Property:
                    string propertyCode = propertySyntaxWriter.Write((PropertyInfo)member);
                    
                    sb.AppendLine(propertyCode);
                    
                    break;
                case MemberTypes.Field:
                    string fieldCode = fieldSyntaxWriter.Write((FieldInfo)member);
                    
                    sb.AppendLine(fieldCode);
                    
                    break;
                case MemberTypes.Event:
                    string eventCode = eventSyntaxWriter.Write((EventInfo)member);
                    
                    sb.AppendLine(eventCode);
                    
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