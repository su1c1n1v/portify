using FastEndpoints;
using PortifyAPI.Features.Accounts.GetAccount;
using PortifyAPI.Features.Pies.GetPieById;
using PortifyAPI.Features.Pies.GetPies;
using PortifyAPI.Features.Positions.GetPositions;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Infrastructure.Trading212;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.Configure<Trading212ClientOptions>(
    builder.Configuration.GetSection(Trading212ClientOptions.SectionName));

builder.Services.AddScoped<IRequestHandlerMarker, GetPositionsHandler>();
builder.Services.AddScoped<IRequestHandlerMarker, GetAccountHandler>();
builder.Services.AddScoped<IRequestHandlerMarker, GetPieByIdHandler>();
builder.Services.AddScoped<IRequestHandlerMarker, GetPiesHandler>();
builder.Services.AddScoped<IRequestHandlerRegistry, RequestHandlerRegistry>();
builder.Services.AddScoped<IMediator, Mediator>();

builder.Services.AddScoped<ITrading212Client, Trading212Client>();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
});


var app = builder.Build();

app.UseFastEndpoints();
app.Run();