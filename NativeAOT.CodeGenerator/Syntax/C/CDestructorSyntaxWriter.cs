using NativeAOT.CodeGenerator.Generator;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CDestructorSyntaxWriter: IDestructorSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }

    public string Write(Type type, State state)
    {
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        return "// TODO (Destructor)";
    }
}