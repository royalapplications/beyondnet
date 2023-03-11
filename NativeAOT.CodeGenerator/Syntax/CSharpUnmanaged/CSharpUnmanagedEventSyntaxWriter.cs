using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedEventSyntaxWriter: EventSyntaxWriter
{
    public string Write(EventInfo @event)
    {
        StringBuilder sb = new();
        
        string eventNameC = @event.Name;
                    
        sb.AppendLine($"\t// TODO (Event): {eventNameC}");

        return sb.ToString();
    }
}