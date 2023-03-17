using System.Reflection;
using System.Runtime.InteropServices;

using NativeAOT.Core;

namespace NativeAOTSample;

internal unsafe class NativeAOTSample_Person_NewAgeProviderDelegate_t
{
    #region Constants
    private const string NAMESPACE = nameof(NativeAOTSample);
    private const string TYPE_NAME = "Person_NewAgeProviderDelegate";
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants

    #region Properties
    public void* Context { get; }
    public delegate* unmanaged<void*, int> CFunction { get; }
    public delegate* unmanaged<void*, void> CDestructorFunction { get; }
    #endregion Properties

    #region Constructor
    private NativeAOTSample_Person_NewAgeProviderDelegate_t
    (
        void* context,
        delegate* unmanaged<void*, int> cFunction,
        delegate* unmanaged<void*, void> cDestructorFunction
    )
    {
        // Console.WriteLine($"{nameof(NativeAOTSample_Person_NewAgeProviderDelegate_t)}: Constructor");
        
        Context = context;
        CFunction = cFunction;
        CDestructorFunction = cDestructorFunction;
    }
    #endregion Constructor

    #region Finalizer
    ~NativeAOTSample_Person_NewAgeProviderDelegate_t()
    {
        // Console.WriteLine($"{nameof(NativeAOTSample_Person_NewAgeProviderDelegate_t)}: Finalizer");
        
        if (CDestructorFunction is null) {
            return;
        }
        
        CDestructorFunction(Context);
    }
    #endregion Finalizer

    #region Delegate Wrapper
    public Person.NewAgeProviderDelegate? CreateTrampoline()
    {
        // Console.WriteLine($"{nameof(NativeAOTSample_Person_NewAgeProviderDelegate_t)}: {nameof(CreateTrampoline)}");

        if (CFunction is null) {
            return null;
        }
        
        Type typeOfSelf = typeof(NativeAOTSample_Person_NewAgeProviderDelegate_t);

        string nameOfInvocationMethod = nameof(__InvokeByCallingCFunction); 
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic; 
        
        MethodInfo? invocationMethod = typeOfSelf.GetMethod(
            nameOfInvocationMethod,
            bindingFlags
        );

        if (invocationMethod is null) {
            throw new Exception("Failed to retrieve delegate invocation method");
        }
        
        Person.NewAgeProviderDelegate trampoline = (Person.NewAgeProviderDelegate)Delegate.CreateDelegate(
            typeof(Person.NewAgeProviderDelegate),
            this,
            invocationMethod
        );

        return trampoline;
    }
    
    private int __InvokeByCallingCFunction()
    {
        // Console.WriteLine($"{nameof(NativeAOTSample_Person_NewAgeProviderDelegate_t)}: {nameof(__InvokeByCallingCFunction)}");
        
        return CFunction(Context);
    }
    #endregion Delegate Wrapper
    
    #region Native API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(Person.NewAgeProviderDelegate).AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "Create")]
    public static void* Create
    (
        void* context,
        delegate* unmanaged<void*, int> cFunction,
        delegate* unmanaged<void*, void> cDestructorFunction
    )
    {
        // Console.WriteLine($"{nameof(NativeAOTSample_Person_NewAgeProviderDelegate_t)}: {nameof(Create)}");
        
        var self = new NativeAOTSample_Person_NewAgeProviderDelegate_t(
            context,
            cFunction,
            cDestructorFunction
        );

        void* selfHandle = self.AllocateGCHandleAndGetAddress();

        return selfHandle;
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "Context_Get")]
    public static void* Context_Get(void* selfHandle)
    {
        // Console.WriteLine($"{nameof(NativeAOTSample_Person_NewAgeProviderDelegate_t)}: {nameof(Context_Get)}");
        
        NativeAOTSample_Person_NewAgeProviderDelegate_t? instance = InteropUtils.GetInstance<NativeAOTSample_Person_NewAgeProviderDelegate_t>(selfHandle);

        if (instance is null) {
            return null;
        }

        return instance.Context;
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "CFunction_Get")]
    public static delegate* unmanaged<void*, int> CFunction_Get(void* selfHandle)
    {
        // Console.WriteLine($"{nameof(NativeAOTSample_Person_NewAgeProviderDelegate_t)}: {nameof(CFunction_Get)}");
        
        NativeAOTSample_Person_NewAgeProviderDelegate_t? instance = InteropUtils.GetInstance<NativeAOTSample_Person_NewAgeProviderDelegate_t>(selfHandle);

        if (instance is null) {
            return null;
        }

        return instance.CFunction;
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "CDestructorFunction_Get")]
    public static delegate* unmanaged<void*, void> CDestructorFunction_Get(void* selfHandle)
    {
        // Console.WriteLine($"{nameof(NativeAOTSample_Person_NewAgeProviderDelegate_t)}: {nameof(CDestructorFunction_Get)}");
        
        NativeAOTSample_Person_NewAgeProviderDelegate_t? instance = InteropUtils.GetInstance<NativeAOTSample_Person_NewAgeProviderDelegate_t>(selfHandle);

        if (instance is null) {
            return null;
        }

        return instance.CDestructorFunction;
    }
    #endregion Native API
}