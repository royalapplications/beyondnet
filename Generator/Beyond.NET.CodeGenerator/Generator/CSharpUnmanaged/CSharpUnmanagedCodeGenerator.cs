using Beyond.NET.CodeGenerator.SourceCode;
using Beyond.NET.CodeGenerator.Syntax.CSharpUnmanaged;

namespace Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;

public class CSharpUnmanagedCodeGenerator: ICodeGenerator
{
    public Settings Settings { get; }
    
    public CSharpUnmanagedCodeGenerator(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public Result Generate(
        IEnumerable<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        SourceCodeWriter writer
    )
    {
        CSharpUnmanagedSyntaxWriterConfiguration? syntaxWriterConfiguration = new(Settings.TypeCollectorSettings!);
        
        SourceCodeSection headerSection = writer.AddSection("Header");
        SourceCodeSection sharedCodeSection = writer.AddSection("Shared Code");
        SourceCodeSection unsupportedTypesSection = writer.AddSection("Unsupported Types");
        SourceCodeSection apisSection = writer.AddSection("APIs");

        string header = GetHeaderCode();
        headerSection.Code.AppendLine(header);

        string sharedCode = GetSharedCode();
        sharedCodeSection.Code.AppendLine(sharedCode);

        if (Settings.EmitUnsupported) {
            foreach (var kvp in unsupportedTypes) {
                Type type = kvp.Key;
                string reason = kvp.Value;
    
                string typeName = type.FullName ?? type.Name;
    
                unsupportedTypesSection.Code.AppendLine($"// Unsupported Type \"{typeName}\": {reason}");
            }
        } else {
            unsupportedTypesSection.Code.AppendLine("// Omitted due to settings");
        }

        CSharpUnmanagedTypeSyntaxWriter typeSyntaxWriter = new(Settings);

        Result result = new();
        
        foreach (Type type in types) {
            Syntax.State state = new() {
                Settings = Settings
            };
            
            string typeCode = typeSyntaxWriter.Write(type, state, syntaxWriterConfiguration);
            
            apisSection.Code.AppendLine(typeCode);
            
            result.AddGeneratedType(
                type,
                state.GeneratedMembers
            );
        }

        return result;
    }

    private string GetHeaderCode()
    {
        return /*lang=C#*/$"""
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace {Settings.NamespaceForGeneratedCode};

""";
    }

    private string GetSharedCode()
    {
        return CSharpUnmanagedSharedCode.SharedCode;
    }
}