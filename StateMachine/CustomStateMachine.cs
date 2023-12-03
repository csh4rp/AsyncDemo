using System.Runtime.CompilerServices;

namespace StateMachine;

public struct CustomStateMachine : IAsyncStateMachine
{
    private TaskAwaiter<int> _taskAwaiter;
    private SampleClass? _sample;
    private int _firstResult;
    private int _secondResult;
    
    public int State;
    public AsyncTaskMethodBuilder<int> Builder;
    
    public void MoveNext()
    {
        TaskAwaiter<int> firstMethodAwaiter, secondMethodAwaiter;
        
        if (State != 0)
        {
            if (State != 1)
            {
                _sample = new SampleClass();
                firstMethodAwaiter = _sample.Method1Async().GetAwaiter();

                if (!firstMethodAwaiter.IsCompleted)
                {
                    State = 0;
                    _taskAwaiter = firstMethodAwaiter;
                    Builder.AwaitUnsafeOnCompleted(ref firstMethodAwaiter, ref this);
                    return;
                }
            }

            secondMethodAwaiter = _taskAwaiter;
            _taskAwaiter = default;
            State  = -1;
            GetSecondResult(secondMethodAwaiter);
            return;
        }

        firstMethodAwaiter = _taskAwaiter;
        _taskAwaiter = default;
        State = -1;

        _firstResult = firstMethodAwaiter.GetResult();
        secondMethodAwaiter = _sample!.Method2Async().GetAwaiter();

        if (!secondMethodAwaiter.IsCompleted)
        {
            State = 1;
            _taskAwaiter = secondMethodAwaiter;
            Builder.AwaitUnsafeOnCompleted(ref secondMethodAwaiter, ref this);
            return;
        }
        
        GetSecondResult(secondMethodAwaiter);
    }

    private void GetSecondResult(TaskAwaiter<int> secondMethodAwaiter)
    {
        _secondResult = secondMethodAwaiter.GetResult();

        State = -2;
        _sample = null;
        Builder.SetResult(_firstResult + _secondResult);
    }

    public void SetStateMachine(IAsyncStateMachine stateMachine)
    {
        Builder.SetStateMachine(stateMachine);
    }
}