using System.Reflection;

using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.C;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.C;

public class CEventSyntaxWriter: CMethodSyntaxWriter, IEventSyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((EventInfo)@object, state, configuration);
    }

    public string Write(EventInfo @event, State state, ISyntaxWriterConfiguration? configuration)
    {
        const bool addToState = false;
        
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

        CCodeBuilder sb = new();

        Type? eventHandlerType = @event.EventHandlerType;
        
        if (eventHandlerType is null) {
            return Builder.SingleLineComment($"TODO: {eventName} - Event without Event Handler Type")
                .ToString();
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
                CodeLanguage.C
            );
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
                CodeLanguage.C
            );
        }

        return sb.ToString();
    }
}