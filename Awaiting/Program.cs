using Awaiting;

var awaitableType = new CustomType();

await awaitableType;

await Task.Run(() => Console.WriteLine("Another task"));

Console.WriteLine("Continuation");