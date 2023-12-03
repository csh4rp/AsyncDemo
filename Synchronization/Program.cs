using Synchronization;

var context = new CustomSynchronizationContext();
var synchronizationTask = Task.Run(context.BindToCurrentThread);
var processor = new ItemProcessor(context);

var factory = new TaskFactory(new test());

await factory.StartNew(() => { });


var producerTask = Task.Run(() =>
{
    var spin = new SpinWait();
    
    while (true)
    {
        processor.Enqueue(Guid.NewGuid().ToString());
        spin.SpinOnce(100);
    }
});


await Task.WhenAll(synchronizationTask, producerTask);


class test : TaskScheduler
{
    protected override IEnumerable<Task>? GetScheduledTasks()
    {
        throw new NotImplementedException();
    }

    protected override void QueueTask(Task task)
    {
        throw new NotImplementedException();
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        throw new NotImplementedException();
    }
}