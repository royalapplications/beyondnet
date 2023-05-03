using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.C;

public class CTypeOfSyntaxWriter: CMethodSyntaxWriter, ITypeOfSyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((Type)@object, state, configuration);
    }

    public string Write(Type type, State state, ISyntaxWriterConfiguration? configuration)
    {
        if (type.IsVoid()) {
            return string.Empty;
        }

        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedTypeOf(type) ?? throw new Exception("No C# generated typeOf");

        bool mayThrow = cSharpGeneratedMember.MayThrow;

        string code = WriteMethod(
            cSharpGeneratedMember,
            null,
            MemberKind.TypeOf,
            true,
            mayThrow,
            type,
            typeof(Type),
            Array.Empty<ParameterInfo>(),
            true,
            typeDescriptorRegistry,
            state,
            out _
        );

        return code;
    }
}