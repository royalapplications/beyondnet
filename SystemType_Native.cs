using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class System_Type
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string CLASS_NAME = nameof(Type);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants

    #region Helpers
    internal static Type? GetTypeFromHandleAddress(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();
        Type? @object = handle?.Target as Type;

        return @object;
    }
    #endregion Helpers

    internal static nint Create(Type type)
    {
        GCHandle handle = type.AllocateGCHandle(GCHandleType.Normal);
        nint handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Name_Get")]
    internal static nint Name_Get(nint handleAddress)
    {
        Type? type = GetTypeFromHandleAddress(handleAddress);

        if (type == null) {
            return nint.Zero;
        }

        nint nameC = type.Name.ToCString();

        return nameC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FullName_Get")]
    internal static nint FullName_Get(nint handleAddress)
    {
        Type? type = GetTypeFromHandleAddress(handleAddress);

        if (type == null) {
            return nint.Zero;
        }

        nint nameC = type.FullName?.ToCString() ?? nint.Zero;

        return nameC;
    }
}