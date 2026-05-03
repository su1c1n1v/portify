namespace PortifyAPI.Entities;

public class PieDetails
{
    public List<InstrumentDetails> Instruments { get; set; } = null!;
    public Settings Settings { get; set; } = null!;
}

public class PiePosition
{
    public List<StockSummary> Instruments { get; set; } = null!;
    public Settings Settings { get; set; } = null!;
}

public class Settings
{
    public string Name { get; set; } = null!;
    public decimal? CreationDate { get; set; }
    public string DividendCashAction { get; set; } = null!;
    public string? EndDate { get; set; }
    public decimal? Goal { get; set; }
    public string Icon { get; set; } = null!;
    public int Id { get; set; }
    public decimal? InitialInvestment { get; set; }
    public Dictionary<string, decimal> InstrumentShares { get; set; } = [];
    public string? PublicUrl { get; set; }
}

public class SettingSummary
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal? CreationDate { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal Percentage { get; set; }
    public string DividendCashAction { get; set; } = null!;
}

public class InstrumentDetails
{
    public decimal CurrentShare { get; set; }
    public decimal ExpectedShare { get; set; }
    public List<Issue> Issues { get; set; } = null!;
    public decimal OwnedQuantity { get; set; }
    public PieResult Result { get; set; } = null!;
    public string Ticker { get; set; } = null!;
}

public class Issue
{
    public string Name { get; set; } = null!;
    public string Severity { get; set; } = null!;
}


public class PieSummary
{
    public SettingSummary Settings { get; set; } = null!;
    public IEnumerable<StockSummary> Stocks { get; set; } = [];
}
