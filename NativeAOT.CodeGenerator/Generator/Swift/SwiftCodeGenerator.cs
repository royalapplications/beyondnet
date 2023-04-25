using System.Reflection;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.SourceCode;
using NativeAOT.CodeGenerator.Syntax;
using NativeAOT.CodeGenerator.Syntax.Swift;

namespace NativeAOT.CodeGenerator.Generator.Swift;

public class SwiftCodeGenerator: ICodeGenerator
{
    public Settings Settings { get; }
    public Result CSharpUnmanagedResult { get; }
    public Result CResult { get; }
    
    public SwiftCodeGenerator(
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
        SwiftSyntaxWriterConfiguration syntaxWriterConfiguration = new();
        
        SourceCodeSection headerSection = writer.AddSection("Header");
        SourceCodeSection utilsSection = writer.AddSection("Utils");
        SourceCodeSection commonTypesSection = writer.AddSection("Common Types");
        SourceCodeSection unsupportedTypesSection = writer.AddSection("Unsupported Types");
        SourceCodeSection apisSection = writer.AddSection("APIs");
        SourceCodeSection extensionsSection = writer.AddSection("API Extensions");
        SourceCodeSection footerSection = writer.AddSection("Footer");
        
        string header = GetHeaderCode();
        headerSection.Code.AppendLine(header);
        
        string utilsCode = GetUtilsCode();
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

        SwiftTypeSyntaxWriter typeSyntaxWriter = new(Settings);

        Result result = new();

        var orderedTypes = types
            .OrderByDescending(t => t.IsEnum)
            .ThenByDescending(t => !t.IsDelegate());

        Dictionary<Type, List<GeneratedMember>> typeExtensionMembers = new();

        foreach (Type type in orderedTypes) {
            // bool isInterface = type.IsInterface;
            // syntaxWriterConfiguration.OnlyWriteSignatureForProtocol = isInterface;
            
            Syntax.State state = new(CSharpUnmanagedResult, CResult);
            
            string typeCode = typeSyntaxWriter.Write(
                type,
                state,
                syntaxWriterConfiguration
            );
            
            apisSection.Code.AppendLine(typeCode);

            result.AddGeneratedType(
                type,
                state.GeneratedMembers
            );
            
            /* if (isInterface) {
                syntaxWriterConfiguration.OnlyWriteSignatureForProtocol = false;
                
                string typeImplCode = typeSyntaxWriter.Write(
                    type,
                    state,
                    syntaxWriterConfiguration
                );
            
                apisSection.Code.AppendLine(typeImplCode);
            } */

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

        foreach (var kvp in typeExtensionMembers) {
            Type extendedType = kvp.Key;
            List<GeneratedMember> members = kvp.Value;

            string code = typeSyntaxWriter.WriteTypeExtensionMethods(
                extendedType,
                members
            );

            extensionsSection.Code.AppendLine(code);
        }

        string footerCode = GetFooterCode();
        footerSection.Code.AppendLine(footerCode);

        return result;
    }

    private string GetHeaderCode()
    {
        return """
import Foundation
""";
    }
    
    private string GetUtilsCode()
    {
        return SwiftSharedCode.SharedCode;
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