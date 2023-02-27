using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class UnhandledExceptionTest_Native
{
    #region Constants
    private const string NAMESPACE = nameof(NativeAOTLibraryTest);
    private const string TYPE_NAME = "UnhandledExceptionTest";
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "ThrowUnhandledException")]
    internal static void ThrowUnhandledException()
    {
        throw new Exception("Oh no!");
    }
}