using NativeAOT.CodeGenerator.SourceCode;
using NativeAOT.CodeGenerator.Syntax.C;

namespace NativeAOT.CodeGenerator.Generator.C;

public class CCodeGenerator: ICodeGenerator
{
    public Settings Settings { get; }
    
    public CCodeGenerator(Settings settings)
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

        CTypeSyntaxWriter typeSyntaxWriter = new(Settings);

        Result result = new();
        
        foreach (Type type in types) {
            Syntax.State state = new();
            
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
// TODO (Header Code)
""";
    }

    private string GetSharedCode()
    {
        return $"""
// TODO (Shared Code)
""";
    }
}