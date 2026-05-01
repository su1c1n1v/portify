namespace PortifyAPI.Features.Shared.Mapper;

public interface IMapper
{
}

public interface IMapper<TSource, TDestination> : IMapper
{
    TDestination Map(TSource source);
}
