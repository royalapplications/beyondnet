namespace Beyond.NET.CodeGenerator.Syntax.Swift;

public class SwiftSyntaxWriterConfiguration: ISyntaxWriterConfiguration
{
    public enum InterfaceGenerationPhases
    {
        NoInterface,
        Protocol,
        ProtocolExtensionForDefaultImplementations,
        ImplementationClass
    }

    public InterfaceGenerationPhases InterfaceGenerationPhase { get; set; } = InterfaceGenerationPhases.NoInterface;
}