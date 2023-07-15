using System;

namespace Stravaig.Clock;

/// <summary>
/// The standard implementation that gets the contemporary date time from the
/// system clock.
/// </summary>
public class Clock : IClock
{
    /// <summary>
    /// The only instance of the clock.
    /// </summary>
    public static readonly Clock Instance = new();

    private Clock()
    {
    }
    
    /// <summary>
    /// Gets a DateTime object that is set to the current date and time on this
    /// computer, expressed as the local time.
    /// </summary>
    public DateTime LocalNow => DateTime.Now;

    /// <summary>
    /// Gets a DateTime object that is set to the current date and time on this
    /// computer, expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    public DateTime UtcNow => DateTime.UtcNow;

    /// <summary>
    /// Gets the current date, in the local timezone, with the time component
    /// set to 00:00:00.
    /// </summary>
    public DateTime Today => DateTime.Today;
}