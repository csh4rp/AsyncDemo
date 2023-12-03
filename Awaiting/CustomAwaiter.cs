using System.Runtime.CompilerServices;

namespace Awaiting;

public class CustomAwaiter : INotifyCompletion
{
    public bool IsCompleted => false;
    
    public string GetResult()
    {
        Console.WriteLine("Getting the result");

        return "1";
    }
    
    public void OnCompleted(Action continuation)
    {
        Console.WriteLine("Scheduling completion");

        Console.WriteLine("Running continuation");

        continuation();
            
        Console.WriteLine("Finished continuation");
    }
}