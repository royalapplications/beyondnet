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
        string namespaceForGeneratedCode = $"GeneratedNativeBindings";

        string header = $"""
using System;
using System.Runtime.InteropServices;
using NativeAOT.Core;

namespace {namespaceForGeneratedCode};

""";
        
        writer.Write(header, "Header");

        foreach (var kvp in unsupportedTypes) {
            Type type = kvp.Key;
            string reason = kvp.Value;

            string typeName = type.FullName ?? type.Name;
            
            writer.Write(
                $"// Unsupported Type \"{typeName}\": {reason}\n", 
                "Unsupported Types"
            );
        }

        foreach (var type in types) {
            Generate(type, writer);
        }
    }

    private void Generate(Type type, SourceCodeWriter writer)
    {
        const string sectionName = "APIs";
        
        TypeDescriptorRegistry typeDescriptorRegistry = new();
        
        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            writer.Write($"// Type \"{type.Name}\" was skipped. Reason: It has no full name", sectionName);
            
            return;
        }
        
        string cTypeName = fullTypeName.Replace(".", "_");

        writer.Write($"internal static unsafe class {cTypeName}", sectionName);
        writer.Write("{", sectionName);

        var memberCollector = new MemberCollector(type);
        var members = memberCollector.Collect(out Dictionary<MemberInfo, string> unsupportedMembers);
        
        foreach (var kvp in unsupportedMembers) {
            MemberInfo member = kvp.Key;
            string reason = kvp.Value;

            string memberName = member.Name;

            writer.Write($"// Unsupported Member \"{memberName}\": {reason}", sectionName);
            writer.Write("", sectionName);
        } 
        
        CSharpUnmanagedConstructorSyntaxWriter constructorSyntaxWriter = new();

        foreach (var member in members) {
            var memberType = member.MemberType;

            switch (memberType) {
                case MemberTypes.Constructor:
                    string ctorCode = constructorSyntaxWriter.Write((ConstructorInfo)member);
                    
                    writer.Write(ctorCode, sectionName);

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