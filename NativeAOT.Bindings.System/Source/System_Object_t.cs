using System;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOT.Bindings.System;

internal static unsafe class System_Object_t
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string TYPE_NAME = nameof(Object);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants

    #region Public API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(object).AllocateGCHandleAndGetAddress();
    }

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "Create")]
    internal static void* Create()
    {
        object instance = new();

        return instance.AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Destroy")]
    internal static void Destroy(void* handleAddress)
    {
        InteropUtils.FreeIfAllocated(handleAddress);
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "GetType")]
    internal static void* GetType(void* handleAddress)
    {
        object? instance = InteropUtils.GetInstance<object>(handleAddress);
        Type? type = instance?.GetType();

        if (type is null) {
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

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "ToString")]
    internal static char* ToString(void* handleAddress)
    {
        object? instance = InteropUtils.GetInstance<object>(handleAddress);

        if (instance is null) {
            return null;
        }

        string? @string = instance.ToString();

        if (@string is null) {
            return null;
        }
        
        char* cString = @string.CopyToCString();

        return cString;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "CastAs")]
    internal static void* CastAs(
        void* handleAddress,
        void* targetTypeHandleAddress
    )
    {
        object? instance = InteropUtils.GetInstance<object>(handleAddress);

        if (instance is null) {
            return null;
        }
        
        Type? targetType = InteropUtils.GetInstance<Type>(targetTypeHandleAddress);

        if (targetType is null) {
            return null;
        }

        Type sourceType = instance.GetType();

        if (!sourceType.IsAssignableTo(targetType)) {
            return null;
        }

        return instance.AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Is")]
    internal static CBool Is(
        void* handleAddress,
        void* targetTypeHandleAddress
    )
    {
        object? instance = InteropUtils.GetInstance<object>(handleAddress);

        if (instance is null) {
            return CBool.False;
        }
        
        Type? targetType = InteropUtils.GetInstance<Type>(targetTypeHandleAddress);

        if (targetType is null) {
            return CBool.False;
        }

        Type sourceType = instance.GetType();

        if (!sourceType.IsAssignableTo(targetType)) {
            return CBool.False;
        }

        return CBool.True;
    }
    #endregion Public API
}