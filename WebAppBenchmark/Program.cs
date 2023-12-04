using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Http.CSharp;

var client = new HttpClient();

var simulation = Simulation.Inject(rate: 100,
    interval: TimeSpan.FromMilliseconds(50),
    during: TimeSpan.FromSeconds(30));

var asyncScenario = Scenario.Create("get-async", async _ =>
    {
        using var request = Http.CreateRequest("GET", "http://localhost:5202/get-async");

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(simulation);

var syncScenario = Scenario.Create("get-sync", async _ =>
    {
        using var request = Http.CreateRequest("GET", "http://localhost:5202/get-sync");

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(simulation);

var syncScenarioWithThreadPool = Scenario.Create("get-sync-with-threadpool", async _ =>
    {
        using var request = Http.CreateRequest("GET", "http://localhost:5202/get-sync");

        var response = await Http.Send(client, request);

        return response;
    })
    .WithLoadSimulations(simulation)
    .WithInit(async cnx =>
    {
        using var request = Http.CreateRequest("POST", "http://localhost:5202/set-threadpool");

        _ = await Http.Send(client, request);
    });

Console.WriteLine("Press 's' to start sync scenario,\n'a' to start async scenario,\n't' to start sync scenario with custom threadpool size");

var key = Console.ReadKey();

ScenarioProps props = key.KeyChar switch
{
    's' => syncScenario,
    'a' => asyncScenario,
    _ => syncScenarioWithThreadPool
};

NBomberRunner.RegisterScenarios(props)
    .Run();