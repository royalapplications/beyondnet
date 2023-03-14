using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CDestructorSyntaxWriter: IDestructorSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }

    public string Write(Type type, State state)
    {
        if (type.IsVoid()) {
            return string.Empty;
        }
        
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(TypeDescriptorRegistry.Shared);

        StringBuilder sb = new();

        string fullTypeName = type.GetFullNameOrName();
         
        string methodNameC = $"{fullTypeName.Replace('.', '_')}_Destroy";
        methodNameC = state.UniqueGeneratedName(methodNameC, CodeLanguage.C);

        string typeNameC = typeDescriptor.GetTypeName(CodeLanguage.C, true);
        sb.AppendLine($"void /* System.Void */\n{methodNameC}(\n\t{typeNameC} /* {fullTypeName} */ self\n);");

        string generatedCode = sb.ToString();
        
        return generatedCode;
    }
}