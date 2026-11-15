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

