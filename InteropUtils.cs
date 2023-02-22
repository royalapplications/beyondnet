using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class InteropUtils 
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

    internal static nint ToHandleAddress(this GCHandle handle)
    {
        nint handleAddress = GCHandle.ToIntPtr(handle);

        return handleAddress;
    }

    internal static GCHandle? ToGCHandle(this nint handleAddress)
    {
        if (handleAddress == IntPtr.Zero) {
            return null;
        }

        GCHandle handle = GCHandle.FromIntPtr(handleAddress);

        return handle;
    }

    internal static nint ToCString(this string? @string)
    {
        nint cString = Marshal.StringToHGlobalAuto(@string);

        return cString;
    }

    internal static string? ToDotNetString(this nint cString)
    {
        string? @string = Marshal.PtrToStringAuto(cString);

        return @string;
    }
}