namespace PortifyAPI.Features.Shared.Mediator;

public interface IRequest<TResponse> { }

public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken ct);
}

public interface IRequestHandlerMarker
{
    Type RequestType { get; }
    Task<object> HandleAsync(object request, CancellationToken ct);
}

public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>, IRequestHandlerMarker
    where TRequest : IRequest<TResponse>
{
    public Type RequestType => typeof(TRequest);

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken ct);

    public async Task<object> HandleAsync(object request, CancellationToken ct)
    {
        var result = await Handle((TRequest)request, ct);
        return result!;
    }
}