using JobFit.Domain.Common.Entities;

namespace JobFit.Domain.Common.Commands;

/// <summary>
/// Represents a options to configure command execution and persistence behavior
/// </summary>
public struct CommandOptions() : ICloneable<CommandOptions>
{
    /// <summary>
    /// Gets or sets persistence step behavior of command execution
    /// </summary>
    public bool SkipSavingChanges { get; set; } = false;

    public CommandOptions Clone()
    {
        return new CommandOptions
        {
            SkipSavingChanges = SkipSavingChanges
        };
    }
}