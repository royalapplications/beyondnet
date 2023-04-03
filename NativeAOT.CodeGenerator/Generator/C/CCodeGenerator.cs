using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.SourceCode;
using NativeAOT.CodeGenerator.Syntax.C;

namespace NativeAOT.CodeGenerator.Generator.C;

public class CCodeGenerator: ICodeGenerator
{
    public Settings Settings { get; }
    public Result CSharpUnmanagedResult { get; }
    
    public CCodeGenerator(Settings settings, Result cSharpUnmanagedResult)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        CSharpUnmanagedResult = cSharpUnmanagedResult ?? throw new ArgumentNullException(nameof(cSharpUnmanagedResult));
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
        SourceCodeSection typedefsSection = writer.AddSection("Type Definitions");
        SourceCodeSection apisSection = writer.AddSection("APIs");
        SourceCodeSection footerSection = writer.AddSection("Footer");
        
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

        var orderedTypes = types
            .OrderByDescending(t => t.IsEnum)
            .ThenByDescending(t => !t.IsDelegate());
        
        foreach (Type type in orderedTypes) {
            Syntax.State state = new(CSharpUnmanagedResult);
            
            string typeCode = typeSyntaxWriter.Write(type, state);
            typedefsSection.Code.AppendLine(typeCode);

            string membersCode = typeSyntaxWriter.WriteMembers(type, state);
            apisSection.Code.AppendLine(membersCode);
            
            result.AddGeneratedType(
                type,
                state.GeneratedMembers
            );
        }

        string footerCode = GetFooterCode();
        footerSection.Code.AppendLine(footerCode);

        return result;
    }
    
    private string GetHeaderCode()
    {
        return """
#ifndef TypeDefinitions_h
#define TypeDefinitions_h

#import <stdlib.h>
#import <stdbool.h>
""";
    }

    private string GetSharedCode()
    {
        return """
#pragma mark - BEGIN Common Types
typedef const char* CString;

#pragma mark - END Common Types
""";
    }

    private string GetFooterCode()
    {
        return """
#endif /* TypeDefinitions_h */
""";        
    }
}