using System.Runtime.CompilerServices;
using StateMachine;

var asyncTask = Task.Run(async () =>
{
    var sample = new SampleClass();

    var result1 = await sample.Method1Async();
    var result2 = await sample.Method2Async();

    var sum = result1 + result2;
    
    Console.WriteLine($"Async sum: {sum}");
});


var customTask = Task.Run(() =>
{
    var stateMachine = new CustomStateMachine
    {
        State = -1,
        Builder = AsyncTaskMethodBuilder<int>.Create()
    };

    stateMachine.Builder.Start(ref stateMachine);

    var sum = stateMachine.Builder.Task.GetAwaiter().GetResult();
    
    Console.WriteLine($"Custom State Machine sum: {sum}");
});

await Task.WhenAll(customTask, asyncTask);

