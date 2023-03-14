using System.Reflection;
using NativeAOT.CodeGenerator.Generator;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CConstructorSyntaxWriter: CMethodSyntaxWriter, IConstructorSyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((ConstructorInfo)@object, state);
    }

    public string Write(ConstructorInfo constructor, State state)
    {
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        return "// TODO (Constructor)";
    }
}