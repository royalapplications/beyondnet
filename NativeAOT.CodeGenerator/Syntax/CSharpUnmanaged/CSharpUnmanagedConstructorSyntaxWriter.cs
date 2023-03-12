using System.Reflection;

using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedConstructorSyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IConstructorSyntaxWriter
{
    public new string Write(object @object)
    {
        return Write((ConstructorInfo)@object);
    }
    
    public string Write(ConstructorInfo constructor)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        const bool mayThrow = true;
        const bool isConstructor = true;

        bool isStaticMethod = true;
        string methodName = constructor.Name;

        Type declaringType = constructor.DeclaringType ?? throw new Exception("No declaring type");;
        Type returnType = declaringType;
        IEnumerable<ParameterInfo> parameters = constructor.GetParameters();

        string ctorCode = WriteMethod(
            methodName,
            isStaticMethod,
            isConstructor,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            typeDescriptorRegistry
        );

        return ctorCode;
    }
}