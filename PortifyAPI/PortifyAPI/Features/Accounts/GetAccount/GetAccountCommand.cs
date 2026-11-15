using PortifyAPI.Entities;
using PortifyAPI.Dtos;
using PortifyAPI.Features.Shared.Mapper;
using PortifyAPI.Features.Shared.Mediator;
using PortifyAPI.Features.Shared.Results;
using PortifyAPI.Infrastructure.Trading212;

namespace PortifyAPI.Features.Accounts.GetAccount;

public class GetAccountCommand : IRequest<Result<AccountDto>> { }

public class GetAccountHandler(ITrading212Client trading212Client, ILogger<GetAccountHandler> logger, IMappingFacade mapper) : RequestHandlerBase<GetAccountCommand, Result<AccountDto>>
{
    public override async Task<Result<AccountDto>> Handle(GetAccountCommand request, CancellationToken ct)
    {
        try
        {
            var accounts = await trading212Client.GetAccountsAsync(ct);

            if (accounts == null)
            {
                logger.LogWarning("No accounts found");
                return new NotFoundError("No accounts found");
            }

            var accountDtos = mapper.Map<Account, AccountDto>(accounts);

            logger.LogDebug("Successfully retrieved accounts");
            return Result<AccountDto>.Ok(accountDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving accounts: {Message}", ex.Message);
            return new InternalServerError(ex.Message);
        }
    }
}