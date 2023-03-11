using System.Reflection;
using System.Text;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedConstructorSyntaxWriter: ConstructorSyntaxWriter
{
    public string Write(ConstructorInfo constructor)
    {
        StringBuilder ctorCode = new();

        Type declaringType = constructor.DeclaringType ?? throw new Exception("No declaring type for constructor");
        
        string constructorNameC = "Create";

        ctorCode.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{constructorNameC}\")]");
        ctorCode.AppendLine($"\tinternal static void* {constructorNameC}()");
        ctorCode.AppendLine("\t{");
        ctorCode.AppendLine($"\t\t{declaringType.FullName ?? declaringType.Name} instance = new();");
        ctorCode.AppendLine("\t\treturn instance.AllocateGCHandleAndGetAddress();");
        ctorCode.AppendLine("\t}");

        return ctorCode.ToString();
    }
}