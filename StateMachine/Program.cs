using System.Runtime.CompilerServices;
using StateMachine;

var stateMachine = new CustomStateMachine
{
    State = -1,
    Builder = AsyncTaskMethodBuilder<long>.Create()
};
        
stateMachine.Builder.Start(ref stateMachine);
        
var sum = stateMachine.Builder.Task.GetAwaiter().GetResult();

// var sample = new SampleClass();
//
// var result1 = await sample.Method1Async();
// var result2 = await sample.Method2Async();

// var sum = result1 + result2;

Console.WriteLine(sum);