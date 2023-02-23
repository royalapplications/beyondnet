using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static class Company_Native
{
    #region Constants
    private const string NAMESPACE = nameof(NativeAOTLibraryTest);
    private const string CLASS_NAME = nameof(Company);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants

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
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Create")]
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

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Name_Get")]
    internal static nint Name_Get(nint handleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return nint.Zero;
        }

        nint nameC = company.Name.ToCString();

        return nameC;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "NumberOfEmployees_Get")]
    internal static int NumberOfEmployees_Get(nint handleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return (int)CStatus.Failure;
        }

        int numberOfEmployees = company.NumberOfEmployees;

        return numberOfEmployees;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "AddEmployee")]
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

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "RemoveEmployee")]
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

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "ContainsEmployee")]
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

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "GetEmployeeAtIndex")]
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