using System.Collections.Concurrent;

namespace Synchronization;

public class ItemProcessor
{
    private readonly ConcurrentQueue<string> _queue = new();
    private readonly Task? _task;
    private readonly SynchronizationContext? _synchronizationContext;
    
    public ItemProcessor(SynchronizationContext synchronizationContext)
    {
        _synchronizationContext = synchronizationContext;
        _task = Task.Factory.StartNew(ProcessAsync, TaskCreationOptions.LongRunning);
    }
    
    public void Enqueue(string item)
    {
        _queue.Enqueue(item);
    }

    private async Task ProcessAsync()
    {
        SynchronizationContext.SetSynchronizationContext(_synchronizationContext);
        var spinWait = new SpinWait();
        
        while (true)
        {
            if (_queue.TryDequeue(out var item))
            {
                await ProcessAsync(item).ConfigureAwait(true);
            }
            else
            {
                spinWait.SpinOnce(100);
            }
        }
    }

    private async Task ProcessAsync(string item)
    {
        Console.WriteLine($"Starting processing item: '{item}' on thread: {Environment.CurrentManagedThreadId}");

        await Task.Run(async () =>
        {
            await Task.Delay(1000);
            Console.WriteLine($"Running processing for item: '{item}' on thread: {Environment.CurrentManagedThreadId}");
        }).ConfigureAwait(true);
        
        Console.WriteLine($"Finished processing item: '{item}' on thread: {Environment.CurrentManagedThreadId}");
    }
}