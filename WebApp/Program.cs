var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/get-async", async () =>
{
    await Task.Delay(50);
    
    return Results.NoContent();
});

app.MapGet("/get-sync", () =>
{
    Thread.Sleep(50);
    
    return Results.NoContent();
});

app.MapPost("/set-threadpool", () =>
{
    ThreadPool.SetMinThreads(500, 500);
    return Results.NoContent();
});

await app.RunAsync();
