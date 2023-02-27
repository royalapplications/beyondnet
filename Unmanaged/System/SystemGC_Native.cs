using System;
using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class System_GC
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string TYPE_NAME = nameof(GC);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants
    
    #region Public API
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Collect")]
    internal static void Collect()
    {
        GC.Collect();
    }
    #endregion Public API
}