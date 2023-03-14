using System;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOT.Bindings.System;

internal static unsafe class System_Guid_t
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string TYPE_NAME = nameof(Guid);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants

    #region Public API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(Guid).AllocateGCHandleAndGetAddress();
    }

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "Create")]
    internal static void* Create()
    {
        Guid instance = new();

        return instance.AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "Create1")]
    internal static void* Create1(byte* @string)
    {
        string? stringDotNet = InteropUtils.ToDotNetString(@string);
        
        Guid instance = new(stringDotNet);

        return instance.AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "ToString")]
    internal static byte* ToString(void* handleAddress)
    {
        Guid instance = InteropUtils.GetInstance<Guid>(handleAddress);

        byte* stringC = instance.ToString().CopyToCString();

        return stringC;
    }
    #endregion Public API
}