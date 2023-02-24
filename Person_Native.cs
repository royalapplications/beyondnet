using System;
using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class Person_Native
{
    #region Constants
    private const string NAMESPACE = nameof(NativeAOTLibraryTest);
    private const string CLASS_NAME = nameof(Person);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants

    #region Helpers
    internal static Person? GetPersonFromHandleAddress(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();
        Person? @object = handle?.Target as Person;

        return @object;
    }

    internal static nint AllocateHandleAndGetAddress(this Person @object)
    {
        GCHandle handle = @object.AllocateGCHandle(GCHandleType.Normal);
        nint handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }
    #endregion Helpers

    #region Public API
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Create")]
    internal static unsafe nint Create(char* firstName, char* lastName, int age)
    {
        string? firstNameDn = InteropUtils.ToDotNetString(firstName);

        if (firstNameDn == null) {
            return nint.Zero;
        }

        string? lastNameDn = InteropUtils.ToDotNetString(lastName);

        if (lastNameDn == null) {
            return nint.Zero;
        }

        Person @object = new(
            firstNameDn,
            lastNameDn,
            age
        );

        nint handleAddress = @object.AllocateHandleAndGetAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Age_Get")]
    internal static int Age_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return (int)CStatus.Failure;
        }

        int age = person.Age;

        return age;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Age_Set")]
    internal static void Age_Set(nint handleAddress, int age)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return;
        }

        person.Age = age;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FirstName_Get")]
    internal static unsafe char* FirstName_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return null;
        }

        char* firstNameC = person.FirstName.ToCString();

        return firstNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FirstName_Set")]
    internal static unsafe void FirstName_Set(nint handleAddress, char* firstName)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return;
        }

        string? firstNameDotNet = InteropUtils.ToDotNetString(firstName);

        if (firstNameDotNet == null) {
            return;
        }

        person.FirstName = firstNameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "LastName_Get")]
    internal static unsafe char* LastName_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return null;
        }

        char* lastNameC = person.LastName.ToCString();

        return lastNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "LastName_Set")]
    internal static unsafe void LastName_Set(nint handleAddress, char* lastName)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return;
        }

        string? lastNameDotNet = InteropUtils.ToDotNetString(lastName);

        if (lastNameDotNet == null) {
            return;
        }

        person.LastName = lastNameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FullName_Get")]
    internal static unsafe char* FullName_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return null;
        }

        char* fullNameC = person.FullName.ToCString();

        return fullNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "ReduceAge")]
    internal static unsafe CStatus ReduceAge(
        nint handleAddress,
        int byYears,
        nint* exception
    )
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            if (exception != null) {
                *exception = nint.Zero;
            }
            
            return CStatus.Failure;
        }

        try {
            person.ReduceAge(byYears);

            if (exception != null) {
                *exception = nint.Zero;
            }
            
            return CStatus.Success;
        } catch (Exception ex) {
            if (exception != null) {
                nint exceptionHandleAddress = System_Exception.Create(ex);
                
                *exception = exceptionHandleAddress;
            }
            
            return CStatus.Failure;
        }
    }
    #endregion Public API
}