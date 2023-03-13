// Number of generated types: 67
// Number of generated members: 198

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
	internal static void* /* System.Type */ System_Type_GetType(byte* /* System.String */ typeName, void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint = "System_Type_GetType1")]
	internal static void* /* System.Type */ System_Type_GetType1(void* /* System.Type */ __self, void** /* System.Exception */ __outException)
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
	internal static void* /* System.Reflection.PropertyInfo */ System_Type_GetProperty1(void* /* System.Type */ __self, byte* /* System.String */ name, void* /* System.Type */ returnType, void** /* System.Exception */ __outException)
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
	internal static void* /* System.Type */ System_Type_GetTypeFromProgID1(byte* /* System.String */ progID, byte* /* System.String */ server, void** /* System.Exception */ __outException)
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
	

	// TODO (Property): Namespace
	

	// TODO (Property): AssemblyQualifiedName
	

	// TODO (Property): FullName
	

	// TODO (Property): Assembly
	

	// TODO (Property): Module
	

	// TODO (Property): DeclaringType
	

	// TODO (Property): DeclaringMethod
	

	// TODO (Property): ReflectedType
	

	// TODO (Property): UnderlyingSystemType
	

	// TODO (Property): StructLayoutAttribute
	

	// TODO (Property): TypeInitializer
	

	// TODO (Property): BaseType
	

	// TODO (Property): DefaultBinder
	

	[UnmanagedCallersOnly(EntryPoint="System_Type_Destroy")]
	internal static void /* System.Void */ System_Type_Destroy(void* /* System.Type */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_MemberInfo
{
	// TODO (Property): Name
	

	// TODO (Property): DeclaringType
	

	// TODO (Property): ReflectedType
	

	// TODO (Property): Module
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_MemberInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_MemberInfo_Destroy(void* /* System.Reflection.MemberInfo */ __self)
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
	

	[UnmanagedCallersOnly(EntryPoint = "System_String_Replace")]
	internal static byte* /* System.String */ System_String_Replace(byte* /* System.String */ __self, byte* /* System.String */ oldValue, byte* /* System.String */ newValue, void** /* System.Exception */ __outException)
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
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_GetCultureInfo(byte* /* System.String */ name, void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CultureInfo_GetCultureInfo1")]
	internal static void* /* System.Globalization.CultureInfo */ System_Globalization_CultureInfo_GetCultureInfo1(byte* /* System.String */ name, byte* /* System.String */ altName, void** /* System.Exception */ __outException)
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
	

	// TODO (Property): CurrentCulture
	

	// TODO (Property): CurrentUICulture
	

	// TODO (Property): InstalledUICulture
	

	// TODO (Property): DefaultThreadCurrentCulture
	

	// TODO (Property): DefaultThreadCurrentUICulture
	

	// TODO (Property): InvariantCulture
	

	// TODO (Property): Parent
	

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
	

	// TODO (Property): NumberFormat
	

	// TODO (Property): DateTimeFormat
	

	// TODO (Property): Calendar
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_CultureInfo_Destroy")]
	internal static void /* System.Void */ System_Globalization_CultureInfo_Destroy(void* /* System.Globalization.CultureInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Globalization_CompareInfo
{
	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetCompareInfo")]
	internal static void* /* System.Globalization.CompareInfo */ System_Globalization_CompareInfo_GetCompareInfo(byte* /* System.String */ name, void* /* System.Reflection.Assembly */ assembly, void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetCompareInfo1")]
	internal static void* /* System.Globalization.CompareInfo */ System_Globalization_CompareInfo_GetCompareInfo1(byte* /* System.String */ name, void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint = "System_Globalization_CompareInfo_GetSortKey")]
	internal static void* /* System.Globalization.SortKey */ System_Globalization_CompareInfo_GetSortKey(void* /* System.Globalization.CompareInfo */ __self, byte* /* System.String */ source, void** /* System.Exception */ __outException)
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
	

	// TODO (Property): Location
	

	// TODO (Property): EscapedCodeBase
	

	// TODO (Property): ManifestModule
	

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
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Create")]
	internal static void* /* System.Version */ System_Version_Create(byte* /* System.String */ version, void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint = "System_Version_Create1")]
	internal static void* /* System.Version */ System_Version_Create1(void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint="System_Version_Destroy")]
	internal static void /* System.Void */ System_Version_Destroy(void* /* System.Version */ __self)
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
	[UnmanagedCallersOnly(EntryPoint = "System_IO_FileStream_Create")]
	internal static void* /* System.IO.FileStream */ System_IO_FileStream_Create(byte* /* System.String */ path, void* /* System.IO.FileStreamOptions */ options, void** /* System.Exception */ __outException)
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
	

	// TODO (Property): SafeFileHandle
	

	// TODO (Property): Name
	

	[UnmanagedCallersOnly(EntryPoint="System_IO_FileStream_Destroy")]
	internal static void /* System.Void */ System_IO_FileStream_Destroy(void* /* System.IO.FileStream */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_IO_Stream
{
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
	

	// TODO (Property): Exception
	

	// TODO (Property): AsyncState
	

	// TODO (Property): Factory
	

	// TODO (Property): CompletedTask
	

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
	

	// TODO (Property): Default
	

	// TODO (Property): Current
	

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
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_MethodBase_Destroy")]
	internal static void /* System.Void */ System_Reflection_MethodBase_Destroy(void* /* System.Reflection.MethodBase */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_MethodBody
{
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
	

	// TODO (Property): FieldType
	

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
	

	// TODO (Property): PropertyType
	

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
	

	// TODO (Property): Member
	

	// TODO (Property): Name
	

	// TODO (Property): ParameterType
	

	// TODO (Property): DefaultValue
	

	// TODO (Property): RawDefaultValue
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_ParameterInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_ParameterInfo_Destroy(void* /* System.Reflection.ParameterInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_ICustomAttributeProvider
{
	[UnmanagedCallersOnly(EntryPoint="System_Reflection_ICustomAttributeProvider_Destroy")]
	internal static void /* System.Void */ System_Reflection_ICustomAttributeProvider_Destroy(void* /* System.Reflection.ICustomAttributeProvider */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Collections_IDictionary
{
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
	

	// TODO (Property): Item
	

	// TODO (Property): Keys
	

	// TODO (Property): Values
	

	[UnmanagedCallersOnly(EntryPoint="System_Collections_IDictionary_Destroy")]
	internal static void /* System.Void */ System_Collections_IDictionary_Destroy(void* /* System.Collections.IDictionary */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Collections_ICollection
{
	// TODO (Property): SyncRoot
	

	[UnmanagedCallersOnly(EntryPoint="System_Collections_ICollection_Destroy")]
	internal static void /* System.Void */ System_Collections_ICollection_Destroy(void* /* System.Collections.ICollection */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Array
{
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
	

	// TODO (Property): SyncRoot
	

	[UnmanagedCallersOnly(EntryPoint="System_Array_Destroy")]
	internal static void /* System.Void */ System_Array_Destroy(void* /* System.Array */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Collections_IComparer
{
	[UnmanagedCallersOnly(EntryPoint="System_Collections_IComparer_Destroy")]
	internal static void /* System.Void */ System_Collections_IComparer_Destroy(void* /* System.Collections.IComparer */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Collections_IEnumerator
{
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
	

	// TODO (Property): FullTypeName
	

	// TODO (Property): AssemblyName
	

	// TODO (Property): ObjectType
	

	[UnmanagedCallersOnly(EntryPoint="System_Runtime_Serialization_SerializationInfo_Destroy")]
	internal static void /* System.Void */ System_Runtime_Serialization_SerializationInfo_Destroy(void* /* System.Runtime.Serialization.SerializationInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Runtime_Serialization_SerializationInfoEnumerator
{
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
	

	// TODO (Property): Scheduler
	

	[UnmanagedCallersOnly(EntryPoint="System_Threading_Tasks_TaskFactory_Destroy")]
	internal static void /* System.Void */ System_Threading_Tasks_TaskFactory_Destroy(void* /* System.Threading.Tasks.TaskFactory */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_IAsyncResult
{
	// TODO (Property): AsyncWaitHandle
	

	// TODO (Property): AsyncState
	

	[UnmanagedCallersOnly(EntryPoint="System_IAsyncResult_Destroy")]
	internal static void /* System.Void */ System_IAsyncResult_Destroy(void* /* System.IAsyncResult */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Threading_WaitHandle
{
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
	

	[UnmanagedCallersOnly(EntryPoint="Microsoft_Win32_SafeHandles_SafeWaitHandle_Destroy")]
	internal static void /* System.Void */ Microsoft_Win32_SafeHandles_SafeWaitHandle_Destroy(void* /* Microsoft.Win32.SafeHandles.SafeWaitHandle */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid
{
	[UnmanagedCallersOnly(EntryPoint="Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid_Destroy")]
	internal static void /* System.Void */ Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid_Destroy(void* /* Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Runtime_InteropServices_SafeHandle
{
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
	internal static void* /* Microsoft.Win32.SafeHandles.SafeFileHandle */ Microsoft_Win32_SafeHandles_SafeFileHandle_Create(void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint="System_IO_FileStreamOptions_Destroy")]
	internal static void /* System.Void */ System_IO_FileStreamOptions_Destroy(void* /* System.IO.FileStreamOptions */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_ManifestResourceInfo
{
	// TODO (Property): ReferencedAssembly
	

	// TODO (Property): FileName
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_ManifestResourceInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_ManifestResourceInfo_Destroy(void* /* System.Reflection.ManifestResourceInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_Module
{
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
	

	// TODO (Property): ScopeName
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_Module_Destroy")]
	internal static void /* System.Void */ System_Reflection_Module_Destroy(void* /* System.Reflection.Module */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Globalization_SortKey
{
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
	internal static byte* /* System.String */ System_Globalization_TextInfo_ToLower(void* /* System.Globalization.TextInfo */ __self, byte* /* System.String */ str, void** /* System.Exception */ __outException)
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
	internal static byte* /* System.String */ System_Globalization_TextInfo_ToUpper(void* /* System.Globalization.TextInfo */ __self, byte* /* System.String */ str, void** /* System.Exception */ __outException)
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
	

	// TODO (Property): CultureName
	

	// TODO (Property): ListSeparator
	

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
	

	// TODO (Property): CurrencyDecimalSeparator
	

	// TODO (Property): CurrencyGroupSeparator
	

	// TODO (Property): CurrencySymbol
	

	// TODO (Property): CurrentInfo
	

	// TODO (Property): NaNSymbol
	

	// TODO (Property): NegativeInfinitySymbol
	

	// TODO (Property): NegativeSign
	

	// TODO (Property): NumberDecimalSeparator
	

	// TODO (Property): NumberGroupSeparator
	

	// TODO (Property): PositiveInfinitySymbol
	

	// TODO (Property): PositiveSign
	

	// TODO (Property): PercentDecimalSeparator
	

	// TODO (Property): PercentGroupSeparator
	

	// TODO (Property): PercentSymbol
	

	// TODO (Property): PerMilleSymbol
	

	[UnmanagedCallersOnly(EntryPoint="System_Globalization_NumberFormatInfo_Destroy")]
	internal static void /* System.Void */ System_Globalization_NumberFormatInfo_Destroy(void* /* System.Globalization.NumberFormatInfo */ __self)
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
	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetEncoding")]
	internal static void* /* System.Text.Encoding */ System_Text_Encoding_GetEncoding(byte* /* System.String */ name, void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint = "System_Text_Encoding_GetEncoding1")]
	internal static void* /* System.Text.Encoding */ System_Text_Encoding_GetEncoding1(byte* /* System.String */ name, void* /* System.Text.EncoderFallback */ encoderFallback, void* /* System.Text.DecoderFallback */ decoderFallback, void** /* System.Exception */ __outException)
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
	

	// TODO (Property): Default
	

	// TODO (Property): BodyName
	

	// TODO (Property): EncodingName
	

	// TODO (Property): HeaderName
	

	// TODO (Property): WebName
	

	// TODO (Property): EncoderFallback
	

	// TODO (Property): DecoderFallback
	

	// TODO (Property): ASCII
	

	// TODO (Property): Latin1
	

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
	internal static void* /* System.Text.Encoding */ System_Text_EncodingProvider_GetEncoding1(void* /* System.Text.EncodingProvider */ __self, byte* /* System.String */ name, void* /* System.Text.EncoderFallback */ encoderFallback, void* /* System.Text.DecoderFallback */ decoderFallback, void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_EncoderFallback_Destroy")]
	internal static void /* System.Void */ System_Text_EncoderFallback_Destroy(void* /* System.Text.EncoderFallback */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_EncoderFallbackBuffer
{
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
	

	[UnmanagedCallersOnly(EntryPoint="System_Text_DecoderFallback_Destroy")]
	internal static void /* System.Void */ System_Text_DecoderFallback_Destroy(void* /* System.Text.DecoderFallback */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_DecoderFallbackBuffer
{
	[UnmanagedCallersOnly(EntryPoint="System_Text_DecoderFallbackBuffer_Destroy")]
	internal static void /* System.Void */ System_Text_DecoderFallbackBuffer_Destroy(void* /* System.Text.DecoderFallbackBuffer */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Text_Decoder
{
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
	[UnmanagedCallersOnly(EntryPoint="System_Runtime_InteropServices_StructLayoutAttribute_Destroy")]
	internal static void /* System.Void */ System_Runtime_InteropServices_StructLayoutAttribute_Destroy(void* /* System.Runtime.InteropServices.StructLayoutAttribute */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Attribute
{
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
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute1(void* /* System.Reflection.ParameterInfo */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute2")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute2(void* /* System.Reflection.Module */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
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
	

	[UnmanagedCallersOnly(EntryPoint = "System_Attribute_GetCustomAttribute3")]
	internal static void* /* System.Attribute */ System_Attribute_GetCustomAttribute3(void* /* System.Reflection.Assembly */ element, void* /* System.Type */ attributeType, void** /* System.Exception */ __outException)
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
	

	// TODO (Property): TypeId
	

	[UnmanagedCallersOnly(EntryPoint="System_Attribute_Destroy")]
	internal static void /* System.Void */ System_Attribute_Destroy(void* /* System.Attribute */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}


internal static unsafe class System_Reflection_ConstructorInfo
{
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
	

	// TODO (Property): AddMethod
	

	// TODO (Property): RemoveMethod
	

	// TODO (Property): RaiseMethod
	

	// TODO (Property): EventHandlerType
	

	[UnmanagedCallersOnly(EntryPoint="System_Reflection_EventInfo_Destroy")]
	internal static void /* System.Void */ System_Reflection_EventInfo_Destroy(void* /* System.Reflection.EventInfo */ __self)
	{
		InteropUtils.FreeIfAllocated(__self);
	}
	

}



// </APIs>
