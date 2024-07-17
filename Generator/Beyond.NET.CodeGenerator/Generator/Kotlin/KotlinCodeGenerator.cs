using System.Reflection;
using System.Text;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.SourceCode;
using Beyond.NET.CodeGenerator.Syntax;
using Beyond.NET.CodeGenerator.Syntax.Kotlin;
// using Builder = Beyond.NET.CodeGenerator.Syntax.Kotlin.Builder;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Generator.Kotlin;

public class KotlinCodeGenerator: ICodeGenerator
{
    public Settings Settings { get; }
    public Result CSharpUnmanagedResult { get; }
    public Result CResult { get; }
    
    public KotlinCodeGenerator(
        Settings settings,
        Result cSharpUnmanagedResult,
        Result cResult
    )
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        CSharpUnmanagedResult = cSharpUnmanagedResult ?? throw new ArgumentNullException(nameof(cSharpUnmanagedResult));
        CResult = cResult ?? throw new ArgumentNullException(nameof(cResult));
    }

    public Result Generate(
        IEnumerable<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        SourceCodeWriter writer
    )
    {
        KotlinSyntaxWriterConfiguration defaultSyntaxWriterConfiguration = new();
        
        SourceCodeSection headerSection = writer.AddSection("Header");
        SourceCodeSection utilsSection = writer.AddSection("Utils");
        SourceCodeSection commonTypesSection = writer.AddSection("Common Types");
        SourceCodeSection unsupportedTypesSection = writer.AddSection("Unsupported Types");
        SourceCodeSection apisSection = writer.AddSection("APIs");
        SourceCodeSection extensionsSection = writer.AddSection("API Extensions");
        SourceCodeSection namespacesSection = writer.AddSection("Namespaces");
        SourceCodeSection footerSection = writer.AddSection("Footer");
        
        string header = GetHeaderCode();
        headerSection.Code.AppendLine(header);
        
        string utilsCode = GetUtilsCode(types.ToArray());
        utilsSection.Code.AppendLine(utilsCode);

        string commonTypes = GetCommonTypesCode();
        commonTypesSection.Code.AppendLine(commonTypes);

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

        KotlinTypeSyntaxWriter typeSyntaxWriter = new(Settings);

        Result result = new();

        var orderedTypes = types
            .OrderByDescending(t => t.IsEnum)
            .ThenByDescending(t => !t.IsDelegate());

        Dictionary<Type, List<GeneratedMember>> typeExtensionMembers = new();

        foreach (Type type in orderedTypes) {
            bool isInterface = type.IsInterface;
            
            Syntax.State state = new(CSharpUnmanagedResult, CResult);
            
            string typeCode = typeSyntaxWriter.Write(
                type,
                state,
                defaultSyntaxWriterConfiguration
            );
            
            apisSection.Code.AppendLine(typeCode);
            
            if (state.SkippedTypes.Contains(type)) {
                continue;
            }
            
            result.AddGeneratedType(
                type,
                state.GeneratedMembers
            );                

            IEnumerable<GeneratedMember> newExtensionMembers = state.GetGeneratedMembersThatAreExtensions();

            foreach (var generatedMember in newExtensionMembers) {
                MethodBase? methodBase = generatedMember.Member as MethodBase;

                if (methodBase is null) {
                    continue;
                }

                var firstParameter = methodBase.GetParameters().FirstOrDefault();

                if (firstParameter is null) {
                    continue;
                }

                Type extendedType = firstParameter.ParameterType;
                
                if (!typeExtensionMembers.TryGetValue(extendedType, out List<GeneratedMember>? extensionMembers)) {
                    extensionMembers = new();
                }
            
                extensionMembers.Add(generatedMember);

                if (extensionMembers.Count > 0) {
                    typeExtensionMembers[extendedType] = extensionMembers;
                } else if (typeExtensionMembers.ContainsKey(extendedType)) {
                    typeExtensionMembers.Remove(extendedType);
                }
            }
        }

        // TODO
        // foreach (var kvp in typeExtensionMembers) {
        //     Type extendedType = kvp.Key;
        //     List<GeneratedMember> members = kvp.Value;
        //
        //     string code = typeSyntaxWriter.WriteTypeExtensionMethods(
        //         extendedType,
        //         members
        //     );
        //
        //     extensionsSection.Code.AppendLine(code);
        // }

        string footerCode = GetFooterCode();
        footerSection.Code.AppendLine(footerCode);

        return result;
    }

    private string GetHeaderCode()
    {
        return """
import com.sun.jna.*
import com.sun.jna.ptr.*
""";
    }
    
    private string GetUtilsCode(Type[] types)
    {
        StringBuilder sb = new();
        
        sb.AppendLine(KotlinSharedCode.SharedCode);
        sb.AppendLine();

        string code = sb.ToString();
        
        return code;
    }

    private string GetCommonTypesCode()
    {
        return "";
    }
    
    private string GetFooterCode()
    {
        return """

""";
    }
}