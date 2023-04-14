using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.Swift;

public class SwiftEventSyntaxWriter: SwiftMethodSyntaxWriter, IEventSyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((EventInfo)@object, state);
    }

    public string Write(EventInfo @event, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");

        GeneratedMember? generatedAdderMember = cSharpUnmanagedResult.GetGeneratedMember(@event, MemberKind.EventHandlerAdder);
        GeneratedMember? generatedRemoverMember = cSharpUnmanagedResult.GetGeneratedMember(@event, MemberKind.EventHandlerRemover);

        if (generatedAdderMember is null &&
            generatedRemoverMember is null) {
            throw new Exception("No generated C# Unmanaged Member");
        }

        bool adderMayThrow = generatedAdderMember?.MayThrow ?? true;
        bool removerMayThrow = generatedRemoverMember?.MayThrow ?? true;
        
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

        if (adderMethod is not null &&
            generatedAdderMember is not null) {
            bool isStaticMethod = adderMethod.IsStatic;
            
            string adderCode = WriteMethod(
                generatedAdderMember,
                adderMethod,
                MemberKind.EventHandlerAdder,
                isStaticMethod,
                adderMayThrow,
                declaringType,
                eventHandlerType,
                parameters,
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(adderCode);
        }
        
        if (removerMethod is not null &&
            generatedRemoverMember is not null) {
            bool isStaticMethod = removerMethod.IsStatic;
            
            string removerCode = WriteMethod(
                generatedRemoverMember,
                removerMethod,
                MemberKind.EventHandlerRemover,
                isStaticMethod,
                removerMayThrow,
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