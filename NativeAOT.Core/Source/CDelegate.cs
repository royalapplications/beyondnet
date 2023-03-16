using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace NativeAOT.Core;

// TODO
// public class ChangeAgeNewAgeProviderWrapper
// {
//     private Func<int> Delegate { get; }
//     
//     public Delegate ActualDelegate { get; }
//     
//     public CDelegate CDelegate { get; }
//
//     ~ChangeAgeNewAgeProviderWrapper()
//     {
//         CDelegate.AnnounceReadyToBeDestroyed();
//     }
//
//     ChangeAgeNewAgeProviderWrapper(Func<int> @delegate, CDelegate cDelegate)
//     {
//         System.Delegate del = System.Delegate.CreateDelegate(
//             Delegate.GetType(),
//             this,
//             "Invoke"
//         );
//
//         ActualDelegate = del;
//     }
//     
//     private int Invoke()
//     {
//         return Delegate?.Invoke() ?? 0;
//     }
// }

public sealed unsafe class CDelegate
{
    #region Constants
    private const string NAMESPACE = "NativeAOT_Core";
    private const string TYPE_NAME = nameof(CDelegate);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants
    
    private bool m_trampolineHasBeenSet;
    private bool m_readyForDestructionHasBeenAnnounced;
    private WeakReference<Delegate>? m_weakTrampoline;
    
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

    public bool TryGetTrampoline([NotNullWhen(true)] out Delegate? trampoline)
    {
        if (m_weakTrampoline?.TryGetTarget(out trampoline) ?? false) {
            return true;
        } else {
            trampoline = null;
            
            return false;
        }
    }

    public void SetTrampoline(Delegate trampoline)
    {
        if (m_trampolineHasBeenSet) {
            throw new Exception("Trampoline can only be set once");
        }
            
        m_trampolineHasBeenSet = true;
        
        m_weakTrampoline = new(trampoline);

        CDelegateTracker.Shared.Add(this);
    }
    
    public void AnnounceReadyToBeDestroyed()
    {
        if (m_readyForDestructionHasBeenAnnounced) {
            return;
        }

        m_readyForDestructionHasBeenAnnounced = true;
        
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