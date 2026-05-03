using PortifyAPI.Entities;
using PortifyAPI.Features.Shared.Mapper.Profiles;

namespace PortifyAPI.Features.Shared.Mapper;

public static class MapperModule
{
    public static IServiceCollection RegisterMappings(this IServiceCollection services)
    {
        services.AddScoped<IMapper, PositionsToPositionsSummary>();
        services.AddScoped<IMapper, PiePositionToPieSummary>();
        services.AddSingleton<IMappingFacade, MappingFacade>();

        return services;
    }
}
