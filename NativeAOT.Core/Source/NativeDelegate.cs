using System.Runtime.InteropServices;

namespace NativeAOT.Core;

public unsafe class NativeDelegate
{
    private WeakReference<Delegate>? m_weakTrampoline;

    public Delegate? Trampoline
    {
        get {
            var weakTrampoline = m_weakTrampoline;

            if (weakTrampoline == null) {
                return null;
            }
            
            weakTrampoline.TryGetTarget(out Delegate? trampoline);

            return trampoline;
        }
        set {
            m_weakTrampoline = value != null
                ? new(value) 
                : null;
        }
    }
    
    public void* Context { get; }
    public void* NativeFunction { get; }
    public delegate* unmanaged<void*, void>? NativeDestructorFunction { get; }

    public bool HasTrampolineBeenCollected
    {
        get {
            var weakTrampoline = m_weakTrampoline;
            
            if (weakTrampoline == null) {
                return false;
            }

            bool hasTrampoline = weakTrampoline.TryGetTarget(out _);
            bool hasBeenCollected = !hasTrampoline;

            return hasBeenCollected;
        }
    }

    public NativeDelegate(
        void* context,
        void* nativeFunction,
        delegate* unmanaged<void*, void>? nativeDestructorFunction
    )
    {
        Context = context;
        NativeFunction = nativeFunction;
        NativeDestructorFunction = nativeDestructorFunction;
        
        NativeDelegateTracker.Shared.Add(this);
    }

    [UnmanagedCallersOnly(EntryPoint = "NativeAOT_Core_NativeDelegate_Create")]
    public static void* Create(
        void* context,
        void* nativeFunction,
        delegate* unmanaged<void*, void> nativeDestructorFunction
    )
    {
        NativeDelegate self = new(
            context,
            nativeFunction,
            nativeDestructorFunction
        );

        void* handleAddress = self.AllocateGCHandleAndGetAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint = "NativeAOT_Core_NativeDelegate_Destroy")]
    public static void Destroy(void* __self)
    {
        InteropUtils.FreeIfAllocated(__self);
    }

    public void AnnounceReadyToBeDestroyed()
    {
        var destructorFunction = NativeDestructorFunction;

        if (destructorFunction is null) {
            return;
        }
        
        destructorFunction(Context);
    }
}