using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.SourceCode;
using Beyond.NET.CodeGenerator.Syntax.C;

namespace Beyond.NET.CodeGenerator.Generator.C;

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
        CSyntaxWriterConfiguration? syntaxWriterConfiguration = null;
        
        SourceCodeSection headerSection = writer.AddSection("Header");
        SourceCodeSection commonTypesSection = writer.AddSection("Common Types");
        SourceCodeSection unsupportedTypesSection = writer.AddSection("Unsupported Types");
        SourceCodeSection typedefsSection = writer.AddSection("Type Definitions");
        SourceCodeSection apisSection = writer.AddSection("APIs");
        SourceCodeSection utilsSection = writer.AddSection("Utils");
        SourceCodeSection footerSection = writer.AddSection("Footer");
        
        string header = CSharedCode.HeaderCode;
        headerSection.Code.AppendLine(header);

        string commonTypes = CSharedCode.CommonTypesCode;
        commonTypesSection.Code.AppendLine(commonTypes);

        if (Settings.EmitUnsupported) {
            foreach (var kvp in unsupportedTypes) {
                Type type = kvp.Key;
                string reason = kvp.Value;
    
                string typeName = type.FullName ?? type.Name;

                unsupportedTypesSection.Code.AppendLine(
                    Builder.SingleLineComment($"Unsupported Type \"{typeName}\": {reason}").ToString()
                );
            }
        } else {
            unsupportedTypesSection.Code.AppendLine(
                Builder.SingleLineComment("Omitted due to settings").ToString()
            );
        }

        CTypeSyntaxWriter typeSyntaxWriter = new(Settings);

        Result result = new();

        var orderedTypes = types
            .OrderByDescending(t => t.IsEnum)
            .ThenByDescending(t => !t.IsDelegate());
        
        foreach (Type type in orderedTypes) {
            Syntax.State state = new(CSharpUnmanagedResult);
            
            string typeCode = typeSyntaxWriter.Write(type, state, syntaxWriterConfiguration);
            typedefsSection.Code.AppendLine(typeCode);

            string membersCode = typeSyntaxWriter.WriteMembers(type, state, syntaxWriterConfiguration);
            apisSection.Code.AppendLine(membersCode);
            
            result.AddGeneratedType(
                type,
                state.GeneratedMembers
            );
        }
        
        string utilsCode = CSharedCode.UtilsCode;
        utilsSection.Code.AppendLine(utilsCode);

        string footerCode = CSharedCode.FooterCode;
        footerSection.Code.AppendLine(footerCode);

        return result;
    }
}