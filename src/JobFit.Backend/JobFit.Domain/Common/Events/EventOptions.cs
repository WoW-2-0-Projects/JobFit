namespace JobFit.Domain.Common.Events;

/// <summary>
/// Represents event options
/// </summary>
public readonly struct EventOptions()
{
    /// <summary>
    /// Gets event correlation Id
    /// </summary>
    public string CorrelationId { get; init; }

    /// <summary>
    /// Event exchange name
    /// </summary>
    public string Exchange { get; init; } = default!;

    /// <summary>
    /// Gets event routing key for the exchange
    /// </summary>
    public string RoutingKey { get; init; } = default!;
}