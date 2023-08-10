using System;

namespace Stravaig.Clock.Testing;

/// <summary>
/// Represents an error that occurs with the time during a test.
/// </summary>
public class TestClockException : Exception
{
    /// <summary>
    /// Initialises a new instance of a TestClockException.
    /// </summary>
    /// <param name="message">The message that describes the error in more detail.</param>
    public TestClockException(string message)
        : base(message)
    {
    }
}