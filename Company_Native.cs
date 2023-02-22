using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class Company_Native
{
    #region Helpers
    internal static Company? GetCompanyFromHandleAddress(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();
        Company? @object = handle?.Target as Company;

        return @object;
    }

    internal static nint AllocateHandleAndGetAddress(this Company @object)
    {
        GCHandle handle = @object.AllocateGCHandle(GCHandleType.Normal);
        nint handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }
    #endregion Helpers

    #region Public API
    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_create")]
    internal static nint Create(nint name)
    {
        string? nameDn = name.ToDotNetString();

        if (nameDn == null) {
            return nint.Zero;
        }

        Company @object = new(
            nameDn
        );

        nint handleAddress = @object.AllocateHandleAndGetAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_destroy")]
    internal static void Destroy(nint handleAddress)
    {
        GCHandle? handle = handleAddress.ToGCHandle();

        handle?.FreeIfAllocated();
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_name_get")]
    internal static nint Name_Get(nint handleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return nint.Zero;
        }

        nint nameC = company.Name.ToCString();

        return nameC;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_numberofemployees_get")]
    internal static int NumberOfEmployees_Get(nint handleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return (int)CStatus.Failure;
        }

        int numberOfEmployees = company.NumberOfEmployees;

        return numberOfEmployees;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_addemployee")]
    internal static int AddEmployee(nint handleAddress, nint employeeHandleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return (int)CStatus.Failure;
        }

        Person? employee = Person_Native.GetPersonFromHandleAddress(employeeHandleAddress);

        if (employee == null) {
            return (int)CStatus.Failure;
        }

        try {
            company.AddEmployee(employee);
        } catch {
            return (int)CStatus.Failure;
        }

        return (int)CStatus.Success;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_removeemployee")]
    internal static int RemoveEmployee(nint handleAddress, nint employeeHandleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return (int)CStatus.Failure;
        }

        Person? employee = Person_Native.GetPersonFromHandleAddress(employeeHandleAddress);

        if (employee == null) {
            return (int)CStatus.Failure;
        }

        try {
            company.RemoveEmployee(employee);
        } catch {
            return (int)CStatus.Failure;
        }

        return (int)CStatus.Success;
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_containsemployee")]
    internal static int ContainsEmployee(nint handleAddress, nint employeeHandleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return false.ToCBool();
        }

        Person? employee = Person_Native.GetPersonFromHandleAddress(employeeHandleAddress);

        if (employee == null) {
            return false.ToCBool();
        }

        bool contains = company.ContainsEmployee(employee);

        if (contains) {
            return true.ToCBool();
        } else {
            return false.ToCBool();
        }
    }

    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_company_getemployeeatindex")]
    internal static nint GetEmployeeAtIndex(nint handleAddress, int index)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return nint.Zero;
        }

        Person? employee = company.GetEmployeeAtIndex(index);
        nint employeeHandleAddress = employee?.AllocateHandleAndGetAddress() ?? IntPtr.Zero;

        return employeeHandleAddress;
    }
    #endregion Public API
}