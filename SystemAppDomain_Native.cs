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
    
    #region Helpers
    internal static unsafe AppDomain? GetInstanceFromHandleAddress(void* handleAddress)
    {
        if (handleAddress == null) {
            return null;
        }
        
        GCHandle? handle = InteropUtils.GetGCHandle(handleAddress);
        AppDomain? instance = handle?.Target as AppDomain;

        return instance;
    }

    internal static unsafe void* AllocateHandleAndGetAddress(this AppDomain instance)
    {
        GCHandle handle = instance.AllocateGCHandle(GCHandleType.Normal);
        void* handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }
    
    internal static unsafe void* Create(AppDomain? instance)
    {
        if (instance == null) {
            return null;
        }
        
        void* handleAddress = AllocateHandleAndGetAddress(instance);

        return handleAddress;
    }
    #endregion Helpers

    #region Public API
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Id_Get")]
    internal static unsafe int Id_Get(void* handleAddress)
    {
        AppDomain? instance = GetInstanceFromHandleAddress(handleAddress);

        if (instance == null) {
            return (int)CStatus.Failure;
        }

        return instance.Id;
    }
    #endregion Public API
}