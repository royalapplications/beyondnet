using System.Reflection;
using Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedEventSyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IEventSyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((EventInfo)@object, state, configuration);
    }
    
    public string Write(EventInfo @event, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        const bool mayThrow = false;
        const bool addToState = false;

        string eventName = @event.Name;

        Type declaringType = @event.DeclaringType ?? throw new Exception("No declaring type");;

        IEnumerable<ParameterInfo> parameters = Array.Empty<ParameterInfo>();

        CSharpCodeBuilder sb = new();

        Type? eventHandlerType = @event.EventHandlerType;

        if (eventHandlerType is null) {
            return $"// TODO: {eventName} - Event without Event Handler Type";
        }

        MethodInfo? addMethod = @event.GetAddMethod(false);
        MethodInfo? removeMethod = @event.GetRemoveMethod(false);

        if (addMethod != null) {
            bool isStaticMethod = addMethod.IsStatic;
            
            string adderCode = WriteMethod(
                addMethod,
                MemberKind.EventHandlerAdder,
                eventName,
                isStaticMethod,
                mayThrow,
                declaringType,
                eventHandlerType,
                parameters,
                addToState,
                typeDescriptorRegistry,
                state,
                out string generatedName
            );

            sb.AppendLine(adderCode);

            state.AddGeneratedMember(
                MemberKind.EventHandlerAdder,
                @event,
                mayThrow,
                generatedName,
                CodeLanguage.CSharpUnmanaged
            );
        }
        
        if (removeMethod != null) {
            bool isStaticMethod = removeMethod.IsStatic;
            
            string removerCode = WriteMethod(
                removeMethod,
                MemberKind.EventHandlerRemover,
                eventName,
                isStaticMethod,
                mayThrow,
                declaringType,
                eventHandlerType,
                parameters,
                addToState,
                typeDescriptorRegistry,
                state,
                out string generatedName
            );

            sb.AppendLine(removerCode);
            
            state.AddGeneratedMember(
                MemberKind.EventHandlerRemover,
                @event,
                mayThrow,
                generatedName,
                CodeLanguage.CSharpUnmanaged
            );
        }

        return sb.ToString();
    }
}