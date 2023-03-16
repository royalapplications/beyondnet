using System;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOTSample;

internal static unsafe class NativeAOTSample_Person_t
{
    #region Constants
    private const string NAMESPACE = nameof(NativeAOTSample);
    private const string TYPE_NAME = nameof(Person);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants

    #region Public API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(Person).AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Create")]
    internal static void* Create(
        byte* firstName, 
        byte* lastName,
        int age
    )
    {
        string? firstNameDn = InteropUtils.ToDotNetString(firstName);

        if (firstNameDn is null) {
            return null;
        }

        string? lastNameDn = InteropUtils.ToDotNetString(lastName);

        if (lastNameDn is null) {
            return null;
        }

        Person instance = new(
            firstNameDn,
            lastNameDn,
            age
        );

        void* handleAddress = instance.AllocateGCHandleAndGetAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Age_Get")]
    internal static int Age_Get(void* handleAddress)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance is null) {
            return (int)CStatus.Failure;
        }

        int age = instance.Age;

        return age;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Age_Set")]
    internal static void Age_Set(void* handleAddress, int age)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance is null) {
            return;
        }

        instance.Age = age;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FirstName_Get")]
    internal static byte* FirstName_Get(void* handleAddress)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance is null) {
            return null;
        }

        byte* firstNameC = instance.FirstName.CopyToCString();

        return firstNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FirstName_Set")]
    internal static void FirstName_Set(void* handleAddress, byte* firstName)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance is null) {
            return;
        }

        string? firstNameDotNet = InteropUtils.ToDotNetString(firstName);

        if (firstNameDotNet is null) {
            return;
        }

        instance.FirstName = firstNameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "LastName_Get")]
    internal static byte* LastName_Get(void* handleAddress)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance is null) {
            return null;
        }

        byte* lastNameC = instance.LastName.CopyToCString();

        return lastNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "LastName_Set")]
    internal static void LastName_Set(void* handleAddress, byte* lastName)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance is null) {
            return;
        }

        string? lastNameDotNet = InteropUtils.ToDotNetString(lastName);

        if (lastNameDotNet is null) {
            return;
        }

        instance.LastName = lastNameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FullName_Get")]
    internal static byte* FullName_Get(void* handleAddress)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance is null) {
            return null;
        }

        byte* fullNameC = instance.FullName.CopyToCString();

        return fullNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "ReduceAge")]
    internal static CStatus ReduceAge(
        void* handleAddress,
        int byYears,
        void** outException
    )
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance is null) {
            if (outException is not null) {
                *outException = null;
            }
            
            return CStatus.Failure;
        }

        try {
            instance.ReduceAge(byYears);

            if (outException is not null) {
                *outException = null;
            }
            
            return CStatus.Success;
        } catch (Exception ex) {
            if (outException is not null) {
                void* exceptionHandleAddress = ex.AllocateGCHandleAndGetAddress();
                
                *outException = exceptionHandleAddress;
            }
            
            return CStatus.Failure;
        }
    }
    
    // Sample API for demonstrating non-escaping closures
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "ChangeAgeNew")]
    internal static CStatus ChangeAgeNew(
        void* handleAddress,
        void* nativeDelegateHandleAddress,
        void** outException
    )
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);
        
        if (instance is null) {
            if (outException is not null) {
                *outException = null;
            }
            
            return CStatus.Failure;
        }
        
        Console.WriteLine("ChangeAgeNew 01");
        
        CDelegate? nativeDelegate = InteropUtils.GetInstance<CDelegate>(nativeDelegateHandleAddress);
        
        Console.WriteLine("ChangeAgeNew 02");
        
        if (nativeDelegate == null) {
            if (outException is not null) {
                *outException = null;
            }
            
            return CStatus.Failure;
        }
        
        Console.WriteLine("ChangeAgeNew 03");
        
        void* context = nativeDelegate.Context;
        delegate* unmanaged<void*, int> nativeFunction = (delegate* unmanaged<void*, int>)nativeDelegate.CFunction;
        
        Console.WriteLine("ChangeAgeNew 04");

        Func<int> trampoline = () => {
            Console.WriteLine("ChangeAgeNew (inside trampoline) 01");
            
            Console.WriteLine("ChangeAgeNew (inside trampoline) 02");
        
            int returnValue = nativeFunction(context);
            
            Console.WriteLine("ChangeAgeNew (inside trampoline) 03");
        
            return returnValue;
        };
        
        Console.WriteLine("ChangeAgeNew 05");
        
        nativeDelegate.Trampoline = trampoline;
        
        Console.WriteLine("ChangeAgeNew 06");
        
        instance.ChangeAge(trampoline);
        
        Console.WriteLine("ChangeAgeNew 07");
        
        if (outException is not null) {
            *outException = null;
        }
        
        Console.WriteLine("ChangeAgeNew 08");
            
        return CStatus.Success;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "ChangeAge")]
    internal static CStatus ChangeAge(
        void* handleAddress,
        void* context,
        delegate* unmanaged<void*, int> functionPointer,
        void** outException
    )
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance is null) {
            if (outException is not null) {
                *outException = null;
            }
            
            return CStatus.Failure;
        }

        try {
            Func<int>? trampoline;

            if ((nint)functionPointer != nint.Zero) {
                trampoline = () => functionPointer(context);
            } else {
                trampoline = null;
            }
            
            instance.ChangeAge(trampoline);

            if (outException is not null) {
                *outException = null;
            }
            
            return CStatus.Success;
        } catch (Exception ex) {
            if (outException is not null) {
                void* exceptionHandleAddress = ex.AllocateGCHandleAndGetAddress();
                
                *outException = exceptionHandleAddress;
            }
            
            return CStatus.Failure;
        }
    }
    #endregion Public API
}