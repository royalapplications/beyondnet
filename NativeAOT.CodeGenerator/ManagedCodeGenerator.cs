using System.Reflection;
using System.Text;
using NativeAOT.Core;

namespace NativeAOT.CodeGenerator;

public class ManagedCodeGenerator
{
    private readonly Type m_type;

    public ManagedCodeGenerator(Type type)
    {
        m_type = type;
    }

    public string Generate()
    {
        StringBuilder sb = new();

        string generatedNamespace = $"{m_type.Namespace}.NativeBindings";
        string cTypeName = m_type.FullName.Replace(".", "_");

        sb.AppendLine($"""
using System;
using System.Runtime.InteropServices;
using NativeAOT.Core;

namespace {generatedNamespace};

""");

        sb.AppendLine($"internal static unsafe class {cTypeName}");
        sb.AppendLine("{");

        foreach (var memberInfo in m_type.GetMembers()) {
            var memberType = memberInfo.MemberType;

            switch (memberType) {
                case MemberTypes.Constructor:
                    string constructorNameC = "Create";
                    
                    sb.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{constructorNameC}\")]");
                    sb.AppendLine($"\tinternal static void* {constructorNameC}()");
                    sb.AppendLine("\t{");
                    sb.AppendLine($"\t\t{m_type.FullName} instance = new();");
                    sb.AppendLine("\t\treturn instance.AllocateGCHandleAndGetAddress();");
                    sb.AppendLine("\t}");
                    
                    break;
                case MemberTypes.Method:
                    MethodInfo? methodInfo = (MethodInfo)memberInfo;
                    
                    string methodNameC = methodInfo.Name;
                    
                    Type returnType = methodInfo.ReturnType;
                    WellKnownType? wellKnownReturnType = WellKnownType.GetWellKnownType(returnType);
                    
                    sb.AppendLine($"\t// TODO: {methodNameC}");

                    if (wellKnownReturnType != null) {
                        sb.AppendLine($"\t// Well known return type: {wellKnownReturnType.ExportedTypeNames[CodeLanguage.CSharpUnmanaged]}");
                    }

                    break;
                default:
                    throw new NotSupportedException("Unknown member type");
            }
        }
        
        sb.AppendLine("}");
        
        return sb.ToString();
    }
}