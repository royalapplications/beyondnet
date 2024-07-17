using System.Reflection;

using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public class KotlinConstructorSyntaxWriter: KotlinMethodSyntaxWriter, IConstructorSyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((ConstructorInfo)@object, state, configuration);
    }

    public string Write(ConstructorInfo constructor, State state, ISyntaxWriterConfiguration? configuration)
    {
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedMember(constructor) ?? throw new Exception("No C# generated member");
        GeneratedMember cGeneratedMember = cResult.GetGeneratedMember(constructor) ?? throw new Exception("No C generated member");
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        bool mayThrow = cSharpGeneratedMember.MayThrow;
        const MemberKind methodKind = MemberKind.Constructor;

        bool isStaticMethod = true;

        Type declaringType = constructor.DeclaringType ?? throw new Exception("No declaring type");;

        if (declaringType.IsAbstract) {
            return string.Empty;
        }
        
        Type returnType = declaringType;
        IEnumerable<ParameterInfo> parameters = constructor.GetParameters();

        string ctorCode = WriteMethod(
            cSharpGeneratedMember,
            cGeneratedMember,
            constructor,
            methodKind,
            isStaticMethod,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            configuration,
            true,
            typeDescriptorRegistry,
            state,
            constructor,
            out _
        );

        return ctorCode;
    }
}