using System.Reflection;
using NativeAOT.CodeGenerator.Generator;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CMethodSyntaxWriter: ICSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((MethodInfo)@object, state);
    }

    public string Write(MethodInfo method, State state)
    {
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");

        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedMember(method) ?? throw new Exception("No C# generated member");
        
        string nativeName = cSharpGeneratedMember.GetGeneratedName(CodeLanguage.CSharpUnmanaged) ?? throw new Exception("No native name");
        
        

        return $"// TODO (Method): {nativeName}";
    }
}