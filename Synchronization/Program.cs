using Synchronization;

var context = new CustomSynchronizationContext();
var synchronizationTask = Task.Run(context.BindToCurrentThread);
var processor = new ItemProcessor(context);

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


