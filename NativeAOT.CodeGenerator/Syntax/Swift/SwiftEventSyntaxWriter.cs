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
        const bool addToState = false;
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");

        GeneratedMember? cSharpGeneratedAdderMember = cSharpUnmanagedResult.GetGeneratedMember(@event, MemberKind.EventHandlerAdder);
        GeneratedMember? cGeneratedAdderMember = cResult.GetGeneratedMember(@event, MemberKind.EventHandlerAdder);
        
        GeneratedMember? cSharpGeneratedRemoverMember = cSharpUnmanagedResult.GetGeneratedMember(@event, MemberKind.EventHandlerRemover);
        GeneratedMember? cGeneratedRemoverMember = cResult.GetGeneratedMember(@event, MemberKind.EventHandlerRemover);

        if (cSharpGeneratedAdderMember is null &&
            cSharpGeneratedRemoverMember is null) {
            throw new Exception("No generated C# Unmanaged Member");
        }
        
        if (cGeneratedAdderMember is null &&
            cGeneratedRemoverMember is null) {
            throw new Exception("No generated C Member");
        }

        bool adderMayThrow = cSharpGeneratedAdderMember?.MayThrow ?? true;
        bool removerMayThrow = cSharpGeneratedRemoverMember?.MayThrow ?? true;
        
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
            cSharpGeneratedAdderMember is not null &&
            cGeneratedAdderMember is not null) {
            bool isStaticMethod = adderMethod.IsStatic;
            
            string adderCode = WriteMethod(
                cSharpGeneratedAdderMember,
                cGeneratedAdderMember,
                adderMethod,
                MemberKind.EventHandlerAdder,
                isStaticMethod,
                adderMayThrow,
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
                adderMayThrow,
                generatedName,
                CodeLanguage.Swift
            );
        }
        
        if (removerMethod is not null &&
            cSharpGeneratedRemoverMember is not null &&
            cGeneratedRemoverMember is not null) {
            bool isStaticMethod = removerMethod.IsStatic;
            
            string removerCode = WriteMethod(
                cSharpGeneratedRemoverMember,
                cGeneratedRemoverMember,
                removerMethod,
                MemberKind.EventHandlerRemover,
                isStaticMethod,
                removerMayThrow,
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
                removerMayThrow,
                generatedName,
                CodeLanguage.Swift
            );
        }

        return sb.ToString();
    }
}