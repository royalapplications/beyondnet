namespace Beyond.NET.CodeGenerator.Generator.CSharpUnmanaged;

internal static class CSharpUnmanagedSharedCode
{
    internal const string SharedCode = /*lang=C#*/"""
internal class __BeyondNETNativeModuleInitializer
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    internal static unsafe void BeyondNETNativeModuleInitializer()
    {
        // TODO: We could probably remove the native implementation if we port it to C# by using DllImport's for CoreFoundation stuff
         
        const string dnLibraryInitFuncName = "_DNLibraryInit";

        var selfHandle = System.Runtime.InteropServices.NativeLibrary.GetMainProgramHandle();

        if (selfHandle == IntPtr.Zero) {
            return;
        }

        bool getExportSuccess = System.Runtime.InteropServices.NativeLibrary.TryGetExport(
            selfHandle,
            dnLibraryInitFuncName,
            out IntPtr dnLibraryInitSymbol
        );

        if (!getExportSuccess ||
            dnLibraryInitSymbol == IntPtr.Zero) {
            return;
        }
    
        delegate* unmanaged<void> dnLibraryInitFunc = (delegate* unmanaged<void>)dnLibraryInitSymbol;

        dnLibraryInitFunc();
    }
}

internal unsafe class NativeDelegateBox<TDelegateType, TFunctionPointerType>
    where TDelegateType: Delegate
    where TFunctionPointerType: unmanaged
{
    internal TDelegateType Trampoline { get; }
    internal void* Context { get; }
    internal TFunctionPointerType FunctionPointer { get; }

    internal NativeDelegateBox(
        TDelegateType trampoline,
        void* context,
        TFunctionPointerType? functionPointer
    )
    {
        Trampoline = trampoline ?? throw new ArgumentNullException(nameof(trampoline));
        Context = context is not null ? context : throw new ArgumentNullException(nameof(context));
        FunctionPointer = functionPointer ?? throw new ArgumentNullException(nameof(functionPointer));
    }
}

internal static unsafe class InteropUtils
{
    #region Allocation
    internal static GCHandle AllocateGCHandle(this object instance, GCHandleType handleType)
    {
        GCHandle handle = GCHandle.Alloc(instance, handleType);

        return handle;
    }
    
    internal static void* AllocateGCHandleAndGetAddress(this object? instance)
    {
        if (instance is null) {
            return null;
        }
        
        GCHandle handle = instance.AllocateGCHandle(GCHandleType.Normal);
        void* handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }
    #endregion Allocation

    #region Free
    internal static void FreeIfAllocated(void* handleAddress)
    {
        if (handleAddress is null) {
            return;
        }

        GCHandle? handle = GetGCHandle(handleAddress);
        
        handle?.FreeIfAllocated();
    }

    internal static void CheckedFreeIfAllocated<T>(void* handleAddress)
    {
        if (handleAddress is null) {
            return;
        }

        GCHandle? handle = GetGCHandle(handleAddress);

        if (handle is null) {
            return;
        }

        object? target = handle?.Target;

        if (target is not null &&
            !(target is T)) {
            throw new Exception("Type of handle is unexpected");
        }
        
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
        if (handleAddress is null) {
            return null;
        }
        
        GCHandle handle = GCHandle.FromIntPtr((nint)handleAddress);

        return handle;
    }

    internal static T? GetInstance<T>(void* handleAddress)
    {
        GCHandle? handle = GetGCHandle(handleAddress);

        object? target = handle?.Target;

        if (target is null) {
            return default;
        }

        T instance = (T)target;

        return instance;
    }

    internal static void ReplaceInstance(void* handleAddress, object? newInstance)
    {
        GCHandle? maybeHandle = GetGCHandle(handleAddress);

        if (!maybeHandle.HasValue) {
            return;
        }

        GCHandle handle = maybeHandle.Value;
        
        handle.Target = newInstance;
    }
    #endregion Handle Address/GCHandle <-> Object Conversion

    #region Type Conversion
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastTo")]
    internal static void* /* System.Object */ DNObjectCastTo(void* /* System.Object */ @object, void* /* System.Type */ type, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
        System.Type typeConverted = InteropUtils.GetInstance<System.Type>(type);
    
        try {
	        System.Type currentType = objectConverted.GetType();
	        bool isValidCast = currentType.IsAssignableTo(typeConverted);

	        if (!isValidCast) {
		        throw new InvalidCastException();
	        }
    
            if (outException is not null) {
                *outException = null;
            }
    
            return objectConverted.AllocateGCHandleAndGetAddress();
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return null;
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastAs")]
    internal static void* /* System.Object */ DNObjectCastAs(void* /* System.Object */ @object, void* /* System.Type */ type)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
        System.Type typeConverted = InteropUtils.GetInstance<System.Type>(type);
    
        try {
	        System.Type currentType = objectConverted.GetType();
	        bool isValidCast = currentType.IsAssignableTo(typeConverted);

	        if (!isValidCast) {
		        return null;
	        }
    
            return objectConverted.AllocateGCHandleAndGetAddress();
        } catch {
            return null;
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "DNObjectIs")]
    internal static byte DNObjectIs(void* /* System.Object */ @object, void* /* System.Type */ type)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
        System.Type typeConverted = InteropUtils.GetInstance<System.Type>(type);
    
        try {
	        System.Type currentType = objectConverted.GetType();
	        bool isValidCast = currentType.IsAssignableTo(typeConverted);

            return isValidCast.ToCBool();
        } catch {
            return false.ToCBool();
        }
    }
    #endregion Type Conversion

    #region Strings
    /// <summary>
    /// This allocates a native char* and copies the contents of the managed string into it.
    /// The allocated native string must be freed when not needed anymore!
    /// </summary>
    [UnmanagedCallersOnly(EntryPoint = "DNStringToC")]
    internal static byte* DNStringToC(void* /* System.String? */ systemString)
    {
        if (systemString is null) {
            return null;
        }

        System.String? systemStringConverted = InteropUtils.GetInstance<System.String>(systemString);

        if (systemStringConverted is null) {
            return null;
        }

        byte* cString = (byte*)Marshal.StringToHGlobalAuto(systemStringConverted);
        
        return cString;
    }

    /// <summary>
    /// This allocates a managed string and copies the contents of the native char* into it.
    /// </summary>
    [UnmanagedCallersOnly(EntryPoint = "DNStringFromC")]
    internal static void* /* System.String? */ DNStringFromC(byte* cString)
    {
        if (cString is null) {
            return null;
        }
        
        System.String? systemString = Marshal.PtrToStringAuto((nint)cString);

        if (systemString is null) {
            return null;
        }
        
        void* systemStringNative = systemString.AllocateGCHandleAndGetAddress();

        return systemStringNative;
    }
    #endregion Strings

    #region Bools
    internal static byte ToCBool(this bool @bool)
    {
        if (@bool) {
            return 1;
        } else {
            return 0;
        }
    }

    public static bool ToBool(this byte cBool)
    {
        return cBool == 1;
    }
    #endregion Bools

    #region Boxing/Unboxing of primitives
    #region Bool
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToBool")]
    internal static byte DNObjectCastToBool(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            bool returnValue = (bool)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue.ToCBool();
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(bool).ToCBool();
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromBool")]
    internal static void* /* System.Object */ DNObjectFromBool(byte value)
    {
        return ((System.Object)value.ToBool()).AllocateGCHandleAndGetAddress();
    }
    #endregion Bool
    
    #region Char
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToChar")]
    internal static char DNObjectCastToChar(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            char returnValue = (char)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(char);
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromChar")]
    internal static void* /* System.Object */ DNObjectFromChar(char value)
    {
        return ((System.Object)value).AllocateGCHandleAndGetAddress();
    }
    #endregion Char
    
    #region Float
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToFloat")]
    internal static float DNObjectCastToFloat(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            float returnValue = (float)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(float);
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromFloat")]
    internal static void* /* System.Object */ DNObjectFromFloat(float number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion Float

    #region Double
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToDouble")]
    internal static double DNObjectCastToDouble(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            double returnValue = (double)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(double);
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromDouble")]
    internal static void* /* System.Object */ DNObjectFromDouble(double number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion Double

    #region Int8
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToInt8")]
    internal static sbyte DNObjectCastToInt8(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            sbyte returnValue = (sbyte)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(sbyte);
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromInt8")]
    internal static void* /* System.Object */ DNObjectFromInt8(sbyte number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion Int8
    
    #region UInt8
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToUInt8")]
    internal static byte DNObjectCastToUInt8(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            byte returnValue = (byte)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(byte);
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromUInt8")]
    internal static void* /* System.Object */ DNObjectFromUInt8(byte number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion UInt8

    #region Int16
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToInt16")]
    internal static Int16 DNObjectCastToInt16(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            Int16 returnValue = (Int16)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(Int16);
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromInt16")]
    internal static void* /* System.Object */ DNObjectFromInt16(Int16 number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion Int16
    
    #region UInt16
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToUInt16")]
    internal static UInt16 DNObjectCastToUInt16(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            UInt16 returnValue = (UInt16)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(UInt16);
        }
    }
    
    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromUInt16")]
    internal static void* /* System.Object */ DNObjectFromUInt16(UInt16 number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion UInt16

    #region Int32
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToInt32")]
    internal static Int32 DNObjectCastToInt32(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
	        Int32 returnValue = (Int32)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(Int32);
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromInt32")]
    internal static void* /* System.Object */ DNObjectFromInt32(Int32 number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion Int32
    
    #region UInt32
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToUInt32")]
    internal static UInt32 DNObjectCastToUInt32(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
	        UInt32 returnValue = (UInt32)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(UInt32);
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromUInt32")]
    internal static void* /* System.Object */ DNObjectFromUInt32(UInt32 number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion UInt32

    #region Int64
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToInt64")]
    internal static Int64 DNObjectCastToInt64(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            Int64 returnValue = (Int64)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(Int64);
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromInt64")]
    internal static void* /* System.Object */ DNObjectFromInt64(Int64 number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion Int64
    
    #region UInt64
    [UnmanagedCallersOnly(EntryPoint = "DNObjectCastToUInt64")]
    internal static UInt64 DNObjectCastToUInt64(void* /* System.Object */ @object, void** /* out System.Exception */ outException)
    {
        System.Object objectConverted = InteropUtils.GetInstance<System.Object>(@object);
    
        try {
            UInt64 returnValue = (UInt64)objectConverted;
    
            if (outException is not null) {
                *outException = null;
            }
    
            return returnValue;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
    
            return default(UInt64);
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "DNObjectFromUInt64")]
    internal static void* /* System.Object */ DNObjectFromUInt64(UInt64 number)
    {
        return ((System.Object)number).AllocateGCHandleAndGetAddress();
    }
    #endregion UInt64
    #endregion Boxing/Unboxing of primitives
    
    #region Byte Conversions
    [UnmanagedCallersOnly(EntryPoint = "DNGetPinnedPointerToByteArray")]
    internal static void* DNGetPinnedPointerToByteArray(
        void* /* byte[] */ byteArray,
        void** /* out System.Runtime.InteropServices.GCHandle? */ outGCHandle,
        void** /* out System.Exception */ outException
    )
    {
        byte[] byteArrayConverted = InteropUtils.GetInstance<byte[]>(byteArray);
        
        if (byteArrayConverted is null) {
            if (outGCHandle is not null) {
                *outGCHandle = null;
            }
            
            if (outException is not null) {
                *outException = null;
            }
            
            return null;
        }
        
        try {
            var length = byteArrayConverted.Length;
            
            if (length <= 0) {
                if (outGCHandle is not null) {
                   *outGCHandle = null;
                }
               
                if (outException is not null) {
                    *outException = null;
                }
                
                return null;
            }
            
            var gcHandle = System.Runtime.InteropServices.GCHandle.Alloc(byteArrayConverted, GCHandleType.Pinned);
            var byteArrayConvertedPtr = System.Runtime.CompilerServices.Unsafe.AsPointer(ref byteArrayConverted[0]);
            
            if (outGCHandle is not null) {
                *outGCHandle = gcHandle.AllocateGCHandleAndGetAddress();
            }
            
            if (outException is not null) {
                *outException = null;
            }
            
            return byteArrayConvertedPtr;
        } catch (Exception exception) {
            if (outException is not null) {
                void* exceptionHandleAddress = exception.AllocateGCHandleAndGetAddress();
                    
                *outException = exceptionHandleAddress;
            }
            
            if (outGCHandle is not null) {
                *outGCHandle = null;
            }
        
            return null;
        }
    }
    #endregion Byte Conversions
}

#region DNReadOnlySpanOfByte
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
internal readonly unsafe struct DNReadOnlySpanOfByte
{
    internal void* DataPointer { get; } = null;
    internal int DataLength { get; } = 0;
    
    internal DNReadOnlySpanOfByte(ReadOnlySpan<byte> span)
    {
        var length = span.Length;
        
        if (length <= 0) {
            DataPointer = null;
            DataLength = 0;
            
            return;
        }
        
        // For a no-copy option see: https://www.answeroverflow.com/m/1042197174982811719

        // Console.WriteLine($"[C#] Allocating pointer to buffer of {length} bytes to copy data of ReadOnlySpan<byte> to");
        var destinationDataPointer = System.Runtime.InteropServices.NativeMemory.Alloc((nuint)length);

        var destinationSpan = new Span<byte>(
            destinationDataPointer,
            length
        );
        
        // Console.WriteLine($"[C#] Copying ReadOnlySpan<byte> to native pointer at 0x{(nint)destinationDataPointer:X}");
        span.CopyTo(destinationSpan);
        
        DataPointer = destinationDataPointer;
        DataLength = length;
    }

    internal ReadOnlySpan<byte> CopyDataToManagedReadOnlySpanAndFree()
    {
        ReadOnlySpan<byte> span;

        var dataLength = DataLength;
        var dataPointer = DataPointer;

        if (dataPointer is not null) {
            if (dataLength > 0) {
                // Console.WriteLine($"[C#] Allocating byte[] of {dataLength} bytes to copy data of native pointer to");
                var array = new byte[dataLength];
            
                // Console.WriteLine($"[C#] Copying data of native pointer at 0x{(nint)dataPointer:X} to byte[]");
                System.Runtime.InteropServices.Marshal.Copy(
                    (nint)dataPointer,
                    array,
                    0,
                    dataLength
                );

                span = array;
            } else {
                span = ReadOnlySpan<byte>.Empty;
            }

            System.Runtime.InteropServices.NativeMemory.Free(dataPointer);
        } else {
            span = ReadOnlySpan<byte>.Empty;
        }
     
        return span;
    }
    
    internal static DNReadOnlySpanOfByte Empty => new();
}

internal static class DNReadOnlySpanOfByte_Extensions
{
    internal static DNReadOnlySpanOfByte CopyToNativeReadOnlySpanOfByte(this ReadOnlySpan<byte> span)
    {
        return new DNReadOnlySpanOfByte(span);
    }
}
#endregion DNReadOnlySpanOfByte
""";
}