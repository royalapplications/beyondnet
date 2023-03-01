using System.Collections.Concurrent;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOTSample;

internal static unsafe class NativeAOTSample_Company_t
{
    #region Constants
    private const string NAMESPACE = nameof(NativeAOTSample);
    private const string TYPE_NAME = nameof(Company);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants

    #region Public API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(Company).AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Create")]
    internal static void* Create(char* name)
    {
        string? nameDn = InteropUtils.ToDotNetString(name);

        if (nameDn is null) {
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

        if (instance is null) {
            return null;
        }

        char* nameC = instance.Name.CopyToCString();

        return nameC;
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Name_Set")]
    internal static void Name_Set(void* handleAddress, char* name)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance is null) {
            return;
        }

        string? nameDotNet = InteropUtils.ToDotNetString(name);

        if (nameDotNet is null) {
            return;
        }

        instance.Name = nameDotNet;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "NumberOfEmployees_Get")]
    internal static int NumberOfEmployees_Get(void* handleAddress)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance is null) {
            return (int)CStatus.Failure;
        }

        int numberOfEmployees = instance.NumberOfEmployees;

        return numberOfEmployees;
    }

    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "AddEmployee")]
    internal static CStatus AddEmployee(void* handleAddress, void* employeeHandleAddress)
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance is null) {
            return CStatus.Failure;
        }

        Person? employee = InteropUtils.GetInstance<Person>(employeeHandleAddress);

        if (employee is null) {
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

        if (instance is null) {
            return CStatus.Failure;
        }

        Person? employee = InteropUtils.GetInstance<Person>(employeeHandleAddress);

        if (employee is null) {
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

        if (instance is null) {
            return false.ToCBool();
        }

        Person? employee = InteropUtils.GetInstance<Person>(employeeHandleAddress);

        if (employee is null) {
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

        if (instance is null) {
            return null;
        }

        Person? employee = instance.GetEmployeeAtIndex(index);

        if (employee is null) {
            return null;
        }

        void* employeeHandleAddress = employee.AllocateGCHandleAndGetAddress();

        return employeeHandleAddress;
    }

    // Sample API for demonstrating escaping closures
    #region NumberOfEmployeesChanged
    private class NumberOfEmployeesChangedHandler_Native
    {
        internal Company.NumberOfEmployeesChangedDelegate Trampoline { get; }
        internal void* Context { get; }
        internal delegate* unmanaged<void*, void> FunctionPointer { get; }

        internal NumberOfEmployeesChangedHandler_Native(
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
    
    private static readonly ConcurrentDictionary<Company, NumberOfEmployeesChangedHandler_Native> m_numberOfEmployeesChangedNativeHandlers = new();

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "NumberOfEmployeesChanged_Get")]
    internal static CStatus NumberOfEmployeesChanged_Get(
        void* handleAddress,
        void** outContext,
        delegate* unmanaged<void*, void>* outFunctionPointer
    )
    {
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance is null) {
            return CStatus.Failure;
        }

        NumberOfEmployeesChangedHandler_Native? handler;

        Company.NumberOfEmployeesChangedDelegate? storedDelegate = instance.NumberOfEmployeesChanged;

        if (storedDelegate is not null) {
            if (m_numberOfEmployeesChangedNativeHandlers.TryGetValue(instance, out NumberOfEmployeesChangedHandler_Native? tempHandler) &&
                tempHandler.Trampoline == storedDelegate) {
                handler = tempHandler;
            } else {
                handler = null;
            }
        } else {
            handler = null;
        }

        if (handler is not null) {
            void* context = handler.Context;
            delegate* unmanaged<void*, void> functionPointer = handler.FunctionPointer;

            if (outContext is not null) {
                *outContext = context;
            }
            
            if (outFunctionPointer is not null) {
                *outFunctionPointer = functionPointer;
            }
        } else {
            if (outContext is not null) {
                *outContext = null;
            }
            
            if (outFunctionPointer is not null) {
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
        Company? instance = InteropUtils.GetInstance<Company>(handleAddress);

        if (instance is null) {
            return;
        }

        Company.NumberOfEmployeesChangedDelegate? trampoline;

        if ((nint)functionPointer != nint.Zero) {
            trampoline = () => {
                functionPointer(context);
            };

            m_numberOfEmployeesChangedNativeHandlers[instance] = new NumberOfEmployeesChangedHandler_Native(
                trampoline,
                context,
                functionPointer
            );
        } else {
            trampoline = null;

            m_numberOfEmployeesChangedNativeHandlers.TryRemove(instance, out _);
        }

        instance.NumberOfEmployeesChanged = trampoline;
    }
    #endregion NumberOfEmployeesChanged
    #endregion Public API
}