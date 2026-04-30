namespace PortifyAPI.Entities;

public class Pie
{
    public int Id { get; set; }
    public decimal Cash { get; set; }
    public decimal? Progress { get; set; }
    public DividendDetails DividendDetails { get; set; } = null!;
    public PieResult Result { get; set; } = null!;
    public PieStatus? Status { get; set; }
}

public enum PieStatus
{
    AHEAD,
    ON_TRACK,
    BEHIND
}

public class DividendDetails
{
    public decimal Gained { get; set; }
    public decimal InCash { get; set; }
    public decimal Reinvested { get; set; }
}

public class PieResult
{
    public decimal PriceAvgInvestedValue { get; set; }
    public decimal PriceAvgResult { get; set; }
    public decimal PriceAvgResultCoef { get; set; }
    public decimal PriceAvgValue { get; set; }
}