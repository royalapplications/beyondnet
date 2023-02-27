using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

internal static unsafe class Company_Native
{
    #region Constants
    private const string NAMESPACE = nameof(NativeAOTLibraryTest);
    private const string CLASS_NAME = nameof(Company);
    private const string FULL_CLASS_NAME = NAMESPACE + "_" + CLASS_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_CLASS_NAME + "_";
    #endregion Constants

    #region Public API
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Create")]
    internal static void* Create(char* name)
    {
        string? nameDn = InteropUtils.ToDotNetString(name);

        if (nameDn == null) {
            return null;
        }

        Company instance = new(
            nameDn
        );

        void* handleAddress = instance.AllocateGCHandleAndGetAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Name_Get")]
    internal static char* Name_Get(void* handleAddress)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance == null) {
            return null;
        }

        char* nameC = instance.Name.ToCString();

        return nameC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Name_Set")]
    internal static void Name_Set(void* handleAddress, char* name)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance == null) {
            return;
        }

        string? nameDotNet = InteropUtils.ToDotNetString(name);

        if (nameDotNet == null) {
            return;
        }

        instance.Name = nameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "NumberOfEmployees_Get")]
    internal static int NumberOfEmployees_Get(void* handleAddress)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance == null) {
            return (int)CStatus.Failure;
        }

        int numberOfEmployees = instance.NumberOfEmployees;

        return numberOfEmployees;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "AddEmployee")]
    internal static CStatus AddEmployee(void* handleAddress, void* employeeHandleAddress)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance == null) {
            return CStatus.Failure;
        }

        Person? employee = InteropUtils.GetInstance<Person>(employeeHandleAddress);

        if (employee == null) {
            return CStatus.Failure;
        }

        try {
            instance.AddEmployee(employee);
        } catch {
            return CStatus.Failure;
        }

        return CStatus.Success;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "RemoveEmployee")]
    internal static CStatus RemoveEmployee(void* handleAddress, void* employeeHandleAddress)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance == null) {
            return CStatus.Failure;
        }

        Person? employee = InteropUtils.GetInstance<Person>(employeeHandleAddress);

        if (employee == null) {
            return CStatus.Failure;
        }

        try {
            instance.RemoveEmployee(employee);
        } catch {
            return CStatus.Failure;
        }

        return CStatus.Success;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "ContainsEmployee")]
    internal static CBool ContainsEmployee(void* handleAddress, void* employeeHandleAddress)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance == null) {
            return false.ToCBool();
        }

        Person? employee = InteropUtils.GetInstance<Person>(employeeHandleAddress);

        if (employee == null) {
            return false.ToCBool();
        }

        bool contains = instance.ContainsEmployee(employee);

        if (contains) {
            return true.ToCBool();
        } else {
            return false.ToCBool();
        }
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "GetEmployeeAtIndex")]
    internal static void* GetEmployeeAtIndex(void* handleAddress, int index)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance == null) {
            return null;
        }

        Person? employee = instance.GetEmployeeAtIndex(index);

        if (employee == null) {
            return null;
        }

        void* employeeHandleAddress = employee.AllocateGCHandleAndGetAddress();

        return employeeHandleAddress;
    }
    
    private class NumberOfEmployeesChangedHandler
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

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "NumberOfEmployeesChanged_Get")]
    internal static CStatus NumberOfEmployeesChanged_Get(
        void* handleAddress,
        void** outContext,
        delegate* unmanaged<void*, void>* outFunctionPointer
    )
    {
        Company? company = InteropUtils.GetInstance<Company>(handleAddress);

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
    internal static void NumberOfEmployeesChanged_Set(
        void* handleAddress,
        void* context,
        delegate* unmanaged<void*, void> functionPointer
    )
    {
        Company? company = InteropUtils.GetInstance<Company>(handleAddress);

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