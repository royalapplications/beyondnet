using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

public static class Person_Native
{
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

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_person_create")]
    public static nint Person_Create(int age)
    {
        Person person = new(age);

        GCHandle handle = GCHandle.Alloc(person, GCHandleType.Pinned);

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
}