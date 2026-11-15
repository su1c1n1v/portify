namespace PortifyAPI.Dtos;

public sealed record AccountDto(
    int Id,
    string Currency,
    decimal TotalValue,
    CashDto Cash,
    InvestmentsDto Investments);

public sealed record CashDto(
    decimal AvailableToTrade,
    decimal InPies,
    decimal ReservedForOrders);

public sealed record InvestmentsDto(
    decimal CurrentValue,
    decimal RealizedProfitLoss,
    decimal TotalCost,
    decimal UnrealizedProfitLoss);
