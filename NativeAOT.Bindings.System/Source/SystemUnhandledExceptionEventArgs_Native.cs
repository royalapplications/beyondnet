using System;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOT.Bindings.System;

internal static unsafe class SystemUnhandledExceptionEventArgs_Native
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string TYPE_NAME = nameof(UnhandledExceptionEventArgs);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants

    #region Public API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(UnhandledExceptionEventArgs).AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "ExceptionObject_Get")]
    internal static void* ExceptionObject_Get(void* handleAddress)
    {
        UnhandledExceptionEventArgs? instance = InteropUtils.GetInstance<UnhandledExceptionEventArgs>(handleAddress);

        if (instance == null) {
            return null;
        }

        object exceptionObject = instance.ExceptionObject;
        void* exceptionObjectHandleAddress = exceptionObject.AllocateGCHandleAndGetAddress();

        return exceptionObjectHandleAddress;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "IsTerminating_Get")]
    internal static CBool IsTerminating_Get(void* handleAddress)
    {
        UnhandledExceptionEventArgs? instance = InteropUtils.GetInstance<UnhandledExceptionEventArgs>(handleAddress);

        if (instance == null) {
            return CBool.False;
        }

        return instance.IsTerminating.ToCBool();
    }
    #endregion Public API
}