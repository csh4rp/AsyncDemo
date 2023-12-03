namespace WebApp;

public class EchoService
{
    private readonly HttpClient _httpClient;

    public EchoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendEchoRequestAsync(CancellationToken cancellationToken)
    {
        using var response = await _httpClient.GetAsync("get", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}