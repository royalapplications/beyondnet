using System;
using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class System_AppDomain
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string CLASS_NAME = nameof(AppDomain);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants
    
    #region Public API
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Id_Get")]
    internal static unsafe int Id_Get(void* handleAddress)
    {
        AppDomain? instance = InteropUtils.GetInstance<AppDomain>(handleAddress);

        if (instance == null) {
            return (int)CStatus.Failure;
        }

        return instance.Id;
    }
    #endregion Public API
}