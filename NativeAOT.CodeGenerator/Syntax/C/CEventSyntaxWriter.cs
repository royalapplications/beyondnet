using System.Reflection;
using NativeAOT.CodeGenerator.Generator;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CEventSyntaxWriter: ICSyntaxWriter, IEventSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((EventInfo)@object, state);
    }

    public string Write(EventInfo @event, State state)
    {
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        return "// TODO (Event)";
    }
}