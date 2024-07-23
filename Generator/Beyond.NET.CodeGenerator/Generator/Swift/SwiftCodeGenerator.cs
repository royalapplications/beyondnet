using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.SourceCode;
using Beyond.NET.CodeGenerator.Syntax;
using Beyond.NET.CodeGenerator.Syntax.Swift;
using Builder = Beyond.NET.CodeGenerator.Syntax.Swift.Builder;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Generator.Swift;

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
        SwiftSyntaxWriterConfiguration defaultSyntaxWriterConfiguration = new();
        
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

        SwiftTypeSyntaxWriter typeSyntaxWriter = new(Settings);

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
                isInterface ? new SwiftSyntaxWriterConfiguration {
                    InterfaceGenerationPhase = SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol
                } : defaultSyntaxWriterConfiguration
            );
            
            apisSection.Code.AppendLine(typeCode);
            
            if (isInterface) {
                string protocolExtensionCode = typeSyntaxWriter.Write(
                    type,
                    state,
                    new SwiftSyntaxWriterConfiguration {
                        InterfaceGenerationPhase = SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations
                    }
                );
                
                apisSection.Code.AppendLine(protocolExtensionCode);
                
                string implementationClassCode = typeSyntaxWriter.Write(
                    type,
                    state,
                    new SwiftSyntaxWriterConfiguration {
                        InterfaceGenerationPhase = SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass
                    }
                );
                
                apisSection.Code.AppendLine(implementationClassCode);
            }

            if (state.SkippedTypes.Contains(type)) {
                continue;
            }
            
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

        if (!Settings.DoNotGenerateSwiftNestedTypeAliases) {
            string namespacesCode = GetNamespacesCode(
                result,
                TypeDescriptorRegistry.Shared
            );
            
            namespacesSection.Code.AppendLine(namespacesCode);
        } else {
            namespacesSection.Code.AppendLine("// Omitted due to settings");
        }
        
        string footerCode = GetFooterCode();
        footerSection.Code.AppendLine(footerCode);

        return result;
    }

    private string GetNamespacesCode(
        Result result,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        SwiftCodeBuilder sb = new();
        
        var namespaceTree = result.GetNamespaceTreeOfGeneratedTypes();
        
        foreach (var node in namespaceTree.Children) {
            string nodeCode = GetNamespaceCode(
                node,
                result,
                typeDescriptorRegistry
            );

            sb.AppendLine(nodeCode);
        }

        string allCode = sb.ToString();

        return allCode;
    }

    private string GetNamespaceCode(
        NamespaceNode namespaceNode,
        Result result,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        if (namespaceNode.IsTreeRoot) { // Should never happen
            return string.Empty;
        }

        SwiftCodeBuilder sb = new();
        
        string name = namespaceNode.Name;
        
        string parentNames = namespaceNode.CompoundParentNames;
        string fullNamespaceName = namespaceNode.FullName;
        var typesInNamespace = result.GetTypesInNamespace(fullNamespaceName);

        Dictionary<string, Tuple<Type, string>> typeAliases = new();

        foreach (var type in typesInNamespace) {
            if (type.IsArray) {
                continue;
            }
            
            Type nonByRefType = type.GetNonByRefType();

            if (nonByRefType.IsArray) {
                continue;
            }

            if (nonByRefType.IsGenericType || 
                nonByRefType.IsConstructedGenericType ||
                nonByRefType.IsGenericTypeDefinition ||
                nonByRefType.IsGenericParameter) {
                continue;
            }

            if (nonByRefType.HasElementType) {
                Type? elementType = nonByRefType.GetElementType();

                if (elementType is not null) {
                    if (elementType.IsGenericType || 
                        elementType.IsConstructedGenericType ||
                        elementType.IsGenericTypeDefinition ||
                        elementType.IsGenericParameter) {
                        continue;
                    }
                }
            }

            string swiftTypeName;

            if (nonByRefType.IsPrimitive) {
                swiftTypeName = nonByRefType.CTypeName();
            } else {
                swiftTypeName = nonByRefType.GetTypeDescriptor(typeDescriptorRegistry).GetTypeName(
                    CodeLanguage.Swift,
                    false
                );
            }

            string swiftNamespacePrefix = fullNamespaceName.Replace('.', '_') + "_";
            
            string swiftTypeNameWithoutNamespace = swiftTypeName
                .Replace(swiftNamespacePrefix, string.Empty)
                .EscapedSwiftTypeAliasTypeName();

            typeAliases[swiftTypeName] = new(nonByRefType, swiftTypeNameWithoutNamespace);
        }

        SwiftCodeBuilder sbTypeAliases = new();

        foreach (var kvp in typeAliases) {
            var swiftTypeName = kvp.Key;
            var type = kvp.Value.Item1;
            var swiftTypeNameWithoutNamespace = kvp.Value.Item2;
            
            var typeAlias = Builder.TypeAlias(swiftTypeNameWithoutNamespace, swiftTypeName)
                .Public()
                .Build()
                .ToString();
            
            var typeDocumentationComment = type.GetDocumentation()
                ?.GetFormattedDocumentationComment();

            sbTypeAliases.AppendLine(typeDocumentationComment);
            sbTypeAliases.AppendLine(typeAlias);

            if (type.IsInterface) {
                var interfaceImplTypeAlias = Builder.TypeAlias($"{swiftTypeNameWithoutNamespace}{TypeDescriptor.SwiftDotNETInterfaceImplementationSuffix}", $"{swiftTypeName}{TypeDescriptor.SwiftDotNETInterfaceImplementationSuffix}")
                    .Public()
                    .Build()
                    .ToString();
            
                sbTypeAliases.AppendLine(typeDocumentationComment);
                sbTypeAliases.AppendLine(interfaceImplTypeAlias);
            }
        }

        string typeAliasesCode = sbTypeAliases.ToString();
        
        string nodeCode;
            
        if (string.IsNullOrEmpty(parentNames)) {
            nodeCode = $@"
public struct {name} {{
{typeAliasesCode.IndentAllLines(1)}
}}
";
        } else {
            nodeCode = $@"
public extension {parentNames} {{
    struct {name} {{
{typeAliasesCode.IndentAllLines(2)}
    }}
}}
";
        }

        sb.AppendLine(nodeCode);
        
        foreach (var subNode in namespaceNode.Children) {
            string subNodeCode = GetNamespaceCode(
                subNode,
                result,
                typeDescriptorRegistry
            );
                
            sb.AppendLine(subNodeCode);
        }

        string allCode = sb.ToString();

        return allCode;
    }

    private string GetHeaderCode()
    {
        return /*lang=Swift*/"""
import Foundation
""";
    }
    
    private string GetUtilsCode(Type[] types)
    {
        SwiftCodeBuilder sb = new();
        
        sb.AppendLine(SwiftSharedCode.SharedCode);
        sb.AppendLine();

        if (types.Contains(typeof(System.Guid))) {
            sb.AppendLine(SwiftSharedCode.GuidExtensions);
            sb.AppendLine();
        }

        if (types.Contains(typeof(System.Array))) {
            sb.AppendLine(SwiftSharedCode.ArrayExtensions);
            sb.AppendLine();
        }

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