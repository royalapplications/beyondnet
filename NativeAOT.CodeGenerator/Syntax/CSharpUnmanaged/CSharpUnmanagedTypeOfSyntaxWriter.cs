using System.Reflection;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedTypeOfSyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, ITypeOfSyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }

    public string Write(Type type, State state)
    {
        if (type.IsVoid() ||
            type.IsEnum) {
            return string.Empty;
        }
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        const bool mayThrow = false;
        const bool addToState = true;

        string destructorCode = WriteMethod(
            null,
            MemberKind.TypeOf,
            string.Empty,
            true,
            mayThrow,
            type,
            typeof(Type),
            Array.Empty<ParameterInfo>(),
            addToState,
            typeDescriptorRegistry,
            state,
            out _
        );

        return destructorCode;
    }
}