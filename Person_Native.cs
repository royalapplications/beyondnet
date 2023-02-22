using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

public static class Person_Native
{
    #region Private Helpers
    private static Person? GetPersonFromHandleAddress(nint personHandleAddress)
    {
        GCHandle? handle = GetGCHandleFromPersonHandleAddress(personHandleAddress);
        Person? person = handle?.Target as Person;

        return person;
    }

    private static GCHandle? GetGCHandleFromPersonHandleAddress(nint personHandleAddress)
    {
        if (personHandleAddress == IntPtr.Zero) {
            return null;
        }

        GCHandle handle = GCHandle.FromIntPtr(personHandleAddress);

        return handle;
    }
    #endregion Private Helpers

    #region Public API
    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_create")]
    public static nint Person_Create(nint firstName, nint lastName, int age)
    {
        string? firstNameDn = Marshal.PtrToStringAuto(firstName);

        if (firstNameDn == null) {
            return IntPtr.Zero;
        }

        string? lastNameDn = Marshal.PtrToStringAuto(lastName);

        if (lastNameDn == null) {
            return IntPtr.Zero;
        }

        Person person = new(
            firstNameDn,
            lastNameDn,
            age
        );

        GCHandle handle = GCHandle.Alloc(person, GCHandleType.Normal);

        if (!handle.IsAllocated) {
            return IntPtr.Zero;
        }

        nint handleAddress = GCHandle.ToIntPtr(handle);

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_destroy")]
    public static void Person_Destroy(nint personHandleAddress)
    {
        GCHandle? personHandle = GetGCHandleFromPersonHandleAddress(personHandleAddress);

        if (!(personHandle?.IsAllocated ?? false)) {
            return;
        }

        personHandle?.Free();
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

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_firstname_get")]
    public static nint Person_FirstName_Get(nint personHandleAddress)
    {
        Person? person = GetPersonFromHandleAddress(personHandleAddress);

        if (person == null) {
            return IntPtr.Zero;
        }

        string firstName = person.FirstName;

        nint firstNameC = Marshal.StringToHGlobalAuto(firstName);

        return firstNameC;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_lastname_get")]
    public static nint Person_LastName_Get(nint personHandleAddress)
    {
        Person? person = GetPersonFromHandleAddress(personHandleAddress);

        if (person == null) {
            return IntPtr.Zero;
        }

        string lastName = person.LastName;

        nint lastNameC = Marshal.StringToHGlobalAuto(lastName);

        return lastNameC;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_fullname_get")]
    public static nint Person_FullName_Get(nint personHandleAddress)
    {
        Person? person = GetPersonFromHandleAddress(personHandleAddress);

        if (person == null) {
            return IntPtr.Zero;
        }

        string fullName = person.FullName;

        nint fullNameC = Marshal.StringToHGlobalAuto(fullName);

        return fullNameC;
    }
    #endregion Public API
}