﻿
namespace JobFit.Persistence.Caching.Models;

/// <summary>
/// Represents the options for configuring cache entry behavior.
/// </summary>
public readonly struct CacheEntryOptions(TimeSpan? absoluteExpiration, TimeSpan? slidingExpiration)
{
    /// <summary>
    /// Gets or sets the absolute expiration time relative to the current moment.
    /// </summary>
    public TimeSpan? AbsoluteExpiration { get; } = absoluteExpiration;

    /// <summary>
    /// Gets or sets the sliding expiration time for cached items.
    /// </summary>
    public TimeSpan? SlidingExpiration { get; } = slidingExpiration;
}