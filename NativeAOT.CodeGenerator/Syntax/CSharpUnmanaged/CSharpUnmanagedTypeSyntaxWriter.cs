using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Collectors;
using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedTypeSyntaxWriter: ICSharpUnmanagedSyntaxWriter, ITypeSyntaxWriter
{
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, ICSharpUnmanagedSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new CSharpUnmanagedConstructorSyntaxWriter() },
        { MemberTypes.Property, new CSharpUnmanagedPropertySyntaxWriter() },
        { MemberTypes.Method, new CSharpUnmanagedMethodSyntaxWriter() },
        { MemberTypes.Field, new CSharpUnmanagedFieldSyntaxWriter() },
        { MemberTypes.Event, new CSharpUnmanagedEventSyntaxWriter() }
    };

    private CSharpUnmanagedDestructorSyntaxWriter m_destructorSyntaxWriter = new();

    public CSharpUnmanagedTypeSyntaxWriter(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }
    
    public string Write(Type type, State state)
    {
        if (type.IsPrimitive ||
            type.IsEnum ||
            type.IsPointer ||
            type.IsByRef) {
            // No need to generate C# Unmanaged code for those kinds of types

            return string.Empty;
        }
        
        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            return $"// Type \"{type.Name}\" was skipped. Reason: It has no full name.";
        }
        
        string cTypeName = fullTypeName.CTypeName();

        bool isDelegate = type.IsDelegate();
        
        MethodInfo? delegateInvokeMethod = isDelegate
            ? type.GetDelegateInvokeMethod()
            : null;
        
        StringBuilder sb = new();
        
        sb.AppendLine($"internal unsafe class {cTypeName}");
        sb.AppendLine("{");

        if (type.IsDelegate()) {
            WriteDelegateType(
                type,
                fullTypeName,
                cTypeName,
                delegateInvokeMethod!,
                sb,
                state
            );
        } else {
            WriteRegularType(
                type,
                sb,
                state
            );
        }

        sb.AppendLine("}");
        sb.AppendLine();

        return sb.ToString();
    }

    private void WriteRegularType(
        Type type,
        StringBuilder sb,
        State state
    )
    {
        var memberCollector = new MemberCollector(type);
        var members = memberCollector.Collect(out Dictionary<MemberInfo, string> unsupportedMembers);

        if (Settings.EmitUnsupported) {
            foreach (var kvp in unsupportedMembers) {
                MemberInfo member = kvp.Key;
                string reason = kvp.Value;
    
                string memberName = member.Name;
    
                sb.AppendLine($"\t// Unsupported Member \"{memberName}\": {reason}");
                sb.AppendLine();
            }
        }
        
        foreach (var member in members) {
            var memberType = member.MemberType;

            ICSharpUnmanagedSyntaxWriter? syntaxWriter = GetSyntaxWriter(
                MemberKind.Automatic,
                memberType
            );

            if (syntaxWriter == null) {
                if (Settings.EmitUnsupported) {
                    sb.AppendLine($"\t// TODO: Unsupported Member Type \"{memberType}\"");
                }
                    
                continue;
            }

            string memberCode = syntaxWriter.Write(member, state)
                .IndentAllLines(1);

            sb.AppendLine(memberCode);
        }
        
        WriteDestructor(
            type,
            sb,
            state
        );
    }

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
        sb.AppendLine($"\tpublic {contextType} Context {{ get; }}");
        sb.AppendLine($"\tpublic {cFunctionSignature} CFunction {{ get; }}");
        sb.AppendLine($"\tpublic {cDestructorFunctionSignature} CDestructorFunction {{ get; }}");

        #endregion Properties
        sb.AppendLine();

        #region Constructor
        sb.AppendLine($"\tprivate {cTypeName}({contextType} context, {cFunctionSignature} cFunction, {cDestructorFunctionSignature} cDestructorFunction)");
        sb.AppendLine("\t{");

        sb.AppendLine("\t\tContext = context;");
        sb.AppendLine("\t\tCFunction = cFunction;");
        sb.AppendLine("\t\tCDestructorFunction = cDestructorFunction;");

        sb.AppendLine("\t}");
        #endregion Constructor

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
        sb.AppendLine($"\tinternal {fullTypeName}? CreateTrampoline()");
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

        string invocationPrefix = returnType.IsVoid()
            ? string.Empty
            : "return ";

        string? returnTypeConversion = returnTypeDescriptor.GetTypeConversion(
            CodeLanguage.CSharpUnmanaged,
            CodeLanguage.CSharp
        );

        string invocationWithoutReturnTypeConversion = $"CFunction(Context{parameterNamesString})";

        string invocationWithReturnTypeConversion;

        if (!string.IsNullOrEmpty(returnTypeConversion)) {
            invocationWithReturnTypeConversion = string.Format(
                returnTypeConversion,
                invocationWithoutReturnTypeConversion
            );
        } else {
            invocationWithReturnTypeConversion = invocationWithoutReturnTypeConversion;
        }

        sb.AppendLine($"\t\t{invocationPrefix}{invocationWithReturnTypeConversion};");

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

        #region Context Get
        // TODO
        #endregion Context Get

        #region CFunction Get
        // TODO
        #endregion CFunction Get

        #region CDestructorFunction Get
        // TODO
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

    private void WriteDestructor(
        Type type,
        StringBuilder sb,
        State state
    )
    {
        ICSharpUnmanagedSyntaxWriter? destructorSyntaxWriter = GetSyntaxWriter(
            MemberKind.Destructor,
            MemberTypes.Custom
        );

        if (destructorSyntaxWriter == null) {
            throw new Exception("No destructor syntax writer");
        }

        string destructorCode = destructorSyntaxWriter.Write(type, state)
            .IndentAllLines(1);

        sb.AppendLine(destructorCode);
    }

    private ICSharpUnmanagedSyntaxWriter? GetSyntaxWriter(
        MemberKind memberKind,
        MemberTypes memberType
    )
    {
        if (memberKind == MemberKind.Destructor) {
            return m_destructorSyntaxWriter;
        }

        m_syntaxWriters.TryGetValue(
            memberType,
            out ICSharpUnmanagedSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
}