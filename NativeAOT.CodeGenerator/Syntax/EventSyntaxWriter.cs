using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface EventSyntaxWriter: SyntaxWriter
{
    string Write(EventInfo @event);
}