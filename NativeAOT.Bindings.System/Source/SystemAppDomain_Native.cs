using System.Collections.Concurrent;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOT.Bindings.System;

internal static unsafe class System_AppDomain
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string TYPE_NAME = nameof(AppDomain);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants
    
    #region Public API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(AppDomain).AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "CurrentDomain_Get")]
    internal static void* CurrentDomain_Get()
    {
        AppDomain instance = AppDomain.CurrentDomain;
        void* handleAddress = instance.AllocateGCHandleAndGetAddress();

        return handleAddress;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Id_Get")]
    internal static int Id_Get(void* handleAddress)
    {
        AppDomain? instance = InteropUtils.GetInstance<AppDomain>(handleAddress);

        if (instance == null) {
            return (int)CStatus.Failure;
        }

        return instance.Id;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "IsDefaultAppDomain")]
    internal static CBool IsDefaultAppDomain(void* handleAddress)
    {
        AppDomain? instance = InteropUtils.GetInstance<AppDomain>(handleAddress);

        if (instance == null) {
            return CBool.False;
        }

        return instance.IsDefaultAppDomain().ToCBool();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "BaseDirectory_Get")]
    internal static char* BaseDirectory(void* handleAddress)
    {
        AppDomain? instance = InteropUtils.GetInstance<AppDomain>(handleAddress);

        if (instance == null) {
            return null;
        }

        string baseDirectory = instance.BaseDirectory;
        char* baseDirectoryC = baseDirectory.ToCString();

        return baseDirectoryC;
    }
    
    private class UnhandledExceptionHandler_Native
    {
        internal UnhandledExceptionEventHandler Trampoline { get; }
        internal void* Context { get; }
        internal delegate* unmanaged<void*, void*, void*, void> FunctionPointer { get; }

        internal UnhandledExceptionHandler_Native(
            UnhandledExceptionEventHandler trampoline,
            void* context,
            delegate* unmanaged<void*, void*, void*, void> functionPointer
        )
        {
            Trampoline = trampoline;
            Context = context;
            FunctionPointer = functionPointer;
        }
    }
    
    private static readonly ConcurrentDictionary<AppDomain, UnhandledExceptionHandler_Native[]> m_unhandledExceptionHandlersNative = new();
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "UnhandledException_Add")]
    internal static void UnhandledException_Add(
        void* handleAddress,
        void* context,
        delegate* unmanaged<void*, void*, void*, void> functionPointer
    )
    {
        AppDomain? instance = InteropUtils.GetInstance<AppDomain>(handleAddress);

        if (instance == null) {
            return;
        }

        if ((nint)functionPointer == nint.Zero) {
            return;
        }

        List<UnhandledExceptionHandler_Native> newNativeHandlers = m_unhandledExceptionHandlersNative.TryGetValue(instance, out UnhandledExceptionHandler_Native[]? currentNativeHandlers)
            ? currentNativeHandlers.ToList()
            : new();

        void Trampoline(object sender, UnhandledExceptionEventArgs eventArgs) 
        {
            void* senderHandleAddress = sender.AllocateGCHandleAndGetAddress();
            void* eventArgsHandleAddress = eventArgs.AllocateGCHandleAndGetAddress();

            functionPointer(context, senderHandleAddress, eventArgsHandleAddress);
        }
            
        newNativeHandlers.Add(new UnhandledExceptionHandler_Native(
            Trampoline,
            context,
            functionPointer
        ));

        m_unhandledExceptionHandlersNative[instance] = newNativeHandlers.ToArray();

        instance.UnhandledException += Trampoline;
    }

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "UnhandledException_Remove")]
    internal static CStatus UnhandledException_Remove(
        void* handleAddress,
        void* context,
        delegate* unmanaged<void*, void*, void*, void> functionPointer
    )
    {
        AppDomain? instance = InteropUtils.GetInstance<AppDomain>(handleAddress);

        if (instance == null) {
            return CStatus.Failure;
        }

        if ((nint)functionPointer == nint.Zero) {
            return CStatus.Failure;
        }

        if (!m_unhandledExceptionHandlersNative.TryGetValue(instance, out UnhandledExceptionHandler_Native[]? currentNativeHandlers)) {
            return CStatus.Failure;
        }
        
        UnhandledExceptionHandler_Native? nativeHandler = currentNativeHandlers.FirstOrDefault(h => 
            h.FunctionPointer == functionPointer && 
            h.Context == context
        );

        if (nativeHandler == null) {
            return CStatus.Failure;
        }

        UnhandledExceptionEventHandler trampoline = nativeHandler.Trampoline;

        instance.UnhandledException -= trampoline;

        return CStatus.Success;
    }
    #endregion Public API
}