using System.Reflection;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CDestructorSyntaxWriter: CMethodSyntaxWriter, IDestructorSyntaxWriter
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
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedDestructor(type) ?? throw new Exception("No C# generated destructor");

        bool mayThrow = cSharpGeneratedMember.MayThrow;

        string code = WriteMethod(
            cSharpGeneratedMember,
            null,
            MemberKind.Destructor,
            false,
            mayThrow,
            type,
            typeof(void),
            Array.Empty<ParameterInfo>(),
            true,
            typeDescriptorRegistry,
            state,
            out _
        );

        return code;
    }
}