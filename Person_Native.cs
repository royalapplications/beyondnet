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
    internal static nint Create(nint firstName, nint lastName, int age)
    {
        string? firstNameDn = firstName.ToDotNetString();

        if (firstNameDn == null) {
            return nint.Zero;
        }

        string? lastNameDn = lastName.ToDotNetString();

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
    internal static nint FirstName_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return nint.Zero;
        }

        nint firstNameC = person.FirstName.ToCString();

        return firstNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FirstName_Set")]
    internal static void FirstName_Set(nint handleAddress, nint firstName)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return;
        }

        string? firstNameDotNet = firstName.ToDotNetString();

        if (firstNameDotNet == null) {
            return;
        }

        person.FirstName = firstNameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "LastName_Get")]
    internal static nint LastName_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return nint.Zero;
        }

        nint lastNameC = person.LastName.ToCString();

        return lastNameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "LastName_Set")]
    internal static void LastName_Set(nint handleAddress, nint lastName)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return;
        }

        string? lastNameDotNet = lastName.ToDotNetString();

        if (lastNameDotNet == null) {
            return;
        }

        person.LastName = lastNameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "FullName_Get")]
    internal static nint FullName_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return nint.Zero;
        }

        nint fullNameC = person.FullName.ToCString();

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