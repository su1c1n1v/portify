using PortifyAPI.Entities;
using PortifyAPI.Dtos;

namespace PortifyAPI.Features.Shared.Mapper.Profiles;

public class PositionsToPositionsSummary : IMapper<IEnumerable<Position>, PositionsSummaryDto>
{
    private const int summaryDecimalAmount = 2;

    public PositionsSummaryDto Map(IEnumerable<Position> source)
    {
        var totalStocks = source.Count();
        var totalCurrentValue = source.Sum(p => p.WalletImpact.CurrentValue);

        return new PositionsSummaryDto
        {
            TotalCurrentValue = totalCurrentValue,
            TotalStocks = totalStocks,
            UnrealizedTotalProfitLoss = source.Sum(p => p.WalletImpact.UnrealizedProfitLoss),
            Currencies = source
                .GroupBy(p => p.Instrument.Currency)
                .Select(g => new CurrencySummaryDto
                {
                    Currency = g.Key,
                    Quantity = g.Count(),
                    Percentage = Math.Round((decimal)g.Count() / totalStocks * 100, summaryDecimalAmount),
                }).ToArray(),

            Stocks = [.. source
                .Select(g => new StockSummaryDto
                {
                    Name = g.Instrument.Name,
                    Ticker = g.Instrument.Ticker,
                    Quantity = g.Quantity,
                    CurrentPrice = g.CurrentPrice,
                    AveragePricePaid = g.AveragePricePaid,
                    CurrentValue = g.WalletImpact.CurrentValue,
                    Currency = g.Instrument.Currency,
                    UnrealizedProfitLossPercentage = Math.Round(g.WalletImpact.UnrealizedProfitLoss / g.WalletImpact.CurrentValue * 100, summaryDecimalAmount),
                    PortfolioProfitLossPercentage = Math.Round(g.WalletImpact.UnrealizedProfitLoss / source.Sum(p => p.WalletImpact.TotalCost) * 100, summaryDecimalAmount),
                    UnrealizedProfitLoss = g.WalletImpact.UnrealizedProfitLoss,
                    Percentage = Math.Round(g.WalletImpact.CurrentValue / totalCurrentValue * 100, summaryDecimalAmount),
                })]
        };
    }
}
