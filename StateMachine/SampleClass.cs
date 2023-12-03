namespace StateMachine;

public class SampleClass 
{
    public async Task<int> Method1Async()
    {
        Console.WriteLine($"Method1 Start - Thread: {Environment.CurrentManagedThreadId}");
        
        await Task.Delay(1_000);
        
        Console.WriteLine($"Method1 End - Thread: {Environment.CurrentManagedThreadId}");
        
        return 1;
    }
    
    public async Task<int> Method2Async()
    {
        Console.WriteLine($"Method2 Start - Thread: {Environment.CurrentManagedThreadId}");
        
        await Task.Delay(1_000);
        
        Console.WriteLine($"Method2 End - Thread: {Environment.CurrentManagedThreadId}");
        
        return 2;
    }
}