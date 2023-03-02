using System.Runtime.CompilerServices;

namespace NativeAOT.Core;

#if NETSTANDARD2_0 ||  NETCOREAPP2_0 ||  NETCOREAPP2_1 ||  NETCOREAPP2_2 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
public static class ConditionalWeakTable_Extensions
{
    public static void AddOrUpdate<TKey, TValue>(
        this ConditionalWeakTable<TKey, TValue> conditionalWeakTable,
        TKey key,
        TValue value
    ) 
        where TKey: class
        where TValue: class
    {
        conditionalWeakTable.Remove(key);
        conditionalWeakTable.Add(key, value);
    }
}
#endif
