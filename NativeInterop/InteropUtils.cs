using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static unsafe class InteropUtils
{
    #region Allocation
    internal static GCHandle AllocateGCHandle(this object instance, GCHandleType handleType)
    {
        GCHandle handle = GCHandle.Alloc(instance, handleType);

        return handle;
    }
    
    internal static void* AllocateGCHandleAndGetAddress(this object instance)
    {
        GCHandle handle = instance.AllocateGCHandle(GCHandleType.Normal);
        void* handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }
    #endregion Allocation

    #region Free
    internal static void FreeIfAllocated(void* handleAddress)
    {
        if (handleAddress == null) {
            return;
        }

        GCHandle? handle = GetGCHandle(handleAddress);
        
        handle?.FreeIfAllocated();
    }
    
    internal static void FreeIfAllocated(this GCHandle handle)
    {
        if (!handle.IsAllocated) {
            return;
        }

        handle.Free();
    }
    #endregion Free

    #region Handle Address/GCHandle <-> Object Conversion
    internal static void* ToHandleAddress(this GCHandle handle)
    {
        void* handleAddress = (void*)GCHandle.ToIntPtr(handle);

        return handleAddress;
    }

    internal static GCHandle? GetGCHandle(void* handleAddress)
    {
        if (handleAddress == null) {
            return null;
        }
        
        GCHandle handle = GCHandle.FromIntPtr((nint)handleAddress);

        return handle;
    }

    internal static T? GetInstance<T>(void* handleAddress) where T : class
    {
        GCHandle? handle = GetGCHandle(handleAddress);

        T? instance = handle?.Target as T;

        return instance;
    }
    #endregion Handle Address/GCHandle <-> Object Conversion

    #region Strings
    internal static char* ToCString(this string? @string)
    {
        if (@string == null) {
            return null;
        }
        
        nint cString = Marshal.StringToHGlobalAuto(@string);

        if (cString == nint.Zero) {
            return null;
        }

        return (char*)cString;
    }

    internal static string? ToDotNetString(char* cString)
    {
        if (cString == null) {
            return null;
        }
        
        string? @string = Marshal.PtrToStringAuto((nint)cString);

        return @string;
    }
    #endregion Strings

    #region Bools
    internal static CBool ToCBool(this bool @bool)
    {
        if (@bool) {
            return CBool.True;
        } else {
            return CBool.False;
        }
    }
    #endregion Bools
}