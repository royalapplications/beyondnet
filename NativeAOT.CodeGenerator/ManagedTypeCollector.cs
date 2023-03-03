using System.Reflection;
using NativeAOT.Core;

namespace NativeAOT.CodeGenerator;

public class ManagedTypeCollector
{
    private readonly Assembly m_assembly;
    
    public ManagedTypeCollector(Assembly assembly)
    {
        m_assembly = assembly;
    }

    public List<ExportedType> CollectExportedTypes()
    {
        var allTypeInfos = m_assembly.DefinedTypes;

        List<ExportedType> exportedTypes = new();
        
        foreach (var typeInfo in allTypeInfos) {
            Type type = typeInfo.AsType();
            NativeExport? nativeExportAttribute = type.GetCustomAttribute<NativeExport>();

            if (nativeExportAttribute != null) {
                ExportedType exportedType = new(type, nativeExportAttribute);
                
                exportedTypes.Add(exportedType);
            }
        }

        return exportedTypes;
    }
}