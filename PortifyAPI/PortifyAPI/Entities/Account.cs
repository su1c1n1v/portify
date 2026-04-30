namespace PortifyAPI.Entities;

public class Account
{
    public int Id { get; set; }
    public string Currency { get; set; } = null!;
    public decimal TotalValue { get; set; }
    public Cash Cash { get; set; } = null!;
    public Investments Investments { get; set; } = null!;
}

public class Cash
{
    public decimal AvailableToTrade { get; set; }
    public decimal InPies { get; set; }
    public decimal ReservedForOrders { get; set; }
}

public class Investments
{
    public decimal CurrentValue { get; set; }
    public decimal RealizedProfitLoss { get; set; }
    public decimal TotalCost { get; set; }
    public decimal UnrealizedProfitLoss { get; set; }
}


