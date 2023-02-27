using System;
using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static unsafe class System_Exception
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string TYPE_NAME = nameof(Exception);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants
    
    #region Public API
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Message_Get")]
    internal static char* Message_Get(void* handleAddress)
    {
        Exception? instance = InteropUtils.GetInstance<Exception>(handleAddress);

        if (instance == null) {
            return null;
        }
        
        char* messageC = instance.Message.ToCString();

        return messageC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "HResult_Get")]
    internal static int HResult_Get(void* handleAddress)
    {
        Exception? instance = InteropUtils.GetInstance<Exception>(handleAddress);

        if (instance == null) {
            return 0;
        }

        return instance.HResult;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "StackTrace_Get")]
    internal static char* StackTrace_Get(void* handleAddress)
    {
        Exception? instance = InteropUtils.GetInstance<Exception>(handleAddress);

        if (instance == null) {
            return null;
        }

        string? stackTrace = instance.StackTrace;

        if (stackTrace == null) {
            return null;
        }
        
        char* stackTraceC = stackTrace.ToCString();

        return stackTraceC;
    }
    #endregion Public API
}