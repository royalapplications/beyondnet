using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CMethodSyntaxWriter: ICSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((MethodInfo)@object, state);
    }

    public string Write(MethodInfo method, State state)
    {
        return "// TODO (Method)";
    }
}