using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PortifyAPI.Entities;
using PortifyAPI.Features.Shared.Cache;

namespace PortifyAPI.Infrastructure.Trading212;

public interface ITrading212Client
{
    Task<IEnumerable<Position>> GetPositionsAsync(CancellationToken ct = default);

    Task<Account> GetAccountsAsync(CancellationToken ct = default);

    Task<IEnumerable<Pie>> GetPiesAsync(CancellationToken ct = default);

    Task<PieDetails> GetPieByIdAsync(int id, CancellationToken ct = default);
}

public class Trading212Client : ITrading212Client
{
    private readonly HttpClient _httpClient;
    private readonly ICache _cache;

    public Trading212Client(IOptions<Trading212ClientOptions> options, ICache cache)
    {
        _cache = cache;

        var encodedCredentials = Encoding.UTF8.GetBytes($"{options.Value.Username}:{options.Value.Password}");
        var base64 = Convert.ToBase64String(encodedCredentials);

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.Value.Url)
        };

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
    }

    public async Task<IEnumerable<Position>> GetPositionsAsync(CancellationToken ct = default)
    {
        var cachedPositions = _cache.Get<List<Position>>("positions");
        if (cachedPositions != null)
        {
            return cachedPositions;
        }

        var response = await _httpClient.GetAsync("equity/positions", ct);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(ct);
        var positions = JsonSerializer.Deserialize<List<Position>>(
            content, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
        _cache.Set("positions", positions);

        return positions;
    }

    public async Task<Account> GetAccountsAsync(CancellationToken ct = default)
    {
        var response = await _httpClient.GetAsync("equity/account/summary", ct);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<Account>(
            content, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new Account();
    }

    public async Task<IEnumerable<Pie>> GetPiesAsync(CancellationToken ct = default)
    {
        var cachedPies = _cache.Get<List<Pie>>("pies");
        if (cachedPies != null)
        {
            return cachedPies;
        }

        var response = await _httpClient.GetAsync("equity/pies", ct);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(ct);
        var pies = JsonSerializer.Deserialize<List<Pie>>(
            content, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Pie>();

        _cache.Set("pies", pies);

        return pies;
    }

    public async Task<PieDetails> GetPieByIdAsync(int id, CancellationToken ct = default)
    {
        var cachedPie = _cache.Get<PieDetails>($"pie_{id}");

        if (cachedPie != null)
        {
            return cachedPie;
        }

        var response = await _httpClient.GetAsync($"equity/pies/{id}", ct);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(ct);
        var pieSummary = JsonSerializer.Deserialize<PieDetails>(
            content, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new PieDetails();

        _cache.Set($"pie_{id}", pieSummary);

        return pieSummary;
    }
}