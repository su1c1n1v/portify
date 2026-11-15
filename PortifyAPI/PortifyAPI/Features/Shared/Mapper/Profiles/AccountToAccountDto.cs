using PortifyAPI.Dtos;
using PortifyAPI.Entities;

namespace PortifyAPI.Features.Shared.Mapper.Profiles;

public class AccountToAccountDto : IMapper<Account, AccountDto>
{
    public AccountDto Map(Account source) => new(
        source.Id,
        source.Currency,
        source.TotalValue,
        new CashDto(
            source.Cash.AvailableToTrade,
            source.Cash.InPies,
            source.Cash.ReservedForOrders),
        new InvestmentsDto(
            source.Investments.CurrentValue,
            source.Investments.RealizedProfitLoss,
            source.Investments.TotalCost,
            source.Investments.UnrealizedProfitLoss));
}
