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
        GCHandle? handle = InteropUtils.GetGCHandle(handleAddress);
        object? instance = handle?.Target;
        Type? type = instance?.GetType();

        if (type == null) {
            return null;
        }

        void* nativeType = System_Type.Create(type);

        return nativeType;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Equals")]
    internal static CBool Equals(void* firstHandleAddress, void* secondHandleAddress)
    {
        GCHandle? firstHandle = InteropUtils.GetGCHandle(firstHandleAddress);
        object? firstObject = firstHandle?.Target;
        
        GCHandle? secondHandle = InteropUtils.GetGCHandle(secondHandleAddress);
        object? secondObject = secondHandle?.Target;

        bool equals = firstObject == secondObject;

        return equals.ToCBool();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Destroy")]
    internal static void Destroy(void* handleAddress)
    {
        GCHandle? instance = InteropUtils.GetGCHandle(handleAddress);

        instance?.FreeIfAllocated();
    }
}