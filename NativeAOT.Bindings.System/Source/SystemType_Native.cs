using System;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOT.Bindings.System;

internal static unsafe class System_Type
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string TYPE_NAME = nameof(Type);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants

    #region Public APIs
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(Type).AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "GetType")]
    internal static void* GetType(char* typeName)
    {
        string? typeNameDn = InteropUtils.ToDotNetString(typeName);

        if (typeNameDn is null) {
            return null;
        }

        Type? type = Type.GetType(typeNameDn, false);

        if (type is not null) {
            return type.AllocateGCHandleAndGetAddress();
        } else {
            return null;
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Name_Get")]
    internal static char* Name_Get(void* handleAddress)
    {
        Type? instance = InteropUtils.GetInstance<Type>(handleAddress);
        
        if (instance is null) {
            return null;
        }

        char* nameC = instance.Name.CopyToCString();

        return nameC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FullName_Get")]
    internal static char* FullName_Get(void* handleAddress)
    {
        Type? instance = InteropUtils.GetInstance<Type>(handleAddress);

        if (instance is null) {
            return null;
        }

        string? fullName = instance.FullName;

        if (fullName is null) {
            return null;
        }
        
        char* nameC = fullName.CopyToCString();

        return nameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "IsAssignableFrom")]
    internal static CBool IsAssignableFrom(
        void* handleAddress, 
        void* targetTypeHandleAddress
    )
    {
        Type? instance = InteropUtils.GetInstance<Type>(handleAddress);

        if (instance is null) {
            return CBool.False;
        }
        
        Type? targetType = InteropUtils.GetInstance<Type>(targetTypeHandleAddress);

        if (targetType is null) {
            return CBool.False;
        }

        bool result = instance.IsAssignableFrom(targetType);

        return result.ToCBool();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "IsAssignableTo")]
    internal static CBool IsAssignableTo(
        void* handleAddress, 
        void* targetTypeHandleAddress
    )
    {
        Type? instance = InteropUtils.GetInstance<Type>(handleAddress);

        if (instance is null) {
            return CBool.False;
        }
        
        Type? targetType = InteropUtils.GetInstance<Type>(targetTypeHandleAddress);

        if (targetType is null) {
            return CBool.False;
        }

        bool result = instance.IsAssignableTo(targetType);

        return result.ToCBool();
    }
    #endregion Public API
}