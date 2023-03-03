using System.Collections.Immutable;
using NativeAOT.Core;

namespace NativeAOT.CodeGenerator;

public class ExportedType
{
    public Type ManagedType { get; }
    public NativeExport NativeExportAttribute { get; }
    
    // public ImmutableDictionary<Language, string> ExportedTypeNames { get; }

    public ExportedType(
        Type managedType,
        NativeExport nativeExportAttribute
    )
    {
        ManagedType = managedType;
        NativeExportAttribute = nativeExportAttribute;
    }
}