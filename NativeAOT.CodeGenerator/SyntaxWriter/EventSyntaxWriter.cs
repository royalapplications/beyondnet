using System.Reflection;

namespace NativeAOT.CodeGenerator;

public interface EventSyntaxWriter: SyntaxWriter
{
    string Write(EventInfo @event);
}