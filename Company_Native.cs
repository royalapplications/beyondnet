using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class Company_Native
{
     #region Private Helpers
    private static Company? GetCompanyFromHandleAddress(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();
        Company? @object = handle?.Target as Company;

        return @object;
    }
    #endregion Private Helpers

    #region Public API
    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_create")]
    internal static nint Company_Create(nint name)
    {
        string? nameDn = name.ToDotNetString();

        if (nameDn == null) {
            return IntPtr.Zero;
        }

        Company @object = new(
            nameDn
        );

        GCHandle handle = @object.AllocateGCHandle(GCHandleType.Normal);
        nint handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_destroy")]
    internal static void Company_Destroy(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();

        handle?.FreeIfAllocated();
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_name_get")]
    internal static nint Company_Name_Get(nint handleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return IntPtr.Zero;
        }

        nint nameC = company.Name.ToCString();

        return nameC;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_numberofemployees_get")]
    internal static int Company_NumberOfEmployees_Get(nint handleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return -1;
        }

        int numberOfEmployees = company.NumberOfEmployees;

        return numberOfEmployees;
    }
    #endregion Public API
}