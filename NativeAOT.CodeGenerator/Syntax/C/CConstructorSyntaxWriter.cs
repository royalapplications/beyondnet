using System.Reflection;

using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CConstructorSyntaxWriter: CMethodSyntaxWriter, IConstructorSyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((ConstructorInfo)@object, state);
    }

    public string Write(ConstructorInfo constructor, State state)
    {
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedMember(constructor) ?? throw new Exception("No C# generated member");
        
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
            constructor,
            methodKind,
            isStaticMethod,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            typeDescriptorRegistry,
            state
        );

        return ctorCode;
    }
}