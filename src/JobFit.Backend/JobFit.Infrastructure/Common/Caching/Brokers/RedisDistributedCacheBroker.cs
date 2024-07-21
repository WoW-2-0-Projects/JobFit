using System.Text;
using Force.DeepCloner;
using JobFit.Application.Common.Serializers.Brokers;
using JobFit.Infrastructure.Common.Caching.Settings;
using JobFit.Persistence.Caching.Brokers;
using JobFit.Persistence.Caching.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace JobFit.Infrastructure.Common.Caching.Brokers;

/// <summary>
/// Provides caching functionalities using an redis for distributed cache.
/// </summary>
public class RedisDistributedCacheBroker(
    IOptions<CacheSettings> cacheSettings,
    IDistributedCache distributedCache,
    IJsonSerializationSettingsProvider jsonSerializationSettingsProvider
) : ICacheBroker
{
    private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheSettings.Value.AbsoluteExpirationInSeconds),
        SlidingExpiration = TimeSpan.FromSeconds(cacheSettings.Value.SlidingExpirationInSeconds)
    };

    public async ValueTask<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await distributedCache.GetAsync(key, cancellationToken);
        return value is not null
            ? JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(value), jsonSerializationSettingsProvider.Get())
            : default;
    }

    public bool TryGetAsync<T>(string key, out T? value, CancellationToken cancellationToken = default)
    {
        value = default;

        if (cancellationToken.IsCancellationRequested)
            return false;

        var foundEntry = distributedCache.GetString(key);
        if (foundEntry is not null)
        {
            value = JsonConvert.DeserializeObject<T>(foundEntry, jsonSerializationSettingsProvider.Get());
            return true;
        }

        return false;
    }

    public async ValueTask<T?> GetOrSetAsync<T>(string key, T value, CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedValue is not null) return JsonConvert.DeserializeObject<T>(cachedValue, jsonSerializationSettingsProvider.Get());

        await SetAsync(key, value, entryOptions, cancellationToken);

        return value;
    }

    public async ValueTask<T?> GetOrSetAsync<T>(string key, Func<T> valueProvider, CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await distributedCache.GetStringAsync(key, token: cancellationToken);
        if (cachedValue is not null) return JsonConvert.DeserializeObject<T>(cachedValue, jsonSerializationSettingsProvider.Get());

        var value = valueProvider();
        await SetAsync(key, value, entryOptions, cancellationToken);

        return value;
    }

    public async ValueTask<T?> GetOrSetAsync<T>(string key, Func<ValueTask<T>> valueProvider, CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedValue is not null) return JsonConvert.DeserializeObject<T>(cachedValue, jsonSerializationSettingsProvider.Get());

        var value = await valueProvider();
        await SetAsync(key, value, entryOptions, cancellationToken);

        return value;
    }

    public async ValueTask SetAsync<T>(string key, T value, CacheEntryOptions? entryOptions = default, CancellationToken cancellationToken = default)
    {
        await distributedCache.SetStringAsync(
            key,
            JsonConvert.SerializeObject(value, jsonSerializationSettingsProvider.Get()),
            GetCacheEntryOptions(entryOptions),
            cancellationToken
        );
    }

    public ValueTask SetAsync<T>(string key, Func<T> valueProvider, CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var value = valueProvider();
        distributedCache.SetString(
            key,
            JsonConvert.SerializeObject(value, jsonSerializationSettingsProvider.Get()),
            GetCacheEntryOptions(entryOptions)
        );

        return ValueTask.CompletedTask;
    }

    public async ValueTask SetAsync<T>(string key, Func<ValueTask<T>> valueProvider, CacheEntryOptions? entryOptions = default,
        CancellationToken cancellationToken = default)
    {
        var value = await valueProvider();
        await distributedCache.SetStringAsync(
            key,
            JsonConvert.SerializeObject(value, jsonSerializationSettingsProvider.Get()),
            GetCacheEntryOptions(entryOptions),
            cancellationToken);
    }

    public async ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken);
    }

    /// <summary>
    /// Gets the cache entry options based on given entry options or default options.
    /// </summary>
    /// <param name="entryOptions">Given cache entry options.</param>
    /// <returns>The distributed cache entry options.</returns>
    private DistributedCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? entryOptions)
    {
        if (!entryOptions.HasValue)
            return _distributedCacheEntryOptions;

        var currentEntryOptions = _distributedCacheEntryOptions.DeepClone();

        currentEntryOptions.AbsoluteExpirationRelativeToNow = entryOptions.Value.AbsoluteExpiration;
        currentEntryOptions.SlidingExpiration = entryOptions.Value.SlidingExpiration;

        return currentEntryOptions;
    }
}