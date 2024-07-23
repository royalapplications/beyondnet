using System.Reflection;
using System.Text;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.SourceCode;
using Beyond.NET.CodeGenerator.Syntax;
using Beyond.NET.CodeGenerator.Syntax.Kotlin;
using Beyond.NET.CodeGenerator.Syntax.Kotlin.Builders;
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
        KotlinSyntaxWriterConfiguration syntaxWriterConfiguration = new() {
            GenerationPhase = KotlinSyntaxWriterConfiguration.GenerationPhases.KotlinBindings
        };
        
        SourceCodeSection headerSection = writer.AddSection("Header");
        SourceCodeSection utilsSection = writer.AddSection("Utils");
        SourceCodeSection commonTypesSection = writer.AddSection("Common Types");
        SourceCodeSection unsupportedTypesSection = writer.AddSection("Unsupported Types");
        SourceCodeSection kotlinSection = writer.AddSection("Kotlin");
        SourceCodeSection jnaSection = writer.AddSection("JNA");
        SourceCodeSection extensionsSection = writer.AddSection("API Extensions");
        SourceCodeSection namespacesSection = writer.AddSection("Namespaces");
        SourceCodeSection footerSection = writer.AddSection("Footer");

        // TODO
        var package = "com.example.jnatest.BeyondDotNETSampleNative";
        
        string header = GetHeaderCode(package);
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

        Generate(
            syntaxWriterConfiguration,
            kotlinSection,
            unsupportedTypes,
            types,
            typeSyntaxWriter,
            result
        );
        
        syntaxWriterConfiguration = new KotlinSyntaxWriterConfiguration
        {
            GenerationPhase = KotlinSyntaxWriterConfiguration.GenerationPhases.JNA
        };

        // TODO: Replace libName, class name
        var jnaStart = """
object BeyondDotNETSampleNative {
    init {
        val libName = "BeyondDotNETSampleNative"
        Native.register(BeyondDotNETSampleNative::class.java, libName)
    }
    
    external fun DNStringFromC(cString: String): Pointer
    external fun DNStringToC(systemString: Pointer): Pointer
                                     
""";

        jnaSection.Code.AppendLine(jnaStart);
        
        Generate(
            syntaxWriterConfiguration,
            jnaSection,
            unsupportedTypes,
            types,
            typeSyntaxWriter,
            result
        );
        
        var jnaEnd = """
}                 
""";

        jnaSection.Code.AppendLine(jnaEnd);

        string footerCode = GetFooterCode();
        footerSection.Code.AppendLine(footerCode);

        return result;
    }

    private void Generate(
        KotlinSyntaxWriterConfiguration syntaxWriterConfiguration,
        SourceCodeSection section,
        Dictionary<Type, string> unsupportedTypes,
        IEnumerable<Type> types,
        ITypeSyntaxWriter typeSyntaxWriter,
        Result result
    )
    {
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
                syntaxWriterConfiguration
            );

            var fullTypeName = type.GetFullNameOrName();

            section.Code.AppendLine();
            section.Code.AppendLine(new SingleLineComment($"MARK: - BEGIN {fullTypeName}").ToString().IndentAllLines(1));
            section.Code.AppendLine(typeCode);
            section.Code.AppendLine(new SingleLineComment($"MARK: - END {fullTypeName}").ToString().IndentAllLines(1));
            section.Code.AppendLine();
            
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
    }

    private string GetHeaderCode(string package)
    {
        return /*lang=Kt*/$"""
package {package}

import com.sun.jna.*
import com.sun.jna.ptr.*
""";
    }
    
    private string GetUtilsCode(Type[] types)
    {
        KotlinCodeBuilder sb = new();
        
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