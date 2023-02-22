using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class Person_Native
{
    #region Private Helpers
    private static Person? GetPersonFromHandleAddress(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();
        Person? @object = handle?.Target as Person;

        return @object;
    }
    #endregion Private Helpers

    #region Public API
    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_create")]
    internal static nint Person_Create(nint firstName, nint lastName, int age)
    {
        string? firstNameDn = firstName.ToDotNetString();

        if (firstNameDn == null) {
            return IntPtr.Zero;
        }

        string? lastNameDn = lastName.ToDotNetString();

        if (lastNameDn == null) {
            return IntPtr.Zero;
        }

        Person @object = new(
            firstNameDn,
            lastNameDn,
            age
        );

        GCHandle handle = @object.AllocateGCHandle(GCHandleType.Normal);
        nint handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_destroy")]
    internal static void Person_Destroy(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();

        handle?.FreeIfAllocated();
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_age_get")]
    internal static int Person_Age_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return -1;
        }

        int age = person.Age;

        return age;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_age_set")]
    internal static void Person_Age_Set(nint handleAddress, int age)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return;
        }

        person.Age = age;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_firstname_get")]
    internal static nint Person_FirstName_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return IntPtr.Zero;
        }

        nint firstNameC = person.FirstName.ToCString();

        return firstNameC;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_firstname_set")]
    internal static void Person_FirstName_Set(nint handleAddress, nint firstName)
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

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_lastname_get")]
    internal static nint Person_LastName_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return IntPtr.Zero;
        }

        nint lastNameC = person.LastName.ToCString();

        return lastNameC;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_lastname_set")]
    internal static void Person_LastName_Set(nint handleAddress, nint lastName)
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

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_fullname_get")]
    internal static nint Person_FullName_Get(nint handleAddress)
    {
        Person? person = GetPersonFromHandleAddress(handleAddress);

        if (person == null) {
            return IntPtr.Zero;
        }

        nint fullNameC = person.FullName.ToCString();

        return fullNameC;
    }
    #endregion Public API
}