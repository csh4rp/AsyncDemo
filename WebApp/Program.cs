var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/get-async", async () =>
{
    await Task.Delay(100);
    return Results.NoContent();
});

app.MapGet("/get-sync", () =>
{
    Thread.Sleep(100);
    return Results.NoContent();
});

app.MapPost("/set-threadpool", () =>
{
    ThreadPool.SetMinThreads(1000, 1000);
    return Results.NoContent();
});

await app.RunAsync();
