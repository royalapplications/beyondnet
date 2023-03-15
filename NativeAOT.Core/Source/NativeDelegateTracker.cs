using System.Timers;
using Timer = System.Timers.Timer;

namespace NativeAOT.Core;

public class NativeDelegateTracker: IDisposable
{
    private List<NativeDelegate> m_nativeDelegates = new();
    private Timer? m_timer;

    private static NativeDelegateTracker? m_shared;
    public static NativeDelegateTracker Shared
    {
        get {
            NativeDelegateTracker tracker;

            NativeDelegateTracker? existingTracker = m_shared;
            
            if (existingTracker == null) {
                tracker = new();
                m_shared = tracker;
            } else {
                tracker = existingTracker;
            }

            return tracker;
        }
    }

    private NativeDelegateTracker()
    {
        Timer timer = new(1.0);
        timer.Elapsed += Timer_Elapsed;

        m_timer = timer;
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Clean();
    }

    public void Add(NativeDelegate nativeDelegate)
    {
        if (m_nativeDelegates.Contains(nativeDelegate)) {
            return;
        }
        
        m_nativeDelegates.Add(nativeDelegate);
    }

    public void Clean()
    {
        List<NativeDelegate> toRemove = new();

        List<NativeDelegate> nativeDelegates = m_nativeDelegates.ToList();
        
        foreach (var nativeDelegate in nativeDelegates) {
            bool hasBeenCollected = nativeDelegate.HasTrampolineBeenCollected;

            if (hasBeenCollected) {
                toRemove.Add(nativeDelegate);
            }
        }

        foreach (var nativeDelegate in toRemove) {
            nativeDelegates.Remove(nativeDelegate);
        }

        m_nativeDelegates = nativeDelegates;

        foreach (var nativeDelegate in toRemove) {
            nativeDelegate.AnnounceReadyToBeDestroyed();
        }
    }

    public void Dispose()
    {
        m_timer?.Dispose(); 
        m_timer = null;
    }
}