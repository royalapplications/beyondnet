using System;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOT.Bindings.System;

internal static unsafe class System_Exception_t
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string TYPE_NAME = nameof(Exception);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants
    
    #region Public API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(Exception).AllocateGCHandleAndGetAddress();
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Create")]
    internal static void* Create(char* message)
    {
        string? messageDn;

        if (message is not null) {
            messageDn = InteropUtils.ToDotNetString(message);
        } else {
            messageDn = null;
        }
        
        Exception instance = new(messageDn);

        return instance.AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Message_Get")]
    internal static char* Message_Get(void* handleAddress)
    {
        Exception? instance = InteropUtils.GetInstance<Exception>(handleAddress);

        if (instance is null) {
            return null;
        }
        
        char* messageC = instance.Message.CopyToCString();

        return messageC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "HResult_Get")]
    internal static int HResult_Get(void* handleAddress)
    {
        Exception? instance = InteropUtils.GetInstance<Exception>(handleAddress);

        if (instance is null) {
            return 0;
        }

        return instance.HResult;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "StackTrace_Get")]
    internal static char* StackTrace_Get(void* handleAddress)
    {
        Exception? instance = InteropUtils.GetInstance<Exception>(handleAddress);

        if (instance is null) {
            return null;
        }

        string? stackTrace = instance.StackTrace;

        if (stackTrace is null) {
            return null;
        }
        
        char* stackTraceC = stackTrace.CopyToCString();

        return stackTraceC;
    }
    #endregion Public API
}