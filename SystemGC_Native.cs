using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class System_GC
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string CLASS_NAME = nameof(GC);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Collect")]
    internal static void Collect()
    {
        GC.Collect();
    }
}