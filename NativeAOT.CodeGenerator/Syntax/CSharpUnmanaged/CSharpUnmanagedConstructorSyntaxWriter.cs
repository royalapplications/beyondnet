using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedConstructorSyntaxWriter: ICSharpUnmanagedSyntaxWriter, IConstructorSyntaxWriter
{
    public string Write(object @object)
    {
        return Write((ConstructorInfo)@object);
    }
    
    public string Write(ConstructorInfo constructor)
    {
        StringBuilder ctorCode = new();

        Type declaringType = constructor.DeclaringType ?? throw new Exception("No declaring type for constructor");
        
        string constructorNameC = "Create";

        ctorCode.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{constructorNameC}\")]");
        ctorCode.AppendLine($"internal static void* {constructorNameC}()");
        ctorCode.AppendLine("{");
        ctorCode.AppendLine($"\t{declaringType.FullName ?? declaringType.Name} instance = new();");
        ctorCode.AppendLine("\treturn instance.AllocateGCHandleAndGetAddress();");
        ctorCode.AppendLine("}");

        return ctorCode.ToString();
    }
}