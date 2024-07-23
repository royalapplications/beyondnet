using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.Syntax.CSharpUnmanaged;

public partial class CSharpUnmanagedTypeSyntaxWriter
{
    private void WriteDelegateType(
        ISyntaxWriterConfiguration? configuration,
        Type type,
        string fullTypeName,
        string cTypeName,
        MethodInfo? invokeMethod,
        CSharpCodeBuilder sb,
        State state
    )
    {
        var parameterInfos = invokeMethod?.GetParameters() ?? Array.Empty<ParameterInfo>();
        var returnType = invokeMethod?.ReturnType ?? typeof(void);
        
        WriteDelegateType(
            configuration,
            type,
            fullTypeName,
            cTypeName,
            parameterInfos,
            returnType,
            sb,
            state
        );
    }
    
    private void WriteDelegateType(
        ISyntaxWriterConfiguration? configuration,
        Type type,
        string fullTypeName,
        string cTypeName,
        ParameterInfo[] parameterInfos,
        Type returnType,
        CSharpCodeBuilder sb,
        State state
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        // TODO: Duh...
        fullTypeName = fullTypeName.Replace("+", ".");
        
        foreach (var parameter in parameterInfos) {
            if (parameter.IsOut) {
                sb.AppendLine("\t// TODO: Unsupported delegate type. Reason: Has out parameters");
                
                return;
            }
            
            if (parameter.IsIn) {
                sb.AppendLine("\t// TODO: Unsupported delegate type. Reason: Has in parameters");
                
                return;
            }

            Type parameterType = parameter.ParameterType;

            if (!ExperimentalFeatureFlags.EnableByRefParametersInDelegates) {
                if (parameterType.IsByRef) {
                    sb.AppendLine("\t// TODO: Unsupported delegate type. Reason: Has by ref parameters");

                    return;
                }
            }

            if (parameterType.IsGenericParameter ||
                parameterType.IsGenericMethodParameter) {
                sb.AppendLine("\t// TODO: Unsupported delegate type. Reason: Has generic parameters");
                
                return;
            }
        }
        
        // TODO: Generics

        string managedParmeters = CSharpUnmanagedMethodSyntaxWriter.WriteParameters(
            CodeLanguage.CSharp,
            MemberKind.Automatic,
            null,
            false,
            true,
            type,
            parameterInfos,
            false,
            Array.Empty<Type>(),
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
            false,
            Array.Empty<Type>(),
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
            Array.Empty<Type>(),
            false,
            typeDescriptorRegistry
        );
        
        if (!string.IsNullOrEmpty(unmanagedParametersForInvocation)) {
            unmanagedParametersForInvocation = ", " + unmanagedParametersForInvocation;
        }
        
        if (returnType.IsByRef) {
            sb.AppendLine("\t// TODO: Unsupported delegate type. Reason: Has by ref return type");

            return;
        }

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
        
        // TODO: Generics

        string parameterConversions = CSharpUnmanagedMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.CSharp,
            CodeLanguage.CSharpUnmanaged,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            Array.Empty<Type>(),
            typeDescriptorRegistry,
            out List<string> convertedParameterNames,
            out _,
            out _,
            out List<string> managedToNativeConvertedTypeDestructors
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

        if (managedToNativeConvertedTypeDestructors.Count > 0) {
            sb.AppendLine();
        }
        
        foreach (var typeDestructor in managedToNativeConvertedTypeDestructors) {
            var indentedTypeDestructor = typeDestructor
                .IndentAllLines(1);
            
            sb.AppendLine(indentedTypeDestructor);
        }

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
        sb.AppendLine($"\tpublic static {unmanagedReturnTypeNameWithComment} Invoke(void* self{unmanagedParametersForInvocation}, void** __outException)");
        sb.AppendLine("\t{");
        
        sb.AppendLine("\t\tif (self is null) {");
        sb.AppendLine("\t\t\tthrow new ArgumentNullException(nameof(self));");
        sb.AppendLine("\t\t}");
        sb.AppendLine();
        sb.AppendLine("\t\ttry {");
        sb.AppendLine($"\t\t\tvar selfConverted = InteropUtils.GetInstance<{cTypeName}>(self);");
        sb.AppendLine();
        
        string parameterConversionsForInvocation = CSharpUnmanagedMethodSyntaxWriter.WriteParameterConversions(
            CodeLanguage.CSharpUnmanaged,
            CodeLanguage.CSharp,
            parameterInfos,
            false,
            Array.Empty<Type>(),
            Array.Empty<Type>(),
            typeDescriptorRegistry,
            out List<string> convertedParameterNamesForInvocation,
            out _,
            out _,
            out List<string> nativeToManagedConvertedTypeDestructors
        );

        sb.AppendLine(
            parameterConversionsForInvocation.IndentAllLines(2)
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

        sb.AppendLine($"\t\t\t{invocationPrefix}{managedInvocation};");

        if (hasReturnType) {
            string? returnTypeConversion = returnTypeDescriptor.GetTypeConversion(
                CodeLanguage.CSharp,
                CodeLanguage.CSharpUnmanaged
            );
        
            string convertedReturnValueName = "__returnValueConverted";
        
            if (!string.IsNullOrEmpty(returnTypeConversion)) {
                returnTypeConversion = string.Format(returnTypeConversion, "__returnValue");
                returnTypeConversion = $"var {convertedReturnValueName} = {returnTypeConversion}";
        
                sb.AppendLine($"\t\t\t{returnTypeConversion};");
                sb.AppendLine();

                sb.AppendLine($"\t\t\treturn {convertedReturnValueName};");
            } else {
                sb.AppendLine($"\t\t\treturn {returnValueName};");
            }
        }

        sb.AppendLine("\t\t} catch (Exception __exception) {");
        sb.AppendLine("\t\t\tif (__outException is not null) {");
        sb.AppendLine("\t\t\t\tvoid* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();");
        sb.AppendLine();
        sb.AppendLine("\t\t\t\t*__outException = __exceptionHandleAddress;");
        sb.AppendLine("\t\t\t}");
        sb.AppendLine();
        
        if (hasReturnType) {
            string returnValue = returnTypeDescriptor.GetDefaultValue()
                                 ?? $"default({returnType.GetFullNameOrName()})";

            sb.AppendLine($"\t\t\treturn {returnValue};");
        }

        sb.AppendLine("\t\t}");
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

        #region TypeOf
        WriteTypeOf(
            configuration,
            type,
            sb,
            state
        );
        #endregion TypeOf

        sb.AppendLine();
        
        #region Destructor
        WriteDestructor(
            configuration,
            type,
            sb,
            state
        );
        #endregion Destructor
        
        // TODO: Add to State
    }
}