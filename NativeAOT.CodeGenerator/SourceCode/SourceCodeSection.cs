using System.Text;

namespace NativeAOT.CodeGenerator.SourceCode;

public class SourceCodeSection
{
    public string Name { get; }
    public StringBuilder Code { get; } = new();
    
    public SourceCodeSection() : this(string.Empty) { }
    public SourceCodeSection(string name)
    {
        Name = name;
    }
}