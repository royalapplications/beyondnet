using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class System_Object
{
    #region Constants
    private const string NAMESPACE = nameof(System);
    private const string CLASS_NAME = nameof(Object);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "GetType")]
    internal static nint GetType(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();
        object? @object = handle?.Target;
        Type? type = @object?.GetType();

        if (type == null) {
            return nint.Zero;
        }

        nint nativeType = System_Type.Create(type);

        return nativeType;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Equals")]
    internal static int Equals(nint firstHandleAddress, nint secondHandleAddress)
    {
        GCHandle? firstHandle = firstHandleAddress.ToGCHandle();
        object? firstObject = firstHandle?.Target;
        
        GCHandle? secondHandle = secondHandleAddress.ToGCHandle();
        object? secondObject = secondHandle?.Target;

        bool equals = firstObject == secondObject;
        int cBool = equals ? (int)CBool.True : (int)CBool.False;
        
        return cBool;
    }
}