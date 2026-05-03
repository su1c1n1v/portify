namespace PortifyAPI.Features.Shared.Cache;

public class CacheSettings
{
    public static string SectionName => "CacheSettings";
    public int DefaultExpirationMinutes { get; set; } = 10;
}