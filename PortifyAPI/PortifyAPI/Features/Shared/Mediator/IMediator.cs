namespace PortifyAPI.Features.Shared.Mediator;

public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}

public sealed class Mediator(IRequestHandlerRegistry registry) : IMediator
{
    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
    {
        return registry.Handle(request, ct);
    }
}
