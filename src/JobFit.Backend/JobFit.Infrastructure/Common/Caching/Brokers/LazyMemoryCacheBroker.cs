using Force.DeepCloner;
using JobFit.Infrastructure.Common.Caching.Settings;
using JobFit.Persistence.Caching.Brokers;
using JobFit.Persistence.Caching.Models;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace JobFit.Infrastructure.Common.Caching.Brokers;

/// <summary>
/// Provides caching functionalities using an in-memory cache.
/// </summary>
public class LazyMemoryCacheBroker(IOptions<CacheSettings> cacheSettings, IAppCache memoryCache) : ICacheBroker
{
    private readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheSettings.Value.AbsoluteExpirationInSeconds),
        SlidingExpiration = TimeSpan.FromSeconds(cacheSettings.Value.SlidingExpirationInSeconds)
    };

    public async ValueTask<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        return await memoryCache.GetAsync<T?>(key);
    }

    public bool TryGetAsync<T>(string key, out T? value, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            value = default;
            return false;
        }
        
        value = default;
        return memoryCache.TryGetValue(key, out value);
    }

    public ValueTask<T?> GetOrSetAsync<T>(
        string key,
        T value,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return ValueTask.FromResult<T?>(memoryCache.GetOrAdd(key, () => value, GetCacheEntryOptions(entryOptions)));
    }

    public ValueTask<T?> GetOrSetAsync<T>(
        string key,
        Func<T> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return new ValueTask<T?>(memoryCache.GetOrAdd(key, valueProvider, GetCacheEntryOptions(entryOptions)));
    }

    public async ValueTask<T?> GetOrSetAsync<T>(
        string key,
        Func<ValueTask<T>> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        return await memoryCache.GetOrAddAsync(key, async () => await valueProvider(), GetCacheEntryOptions(entryOptions));
    }

    public ValueTask SetAsync<T>(string key, T value, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default)
    {
        memoryCache.Add(key, value, GetCacheEntryOptions(entryOptions));
        return ValueTask.CompletedTask;
    }

    public ValueTask SetAsync<T>(
        string key,
        Func<T> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        memoryCache.Add(key, valueProvider(), GetCacheEntryOptions(entryOptions));
        return ValueTask.CompletedTask;
    }

    public async ValueTask SetAsync<T>(
        string key,
        Func<ValueTask<T>> valueProvider,
        CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        memoryCache.Add(key, await valueProvider(), GetCacheEntryOptions(entryOptions));
    }

    public ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        memoryCache.Remove(key);
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Gets the cache entry options based on given entry options or default options.
    /// </summary>
    /// <param name="entryOptions">Given cache entry options.</param>
    /// <returns>The memory cache entry options.</returns>
    private MemoryCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions)
    {
        if (!entryOptions.HasValue)
            return _memoryCacheEntryOptions;

        var currentEntryOptions = _memoryCacheEntryOptions.DeepClone();

        currentEntryOptions.AbsoluteExpirationRelativeToNow = entryOptions.Value.AbsoluteExpiration;
        currentEntryOptions.SlidingExpiration = entryOptions.Value.SlidingExpiration;

        if (!entryOptions.Value.AbsoluteExpiration.HasValue && !entryOptions.Value.SlidingExpiration.HasValue)
            currentEntryOptions.Priority = CacheItemPriority.NeverRemove;

        return currentEntryOptions;
    }
}