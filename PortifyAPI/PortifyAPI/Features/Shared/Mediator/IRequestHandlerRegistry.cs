namespace PortifyAPI.Features.Shared.Mediator;

public interface IRequestHandlerRegistry
{
    Task<TResponse> Handle<TResponse>(IRequest<TResponse> request, CancellationToken ct);
}

public sealed class RequestHandlerRegistry(IEnumerable<IRequestHandlerMarker> handlers) : IRequestHandlerRegistry
{
    public async Task<TResponse> Handle<TResponse>(IRequest<TResponse> request, CancellationToken ct)
    {
        var matchingRequestHandler = handlers
            .FirstOrDefault(temp => temp.RequestType == request.GetType());

        if (matchingRequestHandler != null)
        {
            var result = await matchingRequestHandler.HandleAsync(request, ct);
            return (TResponse)result;
        }

        throw new InvalidOperationException($"No handler registered for request type {request.GetType().Name}");
    }
}