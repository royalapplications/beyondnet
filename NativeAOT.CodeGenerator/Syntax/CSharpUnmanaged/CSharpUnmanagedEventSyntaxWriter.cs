using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedEventSyntaxWriter: ICSharpUnmanagedSyntaxWriter, IEventSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((EventInfo)@object, state);
    }
    
    public string Write(EventInfo @event, State state)
    {
        StringBuilder sb = new();
        
        string eventNameC = @event.Name;
                    
        sb.AppendLine($"// TODO (Event): {eventNameC}");

        return sb.ToString();
    }
}