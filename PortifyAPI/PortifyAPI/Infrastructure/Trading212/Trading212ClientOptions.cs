namespace PortifyAPI.Infrastructure.Trading212;

public class Trading212ClientOptions
{
    public static string SectionName => "Trading212ClientOptions";
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Url { get; set; } = null!;
}