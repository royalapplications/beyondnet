using System.Reflection;

namespace NativeAOT.CodeGenerator.Syntax;

public interface IEventSyntaxWriter: ISyntaxWriter
{
    string Write(EventInfo @event);
}