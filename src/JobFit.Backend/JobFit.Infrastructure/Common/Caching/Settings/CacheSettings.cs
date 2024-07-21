using JobFit.Persistence.Caching.Models;

namespace JobFit.Infrastructure.Common.Caching.Settings;

/// <summary>
/// Represents the configuration settings for caching in a system.
/// </summary>
public record CacheSettings
{
    /// <summary>
    /// Gets the absolute expiration time for cached items, measured in seconds.
    /// </summary>
    public uint AbsoluteExpirationInSeconds { get; init; }

    /// <summary>
    /// Gets the sliding expiration time for cached items, measured in seconds. 
    /// </summary>
    public uint SlidingExpirationInSeconds { get; init; }

    /// <summary>
    /// Maps the cache settings to cache entry options.
    /// </summary>
    /// <returns>An instance of <see cref="CacheEntryOptions"/></returns>
    public CacheEntryOptions MapToCacheEntryOptions()
    {
        return new CacheEntryOptions(
            AbsoluteExpirationInSeconds != default ? TimeSpan.FromSeconds(AbsoluteExpirationInSeconds) : null,
            SlidingExpirationInSeconds != default ? TimeSpan.FromSeconds(SlidingExpirationInSeconds) : null);
    }
}