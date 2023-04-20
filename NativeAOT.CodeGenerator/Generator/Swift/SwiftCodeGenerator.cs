using System.Reflection;
using System.Text;
using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.SourceCode;
using NativeAOT.CodeGenerator.Syntax;
using NativeAOT.CodeGenerator.Syntax.Swift;
using NativeAOT.CodeGenerator.Syntax.Swift.Declaration;
using NativeAOT.CodeGenerator.Types;

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
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
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
            Syntax.State state = new(CSharpUnmanagedResult, CResult);
            
            string typeCode = typeSyntaxWriter.Write(type, state);
            apisSection.Code.AppendLine(typeCode);

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

        foreach (var kvp in typeExtensionMembers) {
            Type extendedType = kvp.Key;
            List<GeneratedMember> members = kvp.Value;

            string codeForOptional = GetTypeExtensionsCode(
                extendedType,
                true,
                members,
                typeDescriptorRegistry
            );
            
            string codeForNonOptional = GetTypeExtensionsCode(
                extendedType,
                false,
                members,
                typeDescriptorRegistry
            );

            extensionsSection.Code.AppendLine(codeForOptional);
            extensionsSection.Code.AppendLine(codeForNonOptional);
        }

        string footerCode = GetFooterCode();
        footerSection.Code.AppendLine(footerCode);

        return result;
    }

    private string GetTypeExtensionsCode(
        Type extendedType,
        bool isExtendedTypeOptional,
        List<GeneratedMember> generatedMembers,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        if (generatedMembers.Count <= 0) {
            return string.Empty;
        }
            
        StringBuilder sb = new();

        TypeDescriptor extendedTypeDescriptor = extendedType.GetTypeDescriptor(typeDescriptorRegistry);
        string extendedTypeSwiftName = extendedTypeDescriptor.GetTypeName(CodeLanguage.Swift, false);

        string extendedTypeOptionality = isExtendedTypeOptional
            ? "?"
            : string.Empty;
        
        string typeExtensionDecl = $"extension {extendedTypeSwiftName}{extendedTypeOptionality} {{";
        sb.AppendLine(typeExtensionDecl);

        StringBuilder sbMembers = new();
            
        foreach (GeneratedMember generatedMember in generatedMembers) {
            MethodBase? methodBase = generatedMember.Member as MethodBase;

            if (methodBase is null) {
                continue;
            }

            Type? typeWhereExtensionIsDeclared = methodBase.DeclaringType;

            if (typeWhereExtensionIsDeclared is null) {
                continue;
            }

            TypeDescriptor typeDescriptorWhereExtensionIsDeclared = typeWhereExtensionIsDeclared.GetTypeDescriptor(typeDescriptorRegistry);
            string swiftTypeNameWhereExtensionIsDeclared = typeDescriptorWhereExtensionIsDeclared.GetTypeName(CodeLanguage.Swift, false);
            
            string? generatedName = generatedMember.GetGeneratedName(CodeLanguage.Swift);

            if (string.IsNullOrEmpty(generatedName)) {
                continue;
            }
            
            // TODO: This is likely wrong
            bool isGeneric = false;
            IEnumerable<Type> genericParameters = Array.Empty<Type>();
            
            List<ParameterInfo> parameters = methodBase.GetParameters().ToList();
            parameters.RemoveAt(0);

            string parametersString = SwiftMethodSyntaxWriter.WriteParameters(
                generatedMember.MemberKind,
                null,
                false,
                typeWhereExtensionIsDeclared,
                parameters,
                isGeneric,
                genericParameters,
                false,
                typeDescriptorRegistry
            );

            Type returnType = typeof(void);

            if (methodBase is MethodInfo methodInfo) {
                returnType = methodInfo.ReturnType;
            }
            
            bool returnTypeIsByRef = returnType.IsByRef;

            if (returnTypeIsByRef) {
                returnType = returnType.GetNonByRefType();
            }
            
            TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
            
            const bool returnTypeIsOptional = true;
            
            // TODO: This generates inout TypeName if the return type is by ref
            string swiftReturnTypeName = returnTypeDescriptor.GetTypeName(
                CodeLanguage.Swift,
                true,
                returnTypeIsOptional,
                false,
                returnTypeIsByRef
            );

            bool mayThrow = generatedMember.MayThrow;
            
            SwiftFuncDeclaration decl = new(
                generatedName,
                SwiftVisibilities.Public,
                SwiftTypeAttachmentKinds.Instance,
                false,
                parametersString,
                mayThrow,
                !returnType.IsVoid()
                    ? swiftReturnTypeName
                    : null
            );
            
            string funcSignature = decl.ToString();
                
            sbMembers.AppendLine($"{funcSignature} {{");

            string toReturnOrNotToReturn = !returnType.IsVoid()
                ? "return "
                : string.Empty;
            
            string toTryOrNotToTry = mayThrow
                ? "try "
                : string.Empty;

            string invocationParametersString = SwiftMethodSyntaxWriter.WriteParameters(
                generatedMember.MemberKind,
                null,
                false,
                typeWhereExtensionIsDeclared,
                parameters,
                isGeneric,
                genericParameters,
                true,
                typeDescriptorRegistry
            );

            if (string.IsNullOrEmpty(invocationParametersString)) {
                invocationParametersString = "self";
            } else {
                invocationParametersString = "self, " + invocationParametersString;
            }

            string invocation = $"\t{toReturnOrNotToReturn}{toTryOrNotToTry}{swiftTypeNameWhereExtensionIsDeclared}.{generatedName}({invocationParametersString})";

            sbMembers.AppendLine(invocation);
            
            sbMembers.AppendLine("}");
        }

        sb.AppendLine(sbMembers
            .ToString()
            .IndentAllLines(1));

        sb.AppendLine("}");
        
        string code = sb.ToString();
        
        return code;
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