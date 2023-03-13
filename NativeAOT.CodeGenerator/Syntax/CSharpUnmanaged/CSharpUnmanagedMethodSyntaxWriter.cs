using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedMethodSyntaxWriter: ICSharpUnmanagedSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((MethodInfo)@object, state);
    }
    
    public string Write(MethodInfo method, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        const bool mayThrow = true;
        const bool isConstructor = false;

        bool isStaticMethod = method.IsStatic;
        string methodName = method.Name;

        Type declaringType = method.DeclaringType ?? throw new Exception("No declaring type");;
        Type returnType = method.ReturnType;
        IEnumerable<ParameterInfo> parameters = method.GetParameters();

        string methodCode = WriteMethod(
            method,
            methodName,
            isStaticMethod,
            isConstructor,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            typeDescriptorRegistry,
            state
        );

        return methodCode;
    }

    protected string WriteMethod(
        MemberInfo memberInfo,
        string methodName,
        bool isStaticMethod,
        bool isConstructor,
        bool mayThrow,
        Type declaringType,
        Type returnType,
        IEnumerable<ParameterInfo> parameters,
        TypeDescriptorRegistry typeDescriptorRegistry,
        State state
    )
    {
        string methodNameC;
        
        if (isConstructor) {
            methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_Create";
        } else {
            methodNameC = $"{declaringType.GetFullNameOrName().Replace('.', '_')}_{methodName}";
        }

        methodNameC = state.UniqueGeneratedName(methodNameC, CodeLanguage.CSharpUnmanaged);
        
        state.AddGeneratedMember(
            memberInfo,
            methodNameC,
            CodeLanguage.CSharpUnmanaged
        );
        
        TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
        string unmanagedReturnTypeName = returnTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
        string unmanagedReturnTypeNameWithComment = $"{unmanagedReturnTypeName} /* {returnType.GetFullNameOrName()} */";

        string methodSignatureParameters = WriteParameters(
            mayThrow,
            isStaticMethod,
            declaringType,
            parameters,
            typeDescriptorRegistry
        );
        
        StringBuilder sb = new();
        
        sb.AppendLine($"[UnmanagedCallersOnly(EntryPoint = \"{methodNameC}\")]");
        sb.AppendLine($"internal static {unmanagedReturnTypeNameWithComment} {methodNameC}({methodSignatureParameters})");
        sb.AppendLine("{");

        string? convertedSelfParameterName = null;

        if (!isStaticMethod) {
            string selfConversionCode = WriteSelfConversion(
                declaringType,
                typeDescriptorRegistry,
                out convertedSelfParameterName
            );

            sb.AppendLine(selfConversionCode);
        }

        string parameterConversions = WriteParameterConversions(
            parameters,
            typeDescriptorRegistry,
            out List<string> convertedParameterNames
        );

        sb.AppendLine(parameterConversions);

        if (mayThrow) {
            sb.AppendLine("""
    try {
""");
        }

        string implPrefix = mayThrow 
            ? "\t\t" 
            : "\t";
        
        string methodTarget = isStaticMethod
            ? (isConstructor ? "new " : string.Empty) + declaringType.GetFullNameOrName()
            : convertedSelfParameterName ?? string.Empty;

        string convertedParameterNamesString = string.Join(", ", convertedParameterNames);

        bool isReturning = !returnTypeDescriptor.IsVoid;

        string returnValuePrefix = string.Empty;
        string returnValueName = "__returnValue";

        if (isReturning) {
            returnValuePrefix = $"{returnType.GetFullNameOrName()} {returnValueName} = ";
        }

        string methodNameForInvocation = isConstructor
            ? string.Empty
            : $".{methodName}";
        
        sb.AppendLine($"{implPrefix}{returnValuePrefix}{methodTarget}{methodNameForInvocation}({convertedParameterNamesString});");

        string? convertedReturnValueName = null;

        if (isReturning) {
            string? returnValueTypeConversion = returnTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharp,
                CodeLanguage.CSharpUnmanaged
            );

            if (returnValueTypeConversion != null) {
                string fullReturnValueTypeConversion = string.Format(returnValueTypeConversion, returnValueName);
                
                convertedReturnValueName = "_returnValueNative";
                
                sb.AppendLine($"{implPrefix}{returnTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true)} {convertedReturnValueName} = {fullReturnValueTypeConversion};");
            } else {
                convertedReturnValueName = returnValueName;
            }
        }

        if (mayThrow) {
            sb.AppendLine("""

        if (__outException is not null) {
            *__outException = null;
        }

""");
            
            if (isReturning) {
                sb.AppendLine($"{implPrefix}return {convertedReturnValueName};");
            }
            
            sb.AppendLine("""
    } catch (Exception __exception) {
        if (__outException is not null) {
            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
                
            *__outException = __exceptionHandleAddress;
        }

""");

            if (isReturning) {
                string returnValue = returnTypeDescriptor.GetReturnValueOnException()
                                     ?? $"default({returnType.GetFullNameOrName()})";

                sb.AppendLine($"{implPrefix}return {returnValue};");
            }
            
            sb.AppendLine("\t}");
        } else {
            if (isReturning) {
                sb.AppendLine($"\treturn {convertedReturnValueName};");
            }
        }

        sb.AppendLine("}");

        return sb.ToString();
    }

    protected string WriteSelfConversion(
        Type type,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string convertedSelfParameterName
    )
    {
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
        
        StringBuilder sb = new();
        string parameterName = "__self";
        convertedSelfParameterName = parameterName;
                
        string? typeConversion = typeDescriptor.GetTypeConversion(CodeLanguage.CSharpUnmanaged, CodeLanguage.CSharp);
            
        if (typeConversion != null) {
            string convertedParameterName = $"{parameterName}DotNet";
                
            string fullTypeConversion = string.Format(typeConversion, parameterName);

            bool isSelfPointer = typeDescriptor.RequiresNativePointer;
            string throwPart = isSelfPointer ? $" ?? throw new ArgumentNullException(nameof({parameterName}))" : string.Empty;
            string typeConversionCode = $"{type.GetFullNameOrName()} {convertedParameterName} = {fullTypeConversion}{throwPart};";

            sb.AppendLine($"\t{typeConversionCode}");

            convertedSelfParameterName = convertedParameterName;
        }

        return sb.ToString();
    }

    protected string WriteParameters(
        bool mayThrow,
        bool isStatic,
        Type declaringType,
        IEnumerable<ParameterInfo> parameters,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        List<string> parameterList = new();

        if (!isStatic) {
            TypeDescriptor declaringTypeDescriptor = declaringType.GetTypeDescriptor(typeDescriptorRegistry);
            string declaringTypeName = declaringTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
            string selfParameterName = "__self";
            string parameterString = $"{declaringTypeName} /* {declaringType.GetFullNameOrName()} */ {selfParameterName}";

            parameterList.Add(parameterString);
        }

        foreach (var parameter in parameters) {
            Type parameterType = parameter.ParameterType;
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
            string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);

            string parameterString = $"{unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */ {parameter.Name}";
            parameterList.Add(parameterString);
        }

        if (mayThrow) {
            Type exceptionType = typeof(Exception);
            TypeDescriptor outExceptionTypeDescriptor = exceptionType.GetTypeDescriptor(typeDescriptorRegistry);
            string outExceptionTypeName = outExceptionTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true, true);
            string outExceptionParameterName = "__outException";

            string outExceptionParameterString = $"{outExceptionTypeName} /* {exceptionType.GetFullNameOrName()} */ {outExceptionParameterName}"; 
            parameterList.Add(outExceptionParameterString);
        }

        string parametersString = string.Join(", ", parameterList);

        return parametersString;
    }

    protected string WriteParameterConversions(
        IEnumerable<ParameterInfo> parameters,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out List<string> convertedParameterNames
    )
    {
        StringBuilder sb = new();
        convertedParameterNames = new();
        
        foreach (var parameter in parameters) {
            string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");
            
            Type parameterType = parameter.ParameterType;
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

            string? typeConversion = parameterTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharpUnmanaged, 
                CodeLanguage.CSharp
            );
            
            if (typeConversion != null) {
                string convertedParameterName = $"{parameterName}DotNet";
                
                string fullTypeConversion = string.Format(typeConversion, parameterName);
                string typeConversionCode = $"{parameterType.GetFullNameOrName()} {convertedParameterName} = {fullTypeConversion};";

                sb.AppendLine($"\t{typeConversionCode}");
                
                convertedParameterNames.Add(convertedParameterName);
            } else {
                convertedParameterNames.Add(parameterName);
            }
        }

        return sb.ToString();
    }
}