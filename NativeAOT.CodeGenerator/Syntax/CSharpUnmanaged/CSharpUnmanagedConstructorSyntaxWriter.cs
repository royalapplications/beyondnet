using System.Reflection;

using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedConstructorSyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IConstructorSyntaxWriter
{
    public new string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((ConstructorInfo)@object, state, configuration);
    }
    
    public string Write(ConstructorInfo constructor, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        const bool mayThrow = true;
        const MemberKind methodKind = MemberKind.Constructor;
        const bool addToState = true;

        bool isStaticMethod = true;
        string methodName = constructor.Name;

        Type declaringType = constructor.DeclaringType ?? throw new Exception("No declaring type");;

        if (declaringType.IsAbstract) {
            return string.Empty;
        }
        
        Type returnType = declaringType;
        IEnumerable<ParameterInfo> parameters = constructor.GetParameters();

        string ctorCode = WriteMethod(
            constructor,
            methodKind,
            methodName,
            isStaticMethod,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            addToState,
            typeDescriptorRegistry,
            state,
            out _
        );

        return ctorCode;
    }
}