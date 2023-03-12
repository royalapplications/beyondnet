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
    
    public void Generate(
        IEnumerable<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        SourceCodeWriter writer
    )
    {
        SourceCodeSection headerSection = writer.AddSection("Header");
        SourceCodeSection unsupportedTypesSection = writer.AddSection("Unsupported Types");
        SourceCodeSection apisSection = writer.AddSection("APIs");

        string header = $"""
using System;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace {Settings.NamespaceForGeneratedCode};

""";

        headerSection.Code.AppendLine(header);

        if (Settings.EmitUnsupported) {
            foreach (var kvp in unsupportedTypes) {
                Type type = kvp.Key;
                string reason = kvp.Value;
    
                string typeName = type.FullName ?? type.Name;
    
                unsupportedTypesSection.Code.AppendLine($"// Unsupported Type \"{typeName}\": {reason}");
            }
        }

        CSharpUnmanagedTypeSyntaxWriter typeSyntaxWriter = new(Settings);
        
        foreach (var type in types) {
            string typeCode = typeSyntaxWriter.Write(type);
            
            apisSection.Code.AppendLine(typeCode);
        }
    }
}