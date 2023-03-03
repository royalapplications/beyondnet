using NativeAOT.Core;

namespace NativeAOT.CodeGenerator;

public class WellKnownType
{
    private static WellKnownType[] m_types = new[] {
        new WellKnownType(typeof(void), new() {
            { CodeLanguage.CSharpUnmanaged, "void" },
            { CodeLanguage.C, "void" },
            { CodeLanguage.Swift, "Void" }
        }),
        
        new WellKnownType(typeof(int), new() {
            { CodeLanguage.CSharpUnmanaged, "int" },
            { CodeLanguage.C, "int32_t" },
            { CodeLanguage.Swift, "Int32" }
        }),
        
        new WellKnownType(typeof(string), new() {
            { CodeLanguage.CSharpUnmanaged, "byte*" },
            { CodeLanguage.C, "char*" },
            { CodeLanguage.Swift, "String" }
        })
    };

    public Type ManagedType { get; }
    public Dictionary<CodeLanguage, string> ExportedTypeNames { get; }

    public WellKnownType(Type managedType, Dictionary<CodeLanguage, string> exportedTypeNames)
    {
        ManagedType = managedType;
        ExportedTypeNames = exportedTypeNames;
    }
    
    public static WellKnownType? GetWellKnownType(Type type)
    {
        WellKnownType? wellKnownType = m_types.FirstOrDefault(
            t => t.ManagedType == type
        );

        return wellKnownType;
    }
}