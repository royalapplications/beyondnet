using System.Reflection;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedDestructorSyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IDestructorSyntaxWriter
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

        string destructorCode = WriteMethod(
            null,
            MemberKind.Destructor,
            string.Empty,
            false,
            mayThrow,
            type,
            typeof(void),
            Array.Empty<ParameterInfo>(),
            typeDescriptorRegistry,
            state
        );

        return destructorCode;
    }
}