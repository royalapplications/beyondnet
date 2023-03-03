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

    public List<Type> CollectPublicTypes()
    {
        var allTypeInfos = m_assembly.DefinedTypes;

        List<Type> publicTypes = new();
        
        foreach (var typeInfo in allTypeInfos) {
            Type type = typeInfo.AsType();

            if (!type.IsVisible) {
                continue;
            }
            
            publicTypes.Add(type);
        }

        return publicTypes;
    }
}