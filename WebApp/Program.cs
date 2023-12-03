using Microsoft.AspNetCore.Mvc;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<EchoService>(c =>
{
    c.BaseAddress = new Uri("https://postman-echo.com");
    c.Timeout = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

app.MapGet("/get-async", async ([FromServices] EchoService service,
    CancellationToken cancellationToken) =>
{
    await service.SendEchoRequestAsync(cancellationToken);
    
    return Results.NoContent();
});

app.MapGet("/get-sync", ([FromServices] EchoService service,
    CancellationToken cancellationToken) =>
{
    service.SendEchoRequestAsync(cancellationToken).GetAwaiter().GetResult();
    
    return Results.NoContent();
});

app.MapPost("/set-threadpool", () =>
{
    ThreadPool.SetMinThreads(2000, 1000);
    return Results.NoContent();
});

await app.RunAsync();
