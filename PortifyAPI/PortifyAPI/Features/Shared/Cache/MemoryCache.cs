using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace PortifyAPI.Features.Shared.Cache;

public interface ICache
{
    T? Get<T>(string key) where T : class;
    void Set<T>(string key, T value, TimeSpan? absoluteExpirationRelativeToNow = null) where T : class;
}

public class MemoryCache(IMemoryCache cache, ILogger<MemoryCache> logger, IOptions<CacheSettings> cacheSettings) : ICache
{
    public T? Get<T>(string key) where T : class
    {
        if (cache.TryGetValue(key, out T value))
        {
            logger.LogDebug("Cache hit for key: {Key}", key);
            return value;
        }

        logger.LogDebug("Cache miss for key: {Key}", key);
        return null;
    }

    public void Set<T>(string key, T value, TimeSpan? absoluteExpirationRelativeToNow = null) where T : class
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow ?? TimeSpan.FromMinutes(cacheSettings.Value.DefaultExpirationMinutes)
        };

        cache.Set(key, value, options);
        logger.LogDebug("Value cached for key: {Key} with expiration: {Expiration}", key, options.AbsoluteExpirationRelativeToNow);
    }
}
