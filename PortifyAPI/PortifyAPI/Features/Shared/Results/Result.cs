namespace PortifyAPI.Features.Shared.Results;

public readonly struct Result<T> where T : class
{
    public Result()
    {
        Success = false;
    }

    public bool Success { get; init; } = false;
    public T Value { get; init; } = default!;
    public BaseDomainException Error { get; init; } = new InternalServerError("An error occurred");

    public static Result<T> Ok(T value) => new() { Success = true, Value = value };
    public static Result<T> Fail(BaseDomainException error) => new() { Success = false, Error = error };

    public static implicit operator Result<T>(T value) => Ok(value);
    public static implicit operator Result<T>(BaseDomainException error) => Fail(error);
}

public abstract record BaseDomainException(string Message, int StatusCode);

public record NotFoundError(string Message) : BaseDomainException(Message, StatusCodes.Status404NotFound)
{
}

public record InvalidInputError(string Message) : BaseDomainException(Message, StatusCodes.Status400BadRequest)
{
}

public record InternalServerError(string Message) : BaseDomainException(Message, StatusCodes.Status500InternalServerError)
{
}
