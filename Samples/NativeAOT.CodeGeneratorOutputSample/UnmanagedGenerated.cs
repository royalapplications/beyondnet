// Number of generated types: 124
// Number of generated members: 708

// <Header>
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NativeGeneratedCode;


// </Header>
// <Shared Code>
internal enum CBool: byte
{
    True = 1,
    False = 0
}

internal enum CStatus: int
{
    Success = 1,
    Failure = -1
}

#if NETSTANDARD2_0 ||  NETCOREAPP2_0 ||  NETCOREAPP2_1 ||  NETCOREAPP2_2 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
public static class ConditionalWeakTable_Extensions
{
    public static void AddOrUpdate<TKey, TValue>(
        this ConditionalWeakTable<TKey, TValue> conditionalWeakTable,
        TKey key,
        TValue value
    ) 
        where TKey: class
        where TValue: class
    {
        conditionalWeakTable.Remove(key);
        conditionalWeakTable.Add(key, value);
    }
}
#endif

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
        if (handleAddress is null) {
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
        if (handleAddress is null) {
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
    /// <summary>
    /// This allocates a native char* and copies the contents of the managed string into it.
    /// The allocated native string must be freed when not needed anymore!
    /// </summary>
    internal static byte* CopyToCString(this string? @string)
    {
        if (@string is null) {
            return null;
        }

        byte* cString = (byte*)Marshal.StringToHGlobalAuto(@string);
        
        return cString;
    }

    /// <summary>
    /// This allocates a managed string and copies the contents of the native char* into it.
    /// </summary>
    internal static string? ToDotNetString(byte* cString)
    {
        if (cString is null) {
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

    public static bool ToBool(this CBool cBool)
    {
        return cBool == CBool.True;
    }
    #endregion Bools
}

// </Shared Code>
// <Unsupported Types>
// Omitted due to settings

// </Unsupported Types>
// <APIs>
internal static unsafe class NativeAOT_CodeGeneratorInputSample_TestClass
{
	[UnmanagedCallersOnly(EntryPoint = "NativeAOT_CodeGeneratorInputSample_TestClass_SayHello")]
	internal static void /* System.Void */ NativeAOT_CodeGeneratorInputSample_TestClass_SayHello(void* /* NativeAOT.CodeGeneratorInputSample.TestClass */ __self, void** /* System.Exception */ __outException)
	{
		NativeAOT.CodeGeneratorInputSample.TestClass __selfDotNet = InteropUtils.GetInstance<NativeAOT.CodeGeneratorInputSample.TestClass>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.SayHello();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "NativeAOT_CodeGeneratorInputSample_TestClass_SayHello1")]
	internal static void /* System.Void */ NativeAOT_CodeGeneratorInputSample_TestClass_SayHello1(void* /* NativeAOT.CodeGeneratorInputSample.TestClass */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		NativeAOT.CodeGeneratorInputSample.TestClass __selfDotNet = InteropUtils.GetInstance<NativeAOT.CodeGeneratorInputSample.TestClass>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.SayHello(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "NativeAOT_CodeGeneratorInputSample_TestClass_GetHello")]
	internal static byte* /* System.String */ NativeAOT_CodeGeneratorInputSample_TestClass_GetHello(void* /* NativeAOT.CodeGeneratorInputSample.TestClass */ __self, void** /* System.Exception */ __outException)
	{
		NativeAOT.CodeGeneratorInputSample.TestClass __selfDotNet = InteropUtils.GetInstance<NativeAOT.CodeGeneratorInputSample.TestClass>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.GetHello();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "NativeAOT_CodeGeneratorInputSample_TestClass_Add")]
	internal static int /* System.Int32 */ NativeAOT_CodeGeneratorInputSample_TestClass_Add(void* /* NativeAOT.CodeGeneratorInputSample.TestClass */ __self, int /* System.Int32 */ number1, int /* System.Int32 */ number2, void** /* System.Exception */ __outException)
	{
		NativeAOT.CodeGeneratorInputSample.TestClass __selfDotNet = InteropUtils.GetInstance<NativeAOT.CodeGeneratorInputSample.TestClass>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.Add(number1, number2);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "NativeAOT_CodeGeneratorInputSample_TestClass_Create")]
	internal static void* /* NativeAOT.CodeGeneratorInputSample.TestClass */ NativeAOT_CodeGeneratorInputSample_TestClass_Create(void** /* System.Exception */ __outException)
	{
	
	    try {
			NativeAOT.CodeGeneratorInputSample.TestClass __returnValue = new NativeAOT.CodeGeneratorInputSample.TestClass();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="NativeAOT_CodeGeneratorInputSample_TestClass_Destroy")]
	internal static void /* System.Void */ NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(void* /* NativeAOT.CodeGeneratorInputSample.TestClass */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Object
{
	[UnmanagedCallersOnly(EntryPoint = "System_Object_GetType")]
	internal static void* /* System.Type */ System_Object_GetType(void* /* System.Object */ __self, void** /* System.Exception */ __outException)
	{
		System.Object __selfDotNet = InteropUtils.GetInstance<System.Object>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.GetType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Object_ToString")]
	internal static byte* /* System.String */ System_Object_ToString(void* /* System.Object */ __self, void** /* System.Exception */ __outException)
	{
		System.Object __selfDotNet = InteropUtils.GetInstance<System.Object>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Object_Equals")]
	internal static CBool /* System.Boolean */ System_Object_Equals(void* /* System.Object */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Object __selfDotNet = InteropUtils.GetInstance<System.Object>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Object_Equals1")]
	internal static CBool /* System.Boolean */ System_Object_Equals1(void* /* System.Object */ objA, void* /* System.Object */ objB, void** /* System.Exception */ __outException)
	{
		System.Object objADotNet = InteropUtils.GetInstance<System.Object>(objA);
		System.Object objBDotNet = InteropUtils.GetInstance<System.Object>(objB);
	
	    try {
			System.Boolean __returnValue = System.Object.Equals(objADotNet, objBDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Object_ReferenceEquals")]
	internal static CBool /* System.Boolean */ System_Object_ReferenceEquals(void* /* System.Object */ objA, void* /* System.Object */ objB, void** /* System.Exception */ __outException)
	{
		System.Object objADotNet = InteropUtils.GetInstance<System.Object>(objA);
		System.Object objBDotNet = InteropUtils.GetInstance<System.Object>(objB);
	
	    try {
			System.Boolean __returnValue = System.Object.ReferenceEquals(objADotNet, objBDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Object_GetHashCode")]
	internal static int /* System.Int32 */ System_Object_GetHashCode(void* /* System.Object */ __self, void** /* System.Exception */ __outException)
	{
		System.Object __selfDotNet = InteropUtils.GetInstance<System.Object>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Object_Create")]
	internal static void* /* System.Object */ System_Object_Create(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Object __returnValue = new System.Object();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_Object_Destroy")]
	internal static void /* System.Void */ System_Object_Destroy(void* /* System.Object */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Type
{
	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetType")]
	internal static void* /* System.Type */ System_Type_GetType(byte* /* System.String */ typeName, CBool /* System.Boolean */ throwOnError, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.String typeNameDotNet = InteropUtils.ToDotNetString(typeName);
		System.Boolean throwOnErrorDotNet = throwOnError.ToBool();
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Type __returnValue = System.Type.GetType(typeNameDotNet, throwOnErrorDotNet, ignoreCaseDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetType1")]
	internal static void* /* System.Type */ System_Type_GetType1(byte* /* System.String */ typeName, CBool /* System.Boolean */ throwOnError, void** /* System.Exception */ __outException)
	{
		System.String typeNameDotNet = InteropUtils.ToDotNetString(typeName);
		System.Boolean throwOnErrorDotNet = throwOnError.ToBool();
	
	    try {
			System.Type __returnValue = System.Type.GetType(typeNameDotNet, throwOnErrorDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetType2")]
	internal static void* /* System.Type */ System_Type_GetType2(byte* /* System.String */ typeName, void** /* System.Exception */ __outException)
	{
		System.String typeNameDotNet = InteropUtils.ToDotNetString(typeName);
	
	    try {
			System.Type __returnValue = System.Type.GetType(typeNameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetType3")]
	internal static void* /* System.Type */ System_Type_GetType3(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.GetType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetElementType")]
	internal static void* /* System.Type */ System_Type_GetElementType(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.GetElementType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetArrayRank")]
	internal static int /* System.Int32 */ System_Type_GetArrayRank(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetArrayRank();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetGenericTypeDefinition")]
	internal static void* /* System.Type */ System_Type_GetGenericTypeDefinition(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.GetGenericTypeDefinition();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_IsAssignableTo")]
	internal static CBool /* System.Boolean */ System_Type_IsAssignableTo(void* /* System.Type */ __self, void* /* System.Type */ targetType, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type targetTypeDotNet = InteropUtils.GetInstance<System.Type>(targetType);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsAssignableTo(targetTypeDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetEvent")]
	internal static void* /* System.Reflection.EventInfo */ System_Type_GetEvent(void* /* System.Type */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.EventInfo __returnValue = __selfDotNet.GetEvent(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetEvent1")]
	internal static void* /* System.Reflection.EventInfo */ System_Type_GetEvent1(void* /* System.Type */ __self, byte* /* System.String */ name, System.Reflection.BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.EventInfo __returnValue = __selfDotNet.GetEvent(nameDotNet, bindingAttr);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetField")]
	internal static void* /* System.Reflection.FieldInfo */ System_Type_GetField(void* /* System.Type */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.FieldInfo __returnValue = __selfDotNet.GetField(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetField1")]
	internal static void* /* System.Reflection.FieldInfo */ System_Type_GetField1(void* /* System.Type */ __self, byte* /* System.String */ name, System.Reflection.BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.FieldInfo __returnValue = __selfDotNet.GetField(nameDotNet, bindingAttr);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetMemberWithSameMetadataDefinitionAs")]
	internal static void* /* System.Reflection.MemberInfo */ System_Type_GetMemberWithSameMetadataDefinitionAs(void* /* System.Type */ __self, void* /* System.Reflection.MemberInfo */ member, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Reflection.MemberInfo memberDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(member);
	
	    try {
			System.Reflection.MemberInfo __returnValue = __selfDotNet.GetMemberWithSameMetadataDefinitionAs(memberDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetMethod")]
	internal static void* /* System.Reflection.MethodInfo */ System_Type_GetMethod(void* /* System.Type */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetMethod(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetMethod1")]
	internal static void* /* System.Reflection.MethodInfo */ System_Type_GetMethod1(void* /* System.Type */ __self, byte* /* System.String */ name, System.Reflection.BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetMethod(nameDotNet, bindingAttr);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetNestedType")]
	internal static void* /* System.Type */ System_Type_GetNestedType(void* /* System.Type */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Type __returnValue = __selfDotNet.GetNestedType(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetNestedType1")]
	internal static void* /* System.Type */ System_Type_GetNestedType1(void* /* System.Type */ __self, byte* /* System.String */ name, System.Reflection.BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Type __returnValue = __selfDotNet.GetNestedType(nameDotNet, bindingAttr);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetProperty")]
	internal static void* /* System.Reflection.PropertyInfo */ System_Type_GetProperty(void* /* System.Type */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.PropertyInfo __returnValue = __selfDotNet.GetProperty(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetProperty1")]
	internal static void* /* System.Reflection.PropertyInfo */ System_Type_GetProperty1(void* /* System.Type */ __self, byte* /* System.String */ name, System.Reflection.BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.PropertyInfo __returnValue = __selfDotNet.GetProperty(nameDotNet, bindingAttr);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetProperty2")]
	internal static void* /* System.Reflection.PropertyInfo */ System_Type_GetProperty2(void* /* System.Type */ __self, byte* /* System.String */ name, void* /* System.Type */ returnType, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Type returnTypeDotNet = InteropUtils.GetInstance<System.Type>(returnType);
	
	    try {
			System.Reflection.PropertyInfo __returnValue = __selfDotNet.GetProperty(nameDotNet, returnTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetTypeCode")]
	internal static System.TypeCode /* System.TypeCode */ System_Type_GetTypeCode(void* /* System.Type */ type, void** /* System.Exception */ __outException)
	{
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
	
	    try {
			System.TypeCode __returnValue = System.Type.GetTypeCode(typeDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.TypeCode);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetTypeFromProgID")]
	internal static void* /* System.Type */ System_Type_GetTypeFromProgID(byte* /* System.String */ progID, void** /* System.Exception */ __outException)
	{
		System.String progIDDotNet = InteropUtils.ToDotNetString(progID);
	
	    try {
			System.Type __returnValue = System.Type.GetTypeFromProgID(progIDDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetTypeFromProgID1")]
	internal static void* /* System.Type */ System_Type_GetTypeFromProgID1(byte* /* System.String */ progID, CBool /* System.Boolean */ throwOnError, void** /* System.Exception */ __outException)
	{
		System.String progIDDotNet = InteropUtils.ToDotNetString(progID);
		System.Boolean throwOnErrorDotNet = throwOnError.ToBool();
	
	    try {
			System.Type __returnValue = System.Type.GetTypeFromProgID(progIDDotNet, throwOnErrorDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetTypeFromProgID2")]
	internal static void* /* System.Type */ System_Type_GetTypeFromProgID2(byte* /* System.String */ progID, byte* /* System.String */ server, void** /* System.Exception */ __outException)
	{
		System.String progIDDotNet = InteropUtils.ToDotNetString(progID);
		System.String serverDotNet = InteropUtils.ToDotNetString(server);
	
	    try {
			System.Type __returnValue = System.Type.GetTypeFromProgID(progIDDotNet, serverDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetTypeFromProgID3")]
	internal static void* /* System.Type */ System_Type_GetTypeFromProgID3(byte* /* System.String */ progID, byte* /* System.String */ server, CBool /* System.Boolean */ throwOnError, void** /* System.Exception */ __outException)
	{
		System.String progIDDotNet = InteropUtils.ToDotNetString(progID);
		System.String serverDotNet = InteropUtils.ToDotNetString(server);
		System.Boolean throwOnErrorDotNet = throwOnError.ToBool();
	
	    try {
			System.Type __returnValue = System.Type.GetTypeFromProgID(progIDDotNet, serverDotNet, throwOnErrorDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetInterface")]
	internal static void* /* System.Type */ System_Type_GetInterface(void* /* System.Type */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Type __returnValue = __selfDotNet.GetInterface(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetInterface1")]
	internal static void* /* System.Type */ System_Type_GetInterface1(void* /* System.Type */ __self, byte* /* System.String */ name, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Type __returnValue = __selfDotNet.GetInterface(nameDotNet, ignoreCaseDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_IsInstanceOfType")]
	internal static CBool /* System.Boolean */ System_Type_IsInstanceOfType(void* /* System.Type */ __self, void* /* System.Object */ o, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object oDotNet = InteropUtils.GetInstance<System.Object>(o);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsInstanceOfType(oDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_IsEquivalentTo")]
	internal static CBool /* System.Boolean */ System_Type_IsEquivalentTo(void* /* System.Type */ __self, void* /* System.Type */ other, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type otherDotNet = InteropUtils.GetInstance<System.Type>(other);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsEquivalentTo(otherDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetEnumUnderlyingType")]
	internal static void* /* System.Type */ System_Type_GetEnumUnderlyingType(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.GetEnumUnderlyingType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetEnumValues")]
	internal static void* /* System.Array */ System_Type_GetEnumValues(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Array __returnValue = __selfDotNet.GetEnumValues();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetEnumValuesAsUnderlyingType")]
	internal static void* /* System.Array */ System_Type_GetEnumValuesAsUnderlyingType(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Array __returnValue = __selfDotNet.GetEnumValuesAsUnderlyingType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_MakeArrayType")]
	internal static void* /* System.Type */ System_Type_MakeArrayType(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.MakeArrayType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_MakeArrayType1")]
	internal static void* /* System.Type */ System_Type_MakeArrayType1(void* /* System.Type */ __self, int /* System.Int32 */ rank, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.MakeArrayType(rank);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_MakeByRefType")]
	internal static void* /* System.Type */ System_Type_MakeByRefType(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.MakeByRefType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_MakePointerType")]
	internal static void* /* System.Type */ System_Type_MakePointerType(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.MakePointerType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_MakeGenericMethodParameter")]
	internal static void* /* System.Type */ System_Type_MakeGenericMethodParameter(int /* System.Int32 */ position, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Type __returnValue = System.Type.MakeGenericMethodParameter(position);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_ToString")]
	internal static byte* /* System.String */ System_Type_ToString(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_Equals")]
	internal static CBool /* System.Boolean */ System_Type_Equals(void* /* System.Type */ __self, void* /* System.Object */ o, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object oDotNet = InteropUtils.GetInstance<System.Object>(o);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(oDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetHashCode")]
	internal static int /* System.Int32 */ System_Type_GetHashCode(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_Equals1")]
	internal static CBool /* System.Boolean */ System_Type_Equals1(void* /* System.Type */ __self, void* /* System.Type */ o, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type oDotNet = InteropUtils.GetInstance<System.Type>(o);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(oDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_op_Equality")]
	internal static CBool /* System.Boolean */ System_Type_op_Equality(void* /* System.Type */ left, void* /* System.Type */ right, void** /* System.Exception */ __outException)
	{
		System.Type leftDotNet = InteropUtils.GetInstance<System.Type>(left);
		System.Type rightDotNet = InteropUtils.GetInstance<System.Type>(right);
	
	    try {
			System.Boolean __returnValue = System.Type.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Type_op_Inequality(void* /* System.Type */ left, void* /* System.Type */ right, void** /* System.Exception */ __outException)
	{
		System.Type leftDotNet = InteropUtils.GetInstance<System.Type>(left);
		System.Type rightDotNet = InteropUtils.GetInstance<System.Type>(right);
	
	    try {
			System.Boolean __returnValue = System.Type.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_ReflectionOnlyGetType")]
	internal static void* /* System.Type */ System_Type_ReflectionOnlyGetType(byte* /* System.String */ typeName, CBool /* System.Boolean */ throwIfNotFound, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.String typeNameDotNet = InteropUtils.ToDotNetString(typeName);
		System.Boolean throwIfNotFoundDotNet = throwIfNotFound.ToBool();
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Type __returnValue = System.Type.ReflectionOnlyGetType(typeNameDotNet, throwIfNotFoundDotNet, ignoreCaseDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_IsEnumDefined")]
	internal static CBool /* System.Boolean */ System_Type_IsEnumDefined(void* /* System.Type */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsEnumDefined(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetEnumName")]
	internal static byte* /* System.String */ System_Type_GetEnumName(void* /* System.Type */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.String __returnValue = __selfDotNet.GetEnumName(valueDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_IsSubclassOf")]
	internal static CBool /* System.Boolean */ System_Type_IsSubclassOf(void* /* System.Type */ __self, void* /* System.Type */ c, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type cDotNet = InteropUtils.GetInstance<System.Type>(c);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsSubclassOf(cDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_IsAssignableFrom")]
	internal static CBool /* System.Boolean */ System_Type_IsAssignableFrom(void* /* System.Type */ __self, void* /* System.Type */ c, void** /* System.Exception */ __outException)
	{
		System.Type __selfDotNet = InteropUtils.GetInstance<System.Type>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type cDotNet = InteropUtils.GetInstance<System.Type>(c);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsAssignableFrom(cDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): IsInterface
	

	// TODO (Property): MemberType
	

	// TODO (Property): Namespace
	

	// TODO (Property): AssemblyQualifiedName
	

	// TODO (Property): FullName
	

	// TODO (Property): Assembly
	

	// TODO (Property): Module
	

	// TODO (Property): IsNested
	

	// TODO (Property): DeclaringType
	

	// TODO (Property): DeclaringMethod
	

	// TODO (Property): ReflectedType
	

	// TODO (Property): UnderlyingSystemType
	

	// TODO (Property): IsTypeDefinition
	

	// TODO (Property): IsArray
	

	// TODO (Property): IsByRef
	

	// TODO (Property): IsPointer
	

	// TODO (Property): IsConstructedGenericType
	

	// TODO (Property): IsGenericParameter
	

	// TODO (Property): IsGenericTypeParameter
	

	// TODO (Property): IsGenericMethodParameter
	

	// TODO (Property): IsGenericType
	

	// TODO (Property): IsGenericTypeDefinition
	

	// TODO (Property): IsSZArray
	

	// TODO (Property): IsVariableBoundArray
	

	// TODO (Property): IsByRefLike
	

	// TODO (Property): HasElementType
	

	// TODO (Property): GenericParameterPosition
	

	// TODO (Property): GenericParameterAttributes
	

	// TODO (Property): Attributes
	

	// TODO (Property): IsAbstract
	

	// TODO (Property): IsImport
	

	// TODO (Property): IsSealed
	

	// TODO (Property): IsSpecialName
	

	// TODO (Property): IsClass
	

	// TODO (Property): IsNestedAssembly
	

	// TODO (Property): IsNestedFamANDAssem
	

	// TODO (Property): IsNestedFamily
	

	// TODO (Property): IsNestedFamORAssem
	

	// TODO (Property): IsNestedPrivate
	

	// TODO (Property): IsNestedPublic
	

	// TODO (Property): IsNotPublic
	

	// TODO (Property): IsPublic
	

	// TODO (Property): IsAutoLayout
	

	// TODO (Property): IsExplicitLayout
	

	// TODO (Property): IsLayoutSequential
	

	// TODO (Property): IsAnsiClass
	

	// TODO (Property): IsAutoClass
	

	// TODO (Property): IsUnicodeClass
	

	// TODO (Property): IsCOMObject
	

	// TODO (Property): IsContextful
	

	// TODO (Property): IsEnum
	

	// TODO (Property): IsMarshalByRef
	

	// TODO (Property): IsPrimitive
	

	// TODO (Property): IsValueType
	

	// TODO (Property): IsSignatureType
	

	// TODO (Property): IsSecurityCritical
	

	// TODO (Property): IsSecuritySafeCritical
	

	// TODO (Property): IsSecurityTransparent
	

	// TODO (Property): StructLayoutAttribute
	

	// TODO (Property): TypeInitializer
	

	// TODO (Property): BaseType
	

	// TODO (Property): DefaultBinder
	

	// TODO (Property): IsSerializable
	

	// TODO (Property): ContainsGenericParameters
	

	// TODO (Property): IsVisible
	

	[UnmanagedCallersOnly(EntryPoint="System_Type_Destroy")]
	internal static void /* System.Void */ System_Type_Destroy(void* /* System.Type */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_MemberInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MemberInfo_HasSameMetadataDefinitionAs")]
	internal static CBool /* System.Boolean */ System_Reflection_MemberInfo_HasSameMetadataDefinitionAs(void* /* System.Reflection.MemberInfo */ __self, void* /* System.Reflection.MemberInfo */ other, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Reflection.MemberInfo otherDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(other);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.HasSameMetadataDefinitionAs(otherDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MemberInfo_IsDefined")]
	internal static CBool /* System.Boolean */ System_Reflection_MemberInfo_IsDefined(void* /* System.Reflection.MemberInfo */ __self, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsDefined(attributeTypeDotNet, inheritDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MemberInfo_Equals")]
	internal static CBool /* System.Boolean */ System_Reflection_MemberInfo_Equals(void* /* System.Reflection.MemberInfo */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MemberInfo_GetHashCode")]
	internal static int /* System.Int32 */ System_Reflection_MemberInfo_GetHashCode(void* /* System.Reflection.MemberInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MemberInfo_op_Equality")]
	internal static CBool /* System.Boolean */ System_Reflection_MemberInfo_op_Equality(void* /* System.Reflection.MemberInfo */ left, void* /* System.Reflection.MemberInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(left);
		System.Reflection.MemberInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.MemberInfo.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MemberInfo_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Reflection_MemberInfo_op_Inequality(void* /* System.Reflection.MemberInfo */ left, void* /* System.Reflection.MemberInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(left);
		System.Reflection.MemberInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.MemberInfo.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): MemberType
	

	// TODO (Property): Name
	

	// TODO (Property): DeclaringType
	

	// TODO (Property): ReflectedType
	

	// TODO (Property): Module
	

	// TODO (Property): IsCollectible
	

	// TODO (Property): MetadataToken
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_MemberInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_MemberInfo_Destroy(void* /* System.Reflection.MemberInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Enum
{
	[UnmanagedCallersOnly(EntryPoint = "System_Enum_GetName")]
	internal static byte* /* System.String */ System_Enum_GetName(void* /* System.Type */ enumType, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.String __returnValue = System.Enum.GetName(enumTypeDotNet, valueDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_GetUnderlyingType")]
	internal static void* /* System.Type */ System_Enum_GetUnderlyingType(void* /* System.Type */ enumType, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Type __returnValue = System.Enum.GetUnderlyingType(enumTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_GetValues")]
	internal static void* /* System.Array */ System_Enum_GetValues(void* /* System.Type */ enumType, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Array __returnValue = System.Enum.GetValues(enumTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_GetValuesAsUnderlyingType")]
	internal static void* /* System.Array */ System_Enum_GetValuesAsUnderlyingType(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Array __returnValue = System.Enum.GetValuesAsUnderlyingType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_GetValuesAsUnderlyingType1")]
	internal static void* /* System.Array */ System_Enum_GetValuesAsUnderlyingType1(void* /* System.Type */ enumType, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Array __returnValue = System.Enum.GetValuesAsUnderlyingType(enumTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_HasFlag")]
	internal static CBool /* System.Boolean */ System_Enum_HasFlag(void* /* System.Enum */ __self, void* /* System.Enum */ flag, void** /* System.Exception */ __outException)
	{
		System.Enum __selfDotNet = InteropUtils.GetInstance<System.Enum>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Enum flagDotNet = InteropUtils.GetInstance<System.Enum>(flag);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.HasFlag(flagDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_IsDefined")]
	internal static CBool /* System.Boolean */ System_Enum_IsDefined(void* /* System.Type */ enumType, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Boolean __returnValue = System.Enum.IsDefined(enumTypeDotNet, valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_Parse")]
	internal static void* /* System.Object */ System_Enum_Parse(void* /* System.Type */ enumType, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Object __returnValue = System.Enum.Parse(enumTypeDotNet, valueDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_Parse1")]
	internal static void* /* System.Object */ System_Enum_Parse1(void* /* System.Type */ enumType, byte* /* System.String */ value, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Object __returnValue = System.Enum.Parse(enumTypeDotNet, valueDotNet, ignoreCaseDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_Equals")]
	internal static CBool /* System.Boolean */ System_Enum_Equals(void* /* System.Enum */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Enum __selfDotNet = InteropUtils.GetInstance<System.Enum>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_GetHashCode")]
	internal static int /* System.Int32 */ System_Enum_GetHashCode(void* /* System.Enum */ __self, void** /* System.Exception */ __outException)
	{
		System.Enum __selfDotNet = InteropUtils.GetInstance<System.Enum>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_CompareTo")]
	internal static int /* System.Int32 */ System_Enum_CompareTo(void* /* System.Enum */ __self, void* /* System.Object */ target, void** /* System.Exception */ __outException)
	{
		System.Enum __selfDotNet = InteropUtils.GetInstance<System.Enum>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object targetDotNet = InteropUtils.GetInstance<System.Object>(target);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.CompareTo(targetDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToString")]
	internal static byte* /* System.String */ System_Enum_ToString(void* /* System.Enum */ __self, void** /* System.Exception */ __outException)
	{
		System.Enum __selfDotNet = InteropUtils.GetInstance<System.Enum>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToString1")]
	internal static byte* /* System.String */ System_Enum_ToString1(void* /* System.Enum */ __self, byte* /* System.String */ format, void** /* System.Exception */ __outException)
	{
		System.Enum __selfDotNet = InteropUtils.GetInstance<System.Enum>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
	
	    try {
			System.String __returnValue = __selfDotNet.ToString(formatDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToString2")]
	internal static byte* /* System.String */ System_Enum_ToString2(void* /* System.Enum */ __self, void* /* System.IFormatProvider */ provider, void** /* System.Exception */ __outException)
	{
		System.Enum __selfDotNet = InteropUtils.GetInstance<System.Enum>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IFormatProvider providerDotNet = InteropUtils.GetInstance<System.IFormatProvider>(provider);
	
	    try {
			System.String __returnValue = __selfDotNet.ToString(providerDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToString3")]
	internal static byte* /* System.String */ System_Enum_ToString3(void* /* System.Enum */ __self, byte* /* System.String */ format, void* /* System.IFormatProvider */ provider, void** /* System.Exception */ __outException)
	{
		System.Enum __selfDotNet = InteropUtils.GetInstance<System.Enum>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
		System.IFormatProvider providerDotNet = InteropUtils.GetInstance<System.IFormatProvider>(provider);
	
	    try {
			System.String __returnValue = __selfDotNet.ToString(formatDotNet, providerDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_Format")]
	internal static byte* /* System.String */ System_Enum_Format(void* /* System.Type */ enumType, void* /* System.Object */ value, byte* /* System.String */ format, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
	
	    try {
			System.String __returnValue = System.Enum.Format(enumTypeDotNet, valueDotNet, formatDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_GetTypeCode")]
	internal static System.TypeCode /* System.TypeCode */ System_Enum_GetTypeCode(void* /* System.Enum */ __self, void** /* System.Exception */ __outException)
	{
		System.Enum __selfDotNet = InteropUtils.GetInstance<System.Enum>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.TypeCode __returnValue = __selfDotNet.GetTypeCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.TypeCode);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToObject")]
	internal static void* /* System.Object */ System_Enum_ToObject(void* /* System.Type */ enumType, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Object __returnValue = System.Enum.ToObject(enumTypeDotNet, valueDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToObject1")]
	internal static void* /* System.Object */ System_Enum_ToObject1(void* /* System.Type */ enumType, sbyte /* System.SByte */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Object __returnValue = System.Enum.ToObject(enumTypeDotNet, value);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToObject2")]
	internal static void* /* System.Object */ System_Enum_ToObject2(void* /* System.Type */ enumType, short /* System.Int16 */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Object __returnValue = System.Enum.ToObject(enumTypeDotNet, value);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToObject3")]
	internal static void* /* System.Object */ System_Enum_ToObject3(void* /* System.Type */ enumType, int /* System.Int32 */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Object __returnValue = System.Enum.ToObject(enumTypeDotNet, value);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToObject4")]
	internal static void* /* System.Object */ System_Enum_ToObject4(void* /* System.Type */ enumType, byte /* System.Byte */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Object __returnValue = System.Enum.ToObject(enumTypeDotNet, value);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToObject5")]
	internal static void* /* System.Object */ System_Enum_ToObject5(void* /* System.Type */ enumType, ushort /* System.UInt16 */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Object __returnValue = System.Enum.ToObject(enumTypeDotNet, value);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToObject6")]
	internal static void* /* System.Object */ System_Enum_ToObject6(void* /* System.Type */ enumType, uint /* System.UInt32 */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Object __returnValue = System.Enum.ToObject(enumTypeDotNet, value);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToObject7")]
	internal static void* /* System.Object */ System_Enum_ToObject7(void* /* System.Type */ enumType, long /* System.Int64 */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Object __returnValue = System.Enum.ToObject(enumTypeDotNet, value);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Enum_ToObject8")]
	internal static void* /* System.Object */ System_Enum_ToObject8(void* /* System.Type */ enumType, ulong /* System.UInt64 */ value, void** /* System.Exception */ __outException)
	{
		System.Type enumTypeDotNet = InteropUtils.GetInstance<System.Type>(enumType);
	
	    try {
			System.Object __returnValue = System.Enum.ToObject(enumTypeDotNet, value);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_Enum_Destroy")]
	internal static void /* System.Void */ System_Enum_Destroy(void* /* System.Enum */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_ValueType
{
	[UnmanagedCallersOnly(EntryPoint = "System_ValueType_Equals")]
	internal static CBool /* System.Boolean */ System_ValueType_Equals(void* /* System.ValueType */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.ValueType __selfDotNet = InteropUtils.GetInstance<System.ValueType>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_ValueType_GetHashCode")]
	internal static int /* System.Int32 */ System_ValueType_GetHashCode(void* /* System.ValueType */ __self, void** /* System.Exception */ __outException)
	{
		System.ValueType __selfDotNet = InteropUtils.GetInstance<System.ValueType>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_ValueType_ToString")]
	internal static byte* /* System.String */ System_ValueType_ToString(void* /* System.ValueType */ __self, void** /* System.Exception */ __outException)
	{
		System.ValueType __selfDotNet = InteropUtils.GetInstance<System.ValueType>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_ValueType_Destroy")]
	internal static void /* System.Void */ System_ValueType_Destroy(void* /* System.ValueType */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}




internal static unsafe class System_String
{
	[UnmanagedCallersOnly(EntryPoint = "System_String_Intern")]
	internal static byte* /* System.String */ System_String_Intern(byte* /* System.String */ str, void** /* System.Exception */ __outException)
	{
		System.String strDotNet = InteropUtils.ToDotNetString(str);
	
	    try {
			System.String __returnValue = System.String.Intern(strDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IsInterned")]
	internal static byte* /* System.String */ System_String_IsInterned(byte* /* System.String */ str, void** /* System.Exception */ __outException)
	{
		System.String strDotNet = InteropUtils.ToDotNetString(str);
	
	    try {
			System.String __returnValue = System.String.IsInterned(strDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare")]
	internal static int /* System.Int32 */ System_String_Compare(byte* /* System.String */ strA, byte* /* System.String */ strB, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, strBDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare1")]
	internal static int /* System.Int32 */ System_String_Compare1(byte* /* System.String */ strA, byte* /* System.String */ strB, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, strBDotNet, ignoreCaseDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare2")]
	internal static int /* System.Int32 */ System_String_Compare2(byte* /* System.String */ strA, byte* /* System.String */ strB, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, strBDotNet, comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare3")]
	internal static int /* System.Int32 */ System_String_Compare3(byte* /* System.String */ strA, byte* /* System.String */ strB, void* /* System.Globalization.CultureInfo */ culture, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, strBDotNet, cultureDotNet, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare4")]
	internal static int /* System.Int32 */ System_String_Compare4(byte* /* System.String */ strA, byte* /* System.String */ strB, CBool /* System.Boolean */ ignoreCase, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, strBDotNet, ignoreCaseDotNet, cultureDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare5")]
	internal static int /* System.Int32 */ System_String_Compare5(byte* /* System.String */ strA, int /* System.Int32 */ indexA, byte* /* System.String */ strB, int /* System.Int32 */ indexB, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, indexA, strBDotNet, indexB, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare6")]
	internal static int /* System.Int32 */ System_String_Compare6(byte* /* System.String */ strA, int /* System.Int32 */ indexA, byte* /* System.String */ strB, int /* System.Int32 */ indexB, int /* System.Int32 */ length, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, indexA, strBDotNet, indexB, length, ignoreCaseDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare7")]
	internal static int /* System.Int32 */ System_String_Compare7(byte* /* System.String */ strA, int /* System.Int32 */ indexA, byte* /* System.String */ strB, int /* System.Int32 */ indexB, int /* System.Int32 */ length, CBool /* System.Boolean */ ignoreCase, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, indexA, strBDotNet, indexB, length, ignoreCaseDotNet, cultureDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare8")]
	internal static int /* System.Int32 */ System_String_Compare8(byte* /* System.String */ strA, int /* System.Int32 */ indexA, byte* /* System.String */ strB, int /* System.Int32 */ indexB, int /* System.Int32 */ length, void* /* System.Globalization.CultureInfo */ culture, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, indexA, strBDotNet, indexB, length, cultureDotNet, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Compare9")]
	internal static int /* System.Int32 */ System_String_Compare9(byte* /* System.String */ strA, int /* System.Int32 */ indexA, byte* /* System.String */ strB, int /* System.Int32 */ indexB, int /* System.Int32 */ length, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
	
	    try {
			System.Int32 __returnValue = System.String.Compare(strADotNet, indexA, strBDotNet, indexB, length, comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_CompareOrdinal")]
	internal static int /* System.Int32 */ System_String_CompareOrdinal(byte* /* System.String */ strA, byte* /* System.String */ strB, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
	
	    try {
			System.Int32 __returnValue = System.String.CompareOrdinal(strADotNet, strBDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_CompareOrdinal1")]
	internal static int /* System.Int32 */ System_String_CompareOrdinal1(byte* /* System.String */ strA, int /* System.Int32 */ indexA, byte* /* System.String */ strB, int /* System.Int32 */ indexB, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.String strADotNet = InteropUtils.ToDotNetString(strA);
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
	
	    try {
			System.Int32 __returnValue = System.String.CompareOrdinal(strADotNet, indexA, strBDotNet, indexB, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_CompareTo")]
	internal static int /* System.Int32 */ System_String_CompareTo(byte* /* System.String */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.CompareTo(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_CompareTo1")]
	internal static int /* System.Int32 */ System_String_CompareTo1(byte* /* System.String */ __self, byte* /* System.String */ strB, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String strBDotNet = InteropUtils.ToDotNetString(strB);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.CompareTo(strBDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_EndsWith")]
	internal static CBool /* System.Boolean */ System_String_EndsWith(byte* /* System.String */ __self, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.EndsWith(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_EndsWith1")]
	internal static CBool /* System.Boolean */ System_String_EndsWith1(byte* /* System.String */ __self, byte* /* System.String */ value, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.EndsWith(valueDotNet, comparisonType);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_EndsWith2")]
	internal static CBool /* System.Boolean */ System_String_EndsWith2(byte* /* System.String */ __self, byte* /* System.String */ value, CBool /* System.Boolean */ ignoreCase, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.EndsWith(valueDotNet, ignoreCaseDotNet, cultureDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_EndsWith3")]
	internal static CBool /* System.Boolean */ System_String_EndsWith3(byte* /* System.String */ __self, System.Char /* System.Char */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.EndsWith(value);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Equals")]
	internal static CBool /* System.Boolean */ System_String_Equals(byte* /* System.String */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Equals1")]
	internal static CBool /* System.Boolean */ System_String_Equals1(byte* /* System.String */ __self, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Equals2")]
	internal static CBool /* System.Boolean */ System_String_Equals2(byte* /* System.String */ __self, byte* /* System.String */ value, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(valueDotNet, comparisonType);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Equals3")]
	internal static CBool /* System.Boolean */ System_String_Equals3(byte* /* System.String */ a, byte* /* System.String */ b, void** /* System.Exception */ __outException)
	{
		System.String aDotNet = InteropUtils.ToDotNetString(a);
		System.String bDotNet = InteropUtils.ToDotNetString(b);
	
	    try {
			System.Boolean __returnValue = System.String.Equals(aDotNet, bDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Equals4")]
	internal static CBool /* System.Boolean */ System_String_Equals4(byte* /* System.String */ a, byte* /* System.String */ b, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String aDotNet = InteropUtils.ToDotNetString(a);
		System.String bDotNet = InteropUtils.ToDotNetString(b);
	
	    try {
			System.Boolean __returnValue = System.String.Equals(aDotNet, bDotNet, comparisonType);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_op_Equality")]
	internal static CBool /* System.Boolean */ System_String_op_Equality(byte* /* System.String */ a, byte* /* System.String */ b, void** /* System.Exception */ __outException)
	{
		System.String aDotNet = InteropUtils.ToDotNetString(a);
		System.String bDotNet = InteropUtils.ToDotNetString(b);
	
	    try {
			System.Boolean __returnValue = System.String.op_Equality(aDotNet, bDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_op_Inequality")]
	internal static CBool /* System.Boolean */ System_String_op_Inequality(byte* /* System.String */ a, byte* /* System.String */ b, void** /* System.Exception */ __outException)
	{
		System.String aDotNet = InteropUtils.ToDotNetString(a);
		System.String bDotNet = InteropUtils.ToDotNetString(b);
	
	    try {
			System.Boolean __returnValue = System.String.op_Inequality(aDotNet, bDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_GetHashCode")]
	internal static int /* System.Int32 */ System_String_GetHashCode(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_GetHashCode1")]
	internal static int /* System.Int32 */ System_String_GetHashCode1(byte* /* System.String */ __self, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode(comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_StartsWith")]
	internal static CBool /* System.Boolean */ System_String_StartsWith(byte* /* System.String */ __self, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.StartsWith(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_StartsWith1")]
	internal static CBool /* System.Boolean */ System_String_StartsWith1(byte* /* System.String */ __self, byte* /* System.String */ value, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.StartsWith(valueDotNet, comparisonType);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_StartsWith2")]
	internal static CBool /* System.Boolean */ System_String_StartsWith2(byte* /* System.String */ __self, byte* /* System.String */ value, CBool /* System.Boolean */ ignoreCase, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.StartsWith(valueDotNet, ignoreCaseDotNet, cultureDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_StartsWith3")]
	internal static CBool /* System.Boolean */ System_String_StartsWith3(byte* /* System.String */ __self, System.Char /* System.Char */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.StartsWith(value);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Clone")]
	internal static void* /* System.Object */ System_String_Clone(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Copy")]
	internal static byte* /* System.String */ System_String_Copy(byte* /* System.String */ str, void** /* System.Exception */ __outException)
	{
		System.String strDotNet = InteropUtils.ToDotNetString(str);
	
	    try {
			System.String __returnValue = System.String.Copy(strDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IsNullOrEmpty")]
	internal static CBool /* System.Boolean */ System_String_IsNullOrEmpty(byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = System.String.IsNullOrEmpty(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IsNullOrWhiteSpace")]
	internal static CBool /* System.Boolean */ System_String_IsNullOrWhiteSpace(byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = System.String.IsNullOrWhiteSpace(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ToString")]
	internal static byte* /* System.String */ System_String_ToString(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ToString1")]
	internal static byte* /* System.String */ System_String_ToString1(byte* /* System.String */ __self, void* /* System.IFormatProvider */ provider, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IFormatProvider providerDotNet = InteropUtils.GetInstance<System.IFormatProvider>(provider);
	
	    try {
			System.String __returnValue = __selfDotNet.ToString(providerDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_GetEnumerator")]
	internal static void* /* System.CharEnumerator */ System_String_GetEnumerator(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.CharEnumerator __returnValue = __selfDotNet.GetEnumerator();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_GetTypeCode")]
	internal static System.TypeCode /* System.TypeCode */ System_String_GetTypeCode(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.TypeCode __returnValue = __selfDotNet.GetTypeCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.TypeCode);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IsNormalized")]
	internal static CBool /* System.Boolean */ System_String_IsNormalized(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsNormalized();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IsNormalized1")]
	internal static CBool /* System.Boolean */ System_String_IsNormalized1(byte* /* System.String */ __self, System.Text.NormalizationForm /* System.Text.NormalizationForm */ normalizationForm, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsNormalized(normalizationForm);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Normalize")]
	internal static byte* /* System.String */ System_String_Normalize(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.Normalize();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Normalize1")]
	internal static byte* /* System.String */ System_String_Normalize1(byte* /* System.String */ __self, System.Text.NormalizationForm /* System.Text.NormalizationForm */ normalizationForm, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.Normalize(normalizationForm);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Concat")]
	internal static byte* /* System.String */ System_String_Concat(void* /* System.Object */ arg0, void** /* System.Exception */ __outException)
	{
		System.Object arg0DotNet = InteropUtils.GetInstance<System.Object>(arg0);
	
	    try {
			System.String __returnValue = System.String.Concat(arg0DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Concat1")]
	internal static byte* /* System.String */ System_String_Concat1(void* /* System.Object */ arg0, void* /* System.Object */ arg1, void** /* System.Exception */ __outException)
	{
		System.Object arg0DotNet = InteropUtils.GetInstance<System.Object>(arg0);
		System.Object arg1DotNet = InteropUtils.GetInstance<System.Object>(arg1);
	
	    try {
			System.String __returnValue = System.String.Concat(arg0DotNet, arg1DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Concat2")]
	internal static byte* /* System.String */ System_String_Concat2(void* /* System.Object */ arg0, void* /* System.Object */ arg1, void* /* System.Object */ arg2, void** /* System.Exception */ __outException)
	{
		System.Object arg0DotNet = InteropUtils.GetInstance<System.Object>(arg0);
		System.Object arg1DotNet = InteropUtils.GetInstance<System.Object>(arg1);
		System.Object arg2DotNet = InteropUtils.GetInstance<System.Object>(arg2);
	
	    try {
			System.String __returnValue = System.String.Concat(arg0DotNet, arg1DotNet, arg2DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Concat3")]
	internal static byte* /* System.String */ System_String_Concat3(byte* /* System.String */ str0, byte* /* System.String */ str1, void** /* System.Exception */ __outException)
	{
		System.String str0DotNet = InteropUtils.ToDotNetString(str0);
		System.String str1DotNet = InteropUtils.ToDotNetString(str1);
	
	    try {
			System.String __returnValue = System.String.Concat(str0DotNet, str1DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Concat4")]
	internal static byte* /* System.String */ System_String_Concat4(byte* /* System.String */ str0, byte* /* System.String */ str1, byte* /* System.String */ str2, void** /* System.Exception */ __outException)
	{
		System.String str0DotNet = InteropUtils.ToDotNetString(str0);
		System.String str1DotNet = InteropUtils.ToDotNetString(str1);
		System.String str2DotNet = InteropUtils.ToDotNetString(str2);
	
	    try {
			System.String __returnValue = System.String.Concat(str0DotNet, str1DotNet, str2DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Concat5")]
	internal static byte* /* System.String */ System_String_Concat5(byte* /* System.String */ str0, byte* /* System.String */ str1, byte* /* System.String */ str2, byte* /* System.String */ str3, void** /* System.Exception */ __outException)
	{
		System.String str0DotNet = InteropUtils.ToDotNetString(str0);
		System.String str1DotNet = InteropUtils.ToDotNetString(str1);
		System.String str2DotNet = InteropUtils.ToDotNetString(str2);
		System.String str3DotNet = InteropUtils.ToDotNetString(str3);
	
	    try {
			System.String __returnValue = System.String.Concat(str0DotNet, str1DotNet, str2DotNet, str3DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Format")]
	internal static byte* /* System.String */ System_String_Format(byte* /* System.String */ format, void* /* System.Object */ arg0, void** /* System.Exception */ __outException)
	{
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
		System.Object arg0DotNet = InteropUtils.GetInstance<System.Object>(arg0);
	
	    try {
			System.String __returnValue = System.String.Format(formatDotNet, arg0DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Format1")]
	internal static byte* /* System.String */ System_String_Format1(byte* /* System.String */ format, void* /* System.Object */ arg0, void* /* System.Object */ arg1, void** /* System.Exception */ __outException)
	{
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
		System.Object arg0DotNet = InteropUtils.GetInstance<System.Object>(arg0);
		System.Object arg1DotNet = InteropUtils.GetInstance<System.Object>(arg1);
	
	    try {
			System.String __returnValue = System.String.Format(formatDotNet, arg0DotNet, arg1DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Format2")]
	internal static byte* /* System.String */ System_String_Format2(byte* /* System.String */ format, void* /* System.Object */ arg0, void* /* System.Object */ arg1, void* /* System.Object */ arg2, void** /* System.Exception */ __outException)
	{
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
		System.Object arg0DotNet = InteropUtils.GetInstance<System.Object>(arg0);
		System.Object arg1DotNet = InteropUtils.GetInstance<System.Object>(arg1);
		System.Object arg2DotNet = InteropUtils.GetInstance<System.Object>(arg2);
	
	    try {
			System.String __returnValue = System.String.Format(formatDotNet, arg0DotNet, arg1DotNet, arg2DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Format3")]
	internal static byte* /* System.String */ System_String_Format3(void* /* System.IFormatProvider */ provider, byte* /* System.String */ format, void* /* System.Object */ arg0, void** /* System.Exception */ __outException)
	{
		System.IFormatProvider providerDotNet = InteropUtils.GetInstance<System.IFormatProvider>(provider);
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
		System.Object arg0DotNet = InteropUtils.GetInstance<System.Object>(arg0);
	
	    try {
			System.String __returnValue = System.String.Format(providerDotNet, formatDotNet, arg0DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Format4")]
	internal static byte* /* System.String */ System_String_Format4(void* /* System.IFormatProvider */ provider, byte* /* System.String */ format, void* /* System.Object */ arg0, void* /* System.Object */ arg1, void** /* System.Exception */ __outException)
	{
		System.IFormatProvider providerDotNet = InteropUtils.GetInstance<System.IFormatProvider>(provider);
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
		System.Object arg0DotNet = InteropUtils.GetInstance<System.Object>(arg0);
		System.Object arg1DotNet = InteropUtils.GetInstance<System.Object>(arg1);
	
	    try {
			System.String __returnValue = System.String.Format(providerDotNet, formatDotNet, arg0DotNet, arg1DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Format5")]
	internal static byte* /* System.String */ System_String_Format5(void* /* System.IFormatProvider */ provider, byte* /* System.String */ format, void* /* System.Object */ arg0, void* /* System.Object */ arg1, void* /* System.Object */ arg2, void** /* System.Exception */ __outException)
	{
		System.IFormatProvider providerDotNet = InteropUtils.GetInstance<System.IFormatProvider>(provider);
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
		System.Object arg0DotNet = InteropUtils.GetInstance<System.Object>(arg0);
		System.Object arg1DotNet = InteropUtils.GetInstance<System.Object>(arg1);
		System.Object arg2DotNet = InteropUtils.GetInstance<System.Object>(arg2);
	
	    try {
			System.String __returnValue = System.String.Format(providerDotNet, formatDotNet, arg0DotNet, arg1DotNet, arg2DotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Insert")]
	internal static byte* /* System.String */ System_String_Insert(byte* /* System.String */ __self, int /* System.Int32 */ startIndex, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.String __returnValue = __selfDotNet.Insert(startIndex, valueDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_PadLeft")]
	internal static byte* /* System.String */ System_String_PadLeft(byte* /* System.String */ __self, int /* System.Int32 */ totalWidth, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.PadLeft(totalWidth);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_PadLeft1")]
	internal static byte* /* System.String */ System_String_PadLeft1(byte* /* System.String */ __self, int /* System.Int32 */ totalWidth, System.Char /* System.Char */ paddingChar, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.PadLeft(totalWidth, paddingChar);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_PadRight")]
	internal static byte* /* System.String */ System_String_PadRight(byte* /* System.String */ __self, int /* System.Int32 */ totalWidth, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.PadRight(totalWidth);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_PadRight1")]
	internal static byte* /* System.String */ System_String_PadRight1(byte* /* System.String */ __self, int /* System.Int32 */ totalWidth, System.Char /* System.Char */ paddingChar, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.PadRight(totalWidth, paddingChar);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Remove")]
	internal static byte* /* System.String */ System_String_Remove(byte* /* System.String */ __self, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.Remove(startIndex, count);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Remove1")]
	internal static byte* /* System.String */ System_String_Remove1(byte* /* System.String */ __self, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.Remove(startIndex);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Replace")]
	internal static byte* /* System.String */ System_String_Replace(byte* /* System.String */ __self, byte* /* System.String */ oldValue, byte* /* System.String */ newValue, CBool /* System.Boolean */ ignoreCase, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String oldValueDotNet = InteropUtils.ToDotNetString(oldValue);
		System.String newValueDotNet = InteropUtils.ToDotNetString(newValue);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.String __returnValue = __selfDotNet.Replace(oldValueDotNet, newValueDotNet, ignoreCaseDotNet, cultureDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Replace1")]
	internal static byte* /* System.String */ System_String_Replace1(byte* /* System.String */ __self, byte* /* System.String */ oldValue, byte* /* System.String */ newValue, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String oldValueDotNet = InteropUtils.ToDotNetString(oldValue);
		System.String newValueDotNet = InteropUtils.ToDotNetString(newValue);
	
	    try {
			System.String __returnValue = __selfDotNet.Replace(oldValueDotNet, newValueDotNet, comparisonType);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Replace2")]
	internal static byte* /* System.String */ System_String_Replace2(byte* /* System.String */ __self, System.Char /* System.Char */ oldChar, System.Char /* System.Char */ newChar, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.Replace(oldChar, newChar);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Replace3")]
	internal static byte* /* System.String */ System_String_Replace3(byte* /* System.String */ __self, byte* /* System.String */ oldValue, byte* /* System.String */ newValue, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String oldValueDotNet = InteropUtils.ToDotNetString(oldValue);
		System.String newValueDotNet = InteropUtils.ToDotNetString(newValue);
	
	    try {
			System.String __returnValue = __selfDotNet.Replace(oldValueDotNet, newValueDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ReplaceLineEndings")]
	internal static byte* /* System.String */ System_String_ReplaceLineEndings(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ReplaceLineEndings();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ReplaceLineEndings1")]
	internal static byte* /* System.String */ System_String_ReplaceLineEndings1(byte* /* System.String */ __self, byte* /* System.String */ replacementText, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String replacementTextDotNet = InteropUtils.ToDotNetString(replacementText);
	
	    try {
			System.String __returnValue = __selfDotNet.ReplaceLineEndings(replacementTextDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Substring")]
	internal static byte* /* System.String */ System_String_Substring(byte* /* System.String */ __self, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.Substring(startIndex);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Substring1")]
	internal static byte* /* System.String */ System_String_Substring1(byte* /* System.String */ __self, int /* System.Int32 */ startIndex, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.Substring(startIndex, length);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ToLower")]
	internal static byte* /* System.String */ System_String_ToLower(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToLower();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ToLower1")]
	internal static byte* /* System.String */ System_String_ToLower1(byte* /* System.String */ __self, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.String __returnValue = __selfDotNet.ToLower(cultureDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ToLowerInvariant")]
	internal static byte* /* System.String */ System_String_ToLowerInvariant(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToLowerInvariant();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ToUpper")]
	internal static byte* /* System.String */ System_String_ToUpper(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToUpper();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ToUpper1")]
	internal static byte* /* System.String */ System_String_ToUpper1(byte* /* System.String */ __self, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.String __returnValue = __selfDotNet.ToUpper(cultureDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_ToUpperInvariant")]
	internal static byte* /* System.String */ System_String_ToUpperInvariant(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToUpperInvariant();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Trim")]
	internal static byte* /* System.String */ System_String_Trim(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.Trim();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Trim1")]
	internal static byte* /* System.String */ System_String_Trim1(byte* /* System.String */ __self, System.Char /* System.Char */ trimChar, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.Trim(trimChar);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_TrimStart")]
	internal static byte* /* System.String */ System_String_TrimStart(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.TrimStart();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_TrimStart1")]
	internal static byte* /* System.String */ System_String_TrimStart1(byte* /* System.String */ __self, System.Char /* System.Char */ trimChar, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.TrimStart(trimChar);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_TrimEnd")]
	internal static byte* /* System.String */ System_String_TrimEnd(byte* /* System.String */ __self, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.TrimEnd();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_TrimEnd1")]
	internal static byte* /* System.String */ System_String_TrimEnd1(byte* /* System.String */ __self, System.Char /* System.Char */ trimChar, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.TrimEnd(trimChar);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Contains")]
	internal static CBool /* System.Boolean */ System_String_Contains(byte* /* System.String */ __self, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Contains(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Contains1")]
	internal static CBool /* System.Boolean */ System_String_Contains1(byte* /* System.String */ __self, byte* /* System.String */ value, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Contains(valueDotNet, comparisonType);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Contains2")]
	internal static CBool /* System.Boolean */ System_String_Contains2(byte* /* System.String */ __self, System.Char /* System.Char */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Contains(value);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Contains3")]
	internal static CBool /* System.Boolean */ System_String_Contains3(byte* /* System.String */ __self, System.Char /* System.Char */ value, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Contains(value, comparisonType);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf")]
	internal static int /* System.Int32 */ System_String_IndexOf(byte* /* System.String */ __self, System.Char /* System.Char */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf1")]
	internal static int /* System.Int32 */ System_String_IndexOf1(byte* /* System.String */ __self, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(value, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf2")]
	internal static int /* System.Int32 */ System_String_IndexOf2(byte* /* System.String */ __self, System.Char /* System.Char */ value, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(value, comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf3")]
	internal static int /* System.Int32 */ System_String_IndexOf3(byte* /* System.String */ __self, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(value, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf4")]
	internal static int /* System.Int32 */ System_String_IndexOf4(byte* /* System.String */ __self, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf5")]
	internal static int /* System.Int32 */ System_String_IndexOf5(byte* /* System.String */ __self, byte* /* System.String */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(valueDotNet, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf6")]
	internal static int /* System.Int32 */ System_String_IndexOf6(byte* /* System.String */ __self, byte* /* System.String */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(valueDotNet, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf7")]
	internal static int /* System.Int32 */ System_String_IndexOf7(byte* /* System.String */ __self, byte* /* System.String */ value, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(valueDotNet, comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf8")]
	internal static int /* System.Int32 */ System_String_IndexOf8(byte* /* System.String */ __self, byte* /* System.String */ value, int /* System.Int32 */ startIndex, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(valueDotNet, startIndex, comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_IndexOf9")]
	internal static int /* System.Int32 */ System_String_IndexOf9(byte* /* System.String */ __self, byte* /* System.String */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(valueDotNet, startIndex, count, comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_LastIndexOf")]
	internal static int /* System.Int32 */ System_String_LastIndexOf(byte* /* System.String */ __self, System.Char /* System.Char */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_LastIndexOf1")]
	internal static int /* System.Int32 */ System_String_LastIndexOf1(byte* /* System.String */ __self, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(value, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_LastIndexOf2")]
	internal static int /* System.Int32 */ System_String_LastIndexOf2(byte* /* System.String */ __self, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(value, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_LastIndexOf3")]
	internal static int /* System.Int32 */ System_String_LastIndexOf3(byte* /* System.String */ __self, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_LastIndexOf4")]
	internal static int /* System.Int32 */ System_String_LastIndexOf4(byte* /* System.String */ __self, byte* /* System.String */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(valueDotNet, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_LastIndexOf5")]
	internal static int /* System.Int32 */ System_String_LastIndexOf5(byte* /* System.String */ __self, byte* /* System.String */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(valueDotNet, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_LastIndexOf6")]
	internal static int /* System.Int32 */ System_String_LastIndexOf6(byte* /* System.String */ __self, byte* /* System.String */ value, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(valueDotNet, comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_LastIndexOf7")]
	internal static int /* System.Int32 */ System_String_LastIndexOf7(byte* /* System.String */ __self, byte* /* System.String */ value, int /* System.Int32 */ startIndex, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(valueDotNet, startIndex, comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_LastIndexOf8")]
	internal static int /* System.Int32 */ System_String_LastIndexOf8(byte* /* System.String */ __self, byte* /* System.String */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, System.StringComparison /* System.StringComparison */ comparisonType, void** /* System.Exception */ __outException)
	{
		System.String __selfDotNet = InteropUtils.ToDotNetString(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(valueDotNet, startIndex, count, comparisonType);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Create")]
	internal static byte* /* System.String */ System_String_Create(System.Char /* System.Char */ c, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.String __returnValue = new System.String(c, count);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Chars
	

	// TODO (Property): Length
	

	[UnmanagedCallersOnly(EntryPoint="System_String_Destroy")]
	internal static void /* System.Void */ System_String_Destroy(void* /* System.String */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Globalization_CultureInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_CreateSpecificCulture")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_CreateSpecificCulture(byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Globalization.CultureInfo __returnValue = System.Globalization.CultureInfo.CreateSpecificCulture(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_Equals")]
	internal static CBool /* System.Boolean */ System_Globalization_CultureInfo_Equals(void* /* System.Globalization.CultureInfo */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Globalization.CultureInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_GetHashCode")]
	internal static int /* System.Int32 */ System_Globalization_CultureInfo_GetHashCode(void* /* System.Globalization.CultureInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.CultureInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_ToString")]
	internal static byte* /* System.String */ System_Globalization_CultureInfo_ToString(void* /* System.Globalization.CultureInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.CultureInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_GetFormat")]
	internal static void* /* System.Object */ System_Globalization_CultureInfo_GetFormat(void* /* System.Globalization.CultureInfo */ __self, void* /* System.Type */ formatType, void** /* System.Exception */ __outException)
	{
		System.Globalization.CultureInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type formatTypeDotNet = InteropUtils.GetInstance<System.Type>(formatType);
	
	    try {
			System.Object __returnValue = __selfDotNet.GetFormat(formatTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_ClearCachedData")]
	internal static void /* System.Void */ System_Globalization_CultureInfo_ClearCachedData(void* /* System.Globalization.CultureInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.CultureInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.ClearCachedData();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_GetConsoleFallbackUICulture")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_GetConsoleFallbackUICulture(void* /* System.Globalization.CultureInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.CultureInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Globalization.CultureInfo __returnValue = __selfDotNet.GetConsoleFallbackUICulture();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_Clone")]
	internal static void* /* System.Object */ System_Globalization_CultureInfo_Clone(void* /* System.Globalization.CultureInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.CultureInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_ReadOnly")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_ReadOnly(void* /* System.Globalization.CultureInfo */ ci, void** /* System.Exception */ __outException)
	{
		System.Globalization.CultureInfo ciDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(ci);
	
	    try {
			System.Globalization.CultureInfo __returnValue = System.Globalization.CultureInfo.ReadOnly(ciDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_GetCultureInfo")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_GetCultureInfo(int /* System.Int32 */ culture, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Globalization.CultureInfo __returnValue = System.Globalization.CultureInfo.GetCultureInfo(culture);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_GetCultureInfo1")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_GetCultureInfo1(byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Globalization.CultureInfo __returnValue = System.Globalization.CultureInfo.GetCultureInfo(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_GetCultureInfo2")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_GetCultureInfo2(byte* /* System.String */ name, byte* /* System.String */ altName, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.String altNameDotNet = InteropUtils.ToDotNetString(altName);
	
	    try {
			System.Globalization.CultureInfo __returnValue = System.Globalization.CultureInfo.GetCultureInfo(nameDotNet, altNameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_GetCultureInfo3")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_GetCultureInfo3(byte* /* System.String */ name, CBool /* System.Boolean */ predefinedOnly, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Boolean predefinedOnlyDotNet = predefinedOnly.ToBool();
	
	    try {
			System.Globalization.CultureInfo __returnValue = System.Globalization.CultureInfo.GetCultureInfo(nameDotNet, predefinedOnlyDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_GetCultureInfoByIetfLanguageTag")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_GetCultureInfoByIetfLanguageTag(byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Globalization.CultureInfo __returnValue = System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_Create1")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_Create1(byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Globalization.CultureInfo __returnValue = new System.Globalization.CultureInfo(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_Create2")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_Create2(byte* /* System.String */ name, CBool /* System.Boolean */ useUserOverride, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Boolean useUserOverrideDotNet = useUserOverride.ToBool();
	
	    try {
			System.Globalization.CultureInfo __returnValue = new System.Globalization.CultureInfo(nameDotNet, useUserOverrideDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_Create3")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_Create3(int /* System.Int32 */ culture, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Globalization.CultureInfo __returnValue = new System.Globalization.CultureInfo(culture);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_Create4")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_Create4(int /* System.Int32 */ culture, CBool /* System.Boolean */ useUserOverride, void** /* System.Exception */ __outException)
	{
		System.Boolean useUserOverrideDotNet = useUserOverride.ToBool();
	
	    try {
			System.Globalization.CultureInfo __returnValue = new System.Globalization.CultureInfo(culture, useUserOverrideDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): CurrentCulture
	

	// TODO (Property): CurrentUICulture
	

	// TODO (Property): InstalledUICulture
	

	// TODO (Property): DefaultThreadCurrentCulture
	

	// TODO (Property): DefaultThreadCurrentUICulture
	

	// TODO (Property): InvariantCulture
	

	// TODO (Property): Parent
	

	// TODO (Property): LCID
	

	// TODO (Property): KeyboardLayoutId
	

	// TODO (Property): Name
	

	// TODO (Property): IetfLanguageTag
	

	// TODO (Property): DisplayName
	

	// TODO (Property): NativeName
	

	// TODO (Property): EnglishName
	

	// TODO (Property): TwoLetterISOLanguageName
	

	// TODO (Property): ThreeLetterISOLanguageName
	

	// TODO (Property): ThreeLetterWindowsLanguageName
	

	// TODO (Property): CompareInfo
	

	// TODO (Property): TextInfo
	

	// TODO (Property): IsNeutralCulture
	

	// TODO (Property): CultureTypes
	

	// TODO (Property): NumberFormat
	

	// TODO (Property): DateTimeFormat
	

	// TODO (Property): Calendar
	

	// TODO (Property): UseUserOverride
	

	// TODO (Property): IsReadOnly
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_CultureInfo_Destroy")]
	internal static void /* System.Void */ System_Globalization_CultureInfo_Destroy(void* /* System.Globalization.CultureInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Void
{
	[UnmanagedCallersOnly(EntryPoint="System_Void_Destroy")]
	internal static void /* System.Void */ System_Void_Destroy(void* /* System.Void */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Globalization_CompareInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetCompareInfo")]
	internal static void* /* System.Globalization.CompareInfo */ System_Globalization_CompareInfo_GetCompareInfo(int /* System.Int32 */ culture, void* /* System.Reflection.Assembly */ assembly, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly assemblyDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(assembly);
	
	    try {
			System.Globalization.CompareInfo __returnValue = System.Globalization.CompareInfo.GetCompareInfo(culture, assemblyDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetCompareInfo1")]
	internal static void* /* System.Globalization.CompareInfo */ System_Globalization_CompareInfo_GetCompareInfo1(byte* /* System.String */ name, void* /* System.Reflection.Assembly */ assembly, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Reflection.Assembly assemblyDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(assembly);
	
	    try {
			System.Globalization.CompareInfo __returnValue = System.Globalization.CompareInfo.GetCompareInfo(nameDotNet, assemblyDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetCompareInfo2")]
	internal static void* /* System.Globalization.CompareInfo */ System_Globalization_CompareInfo_GetCompareInfo2(int /* System.Int32 */ culture, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Globalization.CompareInfo __returnValue = System.Globalization.CompareInfo.GetCompareInfo(culture);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetCompareInfo3")]
	internal static void* /* System.Globalization.CompareInfo */ System_Globalization_CompareInfo_GetCompareInfo3(byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Globalization.CompareInfo __returnValue = System.Globalization.CompareInfo.GetCompareInfo(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IsSortable")]
	internal static CBool /* System.Boolean */ System_Globalization_CompareInfo_IsSortable(System.Char /* System.Char */ ch, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Boolean __returnValue = System.Globalization.CompareInfo.IsSortable(ch);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IsSortable1")]
	internal static CBool /* System.Boolean */ System_Globalization_CompareInfo_IsSortable1(byte* /* System.String */ text, void** /* System.Exception */ __outException)
	{
		System.String textDotNet = InteropUtils.ToDotNetString(text);
	
	    try {
			System.Boolean __returnValue = System.Globalization.CompareInfo.IsSortable(textDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_Compare")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_Compare(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ string1, byte* /* System.String */ string2, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String string1DotNet = InteropUtils.ToDotNetString(string1);
		System.String string2DotNet = InteropUtils.ToDotNetString(string2);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.Compare(string1DotNet, string2DotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_Compare1")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_Compare1(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ string1, byte* /* System.String */ string2, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String string1DotNet = InteropUtils.ToDotNetString(string1);
		System.String string2DotNet = InteropUtils.ToDotNetString(string2);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.Compare(string1DotNet, string2DotNet, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_Compare2")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_Compare2(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ string1, int /* System.Int32 */ offset1, int /* System.Int32 */ length1, byte* /* System.String */ string2, int /* System.Int32 */ offset2, int /* System.Int32 */ length2, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String string1DotNet = InteropUtils.ToDotNetString(string1);
		System.String string2DotNet = InteropUtils.ToDotNetString(string2);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.Compare(string1DotNet, offset1, length1, string2DotNet, offset2, length2);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_Compare3")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_Compare3(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ string1, int /* System.Int32 */ offset1, byte* /* System.String */ string2, int /* System.Int32 */ offset2, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String string1DotNet = InteropUtils.ToDotNetString(string1);
		System.String string2DotNet = InteropUtils.ToDotNetString(string2);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.Compare(string1DotNet, offset1, string2DotNet, offset2, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_Compare4")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_Compare4(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ string1, int /* System.Int32 */ offset1, byte* /* System.String */ string2, int /* System.Int32 */ offset2, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String string1DotNet = InteropUtils.ToDotNetString(string1);
		System.String string2DotNet = InteropUtils.ToDotNetString(string2);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.Compare(string1DotNet, offset1, string2DotNet, offset2);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_Compare5")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_Compare5(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ string1, int /* System.Int32 */ offset1, int /* System.Int32 */ length1, byte* /* System.String */ string2, int /* System.Int32 */ offset2, int /* System.Int32 */ length2, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String string1DotNet = InteropUtils.ToDotNetString(string1);
		System.String string2DotNet = InteropUtils.ToDotNetString(string2);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.Compare(string1DotNet, offset1, length1, string2DotNet, offset2, length2, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IsPrefix")]
	internal static CBool /* System.Boolean */ System_Globalization_CompareInfo_IsPrefix(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ prefix, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String prefixDotNet = InteropUtils.ToDotNetString(prefix);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsPrefix(sourceDotNet, prefixDotNet, options);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IsPrefix1")]
	internal static CBool /* System.Boolean */ System_Globalization_CompareInfo_IsPrefix1(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ prefix, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String prefixDotNet = InteropUtils.ToDotNetString(prefix);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsPrefix(sourceDotNet, prefixDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IsSuffix")]
	internal static CBool /* System.Boolean */ System_Globalization_CompareInfo_IsSuffix(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ suffix, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String suffixDotNet = InteropUtils.ToDotNetString(suffix);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsSuffix(sourceDotNet, suffixDotNet, options);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IsSuffix1")]
	internal static CBool /* System.Boolean */ System_Globalization_CompareInfo_IsSuffix1(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ suffix, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String suffixDotNet = InteropUtils.ToDotNetString(suffix);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsSuffix(sourceDotNet, suffixDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf1")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf1(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf2")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf2(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, value, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf3")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf3(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, valueDotNet, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf4")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf4(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, value, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf5")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf5(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, valueDotNet, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf6")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf6(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, value, startIndex, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf7")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf7(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, int /* System.Int32 */ startIndex, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, valueDotNet, startIndex, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf8")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf8(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, value, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf9")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf9(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, valueDotNet, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf10")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf10(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, value, startIndex, count, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_IndexOf11")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_IndexOf11(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.IndexOf(sourceDotNet, valueDotNet, startIndex, count, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf1")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf1(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf2")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf2(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, value, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf3")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf3(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, valueDotNet, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf4")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf4(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, value, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf5")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf5(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, valueDotNet, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf6")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf6(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, value, startIndex, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf7")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf7(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, int /* System.Int32 */ startIndex, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, valueDotNet, startIndex, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf8")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf8(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, value, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf9")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf9(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, valueDotNet, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf10")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf10(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Char /* System.Char */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, value, startIndex, count, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_LastIndexOf11")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_LastIndexOf11(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, byte* /* System.String */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
		System.String valueDotNet = InteropUtils.ToDotNetString(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.LastIndexOf(sourceDotNet, valueDotNet, startIndex, count, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetSortKey")]
	internal static void* /* System.Globalization.SortKey */ System_Globalization_CompareInfo_GetSortKey(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Globalization.SortKey __returnValue = __selfDotNet.GetSortKey(sourceDotNet, options);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetSortKey1")]
	internal static void* /* System.Globalization.SortKey */ System_Globalization_CompareInfo_GetSortKey1(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Globalization.SortKey __returnValue = __selfDotNet.GetSortKey(sourceDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_Equals")]
	internal static CBool /* System.Boolean */ System_Globalization_CompareInfo_Equals(void* /* System.Globalization.CompareInfo */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetHashCode")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_GetHashCode(void* /* System.Globalization.CompareInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetHashCode1")]
	internal static int /* System.Int32 */ System_Globalization_CompareInfo_GetHashCode1(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, System.Globalization.CompareOptions /* System.Globalization.CompareOptions */ options, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sourceDotNet = InteropUtils.ToDotNetString(source);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode(sourceDotNet, options);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_ToString")]
	internal static byte* /* System.String */ System_Globalization_CompareInfo_ToString(void* /* System.Globalization.CompareInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.CompareInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.CompareInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Name
	

	// TODO (Property): Version
	

	// TODO (Property): LCID
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_CompareInfo_Destroy")]
	internal static void /* System.Void */ System_Globalization_CompareInfo_Destroy(void* /* System.Globalization.CompareInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_Assembly
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_Load")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_Load(byte* /* System.String */ assemblyString, void** /* System.Exception */ __outException)
	{
		System.String assemblyStringDotNet = InteropUtils.ToDotNetString(assemblyString);
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.Load(assemblyStringDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_LoadWithPartialName")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_LoadWithPartialName(byte* /* System.String */ partialName, void** /* System.Exception */ __outException)
	{
		System.String partialNameDotNet = InteropUtils.ToDotNetString(partialName);
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.LoadWithPartialName(partialNameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_Load1")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_Load1(void* /* System.Reflection.AssemblyName */ assemblyRef, void** /* System.Exception */ __outException)
	{
		System.Reflection.AssemblyName assemblyRefDotNet = InteropUtils.GetInstance<System.Reflection.AssemblyName>(assemblyRef);
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.Load(assemblyRefDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetExecutingAssembly")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_GetExecutingAssembly(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.GetExecutingAssembly();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetCallingAssembly")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_GetCallingAssembly(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.GetCallingAssembly();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetManifestResourceInfo")]
	internal static void* /* System.Reflection.ManifestResourceInfo */ System_Reflection_Assembly_GetManifestResourceInfo(void* /* System.Reflection.Assembly */ __self, byte* /* System.String */ resourceName, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String resourceNameDotNet = InteropUtils.ToDotNetString(resourceName);
	
	    try {
			System.Reflection.ManifestResourceInfo __returnValue = __selfDotNet.GetManifestResourceInfo(resourceNameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetManifestResourceStream")]
	internal static void* /* System.IO.Stream */ System_Reflection_Assembly_GetManifestResourceStream(void* /* System.Reflection.Assembly */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.IO.Stream __returnValue = __selfDotNet.GetManifestResourceStream(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetManifestResourceStream1")]
	internal static void* /* System.IO.Stream */ System_Reflection_Assembly_GetManifestResourceStream1(void* /* System.Reflection.Assembly */ __self, void* /* System.Type */ type, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.IO.Stream __returnValue = __selfDotNet.GetManifestResourceStream(typeDotNet, nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetName")]
	internal static void* /* System.Reflection.AssemblyName */ System_Reflection_Assembly_GetName(void* /* System.Reflection.Assembly */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.AssemblyName __returnValue = __selfDotNet.GetName();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetName1")]
	internal static void* /* System.Reflection.AssemblyName */ System_Reflection_Assembly_GetName1(void* /* System.Reflection.Assembly */ __self, CBool /* System.Boolean */ copiedName, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Boolean copiedNameDotNet = copiedName.ToBool();
	
	    try {
			System.Reflection.AssemblyName __returnValue = __selfDotNet.GetName(copiedNameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetType")]
	internal static void* /* System.Type */ System_Reflection_Assembly_GetType(void* /* System.Reflection.Assembly */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Type __returnValue = __selfDotNet.GetType(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetType1")]
	internal static void* /* System.Type */ System_Reflection_Assembly_GetType1(void* /* System.Reflection.Assembly */ __self, byte* /* System.String */ name, CBool /* System.Boolean */ throwOnError, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Boolean throwOnErrorDotNet = throwOnError.ToBool();
	
	    try {
			System.Type __returnValue = __selfDotNet.GetType(nameDotNet, throwOnErrorDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetType2")]
	internal static void* /* System.Type */ System_Reflection_Assembly_GetType2(void* /* System.Reflection.Assembly */ __self, byte* /* System.String */ name, CBool /* System.Boolean */ throwOnError, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Boolean throwOnErrorDotNet = throwOnError.ToBool();
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Type __returnValue = __selfDotNet.GetType(nameDotNet, throwOnErrorDotNet, ignoreCaseDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_IsDefined")]
	internal static CBool /* System.Boolean */ System_Reflection_Assembly_IsDefined(void* /* System.Reflection.Assembly */ __self, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsDefined(attributeTypeDotNet, inheritDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_CreateInstance")]
	internal static void* /* System.Object */ System_Reflection_Assembly_CreateInstance(void* /* System.Reflection.Assembly */ __self, byte* /* System.String */ typeName, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String typeNameDotNet = InteropUtils.ToDotNetString(typeName);
	
	    try {
			System.Object __returnValue = __selfDotNet.CreateInstance(typeNameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_CreateInstance1")]
	internal static void* /* System.Object */ System_Reflection_Assembly_CreateInstance1(void* /* System.Reflection.Assembly */ __self, byte* /* System.String */ typeName, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String typeNameDotNet = InteropUtils.ToDotNetString(typeName);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Object __returnValue = __selfDotNet.CreateInstance(typeNameDotNet, ignoreCaseDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetModule")]
	internal static void* /* System.Reflection.Module */ System_Reflection_Assembly_GetModule(void* /* System.Reflection.Assembly */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.Module __returnValue = __selfDotNet.GetModule(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetSatelliteAssembly")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_GetSatelliteAssembly(void* /* System.Reflection.Assembly */ __self, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.Reflection.Assembly __returnValue = __selfDotNet.GetSatelliteAssembly(cultureDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetSatelliteAssembly1")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_GetSatelliteAssembly1(void* /* System.Reflection.Assembly */ __self, void* /* System.Globalization.CultureInfo */ culture, void* /* System.Version */ version, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
		System.Version versionDotNet = InteropUtils.GetInstance<System.Version>(version);
	
	    try {
			System.Reflection.Assembly __returnValue = __selfDotNet.GetSatelliteAssembly(cultureDotNet, versionDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetFile")]
	internal static void* /* System.IO.FileStream */ System_Reflection_Assembly_GetFile(void* /* System.Reflection.Assembly */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.IO.FileStream __returnValue = __selfDotNet.GetFile(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_ToString")]
	internal static byte* /* System.String */ System_Reflection_Assembly_ToString(void* /* System.Reflection.Assembly */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_Equals")]
	internal static CBool /* System.Boolean */ System_Reflection_Assembly_Equals(void* /* System.Reflection.Assembly */ __self, void* /* System.Object */ o, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object oDotNet = InteropUtils.GetInstance<System.Object>(o);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(oDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetHashCode")]
	internal static int /* System.Int32 */ System_Reflection_Assembly_GetHashCode(void* /* System.Reflection.Assembly */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly __selfDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_op_Equality")]
	internal static CBool /* System.Boolean */ System_Reflection_Assembly_op_Equality(void* /* System.Reflection.Assembly */ left, void* /* System.Reflection.Assembly */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly leftDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(left);
		System.Reflection.Assembly rightDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.Assembly.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Reflection_Assembly_op_Inequality(void* /* System.Reflection.Assembly */ left, void* /* System.Reflection.Assembly */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly leftDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(left);
		System.Reflection.Assembly rightDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.Assembly.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_CreateQualifiedName")]
	internal static byte* /* System.String */ System_Reflection_Assembly_CreateQualifiedName(byte* /* System.String */ assemblyName, byte* /* System.String */ typeName, void** /* System.Exception */ __outException)
	{
		System.String assemblyNameDotNet = InteropUtils.ToDotNetString(assemblyName);
		System.String typeNameDotNet = InteropUtils.ToDotNetString(typeName);
	
	    try {
			System.String __returnValue = System.Reflection.Assembly.CreateQualifiedName(assemblyNameDotNet, typeNameDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetAssembly")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_GetAssembly(void* /* System.Type */ type, void** /* System.Exception */ __outException)
	{
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.GetAssembly(typeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_GetEntryAssembly")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_GetEntryAssembly(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.GetEntryAssembly();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_LoadFile")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_LoadFile(byte* /* System.String */ path, void** /* System.Exception */ __outException)
	{
		System.String pathDotNet = InteropUtils.ToDotNetString(path);
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.LoadFile(pathDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_LoadFrom")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_LoadFrom(byte* /* System.String */ assemblyFile, void** /* System.Exception */ __outException)
	{
		System.String assemblyFileDotNet = InteropUtils.ToDotNetString(assemblyFile);
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.LoadFrom(assemblyFileDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_UnsafeLoadFrom")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_UnsafeLoadFrom(byte* /* System.String */ assemblyFile, void** /* System.Exception */ __outException)
	{
		System.String assemblyFileDotNet = InteropUtils.ToDotNetString(assemblyFile);
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.UnsafeLoadFrom(assemblyFileDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_ReflectionOnlyLoad")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_ReflectionOnlyLoad(byte* /* System.String */ assemblyString, void** /* System.Exception */ __outException)
	{
		System.String assemblyStringDotNet = InteropUtils.ToDotNetString(assemblyString);
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.ReflectionOnlyLoad(assemblyStringDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Assembly_ReflectionOnlyLoadFrom")]
	internal static void* /* System.Reflection.Assembly */ System_Reflection_Assembly_ReflectionOnlyLoadFrom(byte* /* System.String */ assemblyFile, void** /* System.Exception */ __outException)
	{
		System.String assemblyFileDotNet = InteropUtils.ToDotNetString(assemblyFile);
	
	    try {
			System.Reflection.Assembly __returnValue = System.Reflection.Assembly.ReflectionOnlyLoadFrom(assemblyFileDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): CodeBase
	

	// TODO (Property): EntryPoint
	

	// TODO (Property): FullName
	

	// TODO (Property): ImageRuntimeVersion
	

	// TODO (Property): IsDynamic
	

	// TODO (Property): Location
	

	// TODO (Property): ReflectionOnly
	

	// TODO (Property): IsCollectible
	

	// TODO (Property): IsFullyTrusted
	

	// TODO (Property): EscapedCodeBase
	

	// TODO (Property): ManifestModule
	

	// TODO (Property): GlobalAssemblyCache
	

	// TODO (Property): HostContext
	

	// TODO (Property): SecurityRuleSet
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_Assembly_Destroy")]
	internal static void /* System.Void */ System_Reflection_Assembly_Destroy(void* /* System.Reflection.Assembly */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_AssemblyName
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_AssemblyName_Clone")]
	internal static void* /* System.Object */ System_Reflection_AssemblyName_Clone(void* /* System.Reflection.AssemblyName */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.AssemblyName __selfDotNet = InteropUtils.GetInstance<System.Reflection.AssemblyName>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_AssemblyName_GetAssemblyName")]
	internal static void* /* System.Reflection.AssemblyName */ System_Reflection_AssemblyName_GetAssemblyName(byte* /* System.String */ assemblyFile, void** /* System.Exception */ __outException)
	{
		System.String assemblyFileDotNet = InteropUtils.ToDotNetString(assemblyFile);
	
	    try {
			System.Reflection.AssemblyName __returnValue = System.Reflection.AssemblyName.GetAssemblyName(assemblyFileDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_AssemblyName_ToString")]
	internal static byte* /* System.String */ System_Reflection_AssemblyName_ToString(void* /* System.Reflection.AssemblyName */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.AssemblyName __selfDotNet = InteropUtils.GetInstance<System.Reflection.AssemblyName>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_AssemblyName_OnDeserialization")]
	internal static void /* System.Void */ System_Reflection_AssemblyName_OnDeserialization(void* /* System.Reflection.AssemblyName */ __self, void* /* System.Object */ sender, void** /* System.Exception */ __outException)
	{
		System.Reflection.AssemblyName __selfDotNet = InteropUtils.GetInstance<System.Reflection.AssemblyName>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object senderDotNet = InteropUtils.GetInstance<System.Object>(sender);
	
	    try {
			__selfDotNet.OnDeserialization(senderDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_AssemblyName_ReferenceMatchesDefinition")]
	internal static CBool /* System.Boolean */ System_Reflection_AssemblyName_ReferenceMatchesDefinition(void* /* System.Reflection.AssemblyName */ reference, void* /* System.Reflection.AssemblyName */ definition, void** /* System.Exception */ __outException)
	{
		System.Reflection.AssemblyName referenceDotNet = InteropUtils.GetInstance<System.Reflection.AssemblyName>(reference);
		System.Reflection.AssemblyName definitionDotNet = InteropUtils.GetInstance<System.Reflection.AssemblyName>(definition);
	
	    try {
			System.Boolean __returnValue = System.Reflection.AssemblyName.ReferenceMatchesDefinition(referenceDotNet, definitionDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_AssemblyName_Create")]
	internal static void* /* System.Reflection.AssemblyName */ System_Reflection_AssemblyName_Create(byte* /* System.String */ assemblyName, void** /* System.Exception */ __outException)
	{
		System.String assemblyNameDotNet = InteropUtils.ToDotNetString(assemblyName);
	
	    try {
			System.Reflection.AssemblyName __returnValue = new System.Reflection.AssemblyName(assemblyNameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_AssemblyName_Create1")]
	internal static void* /* System.Reflection.AssemblyName */ System_Reflection_AssemblyName_Create1(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Reflection.AssemblyName __returnValue = new System.Reflection.AssemblyName();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Name
	

	// TODO (Property): Version
	

	// TODO (Property): CultureInfo
	

	// TODO (Property): CultureName
	

	// TODO (Property): CodeBase
	

	// TODO (Property): EscapedCodeBase
	

	// TODO (Property): ProcessorArchitecture
	

	// TODO (Property): ContentType
	

	// TODO (Property): Flags
	

	// TODO (Property): HashAlgorithm
	

	// TODO (Property): VersionCompatibility
	

	// TODO (Property): KeyPair
	

	// TODO (Property): FullName
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_AssemblyName_Destroy")]
	internal static void /* System.Void */ System_Reflection_AssemblyName_Destroy(void* /* System.Reflection.AssemblyName */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Version
{
	[UnmanagedCallersOnly(EntryPoint = "System_Version_Clone")]
	internal static void* /* System.Object */ System_Version_Clone(void* /* System.Version */ __self, void** /* System.Exception */ __outException)
	{
		System.Version __selfDotNet = InteropUtils.GetInstance<System.Version>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_CompareTo")]
	internal static int /* System.Int32 */ System_Version_CompareTo(void* /* System.Version */ __self, void* /* System.Object */ version, void** /* System.Exception */ __outException)
	{
		System.Version __selfDotNet = InteropUtils.GetInstance<System.Version>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object versionDotNet = InteropUtils.GetInstance<System.Object>(version);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.CompareTo(versionDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_CompareTo1")]
	internal static int /* System.Int32 */ System_Version_CompareTo1(void* /* System.Version */ __self, void* /* System.Version */ value, void** /* System.Exception */ __outException)
	{
		System.Version __selfDotNet = InteropUtils.GetInstance<System.Version>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Version valueDotNet = InteropUtils.GetInstance<System.Version>(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.CompareTo(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Equals")]
	internal static CBool /* System.Boolean */ System_Version_Equals(void* /* System.Version */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Version __selfDotNet = InteropUtils.GetInstance<System.Version>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Equals1")]
	internal static CBool /* System.Boolean */ System_Version_Equals1(void* /* System.Version */ __self, void* /* System.Version */ obj, void** /* System.Exception */ __outException)
	{
		System.Version __selfDotNet = InteropUtils.GetInstance<System.Version>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Version objDotNet = InteropUtils.GetInstance<System.Version>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_GetHashCode")]
	internal static int /* System.Int32 */ System_Version_GetHashCode(void* /* System.Version */ __self, void** /* System.Exception */ __outException)
	{
		System.Version __selfDotNet = InteropUtils.GetInstance<System.Version>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_ToString")]
	internal static byte* /* System.String */ System_Version_ToString(void* /* System.Version */ __self, void** /* System.Exception */ __outException)
	{
		System.Version __selfDotNet = InteropUtils.GetInstance<System.Version>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_ToString1")]
	internal static byte* /* System.String */ System_Version_ToString1(void* /* System.Version */ __self, int /* System.Int32 */ fieldCount, void** /* System.Exception */ __outException)
	{
		System.Version __selfDotNet = InteropUtils.GetInstance<System.Version>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString(fieldCount);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Parse")]
	internal static void* /* System.Version */ System_Version_Parse(byte* /* System.String */ input, void** /* System.Exception */ __outException)
	{
		System.String inputDotNet = InteropUtils.ToDotNetString(input);
	
	    try {
			System.Version __returnValue = System.Version.Parse(inputDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_op_Equality")]
	internal static CBool /* System.Boolean */ System_Version_op_Equality(void* /* System.Version */ v1, void* /* System.Version */ v2, void** /* System.Exception */ __outException)
	{
		System.Version v1DotNet = InteropUtils.GetInstance<System.Version>(v1);
		System.Version v2DotNet = InteropUtils.GetInstance<System.Version>(v2);
	
	    try {
			System.Boolean __returnValue = System.Version.op_Equality(v1DotNet, v2DotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Version_op_Inequality(void* /* System.Version */ v1, void* /* System.Version */ v2, void** /* System.Exception */ __outException)
	{
		System.Version v1DotNet = InteropUtils.GetInstance<System.Version>(v1);
		System.Version v2DotNet = InteropUtils.GetInstance<System.Version>(v2);
	
	    try {
			System.Boolean __returnValue = System.Version.op_Inequality(v1DotNet, v2DotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_op_LessThan")]
	internal static CBool /* System.Boolean */ System_Version_op_LessThan(void* /* System.Version */ v1, void* /* System.Version */ v2, void** /* System.Exception */ __outException)
	{
		System.Version v1DotNet = InteropUtils.GetInstance<System.Version>(v1);
		System.Version v2DotNet = InteropUtils.GetInstance<System.Version>(v2);
	
	    try {
			System.Boolean __returnValue = System.Version.op_LessThan(v1DotNet, v2DotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_op_LessThanOrEqual")]
	internal static CBool /* System.Boolean */ System_Version_op_LessThanOrEqual(void* /* System.Version */ v1, void* /* System.Version */ v2, void** /* System.Exception */ __outException)
	{
		System.Version v1DotNet = InteropUtils.GetInstance<System.Version>(v1);
		System.Version v2DotNet = InteropUtils.GetInstance<System.Version>(v2);
	
	    try {
			System.Boolean __returnValue = System.Version.op_LessThanOrEqual(v1DotNet, v2DotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_op_GreaterThan")]
	internal static CBool /* System.Boolean */ System_Version_op_GreaterThan(void* /* System.Version */ v1, void* /* System.Version */ v2, void** /* System.Exception */ __outException)
	{
		System.Version v1DotNet = InteropUtils.GetInstance<System.Version>(v1);
		System.Version v2DotNet = InteropUtils.GetInstance<System.Version>(v2);
	
	    try {
			System.Boolean __returnValue = System.Version.op_GreaterThan(v1DotNet, v2DotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_op_GreaterThanOrEqual")]
	internal static CBool /* System.Boolean */ System_Version_op_GreaterThanOrEqual(void* /* System.Version */ v1, void* /* System.Version */ v2, void** /* System.Exception */ __outException)
	{
		System.Version v1DotNet = InteropUtils.GetInstance<System.Version>(v1);
		System.Version v2DotNet = InteropUtils.GetInstance<System.Version>(v2);
	
	    try {
			System.Boolean __returnValue = System.Version.op_GreaterThanOrEqual(v1DotNet, v2DotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Create")]
	internal static void* /* System.Version */ System_Version_Create(int /* System.Int32 */ major, int /* System.Int32 */ minor, int /* System.Int32 */ build, int /* System.Int32 */ revision, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Version __returnValue = new System.Version(major, minor, build, revision);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Create1")]
	internal static void* /* System.Version */ System_Version_Create1(int /* System.Int32 */ major, int /* System.Int32 */ minor, int /* System.Int32 */ build, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Version __returnValue = new System.Version(major, minor, build);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Create2")]
	internal static void* /* System.Version */ System_Version_Create2(int /* System.Int32 */ major, int /* System.Int32 */ minor, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Version __returnValue = new System.Version(major, minor);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Create3")]
	internal static void* /* System.Version */ System_Version_Create3(byte* /* System.String */ version, void** /* System.Exception */ __outException)
	{
		System.String versionDotNet = InteropUtils.ToDotNetString(version);
	
	    try {
			System.Version __returnValue = new System.Version(versionDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Create4")]
	internal static void* /* System.Version */ System_Version_Create4(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Version __returnValue = new System.Version();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Major
	

	// TODO (Property): Minor
	

	// TODO (Property): Build
	

	// TODO (Property): Revision
	

	// TODO (Property): MajorRevision
	

	// TODO (Property): MinorRevision
	

	[UnmanagedCallersOnly(EntryPoint="System_Version_Destroy")]
	internal static void /* System.Void */ System_Version_Destroy(void* /* System.Version */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_IFormatProvider
{
	[UnmanagedCallersOnly(EntryPoint = "System_IFormatProvider_GetFormat")]
	internal static void* /* System.Object */ System_IFormatProvider_GetFormat(void* /* System.IFormatProvider */ __self, void* /* System.Type */ formatType, void** /* System.Exception */ __outException)
	{
		System.IFormatProvider __selfDotNet = InteropUtils.GetInstance<System.IFormatProvider>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type formatTypeDotNet = InteropUtils.GetInstance<System.Type>(formatType);
	
	    try {
			System.Object __returnValue = __selfDotNet.GetFormat(formatTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_IFormatProvider_Destroy")]
	internal static void /* System.Void */ System_IFormatProvider_Destroy(void* /* System.IFormatProvider */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}









internal static unsafe class System_Reflection_StrongNameKeyPair
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_StrongNameKeyPair_Create")]
	internal static void* /* System.Reflection.StrongNameKeyPair */ System_Reflection_StrongNameKeyPair_Create(void* /* System.IO.FileStream */ keyPairFile, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream keyPairFileDotNet = InteropUtils.GetInstance<System.IO.FileStream>(keyPairFile);
	
	    try {
			System.Reflection.StrongNameKeyPair __returnValue = new System.Reflection.StrongNameKeyPair(keyPairFileDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_StrongNameKeyPair_Create1")]
	internal static void* /* System.Reflection.StrongNameKeyPair */ System_Reflection_StrongNameKeyPair_Create1(byte* /* System.String */ keyPairContainer, void** /* System.Exception */ __outException)
	{
		System.String keyPairContainerDotNet = InteropUtils.ToDotNetString(keyPairContainer);
	
	    try {
			System.Reflection.StrongNameKeyPair __returnValue = new System.Reflection.StrongNameKeyPair(keyPairContainerDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_StrongNameKeyPair_Destroy")]
	internal static void /* System.Void */ System_Reflection_StrongNameKeyPair_Destroy(void* /* System.Reflection.StrongNameKeyPair */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_IO_FileStream
{
	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Lock")]
	internal static void /* System.Void */ System_IO_FileStream_Lock(void* /* System.IO.FileStream */ __self, long /* System.Int64 */ position, long /* System.Int64 */ length, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Lock(position, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Unlock")]
	internal static void /* System.Void */ System_IO_FileStream_Unlock(void* /* System.IO.FileStream */ __self, long /* System.Int64 */ position, long /* System.Int64 */ length, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Unlock(position, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Flush")]
	internal static void /* System.Void */ System_IO_FileStream_Flush(void* /* System.IO.FileStream */ __self, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Flush();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Flush1")]
	internal static void /* System.Void */ System_IO_FileStream_Flush1(void* /* System.IO.FileStream */ __self, CBool /* System.Boolean */ flushToDisk, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Boolean flushToDiskDotNet = flushToDisk.ToBool();
	
	    try {
			__selfDotNet.Flush(flushToDiskDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_SetLength")]
	internal static void /* System.Void */ System_IO_FileStream_SetLength(void* /* System.IO.FileStream */ __self, long /* System.Int64 */ value, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.SetLength(value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_ReadByte")]
	internal static int /* System.Int32 */ System_IO_FileStream_ReadByte(void* /* System.IO.FileStream */ __self, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.ReadByte();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_WriteByte")]
	internal static void /* System.Void */ System_IO_FileStream_WriteByte(void* /* System.IO.FileStream */ __self, byte /* System.Byte */ value, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.WriteByte(value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_CopyTo")]
	internal static void /* System.Void */ System_IO_FileStream_CopyTo(void* /* System.IO.FileStream */ __self, void* /* System.IO.Stream */ destination, int /* System.Int32 */ bufferSize, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IO.Stream destinationDotNet = InteropUtils.GetInstance<System.IO.Stream>(destination);
	
	    try {
			__selfDotNet.CopyTo(destinationDotNet, bufferSize);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_EndRead")]
	internal static int /* System.Int32 */ System_IO_FileStream_EndRead(void* /* System.IO.FileStream */ __self, void* /* System.IAsyncResult */ asyncResult, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IAsyncResult asyncResultDotNet = InteropUtils.GetInstance<System.IAsyncResult>(asyncResult);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.EndRead(asyncResultDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_EndWrite")]
	internal static void /* System.Void */ System_IO_FileStream_EndWrite(void* /* System.IO.FileStream */ __self, void* /* System.IAsyncResult */ asyncResult, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IAsyncResult asyncResultDotNet = InteropUtils.GetInstance<System.IAsyncResult>(asyncResult);
	
	    try {
			__selfDotNet.EndWrite(asyncResultDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Seek")]
	internal static long /* System.Int64 */ System_IO_FileStream_Seek(void* /* System.IO.FileStream */ __self, long /* System.Int64 */ offset, System.IO.SeekOrigin /* System.IO.SeekOrigin */ origin, void** /* System.Exception */ __outException)
	{
		System.IO.FileStream __selfDotNet = InteropUtils.GetInstance<System.IO.FileStream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int64 __returnValue = __selfDotNet.Seek(offset, origin);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create(System.IntPtr /* System.IntPtr */ handle, System.IO.FileAccess /* System.IO.FileAccess */ access, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(handle, access);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create1")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create1(System.IntPtr /* System.IntPtr */ handle, System.IO.FileAccess /* System.IO.FileAccess */ access, CBool /* System.Boolean */ ownsHandle, void** /* System.Exception */ __outException)
	{
		System.Boolean ownsHandleDotNet = ownsHandle.ToBool();
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(handle, access, ownsHandleDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create2")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create2(System.IntPtr /* System.IntPtr */ handle, System.IO.FileAccess /* System.IO.FileAccess */ access, CBool /* System.Boolean */ ownsHandle, int /* System.Int32 */ bufferSize, void** /* System.Exception */ __outException)
	{
		System.Boolean ownsHandleDotNet = ownsHandle.ToBool();
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(handle, access, ownsHandleDotNet, bufferSize);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create3")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create3(System.IntPtr /* System.IntPtr */ handle, System.IO.FileAccess /* System.IO.FileAccess */ access, CBool /* System.Boolean */ ownsHandle, int /* System.Int32 */ bufferSize, CBool /* System.Boolean */ isAsync, void** /* System.Exception */ __outException)
	{
		System.Boolean ownsHandleDotNet = ownsHandle.ToBool();
		System.Boolean isAsyncDotNet = isAsync.ToBool();
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(handle, access, ownsHandleDotNet, bufferSize, isAsyncDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create4")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create4(void* /* Microsoft.Win32.SafeHandles.SafeFileHandle */ handle, System.IO.FileAccess /* System.IO.FileAccess */ access, void** /* System.Exception */ __outException)
	{
		Microsoft.Win32.SafeHandles.SafeFileHandle handleDotNet = InteropUtils.GetInstance<Microsoft.Win32.SafeHandles.SafeFileHandle>(handle);
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(handleDotNet, access);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create5")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create5(void* /* Microsoft.Win32.SafeHandles.SafeFileHandle */ handle, System.IO.FileAccess /* System.IO.FileAccess */ access, int /* System.Int32 */ bufferSize, void** /* System.Exception */ __outException)
	{
		Microsoft.Win32.SafeHandles.SafeFileHandle handleDotNet = InteropUtils.GetInstance<Microsoft.Win32.SafeHandles.SafeFileHandle>(handle);
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(handleDotNet, access, bufferSize);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create6")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create6(void* /* Microsoft.Win32.SafeHandles.SafeFileHandle */ handle, System.IO.FileAccess /* System.IO.FileAccess */ access, int /* System.Int32 */ bufferSize, CBool /* System.Boolean */ isAsync, void** /* System.Exception */ __outException)
	{
		Microsoft.Win32.SafeHandles.SafeFileHandle handleDotNet = InteropUtils.GetInstance<Microsoft.Win32.SafeHandles.SafeFileHandle>(handle);
		System.Boolean isAsyncDotNet = isAsync.ToBool();
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(handleDotNet, access, bufferSize, isAsyncDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create7")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create7(byte* /* System.String */ path, System.IO.FileMode /* System.IO.FileMode */ mode, void** /* System.Exception */ __outException)
	{
		System.String pathDotNet = InteropUtils.ToDotNetString(path);
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(pathDotNet, mode);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create8")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create8(byte* /* System.String */ path, System.IO.FileMode /* System.IO.FileMode */ mode, System.IO.FileAccess /* System.IO.FileAccess */ access, void** /* System.Exception */ __outException)
	{
		System.String pathDotNet = InteropUtils.ToDotNetString(path);
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(pathDotNet, mode, access);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create9")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create9(byte* /* System.String */ path, System.IO.FileMode /* System.IO.FileMode */ mode, System.IO.FileAccess /* System.IO.FileAccess */ access, System.IO.FileShare /* System.IO.FileShare */ share, void** /* System.Exception */ __outException)
	{
		System.String pathDotNet = InteropUtils.ToDotNetString(path);
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(pathDotNet, mode, access, share);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create10")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create10(byte* /* System.String */ path, System.IO.FileMode /* System.IO.FileMode */ mode, System.IO.FileAccess /* System.IO.FileAccess */ access, System.IO.FileShare /* System.IO.FileShare */ share, int /* System.Int32 */ bufferSize, void** /* System.Exception */ __outException)
	{
		System.String pathDotNet = InteropUtils.ToDotNetString(path);
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(pathDotNet, mode, access, share, bufferSize);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create11")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create11(byte* /* System.String */ path, System.IO.FileMode /* System.IO.FileMode */ mode, System.IO.FileAccess /* System.IO.FileAccess */ access, System.IO.FileShare /* System.IO.FileShare */ share, int /* System.Int32 */ bufferSize, CBool /* System.Boolean */ useAsync, void** /* System.Exception */ __outException)
	{
		System.String pathDotNet = InteropUtils.ToDotNetString(path);
		System.Boolean useAsyncDotNet = useAsync.ToBool();
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(pathDotNet, mode, access, share, bufferSize, useAsyncDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create12")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create12(byte* /* System.String */ path, System.IO.FileMode /* System.IO.FileMode */ mode, System.IO.FileAccess /* System.IO.FileAccess */ access, System.IO.FileShare /* System.IO.FileShare */ share, int /* System.Int32 */ bufferSize, System.IO.FileOptions /* System.IO.FileOptions */ options, void** /* System.Exception */ __outException)
	{
		System.String pathDotNet = InteropUtils.ToDotNetString(path);
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(pathDotNet, mode, access, share, bufferSize, options);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create13")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create13(byte* /* System.String */ path, void* /* System.IO.FileStreamOptions */ options, void** /* System.Exception */ __outException)
	{
		System.String pathDotNet = InteropUtils.ToDotNetString(path);
		System.IO.FileStreamOptions optionsDotNet = InteropUtils.GetInstance<System.IO.FileStreamOptions>(options);
	
	    try {
			System.IO.FileStream __returnValue = new System.IO.FileStream(pathDotNet, optionsDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Handle
	

	// TODO (Property): CanRead
	

	// TODO (Property): CanWrite
	

	// TODO (Property): SafeFileHandle
	

	// TODO (Property): Name
	

	// TODO (Property): IsAsync
	

	// TODO (Property): Length
	

	// TODO (Property): Position
	

	// TODO (Property): CanSeek
	

	[UnmanagedCallersOnly(EntryPoint="System_IO_FileStream_Destroy")]
	internal static void /* System.Void */ System_IO_FileStream_Destroy(void* /* System.IO.FileStream */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_IO_Stream
{
	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_CopyTo")]
	internal static void /* System.Void */ System_IO_Stream_CopyTo(void* /* System.IO.Stream */ __self, void* /* System.IO.Stream */ destination, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IO.Stream destinationDotNet = InteropUtils.GetInstance<System.IO.Stream>(destination);
	
	    try {
			__selfDotNet.CopyTo(destinationDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_CopyTo1")]
	internal static void /* System.Void */ System_IO_Stream_CopyTo1(void* /* System.IO.Stream */ __self, void* /* System.IO.Stream */ destination, int /* System.Int32 */ bufferSize, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IO.Stream destinationDotNet = InteropUtils.GetInstance<System.IO.Stream>(destination);
	
	    try {
			__selfDotNet.CopyTo(destinationDotNet, bufferSize);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_CopyToAsync")]
	internal static void* /* System.Threading.Tasks.Task */ System_IO_Stream_CopyToAsync(void* /* System.IO.Stream */ __self, void* /* System.IO.Stream */ destination, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IO.Stream destinationDotNet = InteropUtils.GetInstance<System.IO.Stream>(destination);
	
	    try {
			System.Threading.Tasks.Task __returnValue = __selfDotNet.CopyToAsync(destinationDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_CopyToAsync1")]
	internal static void* /* System.Threading.Tasks.Task */ System_IO_Stream_CopyToAsync1(void* /* System.IO.Stream */ __self, void* /* System.IO.Stream */ destination, int /* System.Int32 */ bufferSize, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IO.Stream destinationDotNet = InteropUtils.GetInstance<System.IO.Stream>(destination);
	
	    try {
			System.Threading.Tasks.Task __returnValue = __selfDotNet.CopyToAsync(destinationDotNet, bufferSize);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_Dispose")]
	internal static void /* System.Void */ System_IO_Stream_Dispose(void* /* System.IO.Stream */ __self, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Dispose();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_Close")]
	internal static void /* System.Void */ System_IO_Stream_Close(void* /* System.IO.Stream */ __self, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Close();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_Flush")]
	internal static void /* System.Void */ System_IO_Stream_Flush(void* /* System.IO.Stream */ __self, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Flush();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_FlushAsync")]
	internal static void* /* System.Threading.Tasks.Task */ System_IO_Stream_FlushAsync(void* /* System.IO.Stream */ __self, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Threading.Tasks.Task __returnValue = __selfDotNet.FlushAsync();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_EndRead")]
	internal static int /* System.Int32 */ System_IO_Stream_EndRead(void* /* System.IO.Stream */ __self, void* /* System.IAsyncResult */ asyncResult, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IAsyncResult asyncResultDotNet = InteropUtils.GetInstance<System.IAsyncResult>(asyncResult);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.EndRead(asyncResultDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_EndWrite")]
	internal static void /* System.Void */ System_IO_Stream_EndWrite(void* /* System.IO.Stream */ __self, void* /* System.IAsyncResult */ asyncResult, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.IAsyncResult asyncResultDotNet = InteropUtils.GetInstance<System.IAsyncResult>(asyncResult);
	
	    try {
			__selfDotNet.EndWrite(asyncResultDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_Seek")]
	internal static long /* System.Int64 */ System_IO_Stream_Seek(void* /* System.IO.Stream */ __self, long /* System.Int64 */ offset, System.IO.SeekOrigin /* System.IO.SeekOrigin */ origin, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int64 __returnValue = __selfDotNet.Seek(offset, origin);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_SetLength")]
	internal static void /* System.Void */ System_IO_Stream_SetLength(void* /* System.IO.Stream */ __self, long /* System.Int64 */ value, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.SetLength(value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_ReadByte")]
	internal static int /* System.Int32 */ System_IO_Stream_ReadByte(void* /* System.IO.Stream */ __self, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.ReadByte();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_WriteByte")]
	internal static void /* System.Void */ System_IO_Stream_WriteByte(void* /* System.IO.Stream */ __self, byte /* System.Byte */ value, void** /* System.Exception */ __outException)
	{
		System.IO.Stream __selfDotNet = InteropUtils.GetInstance<System.IO.Stream>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.WriteByte(value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_IO_Stream_Synchronized")]
	internal static void* /* System.IO.Stream */ System_IO_Stream_Synchronized(void* /* System.IO.Stream */ stream, void** /* System.Exception */ __outException)
	{
		System.IO.Stream streamDotNet = InteropUtils.GetInstance<System.IO.Stream>(stream);
	
	    try {
			System.IO.Stream __returnValue = System.IO.Stream.Synchronized(streamDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): CanRead
	

	// TODO (Property): CanWrite
	

	// TODO (Property): CanSeek
	

	// TODO (Property): CanTimeout
	

	// TODO (Property): Length
	

	// TODO (Property): Position
	

	// TODO (Property): ReadTimeout
	

	// TODO (Property): WriteTimeout
	

	[UnmanagedCallersOnly(EntryPoint="System_IO_Stream_Destroy")]
	internal static void /* System.Void */ System_IO_Stream_Destroy(void* /* System.IO.Stream */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_MarshalByRefObject
{
	[UnmanagedCallersOnly(EntryPoint = "System_MarshalByRefObject_GetLifetimeService")]
	internal static void* /* System.Object */ System_MarshalByRefObject_GetLifetimeService(void* /* System.MarshalByRefObject */ __self, void** /* System.Exception */ __outException)
	{
		System.MarshalByRefObject __selfDotNet = InteropUtils.GetInstance<System.MarshalByRefObject>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetLifetimeService();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_MarshalByRefObject_InitializeLifetimeService")]
	internal static void* /* System.Object */ System_MarshalByRefObject_InitializeLifetimeService(void* /* System.MarshalByRefObject */ __self, void** /* System.Exception */ __outException)
	{
		System.MarshalByRefObject __selfDotNet = InteropUtils.GetInstance<System.MarshalByRefObject>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.InitializeLifetimeService();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_MarshalByRefObject_Destroy")]
	internal static void /* System.Void */ System_MarshalByRefObject_Destroy(void* /* System.MarshalByRefObject */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Threading_Tasks_Task
{
	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_Task_Start")]
	internal static void /* System.Void */ System_Threading_Tasks_Task_Start(void* /* System.Threading.Tasks.Task */ __self, void** /* System.Exception */ __outException)
	{
		System.Threading.Tasks.Task __selfDotNet = InteropUtils.GetInstance<System.Threading.Tasks.Task>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Start();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_Task_Start1")]
	internal static void /* System.Void */ System_Threading_Tasks_Task_Start1(void* /* System.Threading.Tasks.Task */ __self, void* /* System.Threading.Tasks.TaskScheduler */ scheduler, void** /* System.Exception */ __outException)
	{
		System.Threading.Tasks.Task __selfDotNet = InteropUtils.GetInstance<System.Threading.Tasks.Task>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Threading.Tasks.TaskScheduler schedulerDotNet = InteropUtils.GetInstance<System.Threading.Tasks.TaskScheduler>(scheduler);
	
	    try {
			__selfDotNet.Start(schedulerDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_Task_RunSynchronously")]
	internal static void /* System.Void */ System_Threading_Tasks_Task_RunSynchronously(void* /* System.Threading.Tasks.Task */ __self, void** /* System.Exception */ __outException)
	{
		System.Threading.Tasks.Task __selfDotNet = InteropUtils.GetInstance<System.Threading.Tasks.Task>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.RunSynchronously();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_Task_RunSynchronously1")]
	internal static void /* System.Void */ System_Threading_Tasks_Task_RunSynchronously1(void* /* System.Threading.Tasks.Task */ __self, void* /* System.Threading.Tasks.TaskScheduler */ scheduler, void** /* System.Exception */ __outException)
	{
		System.Threading.Tasks.Task __selfDotNet = InteropUtils.GetInstance<System.Threading.Tasks.Task>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Threading.Tasks.TaskScheduler schedulerDotNet = InteropUtils.GetInstance<System.Threading.Tasks.TaskScheduler>(scheduler);
	
	    try {
			__selfDotNet.RunSynchronously(schedulerDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_Task_Dispose")]
	internal static void /* System.Void */ System_Threading_Tasks_Task_Dispose(void* /* System.Threading.Tasks.Task */ __self, void** /* System.Exception */ __outException)
	{
		System.Threading.Tasks.Task __selfDotNet = InteropUtils.GetInstance<System.Threading.Tasks.Task>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Dispose();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_Task_Wait")]
	internal static void /* System.Void */ System_Threading_Tasks_Task_Wait(void* /* System.Threading.Tasks.Task */ __self, void** /* System.Exception */ __outException)
	{
		System.Threading.Tasks.Task __selfDotNet = InteropUtils.GetInstance<System.Threading.Tasks.Task>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Wait();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_Task_Wait1")]
	internal static CBool /* System.Boolean */ System_Threading_Tasks_Task_Wait1(void* /* System.Threading.Tasks.Task */ __self, int /* System.Int32 */ millisecondsTimeout, void** /* System.Exception */ __outException)
	{
		System.Threading.Tasks.Task __selfDotNet = InteropUtils.GetInstance<System.Threading.Tasks.Task>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Wait(millisecondsTimeout);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_Task_FromException")]
	internal static void* /* System.Threading.Tasks.Task */ System_Threading_Tasks_Task_FromException(void* /* System.Exception */ exception, void** /* System.Exception */ __outException)
	{
		System.Exception exceptionDotNet = InteropUtils.GetInstance<System.Exception>(exception);
	
	    try {
			System.Threading.Tasks.Task __returnValue = System.Threading.Tasks.Task.FromException(exceptionDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_Task_Delay")]
	internal static void* /* System.Threading.Tasks.Task */ System_Threading_Tasks_Task_Delay(int /* System.Int32 */ millisecondsDelay, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Threading.Tasks.Task __returnValue = System.Threading.Tasks.Task.Delay(millisecondsDelay);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Id
	

	// TODO (Property): Exception
	

	// TODO (Property): Status
	

	// TODO (Property): IsCanceled
	

	// TODO (Property): IsCompleted
	

	// TODO (Property): IsCompletedSuccessfully
	

	// TODO (Property): CreationOptions
	

	// TODO (Property): AsyncState
	

	// TODO (Property): Factory
	

	// TODO (Property): CompletedTask
	

	// TODO (Property): IsFaulted
	

	[UnmanagedCallersOnly(EntryPoint="System_Threading_Tasks_Task_Destroy")]
	internal static void /* System.Void */ System_Threading_Tasks_Task_Destroy(void* /* System.Threading.Tasks.Task */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Threading_Tasks_TaskScheduler
{
	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_TaskScheduler_FromCurrentSynchronizationContext")]
	internal static void* /* System.Threading.Tasks.TaskScheduler */ System_Threading_Tasks_TaskScheduler_FromCurrentSynchronizationContext(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Threading.Tasks.TaskScheduler __returnValue = System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): MaximumConcurrencyLevel
	

	// TODO (Property): Default
	

	// TODO (Property): Current
	

	// TODO (Property): Id
	

	[UnmanagedCallersOnly(EntryPoint="System_Threading_Tasks_TaskScheduler_Destroy")]
	internal static void /* System.Void */ System_Threading_Tasks_TaskScheduler_Destroy(void* /* System.Threading.Tasks.TaskScheduler */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_AggregateException
{
	[UnmanagedCallersOnly(EntryPoint = "System_AggregateException_GetBaseException")]
	internal static void* /* System.Exception */ System_AggregateException_GetBaseException(void* /* System.AggregateException */ __self, void** /* System.Exception */ __outException)
	{
		System.AggregateException __selfDotNet = InteropUtils.GetInstance<System.AggregateException>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Exception __returnValue = __selfDotNet.GetBaseException();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_AggregateException_Flatten")]
	internal static void* /* System.AggregateException */ System_AggregateException_Flatten(void* /* System.AggregateException */ __self, void** /* System.Exception */ __outException)
	{
		System.AggregateException __selfDotNet = InteropUtils.GetInstance<System.AggregateException>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.AggregateException __returnValue = __selfDotNet.Flatten();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_AggregateException_ToString")]
	internal static byte* /* System.String */ System_AggregateException_ToString(void* /* System.AggregateException */ __self, void** /* System.Exception */ __outException)
	{
		System.AggregateException __selfDotNet = InteropUtils.GetInstance<System.AggregateException>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_AggregateException_Create")]
	internal static void* /* System.AggregateException */ System_AggregateException_Create(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.AggregateException __returnValue = new System.AggregateException();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_AggregateException_Create1")]
	internal static void* /* System.AggregateException */ System_AggregateException_Create1(byte* /* System.String */ message, void** /* System.Exception */ __outException)
	{
		System.String messageDotNet = InteropUtils.ToDotNetString(message);
	
	    try {
			System.AggregateException __returnValue = new System.AggregateException(messageDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_AggregateException_Create2")]
	internal static void* /* System.AggregateException */ System_AggregateException_Create2(byte* /* System.String */ message, void* /* System.Exception */ innerException, void** /* System.Exception */ __outException)
	{
		System.String messageDotNet = InteropUtils.ToDotNetString(message);
		System.Exception innerExceptionDotNet = InteropUtils.GetInstance<System.Exception>(innerException);
	
	    try {
			System.AggregateException __returnValue = new System.AggregateException(messageDotNet, innerExceptionDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Message
	

	[UnmanagedCallersOnly(EntryPoint="System_AggregateException_Destroy")]
	internal static void /* System.Void */ System_AggregateException_Destroy(void* /* System.AggregateException */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Exception
{
	[UnmanagedCallersOnly(EntryPoint = "System_Exception_GetBaseException")]
	internal static void* /* System.Exception */ System_Exception_GetBaseException(void* /* System.Exception */ __self, void** /* System.Exception */ __outException)
	{
		System.Exception __selfDotNet = InteropUtils.GetInstance<System.Exception>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Exception __returnValue = __selfDotNet.GetBaseException();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Exception_ToString")]
	internal static byte* /* System.String */ System_Exception_ToString(void* /* System.Exception */ __self, void** /* System.Exception */ __outException)
	{
		System.Exception __selfDotNet = InteropUtils.GetInstance<System.Exception>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Exception_GetType")]
	internal static void* /* System.Type */ System_Exception_GetType(void* /* System.Exception */ __self, void** /* System.Exception */ __outException)
	{
		System.Exception __selfDotNet = InteropUtils.GetInstance<System.Exception>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.GetType();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Exception_Create")]
	internal static void* /* System.Exception */ System_Exception_Create(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Exception __returnValue = new System.Exception();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Exception_Create1")]
	internal static void* /* System.Exception */ System_Exception_Create1(byte* /* System.String */ message, void** /* System.Exception */ __outException)
	{
		System.String messageDotNet = InteropUtils.ToDotNetString(message);
	
	    try {
			System.Exception __returnValue = new System.Exception(messageDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Exception_Create2")]
	internal static void* /* System.Exception */ System_Exception_Create2(byte* /* System.String */ message, void* /* System.Exception */ innerException, void** /* System.Exception */ __outException)
	{
		System.String messageDotNet = InteropUtils.ToDotNetString(message);
		System.Exception innerExceptionDotNet = InteropUtils.GetInstance<System.Exception>(innerException);
	
	    try {
			System.Exception __returnValue = new System.Exception(messageDotNet, innerExceptionDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): TargetSite
	

	// TODO (Property): Message
	

	// TODO (Property): Data
	

	// TODO (Property): InnerException
	

	// TODO (Property): HelpLink
	

	// TODO (Property): Source
	

	// TODO (Property): HResult
	

	// TODO (Property): StackTrace
	

	[UnmanagedCallersOnly(EntryPoint="System_Exception_Destroy")]
	internal static void /* System.Void */ System_Exception_Destroy(void* /* System.Exception */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_MethodBase
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodBase_GetCurrentMethod")]
	internal static void* /* System.Reflection.MethodBase */ System_Reflection_MethodBase_GetCurrentMethod(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Reflection.MethodBase __returnValue = System.Reflection.MethodBase.GetCurrentMethod();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodBase_GetMethodImplementationFlags")]
	internal static System.Reflection.MethodImplAttributes /* System.Reflection.MethodImplAttributes */ System_Reflection_MethodBase_GetMethodImplementationFlags(void* /* System.Reflection.MethodBase */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodBase __selfDotNet = InteropUtils.GetInstance<System.Reflection.MethodBase>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodImplAttributes __returnValue = __selfDotNet.GetMethodImplementationFlags();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.Reflection.MethodImplAttributes);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodBase_GetMethodBody")]
	internal static void* /* System.Reflection.MethodBody */ System_Reflection_MethodBase_GetMethodBody(void* /* System.Reflection.MethodBase */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodBase __selfDotNet = InteropUtils.GetInstance<System.Reflection.MethodBase>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodBody __returnValue = __selfDotNet.GetMethodBody();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodBase_Equals")]
	internal static CBool /* System.Boolean */ System_Reflection_MethodBase_Equals(void* /* System.Reflection.MethodBase */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodBase __selfDotNet = InteropUtils.GetInstance<System.Reflection.MethodBase>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodBase_GetHashCode")]
	internal static int /* System.Int32 */ System_Reflection_MethodBase_GetHashCode(void* /* System.Reflection.MethodBase */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodBase __selfDotNet = InteropUtils.GetInstance<System.Reflection.MethodBase>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodBase_op_Equality")]
	internal static CBool /* System.Boolean */ System_Reflection_MethodBase_op_Equality(void* /* System.Reflection.MethodBase */ left, void* /* System.Reflection.MethodBase */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodBase leftDotNet = InteropUtils.GetInstance<System.Reflection.MethodBase>(left);
		System.Reflection.MethodBase rightDotNet = InteropUtils.GetInstance<System.Reflection.MethodBase>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.MethodBase.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodBase_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Reflection_MethodBase_op_Inequality(void* /* System.Reflection.MethodBase */ left, void* /* System.Reflection.MethodBase */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodBase leftDotNet = InteropUtils.GetInstance<System.Reflection.MethodBase>(left);
		System.Reflection.MethodBase rightDotNet = InteropUtils.GetInstance<System.Reflection.MethodBase>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.MethodBase.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): Attributes
	

	// TODO (Property): MethodImplementationFlags
	

	// TODO (Property): CallingConvention
	

	// TODO (Property): IsAbstract
	

	// TODO (Property): IsConstructor
	

	// TODO (Property): IsFinal
	

	// TODO (Property): IsHideBySig
	

	// TODO (Property): IsSpecialName
	

	// TODO (Property): IsStatic
	

	// TODO (Property): IsVirtual
	

	// TODO (Property): IsAssembly
	

	// TODO (Property): IsFamily
	

	// TODO (Property): IsFamilyAndAssembly
	

	// TODO (Property): IsFamilyOrAssembly
	

	// TODO (Property): IsPrivate
	

	// TODO (Property): IsPublic
	

	// TODO (Property): IsConstructedGenericMethod
	

	// TODO (Property): IsGenericMethod
	

	// TODO (Property): IsGenericMethodDefinition
	

	// TODO (Property): ContainsGenericParameters
	

	// TODO (Property): IsSecurityCritical
	

	// TODO (Property): IsSecuritySafeCritical
	

	// TODO (Property): IsSecurityTransparent
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_MethodBase_Destroy")]
	internal static void /* System.Void */ System_Reflection_MethodBase_Destroy(void* /* System.Reflection.MethodBase */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}




internal static unsafe class System_Reflection_MethodBody
{
	// TODO (Property): LocalSignatureMetadataToken
	

	// TODO (Property): MaxStackSize
	

	// TODO (Property): InitLocals
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_MethodBody_Destroy")]
	internal static void /* System.Void */ System_Reflection_MethodBody_Destroy(void* /* System.Reflection.MethodBody */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}




internal static unsafe class System_Reflection_Binder
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Binder_ChangeType")]
	internal static void* /* System.Object */ System_Reflection_Binder_ChangeType(void* /* System.Reflection.Binder */ __self, void* /* System.Object */ value, void* /* System.Type */ type, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.Reflection.Binder __selfDotNet = InteropUtils.GetInstance<System.Reflection.Binder>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			System.Object __returnValue = __selfDotNet.ChangeType(valueDotNet, typeDotNet, cultureDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_Binder_Destroy")]
	internal static void /* System.Void */ System_Reflection_Binder_Destroy(void* /* System.Reflection.Binder */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_FieldInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_FieldInfo_Equals")]
	internal static CBool /* System.Boolean */ System_Reflection_FieldInfo_Equals(void* /* System.Reflection.FieldInfo */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Reflection.FieldInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_FieldInfo_GetHashCode")]
	internal static int /* System.Int32 */ System_Reflection_FieldInfo_GetHashCode(void* /* System.Reflection.FieldInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.FieldInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_FieldInfo_op_Equality")]
	internal static CBool /* System.Boolean */ System_Reflection_FieldInfo_op_Equality(void* /* System.Reflection.FieldInfo */ left, void* /* System.Reflection.FieldInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.FieldInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(left);
		System.Reflection.FieldInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.FieldInfo.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_FieldInfo_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Reflection_FieldInfo_op_Inequality(void* /* System.Reflection.FieldInfo */ left, void* /* System.Reflection.FieldInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.FieldInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(left);
		System.Reflection.FieldInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.FieldInfo.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_FieldInfo_GetValue")]
	internal static void* /* System.Object */ System_Reflection_FieldInfo_GetValue(void* /* System.Reflection.FieldInfo */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Reflection.FieldInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Object __returnValue = __selfDotNet.GetValue(objDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_FieldInfo_SetValue")]
	internal static void /* System.Void */ System_Reflection_FieldInfo_SetValue(void* /* System.Reflection.FieldInfo */ __self, void* /* System.Object */ obj, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Reflection.FieldInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.SetValue(objDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_FieldInfo_SetValue1")]
	internal static void /* System.Void */ System_Reflection_FieldInfo_SetValue1(void* /* System.Reflection.FieldInfo */ __self, void* /* System.Object */ obj, void* /* System.Object */ value, System.Reflection.BindingFlags /* System.Reflection.BindingFlags */ invokeAttr, void* /* System.Reflection.Binder */ binder, void* /* System.Globalization.CultureInfo */ culture, void** /* System.Exception */ __outException)
	{
		System.Reflection.FieldInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
		System.Reflection.Binder binderDotNet = InteropUtils.GetInstance<System.Reflection.Binder>(binder);
		System.Globalization.CultureInfo cultureDotNet = InteropUtils.GetInstance<System.Globalization.CultureInfo>(culture);
	
	    try {
			__selfDotNet.SetValue(objDotNet, valueDotNet, invokeAttr, binderDotNet, cultureDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_FieldInfo_GetRawConstantValue")]
	internal static void* /* System.Object */ System_Reflection_FieldInfo_GetRawConstantValue(void* /* System.Reflection.FieldInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.FieldInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.FieldInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetRawConstantValue();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): MemberType
	

	// TODO (Property): Attributes
	

	// TODO (Property): FieldType
	

	// TODO (Property): IsInitOnly
	

	// TODO (Property): IsLiteral
	

	// TODO (Property): IsNotSerialized
	

	// TODO (Property): IsPinvokeImpl
	

	// TODO (Property): IsSpecialName
	

	// TODO (Property): IsStatic
	

	// TODO (Property): IsAssembly
	

	// TODO (Property): IsFamily
	

	// TODO (Property): IsFamilyAndAssembly
	

	// TODO (Property): IsFamilyOrAssembly
	

	// TODO (Property): IsPrivate
	

	// TODO (Property): IsPublic
	

	// TODO (Property): IsSecurityCritical
	

	// TODO (Property): IsSecuritySafeCritical
	

	// TODO (Property): IsSecurityTransparent
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_FieldInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_FieldInfo_Destroy(void* /* System.Reflection.FieldInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Reflection_PropertyInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_GetGetMethod")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_PropertyInfo_GetGetMethod(void* /* System.Reflection.PropertyInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetGetMethod();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_GetGetMethod1")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_PropertyInfo_GetGetMethod1(void* /* System.Reflection.PropertyInfo */ __self, CBool /* System.Boolean */ nonPublic, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Boolean nonPublicDotNet = nonPublic.ToBool();
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetGetMethod(nonPublicDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_GetSetMethod")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_PropertyInfo_GetSetMethod(void* /* System.Reflection.PropertyInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetSetMethod();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_GetSetMethod1")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_PropertyInfo_GetSetMethod1(void* /* System.Reflection.PropertyInfo */ __self, CBool /* System.Boolean */ nonPublic, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Boolean nonPublicDotNet = nonPublic.ToBool();
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetSetMethod(nonPublicDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_GetValue")]
	internal static void* /* System.Object */ System_Reflection_PropertyInfo_GetValue(void* /* System.Reflection.PropertyInfo */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Object __returnValue = __selfDotNet.GetValue(objDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_GetConstantValue")]
	internal static void* /* System.Object */ System_Reflection_PropertyInfo_GetConstantValue(void* /* System.Reflection.PropertyInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetConstantValue();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_GetRawConstantValue")]
	internal static void* /* System.Object */ System_Reflection_PropertyInfo_GetRawConstantValue(void* /* System.Reflection.PropertyInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetRawConstantValue();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_SetValue")]
	internal static void /* System.Void */ System_Reflection_PropertyInfo_SetValue(void* /* System.Reflection.PropertyInfo */ __self, void* /* System.Object */ obj, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.SetValue(objDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_Equals")]
	internal static CBool /* System.Boolean */ System_Reflection_PropertyInfo_Equals(void* /* System.Reflection.PropertyInfo */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_GetHashCode")]
	internal static int /* System.Int32 */ System_Reflection_PropertyInfo_GetHashCode(void* /* System.Reflection.PropertyInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_op_Equality")]
	internal static CBool /* System.Boolean */ System_Reflection_PropertyInfo_op_Equality(void* /* System.Reflection.PropertyInfo */ left, void* /* System.Reflection.PropertyInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(left);
		System.Reflection.PropertyInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.PropertyInfo.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_PropertyInfo_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Reflection_PropertyInfo_op_Inequality(void* /* System.Reflection.PropertyInfo */ left, void* /* System.Reflection.PropertyInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.PropertyInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(left);
		System.Reflection.PropertyInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.PropertyInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.PropertyInfo.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): MemberType
	

	// TODO (Property): PropertyType
	

	// TODO (Property): Attributes
	

	// TODO (Property): IsSpecialName
	

	// TODO (Property): CanRead
	

	// TODO (Property): CanWrite
	

	// TODO (Property): GetMethod
	

	// TODO (Property): SetMethod
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_PropertyInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_PropertyInfo_Destroy(void* /* System.Reflection.PropertyInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Reflection_MethodInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodInfo_GetGenericMethodDefinition")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_MethodInfo_GetGenericMethodDefinition(void* /* System.Reflection.MethodInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.MethodInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetGenericMethodDefinition();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodInfo_GetBaseDefinition")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_MethodInfo_GetBaseDefinition(void* /* System.Reflection.MethodInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.MethodInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetBaseDefinition();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodInfo_Equals")]
	internal static CBool /* System.Boolean */ System_Reflection_MethodInfo_Equals(void* /* System.Reflection.MethodInfo */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.MethodInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodInfo_GetHashCode")]
	internal static int /* System.Int32 */ System_Reflection_MethodInfo_GetHashCode(void* /* System.Reflection.MethodInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.MethodInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodInfo_op_Equality")]
	internal static CBool /* System.Boolean */ System_Reflection_MethodInfo_op_Equality(void* /* System.Reflection.MethodInfo */ left, void* /* System.Reflection.MethodInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.MethodInfo>(left);
		System.Reflection.MethodInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.MethodInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.MethodInfo.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_MethodInfo_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Reflection_MethodInfo_op_Inequality(void* /* System.Reflection.MethodInfo */ left, void* /* System.Reflection.MethodInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.MethodInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.MethodInfo>(left);
		System.Reflection.MethodInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.MethodInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.MethodInfo.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): MemberType
	

	// TODO (Property): ReturnParameter
	

	// TODO (Property): ReturnType
	

	// TODO (Property): ReturnTypeCustomAttributes
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_MethodInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_MethodInfo_Destroy(void* /* System.Reflection.MethodInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_ParameterInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_ParameterInfo_IsDefined")]
	internal static CBool /* System.Boolean */ System_Reflection_ParameterInfo_IsDefined(void* /* System.Reflection.ParameterInfo */ __self, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.ParameterInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.ParameterInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsDefined(attributeTypeDotNet, inheritDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_ParameterInfo_ToString")]
	internal static byte* /* System.String */ System_Reflection_ParameterInfo_ToString(void* /* System.Reflection.ParameterInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.ParameterInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.ParameterInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Attributes
	

	// TODO (Property): Member
	

	// TODO (Property): Name
	

	// TODO (Property): ParameterType
	

	// TODO (Property): Position
	

	// TODO (Property): IsIn
	

	// TODO (Property): IsLcid
	

	// TODO (Property): IsOptional
	

	// TODO (Property): IsOut
	

	// TODO (Property): IsRetval
	

	// TODO (Property): DefaultValue
	

	// TODO (Property): RawDefaultValue
	

	// TODO (Property): HasDefaultValue
	

	// TODO (Property): MetadataToken
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_ParameterInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_ParameterInfo_Destroy(void* /* System.Reflection.ParameterInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Reflection_ICustomAttributeProvider
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_ICustomAttributeProvider_IsDefined")]
	internal static CBool /* System.Boolean */ System_Reflection_ICustomAttributeProvider_IsDefined(void* /* System.Reflection.ICustomAttributeProvider */ __self, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.ICustomAttributeProvider __selfDotNet = InteropUtils.GetInstance<System.Reflection.ICustomAttributeProvider>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsDefined(attributeTypeDotNet, inheritDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_ICustomAttributeProvider_Destroy")]
	internal static void /* System.Void */ System_Reflection_ICustomAttributeProvider_Destroy(void* /* System.Reflection.ICustomAttributeProvider */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Collections_IDictionary
{
	[UnmanagedCallersOnly(EntryPoint = "System_Collections_IDictionary_Contains")]
	internal static CBool /* System.Boolean */ System_Collections_IDictionary_Contains(void* /* System.Collections.IDictionary */ __self, void* /* System.Object */ key, void** /* System.Exception */ __outException)
	{
		System.Collections.IDictionary __selfDotNet = InteropUtils.GetInstance<System.Collections.IDictionary>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object keyDotNet = InteropUtils.GetInstance<System.Object>(key);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Contains(keyDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Collections_IDictionary_Add")]
	internal static void /* System.Void */ System_Collections_IDictionary_Add(void* /* System.Collections.IDictionary */ __self, void* /* System.Object */ key, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Collections.IDictionary __selfDotNet = InteropUtils.GetInstance<System.Collections.IDictionary>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object keyDotNet = InteropUtils.GetInstance<System.Object>(key);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.Add(keyDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Collections_IDictionary_Clear")]
	internal static void /* System.Void */ System_Collections_IDictionary_Clear(void* /* System.Collections.IDictionary */ __self, void** /* System.Exception */ __outException)
	{
		System.Collections.IDictionary __selfDotNet = InteropUtils.GetInstance<System.Collections.IDictionary>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Clear();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Collections_IDictionary_GetEnumerator")]
	internal static void* /* System.Collections.IDictionaryEnumerator */ System_Collections_IDictionary_GetEnumerator(void* /* System.Collections.IDictionary */ __self, void** /* System.Exception */ __outException)
	{
		System.Collections.IDictionary __selfDotNet = InteropUtils.GetInstance<System.Collections.IDictionary>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Collections.IDictionaryEnumerator __returnValue = __selfDotNet.GetEnumerator();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Collections_IDictionary_Remove")]
	internal static void /* System.Void */ System_Collections_IDictionary_Remove(void* /* System.Collections.IDictionary */ __self, void* /* System.Object */ key, void** /* System.Exception */ __outException)
	{
		System.Collections.IDictionary __selfDotNet = InteropUtils.GetInstance<System.Collections.IDictionary>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object keyDotNet = InteropUtils.GetInstance<System.Object>(key);
	
	    try {
			__selfDotNet.Remove(keyDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): Item
	

	// TODO (Property): Keys
	

	// TODO (Property): Values
	

	// TODO (Property): IsReadOnly
	

	// TODO (Property): IsFixedSize
	

	[UnmanagedCallersOnly(EntryPoint="System_Collections_IDictionary_Destroy")]
	internal static void /* System.Void */ System_Collections_IDictionary_Destroy(void* /* System.Collections.IDictionary */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Collections_ICollection
{
	[UnmanagedCallersOnly(EntryPoint = "System_Collections_ICollection_CopyTo")]
	internal static void /* System.Void */ System_Collections_ICollection_CopyTo(void* /* System.Collections.ICollection */ __self, void* /* System.Array */ array, int /* System.Int32 */ index, void** /* System.Exception */ __outException)
	{
		System.Collections.ICollection __selfDotNet = InteropUtils.GetInstance<System.Collections.ICollection>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
	
	    try {
			__selfDotNet.CopyTo(arrayDotNet, index);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): Count
	

	// TODO (Property): SyncRoot
	

	// TODO (Property): IsSynchronized
	

	[UnmanagedCallersOnly(EntryPoint="System_Collections_ICollection_Destroy")]
	internal static void /* System.Void */ System_Collections_ICollection_Destroy(void* /* System.Collections.ICollection */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Array
{
	[UnmanagedCallersOnly(EntryPoint = "System_Array_Copy")]
	internal static void /* System.Void */ System_Array_Copy(void* /* System.Array */ sourceArray, void* /* System.Array */ destinationArray, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.Array sourceArrayDotNet = InteropUtils.GetInstance<System.Array>(sourceArray);
		System.Array destinationArrayDotNet = InteropUtils.GetInstance<System.Array>(destinationArray);
	
	    try {
			System.Array.Copy(sourceArrayDotNet, destinationArrayDotNet, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Copy1")]
	internal static void /* System.Void */ System_Array_Copy1(void* /* System.Array */ sourceArray, int /* System.Int32 */ sourceIndex, void* /* System.Array */ destinationArray, int /* System.Int32 */ destinationIndex, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.Array sourceArrayDotNet = InteropUtils.GetInstance<System.Array>(sourceArray);
		System.Array destinationArrayDotNet = InteropUtils.GetInstance<System.Array>(destinationArray);
	
	    try {
			System.Array.Copy(sourceArrayDotNet, sourceIndex, destinationArrayDotNet, destinationIndex, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_ConstrainedCopy")]
	internal static void /* System.Void */ System_Array_ConstrainedCopy(void* /* System.Array */ sourceArray, int /* System.Int32 */ sourceIndex, void* /* System.Array */ destinationArray, int /* System.Int32 */ destinationIndex, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.Array sourceArrayDotNet = InteropUtils.GetInstance<System.Array>(sourceArray);
		System.Array destinationArrayDotNet = InteropUtils.GetInstance<System.Array>(destinationArray);
	
	    try {
			System.Array.ConstrainedCopy(sourceArrayDotNet, sourceIndex, destinationArrayDotNet, destinationIndex, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Clear")]
	internal static void /* System.Void */ System_Array_Clear(void* /* System.Array */ array, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
	
	    try {
			System.Array.Clear(arrayDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Clear1")]
	internal static void /* System.Void */ System_Array_Clear1(void* /* System.Array */ array, int /* System.Int32 */ index, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
	
	    try {
			System.Array.Clear(arrayDotNet, index, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetLength")]
	internal static int /* System.Int32 */ System_Array_GetLength(void* /* System.Array */ __self, int /* System.Int32 */ dimension, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetLength(dimension);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetUpperBound")]
	internal static int /* System.Int32 */ System_Array_GetUpperBound(void* /* System.Array */ __self, int /* System.Int32 */ dimension, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetUpperBound(dimension);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetLowerBound")]
	internal static int /* System.Int32 */ System_Array_GetLowerBound(void* /* System.Array */ __self, int /* System.Int32 */ dimension, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetLowerBound(dimension);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Initialize")]
	internal static void /* System.Void */ System_Array_Initialize(void* /* System.Array */ __self, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Initialize();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_CreateInstance")]
	internal static void* /* System.Array */ System_Array_CreateInstance(void* /* System.Type */ elementType, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.Type elementTypeDotNet = InteropUtils.GetInstance<System.Type>(elementType);
	
	    try {
			System.Array __returnValue = System.Array.CreateInstance(elementTypeDotNet, length);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_CreateInstance1")]
	internal static void* /* System.Array */ System_Array_CreateInstance1(void* /* System.Type */ elementType, int /* System.Int32 */ length1, int /* System.Int32 */ length2, void** /* System.Exception */ __outException)
	{
		System.Type elementTypeDotNet = InteropUtils.GetInstance<System.Type>(elementType);
	
	    try {
			System.Array __returnValue = System.Array.CreateInstance(elementTypeDotNet, length1, length2);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_CreateInstance2")]
	internal static void* /* System.Array */ System_Array_CreateInstance2(void* /* System.Type */ elementType, int /* System.Int32 */ length1, int /* System.Int32 */ length2, int /* System.Int32 */ length3, void** /* System.Exception */ __outException)
	{
		System.Type elementTypeDotNet = InteropUtils.GetInstance<System.Type>(elementType);
	
	    try {
			System.Array __returnValue = System.Array.CreateInstance(elementTypeDotNet, length1, length2, length3);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Copy2")]
	internal static void /* System.Void */ System_Array_Copy2(void* /* System.Array */ sourceArray, void* /* System.Array */ destinationArray, long /* System.Int64 */ length, void** /* System.Exception */ __outException)
	{
		System.Array sourceArrayDotNet = InteropUtils.GetInstance<System.Array>(sourceArray);
		System.Array destinationArrayDotNet = InteropUtils.GetInstance<System.Array>(destinationArray);
	
	    try {
			System.Array.Copy(sourceArrayDotNet, destinationArrayDotNet, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Copy3")]
	internal static void /* System.Void */ System_Array_Copy3(void* /* System.Array */ sourceArray, long /* System.Int64 */ sourceIndex, void* /* System.Array */ destinationArray, long /* System.Int64 */ destinationIndex, long /* System.Int64 */ length, void** /* System.Exception */ __outException)
	{
		System.Array sourceArrayDotNet = InteropUtils.GetInstance<System.Array>(sourceArray);
		System.Array destinationArrayDotNet = InteropUtils.GetInstance<System.Array>(destinationArray);
	
	    try {
			System.Array.Copy(sourceArrayDotNet, sourceIndex, destinationArrayDotNet, destinationIndex, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetValue")]
	internal static void* /* System.Object */ System_Array_GetValue(void* /* System.Array */ __self, int /* System.Int32 */ index, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetValue(index);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetValue1")]
	internal static void* /* System.Object */ System_Array_GetValue1(void* /* System.Array */ __self, int /* System.Int32 */ index1, int /* System.Int32 */ index2, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetValue(index1, index2);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetValue2")]
	internal static void* /* System.Object */ System_Array_GetValue2(void* /* System.Array */ __self, int /* System.Int32 */ index1, int /* System.Int32 */ index2, int /* System.Int32 */ index3, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetValue(index1, index2, index3);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_SetValue")]
	internal static void /* System.Void */ System_Array_SetValue(void* /* System.Array */ __self, void* /* System.Object */ value, int /* System.Int32 */ index, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.SetValue(valueDotNet, index);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_SetValue1")]
	internal static void /* System.Void */ System_Array_SetValue1(void* /* System.Array */ __self, void* /* System.Object */ value, int /* System.Int32 */ index1, int /* System.Int32 */ index2, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.SetValue(valueDotNet, index1, index2);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_SetValue2")]
	internal static void /* System.Void */ System_Array_SetValue2(void* /* System.Array */ __self, void* /* System.Object */ value, int /* System.Int32 */ index1, int /* System.Int32 */ index2, int /* System.Int32 */ index3, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.SetValue(valueDotNet, index1, index2, index3);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetValue3")]
	internal static void* /* System.Object */ System_Array_GetValue3(void* /* System.Array */ __self, long /* System.Int64 */ index, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetValue(index);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetValue4")]
	internal static void* /* System.Object */ System_Array_GetValue4(void* /* System.Array */ __self, long /* System.Int64 */ index1, long /* System.Int64 */ index2, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetValue(index1, index2);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetValue5")]
	internal static void* /* System.Object */ System_Array_GetValue5(void* /* System.Array */ __self, long /* System.Int64 */ index1, long /* System.Int64 */ index2, long /* System.Int64 */ index3, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.GetValue(index1, index2, index3);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_SetValue3")]
	internal static void /* System.Void */ System_Array_SetValue3(void* /* System.Array */ __self, void* /* System.Object */ value, long /* System.Int64 */ index, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.SetValue(valueDotNet, index);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_SetValue4")]
	internal static void /* System.Void */ System_Array_SetValue4(void* /* System.Array */ __self, void* /* System.Object */ value, long /* System.Int64 */ index1, long /* System.Int64 */ index2, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.SetValue(valueDotNet, index1, index2);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_SetValue5")]
	internal static void /* System.Void */ System_Array_SetValue5(void* /* System.Array */ __self, void* /* System.Object */ value, long /* System.Int64 */ index1, long /* System.Int64 */ index2, long /* System.Int64 */ index3, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.SetValue(valueDotNet, index1, index2, index3);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetLongLength")]
	internal static long /* System.Int64 */ System_Array_GetLongLength(void* /* System.Array */ __self, int /* System.Int32 */ dimension, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int64 __returnValue = __selfDotNet.GetLongLength(dimension);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Clone")]
	internal static void* /* System.Object */ System_Array_Clone(void* /* System.Array */ __self, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_BinarySearch")]
	internal static int /* System.Int32 */ System_Array_BinarySearch(void* /* System.Array */ array, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = System.Array.BinarySearch(arrayDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_BinarySearch1")]
	internal static int /* System.Int32 */ System_Array_BinarySearch1(void* /* System.Array */ array, int /* System.Int32 */ index, int /* System.Int32 */ length, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = System.Array.BinarySearch(arrayDotNet, index, length, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_BinarySearch2")]
	internal static int /* System.Int32 */ System_Array_BinarySearch2(void* /* System.Array */ array, void* /* System.Object */ value, void* /* System.Collections.IComparer */ comparer, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
		System.Collections.IComparer comparerDotNet = InteropUtils.GetInstance<System.Collections.IComparer>(comparer);
	
	    try {
			System.Int32 __returnValue = System.Array.BinarySearch(arrayDotNet, valueDotNet, comparerDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_BinarySearch3")]
	internal static int /* System.Int32 */ System_Array_BinarySearch3(void* /* System.Array */ array, int /* System.Int32 */ index, int /* System.Int32 */ length, void* /* System.Object */ value, void* /* System.Collections.IComparer */ comparer, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
		System.Collections.IComparer comparerDotNet = InteropUtils.GetInstance<System.Collections.IComparer>(comparer);
	
	    try {
			System.Int32 __returnValue = System.Array.BinarySearch(arrayDotNet, index, length, valueDotNet, comparerDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_CopyTo")]
	internal static void /* System.Void */ System_Array_CopyTo(void* /* System.Array */ __self, void* /* System.Array */ array, int /* System.Int32 */ index, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
	
	    try {
			__selfDotNet.CopyTo(arrayDotNet, index);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_CopyTo1")]
	internal static void /* System.Void */ System_Array_CopyTo1(void* /* System.Array */ __self, void* /* System.Array */ array, long /* System.Int64 */ index, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
	
	    try {
			__selfDotNet.CopyTo(arrayDotNet, index);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_IndexOf")]
	internal static int /* System.Int32 */ System_Array_IndexOf(void* /* System.Array */ array, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = System.Array.IndexOf(arrayDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_IndexOf1")]
	internal static int /* System.Int32 */ System_Array_IndexOf1(void* /* System.Array */ array, void* /* System.Object */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = System.Array.IndexOf(arrayDotNet, valueDotNet, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_IndexOf2")]
	internal static int /* System.Int32 */ System_Array_IndexOf2(void* /* System.Array */ array, void* /* System.Object */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = System.Array.IndexOf(arrayDotNet, valueDotNet, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_LastIndexOf")]
	internal static int /* System.Int32 */ System_Array_LastIndexOf(void* /* System.Array */ array, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = System.Array.LastIndexOf(arrayDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_LastIndexOf1")]
	internal static int /* System.Int32 */ System_Array_LastIndexOf1(void* /* System.Array */ array, void* /* System.Object */ value, int /* System.Int32 */ startIndex, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = System.Array.LastIndexOf(arrayDotNet, valueDotNet, startIndex);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_LastIndexOf2")]
	internal static int /* System.Int32 */ System_Array_LastIndexOf2(void* /* System.Array */ array, void* /* System.Object */ value, int /* System.Int32 */ startIndex, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = System.Array.LastIndexOf(arrayDotNet, valueDotNet, startIndex, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Reverse")]
	internal static void /* System.Void */ System_Array_Reverse(void* /* System.Array */ array, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
	
	    try {
			System.Array.Reverse(arrayDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Reverse1")]
	internal static void /* System.Void */ System_Array_Reverse1(void* /* System.Array */ array, int /* System.Int32 */ index, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
	
	    try {
			System.Array.Reverse(arrayDotNet, index, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Sort")]
	internal static void /* System.Void */ System_Array_Sort(void* /* System.Array */ array, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
	
	    try {
			System.Array.Sort(arrayDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Sort1")]
	internal static void /* System.Void */ System_Array_Sort1(void* /* System.Array */ keys, void* /* System.Array */ items, void** /* System.Exception */ __outException)
	{
		System.Array keysDotNet = InteropUtils.GetInstance<System.Array>(keys);
		System.Array itemsDotNet = InteropUtils.GetInstance<System.Array>(items);
	
	    try {
			System.Array.Sort(keysDotNet, itemsDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Sort2")]
	internal static void /* System.Void */ System_Array_Sort2(void* /* System.Array */ array, int /* System.Int32 */ index, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
	
	    try {
			System.Array.Sort(arrayDotNet, index, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Sort3")]
	internal static void /* System.Void */ System_Array_Sort3(void* /* System.Array */ keys, void* /* System.Array */ items, int /* System.Int32 */ index, int /* System.Int32 */ length, void** /* System.Exception */ __outException)
	{
		System.Array keysDotNet = InteropUtils.GetInstance<System.Array>(keys);
		System.Array itemsDotNet = InteropUtils.GetInstance<System.Array>(items);
	
	    try {
			System.Array.Sort(keysDotNet, itemsDotNet, index, length);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Sort4")]
	internal static void /* System.Void */ System_Array_Sort4(void* /* System.Array */ array, void* /* System.Collections.IComparer */ comparer, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Collections.IComparer comparerDotNet = InteropUtils.GetInstance<System.Collections.IComparer>(comparer);
	
	    try {
			System.Array.Sort(arrayDotNet, comparerDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Sort5")]
	internal static void /* System.Void */ System_Array_Sort5(void* /* System.Array */ keys, void* /* System.Array */ items, void* /* System.Collections.IComparer */ comparer, void** /* System.Exception */ __outException)
	{
		System.Array keysDotNet = InteropUtils.GetInstance<System.Array>(keys);
		System.Array itemsDotNet = InteropUtils.GetInstance<System.Array>(items);
		System.Collections.IComparer comparerDotNet = InteropUtils.GetInstance<System.Collections.IComparer>(comparer);
	
	    try {
			System.Array.Sort(keysDotNet, itemsDotNet, comparerDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Sort6")]
	internal static void /* System.Void */ System_Array_Sort6(void* /* System.Array */ array, int /* System.Int32 */ index, int /* System.Int32 */ length, void* /* System.Collections.IComparer */ comparer, void** /* System.Exception */ __outException)
	{
		System.Array arrayDotNet = InteropUtils.GetInstance<System.Array>(array);
		System.Collections.IComparer comparerDotNet = InteropUtils.GetInstance<System.Collections.IComparer>(comparer);
	
	    try {
			System.Array.Sort(arrayDotNet, index, length, comparerDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_Sort7")]
	internal static void /* System.Void */ System_Array_Sort7(void* /* System.Array */ keys, void* /* System.Array */ items, int /* System.Int32 */ index, int /* System.Int32 */ length, void* /* System.Collections.IComparer */ comparer, void** /* System.Exception */ __outException)
	{
		System.Array keysDotNet = InteropUtils.GetInstance<System.Array>(keys);
		System.Array itemsDotNet = InteropUtils.GetInstance<System.Array>(items);
		System.Collections.IComparer comparerDotNet = InteropUtils.GetInstance<System.Collections.IComparer>(comparer);
	
	    try {
			System.Array.Sort(keysDotNet, itemsDotNet, index, length, comparerDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Array_GetEnumerator")]
	internal static void* /* System.Collections.IEnumerator */ System_Array_GetEnumerator(void* /* System.Array */ __self, void** /* System.Exception */ __outException)
	{
		System.Array __selfDotNet = InteropUtils.GetInstance<System.Array>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Collections.IEnumerator __returnValue = __selfDotNet.GetEnumerator();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Length
	

	// TODO (Property): LongLength
	

	// TODO (Property): Rank
	

	// TODO (Property): SyncRoot
	

	// TODO (Property): IsReadOnly
	

	// TODO (Property): IsFixedSize
	

	// TODO (Property): IsSynchronized
	

	// TODO (Property): MaxLength
	

	[UnmanagedCallersOnly(EntryPoint="System_Array_Destroy")]
	internal static void /* System.Void */ System_Array_Destroy(void* /* System.Array */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Collections_IComparer
{
	[UnmanagedCallersOnly(EntryPoint = "System_Collections_IComparer_Compare")]
	internal static int /* System.Int32 */ System_Collections_IComparer_Compare(void* /* System.Collections.IComparer */ __self, void* /* System.Object */ x, void* /* System.Object */ y, void** /* System.Exception */ __outException)
	{
		System.Collections.IComparer __selfDotNet = InteropUtils.GetInstance<System.Collections.IComparer>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object xDotNet = InteropUtils.GetInstance<System.Object>(x);
		System.Object yDotNet = InteropUtils.GetInstance<System.Object>(y);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.Compare(xDotNet, yDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_Collections_IComparer_Destroy")]
	internal static void /* System.Void */ System_Collections_IComparer_Destroy(void* /* System.Collections.IComparer */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Collections_IEnumerator
{
	[UnmanagedCallersOnly(EntryPoint = "System_Collections_IEnumerator_MoveNext")]
	internal static CBool /* System.Boolean */ System_Collections_IEnumerator_MoveNext(void* /* System.Collections.IEnumerator */ __self, void** /* System.Exception */ __outException)
	{
		System.Collections.IEnumerator __selfDotNet = InteropUtils.GetInstance<System.Collections.IEnumerator>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.MoveNext();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Collections_IEnumerator_Reset")]
	internal static void /* System.Void */ System_Collections_IEnumerator_Reset(void* /* System.Collections.IEnumerator */ __self, void** /* System.Exception */ __outException)
	{
		System.Collections.IEnumerator __selfDotNet = InteropUtils.GetInstance<System.Collections.IEnumerator>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Reset();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): Current
	

	[UnmanagedCallersOnly(EntryPoint="System_Collections_IEnumerator_Destroy")]
	internal static void /* System.Void */ System_Collections_IEnumerator_Destroy(void* /* System.Collections.IEnumerator */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Collections_IDictionaryEnumerator
{
	// TODO (Property): Key
	

	// TODO (Property): Value
	

	[UnmanagedCallersOnly(EntryPoint="System_Collections_IDictionaryEnumerator_Destroy")]
	internal static void /* System.Void */ System_Collections_IDictionaryEnumerator_Destroy(void* /* System.Collections.IDictionaryEnumerator */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Runtime_Serialization_SerializationInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_SetType")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_SetType(void* /* System.Runtime.Serialization.SerializationInfo */ __self, void* /* System.Type */ type, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
	
	    try {
			__selfDotNet.SetType(typeDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetEnumerator")]
	internal static void* /* System.Runtime.Serialization.SerializationInfoEnumerator */ System_Runtime_Serialization_SerializationInfo_GetEnumerator(void* /* System.Runtime.Serialization.SerializationInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Runtime.Serialization.SerializationInfoEnumerator __returnValue = __selfDotNet.GetEnumerator();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void* /* System.Object */ value, void* /* System.Type */ type, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, valueDotNet, typeDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue1")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue1(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue2")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue2(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, CBool /* System.Boolean */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Boolean valueDotNet = value.ToBool();
	
	    try {
			__selfDotNet.AddValue(nameDotNet, valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue3")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue3(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, System.Char /* System.Char */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue4")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue4(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, sbyte /* System.SByte */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue5")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue5(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, byte /* System.Byte */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue6")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue6(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, short /* System.Int16 */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue7")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue7(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, ushort /* System.UInt16 */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue8")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue8(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, int /* System.Int32 */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue9")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue9(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, uint /* System.UInt32 */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue10")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue10(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, long /* System.Int64 */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue11")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue11(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, ulong /* System.UInt64 */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue12")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue12(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, float /* System.Single */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_AddValue13")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_AddValue13(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, double /* System.Double */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			__selfDotNet.AddValue(nameDotNet, value);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetValue")]
	internal static void* /* System.Object */ System_Runtime_Serialization_SerializationInfo_GetValue(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void* /* System.Type */ type, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
	
	    try {
			System.Object __returnValue = __selfDotNet.GetValue(nameDotNet, typeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetBoolean")]
	internal static CBool /* System.Boolean */ System_Runtime_Serialization_SerializationInfo_GetBoolean(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.GetBoolean(nameDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetChar")]
	internal static System.Char /* System.Char */ System_Runtime_Serialization_SerializationInfo_GetChar(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Char __returnValue = __selfDotNet.GetChar(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.Char);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetSByte")]
	internal static sbyte /* System.SByte */ System_Runtime_Serialization_SerializationInfo_GetSByte(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.SByte __returnValue = __selfDotNet.GetSByte(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetByte")]
	internal static byte /* System.Byte */ System_Runtime_Serialization_SerializationInfo_GetByte(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Byte __returnValue = __selfDotNet.GetByte(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return 0;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetInt16")]
	internal static short /* System.Int16 */ System_Runtime_Serialization_SerializationInfo_GetInt16(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Int16 __returnValue = __selfDotNet.GetInt16(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetUInt16")]
	internal static ushort /* System.UInt16 */ System_Runtime_Serialization_SerializationInfo_GetUInt16(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.UInt16 __returnValue = __selfDotNet.GetUInt16(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return 0;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetInt32")]
	internal static int /* System.Int32 */ System_Runtime_Serialization_SerializationInfo_GetInt32(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetInt32(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetUInt32")]
	internal static uint /* System.UInt32 */ System_Runtime_Serialization_SerializationInfo_GetUInt32(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.UInt32 __returnValue = __selfDotNet.GetUInt32(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return 0;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetInt64")]
	internal static long /* System.Int64 */ System_Runtime_Serialization_SerializationInfo_GetInt64(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Int64 __returnValue = __selfDotNet.GetInt64(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetUInt64")]
	internal static ulong /* System.UInt64 */ System_Runtime_Serialization_SerializationInfo_GetUInt64(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.UInt64 __returnValue = __selfDotNet.GetUInt64(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return 0;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetSingle")]
	internal static float /* System.Single */ System_Runtime_Serialization_SerializationInfo_GetSingle(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Single __returnValue = __selfDotNet.GetSingle(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetDouble")]
	internal static double /* System.Double */ System_Runtime_Serialization_SerializationInfo_GetDouble(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Double __returnValue = __selfDotNet.GetDouble(nameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_GetString")]
	internal static byte* /* System.String */ System_Runtime_Serialization_SerializationInfo_GetString(void* /* System.Runtime.Serialization.SerializationInfo */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfo __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.String __returnValue = __selfDotNet.GetString(nameDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_Create")]
	internal static void* /* System.Runtime.Serialization.SerializationInfo */ System_Runtime_Serialization_SerializationInfo_Create(void* /* System.Type */ type, void* /* System.Runtime.Serialization.IFormatterConverter */ converter, void** /* System.Exception */ __outException)
	{
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
		System.Runtime.Serialization.IFormatterConverter converterDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(converter);
	
	    try {
			System.Runtime.Serialization.SerializationInfo __returnValue = new System.Runtime.Serialization.SerializationInfo(typeDotNet, converterDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfo_Create1")]
	internal static void* /* System.Runtime.Serialization.SerializationInfo */ System_Runtime_Serialization_SerializationInfo_Create1(void* /* System.Type */ type, void* /* System.Runtime.Serialization.IFormatterConverter */ converter, CBool /* System.Boolean */ requireSameTokenInPartialTrust, void** /* System.Exception */ __outException)
	{
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
		System.Runtime.Serialization.IFormatterConverter converterDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(converter);
		System.Boolean requireSameTokenInPartialTrustDotNet = requireSameTokenInPartialTrust.ToBool();
	
	    try {
			System.Runtime.Serialization.SerializationInfo __returnValue = new System.Runtime.Serialization.SerializationInfo(typeDotNet, converterDotNet, requireSameTokenInPartialTrustDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): FullTypeName
	

	// TODO (Property): AssemblyName
	

	// TODO (Property): IsFullTypeNameSetExplicit
	

	// TODO (Property): IsAssemblyNameSetExplicit
	

	// TODO (Property): MemberCount
	

	// TODO (Property): ObjectType
	

	[UnmanagedCallersOnly(EntryPoint="System_Runtime_Serialization_SerializationInfo_Destroy")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_Destroy(void* /* System.Runtime.Serialization.SerializationInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Runtime_Serialization_SerializationInfoEnumerator
{
	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfoEnumerator_MoveNext")]
	internal static CBool /* System.Boolean */ System_Runtime_Serialization_SerializationInfoEnumerator_MoveNext(void* /* System.Runtime.Serialization.SerializationInfoEnumerator */ __self, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfoEnumerator __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfoEnumerator>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.MoveNext();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_SerializationInfoEnumerator_Reset")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfoEnumerator_Reset(void* /* System.Runtime.Serialization.SerializationInfoEnumerator */ __self, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.SerializationInfoEnumerator __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.SerializationInfoEnumerator>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Reset();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): Name
	

	// TODO (Property): Value
	

	// TODO (Property): ObjectType
	

	[UnmanagedCallersOnly(EntryPoint="System_Runtime_Serialization_SerializationInfoEnumerator_Destroy")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfoEnumerator_Destroy(void* /* System.Runtime.Serialization.SerializationInfoEnumerator */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}












internal static unsafe class System_Runtime_Serialization_IFormatterConverter
{
	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_Convert")]
	internal static void* /* System.Object */ System_Runtime_Serialization_IFormatterConverter_Convert(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void* /* System.Type */ type, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
		System.Type typeDotNet = InteropUtils.GetInstance<System.Type>(type);
	
	    try {
			System.Object __returnValue = __selfDotNet.Convert(valueDotNet, typeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_Convert1")]
	internal static void* /* System.Object */ System_Runtime_Serialization_IFormatterConverter_Convert1(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, System.TypeCode /* System.TypeCode */ typeCode, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Object __returnValue = __selfDotNet.Convert(valueDotNet, typeCode);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToBoolean")]
	internal static CBool /* System.Boolean */ System_Runtime_Serialization_IFormatterConverter_ToBoolean(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.ToBoolean(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToChar")]
	internal static System.Char /* System.Char */ System_Runtime_Serialization_IFormatterConverter_ToChar(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Char __returnValue = __selfDotNet.ToChar(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.Char);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToSByte")]
	internal static sbyte /* System.SByte */ System_Runtime_Serialization_IFormatterConverter_ToSByte(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.SByte __returnValue = __selfDotNet.ToSByte(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToByte")]
	internal static byte /* System.Byte */ System_Runtime_Serialization_IFormatterConverter_ToByte(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Byte __returnValue = __selfDotNet.ToByte(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return 0;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToInt16")]
	internal static short /* System.Int16 */ System_Runtime_Serialization_IFormatterConverter_ToInt16(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int16 __returnValue = __selfDotNet.ToInt16(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToUInt16")]
	internal static ushort /* System.UInt16 */ System_Runtime_Serialization_IFormatterConverter_ToUInt16(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.UInt16 __returnValue = __selfDotNet.ToUInt16(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return 0;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToInt32")]
	internal static int /* System.Int32 */ System_Runtime_Serialization_IFormatterConverter_ToInt32(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.ToInt32(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToUInt32")]
	internal static uint /* System.UInt32 */ System_Runtime_Serialization_IFormatterConverter_ToUInt32(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.UInt32 __returnValue = __selfDotNet.ToUInt32(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return 0;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToInt64")]
	internal static long /* System.Int64 */ System_Runtime_Serialization_IFormatterConverter_ToInt64(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Int64 __returnValue = __selfDotNet.ToInt64(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToUInt64")]
	internal static ulong /* System.UInt64 */ System_Runtime_Serialization_IFormatterConverter_ToUInt64(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.UInt64 __returnValue = __selfDotNet.ToUInt64(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return 0;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToSingle")]
	internal static float /* System.Single */ System_Runtime_Serialization_IFormatterConverter_ToSingle(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Single __returnValue = __selfDotNet.ToSingle(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToDouble")]
	internal static double /* System.Double */ System_Runtime_Serialization_IFormatterConverter_ToDouble(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Double __returnValue = __selfDotNet.ToDouble(valueDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_Serialization_IFormatterConverter_ToString")]
	internal static byte* /* System.String */ System_Runtime_Serialization_IFormatterConverter_ToString(void* /* System.Runtime.Serialization.IFormatterConverter */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Runtime.Serialization.IFormatterConverter __selfDotNet = InteropUtils.GetInstance<System.Runtime.Serialization.IFormatterConverter>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.String __returnValue = __selfDotNet.ToString(valueDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="System_Runtime_Serialization_IFormatterConverter_Destroy")]
	internal static void /* System.Void */ System_Runtime_Serialization_IFormatterConverter_Destroy(void* /* System.Runtime.Serialization.IFormatterConverter */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}




internal static unsafe class System_Threading_Tasks_TaskFactory
{
	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_TaskFactory_Create")]
	internal static void* /* System.Threading.Tasks.TaskFactory */ System_Threading_Tasks_TaskFactory_Create(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Threading.Tasks.TaskFactory __returnValue = new System.Threading.Tasks.TaskFactory();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_TaskFactory_Create1")]
	internal static void* /* System.Threading.Tasks.TaskFactory */ System_Threading_Tasks_TaskFactory_Create1(void* /* System.Threading.Tasks.TaskScheduler */ scheduler, void** /* System.Exception */ __outException)
	{
		System.Threading.Tasks.TaskScheduler schedulerDotNet = InteropUtils.GetInstance<System.Threading.Tasks.TaskScheduler>(scheduler);
	
	    try {
			System.Threading.Tasks.TaskFactory __returnValue = new System.Threading.Tasks.TaskFactory(schedulerDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_Tasks_TaskFactory_Create2")]
	internal static void* /* System.Threading.Tasks.TaskFactory */ System_Threading_Tasks_TaskFactory_Create2(System.Threading.Tasks.TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ creationOptions, System.Threading.Tasks.TaskContinuationOptions /* System.Threading.Tasks.TaskContinuationOptions */ continuationOptions, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Threading.Tasks.TaskFactory __returnValue = new System.Threading.Tasks.TaskFactory(creationOptions, continuationOptions);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Scheduler
	

	// TODO (Property): CreationOptions
	

	// TODO (Property): ContinuationOptions
	

	[UnmanagedCallersOnly(EntryPoint="System_Threading_Tasks_TaskFactory_Destroy")]
	internal static void /* System.Void */ System_Threading_Tasks_TaskFactory_Destroy(void* /* System.Threading.Tasks.TaskFactory */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_IAsyncResult
{
	// TODO (Property): IsCompleted
	

	// TODO (Property): AsyncWaitHandle
	

	// TODO (Property): AsyncState
	

	// TODO (Property): CompletedSynchronously
	

	[UnmanagedCallersOnly(EntryPoint="System_IAsyncResult_Destroy")]
	internal static void /* System.Void */ System_IAsyncResult_Destroy(void* /* System.IAsyncResult */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Threading_WaitHandle
{
	[UnmanagedCallersOnly(EntryPoint = "System_Threading_WaitHandle_Close")]
	internal static void /* System.Void */ System_Threading_WaitHandle_Close(void* /* System.Threading.WaitHandle */ __self, void** /* System.Exception */ __outException)
	{
		System.Threading.WaitHandle __selfDotNet = InteropUtils.GetInstance<System.Threading.WaitHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Close();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_WaitHandle_Dispose")]
	internal static void /* System.Void */ System_Threading_WaitHandle_Dispose(void* /* System.Threading.WaitHandle */ __self, void** /* System.Exception */ __outException)
	{
		System.Threading.WaitHandle __selfDotNet = InteropUtils.GetInstance<System.Threading.WaitHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Dispose();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_WaitHandle_WaitOne")]
	internal static CBool /* System.Boolean */ System_Threading_WaitHandle_WaitOne(void* /* System.Threading.WaitHandle */ __self, int /* System.Int32 */ millisecondsTimeout, void** /* System.Exception */ __outException)
	{
		System.Threading.WaitHandle __selfDotNet = InteropUtils.GetInstance<System.Threading.WaitHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.WaitOne(millisecondsTimeout);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_WaitHandle_WaitOne1")]
	internal static CBool /* System.Boolean */ System_Threading_WaitHandle_WaitOne1(void* /* System.Threading.WaitHandle */ __self, void** /* System.Exception */ __outException)
	{
		System.Threading.WaitHandle __selfDotNet = InteropUtils.GetInstance<System.Threading.WaitHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.WaitOne();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_WaitHandle_WaitOne2")]
	internal static CBool /* System.Boolean */ System_Threading_WaitHandle_WaitOne2(void* /* System.Threading.WaitHandle */ __self, int /* System.Int32 */ millisecondsTimeout, CBool /* System.Boolean */ exitContext, void** /* System.Exception */ __outException)
	{
		System.Threading.WaitHandle __selfDotNet = InteropUtils.GetInstance<System.Threading.WaitHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Boolean exitContextDotNet = exitContext.ToBool();
	
	    try {
			System.Boolean __returnValue = __selfDotNet.WaitOne(millisecondsTimeout, exitContextDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_WaitHandle_SignalAndWait")]
	internal static CBool /* System.Boolean */ System_Threading_WaitHandle_SignalAndWait(void* /* System.Threading.WaitHandle */ toSignal, void* /* System.Threading.WaitHandle */ toWaitOn, void** /* System.Exception */ __outException)
	{
		System.Threading.WaitHandle toSignalDotNet = InteropUtils.GetInstance<System.Threading.WaitHandle>(toSignal);
		System.Threading.WaitHandle toWaitOnDotNet = InteropUtils.GetInstance<System.Threading.WaitHandle>(toWaitOn);
	
	    try {
			System.Boolean __returnValue = System.Threading.WaitHandle.SignalAndWait(toSignalDotNet, toWaitOnDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Threading_WaitHandle_SignalAndWait1")]
	internal static CBool /* System.Boolean */ System_Threading_WaitHandle_SignalAndWait1(void* /* System.Threading.WaitHandle */ toSignal, void* /* System.Threading.WaitHandle */ toWaitOn, int /* System.Int32 */ millisecondsTimeout, CBool /* System.Boolean */ exitContext, void** /* System.Exception */ __outException)
	{
		System.Threading.WaitHandle toSignalDotNet = InteropUtils.GetInstance<System.Threading.WaitHandle>(toSignal);
		System.Threading.WaitHandle toWaitOnDotNet = InteropUtils.GetInstance<System.Threading.WaitHandle>(toWaitOn);
		System.Boolean exitContextDotNet = exitContext.ToBool();
	
	    try {
			System.Boolean __returnValue = System.Threading.WaitHandle.SignalAndWait(toSignalDotNet, toWaitOnDotNet, millisecondsTimeout, exitContextDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): Handle
	

	// TODO (Property): SafeWaitHandle
	

	[UnmanagedCallersOnly(EntryPoint="System_Threading_WaitHandle_Destroy")]
	internal static void /* System.Void */ System_Threading_WaitHandle_Destroy(void* /* System.Threading.WaitHandle */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class Microsoft_Win32_SafeHandles_SafeWaitHandle
{
	[UnmanagedCallersOnly(EntryPoint = "Microsoft_Win32_SafeHandles_SafeWaitHandle_Create")]
	internal static void* /* Microsoft.Win32.SafeHandles.SafeWaitHandle */ Microsoft_Win32_SafeHandles_SafeWaitHandle_Create(void** /* System.Exception */ __outException)
	{
	
	    try {
			Microsoft.Win32.SafeHandles.SafeWaitHandle __returnValue = new Microsoft.Win32.SafeHandles.SafeWaitHandle();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "Microsoft_Win32_SafeHandles_SafeWaitHandle_Create1")]
	internal static void* /* Microsoft.Win32.SafeHandles.SafeWaitHandle */ Microsoft_Win32_SafeHandles_SafeWaitHandle_Create1(System.IntPtr /* System.IntPtr */ existingHandle, CBool /* System.Boolean */ ownsHandle, void** /* System.Exception */ __outException)
	{
		System.Boolean ownsHandleDotNet = ownsHandle.ToBool();
	
	    try {
			Microsoft.Win32.SafeHandles.SafeWaitHandle __returnValue = new Microsoft.Win32.SafeHandles.SafeWaitHandle(existingHandle, ownsHandleDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint="Microsoft_Win32_SafeHandles_SafeWaitHandle_Destroy")]
	internal static void /* System.Void */ Microsoft_Win32_SafeHandles_SafeWaitHandle_Destroy(void* /* Microsoft.Win32.SafeHandles.SafeWaitHandle */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid
{
	// TODO (Property): IsInvalid
	

	[UnmanagedCallersOnly(EntryPoint="Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid_Destroy")]
	internal static void /* System.Void */ Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid_Destroy(void* /* Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Runtime_InteropServices_SafeHandle
{
	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_InteropServices_SafeHandle_DangerousGetHandle")]
	internal static System.IntPtr /* System.IntPtr */ System_Runtime_InteropServices_SafeHandle_DangerousGetHandle(void* /* System.Runtime.InteropServices.SafeHandle */ __self, void** /* System.Exception */ __outException)
	{
		System.Runtime.InteropServices.SafeHandle __selfDotNet = InteropUtils.GetInstance<System.Runtime.InteropServices.SafeHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.IntPtr __returnValue = __selfDotNet.DangerousGetHandle();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.IntPtr);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_InteropServices_SafeHandle_Close")]
	internal static void /* System.Void */ System_Runtime_InteropServices_SafeHandle_Close(void* /* System.Runtime.InteropServices.SafeHandle */ __self, void** /* System.Exception */ __outException)
	{
		System.Runtime.InteropServices.SafeHandle __selfDotNet = InteropUtils.GetInstance<System.Runtime.InteropServices.SafeHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Close();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_InteropServices_SafeHandle_Dispose")]
	internal static void /* System.Void */ System_Runtime_InteropServices_SafeHandle_Dispose(void* /* System.Runtime.InteropServices.SafeHandle */ __self, void** /* System.Exception */ __outException)
	{
		System.Runtime.InteropServices.SafeHandle __selfDotNet = InteropUtils.GetInstance<System.Runtime.InteropServices.SafeHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Dispose();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_InteropServices_SafeHandle_SetHandleAsInvalid")]
	internal static void /* System.Void */ System_Runtime_InteropServices_SafeHandle_SetHandleAsInvalid(void* /* System.Runtime.InteropServices.SafeHandle */ __self, void** /* System.Exception */ __outException)
	{
		System.Runtime.InteropServices.SafeHandle __selfDotNet = InteropUtils.GetInstance<System.Runtime.InteropServices.SafeHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.SetHandleAsInvalid();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_InteropServices_SafeHandle_DangerousRelease")]
	internal static void /* System.Void */ System_Runtime_InteropServices_SafeHandle_DangerousRelease(void* /* System.Runtime.InteropServices.SafeHandle */ __self, void** /* System.Exception */ __outException)
	{
		System.Runtime.InteropServices.SafeHandle __selfDotNet = InteropUtils.GetInstance<System.Runtime.InteropServices.SafeHandle>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.DangerousRelease();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): IsClosed
	

	// TODO (Property): IsInvalid
	

	[UnmanagedCallersOnly(EntryPoint="System_Runtime_InteropServices_SafeHandle_Destroy")]
	internal static void /* System.Void */ System_Runtime_InteropServices_SafeHandle_Destroy(void* /* System.Runtime.InteropServices.SafeHandle */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Runtime_ConstrainedExecution_CriticalFinalizerObject
{
	[UnmanagedCallersOnly(EntryPoint="System_Runtime_ConstrainedExecution_CriticalFinalizerObject_Destroy")]
	internal static void /* System.Void */ System_Runtime_ConstrainedExecution_CriticalFinalizerObject_Destroy(void* /* System.Runtime.ConstrainedExecution.CriticalFinalizerObject */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class Microsoft_Win32_SafeHandles_SafeFileHandle
{
	[UnmanagedCallersOnly(EntryPoint = "Microsoft_Win32_SafeHandles_SafeFileHandle_Create")]
	internal static void* /* Microsoft.Win32.SafeHandles.SafeFileHandle */ Microsoft_Win32_SafeHandles_SafeFileHandle_Create(System.IntPtr /* System.IntPtr */ preexistingHandle, CBool /* System.Boolean */ ownsHandle, void** /* System.Exception */ __outException)
	{
		System.Boolean ownsHandleDotNet = ownsHandle.ToBool();
	
	    try {
			Microsoft.Win32.SafeHandles.SafeFileHandle __returnValue = new Microsoft.Win32.SafeHandles.SafeFileHandle(preexistingHandle, ownsHandleDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "Microsoft_Win32_SafeHandles_SafeFileHandle_Create1")]
	internal static void* /* Microsoft.Win32.SafeHandles.SafeFileHandle */ Microsoft_Win32_SafeHandles_SafeFileHandle_Create1(void** /* System.Exception */ __outException)
	{
	
	    try {
			Microsoft.Win32.SafeHandles.SafeFileHandle __returnValue = new Microsoft.Win32.SafeHandles.SafeFileHandle();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): IsAsync
	

	// TODO (Property): IsInvalid
	

	[UnmanagedCallersOnly(EntryPoint="Microsoft_Win32_SafeHandles_SafeFileHandle_Destroy")]
	internal static void /* System.Void */ Microsoft_Win32_SafeHandles_SafeFileHandle_Destroy(void* /* Microsoft.Win32.SafeHandles.SafeFileHandle */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}






internal static unsafe class System_IO_FileStreamOptions
{
	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStreamOptions_Create")]
	internal static void* /* System.IO.FileStreamOptions */ System_IO_FileStreamOptions_Create(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.IO.FileStreamOptions __returnValue = new System.IO.FileStreamOptions();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Mode
	

	// TODO (Property): Access
	

	// TODO (Property): Share
	

	// TODO (Property): Options
	

	// TODO (Property): PreallocationSize
	

	// TODO (Property): BufferSize
	

	[UnmanagedCallersOnly(EntryPoint="System_IO_FileStreamOptions_Destroy")]
	internal static void /* System.Void */ System_IO_FileStreamOptions_Destroy(void* /* System.IO.FileStreamOptions */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_ManifestResourceInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_ManifestResourceInfo_Create")]
	internal static void* /* System.Reflection.ManifestResourceInfo */ System_Reflection_ManifestResourceInfo_Create(void* /* System.Reflection.Assembly */ containingAssembly, byte* /* System.String */ containingFileName, System.Reflection.ResourceLocation /* System.Reflection.ResourceLocation */ resourceLocation, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly containingAssemblyDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(containingAssembly);
		System.String containingFileNameDotNet = InteropUtils.ToDotNetString(containingFileName);
	
	    try {
			System.Reflection.ManifestResourceInfo __returnValue = new System.Reflection.ManifestResourceInfo(containingAssemblyDotNet, containingFileNameDotNet, resourceLocation);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): ReferencedAssembly
	

	// TODO (Property): FileName
	

	// TODO (Property): ResourceLocation
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_ManifestResourceInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_ManifestResourceInfo_Destroy(void* /* System.Reflection.ManifestResourceInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Reflection_Module
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_IsResource")]
	internal static CBool /* System.Boolean */ System_Reflection_Module_IsResource(void* /* System.Reflection.Module */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsResource();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_IsDefined")]
	internal static CBool /* System.Boolean */ System_Reflection_Module_IsDefined(void* /* System.Reflection.Module */ __self, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsDefined(attributeTypeDotNet, inheritDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_GetMethod")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_Module_GetMethod(void* /* System.Reflection.Module */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetMethod(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_GetField")]
	internal static void* /* System.Reflection.FieldInfo */ System_Reflection_Module_GetField(void* /* System.Reflection.Module */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.FieldInfo __returnValue = __selfDotNet.GetField(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_GetField1")]
	internal static void* /* System.Reflection.FieldInfo */ System_Reflection_Module_GetField1(void* /* System.Reflection.Module */ __self, byte* /* System.String */ name, System.Reflection.BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Reflection.FieldInfo __returnValue = __selfDotNet.GetField(nameDotNet, bindingAttr);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_GetType")]
	internal static void* /* System.Type */ System_Reflection_Module_GetType(void* /* System.Reflection.Module */ __self, byte* /* System.String */ className, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String classNameDotNet = InteropUtils.ToDotNetString(className);
	
	    try {
			System.Type __returnValue = __selfDotNet.GetType(classNameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_GetType1")]
	internal static void* /* System.Type */ System_Reflection_Module_GetType1(void* /* System.Reflection.Module */ __self, byte* /* System.String */ className, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String classNameDotNet = InteropUtils.ToDotNetString(className);
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Type __returnValue = __selfDotNet.GetType(classNameDotNet, ignoreCaseDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_GetType2")]
	internal static void* /* System.Type */ System_Reflection_Module_GetType2(void* /* System.Reflection.Module */ __self, byte* /* System.String */ className, CBool /* System.Boolean */ throwOnError, CBool /* System.Boolean */ ignoreCase, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String classNameDotNet = InteropUtils.ToDotNetString(className);
		System.Boolean throwOnErrorDotNet = throwOnError.ToBool();
		System.Boolean ignoreCaseDotNet = ignoreCase.ToBool();
	
	    try {
			System.Type __returnValue = __selfDotNet.GetType(classNameDotNet, throwOnErrorDotNet, ignoreCaseDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_ResolveField")]
	internal static void* /* System.Reflection.FieldInfo */ System_Reflection_Module_ResolveField(void* /* System.Reflection.Module */ __self, int /* System.Int32 */ metadataToken, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.FieldInfo __returnValue = __selfDotNet.ResolveField(metadataToken);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_ResolveMember")]
	internal static void* /* System.Reflection.MemberInfo */ System_Reflection_Module_ResolveMember(void* /* System.Reflection.Module */ __self, int /* System.Int32 */ metadataToken, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MemberInfo __returnValue = __selfDotNet.ResolveMember(metadataToken);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_ResolveMethod")]
	internal static void* /* System.Reflection.MethodBase */ System_Reflection_Module_ResolveMethod(void* /* System.Reflection.Module */ __self, int /* System.Int32 */ metadataToken, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodBase __returnValue = __selfDotNet.ResolveMethod(metadataToken);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_ResolveString")]
	internal static byte* /* System.String */ System_Reflection_Module_ResolveString(void* /* System.Reflection.Module */ __self, int /* System.Int32 */ metadataToken, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ResolveString(metadataToken);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_ResolveType")]
	internal static void* /* System.Type */ System_Reflection_Module_ResolveType(void* /* System.Reflection.Module */ __self, int /* System.Int32 */ metadataToken, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Type __returnValue = __selfDotNet.ResolveType(metadataToken);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_Equals")]
	internal static CBool /* System.Boolean */ System_Reflection_Module_Equals(void* /* System.Reflection.Module */ __self, void* /* System.Object */ o, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object oDotNet = InteropUtils.GetInstance<System.Object>(o);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(oDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_GetHashCode")]
	internal static int /* System.Int32 */ System_Reflection_Module_GetHashCode(void* /* System.Reflection.Module */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_op_Equality")]
	internal static CBool /* System.Boolean */ System_Reflection_Module_op_Equality(void* /* System.Reflection.Module */ left, void* /* System.Reflection.Module */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module leftDotNet = InteropUtils.GetInstance<System.Reflection.Module>(left);
		System.Reflection.Module rightDotNet = InteropUtils.GetInstance<System.Reflection.Module>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.Module.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Reflection_Module_op_Inequality(void* /* System.Reflection.Module */ left, void* /* System.Reflection.Module */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module leftDotNet = InteropUtils.GetInstance<System.Reflection.Module>(left);
		System.Reflection.Module rightDotNet = InteropUtils.GetInstance<System.Reflection.Module>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.Module.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_Module_ToString")]
	internal static byte* /* System.String */ System_Reflection_Module_ToString(void* /* System.Reflection.Module */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module __selfDotNet = InteropUtils.GetInstance<System.Reflection.Module>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Assembly
	

	// TODO (Property): FullyQualifiedName
	

	// TODO (Property): Name
	

	// TODO (Property): MDStreamVersion
	

	// TODO (Property): ScopeName
	

	// TODO (Property): MetadataToken
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_Module_Destroy")]
	internal static void /* System.Void */ System_Reflection_Module_Destroy(void* /* System.Reflection.Module */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}




internal static unsafe class System_Globalization_SortKey
{
	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_SortKey_Compare")]
	internal static int /* System.Int32 */ System_Globalization_SortKey_Compare(void* /* System.Globalization.SortKey */ sortkey1, void* /* System.Globalization.SortKey */ sortkey2, void** /* System.Exception */ __outException)
	{
		System.Globalization.SortKey sortkey1DotNet = InteropUtils.GetInstance<System.Globalization.SortKey>(sortkey1);
		System.Globalization.SortKey sortkey2DotNet = InteropUtils.GetInstance<System.Globalization.SortKey>(sortkey2);
	
	    try {
			System.Int32 __returnValue = System.Globalization.SortKey.Compare(sortkey1DotNet, sortkey2DotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_SortKey_Equals")]
	internal static CBool /* System.Boolean */ System_Globalization_SortKey_Equals(void* /* System.Globalization.SortKey */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Globalization.SortKey __selfDotNet = InteropUtils.GetInstance<System.Globalization.SortKey>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_SortKey_GetHashCode")]
	internal static int /* System.Int32 */ System_Globalization_SortKey_GetHashCode(void* /* System.Globalization.SortKey */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.SortKey __selfDotNet = InteropUtils.GetInstance<System.Globalization.SortKey>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_SortKey_ToString")]
	internal static byte* /* System.String */ System_Globalization_SortKey_ToString(void* /* System.Globalization.SortKey */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.SortKey __selfDotNet = InteropUtils.GetInstance<System.Globalization.SortKey>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): OriginalString
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_SortKey_Destroy")]
	internal static void /* System.Void */ System_Globalization_SortKey_Destroy(void* /* System.Globalization.SortKey */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Globalization_SortVersion
{
	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_SortVersion_Equals")]
	internal static CBool /* System.Boolean */ System_Globalization_SortVersion_Equals(void* /* System.Globalization.SortVersion */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Globalization.SortVersion __selfDotNet = InteropUtils.GetInstance<System.Globalization.SortVersion>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_SortVersion_Equals1")]
	internal static CBool /* System.Boolean */ System_Globalization_SortVersion_Equals1(void* /* System.Globalization.SortVersion */ __self, void* /* System.Globalization.SortVersion */ other, void** /* System.Exception */ __outException)
	{
		System.Globalization.SortVersion __selfDotNet = InteropUtils.GetInstance<System.Globalization.SortVersion>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Globalization.SortVersion otherDotNet = InteropUtils.GetInstance<System.Globalization.SortVersion>(other);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(otherDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_SortVersion_GetHashCode")]
	internal static int /* System.Int32 */ System_Globalization_SortVersion_GetHashCode(void* /* System.Globalization.SortVersion */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.SortVersion __selfDotNet = InteropUtils.GetInstance<System.Globalization.SortVersion>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_SortVersion_op_Equality")]
	internal static CBool /* System.Boolean */ System_Globalization_SortVersion_op_Equality(void* /* System.Globalization.SortVersion */ left, void* /* System.Globalization.SortVersion */ right, void** /* System.Exception */ __outException)
	{
		System.Globalization.SortVersion leftDotNet = InteropUtils.GetInstance<System.Globalization.SortVersion>(left);
		System.Globalization.SortVersion rightDotNet = InteropUtils.GetInstance<System.Globalization.SortVersion>(right);
	
	    try {
			System.Boolean __returnValue = System.Globalization.SortVersion.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_SortVersion_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Globalization_SortVersion_op_Inequality(void* /* System.Globalization.SortVersion */ left, void* /* System.Globalization.SortVersion */ right, void** /* System.Exception */ __outException)
	{
		System.Globalization.SortVersion leftDotNet = InteropUtils.GetInstance<System.Globalization.SortVersion>(left);
		System.Globalization.SortVersion rightDotNet = InteropUtils.GetInstance<System.Globalization.SortVersion>(right);
	
	    try {
			System.Boolean __returnValue = System.Globalization.SortVersion.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): FullVersion
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_SortVersion_Destroy")]
	internal static void /* System.Void */ System_Globalization_SortVersion_Destroy(void* /* System.Globalization.SortVersion */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Globalization_TextInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_Clone")]
	internal static void* /* System.Object */ System_Globalization_TextInfo_Clone(void* /* System.Globalization.TextInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_ReadOnly")]
	internal static void* /* System.Globalization.TextInfo */ System_Globalization_TextInfo_ReadOnly(void* /* System.Globalization.TextInfo */ textInfo, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo textInfoDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(textInfo);
	
	    try {
			System.Globalization.TextInfo __returnValue = System.Globalization.TextInfo.ReadOnly(textInfoDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_ToLower")]
	internal static System.Char /* System.Char */ System_Globalization_TextInfo_ToLower(void* /* System.Globalization.TextInfo */ __self, System.Char /* System.Char */ c, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Char __returnValue = __selfDotNet.ToLower(c);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.Char);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_ToLower1")]
	internal static byte* /* System.String */ System_Globalization_TextInfo_ToLower1(void* /* System.Globalization.TextInfo */ __self, byte* /* System.String */ str, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String strDotNet = InteropUtils.ToDotNetString(str);
	
	    try {
			System.String __returnValue = __selfDotNet.ToLower(strDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_ToUpper")]
	internal static System.Char /* System.Char */ System_Globalization_TextInfo_ToUpper(void* /* System.Globalization.TextInfo */ __self, System.Char /* System.Char */ c, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Char __returnValue = __selfDotNet.ToUpper(c);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.Char);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_ToUpper1")]
	internal static byte* /* System.String */ System_Globalization_TextInfo_ToUpper1(void* /* System.Globalization.TextInfo */ __self, byte* /* System.String */ str, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String strDotNet = InteropUtils.ToDotNetString(str);
	
	    try {
			System.String __returnValue = __selfDotNet.ToUpper(strDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_Equals")]
	internal static CBool /* System.Boolean */ System_Globalization_TextInfo_Equals(void* /* System.Globalization.TextInfo */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_GetHashCode")]
	internal static int /* System.Int32 */ System_Globalization_TextInfo_GetHashCode(void* /* System.Globalization.TextInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_ToString")]
	internal static byte* /* System.String */ System_Globalization_TextInfo_ToString(void* /* System.Globalization.TextInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.ToString();
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_TextInfo_ToTitleCase")]
	internal static byte* /* System.String */ System_Globalization_TextInfo_ToTitleCase(void* /* System.Globalization.TextInfo */ __self, byte* /* System.String */ str, void** /* System.Exception */ __outException)
	{
		System.Globalization.TextInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.TextInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String strDotNet = InteropUtils.ToDotNetString(str);
	
	    try {
			System.String __returnValue = __selfDotNet.ToTitleCase(strDotNet);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): ANSICodePage
	

	// TODO (Property): OEMCodePage
	

	// TODO (Property): MacCodePage
	

	// TODO (Property): EBCDICCodePage
	

	// TODO (Property): LCID
	

	// TODO (Property): CultureName
	

	// TODO (Property): IsReadOnly
	

	// TODO (Property): ListSeparator
	

	// TODO (Property): IsRightToLeft
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_TextInfo_Destroy")]
	internal static void /* System.Void */ System_Globalization_TextInfo_Destroy(void* /* System.Globalization.TextInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Globalization_NumberFormatInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_NumberFormatInfo_GetInstance")]
	internal static void* /* System.Globalization.NumberFormatInfo */ System_Globalization_NumberFormatInfo_GetInstance(void* /* System.IFormatProvider */ formatProvider, void** /* System.Exception */ __outException)
	{
		System.IFormatProvider formatProviderDotNet = InteropUtils.GetInstance<System.IFormatProvider>(formatProvider);
	
	    try {
			System.Globalization.NumberFormatInfo __returnValue = System.Globalization.NumberFormatInfo.GetInstance(formatProviderDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_NumberFormatInfo_Clone")]
	internal static void* /* System.Object */ System_Globalization_NumberFormatInfo_Clone(void* /* System.Globalization.NumberFormatInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.NumberFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.NumberFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_NumberFormatInfo_GetFormat")]
	internal static void* /* System.Object */ System_Globalization_NumberFormatInfo_GetFormat(void* /* System.Globalization.NumberFormatInfo */ __self, void* /* System.Type */ formatType, void** /* System.Exception */ __outException)
	{
		System.Globalization.NumberFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.NumberFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type formatTypeDotNet = InteropUtils.GetInstance<System.Type>(formatType);
	
	    try {
			System.Object __returnValue = __selfDotNet.GetFormat(formatTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_NumberFormatInfo_ReadOnly")]
	internal static void* /* System.Globalization.NumberFormatInfo */ System_Globalization_NumberFormatInfo_ReadOnly(void* /* System.Globalization.NumberFormatInfo */ nfi, void** /* System.Exception */ __outException)
	{
		System.Globalization.NumberFormatInfo nfiDotNet = InteropUtils.GetInstance<System.Globalization.NumberFormatInfo>(nfi);
	
	    try {
			System.Globalization.NumberFormatInfo __returnValue = System.Globalization.NumberFormatInfo.ReadOnly(nfiDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_NumberFormatInfo_Create")]
	internal static void* /* System.Globalization.NumberFormatInfo */ System_Globalization_NumberFormatInfo_Create(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Globalization.NumberFormatInfo __returnValue = new System.Globalization.NumberFormatInfo();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): InvariantInfo
	

	// TODO (Property): CurrencyDecimalDigits
	

	// TODO (Property): CurrencyDecimalSeparator
	

	// TODO (Property): IsReadOnly
	

	// TODO (Property): CurrencyGroupSeparator
	

	// TODO (Property): CurrencySymbol
	

	// TODO (Property): CurrentInfo
	

	// TODO (Property): NaNSymbol
	

	// TODO (Property): CurrencyNegativePattern
	

	// TODO (Property): NumberNegativePattern
	

	// TODO (Property): PercentPositivePattern
	

	// TODO (Property): PercentNegativePattern
	

	// TODO (Property): NegativeInfinitySymbol
	

	// TODO (Property): NegativeSign
	

	// TODO (Property): NumberDecimalDigits
	

	// TODO (Property): NumberDecimalSeparator
	

	// TODO (Property): NumberGroupSeparator
	

	// TODO (Property): CurrencyPositivePattern
	

	// TODO (Property): PositiveInfinitySymbol
	

	// TODO (Property): PositiveSign
	

	// TODO (Property): PercentDecimalDigits
	

	// TODO (Property): PercentDecimalSeparator
	

	// TODO (Property): PercentGroupSeparator
	

	// TODO (Property): PercentSymbol
	

	// TODO (Property): PerMilleSymbol
	

	// TODO (Property): DigitSubstitution
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_NumberFormatInfo_Destroy")]
	internal static void /* System.Void */ System_Globalization_NumberFormatInfo_Destroy(void* /* System.Globalization.NumberFormatInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Globalization_DateTimeFormatInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetInstance")]
	internal static void* /* System.Globalization.DateTimeFormatInfo */ System_Globalization_DateTimeFormatInfo_GetInstance(void* /* System.IFormatProvider */ provider, void** /* System.Exception */ __outException)
	{
		System.IFormatProvider providerDotNet = InteropUtils.GetInstance<System.IFormatProvider>(provider);
	
	    try {
			System.Globalization.DateTimeFormatInfo __returnValue = System.Globalization.DateTimeFormatInfo.GetInstance(providerDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetFormat")]
	internal static void* /* System.Object */ System_Globalization_DateTimeFormatInfo_GetFormat(void* /* System.Globalization.DateTimeFormatInfo */ __self, void* /* System.Type */ formatType, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Type formatTypeDotNet = InteropUtils.GetInstance<System.Type>(formatType);
	
	    try {
			System.Object __returnValue = __selfDotNet.GetFormat(formatTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_Clone")]
	internal static void* /* System.Object */ System_Globalization_DateTimeFormatInfo_Clone(void* /* System.Globalization.DateTimeFormatInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetEra")]
	internal static int /* System.Int32 */ System_Globalization_DateTimeFormatInfo_GetEra(void* /* System.Globalization.DateTimeFormatInfo */ __self, byte* /* System.String */ eraName, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String eraNameDotNet = InteropUtils.ToDotNetString(eraName);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetEra(eraNameDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetEraName")]
	internal static byte* /* System.String */ System_Globalization_DateTimeFormatInfo_GetEraName(void* /* System.Globalization.DateTimeFormatInfo */ __self, int /* System.Int32 */ era, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.GetEraName(era);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetAbbreviatedEraName")]
	internal static byte* /* System.String */ System_Globalization_DateTimeFormatInfo_GetAbbreviatedEraName(void* /* System.Globalization.DateTimeFormatInfo */ __self, int /* System.Int32 */ era, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.GetAbbreviatedEraName(era);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetAbbreviatedDayName")]
	internal static byte* /* System.String */ System_Globalization_DateTimeFormatInfo_GetAbbreviatedDayName(void* /* System.Globalization.DateTimeFormatInfo */ __self, System.DayOfWeek /* System.DayOfWeek */ dayofweek, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.GetAbbreviatedDayName(dayofweek);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetShortestDayName")]
	internal static byte* /* System.String */ System_Globalization_DateTimeFormatInfo_GetShortestDayName(void* /* System.Globalization.DateTimeFormatInfo */ __self, System.DayOfWeek /* System.DayOfWeek */ dayOfWeek, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.GetShortestDayName(dayOfWeek);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetDayName")]
	internal static byte* /* System.String */ System_Globalization_DateTimeFormatInfo_GetDayName(void* /* System.Globalization.DateTimeFormatInfo */ __self, System.DayOfWeek /* System.DayOfWeek */ dayofweek, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.GetDayName(dayofweek);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetAbbreviatedMonthName")]
	internal static byte* /* System.String */ System_Globalization_DateTimeFormatInfo_GetAbbreviatedMonthName(void* /* System.Globalization.DateTimeFormatInfo */ __self, int /* System.Int32 */ month, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.GetAbbreviatedMonthName(month);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_GetMonthName")]
	internal static byte* /* System.String */ System_Globalization_DateTimeFormatInfo_GetMonthName(void* /* System.Globalization.DateTimeFormatInfo */ __self, int /* System.Int32 */ month, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo __selfDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.String __returnValue = __selfDotNet.GetMonthName(month);
			byte* _returnValueNative = __returnValue.CopyToCString();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_ReadOnly")]
	internal static void* /* System.Globalization.DateTimeFormatInfo */ System_Globalization_DateTimeFormatInfo_ReadOnly(void* /* System.Globalization.DateTimeFormatInfo */ dtfi, void** /* System.Exception */ __outException)
	{
		System.Globalization.DateTimeFormatInfo dtfiDotNet = InteropUtils.GetInstance<System.Globalization.DateTimeFormatInfo>(dtfi);
	
	    try {
			System.Globalization.DateTimeFormatInfo __returnValue = System.Globalization.DateTimeFormatInfo.ReadOnly(dtfiDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_DateTimeFormatInfo_Create")]
	internal static void* /* System.Globalization.DateTimeFormatInfo */ System_Globalization_DateTimeFormatInfo_Create(void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Globalization.DateTimeFormatInfo __returnValue = new System.Globalization.DateTimeFormatInfo();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): InvariantInfo
	

	// TODO (Property): CurrentInfo
	

	// TODO (Property): AMDesignator
	

	// TODO (Property): Calendar
	

	// TODO (Property): DateSeparator
	

	// TODO (Property): FirstDayOfWeek
	

	// TODO (Property): CalendarWeekRule
	

	// TODO (Property): FullDateTimePattern
	

	// TODO (Property): LongDatePattern
	

	// TODO (Property): LongTimePattern
	

	// TODO (Property): MonthDayPattern
	

	// TODO (Property): PMDesignator
	

	// TODO (Property): RFC1123Pattern
	

	// TODO (Property): ShortDatePattern
	

	// TODO (Property): ShortTimePattern
	

	// TODO (Property): SortableDateTimePattern
	

	// TODO (Property): TimeSeparator
	

	// TODO (Property): UniversalSortableDateTimePattern
	

	// TODO (Property): YearMonthPattern
	

	// TODO (Property): IsReadOnly
	

	// TODO (Property): NativeCalendarName
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_DateTimeFormatInfo_Destroy")]
	internal static void /* System.Void */ System_Globalization_DateTimeFormatInfo_Destroy(void* /* System.Globalization.DateTimeFormatInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Globalization_Calendar
{
	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_Clone")]
	internal static void* /* System.Object */ System_Globalization_Calendar_Clone(void* /* System.Globalization.Calendar */ __self, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_ReadOnly")]
	internal static void* /* System.Globalization.Calendar */ System_Globalization_Calendar_ReadOnly(void* /* System.Globalization.Calendar */ calendar, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar calendarDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(calendar);
	
	    try {
			System.Globalization.Calendar __returnValue = System.Globalization.Calendar.ReadOnly(calendarDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_GetDaysInMonth")]
	internal static int /* System.Int32 */ System_Globalization_Calendar_GetDaysInMonth(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ month, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetDaysInMonth(year, month);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_GetDaysInMonth1")]
	internal static int /* System.Int32 */ System_Globalization_Calendar_GetDaysInMonth1(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ month, int /* System.Int32 */ era, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetDaysInMonth(year, month, era);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_GetDaysInYear")]
	internal static int /* System.Int32 */ System_Globalization_Calendar_GetDaysInYear(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetDaysInYear(year);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_GetDaysInYear1")]
	internal static int /* System.Int32 */ System_Globalization_Calendar_GetDaysInYear1(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ era, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetDaysInYear(year, era);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_GetMonthsInYear")]
	internal static int /* System.Int32 */ System_Globalization_Calendar_GetMonthsInYear(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetMonthsInYear(year);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_GetMonthsInYear1")]
	internal static int /* System.Int32 */ System_Globalization_Calendar_GetMonthsInYear1(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ era, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetMonthsInYear(year, era);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_IsLeapDay")]
	internal static CBool /* System.Boolean */ System_Globalization_Calendar_IsLeapDay(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ month, int /* System.Int32 */ day, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsLeapDay(year, month, day);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_IsLeapDay1")]
	internal static CBool /* System.Boolean */ System_Globalization_Calendar_IsLeapDay1(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ month, int /* System.Int32 */ day, int /* System.Int32 */ era, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsLeapDay(year, month, day, era);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_IsLeapMonth")]
	internal static CBool /* System.Boolean */ System_Globalization_Calendar_IsLeapMonth(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ month, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsLeapMonth(year, month);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_IsLeapMonth1")]
	internal static CBool /* System.Boolean */ System_Globalization_Calendar_IsLeapMonth1(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ month, int /* System.Int32 */ era, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsLeapMonth(year, month, era);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_GetLeapMonth")]
	internal static int /* System.Int32 */ System_Globalization_Calendar_GetLeapMonth(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetLeapMonth(year);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_GetLeapMonth1")]
	internal static int /* System.Int32 */ System_Globalization_Calendar_GetLeapMonth1(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ era, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetLeapMonth(year, era);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_IsLeapYear")]
	internal static CBool /* System.Boolean */ System_Globalization_Calendar_IsLeapYear(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsLeapYear(year);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_IsLeapYear1")]
	internal static CBool /* System.Boolean */ System_Globalization_Calendar_IsLeapYear1(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, int /* System.Int32 */ era, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsLeapYear(year, era);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_Calendar_ToFourDigitYear")]
	internal static int /* System.Int32 */ System_Globalization_Calendar_ToFourDigitYear(void* /* System.Globalization.Calendar */ __self, int /* System.Int32 */ year, void** /* System.Exception */ __outException)
	{
		System.Globalization.Calendar __selfDotNet = InteropUtils.GetInstance<System.Globalization.Calendar>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.ToFourDigitYear(year);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	// TODO (Property): AlgorithmType
	

	// TODO (Property): IsReadOnly
	

	// TODO (Property): TwoDigitYearMax
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_Calendar_Destroy")]
	internal static void /* System.Void */ System_Globalization_Calendar_Destroy(void* /* System.Globalization.Calendar */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}





internal static unsafe class System_CharEnumerator
{
	[UnmanagedCallersOnly(EntryPoint = "System_CharEnumerator_Clone")]
	internal static void* /* System.Object */ System_CharEnumerator_Clone(void* /* System.CharEnumerator */ __self, void** /* System.Exception */ __outException)
	{
		System.CharEnumerator __selfDotNet = InteropUtils.GetInstance<System.CharEnumerator>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_CharEnumerator_MoveNext")]
	internal static CBool /* System.Boolean */ System_CharEnumerator_MoveNext(void* /* System.CharEnumerator */ __self, void** /* System.Exception */ __outException)
	{
		System.CharEnumerator __selfDotNet = InteropUtils.GetInstance<System.CharEnumerator>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.MoveNext();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_CharEnumerator_Dispose")]
	internal static void /* System.Void */ System_CharEnumerator_Dispose(void* /* System.CharEnumerator */ __self, void** /* System.Exception */ __outException)
	{
		System.CharEnumerator __selfDotNet = InteropUtils.GetInstance<System.CharEnumerator>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Dispose();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_CharEnumerator_Reset")]
	internal static void /* System.Void */ System_CharEnumerator_Reset(void* /* System.CharEnumerator */ __self, void** /* System.Exception */ __outException)
	{
		System.CharEnumerator __selfDotNet = InteropUtils.GetInstance<System.CharEnumerator>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Reset();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): Current
	

	[UnmanagedCallersOnly(EntryPoint="System_CharEnumerator_Destroy")]
	internal static void /* System.Void */ System_CharEnumerator_Destroy(void* /* System.CharEnumerator */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Text_CompositeFormat
{
	[UnmanagedCallersOnly(EntryPoint = "System_Text_CompositeFormat_Parse")]
	internal static void* /* System.Text.CompositeFormat */ System_Text_CompositeFormat_Parse(byte* /* System.String */ format, void** /* System.Exception */ __outException)
	{
		System.String formatDotNet = InteropUtils.ToDotNetString(format);
	
	    try {
			System.Text.CompositeFormat __returnValue = System.Text.CompositeFormat.Parse(formatDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Format
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_CompositeFormat_Destroy")]
	internal static void /* System.Void */ System_Text_CompositeFormat_Destroy(void* /* System.Text.CompositeFormat */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



internal static unsafe class System_Text_Encoding
{
	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_RegisterProvider")]
	internal static void /* System.Void */ System_Text_Encoding_RegisterProvider(void* /* System.Text.EncodingProvider */ provider, void** /* System.Exception */ __outException)
	{
		System.Text.EncodingProvider providerDotNet = InteropUtils.GetInstance<System.Text.EncodingProvider>(provider);
	
	    try {
			System.Text.Encoding.RegisterProvider(providerDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetEncoding")]
	internal static void* /* System.Text.Encoding */ System_Text_Encoding_GetEncoding(int /* System.Int32 */ codepage, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Text.Encoding __returnValue = System.Text.Encoding.GetEncoding(codepage);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetEncoding1")]
	internal static void* /* System.Text.Encoding */ System_Text_Encoding_GetEncoding1(int /* System.Int32 */ codepage, void* /* System.Text.EncoderFallback */ encoderFallback, void* /* System.Text.DecoderFallback */ decoderFallback, void** /* System.Exception */ __outException)
	{
		System.Text.EncoderFallback encoderFallbackDotNet = InteropUtils.GetInstance<System.Text.EncoderFallback>(encoderFallback);
		System.Text.DecoderFallback decoderFallbackDotNet = InteropUtils.GetInstance<System.Text.DecoderFallback>(decoderFallback);
	
	    try {
			System.Text.Encoding __returnValue = System.Text.Encoding.GetEncoding(codepage, encoderFallbackDotNet, decoderFallbackDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetEncoding2")]
	internal static void* /* System.Text.Encoding */ System_Text_Encoding_GetEncoding2(byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Text.Encoding __returnValue = System.Text.Encoding.GetEncoding(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetEncoding3")]
	internal static void* /* System.Text.Encoding */ System_Text_Encoding_GetEncoding3(byte* /* System.String */ name, void* /* System.Text.EncoderFallback */ encoderFallback, void* /* System.Text.DecoderFallback */ decoderFallback, void** /* System.Exception */ __outException)
	{
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Text.EncoderFallback encoderFallbackDotNet = InteropUtils.GetInstance<System.Text.EncoderFallback>(encoderFallback);
		System.Text.DecoderFallback decoderFallbackDotNet = InteropUtils.GetInstance<System.Text.DecoderFallback>(decoderFallback);
	
	    try {
			System.Text.Encoding __returnValue = System.Text.Encoding.GetEncoding(nameDotNet, encoderFallbackDotNet, decoderFallbackDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_Clone")]
	internal static void* /* System.Object */ System_Text_Encoding_Clone(void* /* System.Text.Encoding */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Object __returnValue = __selfDotNet.Clone();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetByteCount")]
	internal static int /* System.Int32 */ System_Text_Encoding_GetByteCount(void* /* System.Text.Encoding */ __self, byte* /* System.String */ s, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sDotNet = InteropUtils.ToDotNetString(s);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetByteCount(sDotNet);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetByteCount1")]
	internal static int /* System.Int32 */ System_Text_Encoding_GetByteCount1(void* /* System.Text.Encoding */ __self, byte* /* System.String */ s, int /* System.Int32 */ index, int /* System.Int32 */ count, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String sDotNet = InteropUtils.ToDotNetString(s);
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetByteCount(sDotNet, index, count);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_IsAlwaysNormalized")]
	internal static CBool /* System.Boolean */ System_Text_Encoding_IsAlwaysNormalized(void* /* System.Text.Encoding */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsAlwaysNormalized();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_IsAlwaysNormalized1")]
	internal static CBool /* System.Boolean */ System_Text_Encoding_IsAlwaysNormalized1(void* /* System.Text.Encoding */ __self, System.Text.NormalizationForm /* System.Text.NormalizationForm */ form, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsAlwaysNormalized(form);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetDecoder")]
	internal static void* /* System.Text.Decoder */ System_Text_Encoding_GetDecoder(void* /* System.Text.Encoding */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Text.Decoder __returnValue = __selfDotNet.GetDecoder();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetEncoder")]
	internal static void* /* System.Text.Encoder */ System_Text_Encoding_GetEncoder(void* /* System.Text.Encoding */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Text.Encoder __returnValue = __selfDotNet.GetEncoder();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetMaxByteCount")]
	internal static int /* System.Int32 */ System_Text_Encoding_GetMaxByteCount(void* /* System.Text.Encoding */ __self, int /* System.Int32 */ charCount, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetMaxByteCount(charCount);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetMaxCharCount")]
	internal static int /* System.Int32 */ System_Text_Encoding_GetMaxCharCount(void* /* System.Text.Encoding */ __self, int /* System.Int32 */ byteCount, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetMaxCharCount(byteCount);
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_Equals")]
	internal static CBool /* System.Boolean */ System_Text_Encoding_Equals(void* /* System.Text.Encoding */ __self, void* /* System.Object */ value, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object valueDotNet = InteropUtils.GetInstance<System.Object>(value);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(valueDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetHashCode")]
	internal static int /* System.Int32 */ System_Text_Encoding_GetHashCode(void* /* System.Text.Encoding */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.Encoding __selfDotNet = InteropUtils.GetInstance<System.Text.Encoding>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_CreateTranscodingStream")]
	internal static void* /* System.IO.Stream */ System_Text_Encoding_CreateTranscodingStream(void* /* System.IO.Stream */ innerStream, void* /* System.Text.Encoding */ innerStreamEncoding, void* /* System.Text.Encoding */ outerStreamEncoding, CBool /* System.Boolean */ leaveOpen, void** /* System.Exception */ __outException)
	{
		System.IO.Stream innerStreamDotNet = InteropUtils.GetInstance<System.IO.Stream>(innerStream);
		System.Text.Encoding innerStreamEncodingDotNet = InteropUtils.GetInstance<System.Text.Encoding>(innerStreamEncoding);
		System.Text.Encoding outerStreamEncodingDotNet = InteropUtils.GetInstance<System.Text.Encoding>(outerStreamEncoding);
		System.Boolean leaveOpenDotNet = leaveOpen.ToBool();
	
	    try {
			System.IO.Stream __returnValue = System.Text.Encoding.CreateTranscodingStream(innerStreamDotNet, innerStreamEncodingDotNet, outerStreamEncodingDotNet, leaveOpenDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Default
	

	// TODO (Property): BodyName
	

	// TODO (Property): EncodingName
	

	// TODO (Property): HeaderName
	

	// TODO (Property): WebName
	

	// TODO (Property): WindowsCodePage
	

	// TODO (Property): IsBrowserDisplay
	

	// TODO (Property): IsBrowserSave
	

	// TODO (Property): IsMailNewsDisplay
	

	// TODO (Property): IsMailNewsSave
	

	// TODO (Property): IsSingleByte
	

	// TODO (Property): EncoderFallback
	

	// TODO (Property): DecoderFallback
	

	// TODO (Property): IsReadOnly
	

	// TODO (Property): ASCII
	

	// TODO (Property): Latin1
	

	// TODO (Property): CodePage
	

	// TODO (Property): Unicode
	

	// TODO (Property): BigEndianUnicode
	

	// TODO (Property): UTF7
	

	// TODO (Property): UTF8
	

	// TODO (Property): UTF32
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_Encoding_Destroy")]
	internal static void /* System.Void */ System_Text_Encoding_Destroy(void* /* System.Text.Encoding */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_EncodingProvider
{
	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncodingProvider_GetEncoding")]
	internal static void* /* System.Text.Encoding */ System_Text_EncodingProvider_GetEncoding(void* /* System.Text.EncodingProvider */ __self, byte* /* System.String */ name, void** /* System.Exception */ __outException)
	{
		System.Text.EncodingProvider __selfDotNet = InteropUtils.GetInstance<System.Text.EncodingProvider>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
	
	    try {
			System.Text.Encoding __returnValue = __selfDotNet.GetEncoding(nameDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncodingProvider_GetEncoding1")]
	internal static void* /* System.Text.Encoding */ System_Text_EncodingProvider_GetEncoding1(void* /* System.Text.EncodingProvider */ __self, int /* System.Int32 */ codepage, void** /* System.Exception */ __outException)
	{
		System.Text.EncodingProvider __selfDotNet = InteropUtils.GetInstance<System.Text.EncodingProvider>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Text.Encoding __returnValue = __selfDotNet.GetEncoding(codepage);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncodingProvider_GetEncoding2")]
	internal static void* /* System.Text.Encoding */ System_Text_EncodingProvider_GetEncoding2(void* /* System.Text.EncodingProvider */ __self, byte* /* System.String */ name, void* /* System.Text.EncoderFallback */ encoderFallback, void* /* System.Text.DecoderFallback */ decoderFallback, void** /* System.Exception */ __outException)
	{
		System.Text.EncodingProvider __selfDotNet = InteropUtils.GetInstance<System.Text.EncodingProvider>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.String nameDotNet = InteropUtils.ToDotNetString(name);
		System.Text.EncoderFallback encoderFallbackDotNet = InteropUtils.GetInstance<System.Text.EncoderFallback>(encoderFallback);
		System.Text.DecoderFallback decoderFallbackDotNet = InteropUtils.GetInstance<System.Text.DecoderFallback>(decoderFallback);
	
	    try {
			System.Text.Encoding __returnValue = __selfDotNet.GetEncoding(nameDotNet, encoderFallbackDotNet, decoderFallbackDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncodingProvider_GetEncoding3")]
	internal static void* /* System.Text.Encoding */ System_Text_EncodingProvider_GetEncoding3(void* /* System.Text.EncodingProvider */ __self, int /* System.Int32 */ codepage, void* /* System.Text.EncoderFallback */ encoderFallback, void* /* System.Text.DecoderFallback */ decoderFallback, void** /* System.Exception */ __outException)
	{
		System.Text.EncodingProvider __selfDotNet = InteropUtils.GetInstance<System.Text.EncodingProvider>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Text.EncoderFallback encoderFallbackDotNet = InteropUtils.GetInstance<System.Text.EncoderFallback>(encoderFallback);
		System.Text.DecoderFallback decoderFallbackDotNet = InteropUtils.GetInstance<System.Text.DecoderFallback>(decoderFallback);
	
	    try {
			System.Text.Encoding __returnValue = __selfDotNet.GetEncoding(codepage, encoderFallbackDotNet, decoderFallbackDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	

	[UnmanagedCallersOnly(EntryPoint="System_Text_EncodingProvider_Destroy")]
	internal static void /* System.Void */ System_Text_EncodingProvider_Destroy(void* /* System.Text.EncodingProvider */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_EncoderFallback
{
	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncoderFallback_CreateFallbackBuffer")]
	internal static void* /* System.Text.EncoderFallbackBuffer */ System_Text_EncoderFallback_CreateFallbackBuffer(void* /* System.Text.EncoderFallback */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.EncoderFallback __selfDotNet = InteropUtils.GetInstance<System.Text.EncoderFallback>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Text.EncoderFallbackBuffer __returnValue = __selfDotNet.CreateFallbackBuffer();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): ReplacementFallback
	

	// TODO (Property): ExceptionFallback
	

	// TODO (Property): MaxCharCount
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_EncoderFallback_Destroy")]
	internal static void /* System.Void */ System_Text_EncoderFallback_Destroy(void* /* System.Text.EncoderFallback */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_EncoderFallbackBuffer
{
	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncoderFallbackBuffer_Fallback")]
	internal static CBool /* System.Boolean */ System_Text_EncoderFallbackBuffer_Fallback(void* /* System.Text.EncoderFallbackBuffer */ __self, System.Char /* System.Char */ charUnknown, int /* System.Int32 */ index, void** /* System.Exception */ __outException)
	{
		System.Text.EncoderFallbackBuffer __selfDotNet = InteropUtils.GetInstance<System.Text.EncoderFallbackBuffer>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Fallback(charUnknown, index);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncoderFallbackBuffer_Fallback1")]
	internal static CBool /* System.Boolean */ System_Text_EncoderFallbackBuffer_Fallback1(void* /* System.Text.EncoderFallbackBuffer */ __self, System.Char /* System.Char */ charUnknownHigh, System.Char /* System.Char */ charUnknownLow, int /* System.Int32 */ index, void** /* System.Exception */ __outException)
	{
		System.Text.EncoderFallbackBuffer __selfDotNet = InteropUtils.GetInstance<System.Text.EncoderFallbackBuffer>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Fallback(charUnknownHigh, charUnknownLow, index);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncoderFallbackBuffer_GetNextChar")]
	internal static System.Char /* System.Char */ System_Text_EncoderFallbackBuffer_GetNextChar(void* /* System.Text.EncoderFallbackBuffer */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.EncoderFallbackBuffer __selfDotNet = InteropUtils.GetInstance<System.Text.EncoderFallbackBuffer>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Char __returnValue = __selfDotNet.GetNextChar();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.Char);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncoderFallbackBuffer_MovePrevious")]
	internal static CBool /* System.Boolean */ System_Text_EncoderFallbackBuffer_MovePrevious(void* /* System.Text.EncoderFallbackBuffer */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.EncoderFallbackBuffer __selfDotNet = InteropUtils.GetInstance<System.Text.EncoderFallbackBuffer>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.MovePrevious();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_EncoderFallbackBuffer_Reset")]
	internal static void /* System.Void */ System_Text_EncoderFallbackBuffer_Reset(void* /* System.Text.EncoderFallbackBuffer */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.EncoderFallbackBuffer __selfDotNet = InteropUtils.GetInstance<System.Text.EncoderFallbackBuffer>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Reset();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): Remaining
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_EncoderFallbackBuffer_Destroy")]
	internal static void /* System.Void */ System_Text_EncoderFallbackBuffer_Destroy(void* /* System.Text.EncoderFallbackBuffer */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_DecoderFallback
{
	[UnmanagedCallersOnly(EntryPoint = "System_Text_DecoderFallback_CreateFallbackBuffer")]
	internal static void* /* System.Text.DecoderFallbackBuffer */ System_Text_DecoderFallback_CreateFallbackBuffer(void* /* System.Text.DecoderFallback */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.DecoderFallback __selfDotNet = InteropUtils.GetInstance<System.Text.DecoderFallback>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Text.DecoderFallbackBuffer __returnValue = __selfDotNet.CreateFallbackBuffer();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): ReplacementFallback
	

	// TODO (Property): ExceptionFallback
	

	// TODO (Property): MaxCharCount
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_DecoderFallback_Destroy")]
	internal static void /* System.Void */ System_Text_DecoderFallback_Destroy(void* /* System.Text.DecoderFallback */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_DecoderFallbackBuffer
{
	[UnmanagedCallersOnly(EntryPoint = "System_Text_DecoderFallbackBuffer_GetNextChar")]
	internal static System.Char /* System.Char */ System_Text_DecoderFallbackBuffer_GetNextChar(void* /* System.Text.DecoderFallbackBuffer */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.DecoderFallbackBuffer __selfDotNet = InteropUtils.GetInstance<System.Text.DecoderFallbackBuffer>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Char __returnValue = __selfDotNet.GetNextChar();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return default(System.Char);
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_DecoderFallbackBuffer_MovePrevious")]
	internal static CBool /* System.Boolean */ System_Text_DecoderFallbackBuffer_MovePrevious(void* /* System.Text.DecoderFallbackBuffer */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.DecoderFallbackBuffer __selfDotNet = InteropUtils.GetInstance<System.Text.DecoderFallbackBuffer>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.MovePrevious();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_DecoderFallbackBuffer_Reset")]
	internal static void /* System.Void */ System_Text_DecoderFallbackBuffer_Reset(void* /* System.Text.DecoderFallbackBuffer */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.DecoderFallbackBuffer __selfDotNet = InteropUtils.GetInstance<System.Text.DecoderFallbackBuffer>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Reset();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): Remaining
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_DecoderFallbackBuffer_Destroy")]
	internal static void /* System.Void */ System_Text_DecoderFallbackBuffer_Destroy(void* /* System.Text.DecoderFallbackBuffer */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_Decoder
{
	[UnmanagedCallersOnly(EntryPoint = "System_Text_Decoder_Reset")]
	internal static void /* System.Void */ System_Text_Decoder_Reset(void* /* System.Text.Decoder */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.Decoder __selfDotNet = InteropUtils.GetInstance<System.Text.Decoder>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Reset();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): Fallback
	

	// TODO (Property): FallbackBuffer
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_Decoder_Destroy")]
	internal static void /* System.Void */ System_Text_Decoder_Destroy(void* /* System.Text.Decoder */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_Encoder
{
	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoder_Reset")]
	internal static void /* System.Void */ System_Text_Encoder_Reset(void* /* System.Text.Encoder */ __self, void** /* System.Exception */ __outException)
	{
		System.Text.Encoder __selfDotNet = InteropUtils.GetInstance<System.Text.Encoder>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			__selfDotNet.Reset();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
		}
	}
	

	// TODO (Property): Fallback
	

	// TODO (Property): FallbackBuffer
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_Encoder_Destroy")]
	internal static void /* System.Void */ System_Text_Encoder_Destroy(void* /* System.Text.Encoder */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}




internal static unsafe class System_Runtime_InteropServices_StructLayoutAttribute
{
	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_InteropServices_StructLayoutAttribute_Create")]
	internal static void* /* System.Runtime.InteropServices.StructLayoutAttribute */ System_Runtime_InteropServices_StructLayoutAttribute_Create(System.Runtime.InteropServices.LayoutKind /* System.Runtime.InteropServices.LayoutKind */ layoutKind, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Runtime.InteropServices.StructLayoutAttribute __returnValue = new System.Runtime.InteropServices.StructLayoutAttribute(layoutKind);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Runtime_InteropServices_StructLayoutAttribute_Create1")]
	internal static void* /* System.Runtime.InteropServices.StructLayoutAttribute */ System_Runtime_InteropServices_StructLayoutAttribute_Create1(short /* System.Int16 */ layoutKind, void** /* System.Exception */ __outException)
	{
	
	    try {
			System.Runtime.InteropServices.StructLayoutAttribute __returnValue = new System.Runtime.InteropServices.StructLayoutAttribute(layoutKind);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	// TODO (Property): Value
	

	[UnmanagedCallersOnly(EntryPoint="System_Runtime_InteropServices_StructLayoutAttribute_Destroy")]
	internal static void /* System.Void */ System_Runtime_InteropServices_StructLayoutAttribute_Destroy(void* /* System.Runtime.InteropServices.StructLayoutAttribute */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Attribute
{
	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_IsDefined")]
	internal static CBool /* System.Boolean */ System_Attribute_IsDefined(void* /* System.Reflection.MemberInfo */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo elementDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
	
	    try {
			System.Boolean __returnValue = System.Attribute.IsDefined(elementDotNet, attributeTypeDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_IsDefined1")]
	internal static CBool /* System.Boolean */ System_Attribute_IsDefined1(void* /* System.Reflection.MemberInfo */ element, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo elementDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Boolean __returnValue = System.Attribute.IsDefined(elementDotNet, attributeTypeDotNet, inheritDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute(void* /* System.Reflection.MemberInfo */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo elementDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
	
	    try {
			System.Attribute __returnValue = System.Attribute.GetCustomAttribute(elementDotNet, attributeTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute1")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute1(void* /* System.Reflection.MemberInfo */ element, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.MemberInfo elementDotNet = InteropUtils.GetInstance<System.Reflection.MemberInfo>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Attribute __returnValue = System.Attribute.GetCustomAttribute(elementDotNet, attributeTypeDotNet, inheritDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_IsDefined2")]
	internal static CBool /* System.Boolean */ System_Attribute_IsDefined2(void* /* System.Reflection.ParameterInfo */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
	{
		System.Reflection.ParameterInfo elementDotNet = InteropUtils.GetInstance<System.Reflection.ParameterInfo>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
	
	    try {
			System.Boolean __returnValue = System.Attribute.IsDefined(elementDotNet, attributeTypeDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_IsDefined3")]
	internal static CBool /* System.Boolean */ System_Attribute_IsDefined3(void* /* System.Reflection.ParameterInfo */ element, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.ParameterInfo elementDotNet = InteropUtils.GetInstance<System.Reflection.ParameterInfo>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Boolean __returnValue = System.Attribute.IsDefined(elementDotNet, attributeTypeDotNet, inheritDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute2")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute2(void* /* System.Reflection.ParameterInfo */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
	{
		System.Reflection.ParameterInfo elementDotNet = InteropUtils.GetInstance<System.Reflection.ParameterInfo>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
	
	    try {
			System.Attribute __returnValue = System.Attribute.GetCustomAttribute(elementDotNet, attributeTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute3")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute3(void* /* System.Reflection.ParameterInfo */ element, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.ParameterInfo elementDotNet = InteropUtils.GetInstance<System.Reflection.ParameterInfo>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Attribute __returnValue = System.Attribute.GetCustomAttribute(elementDotNet, attributeTypeDotNet, inheritDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_IsDefined4")]
	internal static CBool /* System.Boolean */ System_Attribute_IsDefined4(void* /* System.Reflection.Module */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module elementDotNet = InteropUtils.GetInstance<System.Reflection.Module>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
	
	    try {
			System.Boolean __returnValue = System.Attribute.IsDefined(elementDotNet, attributeTypeDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_IsDefined5")]
	internal static CBool /* System.Boolean */ System_Attribute_IsDefined5(void* /* System.Reflection.Module */ element, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module elementDotNet = InteropUtils.GetInstance<System.Reflection.Module>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Boolean __returnValue = System.Attribute.IsDefined(elementDotNet, attributeTypeDotNet, inheritDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute4")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute4(void* /* System.Reflection.Module */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module elementDotNet = InteropUtils.GetInstance<System.Reflection.Module>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
	
	    try {
			System.Attribute __returnValue = System.Attribute.GetCustomAttribute(elementDotNet, attributeTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute5")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute5(void* /* System.Reflection.Module */ element, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.Module elementDotNet = InteropUtils.GetInstance<System.Reflection.Module>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Attribute __returnValue = System.Attribute.GetCustomAttribute(elementDotNet, attributeTypeDotNet, inheritDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_IsDefined6")]
	internal static CBool /* System.Boolean */ System_Attribute_IsDefined6(void* /* System.Reflection.Assembly */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly elementDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
	
	    try {
			System.Boolean __returnValue = System.Attribute.IsDefined(elementDotNet, attributeTypeDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_IsDefined7")]
	internal static CBool /* System.Boolean */ System_Attribute_IsDefined7(void* /* System.Reflection.Assembly */ element, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly elementDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Boolean __returnValue = System.Attribute.IsDefined(elementDotNet, attributeTypeDotNet, inheritDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute6")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute6(void* /* System.Reflection.Assembly */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly elementDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
	
	    try {
			System.Attribute __returnValue = System.Attribute.GetCustomAttribute(elementDotNet, attributeTypeDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute7")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute7(void* /* System.Reflection.Assembly */ element, void* /* System.Type */ attributeType, CBool /* System.Boolean */ inherit, void** /* System.Exception */ __outException)
	{
		System.Reflection.Assembly elementDotNet = InteropUtils.GetInstance<System.Reflection.Assembly>(element);
		System.Type attributeTypeDotNet = InteropUtils.GetInstance<System.Type>(attributeType);
		System.Boolean inheritDotNet = inherit.ToBool();
	
	    try {
			System.Attribute __returnValue = System.Attribute.GetCustomAttribute(elementDotNet, attributeTypeDotNet, inheritDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_Equals")]
	internal static CBool /* System.Boolean */ System_Attribute_Equals(void* /* System.Attribute */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Attribute __selfDotNet = InteropUtils.GetInstance<System.Attribute>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetHashCode")]
	internal static int /* System.Int32 */ System_Attribute_GetHashCode(void* /* System.Attribute */ __self, void** /* System.Exception */ __outException)
	{
		System.Attribute __selfDotNet = InteropUtils.GetInstance<System.Attribute>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_Match")]
	internal static CBool /* System.Boolean */ System_Attribute_Match(void* /* System.Attribute */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Attribute __selfDotNet = InteropUtils.GetInstance<System.Attribute>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Match(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_IsDefaultAttribute")]
	internal static CBool /* System.Boolean */ System_Attribute_IsDefaultAttribute(void* /* System.Attribute */ __self, void** /* System.Exception */ __outException)
	{
		System.Attribute __selfDotNet = InteropUtils.GetInstance<System.Attribute>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Boolean __returnValue = __selfDotNet.IsDefaultAttribute();
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): TypeId
	

	[UnmanagedCallersOnly(EntryPoint="System_Attribute_Destroy")]
	internal static void /* System.Void */ System_Attribute_Destroy(void* /* System.Attribute */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}




internal static unsafe class System_Reflection_ConstructorInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_ConstructorInfo_Equals")]
	internal static CBool /* System.Boolean */ System_Reflection_ConstructorInfo_Equals(void* /* System.Reflection.ConstructorInfo */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Reflection.ConstructorInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.ConstructorInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_ConstructorInfo_GetHashCode")]
	internal static int /* System.Int32 */ System_Reflection_ConstructorInfo_GetHashCode(void* /* System.Reflection.ConstructorInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.ConstructorInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.ConstructorInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_ConstructorInfo_op_Equality")]
	internal static CBool /* System.Boolean */ System_Reflection_ConstructorInfo_op_Equality(void* /* System.Reflection.ConstructorInfo */ left, void* /* System.Reflection.ConstructorInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.ConstructorInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.ConstructorInfo>(left);
		System.Reflection.ConstructorInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.ConstructorInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.ConstructorInfo.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_ConstructorInfo_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Reflection_ConstructorInfo_op_Inequality(void* /* System.Reflection.ConstructorInfo */ left, void* /* System.Reflection.ConstructorInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.ConstructorInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.ConstructorInfo>(left);
		System.Reflection.ConstructorInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.ConstructorInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.ConstructorInfo.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): MemberType
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_ConstructorInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_ConstructorInfo_Destroy(void* /* System.Reflection.ConstructorInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_EventInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_GetAddMethod")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_EventInfo_GetAddMethod(void* /* System.Reflection.EventInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetAddMethod();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_GetRemoveMethod")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_EventInfo_GetRemoveMethod(void* /* System.Reflection.EventInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetRemoveMethod();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_GetRaiseMethod")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_EventInfo_GetRaiseMethod(void* /* System.Reflection.EventInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetRaiseMethod();
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_GetAddMethod1")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_EventInfo_GetAddMethod1(void* /* System.Reflection.EventInfo */ __self, CBool /* System.Boolean */ nonPublic, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Boolean nonPublicDotNet = nonPublic.ToBool();
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetAddMethod(nonPublicDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_GetRemoveMethod1")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_EventInfo_GetRemoveMethod1(void* /* System.Reflection.EventInfo */ __self, CBool /* System.Boolean */ nonPublic, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Boolean nonPublicDotNet = nonPublic.ToBool();
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetRemoveMethod(nonPublicDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_GetRaiseMethod1")]
	internal static void* /* System.Reflection.MethodInfo */ System_Reflection_EventInfo_GetRaiseMethod1(void* /* System.Reflection.EventInfo */ __self, CBool /* System.Boolean */ nonPublic, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Boolean nonPublicDotNet = nonPublic.ToBool();
	
	    try {
			System.Reflection.MethodInfo __returnValue = __selfDotNet.GetRaiseMethod(nonPublicDotNet);
			void* _returnValueNative = __returnValue.AllocateGCHandleAndGetAddress();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return null;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_Equals")]
	internal static CBool /* System.Boolean */ System_Reflection_EventInfo_Equals(void* /* System.Reflection.EventInfo */ __self, void* /* System.Object */ obj, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
		System.Object objDotNet = InteropUtils.GetInstance<System.Object>(obj);
	
	    try {
			System.Boolean __returnValue = __selfDotNet.Equals(objDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_GetHashCode")]
	internal static int /* System.Int32 */ System_Reflection_EventInfo_GetHashCode(void* /* System.Reflection.EventInfo */ __self, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo __selfDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(__self) ?? throw new ArgumentNullException(nameof(__self));
	
	
	    try {
			System.Int32 __returnValue = __selfDotNet.GetHashCode();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return __returnValue;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return -1;
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_op_Equality")]
	internal static CBool /* System.Boolean */ System_Reflection_EventInfo_op_Equality(void* /* System.Reflection.EventInfo */ left, void* /* System.Reflection.EventInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(left);
		System.Reflection.EventInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.EventInfo.op_Equality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	[UnmanagedCallersOnly(EntryPoint = "System_Reflection_EventInfo_op_Inequality")]
	internal static CBool /* System.Boolean */ System_Reflection_EventInfo_op_Inequality(void* /* System.Reflection.EventInfo */ left, void* /* System.Reflection.EventInfo */ right, void** /* System.Exception */ __outException)
	{
		System.Reflection.EventInfo leftDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(left);
		System.Reflection.EventInfo rightDotNet = InteropUtils.GetInstance<System.Reflection.EventInfo>(right);
	
	    try {
			System.Boolean __returnValue = System.Reflection.EventInfo.op_Inequality(leftDotNet, rightDotNet);
			CBool _returnValueNative = __returnValue.ToCBool();
	
	        if (__outException is not null) {
	            *__outException = null;
	        }
	
			return _returnValueNative;
	    } catch (Exception __exception) {
	        if (__outException is not null) {
	            void* __exceptionHandleAddress = __exception.AllocateGCHandleAndGetAddress();
	                
	            *__outException = __exceptionHandleAddress;
	        }
	
			return false.ToCBool();
		}
	}
	

	// TODO (Property): MemberType
	

	// TODO (Property): Attributes
	

	// TODO (Property): IsSpecialName
	

	// TODO (Property): AddMethod
	

	// TODO (Property): RemoveMethod
	

	// TODO (Property): RaiseMethod
	

	// TODO (Property): IsMulticast
	

	// TODO (Property): EventHandlerType
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_EventInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_EventInfo_Destroy(void* /* System.Reflection.EventInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}




// </APIs>
