using System.Reflection;
using System.Text;

using Beyond.NET.CodeGenerator.Collectors;
using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public class KotlinMethodSyntaxWriter: IKotlinSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((MethodInfo)@object, state, configuration);
    }

    public string Write(MethodInfo method, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedMember(method) ?? throw new Exception("No C# generated member");
        GeneratedMember cGeneratedMember = cResult.GetGeneratedMember(method) ?? throw new Exception("No C generated member");

        bool mayThrow = cSharpGeneratedMember.MayThrow;
        const MemberKind methodKind = MemberKind.Method;

        bool isStaticMethod = method.IsStatic;

        Type declaringType = method.DeclaringType ?? throw new Exception("No declaring type");
        Type returnType = method.ReturnType;
        IEnumerable<ParameterInfo> parameters = method.GetParameters();

        string methodCode = WriteMethod(
            cSharpGeneratedMember,
            cGeneratedMember,
            method,
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
            method,
            out _
        );

        return methodCode;
    }

    protected string WriteMethod(
        GeneratedMember cSharpGeneratedMember,
        GeneratedMember cMember,
        MemberInfo? memberInfo,
        MemberKind memberKind,
        bool isStaticMethod,
        bool mayThrow,
        Type declaringType,
        Type returnOrSetterOrEventHandlerType,
        IEnumerable<ParameterInfo> parameters,
        ISyntaxWriterConfiguration? syntaxWriterConfiguration,
        bool addToState,
        TypeDescriptorRegistry typeDescriptorRegistry,
        State state,
        MemberInfo? originatingMemberInfo,
        out string generatedName
    )
    {
        #region Preparation
        if (memberInfo == null &&
            memberKind != MemberKind.Destructor &&
            memberKind != MemberKind.TypeOf) {
            throw new Exception("memberInfo may only be null when memberKind is Destructor");
        }

        #region TODO: Unsupported Stuff
        if (returnOrSetterOrEventHandlerType.IsByRef) {
            generatedName = string.Empty;
            
            return $"// TODO: Method with by ref return or setter or event handler type ({cMember.GetGeneratedName(CodeLanguage.C)})";
        }
        #endregion TODO: Unsupported Stuff
        #endregion Preparation

        generatedName = string.Empty;

        return string.Empty;
    }
}