using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CDestructorSyntaxWriter: ICSyntaxWriter, IDestructorSyntaxWriter
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
        
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(TypeDescriptorRegistry.Shared);
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedDestructor(type) ?? throw new Exception("No C# generated destructor");

        StringBuilder sb = new();

        string fullTypeName = type.GetFullNameOrName();
        string methodNameC = cSharpGeneratedMember.GetGeneratedName(CodeLanguage.CSharpUnmanaged) ?? throw new Exception("No generated unmanaged C# name");
        bool mayThrow = cSharpGeneratedMember.MayThrow;

        string typeNameC = typeDescriptor.GetTypeName(CodeLanguage.C, true);
        sb.AppendLine($"void /* System.Void */\n{methodNameC}(\n\t{typeNameC} /* {fullTypeName} */ self\n);");

        string generatedCode = sb.ToString();
        
        state.AddGeneratedMember(
            MemberKind.Destructor,
            null,
            mayThrow,
            methodNameC,
            CodeLanguage.C
        );
        
        return generatedCode;
    }
}