using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedEventSyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IEventSyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((EventInfo)@object, state);
    }
    
    public string Write(EventInfo @event, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        const bool mayThrow = false;

        string eventName = @event.Name;

        Type declaringType = @event.DeclaringType ?? throw new Exception("No declaring type");;

        IEnumerable<ParameterInfo> parameters = Array.Empty<ParameterInfo>();

        StringBuilder sb = new();

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
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(adderCode);
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
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(removerCode);
        }

        return sb.ToString();
    }
}