namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public class KotlinSyntaxWriterConfiguration: ISyntaxWriterConfiguration
{
    public enum GenerationPhases
    {
        KotlinBindings,
        JNA
    }

    public GenerationPhases GenerationPhase { get; set; } = GenerationPhases.KotlinBindings;
}