using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CConstructorSyntaxWriter: CMethodSyntaxWriter, IConstructorSyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((ConstructorInfo)@object, state);
    }

    public string Write(ConstructorInfo constructor, State state)
    {
        return "// TODO (Constructor)";
    }
}