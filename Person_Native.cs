using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

public static class Person_Native
{
    #region Private Helpers
    private static Person? GetPersonFromHandleAddress(nint personHandleAddress)
    {
        GCHandle? handle = personHandleAddress.ToGCHandle();
        Person? person = handle?.Target as Person;

        return person;
    }
    #endregion Private Helpers

    #region Public API
    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_create")]
    public static nint Person_Create(nint firstName, nint lastName, int age)
    {
        string? firstNameDn = firstName.ToDotNetString();

        if (firstNameDn == null) {
            return IntPtr.Zero;
        }

        string? lastNameDn = lastName.ToDotNetString();

        if (lastNameDn == null) {
            return IntPtr.Zero;
        }

        Person person = new(
            firstNameDn,
            lastNameDn,
            age
        );

        GCHandle handle = person.AllocateGCHandle(GCHandleType.Normal);
        nint handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_destroy")]
    public static void Person_Destroy(nint personHandleAddress)
    {
        GCHandle? personHandle = personHandleAddress.ToGCHandle();

        personHandle?.FreeIfAllocated();
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_age_get")]
    public static int Person_Age_Get(nint personHandleAddress)
    {
        Person? person = GetPersonFromHandleAddress(personHandleAddress);

        if (person == null) {
            return -1;
        }

        int age = person.Age;

        return age;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_age_set")]
    public static void Person_Age_Set(nint personHandleAddress, int age)
    {
        Person? person = GetPersonFromHandleAddress(personHandleAddress);

        if (person == null) {
            return;
        }

        person.Age = age;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_firstname_get")]
    public static nint Person_FirstName_Get(nint personHandleAddress)
    {
        Person? person = GetPersonFromHandleAddress(personHandleAddress);

        if (person == null) {
            return IntPtr.Zero;
        }

        nint firstNameC = person.FirstName.ToCString();

        return firstNameC;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_lastname_get")]
    public static nint Person_LastName_Get(nint personHandleAddress)
    {
        Person? person = GetPersonFromHandleAddress(personHandleAddress);

        if (person == null) {
            return IntPtr.Zero;
        }

        nint lastNameC = person.LastName.ToCString();

        return lastNameC;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_fullname_get")]
    public static nint Person_FullName_Get(nint personHandleAddress)
    {
        Person? person = GetPersonFromHandleAddress(personHandleAddress);

        if (person == null) {
            return IntPtr.Zero;
        }

        nint fullNameC = person.FullName.ToCString();

        return fullNameC;
    }
    #endregion Public API
}