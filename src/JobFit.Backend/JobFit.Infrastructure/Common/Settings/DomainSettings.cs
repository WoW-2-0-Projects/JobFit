namespace JobFit.Infrastructure.Common.Settings;

/// <summary>
/// Represents core domain business settings
/// </summary>
public sealed record DomainSettings
{
    /// <summary>
    /// Gets application official name
    /// </summary>
    public string AppBusinessName { get; init; } = default!;

    /// <summary>
    /// Gets application domain name
    /// </summary>
    public string DomainName { get; init; } = default!;

    /// <summary>
    /// Gets application domain URL
    /// </summary>
    public string ApiDomainUrl { get; init; } = default!;
}