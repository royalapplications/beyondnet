using System.Runtime.InteropServices;

namespace NativeAOT.Core;

public unsafe class CDelegate
{
    #region Constants
    private const string NAMESPACE = "NativeAOT_Core";
    private const string TYPE_NAME = nameof(CDelegate);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants
    
    private bool m_trampolineHasBeenSet;
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
            if (m_trampolineHasBeenSet) {
                throw new Exception("Trampoline can only be set once");
            }
            
            m_weakTrampoline = value != null
                ? new(value) 
                : null;

            m_trampolineHasBeenSet = true;
            
            CDelegateTracker.Shared.Add(this);
        }
    }
    
    public void* Context { get; }
    public void* CFunction { get; }
    public delegate* unmanaged<void*, void> CDestructorFunction { get; }

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

    public CDelegate(
        void* context,
        void* cFunction,
        delegate* unmanaged<void*, void> cDestructorFunction
    )
    {
        Context = context;
        CFunction = cFunction;
        CDestructorFunction = cDestructorFunction;
    }
    
    public void AnnounceReadyToBeDestroyed()
    {
        var destructorFunction = CDestructorFunction;

        if (destructorFunction is null) {
            return;
        }
        
        destructorFunction(Context);
    }
    
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(CDelegate).AllocateGCHandleAndGetAddress();
    }

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "Create")]
    public static void* Create(
        void* context,
        void* cFunction,
        delegate* unmanaged<void*, void> cDestructorFunction
    )
    {
        CDelegate self = new(
            context,
            cFunction,
            cDestructorFunction
        );

        void* handleAddress = self.AllocateGCHandleAndGetAddress();

        return handleAddress;
    }
}