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

internal static class InteropUtils 
{
    internal delegate void VoidDelegate();
    internal unsafe delegate void ContextDelegate(void* context);
    
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

    internal static nint ToHandleAddress(this GCHandle handle)
    {
        nint handleAddress = GCHandle.ToIntPtr(handle);

        return handleAddress;
    }

    internal static GCHandle? ToGCHandle(this nint handleAddress)
    {
        if (handleAddress == nint.Zero) {
            return null;
        }

        GCHandle handle = GCHandle.FromIntPtr(handleAddress);

        return handle;
    }

    internal static unsafe char* ToCString(this string? @string)
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

    internal static unsafe string? ToDotNetString(char* cString)
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