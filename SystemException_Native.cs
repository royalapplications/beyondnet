using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class System_Exception
{
    internal static nint Create(Exception? exception)
    {
        if (exception == null) {
            return nint.Zero;
        }

        GCHandle handle = InteropUtils.AllocateGCHandle(exception, GCHandleType.Normal);
        nint handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }

    internal static nint Message_Get(nint handleAddress)
    {
        // TODO
        return nint.Zero;
    }
}