namespace PortifyAPI.Dtos;

public sealed record PieDetailsDto(
    IReadOnlyList<InstrumentDetailsDto> Instruments,
    PieSettingsDto Settings);

public sealed record PieSettingsDto(
    string Name,
    decimal? CreationDate,
    string DividendCashAction,
    string? EndDate,
    decimal? Goal,
    string Icon,
    int Id,
    decimal? InitialInvestment,
    IReadOnlyDictionary<string, decimal> InstrumentShares,
    string? PublicUrl);

public sealed record InstrumentDetailsDto(
    decimal CurrentShare,
    decimal ExpectedShare,
    IReadOnlyList<InstrumentIssueDto> Issues,
    decimal OwnedQuantity,
    PieResultDto Result,
    string Ticker);

public sealed record InstrumentIssueDto(string Name, string Severity);

public sealed record PieResultDto(
    decimal PriceAvgInvestedValue,
    decimal PriceAvgResult,
    decimal PriceAvgResultCoef,
    decimal PriceAvgValue);
