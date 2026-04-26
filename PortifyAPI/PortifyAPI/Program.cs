using FastEndpoints;
using PortifyAPI.Features.Positions.GetPositions;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Infrastructure.Trading212;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.Configure<Trading212ClientOptions>(
    builder.Configuration.GetSection(Trading212ClientOptions.SectionName));

builder.Services.AddScoped<IRequestHandlerMarker, GetPositionsHandler>();
builder.Services.AddScoped<IRequestHandlerRegistry, RequestHandlerRegistry>();
builder.Services.AddScoped<IMediator, Mediator>();

builder.Services.AddScoped<ITrading212Client, Trading212Client>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
});


var app = builder.Build();

app.UseFastEndpoints();
app.Run();