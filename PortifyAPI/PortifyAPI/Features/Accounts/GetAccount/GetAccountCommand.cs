using PortifyAPI.Entities;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Features.Shared.Results;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Accounts.GetAccount;

public class GetAccountCommand : IRequest<Result<Account>> { }

public class GetAccountHandler(ITrading212Client trading212Client, ILogger<GetAccountHandler> logger) : RequestHandlerBase<GetAccountCommand, Result<Account>>
{
    public override async Task<Result<Account>> Handle(GetAccountCommand request, CancellationToken ct)
    {
        try
        {
            var accounts = await trading212Client.GetAccountsAsync(ct);

            if (accounts == null)
            {
                logger.LogWarning("No accounts found");
                return new NotFoundError("No accounts found");
            }

            logger.LogDebug("Successfully retrieved accounts");
            return Result<Account>.Ok(accounts);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving accounts: {Message}", ex.Message);
            return new InternalServerError(ex.Message);
        }
    }
}