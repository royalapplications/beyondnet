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
    internal static unsafe nint Create(char* name)
    {
        string? nameDn = InteropUtils.ToDotNetString(name);

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
    internal static unsafe char* Name_Get(nint handleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return null;
        }

        char* nameC = company.Name.ToCString();

        return nameC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Name_Set")]
    internal static unsafe void Name_Set(nint handleAddress, char* name)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return;
        }

        string? nameDotNet = InteropUtils.ToDotNetString(name);

        if (nameDotNet == null) {
            return;
        }

        company.Name = nameDotNet;
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
    internal static CStatus AddEmployee(nint handleAddress, nint employeeHandleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return CStatus.Failure;
        }

        Person? employee = Person_Native.GetPersonFromHandleAddress(employeeHandleAddress);

        if (employee == null) {
            return CStatus.Failure;
        }

        try {
            company.AddEmployee(employee);
        } catch {
            return CStatus.Failure;
        }

        return CStatus.Success;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "RemoveEmployee")]
    internal static CStatus RemoveEmployee(nint handleAddress, nint employeeHandleAddress)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return CStatus.Failure;
        }

        Person? employee = Person_Native.GetPersonFromHandleAddress(employeeHandleAddress);

        if (employee == null) {
            return CStatus.Failure;
        }

        try {
            company.RemoveEmployee(employee);
        } catch {
            return CStatus.Failure;
        }

        return CStatus.Success;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "ContainsEmployee")]
    internal static CBool ContainsEmployee(nint handleAddress, nint employeeHandleAddress)
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
        nint employeeHandleAddress = employee?.AllocateHandleAndGetAddress() ?? nint.Zero;

        return employeeHandleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "NumberOfEmployeesChanged_Set")]
    internal static unsafe void NumberOfEmployeesChanged_Set(nint handleAddress, delegate* unmanaged<void*> functionPointer)
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return;
        }

        nint functionPointerInt = (nint)functionPointer;

        Company.NumberOfEmployeesChangedDelegate @delegate = Marshal.GetDelegateForFunctionPointer<Company.NumberOfEmployeesChangedDelegate>(functionPointerInt);

        company.NumberOfEmployeesChanged = @delegate;
    }
    #endregion Public API
}