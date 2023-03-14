using System.Text;

using NativeAOT.CodeGenerator.Extensions;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedDestructorSyntaxWriter: IDestructorSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }

    public string Write(Type type, State state)
    {
        if (type.IsVoid() ||
            type.IsEnum) {
            return string.Empty;
        }
        
        StringBuilder sb = new();

        string fullTypeName = type.GetFullNameOrName();
        
        string methodNameC = $"{fullTypeName.Replace('.', '_')}_Destroy";
        methodNameC = state.UniqueGeneratedName(methodNameC, CodeLanguage.CSharpUnmanaged);

        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint=\"{methodNameC}\")]");
        sb.AppendLine($"internal static void /* System.Void */ {methodNameC}(void* /* {fullTypeName} */ __self)");
        sb.AppendLine("{");
        sb.AppendLine("\tInteropUtils.FreeIfAllocated(__self);");
        sb.AppendLine("}");

        string generatedCode = sb.ToString();
        
        return generatedCode;
    }
}