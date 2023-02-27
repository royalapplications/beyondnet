using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal enum CStatus: int
{
    Success = 1,
    Failure = -1
}

internal enum CBool: int
{
    True = 1,
    False = 0
}

internal static unsafe class InteropUtils 
{
    internal static GCHandle AllocateGCHandle(this object @object, GCHandleType handleType)
    {
        GCHandle handle = GCHandle.Alloc(@object, handleType);

        return handle;
    }

    internal static void FreeIfAllocated(this GCHandle handle)
    {
        if (!handle.IsAllocated) {
            return;
        }

        handle.Free();
    }

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

    internal static CBool ToCBool(this bool @bool)
    {
        if (@bool) {
            return CBool.True;
        } else {
            return CBool.False;
        }
    }
}