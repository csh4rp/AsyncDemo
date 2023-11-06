using System.Collections.Concurrent;

namespace Synchronization;

public class CustomSynchronizationContext : SynchronizationContext
{
    private readonly ConcurrentQueue<(SendOrPostCallback Callback, object? State)> _callbacks = new();
    
    public override void Post(SendOrPostCallback callback, object? state)
    {
        Console.WriteLine($"Enqueued callback on thread: {Environment.CurrentManagedThreadId}");
        
        _callbacks.Enqueue((callback, state));
    }
    
    public void BindToCurrentThread()
    {
        var spinWait = new SpinWait();
        SetSynchronizationContext(this);
        
        Console.WriteLine($"Synchronization Context Thread: {Environment.CurrentManagedThreadId}");
        
        while (true)
        {
            if (_callbacks.TryDequeue(out var cb))
            {
                cb.Callback.Invoke(cb.State);
                
                Console.WriteLine($"Invoked callback on thread: {Environment.CurrentManagedThreadId}");
            }
            else
            {
                spinWait.SpinOnce(100);
            }
        }
    }
}