namespace PortifyAPI.Features.Shared.Mediator;

public interface IRequestHandlerRegistry
{
    Task<TResponse> Handle<TResponse>(IRequest<TResponse> request, CancellationToken ct);
}

public sealed class RequestHandlerRegistry(IEnumerable<IRequestHandlerMarker> handlers) : IRequestHandlerRegistry
{
    private readonly Dictionary<Type, IRequestHandlerMarker> handlerMap = handlers.ToDictionary(h => h.RequestType, h => h);

    public async Task<TResponse> Handle<TResponse>(IRequest<TResponse> request, CancellationToken ct)
    {
        if (handlerMap.TryGetValue(request.GetType(), out var handler))
        {
            var result = await handler.HandleAsync(request, ct);
            return (TResponse)result;
        }

        throw new InvalidOperationException($"No handler registered for request type {request.GetType().Name}");
    }
}