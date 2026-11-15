using PortifyAPI.Features.Shared.Mapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace PortifyAPI.Features.Shared.Mapper;

public static class MapperModule
{
    public static IServiceCollection RegisterMappings(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IMapper))
            .AddClasses(c => c.AssignableTo<IMapper>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped<IMappingFacade, MappingFacade>();

        return services;
    }
}
