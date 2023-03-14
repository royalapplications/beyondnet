using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CEventSyntaxWriter: ICSyntaxWriter, IEventSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((EventInfo)@object, state);
    }

    public string Write(EventInfo @event, State state)
    {
        return "// TODO (Event)";
    }
}