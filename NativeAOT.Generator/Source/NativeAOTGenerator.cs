using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

// using NativeAOT.Core;

namespace NativeAOT.Generator;

[Generator]
public class NativeAOTGenerator: IIncrementalGenerator
{
    private static string NativeExportAttributeFullName => "NativeAOT.Core.NativeExport";
    
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Uncomment to wait for debugger
        // SpinWait.SpinUntil(() => Debugger.IsAttached);

        // Do a simple filter for enums
        IncrementalValuesProvider<ClassDeclarationSyntax> classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // select classes with attributes
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // select the class with the [NativeExport] attribute
            .Where(static m => m is not null)!; // filter out attributed classes that we don't care about

        // Combine the selected enums with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilationAndClasses
            = context.CompilationProvider.Combine(classDeclarations.Collect());

        // Generate the source using the compilation and classes
        context.RegisterSourceOutput(compilationAndClasses,
            static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        bool isClassNode = node is ClassDeclarationSyntax;
        
        return isClassNode;
    }
    
    static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a ClassDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        ClassDeclarationSyntax classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists) {
            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes) {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol) {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                string fullName = attributeContainingTypeSymbol.ToDisplayString();

                // Is the attribute the [NativeExport] attribute?
                if (fullName == NativeExportAttributeFullName) {
                    // return the class
                    return classDeclarationSyntax;
                }
            }
        }

        // we didn't find the attribute we were looking for
        
        return null;
    }
    
    static void Execute(
        Compilation compilation,
        ImmutableArray<ClassDeclarationSyntax> classes,
        SourceProductionContext context
    )
    {
        if (classes.IsDefaultOrEmpty) {
            // nothing to do yet
            return;
        }

        // I'm not sure if this is actually necessary, but `[LoggerMessage]` does it, so seems like a good idea!
        IEnumerable<ClassDeclarationSyntax> distinctClasses = classes.Distinct();

        // Convert each ClassDeclarationSyntax to an NativeClass
        List<NativeClass> nativeClasses = GetNativeClassesToGenerate(
            compilation,
            distinctClasses,
            context.CancellationToken
        );

        foreach (NativeClass nativeClass in nativeClasses) {
            string generatedSourceCode = GenerateSourceCodeForNativeClass(nativeClass);

            string fileName = $"{nativeClass.NameWithUnderscores}_Native.g.cs";
            
            context.AddSource(fileName, generatedSourceCode);
        }
    }
    
    static List<NativeClass> GetNativeClassesToGenerate(
        Compilation compilation,
        IEnumerable<ClassDeclarationSyntax> classes,
        CancellationToken ct
    )
    {
        // Create a list to hold our output
        var nativeClasses = new List<NativeClass>();
        
        // Get the semantic representation of our marker attribute 
        INamedTypeSymbol? nativeExportAttribute = compilation.GetTypeByMetadataName(NativeExportAttributeFullName);

        if (nativeExportAttribute is null) {
            // If this is null, the compilation couldn't find the marker attribute type
            // which suggests there's something very wrong! Bail out..
            
            return nativeClasses;
        }

        foreach (ClassDeclarationSyntax classDeclarationSyntax in classes) {
            // stop if we're asked to
            ct.ThrowIfCancellationRequested();

            // Get the semantic representation of the class syntax
            SemanticModel semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
            
            if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol) {
                // something went wrong, bail out
                continue;
            }

            // Get the full type name of the class
            string className = classSymbol.ToString();

            // // Get all the members in the class
            // ImmutableArray<ISymbol> classMembers = classSymbol.GetMembers();
            //
            // List<string> members = new(classMembers.Length);
            //
            // // Get all the fields from the enum, and add their name to the list
            // foreach (ISymbol member in classMembers) {
            //     if (member is IFieldSymbol field && 
            //         field.ConstantValue is not null) {
            //         members.Add(member.Name);
            //     }
            // }

            // Create an EnumToGenerate for use in the generation phase
            NativeClass nativeClass = new(className);

            if (!nativeClasses.Contains(nativeClass)) {
                nativeClasses.Add(nativeClass);
            }
        }

        return nativeClasses;
    }
    
    internal static string GenerateSourceCodeForNativeClass(NativeClass nativeClass)
    {
//         string sourceCode = @$"""
// using System.Runtime.InteropServices;
//
// using NativeAOT.Core;
//
// namespace NativeAOTSample
// {{
//     internal static unsafe class {nativeClass.NameWithUnderscores}_Native
//     {{
//         // TODO
//     }}
// }}
// """;

        string sourceCode = "// Hello, this was generated!";

        return sourceCode;
    }
}