namespace PortifyAPI.Dtos;

public sealed record PositionsSummaryDto
{
    public int TotalStocks { get; init; }
    public decimal TotalCurrentValue { get; init; }
    public decimal UnrealizedTotalProfitLoss { get; init; }
    public IEnumerable<CurrencySummaryDto> Currencies { get; init; } = [];
    public IEnumerable<StockSummaryDto> Stocks { get; init; } = [];
}

public sealed record CurrencySummaryDto
{
    public required string Currency { get; init; }
    public int Quantity { get; init; }
    public decimal Percentage { get; init; }
}

public sealed record StockSummaryDto
{
    public required string Name { get; init; }
    public required string Ticker { get; init; }
    public decimal CurrentValue { get; init; }
    public decimal AveragePricePaid { get; init; }
    public decimal CurrentPrice { get; init; }
    public decimal Quantity { get; init; }
    public required string Currency { get; init; }
    public decimal UnrealizedProfitLoss { get; init; }
    public decimal UnrealizedProfitLossPercentage { get; init; }
    public decimal PortfolioProfitLossPercentage { get; init; }
    public decimal Percentage { get; init; }
    public decimal TotalPositionPercentage { get; init; }
    public string? Pie { get; init; }
}
