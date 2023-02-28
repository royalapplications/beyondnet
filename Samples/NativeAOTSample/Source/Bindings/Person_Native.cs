using System;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOTSample;

internal static unsafe class Person_Native
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
        char* firstName, 
        char* lastName,
        int age
    )
    {
        string? firstNameDn = InteropUtils.ToDotNetString(firstName);

        if (firstNameDn == null) {
            return null;
        }

        string? lastNameDn = InteropUtils.ToDotNetString(lastName);

        if (lastNameDn == null) {
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

        if (instance == null) {
            return (int)CStatus.Failure;
        }

        int age = instance.Age;

        return age;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Age_Set")]
    internal static void Age_Set(void* handleAddress, int age)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance == null) {
            return;
        }

        instance.Age = age;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FirstName_Get")]
    internal static char* FirstName_Get(void* handleAddress)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance == null) {
            return null;
        }

        char* firstNameC = instance.FirstName.ToCString();

        return firstNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FirstName_Set")]
    internal static void FirstName_Set(void* handleAddress, char* firstName)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance == null) {
            return;
        }

        string? firstNameDotNet = InteropUtils.ToDotNetString(firstName);

        if (firstNameDotNet == null) {
            return;
        }

        instance.FirstName = firstNameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "LastName_Get")]
    internal static char* LastName_Get(void* handleAddress)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance == null) {
            return null;
        }

        char* lastNameC = instance.LastName.ToCString();

        return lastNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "LastName_Set")]
    internal static void LastName_Set(void* handleAddress, char* lastName)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance == null) {
            return;
        }

        string? lastNameDotNet = InteropUtils.ToDotNetString(lastName);

        if (lastNameDotNet == null) {
            return;
        }

        instance.LastName = lastNameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FullName_Get")]
    internal static char* FullName_Get(void* handleAddress)
    {
        Person? instance = InteropUtils.GetInstance<Person>(handleAddress);

        if (instance == null) {
            return null;
        }

        char* fullNameC = instance.FullName.ToCString();

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

        if (instance == null) {
            if (outException != null) {
                *outException = null;
            }
            
            return CStatus.Failure;
        }

        try {
            instance.ReduceAge(byYears);

            if (outException != null) {
                *outException = null;
            }
            
            return CStatus.Success;
        } catch (Exception ex) {
            if (outException != null) {
                void* exceptionHandleAddress = ex.AllocateGCHandleAndGetAddress();
                
                *outException = exceptionHandleAddress;
            }
            
            return CStatus.Failure;
        }
    }
    #endregion Public API
}