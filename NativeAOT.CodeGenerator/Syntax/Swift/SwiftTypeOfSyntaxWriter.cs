using System.Reflection;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.Swift;

public class SwiftTypeOfSyntaxWriter: SwiftMethodSyntaxWriter, ITypeOfSyntaxWriter
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
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedTypeOf(type) ?? throw new Exception("No C# generated typeOf");
        GeneratedMember cGeneratedMember = cResult.GetGeneratedTypeOf(type) ?? throw new Exception("No C generated typeOf");

        bool mayThrow = cSharpGeneratedMember.MayThrow;

        string code = WriteMethod(
            cSharpGeneratedMember,
            cGeneratedMember,
            null,
            MemberKind.TypeOf,
            true,
            mayThrow,
            type,
            typeof(Type),
            Array.Empty<ParameterInfo>(),
            configuration,
            true,
            typeDescriptorRegistry,
            state,
            out _
        );

        return code;
    }
}