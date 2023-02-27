using System;
using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static unsafe class System_Exception
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string CLASS_NAME = nameof(Exception);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants
    
    #region Helpers
    internal static Exception? GetInstanceFromHandleAddress(void* handleAddress)
    {
        GCHandle? handle = InteropUtils.GetGCHandle(handleAddress);
        Exception? instance = handle?.Target as Exception;

        return instance;
    }

    internal static void* AllocateHandleAndGetAddress(this Exception instance)
    {
        GCHandle handle = instance.AllocateGCHandle(GCHandleType.Normal);
        void* handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }
    
    internal static void* Create(Exception? instance)
    {
        if (instance == null) {
            return null;
        }
        
        void* handleAddress = AllocateHandleAndGetAddress(instance);

        return handleAddress;
    }
    #endregion Helpers

    #region Public API
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Message_Get")]
    internal static char* Message_Get(void* handleAddress)
    {
        Exception? exception = GetInstanceFromHandleAddress(handleAddress);

        if (exception == null) {
            return null;
        }
        
        char* messageC = exception.Message.ToCString();

        return messageC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "HResult_Get")]
    internal static int HResult_Get(void* handleAddress)
    {
        Exception? exception = GetInstanceFromHandleAddress(handleAddress);

        if (exception == null) {
            return 0;
        }

        return exception.HResult;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "StackTrace_Get")]
    internal static char* StackTrace_Get(void* handleAddress)
    {
        Exception? exception = GetInstanceFromHandleAddress(handleAddress);

        if (exception == null) {
            return null;
        }

        string? stackTrace = exception.StackTrace;

        if (stackTrace == null) {
            return null;
        }
        
        char* stackTraceC = stackTrace.ToCString();

        return stackTraceC;
    }
    #endregion Public API
}