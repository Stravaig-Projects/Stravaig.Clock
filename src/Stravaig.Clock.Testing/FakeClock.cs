using System;

namespace Stravaig.Clock.Testing;

/// <summary>
/// A fake clock that exposes a fixed time regardless of how many times it is called.
/// </summary>
public class FakeClock : IClock
{
    /// <summary>
    /// Initialises a new instance of the <see cref="FakeClock"/> class with the
    /// current time.
    /// </summary>
    public FakeClock()
        : this(DateTime.UtcNow)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="FakeClock"/> class with a
    /// specific time.
    /// </summary>
    /// <param name="time">The time to set the FakeClock to.</param>
    public FakeClock(DateTime time)
    {
        SetTime(time);
    }

    /// <summary>
    /// Gets a new FakeClock set to the current time.
    /// </summary>
    public static FakeClock Now => new();

    /// <summary>
    /// Gets the set time in the local timezone.
    /// </summary>
    public DateTime LocalNow { get; private set; }

    /// <summary>
    /// Gets the set time as Universal Coordinated Time (UTC).
    /// </summary>
    public DateTime UtcNow { get; private set; }

    /// <summary>
    /// Gets the Today's date with the time component zeroed out, in the local timezone.
    /// </summary>
    public DateTime Today { get; private set; }

    /// <summary>
    /// Create a new FakeClock set to the given time.
    /// </summary>
    /// <param name="time">The time to set the FakeClock to.</param>
    /// <returns>A new FakeClock with the given time.</returns>
    public static FakeClock SetTo(DateTime time) => new(time);

    /// <summary>
    /// Update the date and time for the fake clock.
    /// </summary>
    /// <param name="dateTime">The date and time to use going forward.</param>
    /// <exception cref="ArgumentException">The given DateTime does not specify the kind.</exception>
    public void SetTime(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Utc)
        {
            UtcNow = dateTime;
            LocalNow = dateTime.ToLocalTime();
        }
        else if (dateTime.Kind == DateTimeKind.Local)
        {
            LocalNow = dateTime;
            UtcNow = dateTime.ToUniversalTime();
        }
        else
        {
            throw new ArgumentException(
                $"The DateTime object must have the Kind property set to either Utc or Local. It is {dateTime.Kind}.",
                nameof(dateTime));
        }

        Today = LocalNow.Date;
    }
}