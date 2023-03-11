using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedEventSyntaxWriter: ICSharpUnmanagedSyntaxWriter, IEventSyntaxWriter
{
    public string Write(object @object)
    {
        return Write((EventInfo)@object);
    }
    
    public string Write(EventInfo @event)
    {
        StringBuilder sb = new();
        
        string eventNameC = @event.Name;
                    
        sb.AppendLine($"// TODO (Event): {eventNameC}");

        return sb.ToString();
    }
}