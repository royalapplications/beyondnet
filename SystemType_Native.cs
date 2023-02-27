using System;
using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static unsafe class System_Type
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string CLASS_NAME = nameof(Type);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Name_Get")]
    internal static char* Name_Get(void* handleAddress)
    {
        Type? type = InteropUtils.GetInstance<Type>(handleAddress);

        if (type == null) {
            return null;
        }

        char* nameC = type.Name.ToCString();

        return nameC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FullName_Get")]
    internal static char* FullName_Get(void* handleAddress)
    {
        Type? type = InteropUtils.GetInstance<Type>(handleAddress);

        if (type == null) {
            return null;
        }

        string? fullName = type.FullName;

        if (fullName == null) {
            return null;
        }
        
        char* nameC = fullName.ToCString();

        return nameC;
    }
}