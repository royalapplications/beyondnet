using NativeAOT.CodeGenerator.SourceCode;
using NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

namespace NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;

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
            
            string typeCode = typeSyntaxWriter.Write(type, state);
            
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
        return $"""
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