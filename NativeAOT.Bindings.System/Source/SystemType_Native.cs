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

        if (typeNameDn == null) {
            return null;
        }

        Type? type = Type.GetType(typeNameDn, false);

        if (type != null) {
            return type.AllocateGCHandleAndGetAddress();
        } else {
            return null;
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Name_Get")]
    internal static char* Name_Get(void* handleAddress)
    {
        Type? instance = InteropUtils.GetInstance<Type>(handleAddress);
        
        if (instance == null) {
            return null;
        }

        char* nameC = instance.Name.ToCString();

        return nameC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FullName_Get")]
    internal static char* FullName_Get(void* handleAddress)
    {
        Type? instance = InteropUtils.GetInstance<Type>(handleAddress);

        if (instance == null) {
            return null;
        }

        string? fullName = instance.FullName;

        if (fullName == null) {
            return null;
        }
        
        char* nameC = fullName.ToCString();

        return nameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "IsAssignableFrom")]
    internal static CBool IsAssignableFrom(
        void* handleAddress, 
        void* targetTypeHandleAddress
    )
    {
        Type? instance = InteropUtils.GetInstance<Type>(handleAddress);

        if (instance == null) {
            return false.ToCBool();
        }
        
        Type? targetType = InteropUtils.GetInstance<Type>(targetTypeHandleAddress);

        if (targetType == null) {
            return false.ToCBool();
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

        if (instance == null) {
            return false.ToCBool();
        }
        
        Type? targetType = InteropUtils.GetInstance<Type>(targetTypeHandleAddress);

        if (targetType == null) {
            return false.ToCBool();
        }

        bool result = instance.IsAssignableTo(targetType);

        return result.ToCBool();
    }
    #endregion Public API
}