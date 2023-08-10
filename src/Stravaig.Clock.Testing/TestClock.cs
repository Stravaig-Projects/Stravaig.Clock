using System;

namespace Stravaig.Clock.Testing;

/// <summary>
/// Represents a clock that can be used to check the time during a test.
/// </summary>
public class TestClock
{
    private DateTime? _end;

    private TestClock()
    {
        StartTime = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the start time of the clock.
    /// </summary>
    internal DateTime StartTime { get; }

    /// <summary>
    /// Gets the end time of the clock. If Stop() has not been called this will be the current time.
    /// </summary>
    internal DateTime EndTime => _end ?? DateTime.UtcNow;

    /// <summary>
    /// Creates and starts the clock at the beginning of a test.
    /// </summary>
    /// <returns>A new clock for checking times in the test.</returns>
    public static TestClock Start() => new();

    /// <summary>
    /// Stops the clock.
    /// </summary>
    public void Stop()
    {
        _end = DateTime.UtcNow;
    }
}