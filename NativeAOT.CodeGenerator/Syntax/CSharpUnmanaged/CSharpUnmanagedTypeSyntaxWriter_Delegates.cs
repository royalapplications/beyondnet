using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public partial class CSharpUnmanagedTypeSyntaxWriter
{
    private void WriteDelegateType(
        Type type,
        string fullTypeName,
        string cTypeName,
        MethodInfo? invokeMethod,
        StringBuilder sb,
        State state
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        // TODO: Duh...
        fullTypeName = fullTypeName.Replace("+", ".");

        var parameterInfos = invokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>();

        string managedParmeters = CSharpUnmanagedMethodSyntaxWriter.WriteParameters(
            CodeLanguage.CSharp,
            MemberKind.Automatic,
            null,
            false,
            true,
            type,
            parameterInfos,
            false,
            typeDescriptorRegistry
        );

        string unmanagedParameters = CSharpUnmanagedMethodSyntaxWriter.WriteParameters(
            CodeLanguage.CSharpUnmanaged,
            MemberKind.Automatic,
            null,
            false,
            true,
            type,
            parameterInfos,
            true,
            typeDescriptorRegistry
        );

        if (!string.IsNullOrEmpty(unmanagedParameters)) {
            unmanagedParameters += ", ";
        }
        
        string unmanagedParametersForInvocation = CSharpUnmanagedMethodSyntaxWriter.WriteParameters(
            CodeLanguage.CSharpUnmanaged,
            MemberKind.Automatic,
            null,
            false,
            true,
            type,
            parameterInfos,
            false,
            typeDescriptorRegistry
        );
        
        if (!string.IsNullOrEmpty(unmanagedParametersForInvocation)) {
            unmanagedParametersForInvocation = ", " + unmanagedParametersForInvocation;
        }

        var returnType = invokeMethod?.ReturnType ?? typeof(void);

        TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
        string unmanagedReturnTypeName = returnTypeDescriptor.GetTypeName(CodeLanguage.CSharpUnmanaged, true);
        string unmanagedReturnTypeNameWithComment = $"{unmanagedReturnTypeName} /* {returnType.GetFullNameOrName()} */";

        string managedReturnTypeName = returnType.IsVoid()
            ? "void"
            : returnType.GetFullNameOrName();

        string contextType = "void*";
        string cFunctionSignature = $"delegate* unmanaged<void* /* context */, {unmanagedParameters}{unmanagedReturnTypeNameWithComment} /* return type */>";
        string cDestructorFunctionSignature = $"delegate* unmanaged<{contextType}, void>";

        #region Properties
        sb.AppendLine($"\tinternal {contextType} Context {{ get; }}");
        sb.AppendLine($"\tinternal {cFunctionSignature} CFunction {{ get; }}");
        sb.AppendLine($"\tinternal {cDestructorFunctionSignature} CDestructorFunction {{ get; }}");
        
        sb.AppendLine();

        sb.AppendLine($"\tprivate WeakReference<{fullTypeName}> m_trampoline;");
        sb.AppendLine($"\tinternal {fullTypeName} Trampoline");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tget {");
        sb.AppendLine($"\t\t\t{fullTypeName}? trampoline;");
        sb.AppendLine();
        sb.AppendLine("\t\t\tif (m_trampoline is not null) {");
        sb.AppendLine("\t\t\t\tm_trampoline.TryGetTarget(out trampoline);");
        sb.AppendLine("\t\t\t} else {");
        sb.AppendLine("\t\t\t\ttrampoline = null;");
        sb.AppendLine("\t\t\t}");
        sb.AppendLine();
        sb.AppendLine("\t\t\tif (trampoline is null) {");
        sb.AppendLine("\t\t\t\ttrampoline = CreateTrampoline();");
        sb.AppendLine("\t\t\t\tm_trampoline = new(trampoline);");
        sb.AppendLine("\t\t\t}");
        sb.AppendLine();
        sb.AppendLine("\t\t\treturn trampoline;");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        #endregion Properties
        
        sb.AppendLine();

        #region Native Constructor
        sb.AppendLine($"\tprivate {cTypeName}({contextType} context, {cFunctionSignature} cFunction, {cDestructorFunctionSignature} cDestructorFunction)");
        sb.AppendLine("\t{");

        sb.AppendLine("\t\tContext = context;");
        sb.AppendLine("\t\tCFunction = cFunction;");
        sb.AppendLine("\t\tCDestructorFunction = cDestructorFunction;");

        sb.AppendLine("\t}");
        #endregion Native Constructor

        sb.AppendLine();
        
        #region Managed Constructor
        sb.AppendLine($"\tinternal {cTypeName}({fullTypeName} originalDelegate)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tm_trampoline = new(originalDelegate);");
        sb.AppendLine("\t}");
        #endregion Managed Constructor
        
        sb.AppendLine();
        
        #region Finalizer
        sb.AppendLine($"\t~{cTypeName}()");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tif (CDestructorFunction is null) {");
        sb.AppendLine("\t\t\treturn;");
        sb.AppendLine("\t\t}");
        sb.AppendLine();
        sb.AppendLine("\t\tCDestructorFunction(Context);");
        sb.AppendLine("\t}");
        #endregion Finalizer

        sb.AppendLine();

        #region Delegate Wrapper
        #region Trampoline
        sb.AppendLine($"\tprivate {fullTypeName}? CreateTrampoline()");
        sb.AppendLine("\t{");

        sb.AppendLine("\t\tif (CFunction is null) {");
        sb.AppendLine("\t\t\treturn null;");
        sb.AppendLine("\t\t}");

        sb.AppendLine();

        sb.AppendLine($"\t\tSystem.Type typeOfSelf = typeof({cTypeName});");
        sb.AppendLine("\t\tstring nameOfInvocationMethod = nameof(__InvokeByCallingCFunction);");
        sb.AppendLine("\t\tSystem.Reflection.BindingFlags bindingFlags = System.Reflection.BindingFlags.Instance | BindingFlags.NonPublic;");
        sb.AppendLine("\t\tSystem.Reflection.MethodInfo? invocationMethod = typeOfSelf.GetMethod(nameOfInvocationMethod, bindingFlags);");

        sb.AppendLine();

        sb.AppendLine("\t\tif (invocationMethod is null) {");
        sb.AppendLine("\t\t\tthrow new Exception(\"Failed to retrieve delegate invocation method\");");
        sb.AppendLine("\t\t}");

        sb.AppendLine();

        sb.AppendLine($"\t\t{fullTypeName} trampoline = ({fullTypeName})System.Delegate.CreateDelegate(typeof({fullTypeName}), this, invocationMethod);");
        sb.AppendLine();
        sb.AppendLine("\t\treturn trampoline;");

        sb.AppendLine("\t}");
        #endregion Trampoline

        sb.AppendLine();
        #region Invocation

        sb.AppendLine($"\tprivate {managedReturnTypeName} __InvokeByCallingCFunction({managedParmeters})");
        sb.AppendLine("\t{");

        string parameterConversions = CSharpUnmanagedMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.CSharp,
            CodeLanguage.CSharpUnmanaged,
            parameterInfos,
            typeDescriptorRegistry,
            out List<string> convertedParameterNames
        );

        sb.AppendLine(
            parameterConversions.IndentAllLines(1)
        );

        sb.AppendLine();

        string parameterNamesString = string.Join(", ", convertedParameterNames);

        if (!string.IsNullOrEmpty(parameterNamesString)) {
            parameterNamesString = ", " + parameterNamesString;
        }

        bool hasReturnType = !returnType.IsVoid();

        string returnValueName = "__returnValue"; 

        string invocationPrefix = hasReturnType
            ? $"var {returnValueName} = "
            : string.Empty;

        string invocation = $"CFunction(Context{parameterNamesString})";

        sb.AppendLine($"\t\t{invocationPrefix}{invocation};");

        if (hasReturnType) {
            string? returnTypeConversion = returnTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharpUnmanaged,
                CodeLanguage.CSharp
            );

            string convertedReturnValueName = "__returnValueConverted";

            string? returnValueDestructorFormat = returnTypeDescriptor.GetDestructor(CodeLanguage.CSharpUnmanaged);
            string? returnValueDestructor;

            if (!string.IsNullOrEmpty(returnValueDestructorFormat)) {
                returnValueDestructor = string.Format(returnValueDestructorFormat, returnValueName) + ";";
            } else {
                returnValueDestructor = null;
            }
    
            if (!string.IsNullOrEmpty(returnTypeConversion)) {
                returnTypeConversion = string.Format(returnTypeConversion, "__returnValue");
                returnTypeConversion = $"var {convertedReturnValueName} = {returnTypeConversion}";

                sb.AppendLine($"\t\t{returnTypeConversion};");
                sb.AppendLine();

                if (!string.IsNullOrEmpty(returnValueDestructor)) {
                    sb.AppendLine($"\t\t{returnValueDestructor}");
                    sb.AppendLine();
                }

                sb.AppendLine($"\t\treturn {convertedReturnValueName};");
            } else {
                if (!string.IsNullOrEmpty(returnValueDestructor)) {
                    sb.AppendLine($"\t\t{returnValueDestructor}");
                    sb.AppendLine();
                }
                
                sb.AppendLine($"\t\treturn {returnValueName};");
            }
        }

        sb.AppendLine("\t}");
        #endregion Invocation
        #endregion Delegate Wrapper

        sb.AppendLine();

        #region Native API
        #region Create
        sb.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{cTypeName}_Create\")]");
        sb.AppendLine($"\tpublic static void* Create({contextType} context, {cFunctionSignature} cFunction, {cDestructorFunctionSignature} cDestructorFunction)");
        sb.AppendLine("\t{");
        sb.AppendLine($"\t\tvar self = new {cTypeName}(context, cFunction, cDestructorFunction);");
        sb.AppendLine("\t\tvoid* selfHandle = self.AllocateGCHandleAndGetAddress();");
        sb.AppendLine();
        sb.AppendLine("\t\treturn selfHandle;");
        sb.AppendLine("\t}");
        #endregion Create

        sb.AppendLine();

        #region Invoke
        sb.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{cTypeName}_Invoke\")]");
        sb.AppendLine($"\tpublic static {unmanagedReturnTypeNameWithComment} Invoke(void* self{unmanagedParametersForInvocation})");
        sb.AppendLine("\t{");
        
        sb.AppendLine("\t\tif (self is null) {");
        sb.AppendLine("\t\t\tthrow new ArgumentNullException(nameof(self));");
        sb.AppendLine("\t\t}");
        sb.AppendLine();
        sb.AppendLine($"\t\tvar selfConverted = InteropUtils.GetInstance<{cTypeName}>(self);");
        sb.AppendLine();
        
        string parameterConversionsForInvocation = CSharpUnmanagedMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.CSharpUnmanaged,
            CodeLanguage.CSharp,
            parameterInfos,
            typeDescriptorRegistry,
            out List<string> convertedParameterNamesForInvocation
        );

        sb.AppendLine(
            parameterConversionsForInvocation.IndentAllLines(1)
        );

        sb.AppendLine();

        string parameterNamesStringForInvocation = string.Join(", ", convertedParameterNamesForInvocation);

        string trampolineInvocationSuffix;

        if (type == typeof(System.Delegate) ||
            type == typeof(System.MulticastDelegate)) {
            trampolineInvocationSuffix = ".DynamicInvoke";
        } else {
            trampolineInvocationSuffix = string.Empty;
        }
        
        string managedInvocation = $"selfConverted.Trampoline{trampolineInvocationSuffix}({parameterNamesStringForInvocation})";

        sb.AppendLine($"\t\t{invocationPrefix}{managedInvocation};");

        if (hasReturnType) {
            string? returnTypeConversion = returnTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharp,
                CodeLanguage.CSharpUnmanaged
            );
        
            string convertedReturnValueName = "__returnValueConverted";
        
            if (!string.IsNullOrEmpty(returnTypeConversion)) {
                returnTypeConversion = string.Format(returnTypeConversion, "__returnValue");
                returnTypeConversion = $"var {convertedReturnValueName} = {returnTypeConversion}";
        
                sb.AppendLine($"\t\t{returnTypeConversion};");
                sb.AppendLine();

                sb.AppendLine($"\t\treturn {convertedReturnValueName};");
            } else {
                sb.AppendLine($"\t\treturn {returnValueName};");
            }
        }
        
        sb.AppendLine("\t}");
        #endregion Invoke
        
        sb.AppendLine();

        #region Context Get
        sb.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{cTypeName}_Context_Get\")]");
        sb.AppendLine($"\tpublic static {contextType} Context_Get(void* self)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tif (self is null) {");
        sb.AppendLine("\t\t\tthrow new ArgumentNullException(nameof(self));");
        sb.AppendLine("\t\t}");
        sb.AppendLine();
        sb.AppendLine($"\t\tvar selfConverted = InteropUtils.GetInstance<{cTypeName}>(self);");
        sb.AppendLine();
        sb.AppendLine("\t\treturn selfConverted.Context;");
        sb.AppendLine("\t}");
        #endregion Context Get
        
        sb.AppendLine();

        #region CFunction Get
        sb.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{cTypeName}_CFunction_Get\")]");
        sb.AppendLine($"\tpublic static {cFunctionSignature} CFunction_Get(void* self)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tif (self is null) {");
        sb.AppendLine("\t\t\tthrow new ArgumentNullException(nameof(self));");
        sb.AppendLine("\t\t}");
        sb.AppendLine();
        sb.AppendLine($"\t\tvar selfConverted = InteropUtils.GetInstance<{cTypeName}>(self);");
        sb.AppendLine();
        sb.AppendLine("\t\treturn selfConverted.CFunction;");
        sb.AppendLine("\t}");
        #endregion CFunction Get
        
        sb.AppendLine();

        #region CDestructorFunction Get
        sb.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{cTypeName}_CDestructorFunction_Get\")]");
        sb.AppendLine($"\tpublic static {cDestructorFunctionSignature} CDestructorFunction_Get(void* self)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tif (self is null) {");
        sb.AppendLine("\t\t\tthrow new ArgumentNullException(nameof(self));");
        sb.AppendLine("\t\t}");
        sb.AppendLine();
        sb.AppendLine($"\t\tvar selfConverted = InteropUtils.GetInstance<{cTypeName}>(self);");
        sb.AppendLine();
        sb.AppendLine("\t\treturn selfConverted.CDestructorFunction;");
        sb.AppendLine("\t}");
        #endregion CDestructorFunction Get
        #endregion Native API

        sb.AppendLine();
        
        // TODO: Add to State

        WriteDestructor(
            type,
            sb,
            state
        );
    }
}