using System;

namespace Stravaig.Clock;

/// <summary>
/// An interface for getting contemporary time information.
/// </summary>
public interface IClock
{
    /// <summary>
    /// Gets a DateTime object that is set to the current date and time on this
    /// computer, expressed as the local time.
    /// </summary>
    DateTime LocalNow { get; }

    /// <summary>
    /// Gets a DateTime object that is set to the current date and time on this
    /// computer, expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    DateTime UtcNow { get; }
    
    /// <summary>
    /// Gets the current date, with the time component set to 00:00:00.
    /// </summary>
    DateTime Today { get; }
}