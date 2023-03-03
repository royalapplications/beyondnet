using System.Reflection;
using System.Text;
using NativeAOT.Core;

namespace NativeAOT.CodeGenerator;

public class ManagedCodeGenerator
{
    private readonly ExportedType m_exportedType;
    
    public ManagedCodeGenerator(ExportedType exportedType)
    {
        m_exportedType = exportedType;
    }

    public string Generate()
    {
        StringBuilder sb = new();

        string generatedNamespace = $"{m_exportedType.ManagedType.Namespace}.NativeBindings";
        string cTypeName = m_exportedType.NativeExportAttribute.CName;

        sb.AppendLine($"""
using System;
using System.Runtime.InteropServices;
using NativeAOT.Core;

namespace {generatedNamespace};

""");

        sb.AppendLine($"internal static unsafe class {cTypeName}");
        sb.AppendLine("{");

        foreach (var memberInfo in m_exportedType.ManagedType.GetMembers()) {
            NativeExport? memberNativeExport = memberInfo.GetCustomAttribute<NativeExport>();

            if (memberNativeExport == null) {
                continue;
            }

            string memberCName = memberNativeExport.CName;
            

            var memberType = memberInfo.MemberType;

            switch (memberType) {
                case MemberTypes.Constructor:
                    sb.AppendLine($"\t[UnmanagedCallersOnly(EntryPoint = \"{memberCName}\")]");
                    sb.AppendLine($"\tinternal static void* {memberCName}()");
                    sb.AppendLine("\t{");
                    sb.AppendLine($"\t\t{m_exportedType.NativeExportAttribute.ManagedType.FullName} instance = new();");
                    sb.AppendLine("\t\treturn instance.AllocateGCHandleAndGetAddress();");
                    sb.AppendLine("\t}");
                    
                    break;
                case MemberTypes.Method:
                    sb.AppendLine($"\t// TODO: {memberCName}");

                    break;
                default:
                    throw new NotSupportedException("Unknown member type");
            }
        }
        
        sb.AppendLine("}");
        
        return sb.ToString();
    }
}