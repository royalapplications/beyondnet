using System;
using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class System_Exception
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string CLASS_NAME = nameof(Exception);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants
    
    #region Helpers
    internal static Exception? GetExceptionFromHandleAddress(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();
        Exception? @object = handle?.Target as Exception;

        return @object;
    }

    internal static nint AllocateHandleAndGetAddress(this Exception @object)
    {
        GCHandle handle = @object.AllocateGCHandle(GCHandleType.Normal);
        nint handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }
    
    internal static nint Create(Exception? exception)
    {
        if (exception == null) {
            return nint.Zero;
        }
        
        nint handleAddress = AllocateHandleAndGetAddress(exception);

        return handleAddress;
    }
    #endregion Helpers

    #region Public API
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Message_Get")]
    internal static nint Message_Get(nint handleAddress)
    {
        Exception? exception = GetExceptionFromHandleAddress(handleAddress);

        if (exception == null) {
            return nint.Zero;
        }
        
        nint messageC = exception.Message.ToCString();

        return messageC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "HResult_Get")]
    internal static int HResult_Get(nint handleAddress)
    {
        Exception? exception = GetExceptionFromHandleAddress(handleAddress);

        if (exception == null) {
            return 0;
        }

        return exception.HResult;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "StackTrace_Get")]
    internal static nint StackTrace_Get(nint handleAddress)
    {
        Exception? exception = GetExceptionFromHandleAddress(handleAddress);

        if (exception == null) {
            return nint.Zero;
        }
        
        nint stackTraceC = exception.StackTrace?.ToCString() ?? nint.Zero;

        return stackTraceC;
    }
    #endregion Public API
}