using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedDestructorSyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IDestructorSyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((Type)@object, state, configuration);
    }

    public string Write(Type type, State state, ISyntaxWriterConfiguration? configuration)
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
            MemberKind.Destructor,
            string.Empty,
            false,
            mayThrow,
            type,
            typeof(void),
            Array.Empty<ParameterInfo>(),
            addToState,
            typeDescriptorRegistry,
            state,
            out _
        );

        return destructorCode;
    }
}