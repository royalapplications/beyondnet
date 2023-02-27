using System;
using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static unsafe class System_Object
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string CLASS_NAME = nameof(Object);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "GetType")]
    internal static void* GetType(void* handleAddress)
    {
        object? instance = InteropUtils.GetInstance<object>(handleAddress);
        Type? type = instance?.GetType();

        if (type == null) {
            return null;
        }

        void* nativeType = type.AllocateGCHandleAndGetAddress();

        return nativeType;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Equals")]
    internal static CBool Equals(void* firstHandleAddress, void* secondHandleAddress)
    {
        object? firstObject = InteropUtils.GetInstance<object>(firstHandleAddress);
        object? secondObject = InteropUtils.GetInstance<object>(secondHandleAddress);

        bool equals = firstObject == secondObject;

        return equals.ToCBool();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Destroy")]
    internal static void Destroy(void* handleAddress)
    {
        InteropUtils.FreeIfAllocated(handleAddress);
    }
}