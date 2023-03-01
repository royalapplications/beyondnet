using System;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOTSample;

internal static unsafe class NativeAOTSample_UnhandledExceptionTest_t
{
    #region Constants
    private const string NAMESPACE = nameof(NativeAOTSample);
    private const string TYPE_NAME = "UnhandledExceptionTest";
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants

    #region Public API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(NativeAOTSample_UnhandledExceptionTest_t).AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "ThrowUnhandledException")]
    internal static void ThrowUnhandledException()
    {
        Console.WriteLine("Will throw an unhandled exception");
        
        throw new Exception("Oh no!");
    }
    #endregion Public API
}