namespace PortifyAPI.Features.Shared.Mapper;

public interface IMappingFacade
{
    TDestination Map<TSource, TDestination>(TSource source);
}

public class MappingFacade(IEnumerable<IMapper> mappers, ILogger<IMappingFacade> logger) : IMappingFacade
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        var mapper = mappers
            .OfType<IMapper<TSource, TDestination>>()
            .FirstOrDefault();

        logger.LogDebug("Mapping from {SourceType} to {DestinationType} using {MapperType}",
            typeof(TSource).Name, typeof(TDestination).Name, mapper?.GetType().Name ?? "No Mapper Found");

        if (mapper is null)
        {
            logger.LogError("No mapper found for {SourceType} to {DestinationType}", typeof(TSource).Name, typeof(TDestination).Name);

            throw new InvalidOperationException(
                $"No mapper registered for {typeof(TSource).Name} => {typeof(TDestination).Name}");
        }

        logger.LogDebug("Successfully found mapper for {SourceType} to {DestinationType}: {MapperType}",
            typeof(TSource).Name, typeof(TDestination).Name, mapper.GetType().Name);

        return mapper.Map(source);
    }
}
