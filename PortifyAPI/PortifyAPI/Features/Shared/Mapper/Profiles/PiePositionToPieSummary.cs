using PortifyAPI.Entities;

namespace PortifyAPI.Features.Shared.Mapper.Profiles;

public class PiePositionToPieSummary : IMapper<IEnumerable<PiePosition>, IEnumerable<PieSummary>>
{
    public IEnumerable<PieSummary> Map(IEnumerable<PiePosition> source)
    {
        return source.Select(piePosition =>
        {
            var currentValue = piePosition.Instruments.Sum(i => i.CurrentValue);
            return new PieSummary
            {
                Settings = new SettingSummary
                {
                    Id = piePosition.Settings.Id,
                    Name = piePosition.Settings.Name,
                    CreationDate = piePosition.Settings.CreationDate,
                    CurrentValue = currentValue,
                    Percentage = piePosition.Instruments.Sum(i => i.Percentage),
                    DividendGained = piePosition.DividendDetails.Gained,
                    DividendCashAction = piePosition.Settings.DividendCashAction,
                },
                Stocks = piePosition.Instruments.Select(i => new StockSummary
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
                })
            };
        });
    }
}