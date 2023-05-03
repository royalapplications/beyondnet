using System.Reflection;

namespace Beyond.NET.CodeGenerator.Syntax;

public interface IEventSyntaxWriter: ISyntaxWriter
{
    string Write(EventInfo @event, State state, ISyntaxWriterConfiguration? configuration);
}