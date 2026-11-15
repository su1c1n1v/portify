namespace PortifyAPI.Dtos;

public sealed record PieSummaryDto
{
    public required SettingSummaryDto Settings { get; init; }
    public IEnumerable<StockSummaryDto> Stocks { get; init; } = [];
}

public sealed record SettingSummaryDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal? CreationDate { get; init; }
    public decimal CurrentValue { get; init; }
    public decimal Percentage { get; init; }
    public required string DividendCashAction { get; init; }
    public decimal DividendGained { get; init; }
}
