using System.Collections.Concurrent;
using System.Timers;
using Timer = System.Timers.Timer;

namespace NativeAOT.Core;

public class CDelegateTracker: IDisposable
{
    private const double INTERVAL = 5 // second(s) 
                                    * 1000.0;

    private readonly ConcurrentDictionary<CDelegate, object?> m_cDelegates = new();
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
        m_cDelegates[cDelegate] = null;
    }

    public void Clean()
    {
        // Console.WriteLine("Cleaning C Delegates...");

        var collectedCDelegates = m_cDelegates.Select(kvp => 
            kvp.Key.HasTrampolineBeenCollected
                ? kvp.Key 
                : null
        );

        HashSet<CDelegate> removedCDelegates = new();

        foreach (var cDelegate in collectedCDelegates) {
            if (cDelegate == null) {
                continue;
            }

            if (m_cDelegates.TryRemove(cDelegate, out _)) {
                removedCDelegates.Add(cDelegate);
            }
        }
        
        foreach (var cDelegate in removedCDelegates) {
            // Console.WriteLine("Announcing that C Delegate is ready to be destroyed");
            
            cDelegate.AnnounceReadyToBeDestroyed();
        }
    }

    public void Dispose()
    {
        m_timer?.Stop();
        m_timer?.Dispose();
        
        m_timer = null;
    }
}