using PortifyAPI.Dtos;
using PortifyAPI.Features.Pies.GetPies;

namespace PortifyAPI.Features.Shared.Mapper.Profiles;

internal class PiePositionToPieSummary : IMapper<IEnumerable<PiePosition>, IEnumerable<PieSummaryDto>>
{
    public IEnumerable<PieSummaryDto> Map(IEnumerable<PiePosition> source)
    {
        return source.Select(piePosition =>
        {
            var currentValue = piePosition.Instruments.Sum(i => i.CurrentValue);
            return new PieSummaryDto
            {
                Settings = new SettingSummaryDto
                {
                    Id = piePosition.Settings.Id,
                    Name = piePosition.Settings.Name,
                    CreationDate = piePosition.Settings.CreationDate,
                    CurrentValue = currentValue,
                    Percentage = piePosition.Instruments.Sum(i => i.Percentage),
                    DividendGained = piePosition.DividendDetails.Gained,
                    DividendCashAction = piePosition.Settings.DividendCashAction,
                },
                Stocks = piePosition.Instruments.Select(i => new StockSummaryDto
                {
                    Name = i.Name,
                    Ticker = i.Ticker,
                    CurrentValue = i.CurrentValue,
                    AveragePricePaid = i.AveragePricePaid,
                    CurrentPrice = i.CurrentPrice,
                    Currency = i.Currency,
                    Pie = piePosition.Settings.Name,
                    Quantity = i.Quantity,
                    UnrealizedProfitLoss = i.UnrealizedProfitLoss,
                    UnrealizedProfitLossPercentage = i.UnrealizedProfitLossPercentage,
                    PortfolioProfitLossPercentage = i.PortfolioProfitLossPercentage,
                    Percentage = i.CurrentValue / currentValue * 100,
                    TotalPositionPercentage = i.Percentage,
                }).ToArray()
            };
        }).ToArray();
    }
}