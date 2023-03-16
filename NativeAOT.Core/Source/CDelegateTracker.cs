using System.Collections.Concurrent;
using System.Timers;
using Timer = System.Timers.Timer;

namespace NativeAOT.Core;

public sealed class CDelegateTracker: IDisposable
{
    private const double DEFAULT_INTERVAL = 5 // second(s) 
                                            * 1000.0;

    private readonly ConcurrentDictionary<CDelegate, object?> m_cDelegates = new();
    private Timer? m_timer;
    
    private static readonly Lazy<CDelegateTracker> m_shared = new(() => 
        new(DEFAULT_INTERVAL)
    );
    
    public static CDelegateTracker Shared => m_shared.Value;

    private CDelegateTracker(double intervalInMilliseconds)
    {
        Timer timer = new(intervalInMilliseconds);
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
        
        // Console.WriteLine($"A new C Delegate has been added to tracker. The current total number of tracked delegates is {m_cDelegates.Count}.");
    }

    private void Clean()
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
        
        // Console.WriteLine($"C Delegate tracker has been cleaned. The current total number of tracked delegates is {m_cDelegates.Count}.");
    }

    public void Dispose()
    {
        m_timer?.Stop();
        m_timer?.Dispose();
        
        m_timer = null;
        
        Clean();
    }
}