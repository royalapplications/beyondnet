namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public class KotlinSyntaxWriterConfiguration: ISyntaxWriterConfiguration
{
    public enum GenerationPhases
    {
        KotlinBindings,
        JNA
    }
    
    public enum ExtensionMethodKinds
    {
        None,
        Optional,
        NonOptional
    }

    public GenerationPhases GenerationPhase { get; set; } = GenerationPhases.KotlinBindings;
    
    public ExtensionMethodKinds ExtensionMethodKind { get; set; } = ExtensionMethodKinds.None;
    public Type? ExtensionMethodType { get; set; } = null;
    
    public bool IsExtensionMethod => 
        ExtensionMethodKind != ExtensionMethodKinds.None && ExtensionMethodType is not null;
}