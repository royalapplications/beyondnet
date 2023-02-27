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

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "CurrentDomain_Get")]
    internal static unsafe void* CurrentDomain_Get()
    {
        AppDomain instance = AppDomain.CurrentDomain;
        void* handleAddress = instance.AllocateGCHandleAndGetAddress();

        return handleAddress;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Id_Get")]
    internal static unsafe int Id_Get(void* handleAddress)
    {
        AppDomain? instance = InteropUtils.GetInstance<AppDomain>(handleAddress);

        if (instance == null) {
            return (int)CStatus.Failure;
        }

        return instance.Id;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "IsDefaultAppDomain")]
    internal static unsafe CBool IsDefaultAppDomain(void* handleAddress)
    {
        AppDomain? instance = InteropUtils.GetInstance<AppDomain>(handleAddress);

        if (instance == null) {
            return CBool.False;
        }

        return instance.IsDefaultAppDomain().ToCBool();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "BaseDirectory_Get")]
    internal static unsafe char* BaseDirectory(void* handleAddress)
    {
        AppDomain? instance = InteropUtils.GetInstance<AppDomain>(handleAddress);

        if (instance == null) {
            return null;
        }

        string baseDirectory = instance.BaseDirectory;
        char* baseDirectoryC = baseDirectory.ToCString();

        return baseDirectoryC;
    }
    #endregion Public API
}