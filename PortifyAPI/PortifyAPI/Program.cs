using FastEndpoints;
using PortifyAPI.Features.Shared.Cache;
using PortifyAPI.Features.Shared.Mapper;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Infrastructure.Trading212;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.Configure<Trading212ClientOptions>(
    builder.Configuration.GetSection(Trading212ClientOptions.SectionName));

builder.Services.Configure<CacheSettings>(
    builder.Configuration.GetSection(CacheSettings.SectionName));

builder.Services.RegisterMediator();
builder.Services.AddScoped<ITrading212Client, Trading212Client>();
builder.Services.AddScoped<ICache, MemoryCache>();

builder.Services.RegisterMappings();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddMemoryCache();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
});


var app = builder.Build();

app.UseFastEndpoints();
app.Run();