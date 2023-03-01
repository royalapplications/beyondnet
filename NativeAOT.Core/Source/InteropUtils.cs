using System.Runtime.InteropServices;

namespace NativeAOT.Core;

public static unsafe class InteropUtils
{
    #region Allocation
    public static GCHandle AllocateGCHandle(this object instance, GCHandleType handleType)
    {
        GCHandle handle = GCHandle.Alloc(instance, handleType);

        return handle;
    }
    
    public static void* AllocateGCHandleAndGetAddress(this object instance)
    {
        GCHandle handle = instance.AllocateGCHandle(GCHandleType.Normal);
        void* handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }
    #endregion Allocation

    #region Free
    public static void FreeIfAllocated(void* handleAddress)
    {
        if (handleAddress is null) {
            return;
        }

        GCHandle? handle = GetGCHandle(handleAddress);
        
        handle?.FreeIfAllocated();
    }
    
    public static void FreeIfAllocated(this GCHandle handle)
    {
        if (!handle.IsAllocated) {
            return;
        }

        handle.Free();
    }
    #endregion Free

    #region Handle Address/GCHandle <-> Object Conversion
    public static void* ToHandleAddress(this GCHandle handle)
    {
        void* handleAddress = (void*)GCHandle.ToIntPtr(handle);

        return handleAddress;
    }

    public static GCHandle? GetGCHandle(void* handleAddress)
    {
        if (handleAddress is null) {
            return null;
        }
        
        GCHandle handle = GCHandle.FromIntPtr((nint)handleAddress);

        return handle;
    }

    public static T? GetInstance<T>(void* handleAddress) where T : class
    {
        GCHandle? handle = GetGCHandle(handleAddress);

        T? instance = handle?.Target as T;

        return instance;
    }
    #endregion Handle Address/GCHandle <-> Object Conversion

    #region Strings
    public static char* ToCString(this string? @string)
    {
        if (@string is null) {
            return null;
        }
        
        char* cString = (char*)Marshal.StringToHGlobalAuto(@string);
        
        return cString;
    }

    public static string? ToDotNetString(char* cString)
    {
        if (cString is null) {
            return null;
        }
        
        string? @string = Marshal.PtrToStringAuto((nint)cString);

        return @string;
    }
    #endregion Strings

    #region Bools
    public static CBool ToCBool(this bool @bool)
    {
        if (@bool) {
            return CBool.True;
        } else {
            return CBool.False;
        }
    }
    #endregion Bools
}