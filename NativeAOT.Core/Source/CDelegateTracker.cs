using System.Timers;
using Timer = System.Timers.Timer;

namespace NativeAOT.Core;

public class CDelegateTracker: IDisposable
{
    private const double INTERVAL = 1.0;

    private readonly object m_lock = new();
    private List<CDelegate> m_cDelegates = new();
    private Timer? m_timer;

    private static CDelegateTracker? m_shared;
    public static CDelegateTracker Shared
    {
        get {
            CDelegateTracker tracker;

            CDelegateTracker? existingTracker = m_shared;
            
            if (existingTracker == null) {
                tracker = new();
                m_shared = tracker;
            } else {
                tracker = existingTracker;
            }

            return tracker;
        }
    }

    private CDelegateTracker()
    {
        Timer timer = new(INTERVAL);
        timer.Elapsed += Timer_Elapsed;
        
        timer.Start();

        m_timer = timer;
    }

    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Clean();
    }

    public void Add(CDelegate cDelegate)
    {
        lock (m_lock) {
            if (m_cDelegates.Contains(cDelegate)) {
                return;
            }

            m_cDelegates.Add(cDelegate);
        }
    }

    public void Clean()
    {
        lock (m_lock) {
            // Console.WriteLine("Cleaning C Delegates...");

            List<CDelegate> cDelegatesToRemove = new();

            List<CDelegate> cDelegates = m_cDelegates.ToList();

            foreach (var cDelegate in cDelegates) {
                bool hasBeenCollected = cDelegate.HasTrampolineBeenCollected;

                if (hasBeenCollected) {
                    cDelegatesToRemove.Add(cDelegate);
                }
            }

            foreach (var cDelegate in cDelegatesToRemove) {
                cDelegates.Remove(cDelegate);
            }

            m_cDelegates = cDelegates;

            foreach (var cDelegate in cDelegatesToRemove) {
                // Console.WriteLine("Announcing that C Delegate is ready to be destroyed");
                
                cDelegate.AnnounceReadyToBeDestroyed();
            }
        }
    }

    public void Dispose()
    {
        m_timer?.Stop();
        m_timer?.Dispose(); 
        m_timer = null;
    }
}