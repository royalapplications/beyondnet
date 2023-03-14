namespace NativeAOT.CodeGenerator.Syntax.C;

public class CDestructorSyntaxWriter: IDestructorSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }

    public string Write(Type type, State state)
    {
        return "// TODO (Destructor)";
    }
}