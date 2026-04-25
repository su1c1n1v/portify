using System.Net.Http.Headers;
using System.Text;
using FastEndpoints;
using Microsoft.Extensions.Options;
using PortifyAPI.Infrastructure.Trading212;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddHttpClient<ITrading212Client, Trading212Client>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<Trading212ClientOptions>>().Value;
    var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.Username}:{options.Password}"));

    client.BaseAddress = new Uri(options.Url);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Authorization", options.Username);
});

var app = builder.Build();

app.UseFastEndpoints();
app.Run();