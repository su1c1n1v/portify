namespace PortifyAPI.Infrastructure.Trading212;

public interface ITrading212Client
{
    Task<string> GetPositionsAsync(CancellationToken ct = default);
}

public class Trading212Client(HttpClient httpClient) : ITrading212Client
{
    public async Task<string> GetPositionsAsync(CancellationToken ct = default)
    {
        var response = await httpClient.GetAsync("/equity/positions", ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(ct);
    }
}