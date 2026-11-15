using PortifyAPI.Features.Accounts.GetAccount;
using PortifyAPI.Features.Pies.GetPieById;
using PortifyAPI.Features.Pies.GetPies;
using PortifyAPI.Features.Positions.GetPositions;

namespace PortifyAPI.Features.Shared.Mediator;

public static class MediatorModule
{
    public static IServiceCollection RegisterMediator(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandlerMarker, GetPositionsHandler>();
        services.AddScoped<IRequestHandlerMarker, GetAccountHandler>();
        services.AddScoped<IRequestHandlerMarker, GetPieByIdHandler>();
        services.AddScoped<IRequestHandlerMarker, GetPiesHandler>();
        services.AddScoped<IRequestHandlerRegistry, RequestHandlerRegistry>();
        services.AddScoped<IMediator, Mediator>();

        return services;
    }
}