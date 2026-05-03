using PortifyAPI.Entities;

namespace PortifyAPI.Features.Shared.Mapper.Profiles;

public class PositionsToPositionsSummary : IMapper<IEnumerable<Position>, PositionsSummary>
{
    private const int summaryDecimalAmount = 2;

    public PositionsSummary Map(IEnumerable<Position> source)
    {
        var totalStocks = source.Count();
        var totalCurrentValue = source.Sum(p => p.WalletImpact.CurrentValue);

        return new PositionsSummary
        {
            TotalCurrentValue = totalCurrentValue,
            TotalStocks = totalStocks,
            UnrealizedTotalProfitLoss = source.Sum(p => p.WalletImpact.UnrealizedProfitLoss),
            Currencies = source
                .GroupBy(p => p.Instrument.Currency)
                .Select(g => new CurrencySummary
                {
                    Currency = g.Key,
                    Quantity = g.Count(),
                    Percentage = Math.Round((decimal)g.Count() / totalStocks * 100, summaryDecimalAmount),
                }),

            Stocks = source
                .Select(g => new StockSummary
                {
                    Name = g.Instrument.Name,
                    Quantity = g.Quantity,
                    CurrentValue = g.WalletImpact.CurrentValue,
                    Currency = g.Instrument.Currency,
                    UnrealizedProfitLossPercentage = Math.Round(g.WalletImpact.UnrealizedProfitLoss / g.WalletImpact.CurrentValue * 100, summaryDecimalAmount),
                    PortfolioProfitLossPercentage = Math.Round(g.WalletImpact.UnrealizedProfitLoss / source.Sum(p => p.WalletImpact.TotalCost) * 100, summaryDecimalAmount),
                    UnrealizedProfitLoss = g.WalletImpact.UnrealizedProfitLoss,
                    Percentage = Math.Round(g.WalletImpact.CurrentValue / totalCurrentValue * 100, summaryDecimalAmount),
                })
        };
    }
}