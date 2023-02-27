using System.Collections.Concurrent;
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

    #region Variables
    private unsafe class NumberOfEmployeesChangedHandler
    {
        internal Company.NumberOfEmployeesChangedDelegate Trampoline { get; }
        internal void* Context { get; }
        internal delegate* unmanaged<void*, void> FunctionPointer { get; }

        internal NumberOfEmployeesChangedHandler(
            Company.NumberOfEmployeesChangedDelegate trampoline,
            void* context,
            delegate* unmanaged<void*, void> functionPointer
        )
        {
            Trampoline = trampoline;
            Context = context;
            FunctionPointer = functionPointer;
        }
    }
    
    private static readonly ConcurrentDictionary<Company, NumberOfEmployeesChangedHandler> m_numberOfEmployeesChangedHandlers = new();
    #endregion Variables

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

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "NumberOfEmployeesChanged_Get")]
    internal static unsafe CStatus NumberOfEmployeesChanged_Get(
        nint handleAddress,
        void** outContext,
        delegate* unmanaged<void*, void>* outFunctionPointer
    )
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return CStatus.Failure;
        }

        NumberOfEmployeesChangedHandler? handler;

        Company.NumberOfEmployeesChangedDelegate? storedDelegate = company.NumberOfEmployeesChanged;

        if (storedDelegate != null) {
            if (m_numberOfEmployeesChangedHandlers.TryGetValue(company, out NumberOfEmployeesChangedHandler? tempHandler) &&
                tempHandler.Trampoline == storedDelegate) {
                handler = tempHandler;
            } else {
                handler = null;
            }
        } else {
            handler = null;
        }

        if (handler != null) {
            void* context = handler.Context;
            delegate* unmanaged<void*, void> functionPointer = handler.FunctionPointer;

            if (outContext != null) {
                *outContext = context;
            }
            
            if (outFunctionPointer != null) {
                *outFunctionPointer = functionPointer;
            }
        } else {
            if (outContext != null) {
                *outContext = null;
            }
            
            if (outFunctionPointer != null) {
                *outFunctionPointer = null;
            }
        }
        
        return CStatus.Success;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "NumberOfEmployeesChanged_Set")]
    internal static unsafe void NumberOfEmployeesChanged_Set(
        nint handleAddress,
        void* context,
        delegate* unmanaged<void*, void> functionPointer
    )
    {
        Company? company = GetCompanyFromHandleAddress(handleAddress);

        if (company == null) {
            return;
        }

        Company.NumberOfEmployeesChangedDelegate? trampoline;

        if ((nint)functionPointer != nint.Zero) {
            trampoline = () => {
                functionPointer(context);
            };

            m_numberOfEmployeesChangedHandlers[company] = new NumberOfEmployeesChangedHandler(
                trampoline,
                context,
                functionPointer
            );
        } else {
            trampoline = null;

            m_numberOfEmployeesChangedHandlers.TryRemove(company, out _);
        }

        company.NumberOfEmployeesChanged = trampoline;
    }
    #endregion Public API
}