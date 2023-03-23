using System.Reflection;
using System.Text;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CEventSyntaxWriter: CMethodSyntaxWriter, IEventSyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((EventInfo)@object, state);
    }

    public string Write(EventInfo @event, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");

        var generatedCSharpUnmanagedMember = cSharpUnmanagedResult.GetGeneratedMember(@event);

        if (generatedCSharpUnmanagedMember == null) {
            throw new Exception("No generated C# Unmanaged Member");
        }

        bool mayThrow = generatedCSharpUnmanagedMember.MayThrow;
        
        string eventName = @event.Name;
        
        Type declaringType = @event.DeclaringType ?? throw new Exception("No declaring type");;

        IEnumerable<ParameterInfo> parameters = Array.Empty<ParameterInfo>();

        StringBuilder sb = new();

        Type? eventHandlerType = @event.EventHandlerType;
        
        if (eventHandlerType is null) {
            return $"// TODO: {eventName} - Event without Event Handler Type";
        }

        MethodInfo? adderMethod = @event.GetAddMethod(false);
        MethodInfo? removerMethod = @event.GetRemoveMethod(false);

        if (adderMethod != null) {
            bool isStaticMethod = adderMethod.IsStatic;
            
            string adderCode = WriteMethod(
                adderMethod,
                MemberKind.EventHandlerAdder,
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
        
        if (removerMethod != null) {
            bool isStaticMethod = removerMethod.IsStatic;
            
            string removerCode = WriteMethod(
                removerMethod,
                MemberKind.EventHandlerRemover,
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