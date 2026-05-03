namespace PortifyAPI.Entities;

public class Position
{
    public decimal AveragePricePaid { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal CurrentPrice { get; set; }
    public Instrument Instrument { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal QuantityAvailableForTrading { get; set; }
    public decimal QuantityInPies { get; set; }
    public WalletImpact WalletImpact { get; set; } = null!;
}

public class Instrument
{
    public string Currency { get; set; } = null!;
    public string Isin { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Ticker { get; set; } = null!;
}

public class WalletImpact
{
    public string Currency { get; set; } = null!;
    public decimal CurrentValue { get; set; }
    public decimal? FxImpact { get; set; }
    public decimal TotalCost { get; set; }
    public decimal UnrealizedProfitLoss { get; set; }
}


public class PositionsSummary
{
    public int TotalStocks { get; set; }
    public decimal TotalCurrentValue { get; set; }
    public decimal UnrealizedTotalProfitLoss { get; set; }
    public IEnumerable<CurrencySummary> Currencies { get; set; } = [];
    public IEnumerable<StockSummary> Stocks { get; set; } = [];
}

public class CurrencySummary
{
    public string Currency { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Percentage { get; set; }
}

public class StockSummary
{
    public string Name { get; set; } = null!;
    public string Ticker { get; set; } = null!;
    public decimal CurrentValue { get; set; }
    public decimal Quantity { get; set; }
    public string Currency { get; set; } = null!;
    public decimal UnrealizedProfitLoss { get; set; }
    public decimal UnrealizedProfitLossPercentage { get; set; }
    public decimal PortfolioProfitLossPercentage { get; set; }
    public decimal Percentage { get; set; }
    public string Pie { get; set; } = null!;
}